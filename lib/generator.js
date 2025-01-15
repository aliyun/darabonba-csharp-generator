/* eslint-disable complexity */
'use strict';

const assert = require('assert');

const path = require('path');
const fs = require('fs');
const DSL = require('@darabonba/parser');
const xml2js = require('xml2js');
const Entities = require('html-entities').XmlEntities;
const Annotation = require('@darabonba/annotation-parser');
var UUID = require('uuid');
const getBuiltin = require('./builtin');
const { Tag } = DSL.Tag;

// Node.js 16 及以后版本才支持 Object.hasOwn，因此在不支持的环境提供 monkey patch
if (typeof Object.hasOwn !== 'function') {
  Object.hasOwn = function (target, key) {
    return Object.prototype.hasOwnProperty.call(target, key);
  };
}

const REQUEST = 'request_';
const RESPONSE = 'response_';
const RUNTIME = 'runtime_';

const attrList = { 'maxLength': 'value', 'pattern': 'string' };

const {
  _name, _upperFirst, _string, _type, _lowerFirst,
  _avoidReserveName, remove, _vid, parse, render, _format, md2Html,
  _escape, _subModelName, _upperPath, _nameSpace, builtinMap, _isBuiltinModel,
  splitByLastDot, nullableTypes
} = require('./helper');

function md2Xml(str) {
  return _format(md2Html(str));
}
function getAttr(node, attrName) {
  for (let i = 0; i < node.attrs.length; i++) {
    if (_name(node.attrs[i].attrName) === attrName) {
      return node.attrs[i].attrValue.string || node.attrs[i].attrValue.lexeme;
    }
  }
}

function _isBinaryOp(type) {
  const op = [
    'or', 'eq', 'neq',
    'gt', 'gte', 'lt',
    'lte', 'add', 'subtract',
    'div', 'multi', 'and'
  ];
  return op.includes(type);
}

class Visitor {
  constructor(option = {}) {
    this.config = Object.assign({
      outputDir: '',
      indent: '    ',
      clientPath: `${option.className || 'Client'}.cs`
    }, option);
    assert.ok(this.config.outputDir, '`option.outputDir` should not empty');
    assert.ok(this.config.namespace, `Darafile -> csharp -> namespace should not empty, please add csharp option into Darafile.
      example:
        "csharp": {
          "namespace": "NameSpace",
          "className": "Client"
        }`);
    this.used = [];
    this.conflictModelNameMap = [];
    this.typedef = this.config.typedef || {};

    this.namespace = option.namespace;
    this.className = option.className || 'Client';
    this.output = '';
    this.outputDir = this.config.outputDir + '/core';
    this.csprojOutputDir = option.outputDir + '/core';
    this.release = option.releases && option.releases.csharp || this.namespace + ':0.0.1';
    this.config.packageInfo = this.config.packageInfo || {};
    this.isExec = this.config.exec;
    this.asyncOnly = this.config.asyncOnly;
    this.editable = option.editable;
    this.classNamespace = new Map();
    this.releaseVersion = this.config.releaseVersion;
    this.packageManager = this.config.packageManager;

    if (!fs.existsSync(this.outputDir)) {
      fs.mkdirSync(this.outputDir, {
        recursive: true
      });
    }

    remove(path.join(this.outputDir, 'Models/'));
    remove(path.join(this.outputDir, `I${this.className ? this.className : 'Client'}.cs`));
  }

  getAliasName(classNamespace, name, aliasId) {
    let aliasName = '';
    if (!this.clientName.has(name)) {
      this.clientName.set(name, true);
      return aliasName;
    }
    // 别名方式
    if (aliasId) {
      aliasName = aliasId + name;
    }
    // 全写方式
    // aliasName = classNamespace + '.' + name;
    if (aliasName && !this.clientName.has(aliasName)) {
      this.clientName.set(aliasName, true);
      return aliasName;
    }
    const arr = classNamespace.split('.');
    for (let i = arr.length - 1; i >= 0; i--) {
      aliasName = arr[i] + name;
      if (!this.clientName.has(aliasName)) {
        this.clientName.set(aliasName, true);
        return aliasName;
      }
    }
  }

  getInnerClient(aliasId, csPath) {
    const moduleAst = this.ast.innerDep.get(aliasId);
    const beginNotes = DSL.note.getNotes(moduleAst.notes, 0, moduleAst.moduleBody.nodes[0].tokenRange[0]);
    const clientNote = beginNotes.find(note => note.note.lexeme === '@clientName');
    if (clientNote) {
      return _string(clientNote.arg.value);
    }
    const fileInfo = path.parse(csPath);
    return `${_upperFirst(fileInfo.name.toLowerCase())}Client`;
  }

  getAttributes(ast, name) {
    const attr = ast.attrs.find((item) => {
      return item.attrName.lexeme === name;
    });
    if (!attr) {
      return;
    }
    return attr.attrValue.string || attr.attrValue.lexeme || attr.attrValue.value;
  }

  getRealClientName(aliasId) {
    const moduleInfo = this.moduleClass.get(aliasId);
    if (!moduleInfo) {
      return;
    }
    if (moduleInfo.aliasName) {
      const allUsed = [...this.used, `${moduleInfo.namespace}.${moduleInfo.className}`];
      const updateAliasName = (alias) => {
        allUsed.forEach(used => {
          const namespace = used.split('.');
          if (namespace.includes(alias)) {
            alias = '_' + alias;
          }
        });
        return alias;
      };
      moduleInfo.aliasName = updateAliasName(moduleInfo.aliasName);
      this.used.push(`${moduleInfo.aliasName} = ${moduleInfo.namespace}.${moduleInfo.className}`);
      return moduleInfo.aliasName;
    }
    // 同一个命名空间就不用using了，如sdk.dara中导入的api
    this.used.push(`${moduleInfo.namespace}`);
    return moduleInfo.className;
  }

  getRealModelName(namespace, subModelName, type = 'Models') {
    // fullModelName example: Darabonba.Source.Models.M  Darabonba.Test.Model.Models.Info Darabonba.import.Models.Request.submodel
    if (!namespace || namespace === 'Tea') {
      subModelName = _type(subModelName);
    }
    if (namespace !== 'Darabonba.DaraModel') {
      const subModelNameArr = subModelName.split('.');
      subModelName = subModelNameArr.map((m, i) => {
        return _avoidReserveName(m);
      }).join('.');
    }

    if (type === 'Exceptions') {
      subModelName += 'Exception';
    }

    // subModel 的处理：例如fullModelName为 Darabonba.import.Models.Request.submodel的情况，index === 0时将Request作为modelName传入
    // namespace为 Darabonba.import.Models， modelName为 Request.RequestSubmodel
    const existName = this.usedClass.get(subModelName);
    if (existName && existName !== namespace) {
      if (!namespace) {
        return subModelName;
      }
      return `${namespace}.${subModelName}`;
    } else if (this.conflictModelNameMap.includes(subModelName)) {
      return `${namespace}.${subModelName}`;
    }

    if (namespace) {
      this.used.push(`${(this.getType((namespace)))}`);
      this.usedClass.set(subModelName, namespace);
    }
    
    return _type(subModelName);
  }

  getType(name) {
    if (name === '$Request' || name === '$Model' ||
      name === '$Response' || name === '$URL' || name === '$Date' ||
      name === '$Model' || name === '$File') {
      return 'Tea';
    } else if (name === '$Error' || name === '$ResponseError') {
      return 'Darabonba.Exceptions';
    } else if (name === '$SSEEvent' || name === '$RuntimeOptions') {
      return 'Darabonba.Models';
    } else if (name === '$RetryOptions') {
      return 'Darabonba.RetryPolicy';
    } else if (name === '$Stream') {
      return 'Darabonba.Streams';
    } else if (name === '$Bytes' || name === '$Form' || name === '$String' ||
      name === '$XML' || name === '$JSON' || name === '$Number') {
      return 'Darabonba.Utils';
    }
    return name;
  }

  visit(ast, level = 0) {
    this.conflictModels = ast.conflictModels;
    for (const key of this.conflictModels.keys()) {
      const conflicts = key.split(':');
      this.conflictModelNameMap.push(conflicts[1] ? conflicts[1] : conflicts[0]);
    }
    this.classNamespace.set(this.config.clientPath, this.className);
    this.visitModule(ast, this.config.clientPath, true, level);
  }

  saveInnerModule(ast, targetPath) {
    const keys = ast.innerModule.keys();
    let data = keys.next();
    while (!data.done) {
      const aliasId = data.value;
      const moduleAst = ast.innerDep.get(aliasId);
      let filepath = path.join(path.dirname(targetPath), ast.innerModule.get(aliasId));
      filepath = _upperPath(filepath);
      this.visitModule(moduleAst, filepath, false, 0);
      data = keys.next();
    }
  }

  save(filepath) {
    const targetPath = path.join(this.outputDir, filepath);
    fs.mkdirSync(path.dirname(targetPath), {
      recursive: true
    });

    const namespace = this.getClassNamespace(filepath);
    const uniqueUsed = [...new Set(this.used)];
    // 当前命名空间去重
    const filterUsed = uniqueUsed.filter(item => item !== namespace).map(used => `using ${used};`).join('\n');
    const editDesc = this.editable !== true ? `// This file is auto-generated, don't edit it. Thanks.

` : '';
    const content = `${editDesc}${filterUsed}

namespace ${namespace}
{
${this.output}
`;

    fs.writeFileSync(targetPath, content);
    this.output = '';
    this.used = [];
    this.usedClass = new Map();

    if (this.isExec) {
      this.visitConsoleCSProj();
    } else {
      this.visitCSProj();
      this.visitAssemblyInfo();
    }
  }

  getClassNamespace(filePath) {
    if (filePath.startsWith(this.outputDir)) {
      const baseDir = path.join(this.outputDir, 'core', path.sep);
      filePath = filePath.replace(baseDir, '');
    }

    const arr = filePath.split(path.sep).slice(0, -1);
    let className = this.namespace;
    arr.map(key => {
      className += '.' + key;
    });
    return className;
  }

  emit(str, level) {
    this.output += ' '.repeat(level * 4) + str;
  }

  visitModule(ast, filepath, main, level) {
    assert.equal(ast.type, 'module');
    this.ast = ast;
    this.predefined = ast.models;
    this.comments = ast.comments;
    this.builtin = getBuiltin(this);
    this.moduleTypedef = {};
    ast.innerModule = new Map();
    this.moduleClass = new Map();
    this.clientName = new Map();
    this.usedClass = new Map();
    this.fileName;
    this.notes = ast.notes;
    this.usedExternException = ast.usedExternException;

    this.clientName.set(this.className, true);

    this.importBefore(level);
    this.importAfter();

    this.visitAnnotation(ast.annotation, level + 1);

    this.eachImport(ast.imports, ast.usedExternModel, ast.innerModule, filepath, level);

    this.moduleBefore();

    const extendParam = { extend: [] };

    const apis = ast.moduleBody.nodes.filter((item) => {
      return item.type === 'api';
    });
    const models = ast.moduleBody.nodes.filter((item) => {
      return item.type === 'model';
    });
    const functions = ast.moduleBody.nodes.filter((item) => {
      return item.type === 'function';
    });

    extendParam.modelsCount = models.length;
    if (ast.extends) {
      var extendsName = _name(ast.extends);
      let extendClass = `${this.imports[extendsName].namespace}.${this.imports[extendsName].className || 'Client'}`;
      extendParam.extend = [...extendParam.extend, extendClass];
    }

    // Models
    this.visitModels(ast, filepath, level);

    // Exceptions
    this.visitExceptions(ast, filepath, level);

    // interface
    if (this.config.packageInfo.interface) {
      extendParam.extend = [...extendParam.extend, `I${this.className ? this.className : 'Client'}`];
      this.used.push('System');
      this.used.push('System.IO');
      this.used.push('System.Collections');
      this.used.push('System.Collections.Generic');
      this.used.push('System.Threading.Tasks');
      this.used.push('Tea');
      this.used.push('Tea.Utils');
      // this.used.push('Darabonba');
      // this.used.push('Darabonba.Utils');
      this.emit(`public interface I${this.className ? this.className : 'Client'}\n`, level + 1);
      this.emit(`{\n`, level + 1);

      //interface api
      for (let i = 0; i < apis.length; i++) {
        if (i !== 0) {
          this.emit('\n');
        }

        this.interfaceEachAPI(apis[i], level + 2);
      }

      //interface function
      for (let i = 0; i < functions.length; i++) {
        if (functions[i].isStatic) {
          continue;
        }

        if (i !== 0) {
          this.emit('\n');
        }

        this.InterfaceEachFunction(functions[i], level + 2);
      }

      this.interfaceAfter(level + 1);
      this.save(`I${this.className ? this.className : 'Client'}.cs`);
    }

    // Api
    this.apiBefore(level, extendParam, filepath, main);

    const types = ast.moduleBody.nodes.filter((item) => {
      return item.type === 'type';
    });
    const inits = ast.moduleBody.nodes.filter((item) => {
      return item.type === 'init';
    });
    const [init] = inits;
    if (init) {
      this.visitInit(init, types, main, filepath, level);
    }

    for (let i = 0; i < apis.length; i++) {
      if (i !== 0) {
        this.emit('\n');
      }

      this.eachAPI(apis[i], level + 2, {
        isAsyncMode: false
      });

      this.emit('\n');

      this.eachAPI(apis[i], level + 2, {
        isAsyncMode: true
      });
    }

    this.apiAfter();

    this.wrapBefore();

    for (let i = 0; i < functions.length; i++) {
      this.emit('\n');

      // TODO 返回值为asyncIterator时不生成同步方法
      if (!(this.isExec && this.asyncOnly && functions[i].isAsync)) {
        this.eachFunction(functions[i], level + 2, {
          isAsyncMode: false
        });
      }

      // this.eachFunction(functions[i], level + 2, {
      //   isAsyncMode: false
      // });

      if (functions[i].isAsync) {
        this.emit('\n');
        this.eachFunction(functions[i], level + 2, {
          isAsyncMode: true
        });
      }
    }

    this.wrapAfter();

    this.moduleAfter();
    this.save(filepath);
    this.saveInnerModule(ast, filepath);
  }

  emitAnnotation(node, attrList, level) {
    const realName = getAttr(node, 'name') || _name(node.fieldName);
    this.emit(`[NameInMap("${realName}")]\n`, level);
    this.emit('[Validation(Required=', level);
    node.required ? this.emit('true') : this.emit('false');

    for (let i = 0; i < node.attrs.length; i++) {
      const attrValueType = attrList[node.attrs[i].attrName.lexeme];
      if (attrValueType) {
        this.emit(', ');
        var attrName = _upperFirst(node.attrs[i].attrName.lexeme);
        attrName = attrName.split('-').join('');
        if (attrValueType === 'string') {
          this.emit(`${attrName}="${node.attrs[i].attrValue[attrValueType]}"`);
        } else {
          this.emit(`${attrName}=${node.attrs[i].attrValue[attrValueType]}`);
        }
      }
    }

    this.emit(')]\n');

    const deprecated = getAttr(node, 'deprecated');
    if (deprecated === 'true') {
      this.emit(`[Obsolete]\n`, level);
    }
  }

  visitModels(ast, filepath, level) {
    const models = ast.moduleBody.nodes.filter((item) => {
      return item.type === 'model';
    });
    for (let i = 0; i < models.length; i++) {
      const modelName = _upperFirst(models[i].modelName.lexeme);
      this.modelSpace = modelName;
      this.used.push('System');
      this.used.push('System.IO');
      this.used.push('System.Collections');
      this.used.push('System.Collections.Generic');
      // this.used.push('Darabonba');
      this.used.push('Tea');
      this.visitAnnotation(models[i].annotation, level + 1);
      let comments = DSL.comment.getFrontComments(this.comments, models[i].tokenRange[0]);
      this.visitComments(comments, level);
      this.eachModel(models[i], modelName, level + 1, ast.predefined);
      this.modelAfter();
      const modelDir = path.join(path.dirname(filepath), 'Models');
      const modelFilepath = path.join(modelDir, `${modelName}.cs`);
      this.save(modelFilepath);
    }
  }

  visitModelBody(ast, level, env, modelName) {
    assert.equal(ast.type, 'modelBody');
    let node;
    for (let i = 0; i < ast.nodes.length; i++) {
      node = ast.nodes[i];
      let comments = DSL.comment.getFrontComments(this.comments, node.tokenRange[0]);
      this.visitComments(comments, level);
      var paramName = _avoidReserveName(_upperFirst(_name(node.fieldName)));
      if (paramName === modelName) {
        paramName = paramName + '_';
      }
      const description = getAttr(node, 'description');
      const example = getAttr(node, 'example');
      const checkBlank = getAttr(node, 'checkBlank');
      const nullable = getAttr(node, 'nullable');
      const sensitive = getAttr(node, 'sensitive');
      const deprecated = getAttr(node, 'deprecated');
      let hasNextSection = false;
      if (deprecated === 'true') {
        this.emit(`/// <term><b>Obsolete</b></term>\n`, level);
        hasNextSection = true;
      }
      if (description || example || typeof checkBlank !== 'undefined' || typeof nullable !== 'undefined' || typeof sensitive !== 'undefined') {
        if (hasNextSection) {
          this.emit(`/// \n`, level);
        }
        this.emit('/// <summary>\n', level);
        if (description) {
          const descriptions = md2Xml(description).split('\n');
          for (let k = 0; k < descriptions.length; k++) {
            this.emit(`/// ${descriptions[k]}\n`, level);
          }
          hasNextSection = true;
        }
        if (example) {
          if (hasNextSection) {
            this.emit('/// \n', level);
          }
          this.emit('/// <b>Example:</b>\n', level);
          const examples = md2Xml(example).split('\n');
          for (let k = 0; k < examples.length; k++) {
            this.emit(`/// ${examples[k]}\n`, level);
          }
          hasNextSection = true;
        }
        if (typeof checkBlank !== 'undefined') {
          if (hasNextSection) {
            this.emit('/// \n', level);
          }
          this.emit('/// <b>check if is blank:</b>\n', level);
          this.emit(`/// <c>${checkBlank}</c>\n`, level);
          hasNextSection = true;
        }
        if (typeof nullable !== 'undefined') {
          if (hasNextSection) {
            this.emit('/// \n', level);
          }
          this.emit('/// <b>if can be null:</b>\n', level);
          this.emit(`/// <c>${nullable}</c>\n`, level);
          hasNextSection = true;
        }
        if (typeof sensitive !== 'undefined') {
          if (hasNextSection) {
            this.emit('/// \n', level);
          }
          this.emit('/// <b>if is sensitive:</b>\n', level);
          this.emit(`/// <c>${sensitive}</c>\n`, level);
        }
        this.emit('/// </summary>\n', level);
      }
      this.emitAnnotation(node, attrList, level);

      var publicDeclar = 'public ';
      if (node.fieldValue.fieldType === 'array') {
        let subModelName = modelName + _upperFirst(paramName);
        if (node.fieldValue.fieldItemType.nodes !== undefined) {
          this.emit(publicDeclar, level);
          this.emit(`List<${subModelName}>`);
          this.emit(` ${_upperFirst(paramName)} { get; set; }`);
          this.emit('\n');
          this.emit(`public class ${subModelName} : TeaModel {\n`, level);
          this.visitModelBody(node.fieldValue.fieldItemType, level + 1, env, subModelName);
          this.emit('}\n', level);
        } else {
          this.emit(publicDeclar, level);
          this.emit('List<');
          if (node.fieldValue.fieldItemType.fieldType === 'array') {
            let item = JSON.parse(JSON.stringify(node.fieldValue.fieldItemType));
            while (item.fieldType === 'array' && item.nodes === undefined) {
              this.emit('List<');
              item = item.fieldItemType;
            }
            if (item.type === 'modelBody') {
              this.emit(`${subModelName}`);
            } else {
              this.visitFieldType(item, level, modelName, _name(node.fieldName));
            }
            item = JSON.parse(JSON.stringify(node.fieldValue.fieldItemType));
            while (item.fieldType === 'array' && item.nodes === undefined) {
              this.emit('>');
              item = item.fieldItemType;
            }
            this.emit('>');
            this.emit(` ${_upperFirst(paramName)} { get; set; }`);
            this.emit('\n');
            if (item.type === 'modelBody') {
              this.emit(`public class ${subModelName} : TeaModel {\n`, level);
              this.visitModelBody(item, level + 1, env, subModelName);
              this.emit('}\n', level);
            }
          } else {
            this.visitFieldType(node.fieldValue.fieldItemType, level, modelName);
            this.emit('>');
            this.emit(` ${paramName} { get; set; }`);
            this.emit('\n');
          }
        }
      } else if (node.fieldValue.fieldType === 'map') {
        this.emit(publicDeclar, level);
        this.visitFieldType(node.fieldValue, level, modelName, _name(node.fieldValue));
        this.emit(` ${paramName} { get; set; }`);
        this.emit('\n');
      } else if (typeof node.fieldValue.fieldType === 'string') {
        this.emit(publicDeclar, level);
        this.emit(`${_type(node.fieldValue.fieldType)}`);
        this.emit(` ${paramName} { get; set; }`);
        this.emit('\n');
      } else if (node.fieldValue.fieldType) {
        this.emit(publicDeclar, level);
        if (node.fieldValue.fieldType.idType === 'module') {
          const realClsName = this.getRealClientName(`${_name(node.fieldValue.fieldType) || 'Client'}`);
          this.emit(realClsName);
        } else if (node.fieldValue.fieldType.type === 'moduleModel') {
          const [moduleId, ...models] = node.fieldValue.fieldType.path;
          const { namespace } = this.moduleClass.get(moduleId.lexeme);
          // const subModelName = models.map((item) => {
          //   return item.lexeme;
          // }).join('.');
          let subModelName = '';
          let tempName = '';
          models.forEach((item, index) => {
            tempName += _upperFirst(item.lexeme);
            if (index < models.length - 1) {
              subModelName += tempName + '.';
            } else {
              subModelName += tempName;
            }
          });
          const realModelName = this.getRealModelName(`${namespace}.Models`, subModelName);

          this.emit(realModelName);
        } else if (node.fieldValue.idType === 'builtin_model') {
          const [namespace, subModelName] = splitByLastDot(this.getType(node.fieldValue.lexeme));
          const realModelName = this.getRealModelName(namespace, subModelName);
          this.emit(realModelName);
        } else if (node.fieldValue.fieldType.type === 'moduleTypedef') {
          const [moduleId, modelName] = node.fieldValue.fieldType.path;
          const moduleTypedef = this.moduleTypedef[moduleId.lexeme];
          const typedef = moduleTypedef[modelName.lexeme];
          if (!typedef.import) {
            this.emit(typedef.type);
          } else {
            // const typedefName = this.getRealModelName(`${typedef.import}.${typedef.type}`);
            const typedefName = this.getRealModelName(typedef.import, typedef.type);
            this.emit(typedefName);
          }
        } else if (node.fieldValue.fieldType.type === 'typedef' || node.fieldValue.fieldType.idType === 'typedef') {
          const typedef = this.typedef[node.fieldValue.fieldType.lexeme];
          if (!typedef.import) {
            this.emit(typedef.type);
          } else {
            const typedefName = this.getRealModelName(typedef.import, typedef.type);
            this.emit(typedefName);
          }
        } else if (node.fieldValue.fieldType.idType === 'model') {
          // const realModelName = this.getRealModelName(`${this.namespace}.Models.${node.fieldValue.fieldType.lexeme}`);
          const realModelName = this.getRealModelName(`${this.namespace}.Models`, node.fieldValue.fieldType.lexeme);
          this.emit(realModelName);
        } else {
          this.emit(`${_type(node.fieldValue.fieldType.lexeme)}`);
        }
        this.emit(` ${paramName} { get; set; }`);
        this.emit('\n');
      } else {
        const fieldName = _avoidReserveName(_upperFirst(_name(node.fieldName)));
        var subModelName = modelName + _upperFirst(fieldName);
        this.emit(`public ${subModelName} ${_upperFirst(fieldName)} { get; set; }\n`, level);
        this.emit(`public class ${subModelName} : TeaModel {\n`, level);
        this.visitModelBody(node.fieldValue, level + 1, {}, subModelName);
        this.emit('}\n', level);
      }
      this.emit('\n');
    }
    if (node) {
      //find the last node's back comment
      let comments = DSL.comment.getBetweenComments(this.comments, node.tokenRange[0], ast.tokenRange[1]);
      this.visitComments(comments, level);
    }
    if (ast.nodes.length === 0) {
      //empty block's comment
      let comments = DSL.comment.getBetweenComments(this.comments, ast.tokenRange[0], ast.tokenRange[1]);
      this.visitComments(comments, level);
    }

    // this.emit(`public ${modelName} Copy()\n`, level);
    // this.emit('{\n', level);
    // this.visitModelCopy(modelName, level + 1);
    // this.emit('}\n', level);
    // this.emit('\n');

    // this.emit(`public ${modelName} CopyWithoutStream()\n`, level);
    // this.emit('{\n', level);
    // this.visitModelCopyWithoutStream(modelName, level + 1);
    // this.emit('}\n', level);
    // this.emit('\n');

    // Validator中进行validate(反射方式)
    // this.emit('public new void Validate()\n', level);
    // this.emit('{\n', level);
    // this.visitModelValidate(ast, modelName, level + 1);
    // this.emit('}\n', level);
    // this.emit('\n');

    // this.emit('public Dictionary<string, object> ToMap(bool noStream = false)\n', level);
    // this.emit('{\n', level);
    // this.emit('var map = new Dictionary<string, object>();\n', level + 1);
    // for (let i = 0; i < ast.nodes.length; i++) {
    //   const node = ast.nodes[i];
    //   const nameAttr = node.attrs.find((item) => {
    //     return item.attrName.lexeme === 'name';
    //   });
    //   const realName = nameAttr ? _string(nameAttr.attrValue) : _name(node.fieldName);
    //   var fieldName = _avoidReserveName(_upperFirst(_name(node.fieldName)));
    //   if (fieldName === modelName) {
    //     fieldName = fieldName + '_';
    //   }
    //   this.emit(`if (${fieldName} != null)\n`, level + 1);
    //   this.emit(`{\n`, level + 1);
    //   const depth = 1;
    //   this.visitToMapField(node.fieldValue, modelName, fieldName, `map["${realName}"]`, fieldName, depth, level + 1);
    //   this.emit('}\n', level + 1);
    //   this.emit('\n');
    // }
    // this.emit('return map;\n', level + 1);
    // this.emit('}\n', level);
    // this.emit('\n');

    // this.emit(`public static ${modelName} FromMap(Dictionary<string, object> map)\n`, level);
    // this.emit(`{\n`, level);
    // this.emit(`var model = new ${modelName}();\n`, level + 1);
    // for (let i = 0; i < ast.nodes.length; i++) {
    //   const node = ast.nodes[i];
    //   const nameAttr = node.attrs.find((item) => {
    //     return item.attrName.lexeme === 'name';
    //   });
    //   const realName = nameAttr ? _string(nameAttr.attrValue) : _name(node.fieldName);
    //   var fromFieldName = _avoidReserveName(_upperFirst(_name(node.fieldName)));
    //   if (fromFieldName === modelName) {
    //     fromFieldName = modelName + '_';
    //   }
    //   this.emit(`if (map.ContainsKey("${realName}"))\n`, level + 1);
    //   this.emit(`{\n`, level + 1);
    //   this.visitFromField(node.fieldValue, modelName, fromFieldName, `model.${fromFieldName}`, `map["${realName}"]`, level + 1);
    //   this.emit('}\n', level + 1);
    //   this.emit('\n');
    // }
    // this.emit(`return model;\n`, level + 1);
    // this.emit(`}\n`, level);
  }

  visitModelCopy(modelName, level) {
    this.emit(`${modelName} copy = FromMap(ToMap());\n`, level);
    this.emit(`return copy;\n`, level);
  }

  visitModelCopyWithoutStream(modelName, level) {
    this.emit(`${modelName} copy = FromMap(ToMap(true));\n`, level);
    this.emit(`return copy;\n`, level);
  }


  visitFieldValidate(modelName, value, level, name) {
    if (value.type === 'array' || value.fieldType === 'array') {
      this.emit(`if (${name} is IList) {\n`, level);
      this.emit(`${modelName}.ValidateArray(${name});\n`, level + 1);
      this.emit('}\n', level);
    } else if (value.fieldType === 'map' || value.type === 'map') {
      this.emit(`if (${name} is IDictionary) {\n`, level);
      this.emit(`${modelName}.ValidateMap(${name});\n`, level + 1);
      this.emit('}\n', level);
    } else if (value.type === 'moduleModel' || value.type === 'modelBody'
      || value.type === 'subModel' || value.fieldType.type === 'moduleModel'
      || value.fieldType.idType === 'model' || value.fieldType.idType === 'module') {
      this.emit(`if (${name} != null) {\n`, level);
      this.emit(`${name}.Validate();\n`, level + 1);
      this.emit('}\n', level);
    }
  }

  // TODO 是为了不用反射？老的核心库中用的是反射的方式
  visitModelValidate(ast, modelName, level) {
    const baseModel = 'TeaModel';
    for (let i = 0; i < ast.nodes.length; i++) {
      const node = ast.nodes[i];
      this.visitFieldValidate(baseModel, node.fieldValue, level, `${_upperFirst(_name(node.fieldName))}`);
      var paramName = _avoidReserveName(_upperFirst(_name(node.fieldName)));
      if (paramName === modelName) {
        paramName = paramName + '_';
      }
      const attrName = getAttr(node, 'name') || _name(node.fieldName);
      const pattern = this.getAttributes(node, 'pattern') || '';
      const maxLength = this.getAttributes(node, 'maxLength') || 0;
      const minLength = this.getAttributes(node, 'minLength') || 0;
      const maximum = this.getAttributes(node, 'maximum') || 0;
      const minimum = this.getAttributes(node, 'minimum') || 0;
      const required = node.required || false;
      if (required || maxLength > 0 || minLength > 0 || maximum > 0 || pattern !== '') {
        if (required) {
          this.emit(`${baseModel}.ValidateRequired("${attrName}", ${paramName}, true);\n`, level);
        }
        if (pattern !== '') {
          this.emit(`${baseModel}.ValidateRegex("${attrName}", ${paramName}, "${pattern}");\n`, level);
        }
        if (maxLength > 0 && maxLength <= 2147483647) {
          this.emit(`${baseModel}.ValidateMaxLength("${attrName}", ${paramName}, ${maxLength});\n`, level);
        }
        if (minLength > 0 && minLength <= 2147483647) {
          this.emit(`${baseModel}.ValidateMinLength("${attrName}", ${paramName}, ${minLength});\n`, level);
        }
        if (maximum > 0 && maximum <= Number.MAX_SAFE_INTEGER) {
          this.emit(`${baseModel}.ValidateMaximum("${attrName}", ${paramName}, ${maximum});\n`, level);
        }
        if (minimum > 0 && minimum <= Number.MAX_SAFE_INTEGER) {
          this.emit(`${baseModel}.ValidateMinimum("${attrName}", ${paramName}, ${minimum});\n`, level);
        }
      }
    }
    this.emit('base.Validate();\n', level);
  }

  visitFromField(fieldValue, modelName, fieldName, key, value, level) {
    // 多一个namespace的缩进，所以level-1
    const deep = Math.floor((level - 1) / 2);
    if (fieldValue.type === 'modelBody') {
      const subModelName = modelName + _upperFirst(fieldName);
      this.emit(`if (${value} != null)\n`, level + 1);
      this.emit(`{\n`, level + 1);
      this.emit(`var temp = (Dictionary<string, object>)${value};\n`, level + 2);
      this.emit(`${key} = ${subModelName}.FromMap(temp);\n`, level + 2);
      this.emit(`}\n`, level + 1);
    } else if (fieldValue.type === 'array' || fieldValue.fieldType === 'array') {
      if (deep === 1) {
        this.emit(`var list${deep} = ${value} as `, level + 1);
        this.visitModelType(fieldValue, level + 1, modelName, fieldName);
      } else {
        this.emit(`var list${deep} = ${value}`, level + 1);
      }


      this.emit(';\n');
      this.emit(`if (list${deep} != null && list${deep}.Count > 0)\n`, level + 1);
      this.emit('{\n', level + 1);
      this.emit(`int n${deep} = 0;\n`, level + 2);
      // multiLayerModel.data.results -> MultiLayerModelDataResults
      this.emit(`var modelList${deep} = new `, level + 2);
      this.visitFieldType(fieldValue, level + 2, modelName, fieldName);
      this.emit(`();\n`);
      this.emit(`foreach(var item${deep} in list${deep})\n`, level + 2);
      this.emit('{\n', level + 2);
      this.visitFromField(fieldValue.fieldItemType, modelName, fieldName, `modelList${deep}[n${deep}++]`, `item${deep}`, level + 2);
      this.emit('}\n', level + 2);
      this.emit(`${key} = modelList${deep};\n`, level + 2);
      this.emit('}\n', level + 1);
    } else if (fieldValue.fieldType === 'map' || fieldValue.type === 'map') {
      let valueType;
      if (fieldValue.valueType.idType === 'model' || fieldValue.valueType.type === 'subModel' || (fieldValue.type === 'moduleModel' ||
        (fieldValue.fieldType && fieldValue.fieldType.type === 'moduleModel') || (fieldValue.valueType && fieldValue.valueType.type === 'moduleModel')) || (fieldValue.fieldType && fieldValue.fieldType.idType === 'model') ||
        (fieldValue.fieldType && fieldValue.fieldType.idType === 'builtin_model')) {
        valueType = 'Dictionary<string, object>';
      } else {
        valueType = this.getRealClientName(_name(fieldValue.valueType)) || _type(_name(fieldValue.valueType));
      }
      this.emit(`var dict = ${value} as Dictionary<string, ${valueType}>;\n`, level + 1);
      this.emit(`if (dict != null && dict.Count > 0)\n`, level + 1);
      this.emit(`{\n`, level + 1);
      let type = '';
      if (_name(fieldValue.valueType) && fieldValue.valueType.idType === 'model') {
        // const realModelName = this.getRealModelName(`${this.namespace}.Models.${_upperFirst(_name(fieldValue.valueType))}`);
        const realModelName = this.getRealModelName(`${this.namespace}.Models`, _upperFirst(_name(fieldValue.valueType)));
        type = realModelName;
      } else if (fieldValue.valueType.type === 'moduleModel') {
        const [moduleId, ...models] = fieldValue.valueType.path || fieldValue.valueType.fieldType.path;
        const { namespace } = this.moduleClass.get(moduleId.lexeme);
        const moduleModelName = models.map((item) => {
          return item.lexeme;
        }).join('.');
        // const subModelName = this.getRealModelName(`${namespace}.Models.${moduleModelName}`);
        const subModelName = this.getRealModelName(`${namespace}.Models`, moduleModelName);
        type = subModelName;
      } else if (_name(fieldValue.valueType) && fieldValue.valueType.idType === 'module') {
        const realClsName = this.getRealClientName(_name(fieldValue.valueType));
        type = realClsName;
      } else if (_name(fieldValue.valueType)) {
        type = _type(_name(fieldValue.valueType));
      } else if (!_name(fieldValue.valueType) && fieldValue.valueType.type === 'subModel') {
        const [, ...models] = fieldValue.valueType.path || fieldValue.valueType.fieldType.path;
        type = modelName + models.map((item) => {
          return _upperFirst(item.lexeme);
        }).join('');
      }

      this.emit(`var modelMap${deep} = new Dictionary<string, ${type}>();\n`, level + 2);
      this.emit(`foreach (KeyValuePair<string, ${valueType}> entry${deep} in dict)\n`, level + 2);
      this.emit('{\n', level + 2);
      this.visitFromField(fieldValue.valueType, modelName, fieldName, `modelMap${deep}[entry${deep}.Key]`, `entry${deep}.Value`, level + 2);
      this.emit('}\n', level + 2);
      this.emit(`${key} = modelMap${deep};\n`, level + 2);
      this.emit(`}\n`, level + 1);
    } else if (fieldValue.type === 'moduleModel' ||
      (fieldValue.fieldType && fieldValue.fieldType.type === 'moduleModel')) {
      const [moduleId, ...models] = fieldValue.path || fieldValue.fieldType.path;
      const { namespace } = this.moduleClass.get(moduleId.lexeme);
      const moduleModelName = models.map((item) => {
        return item.lexeme;
      }).join('.');
      // const subModelName = this.getRealModelName(`${namespace}.Models.${moduleModelName}`);
      const subModelName = this.getRealModelName(`${namespace}.Models`, moduleModelName);
      this.emit(`${key} = ${subModelName}.FromMap(${value});\n`, level + 1);
    } else if (fieldValue.type === 'subModel') {
      const [, ...models] = fieldValue.path;
      let model = modelName + models.map((item) => {
        return _upperFirst(item.lexeme);
      }).join('');
      // const realModelName = this.getRealModelName(`${this.namespace}.Models.${model}`);
      const realModelName = this.getRealModelName(`${this.namespace}.Models`, model);
      this.emit(`${key} = ${realModelName}.FromMap(${value});\n`, level + 1);
    } else if (fieldValue.idType === 'model') {
      // const realModelName = this.getRealModelName(`${this.namespace}.Models.${_upperFirst((fieldValue.lexeme))}`);
      const realModelName = this.getRealModelName(`${this.namespace}.Models`, _upperFirst((fieldValue.lexeme)));
      this.emit(`${key} = ${realModelName}.FromMap(${value});\n`, level + 1);
    } else if (fieldValue.fieldType && fieldValue.fieldType.idType === 'model') {
      // const realModelName = this.getRealModelName(`${this.namespace}.Models.${fieldValue.fieldType.lexeme}`);
      const realModelName = this.getRealModelName(`${this.namespace}.Models`, fieldValue.fieldType.lexeme);
      this.emit(`var temp = (Dictionary<string, object>)${value};\n`, level + 1);
      this.emit(`${key} = ${realModelName}.FromMap(temp);\n`, level + 1);
    } else if (fieldValue.fieldType && fieldValue.fieldType.idType === 'builtin_model') {
      this.emit(`var temp = (Dictionary<string, object>)${value};\n`, level + 1);
      // const realModelName = this.getRealModelName((fieldValue.fieldType.lexeme));
      const [namespace, subModelName] = splitByLastDot(fieldValue.fieldType.lexeme);
      const realModelName = this.getRealModelName(namespace, subModelName);
      this.emit(`${key} = ${realModelName}.FromMap(temp);\n`, level + 1);
    } else {
      this.emit(`${key} = (`, level + 1);
      this.visitFieldType(fieldValue, level + 1, modelName, fieldName);
      this.emit(`)${value};\n`);
    }
  }

  visitToMapField(fieldValue, modelName, fieldName, key, value, depth, level) {
    const deep = depth;
    if (fieldValue.type === 'modelBody' || fieldValue.type === 'moduleModel' ||
      fieldValue.type === 'subModel' || fieldValue.idType === 'model') {
      this.emit(`${key} = ${value} != null ? ${value}.ToMap() : null;\n`, level + 1);
    } else if (fieldValue.fieldType && (fieldValue.fieldType.type === 'moduleModel' ||
      fieldValue.fieldType.idType === 'model' || fieldValue.fieldType.idType === 'builtin_model')) {
      this.emit(`${key} = ${value} != null ? ${value}.ToMap() : null;\n`, level + 1);
    } else if (fieldValue.type === 'array' || fieldValue.fieldType === 'array') {
      this.emit(`var list${deep} = new `, level + 1);
      this.visitModelType(fieldValue, level + 1, modelName, fieldName);
      this.emit('();\n');
      this.emit(`int n${deep} = 0;\n`, level + 1);
      this.emit(`foreach (var item${deep} in ${value}) \n`, level + 1);
      this.emit(`{\n`, level + 1);
      this.visitToMapField(fieldValue.fieldItemType, modelName, fieldName, `list${deep}[n${deep}++]`, `item${deep}`, depth + 1, level + 1);
      this.emit('}\n', level + 1);
      if (deep > 1) {
        this.emit(`list${deep - 1}[n${deep - 1}++] = list${deep};\n`, level + 1);
      } else {
        this.emit(`${key} = list${deep};\n`, level + 1);
      }
    } else if (fieldValue.fieldType === 'map' || fieldValue.type === 'map') {
      this.emit(`var dict = new `, level + 1);
      this.visitModelType(fieldValue, level + 1, modelName, fieldName);
      this.emit(`();\n`);
      this.emit(`foreach (var item${deep} in ${value}) \n`, level + 1);
      this.emit(`{\n`, level + 1);
      this.visitToMapField(fieldValue.valueType, modelName, fieldName, `dict[item${deep}.Key]`, `item${deep}.Value`, depth + 1, level + 1);
      this.emit('}\n', level + 1);
      this.emit(`${key} = dict;\n`, level + 1);
    } else {
      this.emit(`${key} = ${value};\n`, level + 1);
    }
  }

  visitType(ast, level, env) {
    if (ast.type === 'array') {
      this.emit('List<');
      this.visitType(ast.subType || ast.itemType, level);
      this.emit('>');
    } else if (ast.type === 'map' || ast.fieldType === 'map') {
      if (env && env.castToObject) {
        this.emit('Dictionary<string, object>');
        env.castToObject = false;
        return;
      }
      this.emit('Dictionary<');
      this.visitType(ast.keyType, level);
      this.emit(', ');
      this.visitType(ast.valueType, level);
      this.emit('>');
    } else if (this.predefined && this.predefined['module:' + _name(ast)]) {
      let clientName = (this.imports[_name(ast)] && this.imports[_name(ast)]['client']) || `${_name(ast)}.Client`;
      this.emit(clientName);
    } else if (ast.type === 'model') {
      let namespace = this.namespace;
      let type = 'Models';
      if (ast.moduleName) {
        namespace = this.moduleClass.get(ast.moduleName).namespace;
        var usedEx;
        if (this.usedExternException) {
          usedEx = this.usedExternException.get(ast.moduleName);
        }
        if (usedEx && usedEx.has(ast.name)) {
          type = 'Exceptions';
        }
      } else if (this.predefined[ast.name] && this.predefined[ast.name].isException) {
        type = 'Exceptions';
      }
      let tempName = '';
      let models = _name(ast).split('.');
      models.forEach((item, index) => {
        if (index > 0) {
          this.emit('.');
        }
        if (index === 0) {
          tempName += _upperFirst(_type(item));
          // const realModelName = this.getRealModelName(`${namespace}.${type}.${tempName}`, type);
          const realModelName = this.getRealModelName(`${namespace}.${type}`, tempName, type);
          this.emit(realModelName);
        } else {
          tempName += _upperFirst(_type(item));
          this.emit(tempName);
        }
      });
    } else if (ast.type === 'moduleModel' && ast.path && ast.path.length > 0) {
      const [moduleId, ...rest] = ast.path;
      const { namespace } = this.moduleClass.get(moduleId.lexeme);
      let moduleModelName = '';
      let tempName = '';
      rest.forEach((item, index) => {
        tempName += _upperFirst(item.lexeme);
        if (index < rest.length - 1) {
          moduleModelName += tempName + '.';
        } else {
          moduleModelName += tempName;
        }
      });
      let type = 'Models';
      if (this.usedExternException) {
        usedEx = this.usedExternException.get(moduleId.lexeme);
      }
      if (usedEx && usedEx.has(moduleModelName)) {
        type = 'Exceptions';
      }
      const subModelName = this.getRealModelName(`${namespace}.${type}`, moduleModelName, type);
      this.emit(subModelName);
    } else if (ast.type === 'module_instance') {
      if (_name(ast) in builtinMap) {
        this.emit(`${builtinMap[_name(ast)]}`);
      } else {
        const realClsName = this.getRealClientName(_name(ast));
        this.emit(realClsName);
      }
    } else if (ast.idType === 'module') {
      let moduleName = _name(ast);
      moduleName = this.getRealClientName(moduleName);
      this.emit(`${moduleName || 'Client'}`);
    } else if (ast.idType === 'model') {
      let type = 'Models';
      if (this.predefined[ast.lexeme] && this.predefined[ast.lexeme].isException) {
        type = 'Exceptions';
      }
      // const realModelName = this.getRealModelName(`${this.namespace}.${type}.${_upperFirst(ast.lexeme)}`, type);
      const realModelName = this.getRealModelName(`${this.namespace}.${type}`, _upperFirst(ast.lexeme), type);
      this.emit(realModelName);
    } else if (ast.idType === 'builtin_model') {
      // const realModelName = this.getRealModelName(ast.lexeme);
      const [namespace, subModelName] = splitByLastDot(ast.lexeme);
      const realModelName = this.getRealModelName(namespace, subModelName);
      this.emit(realModelName);
    } else if (ast.type === 'moduleTypedef') {
      const [moduleId, modelName] = ast.path;
      const moduleTypedef = this.moduleTypedef[moduleId.lexeme];
      const typedef = moduleTypedef[modelName.lexeme];
      if (!typedef.import) {
        this.emit(typedef.type);
      } else {
        // const typedefName = this.getRealModelName(`${typedef.import}.${typedef.type}`);
        const typedefName = this.getRealModelName(typedef.import, typedef.type);
        this.emit(typedefName);
      }
    } else if (ast.type === 'typedef' || ast.idType === 'typedef') {
      const typedef = this.typedef[ast.lexeme];
      if (!typedef.import) {
        this.emit(typedef.type);
      } else {
        const typedefName = this.getRealModelName(typedef.import, typedef.type);
        this.emit(typedefName);
      }
    } else if (ast.fieldType === 'array') {
      this.emit('List<');
      this.visitType(ast.fieldItemType, level);
      this.emit('>');
    } else if (ast.type === 'entry') {
      this.emit('KeyValuePair<string, ');
      this.visitType(ast.valueType);
      this.emit('>');
    } else if (ast.type === 'subModel') {
      let tempName = '';
      for (let i = 0; i < ast.path.length; i++) {
        if (i > 0) {
          this.emit('.');
        }
        tempName += _upperFirst(_name(ast.path[i]));
        this.emit(tempName);
      }
    } else if (typeof ast.fieldType === 'string') {
      this.emit(`${_type(ast.fieldType)}`);
    } else if (this.isIterator(ast)) {
      if (ast.type === 'iterator') {
        this.emit('IEnumerable<');
      } else if (ast.type === 'asyncIterator') {
        this.emit('IAsyncEnumerable<');
      }
      this.visitType(ast.valueType);
      this.emit('>');
    } else if (ast.type === 'basic') {
      this.emit(_type(_name(ast)));
    } else {
      this.emit(_type(_name(ast)));
    }
  }

  visitModelType(value, level, modelName, fieldName) {
    if (value.type === 'array') {
      this.visitType(value);
    } else if (value.fieldType === 'array') {
      this.emit(`List<`);
      this.visitModelType(value.fieldItemType, level, modelName, fieldName);
      this.emit(`>`);
    } else if (value.fieldType === 'map') {
      this.emit(`Dictionary<${value.keyType.lexeme}, `);
      this.visitModelType(value.valueType);
      this.emit(`>`);
    } else if (value.type === 'map') {
      this.emit(`Dictionary<${value.keyType.lexeme}, `);
      this.visitModelType(value.valueType);
      this.emit(`>`);
    } else if (value.tag === Tag.TYPE) {
      this.emit(`${_type(value.lexeme)}`);
    } else if (value.type === 'modelBody' || (value.tag === Tag.ID && (value.idType === 'model' || value.idType === 'module' || value.idType === 'builtin_model')) ||
      value.type === 'moduleModel' || value.type === 'subModel' || value.fieldType.type === 'moduleModel'
      || value.fieldType.idType === 'model' || value.fieldType.idType === 'builtin_model') {
      this.emit('Dictionary<string, object>');
    } else if (value.tag === Tag.ID) {
      this.emit(`${value.lexeme}`);
    } else if (typeof value.fieldType === 'string') {
      this.emit(`${_type(value.fieldType)}`);
    } else if (value.fieldType.type === 'moduleTypedef') {
      const [moduleId, modelName] = value.fieldType.path;
      const moduleTypedef = this.moduleTypedef[moduleId.lexeme];
      const typedef = moduleTypedef[modelName.lexeme];
      if (!typedef.import) {
        this.emit(typedef.type);
      } else {
        const typedefName = this.getRealModelName(typedef.import, typedef.type);
        this.emit(typedefName);
      }
    } else if (value.fieldType.type === 'typedef' || value.fieldType.idType === 'typedef') {
      const typedef = this.typedef[value.fieldType.lexeme];
      if (!typedef.import) {
        this.emit(typedef.type);
      } else {
        const typedefName = this.getRealModelName(typedef.import, typedef.type);
        this.emit(typedefName);
      }
    } else if (value.fieldType.type) {
      this.emit(`${_type(value.fieldType.lexeme)}`);
    } else if (value.fieldType.idType === 'module') {
      let moduleName = _type(value.fieldType.lexeme);
      moduleName = this.getRealClientName(moduleName);
      this.emit(moduleName);
    }
  }

  visitFieldType(value, level, modelName, fieldName) {
    if (value.type === 'modelBody') {
      var paramName = _avoidReserveName(_upperFirst(fieldName));
      const subModelName = modelName + _upperFirst(paramName);
      this.emit(subModelName);
    } else if (value.type === 'array') {
      this.visitType(value);
    } else if (value.fieldType === 'array') {
      this.emit(`List<`);
      this.visitFieldType(value.fieldItemType, level, modelName, fieldName);
      this.emit(`>`);
    } else if (value.fieldType === 'map') {
      this.emit(`Dictionary<${value.keyType.lexeme}, `);
      this.visitFieldType(value.valueType);
      this.emit(`>`);
    } else if (value.type === 'map') {
      this.emit(`Dictionary<${value.keyType.lexeme}, `);
      this.visitFieldType(value.valueType);
      this.emit(`>`);
    } else if (value.tag === Tag.TYPE) {
      this.emit(`${_type(value.lexeme)}`);
    } else if (value.tag === Tag.ID && value.idType === 'model') {
      const modelName = _upperFirst(_name(value));
      // const realModelName = this.getRealModelName(`${this.namespace}.Models.${modelName}`);
      const realModelName = this.getRealModelName(`${this.namespace}.Models`, modelName);
      this.emit(realModelName);
    } else if (value.tag === Tag.ID && value.idType === 'module') {
      let moduleName = _upperFirst(_name(value));
      moduleName = this.getRealClientName(moduleName);
      this.emit(moduleName);
    } else if (value.tag === Tag.ID && value.idType === 'builtin_model') {
      this.emit(`${value.lexeme}`);
    } else if (value.tag === Tag.ID) {
      this.emit(`${value.lexeme}`);
    } else if (value.type === 'moduleModel') {
      const [moduleId, ...models] = value.path;
      const { namespace } = this.moduleClass.get(moduleId.lexeme);
      const moduleModelName = models.map((item) => {
        return item.lexeme;
      }).join('.');
      // const subModelName = this.getRealModelName(`${namespace}.Models.${moduleModelName}`);
      const subModelName = this.getRealModelName(`${namespace}.Models`, moduleModelName);
      this.emit(subModelName);
    } else if (value.type === 'subModel') {
      let tempName = '';
      for (let i = 0; i < value.path.length; i++) {
        if (i > 0) {
          this.emit('.');
        }
        tempName += _upperFirst(_name(value.path[i]));
        this.emit(tempName);
      }
    } else if (typeof value.fieldType === 'string') {
      this.emit(`${_type(value.fieldType)}`);
    } else if (value.fieldType.type === 'moduleModel') {
      const [moduleId, ...models] = value.fieldType.path;
      this.emit(`${moduleId.lexeme}.${_subModelName(models.map((item) => item.lexeme).join('.'))}`);
    } else if (value.fieldType.type === 'moduleTypedef') {
      const [moduleId, modelName] = value.fieldType.path;
      const moduleTypedef = this.moduleTypedef[moduleId.lexeme];
      const typedef = moduleTypedef[modelName.lexeme];
      if (!typedef.import) {
        this.emit(typedef.type);
      } else {
        // const typedefName = this.getRealModelName(`${typedef.import}.${typedef.type}`);
        const typedefName = this.getRealModelName(typedef.import, typedef.type);
        this.emit(typedefName);
      }
    } else if (value.fieldType.type === 'typedef' || value.fieldType.idType === 'typedef') {
      const typedef = this.typedef[value.fieldType.lexeme];
      if (!typedef.import) {
        this.emit(typedef.type);
      } else {
        const typedefName = this.getRealModelName(typedef.import, typedef.type);
        this.emit(typedefName);
      }
    } else if (value.fieldType.type) {
      this.emit(`${_type(value.fieldType.lexeme)}`);
    } else if (value.fieldType.idType === 'model') {
      const realModelName = this.getRealModelName(`${this.namespace}.Models`, value.fieldType.lexeme);
      this.emit(realModelName);
    } else if (value.fieldType.idType === 'module') {
      let moduleName = _type(value.fieldType.lexeme);
      moduleName = this.getRealClientName(moduleName);
      this.emit(moduleName);
    } else if (value.fieldType.idType === 'builtin_model') {
      const [namespace, subModelName] = splitByLastDot(_type(value.fieldType.lexeme));
      const realModelName = this.getRealModelName(namespace, subModelName);
      this.emit(realModelName);
    }
  }

  visitReturnType(ast, level, env) {
    const handleIterator = (typeName) => {
      if (_type(_name(ast.returnType)) === 'void') {
        this.emit(typeName);
      } else {
        this.emit(`${typeName}<`);
        this.visitType(ast.returnType.valueType, level);
        this.emit('>');
      }
    };

    switch (ast.returnType.type) {
      case 'asyncIterator':
        handleIterator('IAsyncEnumerable');
        break;
      case 'iterator':
        handleIterator('IEnumerable');
        break;
      default:
        if (env.isAsyncMode) {
          if (_type(_name(ast.returnType)) === 'void') {
            this.emit('Task');
          } else {
            this.emit('Task<');
            this.visitType(ast.returnType, level);
            this.emit('>');
          }
        } else {
          this.visitType(ast.returnType, level);
        }
        break;
    }
    this.emit(' ');
  }

  visitReturnBody(ast, level, env) {
    assert.equal(ast.type, 'returnBody');
    this.emit('\n');
    this.visitStmts(ast.stmts, level, env);
  }

  visitDefaultReturnBody(level) {
    this.emit('\n');
    this.emit('return;\n', level);
  }

  visitAPIBody(ast, level, env) {
    assert.equal(ast.type, 'apiBody');
    this.emit(`DaraRequest ${REQUEST} = new DaraRequest();\n`, level);

    this.visitStmts(ast.stmts, level, env);
  }

  visitStmts(ast, level, env) {
    assert.equal(ast.type, 'stmts');
    let node;
    for (var i = 0; i < ast.stmts.length; i++) {
      node = ast.stmts[i];
      this.visitStmt(node, level, env);
    }
    if (node) {
      //find the last node's back comment
      let comments = DSL.comment.getBackComments(this.comments, node.tokenRange[1]);
      this.visitComments(comments, level);
    }

    if (ast.stmts.length === 0) {
      //empty block's comment
      let comments = DSL.comment.getBetweenComments(this.comments, ast.tokenRange[0], ast.tokenRange[1]);
      this.visitComments(comments, level);
    }
  }

  visitStmt(ast, level, env) {
    let comments = DSL.comment.getFrontComments(this.comments, ast.tokenRange[0]);
    this.visitComments(comments, level);
    if (ast.type === 'return') {
      this.visitReturn(ast, level, env);
    } else if (ast.type === 'yield') {
      this.visitYield(ast, level, env);
    } else if (ast.type === 'if') {
      this.visitIf(ast, level, env);
    } else if (ast.type === 'throw') {
      this.visitThrow(ast, level, env);
    } else if (ast.type === 'assign') {
      this.visitAssign(ast, level, env);
    } else if (ast.type === 'retry') {
      this.visitRetry(ast, level);
    } else if (ast.type === 'break') {
      this.emit('break;\n', level);
    } else if (ast.type === 'declare') {
      this.visitDeclare(ast, level, env);
    } else if (ast.type === 'while') {
      this.visitWhile(ast, level, env);
    } else if (ast.type === 'for') {
      this.visitFor(ast, level, env);
    } else if (ast.type === 'call') {
      this.emit('', level);
      this.visitExpr(ast, level, env);
      this.emit(';\n');
    } else if (ast.type === 'super') {
      //no need
    } else if (ast.type === 'try') {
      this.visitTry(ast, level, env);
    } else {
      this.emit('', level);
      this.visitExpr(ast, level);
      this.emit(';\n');
    }
  }

  visitTry(ast, level, env) {
    this.emit('try\n', level);
    this.emit('{\n', level);
    this.visitStmts(ast.tryBlock, level + 1, env);
    this.emit('}\n', level);

    if (ast.catchBlocks && ast.catchBlocks.length > 0) {
      ast.catchBlocks.forEach(catchBlock => {
        if (!catchBlock.id) {
          return;
        }
        if (!catchBlock.id.type) {
          // const errorName = this.getRealModelName('Tea.TeaException');
          const errorName = this.getRealModelName('Tea', 'TeaException');
          this.emit(`catch (${errorName} ${_name(catchBlock.id)})\n`, level);
          this.emit(`{\n`, level);
        } else {
          this.emit('catch (', level);
          this.visitType(catchBlock.id.type);
          this.emit(` ${_name(catchBlock.id)})\n`);
          this.emit('{\n', level);
        }
        this.visitStmts(catchBlock.catchStmts, level + 1);
        this.emit('}\n', level);
      });
    } else if (ast.catchBlock && ast.catchBlock.stmts.length > 0) {
      const errorName = this.getRealModelName('Tea', 'TeaException');
      this.emit(`catch (${errorName} ${_name(ast.catchId)})\n`, level);
      this.emit('{\n', level);
      this.visitStmts(ast.catchBlock, level + 1);
      this.emit('}\n', level);
    }

    if (ast.finallyBlock) {
      this.emit('finally\n', level);
      this.emit('{\n', level);
      this.visitStmts(ast.finallyBlock, level + 1, env);
      this.emit('}\n', level);
    }
  }

  visitDeclare(ast, level, env) {
    var id = _name(ast.id);
    this.emit(``, level);
    // random返回double类型需要额外处理
    if (ast.expr.left && ast.expr.left.id && ast.expr.left.id.type === 'builtin_module' &&
      ast.expr.left.id.lexeme === '$Number' && ast.expr.left.propertyPath[0]) {
      if (ast.expr.left.propertyPath[0].lexeme === 'random') {
        this.emit('double');
      } else if (ast.expr.left.propertyPath[0].lexeme === 'min' || ast.expr.left.propertyPath[0].lexeme === 'max') {
        if (ast.expr.args[0].inferred.name !== ast.expr.args[1].inferred.name) {
          this.emit('double');
        } else {
          this.emit(_type(ast.expr.args[0].inferred.name));
        }
      }
    } else {
      this.visitType(ast.expr.inferred);
    }
    this.emit(` ${_avoidReserveName(id)} = `);
    this.visitExpr(ast.expr, level, env);
    this.emit(';\n');
  }

  visitParams(ast) {
    assert.equal(ast.type, 'params');
    this.emit('(');
    for (var i = 0; i < ast.params.length; i++) {
      if (i !== 0) {
        this.emit(', ');
      }
      const node = ast.params[i];
      assert.equal(node.type, 'param');
      this.visitType(node.paramType);
      const name = _name(node.paramName);
      this.emit(` ${_avoidReserveName(name)}`);
    }

    this.emit(')');
  }

  visitRuntimeBefore(ast, level, env) {
    assert.equal(ast.type, 'object');
    const retryType = this.getRealModelName('Darabonba.RetryPolicy', 'RetryOptions');
    this.emit(`Dictionary<string, object> ${RUNTIME} = `, level);
    // this.visitConstructObject(ast, level, env);
    this.visitObject(ast, level, env);
    this.emit(';\n');
    this.emit('\n');
    this.emit('RetryPolicyContext _retryPolicyContext = null;\n', level);
    this.emit('DaraRequest _lastRequest = null;\n', level);
    this.emit('DaraResponse _lastResponse = null;\n', level);
    this.emit('Exception _lastException = null;\n', level);
    this.emit('long _now = System.DateTime.Now.Millisecond;\n', level);
    this.emit('int _retriesAttempted = 0;\n', level);
    this.emit('_retryPolicyContext = new RetryPolicyContext\n', level);
    this.emit('{\n', level);
    this.emit('RetriesAttempted = _retriesAttempted\n', level + 1);
    this.emit('};\n', level);
    this.emit(`while (DaraCore.ShouldRetry((${retryType})${RUNTIME}["retryOptions"], _retryPolicyContext))\n`, level);
    this.emit('{\n', level);
    this.emit('if (_retriesAttempted > 0)\n', level + 1);
    this.emit('{\n', level + 1);
    this.emit(`int backoffTime = DaraCore.GetBackoffDelay((${retryType})${RUNTIME}["retryOptions"], _retryPolicyContext);\n`, level + 2);
    this.emit('if (backoffTime > 0)\n', level + 2);
    this.emit('{\n', level + 2);
    this.emit('DaraCore.Sleep(backoffTime);\n', level + 3);
    this.emit('}\n', level + 2);
    this.emit('}\n', level + 1);
    this.emit('try\n', level + 1);
    this.emit('{\n', level + 1);
  }

  visitRuntimeAfter(ast, level) {
    this.emit('}\n', level + 1);
    this.emit('catch (Exception e)\n', level + 1);
    this.emit('{\n', level + 1);
    this.emit('_retriesAttempted++;\n', level + 2);
    this.emit('_lastException = e;\n', level + 2);
    this.emit('_retryPolicyContext = new RetryPolicyContext\n', level + 2);
    this.emit('{\n', level + 2);
    this.emit('RetriesAttempted = _retriesAttempted,\n', level + 3);
    this.emit('Request = _lastRequest,\n', level + 3);
    this.emit('Response = _lastResponse,\n', level + 3);
    this.emit('Exception = _lastException\n', level + 3);
    this.emit('};\n', level + 2);
    this.emit('}\n', level + 1);
    this.emit('}\n\n', level);
    this.emit('throw _lastException;\n', level);
  }

  visitExpr(ast, level, env) {
    if (ast.type === 'boolean') {
      this.emit(ast.value);
    } else if (ast.type === 'property_access') {
      this.visitPropertyAccess(ast, level, env);
    } else if (ast.type === 'string') {
      let val = _string(ast.value);
      val = val.replace(/(?<!\\)(?:\\{2})*"/g, '\\"');
      this.emit(`"${val}"`);
    } else if (ast.type === 'number') {
      this.emit(ast.value.value);
      if (ast.value.type === 'float') {
        this.emit('f');
      }
    } else if (ast.type === 'object') {
      this.visitObject(ast, level, env);
    } else if (ast.type === 'variable') {
      var id = _name(ast.id);
      if (id === '__response') {
        this.emit(RESPONSE);
      } else if (id === '__request') {
        this.emit(REQUEST);
      } else if (ast.inferred && ast.inferred.name === 'class') {
        this.emit('typeof(' + _avoidReserveName(id) + ')');
      } else if (ast.inferred && ast.inferred.name === 'boolean') {
        this.emit(`${_avoidReserveName(id)}.Value`);
      } else {
        this.emit(_avoidReserveName(id));
      }
    } else if (ast.type === 'virtualVariable') {
      const vid = `_${_lowerFirst(_name(ast.vid).substr(1))}`;
      if (_name(ast.inferred) === 'boolean') {
        this.emit(`${vid}.Value`);
      } else {
        this.emit(`${vid}`);
      }
    } else if (ast.type === 'decrement') {
      if (ast.position === 'front') {
        this.emit('--');
      }
      this.visitExpr(ast.expr, level, env);
      if (ast.position === 'backend') {
        this.emit('--');
      }
    } else if (ast.type === 'increment') {
      if (ast.position === 'front') {
        this.emit('++');
      }
      this.visitExpr(ast.expr, level, env);
      if (ast.position === 'backend') {
        this.emit('++');
      }
    } else if (ast.type === 'template_string') {
      var elements = ast.elements.filter((item) => {
        return item.type !== 'element' || item.value.string.length > 0;
      });

      for (var i = 0; i < elements.length; i++) {
        var item = elements[i];
        if (item.type === 'element') {
          this.emit('"');
          let val = _string(item.value);
          val = val.replace(/(?<!\\)(?:\\{2})*"/g, '\\"');
          val = val.replace(/[\n]/g, '" + \n"');
          this.emit(val);
          this.emit('"');
        } else if (item.type === 'expr') {
          if (i === 0) {
            this.emit('"" + ');
          }
          env.groupOp = true;
          this.visitExpr(item.expr, level, env);
        } else {
          throw new Error('unimpelemented');
        }

        if (i < elements.length - 1) {
          this.emit(' + ');
        }
      }
    } else if (ast.type === 'call') {
      this.visitCall(ast, level, env);
    } else if (ast.type === 'construct') {
      this.visitConstruct(ast, level, env);
    } else if (ast.type === 'group') {
      env.groupOp = false;
      this.emit('(');
      this.visitExpr(ast.expr, level, env);
      this.emit(')');
    } else if (_isBinaryOp(ast.type)) {
      env.groupOp = true;
      this.visitExpr(ast.left, level, env);
      if (ast.type === 'or') {
        this.emit(' || ');
      } else if (ast.type === 'add') {
        this.emit(' + ');
      } else if (ast.type === 'subtract') {
        this.emit(' - ');
      } else if (ast.type === 'div') {
        this.emit(' / ');
      } else if (ast.type === 'multi') {
        this.emit(' * ');
      } else if (ast.type === 'and') {
        this.emit(' && ');
      } else if (ast.type === 'lte') {
        this.emit(' <= ');
      } else if (ast.type === 'lt') {
        this.emit(' < ');
      } else if (ast.type === 'gte') {
        this.emit(' >= ');
      } else if (ast.type === 'gt') {
        this.emit(' > ');
      } else if (ast.type === 'neq') {
        this.emit(' != ');
      } else if (ast.type === 'eq') {
        this.emit(' == ');
      }
      this.visitExpr(ast.right, level, env);
    }
    else if (ast.type === 'array') {
      this.visitArray(ast, level, env);
    } else if (ast.type === 'null') {
      this.emit('null');
    } else if (ast.type === 'not') {
      env.groupOp = true;
      this.emit('!');
      this.visitExpr(ast.expr, level, env);
    } else if (ast.type === 'construct_model') {
      this.visitConstructModel(ast, level, env);
    } else if (ast.type === 'map_access') {
      this.visitMapAccess(ast, level, env);
    } else if (ast.type === 'super') {
      //no need
    } else if (ast.type === 'array_access') {
      this.visitArrayAccess(ast, level, env);
    } else {
      throw new Error('unimpelemented');
    }
  }

  visitMapAccess(ast, level, env) {
    assert.equal(ast.type, 'map_access');
    var mapName = _name(ast.id);
    if (ast.id.tag === Tag.VID) {
      mapName = _vid(ast.id);
    } else {
      mapName = _name(ast.id);
    }

    if (ast.propertyPath && ast.propertyPath.length) {
      var current = ast.id.inferred;
      for (var i = 0; i < ast.propertyPath.length; i++) {
        var name = _name(ast.propertyPath[i]);
        if (current.type === 'model') {
          mapName += `.${_upperFirst(name)}`;
        } else {
          mapName += `["${name}"]`;
        }
        current = ast.propertyPathTypes[i];
      }
    }

    this.emit(`${mapName}.Get(`);

    this.visitExpr(ast.accessKey, level, env);
    this.emit(`)`);
  }

  visitArrayAccess(ast, level, env, isLeft) {
    assert.equal(ast.type, 'array_access');
    let expr;
    if (ast.id.tag === DSL.Tag.Tag.VID) {
      expr = `this.${_vid(ast.id)}`;
    } else {
      expr = `${_name(ast.id)}`;
    }

    if (ast.propertyPath && ast.propertyPath.length) {
      var current = ast.id.inferred;
      for (var i = 0; i < ast.propertyPath.length; i++) {
        var name = _name(ast.propertyPath[i]);
        if (current.type === 'model') {
          expr += `.${_upperFirst(name)}`;
        } else {
          expr += `["${name}"]`;
        }
        current = ast.propertyPathTypes[i];
      }
    }
    if (isLeft) {
      this.emit(`${expr}[`, level);
    } else {
      this.emit(`${expr}[`);
    }

    this.visitExpr(ast.accessKey, level, env);
    if (ast.accessKey.type === 'variable') {
      this.emit(`.Value`);
    }
    this.emit(`]`);
  }

  visitYield(ast, level, env) {
    assert.equal(ast.type, 'yield');
    this.emit('yield return ', level);
    if (!ast.expr) {
      this.emit(';\n');
      return;
    }

    if (ast.needCast) {
      this.emit('TeaModel.ToObject<');
      this.visitType(ast.expectedType);
      this.emit('>(');
    }

    if (ast.expr && ast.expr.type === 'object' && env && env.returnType && _name(env.returnType) === 'object') {
      env.castToObject = true;
    }
    this.visitExpr(ast.expr, level, env);

    if (ast.needCast) {
      this.emit(')');
    }

    this.emit(';\n');
  }

  visitConstructModel(ast, level, env) {
    assert.equal(ast.type, 'construct_model');
    if (ast.aliasId.isModule) {
      let aliasId = ast.aliasId.lexeme;
      const { namespace } = this.moduleClass.get(aliasId);
      this.emit('new ');
      let tempName = '';
      let type = 'Models';
      const moduleModelName = ast.propertyPath.map((item) => {
        return item.lexeme;
      }).join('.');

      var usedEx;
      if (this.usedExternException) {
        usedEx = this.usedExternException.get(aliasId);
      }
      if (usedEx && usedEx.has(moduleModelName)) {
        type = 'Exceptions';
      }
      for (let i = 0; i < ast.propertyPath.length; i++) {
        const item = ast.propertyPath[i];
        if (i > 0) {
          this.emit('.');
        }
        // Request.RequestSubmodel,先判断Request是否冲突
        if (i === 0) {
          tempName += _upperFirst(item.lexeme);
          // const realModelName = this.getRealModelName(`${namespace}.${type}.${tempName}`, type);
          const realModelName = this.getRealModelName(`${namespace}.${type}`, tempName, type);
          this.emit(realModelName);
        } else {
          tempName += _upperFirst(item.lexeme);
          this.emit(tempName);
        }
      }
    } else if (ast.aliasId.isModel) {
      let mainModelName = ast.aliasId;
      let type = 'Models';
      this.emit('new ');
      const modelName = [mainModelName, ...ast.propertyPath].map((item) => {
        return item.lexeme;
      }).join('.');
      if (_isBuiltinModel(modelName)) {
        const [namespace, subName] = splitByLastDot(modelName);
        const subModelName = this.getRealModelName(namespace, subName);
        this.emit(subModelName);
      } else {
        if (this.predefined[modelName] && this.predefined[modelName].isException) {
          type = 'Exceptions';
        }
        const subModelName = this.getRealModelName(`${this.namespace}.${type}`, modelName, type);
        this.emit(subModelName);
      }
    } else {
      this.emit(`new ${_name(ast.aliasId)}`);
    }
    this.visitConstructObject(ast.object, level, env);
  }

  visitConstructObject(ast, level, env) {
    if (ast && ast.fields && ast.fields.length > 0) {
      this.emit('\n');
      this.emit('{\n', level);
      let i = 0;
      ast.fields.forEach((element) => {
        let comments = DSL.comment.getFrontComments(this.comments, element.tokenRange[0]);
        this.visitComments(comments, level + 1);
        this.emit(_avoidReserveName(_upperFirst(_name(element.fieldName))), level + 1);
        this.emit(' = ');
        this.visitExpr(element.expr, level + 1, env);
        this.emit(',\n');
        i++;
      });
      // find the last item's back comment
      let comments = DSL.comment.getBackComments(this.comments, ast.fields[i - 1].tokenRange[1]);
      this.visitComments(comments, level + 1);
      this.emit('}', level);

    } else {
      this.emit('()');
    }
  }

  visitCall(ast, level, env) {
    assert.equal(ast.type, 'call');
    if (ast.left.type === 'method_call') {
      this.visitMethodCall(ast, level, env);
      let wrapName = _upperFirst(_name(ast.left.id));
      if (!wrapName.startsWith('$') && !this.builtin[wrapName] && 
        ast.inferred && ast.inferred.name && nullableTypes.includes(ast.inferred.name)) {
        this.emit('.Value');
      }
    } else if (ast.left.type === 'instance_call') {
      this.visitInstanceCall(ast, level, env);
    } else if (ast.left.type === 'static_call') {
      this.visitStaticCall(ast, level, env);
    } else {
      throw new Error('un-implemented');
    }
  }

  visitArray(ast, level, env) {
    assert.equal(ast.type, 'array');
    this.emit('new List<');
    this.visitType(ast.inferred.itemType, level, env);
    this.emit('>\n');
    this.emit('{\n', level);
    let item;
    for (let i = 0; i < ast.items.length; i++) {
      item = ast.items[i];
      let comments = DSL.comment.getFrontComments(this.comments, item.tokenRange[0]);
      this.visitComments(comments, level + 1);
      this.emit('', level + 1);
      this.visitExpr(item, level + 1, env);
      if (i < ast.items.length - 1) {
        this.emit(',');
      }
      this.emit('\n');
    }
    if (item) {
      //find the last item's back comment
      let comments = DSL.comment.getBetweenComments(this.comments, item.tokenRange[0], ast.tokenRange[1]);
      this.visitComments(comments, level + 1);
    }
    this.emit('}', level);
  }

  visitConstruct(ast, level, env) {
    assert.equal(ast.type, 'construct');
    this.emit('new ');
    if (_name(ast.aliasId) in builtinMap) {
      this.emit(`${builtinMap[_name(ast.aliasId)]}`);
    } else {
      let clientName = this.getRealClientName(_name(ast.aliasId));
      this.emit(clientName);
    }
    this.emit('(');
    for (let i = 0; i < ast.args.length; i++) {
      this.visitExpr(ast.args[i], level, env);
      if (i !== ast.args.length - 1) {
        this.emit(', ');
      }
    }
    this.emit(')');
  }

  visitInstanceCall(ast, level, env) {
    assert.equal(ast.left.type, 'instance_call');
    const method = _name(ast.left.propertyPath[0]);
    let methodName = _upperFirst(method);

    if (ast.builtinModule && this.builtin[ast.builtinModule] && this.builtin[ast.builtinModule][method]) {
      this.builtin[ast.builtinModule][method](ast, level, env);
    } else {
      if (env.isAsyncMode && ast.isAsync) {
        methodName += 'Async';
        this.emit('await ');
      }
      if (ast.left.id.tag === DSL.Tag.Tag.VID) {
        this.emit(`this.${_vid(ast.left.id)}`);
      } else {
        this.emit(`${_avoidReserveName(_name(ast.left.id))}`);
      }
      this.emit(`.${methodName}(`);

      for (let i = 0; i < ast.args.length; i++) {
        const expr = ast.args[i];
        if (expr.type === 'variable' && expr.needCast) {
          env.groupOp = true;
          this.visitExpr(expr, level, env);
          this.emit('.ToMap()');
        } else {
          this.visitExpr(expr, level, env);
        }
        if (i !== ast.args.length - 1) {
          this.emit(', ');
        }
      }
      this.emit(')');
    }
  }

  visitStaticCall(ast, level, env) {
    assert.equal(ast.left.type, 'static_call');
    if (ast.left.id.type === 'builtin_module') {
      this.visitBuiltinStaticCall(ast, level, env);
      return;
    }

    const aliasId = _name(ast.left.id);
    let realClsName = this.getRealClientName(aliasId);
    this.emit(`${realClsName ? `${realClsName}` : ''}.${_upperFirst(_name(ast.left.propertyPath[0]))}`);
    this.visitArgs(ast, level, env);
  }

  visitBuiltinStaticCall(ast, level, env) {
    const moduleName = _name(ast.left.id);
    const builtiner = this.builtin[moduleName];
    if (!builtiner) {
      throw new Error('un-implemented');
    }
    const func = _name(ast.left.propertyPath[0]);
    builtiner[func](ast, level, env);
  }

  visitArgs(ast, level, env) {
    this.emit('(');
    for (let i = 0; i < ast.args.length; i++) {
      const expr = ast.args[i];
      if (expr.needCast) {
        env.groupOp = true;
        this.visitExpr(expr, level, env);
        this.emit('.ToMap()');
      } else {
        this.visitExpr(expr, level, env);
      }
      if (i !== ast.args.length - 1) {
        this.emit(', ');
      }
    }
    this.emit(')');
  }

  visitMethodCall(ast, level, env) {
    assert.equal(ast.left.type, 'method_call');
    let wrapName = _upperFirst(_name(ast.left.id));
    if (wrapName.startsWith('$') && this.builtin[wrapName]) {
      const method = wrapName.replace('$', '');
      this.builtin[wrapName][method](ast, level, env);
      return;
    }
    if (env.isAsyncMode && ast.isAsync) {
      this.emit('await ');
      wrapName += 'Async';
    }
    if (ast.isStatic) {
      this.emit(`${wrapName}(`);
    } else {
      this.emit(`${wrapName}(`);
    }
    for (let i = 0; i < ast.args.length; i++) {
      const expr = ast.args[i];

      if (expr.needCast) {
        env.groupOp = true;
        this.visitExpr(expr, level, env);
        this.emit('.ToMap()');
      } else {
        env.groupOp = false;
        this.visitExpr(expr, level, env);
      }
      if (i !== ast.args.length - 1) {
        this.emit(', ');
      }
    }
    this.emit(')');
  }

  visitIf(ast, level, env) {
    assert.equal(ast.type, 'if');
    this.emit('if (', level);
    this.visitExpr(ast.condition, level + 1, env);
    if (ast.condition.type === 'variable') {
      this.emit('.Value');
    }
    this.emit(')\n');
    this.emit('{\n', level);
    this.visitStmts(ast.stmts, level + 1, env);
    this.emit('}\n', level);

    if (ast.elseIfs) {
      ast.elseIfs.forEach((branch) => {
        this.emit('else if (', level);
        env.groupOp = false;
        this.visitExpr(branch.condition, level, env);
        this.emit(')\n');
        this.emit(`{\n`, level);
        this.visitStmts(branch.stmts, level + 1, env);
        this.emit('}\n', level);
      });
    }

    if (ast.elseStmts) {
      this.emit('else\n', level);
      this.emit('{\n', level);
      for (let i = 0; i < ast.elseStmts.stmts.length; i++) {
        this.visitStmt(ast.elseStmts.stmts[i], level + 1, env);
      }
      if (ast.elseStmts.stmts.length === 0) {
        const comments = DSL.comment.getBetweenComments(this.comments, ast.elseStmts.tokenRange[0], ast.elseStmts.tokenRange[1]);
        this.visitComments(comments, level + 1);
      }
      this.emit('}\n', level);
    }
  }

  visitAssign(ast, level, env) {
    let leftName;
    leftName = ast.left.id && _name(ast.left.id);
    if (leftName === '__response') {
      leftName = RESPONSE;
    } else if (leftName === '__request') {
      leftName = REQUEST;
    } else {
      leftName = _avoidReserveName(leftName);
    }
    if (ast.left.type === 'id') {
      this.emit(`${leftName}`, level);
    } else if (ast.left.type === 'property_assign' || ast.left.type === 'property') {
      this.emit(`${leftName}`, level);
      let isMapNext = ast.left.id.inferred && ast.left.id.inferred.type === 'map';
      for (let i = 0; i < ast.left.propertyPath.length; i++) {
        if (!isMapNext) {
          this.emit(`.${_upperFirst(_name(ast.left.propertyPath[i]))}`);
        } else {
          this.emit(`["${_name(ast.left.propertyPath[i])}"]`);
        }
        isMapNext = ast.left.propertyPathTypes[i].type === 'map';
      }
    } else if (ast.left.type === 'virtualVariable') { // vid
      this.emit(`this._${_lowerFirst(_name(ast.left.vid).substr(1))}`, level);
    } else if (ast.left.type === 'variable') {
      this.emit(`${leftName}`, level);
    } else if (ast.left.type === 'array_access') {
      this.visitArrayAccess(ast.left, level, env, true);
    } else if (ast.left.type === 'map_access') {
      let expr;
      if (ast.left.id.tag === DSL.Tag.Tag.VID) {
        expr = `this.${_vid(ast.left.id)}`;
      } else {
        expr = `${_name(ast.left.id)}`;
      }
      if (ast.left.propertyPath && ast.left.propertyPath.length) {
        var current = ast.left.id.inferred;
        for (let i = 0; i < ast.left.propertyPath.length; i++) {
          var name = _name(ast.left.propertyPath[i]);
          if (current.type === 'model') {
            expr += `.${_upperFirst(name)}`;
          } else {
            expr += `["${name}"]`;
          }
          current = ast.left.propertyPathTypes[i];
        }
      }
      this.emit(`${expr}[`, level);
      this.visitExpr(ast.left.accessKey, level);
      this.emit(`]`);
    } else {
      throw new Error('unimpelemented');
    }

    this.emit(' = ');
    if (ast.expr.needToReadable) {
      this.emit('DaraCore.BytesReadable(');
    }
    if (ast.expr.type === 'object') {
      // paser give error inferred
      let expr = ast.expr;
      expr.inferred = ast.left.inferred;
      this.visitExpr(expr, level, env);
    } else {
      this.visitExpr(ast.expr, level, env);
    }

    if (ast.expr.needToReadable) {
      this.emit(')');
    }
    this.emit(';\n');
  }

  visitThrow(ast, level, env) {
    this.emit('throw ', level);
    if (ast.expr.type === 'construct_model') {
      this.visitConstructModel(ast.expr, level, env);
      this.emit(';\n');
    } else {
      this.emit(`new TeaException(`);
      this.used.push('Tea');
      this.visitObject(ast.expr, level);
      this.emit(');\n');
    }
  }


  visitObject(ast, level, env) {
    assert.equal(ast.type, 'object');
    if (ast.fields.length === 0) {
      this.emit(`new `);
      this.visitType(ast.inferred, level, env);
      this.emit('()');
      let comments = DSL.comment.getBetweenComments(this.comments, ast.tokenRange[0], ast.tokenRange[1]);
      if (comments.length > 0) {
        this.emit('\n');
        this.emit('{\n', level);
        this.visitComments(comments, level + 1);
        this.emit('}', level);
      } else {
        this.emit('{}');
      }
      return;
    }

    var hasExpandField = false;
    for (let i = 0; i < ast.fields.length; i++) {
      const field = ast.fields[i];
      if (field.type === 'expandField') {
        hasExpandField = true;
        break;
      }
    }

    if (!hasExpandField) {
      this.emit('new ');
      this.visitType(ast.inferred, level, env);
      this.emit('\n');
      this.emit('{\n', level);

      for (var i = 0; i < ast.fields.length; i++) {
        this.visitObjectField(ast.fields[i], level + 1, env);
      }
      //find the last item's back comment
      let comments = DSL.comment.getBetweenComments(this.comments, ast.fields[i - 1].tokenRange[0], ast.tokenRange[1]);
      this.visitComments(comments, level + 1);
      this.emit('}', level);
      return;
    }

    var all = [];
    // 分段
    var current = [];
    for (let i = 0; i < ast.fields.length; i++) {
      const field = ast.fields[i];
      if (field.type === 'objectField') {
        current.push(field);
      } else {
        if (current.length > 0) {
          all.push(current);
        }
        all.push(field);
        current = [];
      }
    }

    if (current.length > 0) {
      all.push(current);
    }

    this.emit(`ConverterUtil.Merge<${_type(ast.inferred.valueType.name)}>\n`);
    this.emit('(\n', level);
    for (let i = 0; i < all.length; i++) {
      const item = all[i];
      if (Array.isArray(item)) {
        this.emit(`new Dictionary<${_type(ast.inferred.keyType.name)}, ${_type(ast.inferred.valueType.name)}>()\n`, level + 1);
        this.emit('{\n', level + 1);
        for (var j = 0; j < item.length; j++) {
          this.visitObjectField(item[j], level + 2, env);
        }
        this.emit('}', level + 1);
      } else {
        this.emit('', level + 1);
        this.visitExpr(item.expr, level + 1, env);
      }
      if (i < all.length - 1) {
        this.emit(',');
      }
      this.emit('\n');
    }
    this.emit(')', level);
  }

  visitObjectField(ast, level, env) {
    let comments = DSL.comment.getFrontComments(this.comments, ast.tokenRange[0]);
    this.visitComments(comments, level);
    if (ast.type === 'objectField') {
      var key = _name(ast.fieldName) || _string(ast.fieldName);
      this.emit(`{"${key}", `, level);
      this.visitObjectFieldValue(ast.expr, level, env);
    } else if (ast.type === 'expandField') {
      // // TODO: more cases
      // this.emit(`...`, level);
      // this.visitExpr(ast.expr, level);
    } else {
      throw new Error('unimpelemented');
    }
    this.emit('},\n');
  }

  visitObjectFieldValue(ast, level, env) {
    this.visitExpr(ast, level, env);
  }

  visitPropertyAccess(ast, level, env) {
    assert.equal(ast.type, 'property_access');

    var id = _name(ast.id);

    var expr = '';
    if (id === '__response') {
      expr += RESPONSE;
    } else if (id === '__request') {
      expr += REQUEST;
    } else {
      expr += _avoidReserveName(id);
    }

    var current = ast.id.inferred;
    for (var i = 0; i < ast.propertyPath.length; i++) {
      var name = _name(ast.propertyPath[i]);
      if (current.type === 'model') {
        expr += `.${_upperFirst(name)}`;
      } else if (current.type === 'map') {
        expr += `.Get("${name}")`;
      } else {
        expr += `["${name}"]`;
      }
      current = ast.propertyPathTypes[i];
    }

    this.emit(expr);
  }

  visitReturn(ast, level, env) {
    assert.equal(ast.type, 'return');
    this.emit('return ', level);
    if (!ast.expr) {
      this.emit(';\n');
      return;
    }
    if (ast.needCast) {
      this.emit('TeaModel.ToObject<');
      this.visitType(ast.expectedType);
      this.emit('>(');
    }

    if (ast.expr && ast.expr.type === 'object' && env && env.returnType && _name(env.returnType) === 'object') {
      env.castToObject = true;
    }
    this.visitExpr(ast.expr, level, env);

    if (ast.needCast) {
      this.emit(')');
    }

    this.emit(';\n');
  }

  visitRetry(ast, level) {
    assert.equal(ast.type, 'retry');
    this.emit(`throw new DaraRetryableException(${REQUEST}, ${RESPONSE});\n`, level);
  }

  visitWhile(ast, level, env) {
    assert.equal(ast.type, 'while');
    this.emit('\n');
    this.emit('while (', level);
    this.visitExpr(ast.condition, level + 1, env);
    this.emit(') {\n');
    this.visitStmts(ast.stmts, level + 1, env);
    this.emit('}\n', level);
  }

  visitFor(ast, level, env) {
    assert.equal(ast.type, 'for');
    this.emit('\n');
    this.emit(`foreach (var ${_name(ast.id)} in `, level);
    this.visitExpr(ast.list, level + 1, env);
    this.emit(') {\n');
    this.visitStmts(ast.stmts, level + 1, env);
    this.emit('}\n', level);
  }

  visitExtendOn(extendOn, type = 'model') {
    if (!extendOn) {
      if (type === 'model') {
        return this.emit('TeaModel');
      }
      this.emit('TeaException');
      this.used.push('Tea');
      return;
    }
    let namespace = this.namespace;
    let modelName = _name(extendOn);
    let extendType = 'Models';
    if (this.predefined[modelName] && this.predefined[modelName].isException) {
      extendType = 'Exceptions';
    }
    if (extendOn.type === 'moduleModel') {
      const [moduleId, ...rest] = extendOn.path;
      namespace = this.moduleClass.get(moduleId.lexeme).namespace;
      modelName = rest.map((item) => {
        return item.lexeme;
      }).join('.');
      const usedEx = this.usedExternException.get(moduleId.lexeme);
      if (usedEx && usedEx.has(modelName)) {
        type = 'Exceptions';
      }
    } else if (extendOn.type === 'subModel') {
      const [moduleId, ...rest] = extendOn.path;
      modelName = [moduleId.lexeme, ...rest.map((item) => {
        return item.lexeme;
      })].join('.');
    } else if (extendOn.idType === 'builtin_model') {
      if (extendOn.lexeme === '$Error') {
        this.emit(this.getRealModelName('Tea', 'TeaException'));
      }
    } else {
      if (extendOn.moduleName) {
        this.emit(`${extendOn.moduleName}.`);
      }
      this.emit(this.getRealModelName(`${namespace}.${extendType}`, modelName, extendType));
    }
  }

  visitEcxceptionBody(ast, level, exceptionName, env) {
    assert.equal(ast.type, 'exceptionBody');
    let node;
    for (let i = 0; i < ast.nodes.length; i++) {
      node = ast.nodes[i];
      let comments = DSL.comment.getFrontComments(this.comments, node.tokenRange[0]);
      this.visitComments(comments, level);
      this.emit('public ', level);
      // 不用new也不会编译报错
      // if (_isSuperField(_escape(_name(node.fieldName)))) {
      //   this.emit('new ');
      // }
      this.visitFieldType(node.fieldValue, level, exceptionName, _name(node.fieldName));
      // this.emit(`${node.required ? '' : '?'} `);
      this.emit(` ${_escape(_upperFirst(_name(node.fieldName)))}`);
      this.emit(' { get; set; }\n');
      if (node.fieldValue.type === 'modelBody') {
        this.emit(`public class ${exceptionName}${_upperFirst(node.fieldName.lexeme)} : TeaModel\n`, level);
        this.emit('{\n', level);
        this.visitModelBody(node.fieldValue, level + 1, env, exceptionName + _upperFirst(node.fieldName.lexeme));
        this.emit('}\n', level);
      }
    }
    if (node) {
      //find the last node's back comment
      let comments = DSL.comment.getBetweenComments(this.comments, node.tokenRange[0], ast.tokenRange[1]);
      this.visitComments(comments, level);
    }

    if (ast.nodes.length === 0) {
      //empty block's comment
      let comments = DSL.comment.getBetweenComments(this.comments, ast.tokenRange[0], ast.tokenRange[1]);
      this.visitComments(comments, level);
    }
  }

  visitEcxceptionConstrutor(ast, level) {
    assert.equal(ast.type, 'exceptionBody');
    let node;
    for (let i = 0; i < ast.nodes.length; i++) {
      node = ast.nodes[i];
      this.emit(`${_escape(_name(node.fieldName))} = DictUtils.GetDicValue(map, "${_escape(_name(node.fieldName))}")`, level);
      if (node.fieldValue.fieldType === 'string') {
        this.emit('.ToSafeString();\n');
      } else {
        this.emit(';\n');
      }
    }
  }

  visitExceptions(ast, filepath, level) {
    const exceptions = ast.moduleBody.nodes.filter((item) => {
      return item.type === 'exception';
    });
    const exDir = path.join(path.dirname(filepath), 'Exceptions');
    for (let i = 0; i < exceptions.length; i++) {
      this.used.push('System');
      this.used.push('System.IO');
      this.used.push('System.Collections');
      this.used.push('System.Collections.Generic');
      const exceptionName = _avoidReserveName(exceptions[i].exceptionName.lexeme);
      const realExceptionName = `${exceptionName}Exception`;
      this.fileName = realExceptionName;
      this.eachException(exceptions[i], realExceptionName, level + 1);
      this.exceptionAfter();
      const modelFilePath = path.join(exDir, `${this.fileName ? this.fileName : exceptionName}.cs`);
      this.save(modelFilePath);
    }
  }

  visitException(ast, exceptionName, extendOn, level, env) {
    this.emit(`public class ${exceptionName} : `, level);
    this.visitExtendOn(extendOn, 'exception');
    this.emit(' {\n');
    this.visitEcxceptionBody(ast.exceptionBody, level + 1, exceptionName, env);
    // TODO TeaException属性能否提供set方法，继承TeaException时就可以用无参构造函数并且属性赋值也更美观。
    // this.emit(`public ${exceptionName}(Dictionary<string, object> map) : base(map)\n`, level + 1);
    // this.emit('{\n', level + 1);
    // this.visitEcxceptionConstrutor(exceptionBody, level + 2);
    // this.emit(`}\n`, level + 1);
    this.emit(`\n`);
    for (let i = 0; i < ast.exceptionBody.nodes.length; i++) {
      const node = ast.exceptionBody.nodes[i];
      if (node.fieldValue.type === 'modelBody') {
        this.emit(`public class ${exceptionName}${_upperFirst(node.fieldName.lexeme)} : TeaModel\n`, level + 1);
        this.emit('{\n', level + 1);
        this.visitModelBody(node.fieldValue, level + 2, env, exceptionName + _upperFirst(node.fieldName.lexeme));
        this.emit('}\n', level + 1);
      }
    }
    this.emit(`public ${exceptionName}() : base()\n`, level + 1);
    this.emit(`{\n`, level + 1);
    this.emit(`}\n`, level + 1);
    this.emit('}\n\n', level);
  }

  /*******************************************************/
  eachException(exception, realExceptionName, level, env) {
    assert.equal(exception.type, 'exception');
    const exceptionName = realExceptionName ? realExceptionName : _avoidReserveName(_name(exception.exceptionName));
    this.visitAnnotation(exception.annotation, level);
    let comments = DSL.comment.getFrontComments(this.comments, exception.tokenRange[0]);
    this.visitComments(comments, level);
    this.visitException(exception, exceptionName, exception.extendOn, level, env);
  }

  eachModel(ast, modelName, level, predefined) {
    assert.equal(ast.type, 'model');
    const env = {
      predefined
    };
    // const modelName = _upperFirst(_name(ast.modelName));
    this.emit(`public class ${modelName} : `, level);
    this.visitExtendOn(ast.extendOn);
    this.emit(' {\n');
    this.visitModelBody(ast.modelBody, level + 1, env, modelName);
    this.emit('}\n\n', level);
  }

  eachAPI(ast, level, env) {
    this.visitAnnotation(ast.annotation, level);
    let comments = DSL.comment.getFrontComments(this.comments, ast.tokenRange[0]);
    this.visitComments(comments, level);
    this.emit('public ', level);
    let apiName = _upperFirst(_name(ast.apiName));
    if (env.isAsyncMode) {
      this.emit('async ');
      apiName += 'Async';
    }
    env.returnType = ast.returnType;
    this.visitReturnType(ast, 0, env);
    this.emit(apiName);
    this.visitParams(ast.params, level, env);
    this.emit('\n');
    this.emit('{\n', level);

    // Validator
    // for (var i = 0; i < ast.params.params.length; i++) {
    //   const param = ast.params.params[i];
    //   if (_name(param.paramType) && !DSL.util.isBasicType(_name(param.paramType))) {
    //     this.emit(`${_avoidReserveName(param.paramName.lexeme)}.Validate();\n`, level + 1);
    //   }
    // }

    let baseLevel = ast.runtimeBody ? level + 2 : level;
    // api level
    if (ast.runtimeBody) {
      this.visitRuntimeBefore(ast.runtimeBody, level + 1, env);
    }

    // temp level
    this.visitAPIBody(ast.apiBody, baseLevel + 1, env);

    if (ast.runtimeBody) {
      this.emit(`_lastRequest = ${REQUEST};\n`, baseLevel + 1);
    }
    if (env.isAsyncMode) {
      this.emit(`DaraResponse ${RESPONSE} = await DaraCore.DoActionAsync(${REQUEST}`, baseLevel + 1);
    } else {
      this.emit(`DaraResponse ${RESPONSE} = DaraCore.DoAction(${REQUEST}`, baseLevel + 1);
    }


    if (ast.runtimeBody) {
      this.emit(', runtime_');
    }
    this.emit(');\n');

    if (ast.returns) {
      this.visitReturnBody(ast.returns, baseLevel + 1, env);
    } else {
      this.visitDefaultReturnBody(baseLevel + 1, env);
    }

    if (ast.runtimeBody) {
      this.visitRuntimeAfter(ast.runtimeBody, level + 1);
    }

    this.emit('}\n', level);
  }

  importBefore(level) {
    // Nothing
  }

  eachImport(imports, usedModels, innerModule, filepath, level) {
    this.imports = {};
    if (imports.length === 0) {
      return;
    }

    if (!this.config.pkgDir) {
      throw new Error(`Must specific pkgDir when have imports`);
    }

    let lock;
    const lockPath = path.join(this.config.pkgDir, '.libraries.json');
    if (fs.existsSync(lockPath)) {
      lock = JSON.parse(fs.readFileSync(lockPath, 'utf8'));
    }

    for (let i = 0; i < imports.length; i++) {
      const item = imports[i];
      const aliasId = item.lexeme;
      let moduleDir;
      if (this.config.libraries) {
        moduleDir = this.config.libraries[aliasId];
      }
      const innerPath = item.innerPath;
      const importNameSpace = _nameSpace(path.join(path.dirname(filepath), _upperPath(innerPath)));

      if (!moduleDir && innerPath) {
        let csPath = innerPath.replace(/(\.tea)$|(\.spec)$|(\.dara)$/gi, '');
        innerModule.set(aliasId, `${csPath}.cs`);
        const className = this.getInnerClient(aliasId, csPath);  // Common,Util,User 
        const aliasName = this.getAliasName(`${this.namespace}.${importNameSpace}`, className, aliasId);
        this.moduleClass.set(aliasId, {
          namespace: `${this.namespace}.${importNameSpace}`.replace(/\.$/, ''),
          className: className,
          aliasName: aliasName
        });
        this.imports[aliasId] = {
          namespace: `${this.namespace}.${importNameSpace}`.replace(/\.$/, ''),
          release: '',
          className: aliasId
        };
        continue;
      }
      let targetPath = '';
      if (moduleDir.startsWith('./') || moduleDir.startsWith('../')) {
        targetPath = path.join(this.config.pkgDir, moduleDir);
      } else if (moduleDir.startsWith('/')) {
        targetPath = moduleDir;
      } else {
        targetPath = path.join(this.config.pkgDir, lock[moduleDir]);
      }
      const pkgPath = fs.existsSync(path.join(targetPath, 'Teafile')) ? path.join(targetPath, 'Teafile') : path.join(targetPath, 'Darafile');
      const pkg = JSON.parse(fs.readFileSync(pkgPath));
      let csharpConfig = pkg.csharp;
      pkg.releases = pkg.releases || {};
      if (!csharpConfig) {
        throw new Error(`The '${aliasId}' has no csharp supported.`);
      }
      csharpConfig.release = pkg.releases.csharp;
      this.imports[aliasId] = csharpConfig;
      const className = csharpConfig.className || 'Client';
      const aliasName = this.getAliasName(`${csharpConfig.namespace}`, className, aliasId);
      this.moduleClass.set(aliasId, {
        namespace: `${csharpConfig.namespace}`,
        className: className,
        aliasName: aliasName
      });

      this.moduleTypedef[aliasId] = csharpConfig.typedef;
    }
  }

  importAfter() {
    // Nothing
  }

  moduleBefore() {
    // Nothing
  }

  modelAfter() {
    this.emit('}\n');
  }

  exceptionAfter() {
    this.emit('}\n');
  }

  interfaceEachAPI(ast, level) {
    this.visitAnnotation(ast.annotation, level);
    let comments = DSL.comment.getFrontComments(this.comments, ast.tokenRange[0]);
    this.visitComments(comments, level);
    let apiName = _upperFirst(_name(ast.apiName));
    let env = {};
    env.returnType = ast.returnType;
    this.emit('', level);
    this.visitReturnType(ast, 0, env);
    this.emit(apiName);
    this.visitParams(ast.params, level, env);
    this.emit(';\n');
  }

  InterfaceEachFunction(ast, level) {
    let wrapName = _upperFirst(_name(ast.functionName));
    this.visitAnnotation(ast.annotation, level);
    let comments = DSL.comment.getFrontComments(this.comments, ast.tokenRange[0]);
    this.visitComments(comments, level);

    this.emit('', level);
    let env = {};
    env.returnType = ast.returnType;
    this.visitReturnType(ast, 0, env);
    this.emit(`${wrapName}`);
    if (this.isExec && wrapName === 'Main') {
      this.emit('(string[] args)');
    } else {
      this.visitParams(ast.params, level, env);
    }
    this.emit(';\n');

  }

  interfaceAfter(level) {
    this.emit('}\n', level);
    this.emit('}\n');
  }

  apiBefore(level, extendParam, filepath, main) {
    this.used.push('System');
    this.used.push('System.IO');
    this.used.push('System.Collections');
    this.used.push('System.Collections.Generic');
    this.used.push('System.Threading.Tasks');
    // this.used.push('Darabonba');
    // this.used.push('Darabonba.Utils');
    this.used.push('Tea');
    this.used.push('Tea.Utils');
    let className = this.className;
    const fileInfo = path.parse(filepath);
    if (!main) {
      const beginNotes = DSL.note.getNotes(this.notes, 0, this.ast.moduleBody.nodes[0].tokenRange[0]);
      const clientNote = beginNotes.find(note => note.note.lexeme === '@clientName');
      if (clientNote) {
        className = clientNote.arg.value.string;
      } else {
        className = `${_upperFirst(fileInfo.name.toLowerCase())}Client`;
      }
    }
    this.emit(`public class ${className} `, level + 1);

    if (extendParam.extend && extendParam.extend.length > 0) {
      const extendsModuleName = this.getRealClientName(this.ast.extends.lexeme);
      const extendsInterfaces = extendParam.extend.filter(item => item.startsWith('I'));
      this.emit(`: ${extendsModuleName}${extendsInterfaces.join(', ') === '' ? '' : ', '}${extendsInterfaces.join(', ')}
    {
`);
    } else {
      this.emit(`
    {
`);
    }

  }

  init(level) {
    // Nothing
  }

  apiAfter() {
    // Nothing
  }

  wrapBefore() {

  }

  eachFunction(ast, level, env) {
    let wrapName = _upperFirst(_name(ast.functionName));
    if (wrapName === 'Main' && env.isAsyncMode && !(this.isExec && this.asyncOnly)) {
      return;
    }
    this.visitAnnotation(ast.annotation, level);
    let comments = DSL.comment.getFrontComments(this.comments, ast.tokenRange[0]);
    this.visitComments(comments, level);
    this.emit('public ', level);
    if (ast.isStatic) {
      this.emit('static ');
    }
    if (env.isAsyncMode) {
      this.emit('async ');
      if (wrapName !== 'Main') {
        wrapName += 'Async';
      }
    }

    env.returnType = ast.returnType;
    this.visitReturnType(ast, 0, env);
    this.emit(`${wrapName}`);
    if (this.isExec && wrapName === 'Main') {
      this.emit('(string[] args)');
    } else {
      this.visitParams(ast.params, level, env);
    }

    this.emit('\n');
    this.emit('{\n', level);
    if (ast.functionBody) {
      this.visitWrapBody(ast.functionBody, level + 1, env);
    } else {
      this.emit('throw new NotImplementedException();\n', level + 1);
    }
    this.emit('}\n', level);
  }

  wrapAfter() {
    // Nothing
  }

  moduleAfter() {
    this.emit(`
    }
}\n`, 0);
  }

  isIterator(returnType) {
    if (returnType.type === 'iterator' || returnType.type === 'asyncIterator') {
      return true;
    }
    return false;
  }

  visitWrapBody(ast, level, env) {
    assert.equal(ast.type, 'functionBody');
    this.visitStmts(ast.stmts, level, env);
  }

  visitInit(ast, types, main, filepath, level, env) {
    assert.equal(ast.type, 'init');
    types.forEach((item) => {
      let comments = DSL.comment.getFrontComments(this.comments, item.tokenRange[0]);
      this.visitComments(comments, level + 2);
      this.emit('protected ', level + 2);
      if (item.value.type || item.value.idType) {
        this.visitType(item.value);
        this.emit(' ');
      } else if (this.imports[_name(item.value)]) {
        const realClsName = this.getRealClientName(_name(item.value));
        this.emit(`${realClsName || 'Client'} `);
      } else {
        this.emit(`${_type(_name(item.value))} `);
      }
      this.emit(`${_vid(item.vid)};\n`);
    });

    this.emit('\n');
    this.visitAnnotation(ast.annotation, level + 2);
    let comments = DSL.comment.getFrontComments(this.comments, ast.tokenRange[0]);
    this.visitComments(comments, level + 2);
    let className = this.className;
    if (!main) {
      const fileInfo = path.parse(filepath);
      const beginNotes = DSL.note.getNotes(this.notes, 0, this.ast.moduleBody.nodes[0].tokenRange[0]);
      const clientNote = beginNotes.find(note => note.note.lexeme === '@clientName');
      if (clientNote) {
        className = clientNote.arg.value.string;
      } else {
        className = `${_upperFirst(fileInfo.name.toLowerCase())}Client`;
      }
    }
    this.emit(`public ${className || 'Client'}`, level + 2);
    this.visitParams(ast.params);
    // if (ast.initBody && ast.initBody.stmts[0].type === 'super') {
    if (ast.initBody && ast.initBody.stmts[0] && ast.initBody.stmts[0].type === 'super') {
      this.emit(`: base(`);
      for (let i = 0; i < ast.initBody.stmts[0].args.length; i++) {
        if (i > 0) {
          this.emit(', ');
        }
        this.visitExpr(ast.initBody.stmts[0].args[i], level, env);
      }
      this.emit(`)`);
    }
    this.emit('\n');
    this.emit('{\n', level + 2);
    if (ast.initBody) {
      this.visitStmts(ast.initBody, level + 3, {
        isAsyncMode: false
      });
    }
    this.emit('}\n\n', level + 2);
  }

  visitAnnotation(annotation, level) {
    if (!annotation || !annotation.value) {
      return;
    }
    let comments = DSL.comment.getFrontComments(this.comments, annotation.index);
    this.visitComments(comments, level);
    var ast = Annotation.parse(annotation.value);
    var description = ast.items.find((item) => {
      return item.type === 'description';
    });
    var summary = ast.items.find((item) => {
      return item.type === 'summary';
    });
    var _return = ast.items.find((item) => {
      return item.type === 'return';
    });
    var deprecated = ast.items.find((item) => {
      return item.type === 'deprecated';
    });
    var params = ast.items.filter((item) => {
      return item.type === 'param';
    }).map((item) => {
      return {
        name: item.name.id,
        text: item.text.text
      };
    });
    var throws = ast.items.filter((item) => {
      return item.type === 'throws';
    }).map((item) => {
      return item.text.text;
    });

    const deprecatedText = deprecated ? deprecated.text.text : '';
    const summaryText = summary ? summary.text.text : '';
    const descriptionText = description ? description.text.text.trimEnd() : '';
    const returnText = _return ? _return.text.text.trimEnd() : '';
    let hasNextSection = false;

    if (deprecated) {
      this.emit(`/// <term><b>Deprecated</b></term>\n`, level);
      this.emit(`/// \n`, level);
      deprecatedText.trimEnd().split('\n').forEach((line) => {
        this.emit(`/// ${line}\n`, level);
      });
      hasNextSection = true;
    }
    if (summaryText !== '') {
      if (hasNextSection) {
        this.emit(`/// \n`, level);
      }
      this.emit(`/// <term><b>Summary:</b></term>\n`, level);
      this.emit(`/// <summary>\n`, level);
      const summaryTexts = md2Xml(summaryText);
      summaryTexts.split('\n').forEach((line) => {
        this.emit(`/// ${line}\n`, level);
      });
      this.emit(`/// </summary>\n`, level);
      hasNextSection = true;
    }
    if (descriptionText !== '') {
      if (hasNextSection) {
        this.emit(`/// \n`, level);
      }
      this.emit(`/// <term><b>Description:</b></term>\n`, level);
      this.emit(`/// <description>\n`, level);
      const descriptionTexts = md2Xml(descriptionText);
      descriptionTexts.split('\n').forEach((line) => {
        this.emit(`/// ${line}\n`, level);
      });
      this.emit(`/// </description>\n`, level);
      hasNextSection = true;
    }
    if (params.length > 0) {
      if (hasNextSection) {
        this.emit(`/// \n`, level);
      }
      params.forEach((item) => {
        this.emit(`/// <param name="${item.name}">\n`, level);
        item.text.trimEnd().split('\n').forEach((line) => {
          this.emit(`/// ${line}\n`, level);
        });
        this.emit(`/// </param>\n`, level);
      });
      hasNextSection = true;
    }
    if (returnText) {
      if (hasNextSection) {
        this.emit(`/// \n`, level);
      }
      this.emit(`/// <returns>\n`, level);
      returnText.split('\n').forEach((line) => {
        this.emit(`/// ${line}\n`, level);
      });
      this.emit(`/// </returns>\n`, level);
      hasNextSection = true;
    }
    if (throws.length > 0) {
      if (hasNextSection) {
        this.emit(`/// \n`, level);
      }
      throws.forEach((item, index) => {
        this.emit(`/// <term><b>Exception:</b></term>\n`, level);
        item.trimEnd().split('\n').forEach((line) => {
          this.emit(`/// ${line}\n`, level);
        });
        if (index < throws.length - 1) {
          this.emit(`/// \n`, level);
        }
      });
    }
    if (deprecated) {
      this.emit(`[Obsolete("`, level);
      const lines = deprecatedText.trimEnd().split('\n');
      lines.forEach((line, index) => {
        if (index === lines.length - 1) {
          this.emit(`${line}`);
        } else {
          this.emit(`${line}\\n`);
        }
      });
      this.emit(`")]\n`);
    }
  }

  visitComments(comments, level) {
    comments.forEach(comment => {
      this.emit(`${comment.value}`, level);
      this.emit(`\n`);
    });
  }

  visitConsoleCSProj() {
    let csprojPath = path.join(this.csprojOutputDir, (this.config.packageInfo.name || 'client') + '.csproj');
    let json = {};
    json = parse(fs.readFileSync(path.join(__dirname, 'files', 'consoleCsproj.tmpl')));
    //填写包的基本信息
    let propertyGroup = json.Project.PropertyGroup;
    propertyGroup.forEach((property) => {
      if (Object.hasOwn(property, 'RootNamespace')) {
        property['RootNamespace'] = this.config.namespace;
      }
    });

    let dependenciesClass = [];
    Object.keys(this.imports).forEach((key) => {
      dependenciesClass.push(this.imports[key].release);
    });
    //添加对Tea的依赖
    const teaVersion = this.releaseVersion ? this.releaseVersion : '1.1.3';
    dependenciesClass.push(`Tea:${teaVersion}`);

    let currentItemGroup = {};
    dependenciesClass.forEach((item, index) => {
      let dependency = item.split(':');
      currentItemGroup[dependency[0]] = dependency[1];
    });
    const newItemGroup = { ...currentItemGroup, ...this.packageManager };

    //寻找用来写入的itemGroup， 避开有特殊参数的itemGroup
    let itemGroup = {};

    //添加或更新依赖包
    Object.entries(newItemGroup || {}).forEach(([key, value]) => {
      let writeReference = {};
      itemGroup.PackageReference = itemGroup.PackageReference || [];
      writeReference.$ = { 'Include': key, 'Version': value };
      itemGroup.PackageReference.push(writeReference);
    });

    if (this.typedef) {
      Object.keys(this.typedef).map(key => {
        if (!this.typedef[key].package) {
          return;
        }
        dependenciesClass.push(this.typedef[key].package);
      });
    }

    json.Project.ItemGroup = itemGroup;
    const builder = new xml2js.Builder();
    const newCsproj = builder.buildObject(json);
    fs.writeFileSync(csprojPath, Entities.decode(newCsproj));
  }

  visitCSProj() {
    let csprojPath = path.join(this.csprojOutputDir, (this.config.packageInfo.name || 'client') + '.csproj');
    let json = {};
    // 保留本地已有的包
    if (!fs.existsSync(csprojPath)) {
      json = parse(fs.readFileSync(path.join(__dirname, 'files', 'csproj.tmpl')));
    } else {
      json = parse(fs.readFileSync(csprojPath));
    }

    //填写包的基本信息
    let propertyGroup = json.Project.PropertyGroup;
    let assemblyInfo = this.release.split(':');
    propertyGroup.forEach((property) => {
      if (Object.hasOwn(property, 'RootNamespace')) {
        property['RootNamespace'] = this.config.namespace;
        if (this.config.packageInfo && this.config.packageInfo.company) {
          property['Authors'] = this.config.packageInfo.company;
        } else if (this.config.maintainers && this.config.maintainers.name) {
          property['Authors'] = this.config.maintainers.name;
        }
        if (this.config.packageInfo && this.config.packageInfo.description) {
          property['Description'] = this.config.packageInfo.description;
        }
        if (this.config.packageInfo && this.config.packageInfo.property) {
          Object.assign(property, this.config.packageInfo.property);
        }
      }
      if (Object.hasOwn(property, 'AssemblyName')) {
        property['AssemblyName'] = assemblyInfo[0];
      }
    });

    let dependenciesClass = [];
    Object.keys(this.imports).forEach((key) => {
      if (!this.imports[key].release) {
        return;
      }
      dependenciesClass.push(this.imports[key].release);
    });

    if (this.typedef) {
      Object.keys(this.typedef).map(key => {
        if (!this.typedef[key].package) {
          return;
        }
        dependenciesClass.push(this.typedef[key].package);
      });
    }

    //添加对Tea的依赖
    const teaVersion = this.releaseVersion ? this.releaseVersion : '1.1.3';
    dependenciesClass.push(`Tea:${teaVersion}`);

    //寻找用来写入的itemGroup， 避开有特殊参数的itemGroup
    let itemGroup = json.Project.ItemGroup;
    let writeItem = {};
    itemGroup.forEach((item) => {
      if (!item.$) {
        writeItem = item;
      }
    });

    let newDependenciesClass = [];
    if (this.packageManager) {
      const needAddPackages = Object.entries(this.packageManager || {}).map(([key, value]) => `${key}:${value}`);
      const needAddPackageName = new Set(needAddPackages.map(item => item.split(':')[0]));
      newDependenciesClass = [...needAddPackages];
      dependenciesClass.forEach(item => {
        const [pkgName] = item.split(':');
        if (!needAddPackageName.has(pkgName)) {
          newDependenciesClass.push(item);
        }
      });
    } else {
      newDependenciesClass = dependenciesClass;
    }

    //添加或更新依赖包
    newDependenciesClass.forEach((item, index) => {
      if (item) {
        let dependency = item.split(':');
        //遍历所有的itemGroup，判断是否已存在该依赖
        let writeReference = null;
        itemGroup.forEach((group) => {
          if (group.PackageReference) {
            group.PackageReference.forEach((reference) => {
              if (reference.$.Include === dependency[0]) {
                writeReference = reference;
              }
            });
          }
        });
        if (writeReference) {
          writeReference.$.Version = dependency[1];
        } else {
          writeReference = {};
          writeItem.PackageReference = writeItem.PackageReference || [];
          writeReference.$ = { 'Include': dependency[0], 'Version': dependency[1] };
          writeItem.PackageReference.push(writeReference);
        }
      }
    });

    const builder = new xml2js.Builder();
    const newCsproj = builder.buildObject(json);
    fs.writeFileSync(csprojPath, Entities.decode(newCsproj));
  }

  visitAssemblyInfo() {
    let propertiesDir = path.join(this.csprojOutputDir, 'Properties');
    if (!fs.existsSync(propertiesDir)) {
      fs.mkdirSync(propertiesDir, {
        recursive: true
      });
    }
    let assemblyPath = path.join(propertiesDir, 'AssemblyInfo.cs');
    let content = {};
    if (!fs.existsSync(assemblyPath)) {
      content = fs.readFileSync(path.join(__dirname, 'files', 'assemblyInfo.tmpl')).toString();
      let params = {
        title: this.config.packageInfo.title || '',
        description: this.config.packageInfo.description || '',
        company: this.config.packageInfo.company || '',
        product: this.config.packageInfo.product || '',
        guid: UUID.v1(),
        version: this.release.split(':')[1],
        copyRight: this.config.packageInfo.copyRight || '',
      };

      if (content !== '') {
        content = render(content, params);
      }
      fs.writeFileSync(assemblyPath, content);
    } else {
      content = fs.readFileSync(assemblyPath).toString();
      content = content.replace(/AssemblyVersion\("[\S\s].*?"\)/, `AssemblyVersion("${this.release.split(':')[1]}.0")`);
      content = content.replace(/AssemblyFileVersion\("[\S\s].*?"\)/, `AssemblyFileVersion("${this.release.split(':')[1]}.0")`);
      fs.writeFileSync(assemblyPath, content);
    }

  }
}

module.exports = Visitor;