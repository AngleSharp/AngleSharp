// Regenerate only this legacy W3C case: node src/TestGeneration/Selectors/generate-css3-modsel-14.js
// The original full-suite generator is not checked in. Keep unrelated cases byte-for-byte.
var fs = require('fs');
var path = require('path');
var cs = require('../csharp.js');
var fixture = JSON.parse(fs.readFileSync(path.join(__dirname, 'css3-modsel-14.json'), 'utf8'));
var target = path.join(__dirname, '../../AngleSharp.Core.Tests/Css/CssW3CSelector.cs');
var contents = fs.readFileSync(target, 'utf8');
var source = (fixture.htmlProjection + fixture.source).replace(/"/g, '""');
var method = cs.newMethod('MoreThanOneClassSelectorA')
    .addAttribute('Test')
    .addLine('// ' + fixture.copyright)
    .addLine('// ' + fixture.licenseUrl)
    .addLine('var source = @"' + source + '";')
    .addLine('var doc = source.ToHtmlDocument();')
    .addLine('');

fixture.selectors.forEach(function (entry, index) {
    var name = 'selector' + (index + 1);
    method.addLine('var ' + name + ' = doc.QuerySelectorAll("' + entry[0] + '");')
        .addLine('Assert.AreEqual(' + entry[1] + ', ' + name + '.Length);');
});

var generated = method.serialize().map(function (line) {
    return line.trim() ? '        ' + line.replace(/^\t/, '    ') : '';
}).join('\n');
var pattern = /^        \[Test\]\r?\n        public void MoreThanOneClassSelectorA\(\)\r?\n        \{[\s\S]*?^        \}/gm;
var matches = contents.match(pattern);

if (!matches || matches.length !== 1) {
    throw new Error('Expected exactly one MoreThanOneClassSelectorA method.');
}

fs.writeFileSync(target, contents.replace(pattern, function () { return generated; }));
