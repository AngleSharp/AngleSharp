var fs = require('fs');
var cs = require('../csharp.js');

String.prototype.endsWith = function(suffix) {
  return this.indexOf(suffix, this.length - suffix.length) !== -1;
};

String.prototype.pascalize = function() {
  return this.replace(/(\w)(\w*)/g, function(g0, g1, g2) {
    return g1.toUpperCase() + g2.toLowerCase();
  });
};

function generate(fileName) {
  var name = fileName.replace(/\.json$/i, '');
  var target = "Validity" + name;
  var targetFile = target + '.cs';
  var obj = JSON.parse(fs.readFileSync(fileName, 'utf8'));
  var testee = obj.testElements;
  var tests = [];

  function set_conditions(test, setup) {
    ["required", "pattern", "step", "max", "min", "maxlength", "value", "multiple", "checked", "selected"].forEach(function(item) {
      test.actions.push('element.RemoveAttribute("' + item + '")');
    });

    for (var attr in setup) {
      var value = setup[attr];

      if (attr === 'disabled' || attr === 'readOnly' || attr === 'required' || attr === 'checked' || attr === 'multiple')
        value = value ? '"' + attr + '"' : 'null';
      else
        value = '"' + value + '"';

      if (attr !== 'value')
        test.actions.push('element.SetAttribute("' + attr + '", ' + value + ')'); 
      else
        test.actions.push('element.Value = ' + value);
    }
  }

  function set_dirty(test, element) {
    element = element || 'element';
    test.actions.push(element + '.IsDirty = true');
  }

  function expand(str, vars) {
    var names = Object.keys(vars);

    for (var i = 0; i < names.length; ++i) {
      var regexp = new RegExp('\\$' + names[i] + '\\b', 'g');
      str = str.replace(regexp, vars[names[i]]);
    }

    return str;
  }

  function assert(test, actual, expected) {
    test.asserts.push({actual: actual, expected: expected});
  }

  function assert_true(test, property) {
    assert(test, property, 'true');
  }

  function assert_false(test, property) {
    assert(test, property, 'false');
  }

  function assert_property(test, data, propertyName) {
   if (data.expected)
     assert_true(test, 'element.Validity.' + propertyName);
   else
     assert_false(test, 'element.Validity.' + propertyName);
  }

  var generators = {
    test_tooLong: function(test, data) {
      set_conditions(test, data.conditions);

      if (data.dirty)
        set_dirty(test);

      assert_property(test, data, 'IsTooLong');
    },
    test_tooShort: function(test, data) {
      set_conditions(test, data.conditions);

      if (data.dirty)
        set_dirty(test);

      assert_property(test, data, 'IsTooShort');
    },
    test_patternMismatch: function(test, data) {
      set_conditions(test, data.conditions);
      assert_property(test, data, 'IsPatternMismatch');
    },
    test_valueMissing: function(test, data) {
      set_conditions(test, data.conditions);
      assert_property(test, data, 'IsValueMissing');
    },
    test_typeMismatch: function(test, data) {
      set_conditions(test, data.conditions);
      assert_property(test, data, 'IsTypeMismatch');
    },
    test_rangeOverflow: function(test, data) {
      set_conditions(test, data.conditions);
      assert_property(test, data, 'IsRangeOverflow');
    },
    test_rangeUnderflow: function(test, data) {
      set_conditions(test, data.conditions);
      assert_property(test, data, 'IsRangeUnderflow');
    },
    test_stepMismatch: function(test, data) {
      set_conditions(test, data.conditions);
      assert_property(test, data, 'IsStepMismatch');
    },
    test_badInput: function(test, data) {
      set_conditions(test, data.conditions);
      assert_property(test, data, 'IsBadInput');
    },
    test_customError: function(test, data) {
      test.actions.push('element.SetCustomValidity("' + data.conditions.message + '")');

      if (data.expected) {
        assert_true(test, 'element.Validity.IsCustomError');
        assert(test, 'element.ValidationMessage', '"' + data.conditions.message + '"');
      } else {
        assert_false(test, 'element.Validity.IsCustomError');
        assert(test, 'element.ValidationMessage', '""');
      }
    },
    test_isValid: function (test, data) {
      set_conditions(test, data.conditions);

      if (data.dirty)
        set_dirty(test);

      assert_property(test, data, 'IsValid');
    },
    test_willValidate: function(test, data) {
      set_conditions(test, data.conditions);

      if (data.ancestor) {
        test.actions.push('var dl = document.CreateElement("datalist")');
        test.actions.push('dl.AppendChild(element)');
      }

      if (data.expected)
        assert_true(test, 'element.WillValidate');
      else
        assert_false(test, 'element.WillValidate');
    },
    test_checkValidity: function(test, data) {
      set_conditions(test, data.conditions);

      if (data.dirty)
        set_dirty(test);
    
      if (data.expected)
        assert_true(test, 'element.CheckValidity()');
      else
        assert_false(test, 'element.CheckValidity()');

      test.actions.push('var fm = document.CreateElement("form") as IHtmlFormElement');
      test.actions.push('var element2 = element.Clone(true)');
      test.actions.push('fm.AppendChild(element2)');
      test.actions.push('document.Body.AppendChild(fm)');

      if (data.dirty)
        set_dirty(test, 'element2');

      if (data.expected)
        assert_true(test, 'fm.CheckValidity()');
      else
        assert_false(test, 'fm.CheckValidity()');
    },
  };

  var generator = generators["test_" + name];

  for (var i = 0; i < testee.length; i++) {
    var ti = testee[i];

    if (ti.types.length > 0) {
      for (var typ in ti.types) {
        var tag = ti.tag;
        var type = ti.types[typ];

        for (var j = 0; j < ti.testData.length; j++) {
          var test = { name : ["Test", name.pascalize(), tag.pascalize(), type.pascalize(), (j + 1)].join(''), tag: tag, actions: [], asserts: [], variables: { type: '"' + type + '"' } };
          test.actions.push('element.Type = $type');
          test.asserts.push({ actual: 'element.Type', expected: '$type' });
          generator(test, ti.testData[j]); 
          tests.push(test);
        }
      }
    } else {
      var tag = ti.tag;

      for (var j = 0; j < ti.testData.length; j++) {
        var test = { name : ["Test", name.pascalize(), tag.pascalize(), (j + 1)].join(''), tag: tag, actions: [], asserts: [], variables: { } };

        if (tag === "select") {
          test.actions.push('var option1 = document.CreateElement<IHtmlOptionElement>()');
          test.actions.push('option1.Text = "test1"');
          test.actions.push('option1.Value = ""');
          test.actions.push('var option2 = document.CreateElement<IHtmlOptionElement>()');
          test.actions.push('option2.Text = "test1"');
          test.actions.push('option2.Value = "1"');
          test.actions.push('element.AddOption(option1)');
          test.actions.push('element.AddOption(option2)');
        }

        generator(test, ti.testData[j]);
        tests.push(test);
      }
    }
  }

  var callback = function(err) {
    if (err)
      console.log(err);
    else
      console.log("The tests for " + name + " have been successfully generated in " + targetFile + "!");
  };

  var testClass = cs.newClass(target)
                    .addAttribute('TestFixture');

  for (var i = 0; i < tests.length; ++i) {
    var t = tests[i];
    var method = cs.newMethod(t.name)
                   .addAttribute('Test')
                   .addLine('var document = DocumentBuilder.Html("");')
                   .addLine('var element = document.CreateElement("' + t.tag + '") as HTML' + t.tag.pascalize() + 'Element;')
                   .addLine('Assert.IsNotNull(element);');

    for (var j = 0; j < t.actions.length; ++j)
      method.addLine(expand(t.actions[j], t.variables) + ';');

    for (var j = 0; j < t.asserts.length; ++j)
      method.addLine(['Assert.AreEqual(', expand(t.asserts[j].expected, t.variables), ', ', expand(t.asserts[j].actual, t.variables), ')'].join('') + ';');

    testClass.addMethod(method);
  }

  cs.newFile(targetFile)
    .setNamespace('UnitTests.Html')
    .addNamespaces(['AngleSharp', 'AngleSharp.DOM.Html', 'NUnit.Framework', 'System'])
    .addClass(testClass)
    .save(callback);
}

fs.readdir('.', function(err, files) {
  for (var i = 0; i < files.length; i++) {
    var file = files[i];

    if (file.endsWith('.json'))
      generate(file);
  }
});