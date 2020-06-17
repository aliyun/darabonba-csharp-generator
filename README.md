English | [简体中文](/README-zh-CN.md)

# Darabonba Code Generator for CSharp

## Install

> Darabonba Code Generator was designed to work in Node.js.
> The preferred way to install the Generator is to use the [NPM](https://www.npmjs.com/) package manager.
> Simply type the following into a terminal window:
```shell
npm install @darabonba/csharp-generator
```

## Usage

> Generate CSharp Code
```javascript
'use strict';
const path = require('path');
const fs = require('fs');
const DarabonbaParser = require('@darabonba/parser');
const DarabonbaCSGenerator = require('@darabonba/csharp-generator');
const sourceDir = "<Darabonda package directory>";
const outputDir = "<Generate output directory>";
// generate AST data by DarabonbaParser
let darabonbaPackageMetaFilePath = path.join(sourceDir, 'Teafile');
let darabonbaMainFile = path.join(sourceDir, darabonbaPackageMeta.main);
let darabonbaPackageMeta = JSON.parse(fs.readFileSync(darabonbaPackageMetaFilePath, 'utf8'));
let darabonbaAST = DarabonbaParser.parse(fs.readFileSync(darabonbaMainFile, 'utf8'), darabonbaMainFile);
// initialize generator
let generatorConfig = {
      ...darabonbaPackageMeta,
      pkgDir: sourceDir,
      outputDir
    };
let generator = new DarabonbaCSGenerator(generatorConfig);
// generate csharp code by generator
generator.visit(darabonbaAST);
// The execution result will be output in the 'outputDir'
```

## Issues

[Opening an Issue](https://github.com/aliyun/darabonba-csharp-generator/issues/new/choose), Issues not conforming to the guidelines may be closed immediately.

## Changelog

Detailed changes for each release are documented in the [release notes](/CHANGELOG.md).

## License

[Apache-2.0](/LICENSE)
Copyright (c) 2009-present, Alibaba Cloud All rights reserved.