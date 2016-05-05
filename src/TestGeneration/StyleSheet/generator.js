// Convert tests from https://github.com/reworkcss/css/tree/master/test/cases
var fs = require('fs');
var cs = require('../csharp.js');

String.prototype.endsWith = function (suffix) {
	return this.indexOf(suffix, this.length - suffix.length) !== -1;
};

String.prototype.pascalize = function () {
	return this.replace(/(\w)(\w*)/g, function (g0, g1, g2) {
		return g1.toUpperCase() + g2.toLowerCase();
	}).replace(/\-/g, '');
};

String.prototype.sanatize = function () {
	return this.replace(/"/g, '""');
};

var testCases = fs.readdirSync('.').filter(function (fn) { 
	return fn.endsWith('.json');  
}).map(function (fn) { 
	var obj = JSON.parse(fs.readFileSync(fn, 'utf-8')); 
	obj.name = fn.replace('.json', '');
	return obj;
});

var possibleNames = ['rule', 'subrule', 'subsubrule', 'sssrule', 's4rule', 's5rule', 's6rule'];

var ruleIterator = function (method, parent, name) {
	var newName = possibleNames.filter(function (possibleName) {
		return name.indexOf(possibleName + '.') !== 0;
	})[0];
	method.addLine('Assert.AreEqual(' + parent.rules.length + ', ' + name + '.Rules.Length);');
	method.addLine('');
	method.addLine('foreach (var ' + newName + ' in ' + name + '.Rules)');
	method.addLine('{');

	parent.rules.forEach(function (rule) {
		var inspector = ruleTypes[rule.type] || function () { };
		inspector(method, rule, newName);
	});

	method.addLine('}');
};

var areEqual = function (method, expected, type, name, property) {
	var value = (expected || '').sanatize();
	method.addLine('\tAssert.AreEqual(@"' + value + '", ((' + type + ')' + name  +').' + property + ');');
}

var ruleTypes = {
	'charset': function (method, rule, name) {
		areEqual(method, rule.charset, 'ICssCharsetRule', name, 'CharacterSet');
	},
	'rule': function (method, rule, name) {
		areEqual(method, rule.selectors.join(', '), 'ICssStyleRule', name, 'SelectorText');

		rule.declarations.forEach(function (declaration) {
			areEqual(method, declaration.value, 'ICssStyleRule', name, 'Style["' + declaration.property + '"]');
		});
	},
	'media': function (method, rule, name) {
		areEqual(method, rule.media, 'ICssMediaRule', name, 'MediaText');
		ruleIterator(method, rule, name);
	},
	'import': function (method, rule, name) {
		areEqual(method, rule.import, 'ICssImportRule', name, 'Href');
	},
	'namespace': function (method, rule, name) {
		areEqual(method, rule.namespace, 'ICssNamespaceRule', name, 'NamespaceUri');
	}
};

var testClass = cs.newClass('CssCasesTests').
                   addImplementation('CssConstructionFunctions').
                   addAttribute('TestFixture');

testCases.forEach(function (testCase) {
	var source = testCase.source.sanatize();
	var method = cs.newMethod(testCase.name.pascalize())
	               .addAttribute('Test')
	               .addLine('var sheet = ParseStyleSheet(@"' + source + '");');
	
	ruleIterator(method, testCase.result.stylesheet, 'sheet');
	testClass.addMethod(method);
});

cs.newFile('CssCases.cs').
   setNamespace('AngleSharp.Core.Tests.Css').
   addNamespaces(['AngleSharp.Dom.Css', 'NUnit.Framework']).
   addClass(testClass).
   save(function (err) { console.log(err || "The tests have been successfully generated!"); });