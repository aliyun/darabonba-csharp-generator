'use strict';

const fs = require('fs');
const path = require('path');
const xml2js = require('xml2js');
const marked = require('marked');
marked.use({
  mangle: false,
  headerIds: false
});

const builtinModels = [
  '$Request', '$Response', '$Error', '$SSEEvent', '$Model',
  '$RuntimeOptions', '$ExtendsParameters', '$RetryOptions',
  '$ResponseError', '$FileField'
];

const exceptionFields = ['message', 'code', 'data', 'statusCode', 'description', 'accessDeniedDetail'];

// const typeMap = {
//   any: 'object',
//   number: 'int?',
//   boolean: 'bool?',
//   object: 'Dictionary<string, object>',
//   integer: 'int?',
//   map: 'Dictionary<string, string>',
//   uint16: 'ushort?',
//   int16: 'short?',
//   uint8: 'byte',
//   int8: 'sbyte',
//   uint32: 'uint?',
//   int32: 'int?',
//   uint64: 'ulong?',
//   int64: 'long?',
//   readable: 'Stream',
//   writable: 'Stream',
//   $Request: 'Request',
//   $Response: 'Response',
//   $Model: 'Model',
//   $URL: 'URL',
//   $Date: 'Date',
//   $File: 'File',
//   $Form: 'FormUtils',
//   $Number: 'MathUtils',
//   $XML: 'XmlUtils',
//   $String: 'StringUtils',
//   $ResponseError: 'DaraResponseException',
//   $Error: 'DaraException',
//   $RetryOptions: 'RetryOptions',
//   $RuntimeOptions: 'RuntimeOptions',
//   $SSEEvent: 'SSEEvent',
//   $ExtendsParameters: 'ExtendsParameters',
//   void: 'void',
//   bytes: 'byte[]',
//   float: 'float?',
//   double: 'double?',
//   class: 'Type',
//   long: 'long?',
//   ulong: 'ulong?',
// };

const builtinMap = {
  $URL: 'Darabonba.URL',
  $Stream: 'Darabonba.Utils.StreamUtils',
  $Date: 'Darabonba.Date',
  $File: 'Darabonba.File',
  $Form: 'Darabonba.Utils.FormUtils',
  $JSON: 'Darabonba.Utils.JSONUtils',
  $Number: 'Darabonba.Utils.MathUtils',
  $XML: 'Darabonba.Utils.XmlUtils',
  $String: 'Darabonba.Utils.StringUtils',
};

const REQUEST = 'request_';
const RESPONSE = 'response_';

function _escape(str) {
  return str.includes('-') ? `'${str}'` : str;
}

function _name(str) {
  if (str.lexeme === '__request') {
    return REQUEST;
  }

  if (str.lexeme === '__response') {
    return RESPONSE;
  }

  return str.lexeme || str.name;
}

function _upperFirst(str) {
  return str[0].toUpperCase() + str.substring(1);
}

// 将目录的每一级的首字母大写
function _upperPath(filePath) {
  if (filePath) {
    const parts = filePath.split(path.sep).map(part => _upperFirst(part));
    return parts.join(path.sep);
  }
  return '';
}

// 通过filepath获取namespace的剩余部分，例如Lib/Util.cs -> Lib -> ${this.namespace}.Lib
function _nameSpace(filePath) {
  return path.dirname(_upperPath(filePath)) !== '.' ? path.dirname(_upperPath(filePath)).replace(new RegExp(`\\${path.sep}`, 'g'), '.') : '';
}

function _subModelName(name) {
  return name.split('.').map((name) => _upperFirst(name)).join('');
}

function _lowerFirst(str) {
  return str[0].toLowerCase() + str.substring(1);
}

function _string(str) {
  if (str.string === '""') {
    return '\\"\\"';
  }
  str.string = str.string.replace(/\\/g, '\\\\').replace(/\n/g, '\\n');
  return str.string.replace(/([^\\])"+|^"/g, function (str) {
    return str.replace(/"/g, '\\"');
  });
}

// function _type(name) {
//   if (typeMap[name]) {
//     name = typeMap[name];
//   }
//   return name;
// }

function _avoidReserveName(name) {
  const reserves = [
    'abstract', 'as', 'base', 'bool', 'break', 'byte', 'case',
    'catch', 'char', 'checked', 'class', 'const', 'continue',
    'decimal', 'default', 'delegate', 'do', 'double', 'else',
    'enum', 'event', 'explicit', 'extern', 'false', 'finally',
    'fixed', 'float', 'for', 'foreach', 'goto', 'if', 'implicit',
    'in', 'int', 'interface', 'internal', 'is', 'lock', 'long',
    'namespace', 'new', 'null', 'object', 'operator', 'out',
    'override', 'params', 'private', 'protected', 'public',
    'readonly', 'ref', 'return', 'sbyte', 'switch', 'this',
    'throw', 'true', 'try', 'typeof', 'uint', 'ulong', 'unchecked',
    'unsafe', 'ushort', 'using', 'virtual', 'void', 'volatile',
    'while', 'Equals', 'model'
  ];

  if (reserves.indexOf(name) !== -1) {
    return `${name}_`;
  }

  return name;
}

function remove(...filesPath) {
  filesPath.forEach(filePath => {
    if (fs.existsSync(filePath)) {
      if (fs.statSync(filePath).isDirectory()) {
        const files = fs.readdirSync(filePath);
        files.forEach((file, index) => {
          let curPath = path.join(filePath, file);
          if (fs.statSync(curPath).isDirectory()) {
            remove(curPath);
          } else {
            fs.unlinkSync(curPath);
          }
        });
        fs.rmdirSync(filePath);
      } else {
        fs.unlinkSync(filePath);
      }
    }
  });
}

function _vid(vid) {
  return `_${_name(vid).substr(1)}`;
}

function parse(xml) {
  let _err, _result;
  xml2js.parseString(xml, function (err, result) {
    if (err) {
      _err = err;
      return;
    }
    _result = result;
  });
  if (_err) {
    throw _err;
  }

  return _result;
}

function render(template, params = {}) {
  Object.keys(params).forEach((key) => {
    template = template.split('${' + key + '}').join(params[key]);
  });
  return template;
}

function _format(str) {
  return str.replace(/<p>/g, '<para>')
    .replace(/<\/p>/g, '</para>')
    .replace(/<ul>/g, '<list type="bullet">')
    .replace(/<\/ul>/g, '</list>')
    .replace(/<li>/g, '<item><description>')
    .replace(/<\/li>/g, '</description></item>')
    .replace(/<strong>/g, '<b>')
    .replace(/<\/strong>/g, '</b>')
    .replace(/<code>/g, '<c>')
    .replace(/<\/code>/g, '</c>')
    .replace(/<blockquote>/g, '<remarks>')
    .replace(/<\/blockquote>/g, '</remarks>');
}

function md2Html(mdText) {
  let htmlText = marked.parse(mdText).trimEnd();
  return htmlText;
}

function _isBuiltinModel(name) {
  return builtinModels.includes(name);
}

function _isBuiltinNamespace(namespace) {
  return namespace === 'Darabonba.Utils' || namespace === 'Darabonba.Exceptions' || namespace === 'Darabonba'
    || namespace === 'Darabonba.Models' || namespace === 'Darabonba.RetryPolicy';
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

module.exports = {
  _name, _upperFirst, _string, _lowerFirst, _isBinaryOp,
  _avoidReserveName, remove, _vid, parse, render, _format, md2Html,
  _escape, _subModelName, _upperPath, _nameSpace, builtinMap, _isBuiltinModel, exceptionFields, _isBuiltinNamespace
};