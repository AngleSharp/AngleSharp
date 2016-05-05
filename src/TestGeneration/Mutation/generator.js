var fs = require('fs');
var cs = require('../csharp.js');
var tests;

String.prototype.endsWith = function(suffix) {
  return this.indexOf(suffix, this.length - suffix.length) !== -1;
};

String.prototype.pascalize = function() {
  return this.replace(/(\w)(\w*)/g, function(g0, g1, g2) {
    return g1.toUpperCase() + g2.toLowerCase();
  }).replace(/\s/g, '');
};

test = function(name, callback) {
  var content = callback.toString().replace('function () {', '');
  content = content.substr(0, content.length - 1);
  content = content.replace(/'/g, '"').replace(/    /g, '');
  content = content.replace(/\.createElement/g, '.CreateElement');
  content = content.replace(/\.createTextNode/g, '.CreateTextNode');
  content = content.replace(/\.createDocumentFragment/g, '.CreateDocumentFragment');
  content = content.replace(/\.setAttribute/g, '.SetAttribute');
  content = content.replace(/\.appendChild\(/g, '.AppendChild(');
  content = content.replace(/\.insertBefore\(/g, '.InsertBefore(');
  content = content.replace(/\.removeChild\(/g, '.RemoveChild(');
  content = content.replace(/JsMutationObserver\(function\(\) \{\}\)/g, 'MutationObserver(() => {})');
  content = content.replace(/\.strictEqual/g, '.AreEqual');
  content = content.replace(/assert\./g, 'Assert.');
  content = content.replace(/takeRecords\(\)/g, 'Flush().ToArray()');
  content = content.replace(/\.observe\(([A-Za-z]+[A-Za-z0-9]*), \{/g, '.Connect($1, new MutationObserverInit {');
  content = content.replace(/expectRecord\(records\[(\d+)\], \{/g, 'AssertRecord(records[$1], new TestMutationRecord {');
  content = content.replace(/expectRecord\(records2\[(\d+)\], \{/g, 'AssertRecord(records2[$1], new TestMutationRecord {');
  content = content.replace(/attributes:/g, 'IsObservingAttributes =');
  content = content.replace(/subtree:/g, 'IsObservingSubtree =');
  content = content.replace(/childList:/g, 'IsObservingChildNodes =');
  content = content.replace(/attributeFilter:/g, 'AttributeFilters =');
  content = content.replace(/attributeOldValue:/g, 'IsExaminingOldAttributeValue =');
  content = content.replace(/characterData:/g, 'IsObservingCharacterData =');
  content = content.replace(/type:/g, 'Type =');
  content = content.replace(/target:/g, 'Target =');
  content = content.replace(/nextSibling:/g, 'NextSibling =');
  content = content.replace(/previousSibling:/g, 'PreviousSibling =');
  content = content.replace(/addedNodes:/g, 'Added =');
  content = content.replace(/removedNodes:/g, 'Removed =');
  content = content.replace(/attributeNamespace:/g, 'AttributeNamespace =');
  content = content.replace(/attributeName:/g, 'AttributeName =');
  content = content.replace(/oldValue:/g, 'PreviousValue =');
  content = content.replace(/\.data/g, '.TextContent');
  content = content.replace(/\.innerHTML/g, '.InnerHtml');
  content = content.replace(/\.length/g, '.Count()');
  content = content.replace(/\.firstChild/g, '.FirstChild');
  content = content.replace(/expect\(\)\.fail\(\)/g, 'Assert.Fail()');

  tests.push({
    name: 'MutationObserver' + name.pascalize(),
    actions: content.split('\n')
  });
};

setup = function() { };

teardown = function() { };

suite = function(name, callback) {
  tests = [];

  callback();

  return {
    name: name.pascalize(),
    tests: tests
  };
};

function generate() {
  var tests = [];
  var targetFile = 'MutationObserver.cs';

  fs.readdirSync('.').forEach(function(file) {
    if (file.endsWith('.js') && !file.endsWith('generator.js')) {
      var source = fs.readFileSync(file, 'utf8');
      var generator = new Function('return ' + source);
      var result = generator();
      tests = tests.concat(result.tests);
    }
  });

  var callback = function(err) {
    if (err)
      console.log(err);
    else
      console.log("The tests have been successfully generated in " + targetFile + "!");
  };

  var testClass = cs.newClass('MutationObserverTests').addAttribute('TestFixture');

  for (var i = 0; i < tests.length; ++i) {
    var t = tests[i];
    var method = cs.newMethod(t.name)
                   .addAttribute('Test')
                   .addLine('var document = DocumentBuilder.Html("");');

    for (var j = 0; j < t.actions.length; ++j)
      method.addLine(t.actions[j]);

    testClass.addMethod(method);
  }

  cs.newFile(targetFile)
    .setNamespace('UnitTests.Html')
    .addNamespaces(['AngleSharp', 'AngleSharp.DOM', 'AngleSharp.DOM.Html', 'NUnit.Framework', 'System'])
    .addClass(testClass)
    .save(callback);
}

generate();