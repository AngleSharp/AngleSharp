function CodeBlock() {
  this.content = [];
  this.line = function(line) {
    this.content.push(line || '');
    return this;
  };
  this.lines = function(lines) {
    var intended = intend(lines);
    this.content = this.content.concat(intended);
    return this;
  };
  this.serialize = function() {
    return this.content.join('\n');
  };
}

function intend(arr) {
  return arr.map(function(value) {
    return '\t' + value;
  });
} 

function CsharpFile(name) {
  var namespaces = [];
  var classes = [];
  var namespace = '';
  this.setNamespace = function(newNamespace) {
  	namespace = newNamespace;
  	return this;
  };
  this.addClass = function(cls) {
    classes.push(cls);
    return this;
  };
  this.addNamespace = function(namespace) {
    namespaces.push(namespace);
    return this;
  };
  this.addNamespaces = function(multipleNamespaces) {
  	namespaces = namespaces.concat(multipleNamespaces);
  	return this;
  };
  this.serialize = function() {
    var lines = new CodeBlock();
    
    for (var i = 0; i < namespaces.length; i++)
      lines.line('using ' + namespaces[i] + ';');

    lines.line();

    if (namespace)
      lines.line('namespace ' + namespace).line('{');
    
    for (var i = 0; i < classes.length; i++)
      lines.lines(classes[i].serialize());

    if (namespace)
      lines.line('}');

    return lines.content;
  };
  this.save = function(callback) {
    var fs = require('fs');
    fs.writeFile(name, this.serialize().join('\n'), callback);
    return this;
  };
}

function CsharpClass(name) {
  var attributes = [];
  var methods = [];
  this.addMethod = function(method) {
    methods.push(method);
    return this;
  };
  this.addAttribute = function(attribute) {
    attributes.push(attribute);
    return this;
  };
  this.serialize = function() {
    var cls = new CodeBlock();

    for (var i = 0; i < attributes.length; i++)
      cls.line('[' + attributes[i] + ']');

    cls.line('public class ' + name);
    cls.line('{');

    for (var i = 0; i < methods.length; i++) {
      if (i > 0)
      	cls.line();

      cls.lines(methods[i].serialize());	
    }
    
    return cls.line('}').content;
  };
}

function CsharpMethod(name) {
  var attributes = [];
  var lines = [];
  this.addLine = function(line) {
    lines.push(line);
    return this;
  };
  this.addLines = function(morelines) {
  	lines = lines.concat(morelines);
  	return this;
  };
  this.addAttribute = function(attribute) {
    attributes.push(attribute);
    return this;
  };
  this.serialize = function() {
    var method = new CodeBlock();

    for (var i = 0; i < attributes.length; i++)
      method.line('[' + attributes[i] + ']');

    method.line('public void ' + name + '()');
    return method.line('{').lines(lines).line('}').content;
  };
}

exports.newFile = function(name) {
	return new CsharpFile(name);
};

exports.newClass = function(name) {
	return new CsharpClass(name);
};

exports.newMethod = function(name) {
	return new CsharpMethod(name);
};