var fs = require('fs');var fs = require('fs');
var cs = require('../csharp.js');

function parse(input) {
  var relativeSchemes = ["ftp", "file", "gopher", "http", "https", "ws", "wss"],
      tokenMap = { "\\": "\\", "#": "#", n: "\n", r: "\r", s: " ", t: "\t", f: "\f" },
      resultMap = { s: "scheme", u: "username", pass: "password", h: "host", port: "port", p: "path", q: "query", f: "fragment" },
      results = [],
      lines = input.split("\n");

  function Test() {
    this.input = "";
    this.base = "";
    this.scheme = "";
    this.username = "";
    this.password = null;
    this.host = "";
    this.port = "";
    this.path = "";
    this.query = "";
    this.fragment = "";
    Object.defineProperties(this, {
      "href": { get: function() {
        return !this.scheme ? this.input : this.protocol + (
          relativeSchemes.indexOf(this.scheme) != -1 ? "//" + (
            ("" != this.username || null != this.password) ? this.username + (
              null != this.password ? ":" + this.password : ""
            ) + "@" : ""
          ) + this.host : ""
        ) + (this.port ? ":" + this.port : "") + this.path + this.query + this.fragment
      } },
      "protocol": { get: function() { return this.scheme + ":" } },
      "search": { get: function() { return "?" == this.query ? "" : this.query } },
      "hash": { get: function() { return "#" == this.fragment ? "" : this.fragment } }
    })
  }

  function normalize(input) {
    var output = "";

    for (var i = 0, l = input.length; i < l; i++) {
      var c = input[i];

      if (c === "\\") {
        var nextC = input[++i];

        if (tokenMap.hasOwnProperty(nextC))
          output += tokenMap[nextC];
        else if (nextC == "u")
          output += String.fromCharCode(parseInt(input[++i] + input[++i] + input[++i] + input[++i], 16));
        else
          throw new Error("Input is invalid.");
      } else
        output += c;
    }

    return output;
  }

  for (var i = 0, l = lines.length; i < l; i++) {
    var line = lines[i];

    if (line === "" || line.indexOf("#", 0) === 0) {
      continue;
    }
    var pieces = line.split(" "),
        result = new Test();

    result.input = normalize(pieces.shift());
    var base = pieces.shift();

    if (base === "" || base === undefined)
      result.base = results[results.length - 1].base;
    else
      result.base = normalize(base);

    for (var ii = 0, ll = pieces.length; ii < ll; ii++) {
      var piece = pieces[ii];

      if (piece.indexOf("#", 0) === 0)
        continue;

      var subpieces = piece.split(":"),
          token = subpieces.shift(),
          value = subpieces.join(":");

      result[resultMap[token]] = normalize(value);
    }

    results.push(result);
  }

  return results;
}

function generate(target, source) {
  var urltests = parse(fs.readFileSync(source, 'utf8'));
  var testClass = cs.newClass(target + 'Tests')
                    .addAttribute('TestFixture');

  for(var i = 0; i < urltests.length; i++) {
    var expected = urltests[i];

    var method = cs.newMethod('DocumentUrlTest' + (i + 1))
                   .addAttribute('Test')
                   .addLine('var document = DocumentBuilder.Html("<base id=base>");')
                   .addLine('var element = document.GetElementById("base") as HTMLBaseElement;')
                   .addLine('Assert.IsNotNull(element);')
                   .addLine('element.Href = @"' + (expected.base || 'about:blank') + '";')
                   .addLine('var anchor = document.CreateElement<IHtmlAnchorElement>();')
                   .addLine('anchor.SetAttribute("href", @"' + expected.input + '");');

    if (expected.protocol === ':')
      continue;

    method.addLine('Assert.AreEqual("' + expected.protocol + '", anchor.Protocol);');
    method.addLine('Assert.AreEqual("' + expected.host + '", anchor.HostName);');
    method.addLine('Assert.AreEqual("' + expected.port + '", anchor.Port);');
    method.addLine('Assert.AreEqual("' + expected.path + '", anchor.PathName);');
    method.addLine('Assert.AreEqual("' + expected.search + '", anchor.Search);');
    method.addLine('Assert.AreEqual("' + expected.hash + '", anchor.Hash);');
    method.addLine('Assert.AreEqual("' + expected.href + '", anchor.Href);');

    testClass.addMethod(method);
  }

  var callback = function(err) {
    if (err)
      console.log(err);
    else
      console.log("The URL tests have been successfully generated in " + target + "!");
  };

  cs.newFile(target + '.cs')
    .setNamespace('UnitTests.Library')
    .addNamespaces(['AngleSharp', 'AngleSharp.DOM.Html', 'NUnit.Framework', 'System'])
    .addClass(testClass)
    .save(callback);
}

generate('UrlValidation', 'data.txt');