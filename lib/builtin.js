'use strict';
const DSL = require('@darabonba/parser');
const { _vid, _name, _upperFirst } = require('./helper');

const types = [
  'integer', 'int8', 'int16', 'int32',
  'int64', 'long', 'ulong', 'string',
  'uint8', 'uint16', 'uint32', 'uint64',
  'number', 'float', 'double', 'boolean',
  'bytes', 'readable', 'writable', 'object', 'any'
];

const typesMap = {
  'integer': 'int',
  'int8': 'int',
  'uint8': 'int',
  'int16': 'int',
  'uint16': 'int',
  'int32': 'int',
  'uint32': 'int',
  'int64': 'long',
  'uint64': 'long',
  'long': 'long',
  'ulong': 'long',
  'float': 'float',
  'number': 'int',
};

class Builtin {
  constructor(generator, module = '', methods = []) {
    this.generator = generator;
    this.module = module;

    methods.forEach(method => {
      this[method] = function (ast, level, env) {
        this.generator.emit(`${this.module}.${_upperFirst(method)}`);
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
  split(ast, level, env) {
    this.generator.used.push('System.Linq');
    this.getInstanceName(ast);
    this.generator.emit('.Split');
    this.generator.visitArgs(ast, level, env);
    this.generator.emit('.ToList()');
  }

  // TODO 正则规则不一致，待讨论
  replace(ast, level, env) {
    this.generator.emit('TeaString.Replace(');
    this.getInstanceName(ast);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(', ');
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

  // TODO 包核心库，现：fullStr.Substring(start.Value, end.Value - start.Value);
  subString(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit('.Substring');
    this.generator.emit('(');
    this.generator.visitExpr(ast.args[0], level, env);
    if (ast.args[0].id && ast.args[0].id.tag === DSL.Tag.Tag.ID) {
      this.generator.emit('.Value');
    }
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[1], level, env);
    if (ast.args[1].id && ast.args[1].id.tag === DSL.Tag.Tag.ID) {
      this.generator.emit('.Value');
    }
    this.generator.emit(' - ');
    this.generator.visitExpr(ast.args[0], level, env);
    if (ast.args[0].id && ast.args[0].id.tag === DSL.Tag.Tag.ID) {
      this.generator.emit('.Value');
    }
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
    this.generator.visitExpr(ast.args[0], level, env);

  }

  empty(ast) {
    this.generator.emit('String.IsNullOrEmpty(');
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  toBytes(ast, level, env) {
    this.generator.emit('TeaString.ToBytes(');
    this.getInstanceName(ast);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  parseInt(ast) {
    this.generator.emit('TeaString.ParseInt(');
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  parseLong(ast) {
    this.generator.emit('TeaString.ParseLong(');
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  parseFloat(ast) {
    this.generator.emit('TeaString.ParseFloat(');
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  parseDouble(ast) {
    this.generator.used.push(`System.Globalization`);
    this.generator.emit('Double.Parse(');
    this.getInstanceName(ast);
    this.generator.emit(', NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo)');
  }
}

class Env extends Builtin {
  get(ast, level, env) {
    const key = ast.args[0];
    this.generator.emit(`Environment.GetEnvironmentVariable(`);
    this.generator.visitExpr(key, level, env);
    this.generator.emit(')');
  }

  set(ast, level, env) {
    const key = ast.args[0];
    this.generator.emit(`Environment.SetEnvironmentVariable(`);
    this.generator.visitExpr(key, level, env);
    this.generator.emit(', ');
    const value = ast.args[1];
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
  // TODO 包核心库，现：inum = (int)Math.Floor((double)(inum));
  floor(ast, level, env) {
    this.generator.emit('(int)Math.Floor((double)');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  // TODO 同上 
  round(ast, level, env) {
    this.generator.emit('(int)Math.Round((double)');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  min(ast, level, env) {
    this.generator.emit('TeaNumber.Min(');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[1], level, env);
    this.generator.emit(')');
  }

  max(ast, level, env) {
    this.generator.emit('TeaNumber.Max(');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[1], level, env);
    this.generator.emit(')');
  }

  random() {
    this.generator.emit('new Random().NextDouble()');
  }

  parseInt(ast) {
    this.generator.emit('TeaNumber.ParseInt(');
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  parseLong(ast) {
    this.generator.emit('TeaNumber.ParseLong(');
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  parseFloat(ast) {
    this.generator.emit('TeaNumber.ParseFloat(');
    this.getInstanceName(ast);
    this.generator.emit(')');
  }

  parseDouble(ast) {
    this.generator.emit('Double.Parse(');
    this.getInstanceName(ast);
    this.generator.emit('.Value.ToString())');
  }

  itol(ast) {
    this.getInstanceName(ast);
  }

  ltoi(ast) {
    this.generator.emit('(int)');
    this.getInstanceName(ast);
    this.generator.emit('.Value');
  }
}

class JSON extends Builtin {
  stringify(ast, level, env) {
    this.generator.emit('TeaJSON.SerializeObject');
    this.generator.visitArgs(ast, level, env);
  }

  parseJSON(ast, level, env) {
    // TODO Newtonsoft.Json需要添加packageReference到csproj
    this.generator.used.push(`Newtonsoft.Json`);
    this.generator.emit('JsonConvert.DeserializeObject');
    this.generator.visitArgs(ast, level, env);
  }
}

class Array extends Builtin {
  join(ast, level, env) {
    this.generator.emit(`string.Join(`);
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
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  get(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit(`[`);
    this.generator.visitExpr(ast.args[0], level, env);
    if (ast.args[0].id && ast.args[0].id.tag === DSL.Tag.Tag.ID) {
      this.generator.emit('.Value');
    }
    this.generator.emit(`]`);
  }

  sort(ast, level, env) {
    this.generator.emit('TeaArray.Sort(');
    this.getInstanceName(ast);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  // 写两句可能有问题，如 args.shift().xxx的情况，写在核心库里也无法更新List，因为返回的是List的第一个元素。
  shift(ast, level) {
    this.getInstanceName(ast);
    this.generator.emit('[0];\n');
    this.getInstanceName(ast, level);
    this.generator.emit('.RemoveAt(0)');
  }

  pop(ast, level) {
    this.getInstanceName(ast);
    this.generator.emit('[');
    this.getInstanceName(ast);
    this.generator.emit('.Count - 1];\n');
    this.getInstanceName(ast, level);
    this.generator.emit('.RemoveRange(');
    this.getInstanceName(ast);
    this.generator.emit('.Count - 1, 1)');
  }

  unshift(ast, level, env) {
    this.generator.emit(`TeaArray.unshift(`);
    this.getInstanceName(ast);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  push(ast, level, env) {
    this.generator.emit(`TeaArray.push(`);
    this.getInstanceName(ast);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  concat(ast, level, env) {
    this.generator.emit('TeaArray.Concat(');
    this.getInstanceName(ast);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  append(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit('.Insert(');
    const position = ast.args[1];
    const value = ast.args[0];
    this.generator.visitExpr(position, level, env);
    this.generator.emit(', ');
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
    this.generator.visitExpr(value, level, env);
    this.generator.emit('))');
  }
}

class Func {
  constructor(generator) {
    this.generator = generator;
  }

  sleep(ast, level, env) {
    if (env.isAsyncMode) {
      this.generator.emit('await TeaCore.SleepAsync(');
    } else {
      this.generator.emit('TeaCore.Sleep(');
    }

    this.generator.visitExpr(ast.args[0], level, env);
    if (ast.args[0].id && ast.args[0].id.tag === DSL.Tag.Tag.ID) {
      this.generator.emit('.Value');
    }

    this.generator.emit(')');
  }

  isNull(ast, level, env) {
    this.generator.emit('TeaFunc.IsNull(');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  equal(ast, level, env) {
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(' == ');
    this.generator.visitExpr(ast.args[1], level, env);
  }

  default(ast, level, env) {
    this.generator.emit('!String.IsNullOrEmpty(');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(') ? ');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(' : ');
    this.generator.visitExpr(ast.args[1], level, env);
  }
}

class XML extends Builtin {
  constructor(generator) {
    const methods = ['parseXml', 'toXML'];
    super(generator, `TeaXML`, methods);
  }
}

class URL extends Builtin {
  constructor(generator) {
    const methods = ['parse', 'urlEncode', 'percentEncode', 'pathEncode'];
    super(generator, `TeaURL`, methods);
  }
}

class Stream extends Builtin {
  constructor(generator) {
    const methods = ['readAsBytes', 'readAsBytesAsync', 'readAsJSON', 'readAsJSONAsync', 'readAsString', 'readAsStringAsync', 'readAsSSE', 'readAsSSEAsync'];
    super(generator, 'TeaStream', methods);
  }

  read(ast, level, env) {
    this.generator.emit('TeaStream.Read(');
    this.getInstanceName(ast);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }

  write(ast, level, env) {
    this.getInstanceName(ast);
    this.generator.emit('.Write(');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(', 0, ');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit('.Length');
    this.generator.emit(')');
  }

  pipe(ast, level, env) {
    this.generator.emit('TeaStream.Pipe(');
    this.getInstanceName(ast);
    this.generator.emit(', ');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit(')');
  }
}

class Converter {
  constructor(generator) {
    this.generator = generator;
    this.stream = new Stream(generator);
    Object.keys(typesMap).forEach(key => {
      // 支持小数转整数，int/uint/number都转成Int32，long/ulong都转成Int64。
      this[key] = function (ast, level, env) {
        generator.emit(`TeaConverter.Parse${_upperFirst(typesMap[key])}(`);
        generator.visitExpr(ast.args[0], level, env);
        generator.emit(')');
      };
    });
  }

  double(ast, level, env) {
    this.generator.emit('Double.Parse(');
    this.generator.visitExpr(ast.args[0], level, env);
    this.generator.emit('.ToString())');
  }

  string(ast, level, env) {
    this.generator.emit('(');
    const expr = ast.args[0];
    this.generator.visitExpr(expr, level, env);
    this.generator.emit(').ToString()');
  }

  boolean(ast, level, env) {
    const expr = ast.args[0];
    this.generator.emit('bool.Parse(');
    this.generator.visitExpr(expr, level, env);
    this.generator.emit(')');
  }

  // 只允许传字符串
  bytes(ast, level, env) {
    this.generator.used.push('System.Text');
    const expr = ast.args[0];
    this.generator.emit('Encoding.UTF8.GetBytes(');
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
    this.generator.visitExpr(expr, level, env);
    this.generator.emit('.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value)');
  }

  readable(ast, level, env) {
    this.generator.emit('TeaConverter.ToStream(');
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
    super(generator, 'TeaForm', methods);
  }
}

class File extends Builtin {
  constructor(generator) {
    // TODO read/write/close
    const methods = ['createReadStream', 'createWriteStream', 'exists'];
    super(generator, 'TeaFile', methods);
  }
}

class Bytes extends Builtin {
  from(ast, level, env) {
    // 同 TeaString.ToBytes
    this.generator.emit('TeaBytes.From(');
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
    this.generator.emit('TeaBytes.ToHex(');
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
    this.generator.emit('TeaConverter.merge(');
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

  return builtin;
};