English | [简体中文](/README-zh-CN.md)

# Darabonba Code Generator for CSharp
[![NPM version][npm-image]][npm-url]
[![build status][travis-image]][travis-url]
[![codecov][cov-image]][cov-url]
[![David deps][david-image]][david-url]
[![npm download][download-image]][download-url]

[npm-image]: https://img.shields.io/npm/v/@darabonba/csharp-generator.svg?style=flat-square
[npm-url]: https://npmjs.org/package/@darabonba/csharp-generator
[travis-image]: https://img.shields.io/travis/aliyun/darabonba-csharp-generator.svg?style=flat-square
[travis-url]: https://travis-ci.org/github/aliyun/darabonba-csharp-generator
[cov-image]: https://codecov.io/gh/aliyun/darabonba-csharp-generator/branch/master/graph/badge.svg
[cov-url]: https://codecov.io/gh/aliyun/darabonba-csharp-generator
[david-image]: https://img.shields.io/david/aliyun/darabonba-csharp-generator.svg?style=flat-square
[david-url]: https://david-dm.org/aliyun/darabonba-csharp-generator
[download-image]: https://img.shields.io/npm/dm/@darabonba/csharp-generator.svg?style=flat-square
[download-url]: https://npmjs.org/package/@darabonba/csharp-generator

## Installation

Darabonba Code Generator was designed to work in Node.js. The preferred way to install the Generator is to use the [NPM](https://www.npmjs.com/) package manager. Simply type the following into a terminal window:
```shell
npm install @darabonba/csharp-generator
```

## Usage

```js
'use strict';
const path = require('path');
const fs = require('fs');

const Parser = require('@darabonba/parser');
const CSGenerator = require('@darabonba/csharp-generator');

const sourceDir = "<Darabonda package directory>";
const outputDir = "<Generate output directory>";

// generate ast data by Parser
let packageMetaFilePath = path.join(sourceDir, 'Teafile');
let packageMeta = JSON.parse(fs.readFileSync(packageMetaFilePath, 'utf8'));
let mainFile = path.join(sourceDir, packageMeta.main);
let ast = Parser.parse(fs.readFileSync(mainFile, 'utf8'), mainFile);

// initialize generator
let generatorConfig = {
  ...packageMeta,
  pkgDir: sourceDir,
  outputDir
};

let generator = new CSGenerator(generatorConfig);

// generate csharp code by generator
generator.visit(ast);

// The execution result will be output in the 'outputDir'
```

## Issues

[Opening an Issue](https://github.com/aliyun/darabonba-csharp-generator/issues/new/choose), Issues not conforming to the guidelines may be closed immediately.

## Changelog

Detailed changes for each release are documented in the [release notes](/CHANGELOG.md).

## License

[Apache-2.0](/LICENSE)
Copyright (c) 2009-present, Alibaba Cloud All rights reserved.