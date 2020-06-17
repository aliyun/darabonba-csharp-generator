'use strict';

const path = require('path');
const fs = require('fs');
const assert = require('assert');

const DSL = require('@darabonba/parser');

let Generator = require('../lib/generator');

const expectedDir = path.join(__dirname, 'expected/');
const fixturesDir = path.join(__dirname, 'fixtures');
const outputDir = path.join(__dirname, 'output');

function check(moduleName, expectedFiles = [], options = {}) {
  const mainFilePath = path.join(fixturesDir, moduleName, 'main.tea');
  const moduleOutputDir = path.join(outputDir, moduleName);
  const generator = new Generator({
    outputDir: moduleOutputDir,
    ...options
  });

  const dsl = fs.readFileSync(mainFilePath, 'utf8');
  const ast = DSL.parse(dsl, mainFilePath);
  generator.visit(ast);
  expectedFiles.forEach(element => {
    const outputFilePath = path.join(outputDir, moduleName, 'core', element);
    const expectedFilePath = path.join(expectedDir, moduleName, 'core', element);
    const expected = fs.readFileSync(expectedFilePath, 'utf8');
    assert.deepStrictEqual(fs.readFileSync(outputFilePath, 'utf8'), expected);
  });
}

describe('new Generator', function () {
  it('must pass in outputDir', function () {
    assert.throws(function () {
      new Generator({});
    }, function (err) {
      assert.deepStrictEqual(err.message, '`option.outputDir` should not empty');
      return true;
    });
  });

  it('one model should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/model/Teafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('model', ['Client.cs', 'Models/MyModel.cs', 'Models/LowerModel.cs'], {
      pkgDir: path.join(__dirname, 'fixtures/model'),
      ...pkg.csharp
    });
  });

  it('one api should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/api/Teafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('api', ['Client.cs', 'Models/M.cs'], {
      pkgDir: path.join(__dirname, 'fixtures/api'),
      ...pkg.csharp
    });
  });

  it('one function should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/function/Teafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('function', ['Client.cs'], {
      pkgDir: path.join(__dirname, 'fixtures/function'),
      ...pkg.csharp
    });
  });

  it('statements should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/statements/Teafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('statements', ['Client.cs'], {
      pkgDir: path.join(__dirname, 'fixtures/statements'),
      libraries: pkg.libraries,
      ...pkg.csharp
    });
  });

  it('import should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/import/Teafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('import', ['Client.cs'], {
      pkgDir: path.join(__dirname, 'fixtures/import'),
      libraries: pkg.libraries,
      ...pkg.csharp
    });
  });
  
  it('complex should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/complex/Teafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('complex', ['Client.cs'], {
      pkgDir: path.join(__dirname, 'fixtures/complex'),
      libraries: pkg.libraries,
      ...pkg.csharp
    });
  });

  it('comment should ok', function () {
    const pkgContent = fs.readFileSync(path.join(__dirname, 'fixtures/comment/Teafile'), 'utf8');
    const pkg = JSON.parse(pkgContent);
    check('comment', ['Client.cs'], {
      pkgDir: path.join(__dirname, 'fixtures/comment'),
      libraries: pkg.libraries,
      ...pkg.csharp
    });
  });
});
