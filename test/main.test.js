'use strict';

const path = require('path');
const fs = require('fs');
const assert = require('assert');
const expect = require('expect.js');

const DSL = require('@darabonba/parser');

let Generator = require('../lib/generator');

const fixturesDir = path.join(__dirname, 'fixtures');
const outputDir = path.join(__dirname, 'output');

function compareDirectories(expectedDir, outputDir, moduleName) {
  const expectedFiles = fs.readdirSync(expectedDir);
  const outputFiles = fs.readdirSync(outputDir);
  for (let fileName of expectedFiles) {
    // 忽略core/Properties目录的比较
    if (fileName === 'Properties' || fileName === 'bin' || fileName === 'obj') {
      continue;
    }
    if (!outputFiles.includes(fileName)) {
      assert.ok(false);
    }
    const expectedPath = path.join(outputDir, fileName);
    const actualPath = path.join(expectedDir, fileName);
    const expectedStat = fs.statSync(expectedPath);
    const actualStat = fs.statSync(actualPath);

    if (expectedStat.isDirectory() && actualStat.isDirectory()) {
      compareDirectories(expectedPath, actualPath, moduleName);
    }
    else if (expectedStat.isFile() && actualStat.isFile()) {
      const expectedContent = fs.readFileSync(expectedPath, 'utf8');
      const acutalContent = fs.readFileSync(actualPath, 'utf8');
      assert.deepStrictEqual(acutalContent, expectedContent);
    }
  }
}

function check(moduleName, options = {}) {
  let mainFilePath;
  if (!options.daraName) {
    mainFilePath = fs.existsSync(path.join(fixturesDir, moduleName, 'main.tea')) ? path.join(fixturesDir, moduleName, 'main.tea') : path.join(fixturesDir, moduleName, 'main.dara');
  } else {
    mainFilePath = fs.existsSync(path.join(fixturesDir, moduleName, options.daraName)) ? path.join(fixturesDir, moduleName, options.daraName) : '';
  }
  const moduleOutputDir = path.join(outputDir, moduleName);
  const generator = new Generator({
    outputDir: moduleOutputDir,
    ...options
  });
  const dsl = fs.readFileSync(mainFilePath, 'utf8');
  const ast = DSL.parse(dsl, mainFilePath);
  generator.visit(ast);

  compareDirectories(path.join(__dirname, `expected/${moduleName}`), path.join(__dirname, `output/${moduleName}`), moduleName);
}

describe('new Generator', function () {
  it('add builtin should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/builtin/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('builtin', {
      pkgDir: path.join(__dirname, 'fixtures/builtin'),
      libraries: pkg.libraries,
      exec: true,
      ...pkg.csharp,
      editable: true,
      releaseVersion: '1.0.11',
    });
  });

  it('typedef should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/typedef/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('typedef', {
      pkgDir: path.join(__dirname, 'fixtures/typedef'),
      libraries: pkg.libraries,
      exec: false,
      ...pkg.csharp,
      editable: true
    });
  });

  it('exception should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/exception/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('exception', {
      pkgDir: path.join(__dirname, 'fixtures/exception'),
      libraries: pkg.libraries,
      exec: true,
      ...pkg.csharp,
      editable: false
    });
  });

  it('alias should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/alias/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('alias', {
      pkgDir: path.join(__dirname, 'fixtures/alias'),
      libraries: pkg.libraries,
      exec: true,
      ...pkg.csharp,
      editable: false
    });
  });

  it('multi dara should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/multi/tea/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);

    check('multi', {
      pkgDir: path.join(__dirname, 'fixtures/multi/tea'),
      libraries: pkg.libraries,
      exec: true,
      ...pkg.csharp,
      editable: true,
      daraName: 'tea/sdk.dara'
    });
  });

  it('must pass in outputDir', function () {
    assert.throws(function () {
      new Generator({});
    }, function (err) {
      assert.deepStrictEqual(err.message, '`option.outputDir` should not empty');
      return true;
    });
  });

  it('one model should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/model/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('model', {
      pkgDir: path.join(__dirname, 'fixtures/model'),
      ...pkg.csharp
    });
  });

  it('complex model should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/complexModel/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('complexModel', {
      pkgDir: path.join(__dirname, 'fixtures/complexModel'),
      libraries: pkg.libraries,
      ...pkg.csharp,
      editable: 1
    });
  });

  it('one api should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/api/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('api', {
      libraries: pkg.libraries,
      pkgDir: path.join(__dirname, 'fixtures/api'),
      ...pkg.csharp,
    });
  });

  it('one function should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/function/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('function', {
      pkgDir: path.join(__dirname, 'fixtures/function'),
      ...pkg.csharp
    });
  });

  it('statements should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/statements/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('statements', {
      pkgDir: path.join(__dirname, 'fixtures/statements'),
      libraries: pkg.libraries,
      ...pkg.csharp
    });
  });

  it('import should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/import/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('import', {
      pkgDir: path.join(__dirname, 'fixtures/import'),
      libraries: pkg.libraries,
      ...pkg.csharp,
      editable: false
    });
  });

  it('complex should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/complex/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('complex', {
      pkgDir: path.join(__dirname, 'fixtures/complex'),
      libraries: pkg.libraries,
      ...pkg.csharp,
      editable: 1
    });
  });

  it('comment should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/comment/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('comment', {
      pkgDir: path.join(__dirname, 'fixtures/comment'),
      libraries: pkg.libraries,
      ...pkg.csharp,
      editable: 'test-other'
    });
  });

  it('tea should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/tea/Teafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('tea', {
      pkgDir: path.join(__dirname, 'fixtures/tea'),
      libraries: pkg.libraries,
      ...pkg.csharp,
      editable: 'true'
    });
  });

  it('noOption should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/noOption/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    //const mainFilePath = path.join(fixturesDir, 'noOption', 'main.dara');
    const moduleOutputDir = path.join(outputDir, 'noOption');
    expect(() => {
      new Generator({
        outputDir: moduleOutputDir,
        ...{
          pkgDir: path.join(__dirname, 'fixtures/noOption'),
          libraries: pkg.libraries,
          ...pkg.csharp
        }
      });
    }).to.throwException(function (e) { // get the exception object
      expect(e.message).to.be(`Darafile -> csharp -> namespace should not empty, please add csharp option into Darafile.
      example:
        "csharp": {
          "namespace": "NameSpace",
          "className": "Client"
        }`);
    });
  });

  it('console should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/console/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('console', {
      pkgDir: path.join(__dirname, 'fixtures/tea'),
      libraries: pkg.libraries,
      exec: true,
      ...pkg.csharp,
      editable: true
    });
  });

  it('csporj should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/csproj/Darafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('csproj', {
      pkgDir: path.join(__dirname, 'fixtures/csproj'),
      libraries: pkg.libraries,
      exec: true,
      ...pkg.csharp,
      editable: true
    });
  });
});
