'use strict';
const DSL = require('@darabonba/parser');
const { _vid, _name, _upperFirst, _isBinaryOp } = require('./helper');

const types = [
  'integer', 'int8', 'int16', 'int32',
  'int64', 'long', 'ulong', 'string',
  'uint8', 'uint16', 'uint32', 'uint64',
  'number', 'float', 'double', 'boolean',
  'bytes', 'readable', 'writable', 'object', 'any'
];

const typesMap = {
  'integer': 'int',
  'int32': 'int',
  'number': 'int',
};

const BuiltinModule = {
  StringUtils: {
    namespace: 'Darabonba.Utils',
    className: 'Darabonba.Utils.StringUtils',
  },
  MathUtils: {
    namespace: 'Darabonba.Utils',
    className: 'Darabonba.Utils.MathUtils',
  },
  ListUtils: {
    namespace: 'Darabonba.Utils',
    className: 'Darabonba.Utils.ListUtils',
  },
  StreamUtils: {
    namespace: 'Darabonba.Utils',
    className: 'Darabonba.Utils.StreamUtils',
  },
  XmlUtils: {
    namespace: 'Darabonba.Utils',
    className: 'Darabonba.Utils.XmlUtils',
  },
  URL: {
    namespace: 'Darabonba',
    className: 'Darabonba.URL',
  },
  JSONUtils: {
    namespace: 'Darabonba.Utils',
    className: 'Darabonba.Utils.JSONUtils',
  },
  FormUtils: {
    namespace: 'Darabonba.Utils',
    className: 'Darabonba.Utils.FormUtils',
  },
  File: {
    namespace: 'Darabonba',
    className: 'Darabonba.File',
  },
  BytesUtils: {
    namespace: 'Darabonba.Utils',
    className: 'Darabonba.Utils.BytesUtils',
  },
  Date: {
    namespace: 'Darabonba',
    className: 'Darabonba.Date',
  },
  ConverterUtils: {
    namespace: 'Darabonba.Utils',
    className: 'Darabonba.Utils.ConverterUtils',
  },
  Core: {
    namespace: 'Darabonba',
    className: 'Darabonba.Core',
  },
};

class Builtin {
  constructor(generator, aliasId = '', methods = []) {
    this.generator = generator;
    this.module = module;
    this.aliasId = aliasId;

    methods.forEach(method => {
      this[method] = function (ast, level, env) {
        const clientName = this.getClientName();
        this.generator.emit(`${clientName}.${_upperFirst(method)}`);
        this.generator.visitArgs(ast, level, env);
      };
    });
  }

  getInstanceName(ast, level) {
    if (ast.left.id.tag === DSL.Tag.Tag.VID) {
      this.generator.emit(`this.${_vid(ast.left.id)}`, level);
    } else {
      this.generator.emit(`${_name(ast.left.id)}`, level);
    }
  }

  getClientName(aliasId) {
    const { namespace, className } = BuiltinModule[aliasId || this.aliasId];
    if (!this.generator.moduleClass.has(aliasId || this.aliasId)) {
      this.generator.moduleClass.set(aliasId || this.aliasId, {
        namespace,
        className,
        aliasName: this.generator.getAliasName(namespace, className, 'Darabonba')
      });
    }
    this.generator.clientName.set(className, false);
    return this.generator.getRealClientName(aliasId || this.aliasId);
  }
}

class Date {
  constructor(generator) {
    this.generator = generator;
    const methods = [
      'format', 'unix', 'diff', 'UTC', 'add', 'sub', 'hour',
      'minute', 'second', 'dayOfYear', 'dayOfMonth', 'dayOfWeek',
      'weekOfYear', 'month', 'year'
    ];
    methods.forEach(method => {
      this[method] = function (ast, level, env) {
        this.getInstanceName(ast);
        this.generator.emit(`.${_upperFirst(method)}`);
        this.generator.visitArgs(ast, level, env);
      };
    });
  }

  getInstanceName(ast, level) {
    if (ast.left.id.tag === DSL.Tag.Tag.VID) {
      this.generator.emit(`this.${_vid(ast.left.id)}`, level);
    } else {
      this.generator.emit(`${_name(ast.left.id)}`, level);
    }
  }
}

class String extends Builtin {
  constructor(generator) {
    super(generator, 'StringUtils');
  }
  split(ast, level, env) {
    this.generator.used.push('System.Linq');
    this.getInstanceName(ast);
    this.generator.emit('.Split');
    this.generator.visitArgs(ast, level, env);
    this.generator.emit('.ToList()');
  }

  // TODO 正则规则不一致
  replace(ast, level, env) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.Replace(`);
    this.getInstanceName(ast);
    this.generator.emit(', ');
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(', ');
    env.groupOp = false;
    this.generator.visitExpr(ast.args[1], level, env);
    this.generator.emit(')');
  }

  contains(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit('.Contains');
    this.generator.visitArgs(ast, level, env);
  }

  length(ast) {
    this.getInstanceName(ast);
    this.generator.emit('.Length');
  }

  hasPrefix(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit('.StartsWith');
    this.generator.visitArgs(ast, level, env);
  }

  hasSuffix(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit('.EndsWith');
    this.generator.visitArgs(ast, level, env);
  }

  index(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit('.IndexOf');
    this.generator.visitArgs(ast, level, env);
  }

  subString(ast, level, env) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.SubString(`);
    this.getInstanceName(ast);
    this.generator.emit(', ');
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(', ');
    env.groupOp = false;
    this.generator.visitExpr(ast.args[1], level, env);
    this.generator.emit(')');
  }

  toLower(ast) {
    this.getInstanceName(ast);
    this.generator.emit('.ToLower');
    this.generator.emit('()');
  }

  toUpper(ast) {
    this.getInstanceName(ast);
    this.generator.emit('.ToUpper');
    this.generator.emit('()');
  }

  equals(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit(' == ');
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);

  }

  empty(ast) {
    this.generator.emit('String.IsNullOrEmpty(');
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  toBytes(ast, level, env) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.ToBytes(`);
    this.getInstanceName(ast);
    this.generator.emit(', ');
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  parseInt(ast) {
    this.generator.emit(`int.Parse(`);
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  parseLong(ast) {
    this.generator.emit(`long.Parse(`);
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  parseFloat(ast) {
    this.generator.emit(`float.Parse(`);
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  parseDouble(ast) {
    this.generator.emit('double.Parse(');
    this.getInstanceName(ast);
    this.generator.emit(')');
  }
}

class Env extends Builtin {
  get(ast, level, env) {
    const key = ast.args[0];
    this.generator.emit(`Environment.GetEnvironmentVariable(`);
    env.groupOp = false;
    this.generator.visitExpr(key, level, env);
    this.generator.emit(')');
  }

  set(ast, level, env) {
    const key = ast.args[0];
    this.generator.emit(`Environment.SetEnvironmentVariable(`);
    env.groupOp = false;
    this.generator.visitExpr(key, level, env);
    this.generator.emit(', ');
    const value = ast.args[1];
    env.groupOp = false;
    this.generator.visitExpr(value, level, env);
    this.generator.emit(')');
  }
}

class Logger {
  constructor(generator) {
    this.generator = generator;
    const methods = ['log', 'info', 'debug', 'warning'];
    methods.forEach(method => {
      this[method] = function (ast, level, env) {
        this.generator.emit('Console.WriteLine');
        this.generator.visitArgs(ast, level, env);
      };
    });
  }

  error(args, level, env) {
    this.generator.emit('Console.Error.WriteLine');
    this.generator.visitArgs(args, level, env);
  }
}

class Number extends Builtin {
  constructor(generator) {
    super(generator, 'MathUtils');
  }

  floor(ast, level, env) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.Floor(`);
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  round(ast, level, env) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.Round(`);
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  // TODO 待支持
  // min(ast, level, env) {
  //   const clientName = this.getClientName();
  //   this.generator.emit(`${clientName}.Min(`);
  //   this.generator.visitExpr(ast.args[0], level, env);
  //   this.generator.emit(', ');
  //   this.generator.visitExpr(ast.args[1], level, env);
  //   this.generator.emit(')');
  // }

  // max(ast, level, env) {
  //   const clientName = this.getClientName();
  //   this.generator.emit(`${clientName}.Max(`);
  //   this.generator.visitExpr(ast.args[0], level, env);
  //   this.generator.emit(', ');
  //   this.generator.visitExpr(ast.args[1], level, env);
  //   this.generator.emit(')');
  // }

  random() {
    this.generator.emit('new Random().NextDouble()');
  }

  parseInt(ast) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.ParseInt(`);
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  parseLong(ast) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.ParseLong(`);
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  parseFloat(ast) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.ParseFloat(`);
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  parseDouble(ast) {
    this.generator.emit('double.Parse(');
    this.getInstanceName(ast);
    this.generator.emit('.Value.ToString())');
  }

  itol(ast) {
    this.getInstanceName(ast);
  }

  ltoi(ast) {
    this.generator.emit('(int?)');
    this.getInstanceName(ast);
    this.generator.emit('.Value');
  }
}

class JSON extends Builtin {
  constructor(generator) {
    super(generator, 'JSONUtils');
  }
  stringify(ast, level, env) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.SerializeObject`);
    this.generator.visitArgs(ast, level, env);
  }

  parseJSON(ast, level, env) {
    // TODO Newtonsoft.Json需要添加packageReference到csproj
    this.generator.used.push(`Newtonsoft.Json`);
    this.generator.emit('JsonConvert.DeserializeObject');
    this.generator.visitArgs(ast, level, env);
  }

  readPath(ast, level, options) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.readPath(`);
    this.generator.visitExpr(ast.args[0], level, options);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[1], level, options);
    this.generator.emit(')');
  }
}

class Array extends Builtin {
  constructor(generator) {
    super(generator, 'ListUtils');
  }
  join(ast, level, env) {
    this.generator.emit(`string.Join(`);
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(', ');
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  contains(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit('.Contains');
    this.generator.visitArgs(ast, level, env);
  }

  length(ast) {
    this.getInstanceName(ast);
    this.generator.emit('.Count');
  }

  index(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit('.IndexOf(');
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  get(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit(`[`);
    env.groupOp = false;
    if (ast.args[0].id && ast.args[0].id.tag === DSL.Tag.Tag.ID) {
      env.groupOp = true;
    }
    this.generator.visitExpr(ast.args[0], level, env);
    if (ast.args[0].id && ast.args[0].id.tag === DSL.Tag.Tag.ID) {
      this.generator.emit('.Value');
    }
    this.generator.emit(`]`);
  }

  sort(ast, level, env) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.Sort(`);
    this.getInstanceName(ast);
    this.generator.emit(', ');
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  shift(ast) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.Shift(`);
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  pop(ast, level) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.Pop(`);
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  unshift(ast, level, env) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.Unshift(`);
    this.getInstanceName(ast);
    this.generator.emit(', ');
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  push(ast, level, env) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.Push(`);
    this.getInstanceName(ast);
    this.generator.emit(', ');
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  concat(ast, level, env) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.Concat(`);
    this.getInstanceName(ast);
    this.generator.emit(', ');
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  append(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit('.Insert(');
    const position = ast.args[1];
    const value = ast.args[0];
    env.groupOp = false;
    this.generator.visitExpr(position, level, env);
    this.generator.emit(', ');
    env.groupOp = false;
    this.generator.visitExpr(value, level, env);
    this.generator.emit(')');
  }

  // 删除出现的第一个元素
  remove(ast, level, env) {
    this.getInstanceName(ast);
    const value = ast.args[0];
    this.generator.emit('.RemoveAt(');
    this.getInstanceName(ast);
    this.generator.emit('.IndexOf(');
    env.groupOp = false;
    this.generator.visitExpr(value, level, env);
    this.generator.emit('))');
  }
}

class Func extends Builtin {
  sleep(ast, level, env) {
    if (env.isAsyncMode) {
      this.generator.emit(`await Task.Delay(`);
    } else {
      this.generator.emit(`Thread.Sleep(`);
      this.generator.used.push('System.Threading');
    }
    env.groupOp = false;
    if (ast.args[0].id && ast.args[0].id.tag === DSL.Tag.Tag.ID) {
      env.groupOp = true;
    }
    this.generator.visitExpr(ast.args[0], level, env);
    if (ast.args[0].id && ast.args[0].id.tag === DSL.Tag.Tag.ID) {
      this.generator.emit('.Value');
    }

    this.generator.emit(')');
  }

  isNull(ast, level, env) {
    if(_isBinaryOp(ast.args[0].type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    if(_isBinaryOp(ast.args[0].type)) {
      this.generator.emit(')');
    }
    // 写在核心库的Extensions中，不直接写==null是因为形如if ((data == null))的两层括号的情况
    this.generator.emit(`.IsNull(`);
    this.generator.emit(')');
  }

  equal(ast, level, env) {
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(' == ');
    env.groupOp = true;
    this.generator.visitExpr(ast.args[1], level, env);
  }

  default(ast, level, env) {
    this.generator.emit('Darabonba.Core.GetDefaultValue(');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[1], level, env);
    this.generator.emit(')');
  }
}

class XML extends Builtin {
  constructor(generator) {
    const methods = ['parseXml', 'toXML'];
    super(generator, `XmlUtils`, methods);
  }
}

class URL extends Builtin {
  constructor(generator) {
    const methods = ['parse', 'urlEncode', 'percentEncode', 'pathEncode'];
    super(generator, `URL`, methods);
  }
}

class Stream extends Builtin {
  constructor(generator) {
    const methods = ['readAsBytes', 'readAsBytesAsync', 'readAsJSON', 'readAsJSONAsync', 'readAsString', 'readAsStringAsync', 'readAsSSE', 'readAsSSEAsync'];
    super(generator, 'StreamUtils', methods);
  }

  read(ast, level, env) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.Read(`);
    this.getInstanceName(ast);
    this.generator.emit(', ');
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  write(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit('.Write(');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(', 0, ');
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit('.Length');
    this.generator.emit(')');
  }

  pipe(ast, level, env) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.Pipe(`);
    this.getInstanceName(ast);
    this.generator.emit(', ');
    env.groupOp = false;
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }
}

// 强转
class Converter {
  constructor(generator) {
    this.generator = generator;
    this.stream = new Stream(generator);
    Object.keys(typesMap).forEach(key => {
      // 支持小数转整数，int/uint/number都转成Int32，long/ulong都转成Int64。
      this[key] = function (ast, level, env) {
        generator.emit('(int?)(');
        env.groupOp = false;
        generator.visitExpr(ast.args[0], level, env);
        generator.emit(')');
      };
    });
  }

  getClientName(aliasId) {
    if (!this.generator.moduleClass.has(aliasId)) {
      const { namespace, className } = BuiltinModule[aliasId];
      this.generator.moduleClass.set(aliasId, {
        namespace,
        className,
        aliasName: this.generator.getAliasName(namespace, className, 'Darabonba.Utils')
      });
    }
    return this.generator.getRealClientName(aliasId);
  }

  int8(ast, level, env) {
    this.generator.emit('(sbyte?)');
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit(')');
    }
  }

  uint8(ast, level, env) {
    this.generator.emit('(byte?)');
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit(')');
    }
  }

  int16(ast, level, env) {
    this.generator.emit('(short?)');
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit(')');
    }
  }

  uint16(ast, level, env) {
    this.generator.emit('(ushort?)');
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit(')');
    }
  }

  uint32(ast, level, env) {
    this.generator.emit('(uint?)');
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit(')');
    }
  }

  int64(ast, level, env) {
    this.generator.emit('(long?)');
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit(')');
    }
  }

  uint64(ast, level, env) {
    this.generator.emit('(ulong?)');
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit(')');
    }
  }

  long(ast, level, env) {
    this.generator.emit('(long?)');
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit(')');
    }
  }

  ulong(ast, level, env) {
    this.generator.emit('(ulong?)');
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit(')');
    }
  }

  float(ast, level, env) {
    this.generator.emit('(float?)');
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit(')');
    }
  }

  double(ast, level, env) {
    this.generator.emit('(double?)');
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(ast.args[0], level, env);
    if (_isBinaryOp(ast.args[0].type)) {
      this.generator.emit(')');
    }
  }

  string(ast, level, env) {
    const expr = ast.args[0];
    this.generator.emit('(string)');
    if(_isBinaryOp(expr.type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(expr, level, env);
    if(_isBinaryOp(expr.type)) {
      this.generator.emit(')');
    }
  }

  boolean(ast, level, env) {
    const expr = ast.args[0];
    this.generator.emit('(bool?)');
    if(_isBinaryOp(expr.type)) {
      this.generator.emit('(');
    }
    env.groupOp = true;
    this.generator.visitExpr(expr, level, env);
    if(_isBinaryOp(expr.type)) {
      this.generator.emit(')');
    }
  }

  // 只允许传字符串
  bytes(ast, level, env) {
    const expr = ast.args[0];
    this.generator.emit('(byte[])(');
    this.generator.visitExpr(expr, level, env);
    this.generator.emit(')');
  }

  any(ast, level, env) {
    const expr = ast.args[0];
    this.generator.visitExpr(expr, level, env);
  }

  // 强转成 Dictionary<string, object>
  object(ast, level, env) {
    const expr = ast.args[0];
    if(expr.inferred && expr.inferred.type === 'map') {
      const clientName = this.getClientName('Core');
      this.generator.emit(`${clientName}.ToObject(`);
    } else {
      this.generator.emit('(Dictionary<string, object>)(');
    }
    this.generator.visitExpr(expr, level, env);
    this.generator.emit(')');
  }

  readable(ast, level, env) {
    this.generator.emit('Darabonba.Utils.StreamUtils.BytesReadable(');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  writable(ast, level, env) {
    this.generator.used.push('System.IO');
    const expr = ast.args[0];
    this.generator.emit('(Stream)');
    this.generator.visitExpr(expr, level, env);
  }
}

class Form extends Builtin {
  constructor(generator) {
    const methods = ['toFormString', 'getBoundary', 'toFileForm'];
    super(generator, 'FormUtils', methods);
  }
}

class File extends Builtin {
  constructor(generator) {
    // TODO read/write/close
    const methods = ['createReadStream', 'createWriteStream', 'exists'];
    super(generator, 'File', methods);
  }
}

class Bytes extends Builtin {
  constructor(generator) {
    super(generator, 'BytesUtils');
  }
  from(ast, level, env) {
    // 同 TeaString.ToBytes
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.From(`);
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[1], level, env);
    this.generator.emit(')');
  }

  toString(ast) {
    this.generator.used.push('System.Text');
    this.generator.emit(`Encoding.UTF8.GetString(`);
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  // 和sdk的hexEncode方法保持一致：https://github.com/aliyun/darabonba-openapi-util/blob/master/csharp/core/Client.cs
  // 原生方法 BitConverter.ToString()，生成以’-‘连接的16进制格式
  toHex(ast) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.ToHex(`);
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  toBase64(ast) {
    this.generator.emit('Convert.ToBase64String(');
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  toJSON(ast) {
    this.toString(ast);
  }

  length(ast) {
    this.getInstanceName(ast);
    this.generator.emit('.Length');
  }
}

class Map extends Builtin {
  constructor(generator) {
    super(generator, 'ConverterUtils');
  }

  length(ast) {
    this.getInstanceName(ast);
    this.generator.emit('.Count');
  }

  keySet(ast) {
    this.getInstanceName(ast);
    this.generator.emit('.Keys.ToList()');
  }

  entries(ast) {
    this.getInstanceName(ast);
    this.generator.emit('.ToList()');
  }

  toJSON(ast) {
    this.generator.used.push('Newtonsoft.Json');
    this.generator.emit('JsonConvert.SerializeObject(');
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  merge(ast) {
    const clientName = this.getClientName();
    this.generator.emit(`${clientName}.Merge(`);
    this.getInstanceName(ast);
    this.generator.emit(' , ');
    this.generator.visitExpr(ast.args[0]);
    this.generator.emit(')');
  }
}

class Entry extends Builtin {

  key(ast) {
    this.getInstanceName(ast);
    this.generator.emit('.Key');
  }

  value(ast) {
    this.getInstanceName(ast);
    this.generator.emit('.Value');
  }
}

class ModelInstance extends Builtin {

  toMap(ast) {
    this.getInstanceName(ast);
    this.generator.emit('.ToMap()');
  }

}

module.exports = (generator) => {
  const builtin = {};
  builtin['$Env'] = new Env(generator);
  builtin['$Logger'] = new Logger(generator);
  builtin['$XML'] = new XML(generator);
  builtin['$URL'] = new URL(generator);
  builtin['$Stream'] = new Stream(generator);
  builtin['$Number'] = new Number(generator);
  builtin['$JSON'] = new JSON(generator);
  builtin['$Form'] = new Form(generator);
  builtin['$File'] = new File(generator);
  builtin['$Bytes'] = new Bytes(generator);
  const converter = new Converter(generator);
  types.map(type => {
    builtin[`$${type}`] = converter;
  });

  const func = new Func(generator);
  builtin['$isNull'] = func;
  builtin['$sleep'] = func;
  builtin['$default'] = func;
  builtin['$equal'] = func;

  builtin['$String'] = new String(generator);
  builtin['$Array'] = new Array(generator);
  builtin['$Date'] = new Date(generator);
  builtin['$Map'] = new Map(generator);
  builtin['$Entry'] = new Entry(generator);
  builtin['$ModelInstance'] = new ModelInstance(generator);

  return builtin;
};