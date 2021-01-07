/* eslint-disable complexity */
'use strict';

const assert = require('assert');

const path = require('path');
const fs = require('fs');
const DSL = require('@darabonba/parser');
const xml2js = require('xml2js');
const Entities = require('html-entities').XmlEntities;
var UUID = require('uuid');
const { Tag } = DSL.Tag;

const REQUEST = 'request_';
const RESPONSE = 'response_';
const RUNTIME = 'runtime_';

const attrList = { 'maxLength': 'value', 'pattern': 'string' };

const {
  _name, _upperFirst, _string, _type, _lowerFirst,
  _avoidReserveName, remove, _vid, parse, render
} = require('./helper');

function getAttr(node, attrName) {
  for (let i = 0; i < node.attrs.length; i++) {
    if (_name(node.attrs[i].attrName) === attrName) {
      return node.attrs[i].attrValue.string || node.attrs[i].attrValue.lexeme;
    }
  }
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
    this.namespace = option.namespace;
    this.className = option.className || 'Client';
    this.output = '';
    this.outputDir = this.config.outputDir + '/core';
    this.csprojOutputDir = option.outputDir + '/core';
    this.release = option.releases && option.releases.csharp || this.namespace + ':0.0.1';
    this.config.packageInfo = this.config.packageInfo || {};
    this.isExec = this.config.exec;

    if (!fs.existsSync(this.outputDir)) {
      fs.mkdirSync(this.outputDir, {
        recursive: true
      });
    }

    remove(path.join(this.outputDir, 'Models/'));
    remove(path.join(this.outputDir, `I${this.className}.cs`));
  }

  visit(ast, level = 0) {
    this.visitModule(ast, level);
  }

  save(filepath) {
    const targetPath = path.join(this.outputDir, filepath);
    fs.mkdirSync(path.dirname(targetPath), {
      recursive: true
    });

    fs.writeFileSync(targetPath, this.output);
    this.output = '';

    if(this.isExec){
      this.visitConsoleCSProj();
    } else {
      this.visitCSProj();
      this.visitAssemblyInfo();
    }
    
  }

  emit(str, level) {
    this.output += ' '.repeat(level * 4) + str;
  }

  visitModule(ast, level) {
    assert.equal(ast.type, 'module');
    this.predefined = ast.predefined;
    this.comments = ast.comments;
    this.importBefore(level);
    this.eachImport(ast.imports, ast.usedExternModel, level);

    this.importAfter();

    this.visitAnnotation(ast.annotation, level);

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
    for (let i = 0; i < models.length; i++) {
      const modelName = _upperFirst(models[i].modelName.lexeme);
      this.modelBefore();
      this.eachModel(models[i], level + 1, ast.predefined);
      this.modelAfter();
      this.save('Models/' + modelName + '.cs');
    }

    //interface
    if (this.config.packageInfo.interface) {
      extendParam.extend = [...extendParam.extend, `I${this.className}`];

      this.interfaceBefore(level, extendParam);

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

      this.interfaceAfter();
      this.save(`I${this.className}.cs`);
    }

    // Api
    this.apiBefore(level, extendParam);

    const types = ast.moduleBody.nodes.filter((item) => {
      return item.type === 'type';
    });
    const inits = ast.moduleBody.nodes.filter((item) => {
      return item.type === 'init';
    });
    const [init] = inits;
    if (init) {
      this.visitInit(init, types, level);
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

      this.eachFunction(functions[i], level + 2, {
        isAsyncMode: false
      });

      if (functions[i].isAsync) {
        this.emit('\n');
        this.eachFunction(functions[i], level + 2, {
          isAsyncMode: true
        });
      }
    }

    this.wrapAfter();

    this.moduleAfter();
    this.save(this.config.clientPath);
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

      for (let j = 0; j < node.attrs.length; j++) {
        if (_name(node.attrs[j].attrName) === 'description') {
          const tag = node.attrs[j].attrValue['string'];
          const strs = tag.split('\n');
          this.emit('/// <summary>\n', level);
          for (let k = 0; k < strs.length; k++) {
            this.emit(`/// ${strs[k]}\n`, level);
          }
          this.emit('/// </summary>\n', level);
        }
      }

      this.emitAnnotation(node, attrList, level);


      var publicDeclar = 'public ';
      if (node.fieldValue.fieldType === 'array' && node.fieldValue.fieldItemType.nodes !== undefined) {
        const subModelName = modelName + _upperFirst(paramName);
        this.emit(publicDeclar, level);
        this.emit(`List<${subModelName}>`);
        this.emit(` ${_upperFirst(paramName)} { get; set; }`);
        this.emit('\n');
        this.emit(`public class ${subModelName} : TeaModel {\n`, level);
        this.visitModelBody(node.fieldValue.fieldItemType, level + 1, env, subModelName);
        this.emit('}\n', level);
      } else if (node.fieldValue.fieldType === 'array') {
        this.emit(publicDeclar, level);
        this.emit('List<');
        this.visitType(node.fieldValue.fieldItemType);
        this.emit('>');
        this.emit(` ${paramName} { get; set; }`);
        this.emit('\n');
      } else if (node.fieldValue.fieldType === 'map') {
        this.emit(publicDeclar, level);
        this.visitType(node.fieldValue);
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
          let csConfig = this.imports[_name(node.fieldValue.fieldType)];
          this.emit(`${csConfig.namespace}.${csConfig.className || 'Client'}`);
        } else if (node.fieldValue.fieldType.type === 'moduleModel') {
          let paths = node.fieldValue.fieldType.path;
          paths.forEach((item, index) => {
            if (index !== 0) {
              this.emit('.');
            }
            if (item.idType === 'module') {
              let csConfig = this.imports[_name(item)];
              this.emit(`${csConfig.namespace}.Models`);
            } else {
              this.emit(`${_upperFirst(_name(item))}`);
            }
          });
        } else {
          this.emit(`${_type(node.fieldValue.fieldType.lexeme)}`);
        }
        this.emit(` ${paramName} { get; set; }`);
        this.emit('\n');
      } else {
        this.visitModelField(node, level, modelName, true);
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
  }

  visitModelField(ast, level, modelName, isObject) {
    assert.equal(ast.type, 'modelField');
    const fieldName = _avoidReserveName(_upperFirst(_name(ast.fieldName)));
    var subModelName = modelName + _upperFirst(fieldName);

    if (!isObject) {
      if (!ast.fieldValue.fieldType) {
        subModelName = modelName + _upperFirst(fieldName);
        this.emit(`public ${subModelName} ${_upperFirst(fieldName)} { get; set; }\n`, level);
        this.emit(`public class ${subModelName} : TeaModel {\n`, level);
        this.visitModelBody(ast.fieldValue, level + 1, {}, subModelName);
        this.emit('}\n', level);
      } else {
        var type = _type(ast.fieldValue.fieldType);
        this.emit('public ', level);
        if (type === 'array') {
          if (ast.fieldValue.fieldItemType.nodes !== undefined) {
            const subModelName = modelName + _upperFirst(fieldName);
            this.emit(`List<${subModelName}> `);
            this.emit(`${_upperFirst(fieldName)} { get; set; }\n`);
            this.emit(`public class ${subModelName} : TeaModel {\n`, level);
            if (ast.fieldValue.fieldItemType.nodes[0].fieldValue.type === 'modelBody') {
              this.visitModelBody(ast.fieldValue.fieldItemType.nodes[0].fieldValue, level + 1, {}, subModelName);
            } else {
              ast.fieldValue.fieldItemType.nodes.forEach(cNode => {
                this.visitModelField(cNode, level + 1, subModelName);
              });
            }
            this.emit('}\n', level);
          } else {
            this.emit('List<string> ');
            this.emit(`${_upperFirst(fieldName)} { get; set; }\n`);
          }
        } else if (type.type === `moduleModel`) {
          let paths = ast.fieldValue.fieldType.path;
          paths.forEach((item, index) => {
            if (index !== 0) {
              this.emit('.');
            }
            if (item.idType === 'module') {
              let csConfig = this.imports[_name(item)];
              this.emit(`${csConfig.namespace}.Models`);
            } else {
              this.emit(`${_upperFirst(_name(item))}`);
            }
          });
          this.emit(` ${_upperFirst(fieldName)} { get; set; }\n`);
        } else if (type.idType === 'model') {
          this.emit(`${_upperFirst(_name(type))} `);
          this.emit(`${_upperFirst(fieldName)} { get; set; }\n`);
        } else if (type.idType === 'module') {
          this.emit(`${this.imports[_name(type)].namespace}.${this.imports[_name(type)].className || 'Client'} `);
          this.emit(`${_upperFirst(fieldName)} { get; set; }\n`);
        } else {
          this.emit(`${type} `);
          this.emit(`${_upperFirst(fieldName)} { get; set; }\n`);
        }
      }
    } else {
      this.emit(`public ${subModelName} ${_upperFirst(fieldName)} { get; set; }\n`, level);
      this.emit(`public class ${subModelName} : TeaModel {\n`, level);
      for (let i = 0; i < ast.fieldValue.nodes.length; i++) {
        const node = ast.fieldValue.nodes[i];
        this.emitAnnotation(node, attrList, level + 1);
        this.visitModelField(node, level + 1, subModelName);
        // this.emit(`{"${fieldName}", ""},\n`, level + 1);
      }
      this.emit('};\n', level);
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
      let clientName = (this.import[_name(ast)] && this.import[_name(ast)]['client']) ||
        `${_name(ast)}.Client`;
      this.emit(clientName);
    } else if (ast.type === 'model') {
      if (ast.moduleName) {
        let moduleName = `${this.imports[ast.moduleName].namespace}.Models`;
        this.emit(`${moduleName}.`);
      }
      let tempName = '';
      let models = _name(ast).split('.');
      models.forEach((item, index) => {
        if (index > 0) {
          this.emit('.');
        }
        tempName += _upperFirst(_type(item));
        this.emit(tempName);
      });
    } else if (ast.type === 'moduleModel' && ast.path && ast.path.length > 0) {
      let fullName = '';
      ast.path.forEach((item, index) => {
        if (index === 0) {
          this.emit(`${this.imports[_name(item)].namespace}.Models`);
        } else {
          this.emit('.');
          this.emit(fullName + _upperFirst(_name(item)));
          fullName += _upperFirst(_name(item));
        }
      });
    } else if (ast.type === 'module_instance') {
      let csConfig = this.imports[_name(ast)];
      this.emit(`${csConfig.namespace}.${csConfig.className || 'Client'}`);
    } else if (ast.idType === 'module') {
      let csConfig = this.imports[_name(ast)];
      this.emit(`${csConfig.namespace}.${csConfig.className || 'Client'}`);
    } else if (ast.idType === 'model') {
      this.emit(_upperFirst(_name(ast)));
    } else if (ast.fieldType === 'array') {
      this.emit('List<');
      this.visitType(ast.fieldItemType, level);
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
    } else {
      this.emit(_type(_name(ast)));
    }
  }

  visitReturnType(ast, level, env) {
    if (env.isAsyncMode) {
      if (_type(_name(ast.returnType)) === 'void') {
        this.emit('Task ');
        return;
      }
      this.emit('Task<');
    }
    this.visitType(ast.returnType, level);
    if (env.isAsyncMode) {
      this.emit('>');
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
    this.emit(`TeaRequest ${REQUEST} = new TeaRequest();\n`, level);

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
      throw new Error(`type '${ast.type}' unimpelemented`);
    }
  }

  visitTry(ast, level, env) {
    this.emit('try\n', level);
    this.emit('{\n', level);
    this.visitStmts(ast.tryBlock, level + 1, env);
    this.emit('}\n', level);

    if (ast.catchBlock) {
      this.emit(`catch (TeaException ${_name(ast.catchId)})`, level);
      this.emit('\n');
      this.emit('{\n', level);
      this.visitStmts(ast.catchBlock, level + 1, env);
      this.emit('}\n', level);
      this.emit(`catch (Exception _${_name(ast.catchId)})`, level);
      this.emit('\n');
      this.emit('{\n', level);
      this.emit(`TeaException ${_name(ast.catchId)} = new TeaException(new Dictionary<string, object>\n`, level + 1);
      this.emit('{\n', level + 1);
      this.emit(`{ "message", _${_name(ast.catchId)}.Message }\n`, level + 2);
      this.emit('});\n', level + 1);
      this.visitStmts(ast.catchBlock, level + 1, env);
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
    this.visitType(ast.expr.inferred);
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
    this.emit(`Dictionary<${_type(_name(ast.inferred.keyType))}, ${_type(_name(ast.inferred.valueType))}> ${RUNTIME} = `, level);
    this.visitObject(ast, level, env);
    this.emit(';\n');
    this.emit('\n');
    this.emit('TeaRequest _lastRequest = null;\n', level);
    this.emit('Exception _lastException = null;\n', level);
    this.emit('long _now = System.DateTime.Now.Millisecond;\n', level);
    this.emit('int _retryTimes = 0;\n', level);
    this.emit(`while (TeaCore.AllowRetry((IDictionary) ${RUNTIME}["retry"], _retryTimes, _now))\n`, level);
    this.emit('{\n', level);
    this.emit('if (_retryTimes > 0)\n', level + 1);
    this.emit('{\n', level + 1);
    this.emit(`int backoffTime = TeaCore.GetBackoffTime((IDictionary)${RUNTIME}["backoff"], _retryTimes);\n`, level + 2);
    this.emit('if (backoffTime > 0)\n', level + 2);
    this.emit('{\n', level + 2);
    this.emit('TeaCore.Sleep(backoffTime);\n', level + 3);
    this.emit('}\n', level + 2);
    this.emit('}\n', level + 1);
    this.emit('_retryTimes = _retryTimes + 1;\n', level + 1);
    this.emit('try\n', level + 1);
    this.emit('{\n', level + 1);
  }

  visitRuntimeAfter(ast, level) {
    this.emit('}\n', level + 1);
    this.emit('catch (Exception e)\n', level + 1);
    this.emit('{\n', level + 1);
    this.emit('if (TeaCore.IsRetryable(e))\n', level + 2);
    this.emit('{\n', level + 2);
    this.emit('_lastException = e;\n', level + 3);
    this.emit('continue;\n', level + 3);
    this.emit('}\n', level + 2);
    this.emit('throw e;\n', level + 2);
    this.emit('}\n', level + 1);
    this.emit('}\n\n', level);
    this.emit('throw new TeaUnretryableException(_lastRequest, _lastException);\n', level);
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
    } else if (ast.type === 'null') {
      this.emit('null');
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
    } else if (ast.type === 'array') {
      this.visitArray(ast, level, env);
    } else if (ast.type === 'and') {
      this.visitExpr(ast.left, level, env);
      this.emit(' && ');
      this.visitExpr(ast.right, level, env);
    } else if (ast.type === 'or') {
      this.visitExpr(ast.left, level, env);
      this.emit(' || ');
      this.visitExpr(ast.right, level, env);
    } else if (ast.type === 'null') {
      this.emit('null');
    } else if (ast.type === 'not') {
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
    this.emit(`]`);
  }

  visitConstructModel(ast, level, env) {
    assert.equal(ast.type, 'construct_model');
    if (ast.propertyPath && ast.propertyPath.length > 0) {
      if (ast.inferred && ast.inferred.moduleName) {
        let moduleName = `${_name(ast.aliasId)}`;
        moduleName = `${this.imports[_name(ast.aliasId)].namespace}.Models`;
        this.emit(`new ${moduleName}.`);
      } else {
        this.emit('new ');
      }
      let tempName = '';
      for (let i = 0; i < ast.propertyPath.length; i++) {
        const item = ast.propertyPath[i];
        if (i > 0) {
          this.emit('.');
        }
        tempName += _upperFirst(item.lexeme);
        this.emit(tempName);
      }
    } else {
      this.emit(`new ${_name(ast.aliasId)}`);
    }

    if (ast.object && ast.object.fields && ast.object.fields.length > 0) {
      this.emit('\n');
      this.emit('{\n', level);
      let i = 0;
      ast.object.fields.forEach((element) => {
        let comments = DSL.comment.getFrontComments(this.comments, element.tokenRange[0]);
        this.visitComments(comments, level + 1);
        this.emit(_avoidReserveName(_upperFirst(_name(element.fieldName))), level + 1);
        this.emit(' = ');
        this.visitExpr(element.expr, level + 1, env);
        this.emit(',\n');
        i++;
      });
      //find the last item's back comment
      let comments = DSL.comment.getBetweenComments(this.comments, ast.object.fields[i - 1].tokenRange[0], ast.tokenRange[1]);
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
    let csConfig = this.imports[_name(ast.aliasId)];
    let clientName = `${csConfig.namespace}.${csConfig.className || 'Client'}`;
    this.emit('new ');
    this.emit(clientName);
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
    const method = ast.left.propertyPath[0];
    let methodName = _upperFirst(_name(method));
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

  visitStaticCall(ast, level, env) {
    assert.equal(ast.left.type, 'static_call');
    let callName = this.imports[_name(ast.left.id)].namespace;
    let clsName = this.imports[_name(ast.left.id)].className || 'Common';
    this.emit(`${callName}.${clsName}.${_upperFirst(_name(ast.left.propertyPath[0]))}(`);
    for (let i = 0; i < ast.args.length; i++) {
      const expr = ast.args[i];
      if (expr.needCast) {
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

    //this.emit('\n');
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
      //this.emit()
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
      this.emit('TeaCore.BytesReadable(');
    }
    if(ast.expr.type === 'object') {
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
    this.emit('throw new TeaException(', level);
    this.visitObject(ast.expr, level, env);
    this.emit(');\n');
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
        this.emit('{', level);
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

    this.emit(`TeaConverter.merge<${_type(ast.inferred.valueType.name)}>\n`);
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
      var key = _name(ast.fieldName);
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
    this.emit(`throw new TeaRetryableException(${REQUEST}, ${RESPONSE});\n`, level);
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

  /*******************************************************/

  eachModel(ast, level, predefined) {
    assert.equal(ast.type, 'model');
    const env = {
      predefined
    };
    const modelName = _upperFirst(_name(ast.modelName));
    this.visitAnnotation(ast.annotation, level);
    let comments = DSL.comment.getFrontComments(this.comments, ast.tokenRange[0]);
    this.visitComments(comments, level);
    this.emit(`public class ${modelName} : TeaModel`, level);
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
    for (var i = 0; i < ast.params.params.length; i++) {
      const param = ast.params.params[i];
      if (_name(param.paramType) && !DSL.util.isBasicType(_name(param.paramType))) {
        this.emit(`${_avoidReserveName(param.paramName.lexeme)}.Validate();\n`, level + 1);
      }
    }

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
      this.emit(`TeaResponse ${RESPONSE} = await TeaCore.DoActionAsync(${REQUEST}`, baseLevel + 1);
    } else {
      this.emit(`TeaResponse ${RESPONSE} = TeaCore.DoAction(${REQUEST}`, baseLevel + 1);
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

  eachImport(imports, usedModels, level) {
    this.imports = {};
    if (imports.length === 0) {
      return;
    }

    if (!this.config.pkgDir) {
      throw new Error(`Must specific pkgDir when have imports`);
    }

    const lockPath = path.join(this.config.pkgDir, '.libraries.json');
    const lock = JSON.parse(fs.readFileSync(lockPath, 'utf8'));
    for (let i = 0; i < imports.length; i++) {
      const item = imports[i];
      const aliasId = item.lexeme;
      const moduleDir = this.config.libraries[aliasId];
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
      csharpConfig.release = pkg.releases.csharp || csharpConfig.namespace + ':0.0.1';

      this.imports[aliasId] = csharpConfig;
    }
  }

  importAfter() {
    // Nothing
  }

  moduleBefore() {
    // Nothing
  }

  modelBefore() {
    this.emit(`// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace ${this.namespace}.Models
{
`);
  }

  modelAfter() {
    this.emit('}\n');
  }

  interfaceBefore(level, extendParam) {
    this.emit('// This file is auto-generated, don\'t edit it. Thanks.\n', level);
    this.emit(`
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Tea;
using Tea.Utils;

`);

    if (extendParam.modelsCount && extendParam.modelsCount > 0) {
      this.emit(`using ${this.namespace}.Models;\n`);
    }

    this.emit(`
namespace ${this.namespace}
{
    public interface I${this.className} 
    {
`);

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
    if (wrapName === 'Main') {
      this.emit('(string[] args)');
    } else {
      this.visitParams(ast.params, level, env);
    }

    this.emit(';\n');
  }

  interfaceAfter() {
    this.emit('   }\n');
    this.emit('}\n');
  }

  apiBefore(level, extendParam) {
    this.emit('// This file is auto-generated, don\'t edit it. Thanks.\n', level);
    this.emit(`
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Tea;
using Tea.Utils;

`);

    if (extendParam.modelsCount && extendParam.modelsCount > 0) {
      this.emit(`using ${this.namespace}.Models;\n`);
    }

    this.emit(`
namespace ${this.namespace}
{
    public class ${this.className} `);

    if (extendParam.extend && extendParam.extend.length > 0) {
      this.emit(`: ${extendParam.extend.join(', ')}
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
    if (wrapName === 'Main' && env.isAsyncMode) {
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
      wrapName += 'Async';
    }

    env.returnType = ast.returnType;
    this.visitReturnType(ast, 0, env);
    this.emit(`${wrapName}`);
    if (wrapName === 'Main') {
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

  visitWrapBody(ast, level, env) {
    assert.equal(ast.type, 'functionBody');
    this.visitStmts(ast.stmts, level, env);
  }

  visitInit(ast, types, level, env) {
    assert.equal(ast.type, 'init');
    types.forEach((item) => {
      let comments = DSL.comment.getFrontComments(this.comments, item.tokenRange[0]);
      this.visitComments(comments, level + 2);
      this.emit('protected ', level + 2);
      if (item.value.type) {
        this.visitType(item.value);
        this.emit(' ');
      } else if (this.imports[_name(item.value)]) {
        let csConfig = this.imports[_name(item.value)];
        this.emit(`${csConfig.namespace}.${csConfig.className || 'Client'} `);
      } else {
        this.emit(`${_type(_name(item.value))} `);
      }
      this.emit(`${_vid(item.vid)};\n`);
    });

    this.emit('\n');
    this.visitAnnotation(ast.annotation, level + 2);
    let comments = DSL.comment.getFrontComments(this.comments, ast.tokenRange[0]);
    this.visitComments(comments, level + 2);
    this.emit(`public ${this.className}`, level + 2);
    this.visitParams(ast.params);
    if (ast.initBody && ast.initBody.stmts[0].type === 'super') {
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
    annotation.value.split('\n').forEach((line) => {
      this.emit(`${line}`, level);
      this.emit(`\n`);
    });
  }

  visitComments(comments, level) {
    comments.forEach(comment => {
      this.emit(`${comment.value}`, level);
      this.emit(`\n`);
    });
  }

  async visitConsoleCSProj() {
    let csprojPath = path.join(this.csprojOutputDir, (this.config.packageInfo.name || 'client') + '.csproj');
    let json = {};
    json = await parse(fs.readFileSync(path.join(__dirname, 'files', 'consoleCsproj.tmpl')));
    //填写包的基本信息
    let propertyGroup = json.Project.PropertyGroup;
    propertyGroup.forEach((property) => {
      if (property.hasOwnProperty('RootNamespace')) {
        property['RootNamespace'] = this.config.namespace;
      }
    });

    let dependenciesClass = [];
    Object.keys(this.imports).forEach((key) => {
      dependenciesClass.push(this.imports[key].release);
    });
    //添加对Tea的依赖
    dependenciesClass.push('Tea:1.0.6');

    //寻找用来写入的itemGroup， 避开有特殊参数的itemGroup
    let itemGroup = {};

    //添加或更新依赖包
    dependenciesClass.forEach((item, index) => {
      let dependency = item.split(':');
      let writeReference = {};
      itemGroup.PackageReference = itemGroup.PackageReference || [];
      writeReference.$ = { 'Include': dependency[0], 'Version': dependency[1] };
      itemGroup.PackageReference.push(writeReference);
    });
    json.Project.ItemGroup = itemGroup;

    const builder = new xml2js.Builder();
    const newCsproj = builder.buildObject(json);
    fs.writeFileSync(csprojPath, Entities.decode(newCsproj));
  }

  async visitCSProj() {
    let csprojPath = path.join(this.csprojOutputDir, (this.config.packageInfo.name || 'client') + '.csproj');
    let json = {};
    if (!fs.existsSync(csprojPath)) {
      json = await parse(fs.readFileSync(path.join(__dirname, 'files', 'csproj.tmpl')));
    } else {
      json = await parse(fs.readFileSync(csprojPath));
    }

    //填写包的基本信息
    let propertyGroup = json.Project.PropertyGroup;
    let assemblyInfo = this.release.split(':');
    propertyGroup.forEach((property) => {
      if (property.hasOwnProperty('RootNamespace')) {
        property['RootNamespace'] = this.config.namespace;
      }
      if (property.hasOwnProperty('AssemblyName')) {
        property['AssemblyName'] = assemblyInfo[0];
      }
    });

    let dependenciesClass = [];
    Object.keys(this.imports).forEach((key) => {
      dependenciesClass.push(this.imports[key].release);
    });
    //添加对Tea的依赖
    dependenciesClass.push('Tea:1.0.6');

    //寻找用来写入的itemGroup， 避开有特殊参数的itemGroup
    let itemGroup = json.Project.ItemGroup;
    let writeItem = {};
    itemGroup.forEach((item) => {
      if (!item.$) {
        writeItem = item;
      }
    });

    //添加或更新依赖包
    dependenciesClass.forEach((item, index) => {
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

  async visitAssemblyInfo() {
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