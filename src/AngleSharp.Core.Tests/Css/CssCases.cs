namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    using NUnit.Framework;
    using System;
    using System.Linq;

	[TestFixture]
	public class CssCasesTests : CssConstructionFunctions
	{
        static ICssStyleSheet ParseSheet(String text)
        {
            return ParseStyleSheet(text, new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true,
                IsToleratingInvalidConstraints = true,
                IsToleratingInvalidValues = true,
                IsToleratingInvalidSelectors = true
            });
        }

		[Test]
        public void StyleSheetAtNamespace()
		{
			var sheet = ParseSheet(@"@namespace svg ""http://www.w3.org/2000/svg"";");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetCharsetLinebreak()
		{
			var sheet = ParseSheet(@"@charset
    ""UTF-8""
    ;");
			Assert.AreEqual(1, sheet.Rules.Length);

			foreach (var rule in sheet.Rules)
				Assert.AreEqual(@"UTF-8", ((ICssCharsetRule)rule).CharacterSet);
		}

		[Test]
        public void StyleSheetCharset()
		{
			var sheet = ParseSheet(@"@charset ""UTF-8"";       /* Set the encoding of the style sheet to Unicode UTF-8 */
@charset 'iso-8859-15'; /* Set the encoding of the style sheet to Latin-9 (Western European languages, with euro sign) */
");
			Assert.AreEqual(2, sheet.Rules.Length);
            Assert.AreEqual(@"UTF-8", ((ICssCharsetRule)sheet.Rules[0]).CharacterSet);
            Assert.AreEqual(@"iso-8859-15", ((ICssCharsetRule)sheet.Rules[1]).CharacterSet);
		}

		[Test]
        public void StyleSheetColonSpace()
		{
			var sheet = ParseSheet(@"a {
    margin  : auto;
    padding : 0;
}");
			Assert.AreEqual(1, sheet.Rules.Length);

			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"a", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"auto", ((ICssStyleRule)rule).Style["margin"]);
				Assert.AreEqual(@"0", ((ICssStyleRule)rule).Style["padding"]);
			}
		}

		[Test]
        public void StyleSheetCommaAttribute()
		{
			var sheet = ParseSheet(@".foo[bar=""baz,quz""] {
  foobar: 123;
}

.bar,
#bar[baz=""qux,foo""],
#qux {
  foobar: 456;
}

.baz[qux="",foo""],
.baz[qux=""foo,""],
.baz[qux=""foo,bar,baz""],
.baz[qux="",foo,bar,baz,""],
.baz[qux="" , foo , bar , baz , ""] {
  foobar: 789;
}

.qux[foo='bar,baz'],
.qux[bar=""baz,foo""],
#qux[foo=""foobar""],
#qux[foo=',bar,baz, '] {
  foobar: 012;
}

#foo[foo=""""],
#foo[bar="" ""],
#foo[bar="",""],
#foo[bar="", ""],
#foo[bar="" ,""],
#foo[bar="" , ""],
#foo[baz=''],
#foo[qux=' '],
#foo[qux=','],
#foo[qux=', '],
#foo[qux=' ,'],
#foo[qux=' , '] {
  foobar: 345;
}");
			Assert.AreEqual(5, sheet.Rules.Length);

            Assert.AreEqual(@".foo[bar=""baz,quz""]", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
            Assert.AreEqual(@"123", ((ICssStyleRule)sheet.Rules[0]).Style["foobar"]);

            Assert.AreEqual(@".bar,#bar[baz=""qux,foo""],#qux", ((ICssStyleRule)sheet.Rules[1]).SelectorText);
            Assert.AreEqual(@"456", ((ICssStyleRule)sheet.Rules[1]).Style["foobar"]);

            Assert.AreEqual(@".baz[qux="",foo""],.baz[qux=""foo,""],.baz[qux=""foo,bar,baz""],.baz[qux="",foo,bar,baz,""],.baz[qux="" , foo , bar , baz , ""]", ((ICssStyleRule)sheet.Rules[2]).SelectorText);
            Assert.AreEqual(@"789", ((ICssStyleRule)sheet.Rules[2]).Style["foobar"]);

            Assert.AreEqual(@".qux[foo=""bar,baz""],.qux[bar=""baz,foo""],#qux[foo=""foobar""],#qux[foo="",bar,baz, ""]", ((ICssStyleRule)sheet.Rules[3]).SelectorText);
            Assert.AreEqual(@"012", ((ICssStyleRule)sheet.Rules[3]).Style["foobar"]);

            Assert.AreEqual(@"#foo[foo=""""],#foo[bar="" ""],#foo[bar="",""],#foo[bar="", ""],#foo[bar="" ,""],#foo[bar="" , ""],#foo[baz=""""],#foo[qux="" ""],#foo[qux="",""],#foo[qux="", ""],#foo[qux="" ,""],#foo[qux="" , ""]", ((ICssStyleRule)sheet.Rules[4]).SelectorText);
            Assert.AreEqual(@"345", ((ICssStyleRule)sheet.Rules[4]).Style["foobar"]);
		}

		[Test]
        public void StyleSheetCommaSelectorFunction()
		{
			var sheet = ParseSheet(@".foo:matches(.bar,.baz),
.foo:matches(.bar, .baz),
.foo:matches(.bar , .baz),
.foo:matches(.bar ,.baz) {
  prop: value;
}

.foo:matches(.bar,.baz,.foobar),
.foo:matches(.bar, .baz,),
.foo:matches(,.bar , .baz) {
  anotherprop: anothervalue;
}");
            Assert.AreEqual(2, sheet.Rules.Length);

            Assert.AreEqual(@".foo:matches(.bar,.baz),.foo:matches(.bar,.baz),.foo:matches(.bar,.baz),.foo:matches(.bar,.baz)", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
            Assert.AreEqual(@"value", ((ICssStyleRule)sheet.Rules[0]).Style["prop"]);

            Assert.AreEqual(@".foo:matches(.bar,.baz,.foobar),
.foo:matches(.bar, .baz,),
.foo:matches(,.bar , .baz) ", ((ICssStyleRule)sheet.Rules[1]).SelectorText);
            Assert.AreEqual(@"anothervalue", ((ICssStyleRule)sheet.Rules[1]).Style["anotherprop"]);
		}

		[Test]
        public void StyleSheetCommentIn()
		{
			var sheet = ParseSheet(@"a {
    color/**/: 12px;
    padding/*4815162342*/: 1px /**/ 2px /*13*/ 3px;
    border/*\**/: solid; border-top/*\**/: none\9;
}");
			Assert.AreEqual(1, sheet.Rules.Length);
            var rule = sheet.Rules[0];

            Assert.AreEqual(@"a", ((ICssStyleRule)rule).SelectorText);
            Assert.AreEqual(@"12px", ((ICssStyleRule)rule).Style["color"]);
            Assert.AreEqual(@"1px 2px 3px", ((ICssStyleRule)rule).Style["padding"]);
            Assert.AreEqual(@"solid", ((ICssStyleRule)rule).Style["border"]);
            Assert.AreEqual("none\t", ((ICssStyleRule)rule).Style["border-top"]);
		}

		[Test]
        public void StyleSheetCommentUrl()
		{
			var sheet = ParseSheet(@"/* http://foo.com/bar/baz.html */
/**/

foo { /*/*/
  /* something */
  bar: baz; /* http://foo.com/bar/baz.html */
}");
			Assert.AreEqual(1, sheet.Rules.Length);

            Assert.AreEqual(@"foo", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
            Assert.AreEqual(@"baz", ((ICssStyleRule)sheet.Rules[0]).Style["bar"]);
		}

		[Test]
        public void StyleSheetComment()
		{
			var sheet = ParseSheet(@"/* 1 */

head, /* footer, */body/*, nav */ { /* 2 */
  /* 3 */
  /**/foo: 'bar';
  /* 4 */
} /* 5 */

/* 6 */");
			Assert.AreEqual(1, sheet.Rules.Length);

            Assert.AreEqual(@"head,body", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
            Assert.AreEqual(@"""bar""", ((ICssStyleRule)sheet.Rules[0]).Style["foo"]);
		}

		[Test]
        public void StyleSheetCustomMediaLinebreak()
		{
			var sheet = ParseSheet(@"@custom-media
    --test
    (min-width: 200px)
;");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetCustomMedia()
		{
			var sheet = ParseSheet(@"@custom-media --narrow-window (max-width: 30em);
@custom-media --wide-window screen and (min-width: 40em);
");
			Assert.AreEqual(2, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetDocumentLinebreak()
		{
			var sheet = ParseSheet(@"@document
    url-prefix()
    {

        .test {
            color: blue;
        }

    }");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetDocument()
		{
			var sheet = ParseSheet(@"@-moz-document url-prefix() {
  /* ui above */
  .ui-select .ui-btn select {
    /* ui inside */
    opacity:.0001
  }

  .icon-spin {
    height: .9em;
  }
}");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
		public void StyleSheetEmpty()
		{
			var sheet = ParseSheet(@"");
			Assert.AreEqual(0, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetEscapes()
		{
			var sheet = ParseSheet(@"/* tests compressed for easy testing */
/* http://mathiasbynens.be/notes/css-escapes */
/* will match elements with class="":`("" */
.\3A \`\({}
/* will match elements with class=""1a2b3c"" */
.\31 a2b3c{}
/* will match the element with id=""#fake-id"" */
#\#fake-id{}
/* will match the element with id=""---"" */
#\---{}
/* will match the element with id=""-a-b-c-"" */
#-a-b-c-{}
/* will match the element with id=""©"" */
#©{}
/* More tests from http://mathiasbynens.be/demo/html5-id */
html{font:1.2em/1.6 Arial;}
code{font-family:Consolas;}
li code{background:rgba(255, 255, 255, .5);padding:.3em;}
li{background:orange;}
#♥{background:lime;}
#©{background:lime;}
#“‘’”{background:lime;}
#☺☃{background:lime;}
#⌘⌥{background:lime;}
#𝄞♪♩♫♬{background:lime;}
#\?{background:lime;}
#\@{background:lime;}
#\.{background:lime;}
#\3A \){background:lime;}
#\3A \`\({background:lime;}
#\31 23{background:lime;}
#\31 a2b3c{background:lime;}
#\<p\>{background:lime;}
#\<\>\<\<\<\>\>\<\>{background:lime;}
#\+\+\+\+\+\+\+\+\+\+\[\>\+\+\+\+\+\+\+\>\+\+\+\+\+\+\+\+\+\+\>\+\+\+\>\+\<\<\<\<\-\]\>\+\+\.\>\+\.\+\+\+\+\+\+\+\.\.\+\+\+\.\>\+\+\.\<\<\+\+\+\+\+\+\+\+\+\+\+\+\+\+\+\.\>\.\+\+\+\.\-\-\-\-\-\-\.\-\-\-\-\-\-\-\-\.\>\+\.\>\.{background:lime;}
#\#{background:lime;}
#\#\#{background:lime;}
#\#\.\#\.\#{background:lime;}
#\_{background:lime;}
#\.fake\-class{background:lime;}
#foo\.bar{background:lime;}
#\3A hover{background:lime;}
#\3A hover\3A focus\3A active{background:lime;}
#\[attr\=value\]{background:lime;}
#f\/o\/o{background:lime;}
#f\\o\\o{background:lime;}
#f\*o\*o{background:lime;}
#f\!o\!o{background:lime;}
#f\'o\'o{background:lime;}
#f\~o\~o{background:lime;}
#f\+o\+o{background:lime;}

/* css-parse does not yet pass this test */
/*#\{\}{background:lime;}*/");
			Assert.AreEqual(42, sheet.Rules.Length);

            Assert.AreEqual(@".:`(", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
            Assert.AreEqual(@".1a2b3c", ((ICssStyleRule)sheet.Rules[1]).SelectorText);
            Assert.AreEqual(@"##fake-id", ((ICssStyleRule)sheet.Rules[2]).SelectorText);
            Assert.AreEqual(@"#---", ((ICssStyleRule)sheet.Rules[3]).SelectorText);
            Assert.AreEqual(@"#-a-b-c-", ((ICssStyleRule)sheet.Rules[4]).SelectorText);
            Assert.AreEqual(@"#©", ((ICssStyleRule)sheet.Rules[5]).SelectorText);
            Assert.AreEqual(@"html", ((ICssStyleRule)sheet.Rules[6]).SelectorText);
            Assert.AreEqual(@"1.2em/1.6 Arial", ((ICssStyleRule)sheet.Rules[6]).Style["font"]);
            Assert.AreEqual(@"code", ((ICssStyleRule)sheet.Rules[7]).SelectorText);
            Assert.AreEqual(@"Consolas", ((ICssStyleRule)sheet.Rules[7]).Style["font-family"]);
            Assert.AreEqual(@"li code", ((ICssStyleRule)sheet.Rules[8]).SelectorText);
            Assert.AreEqual(@"rgba(255, 255, 255, .5)", ((ICssStyleRule)sheet.Rules[8]).Style["background"]);
            Assert.AreEqual(@".3em", ((ICssStyleRule)sheet.Rules[8]).Style["padding"]);
            Assert.AreEqual(@"li", ((ICssStyleRule)sheet.Rules[9]).SelectorText);
            Assert.AreEqual(@"orange", ((ICssStyleRule)sheet.Rules[9]).Style["background"]);
            Assert.AreEqual(@"#♥", ((ICssStyleRule)sheet.Rules[10]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[10]).Style["background"]);
            Assert.AreEqual(@"#©", ((ICssStyleRule)sheet.Rules[11]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[11]).Style["background"]);
            Assert.AreEqual(@"#“‘’”", ((ICssStyleRule)sheet.Rules[12]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[12]).Style["background"]);
            Assert.AreEqual(@"#☺☃", ((ICssStyleRule)sheet.Rules[13]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[13]).Style["background"]);
            Assert.AreEqual(@"#⌘⌥", ((ICssStyleRule)sheet.Rules[14]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[14]).Style["background"]);
            Assert.AreEqual(@"#𝄞♪♩♫♬", ((ICssStyleRule)sheet.Rules[15]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[15]).Style["background"]);
            Assert.AreEqual(@"#?", ((ICssStyleRule)sheet.Rules[16]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[16]).Style["background"]);
            Assert.AreEqual(@"#@", ((ICssStyleRule)sheet.Rules[17]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[17]).Style["background"]);
            Assert.AreEqual(@"#.", ((ICssStyleRule)sheet.Rules[18]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[18]).Style["background"]);
            Assert.AreEqual(@"#:)", ((ICssStyleRule)sheet.Rules[19]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[19]).Style["background"]);
            Assert.AreEqual(@"#:`(", ((ICssStyleRule)sheet.Rules[20]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[20]).Style["background"]);
            Assert.AreEqual(@"#123", ((ICssStyleRule)sheet.Rules[21]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[21]).Style["background"]);
            Assert.AreEqual(@"#1a2b3c", ((ICssStyleRule)sheet.Rules[22]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[22]).Style["background"]);
            Assert.AreEqual(@"#<p>", ((ICssStyleRule)sheet.Rules[23]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[23]).Style["background"]);
            Assert.AreEqual(@"#<><<<>><>", ((ICssStyleRule)sheet.Rules[24]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[24]).Style["background"]);
            Assert.AreEqual(@"#++++++++++[>+++++++>++++++++++>+++>+<<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.>.", ((ICssStyleRule)sheet.Rules[25]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[25]).Style["background"]);
            Assert.AreEqual(@"##", ((ICssStyleRule)sheet.Rules[26]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[26]).Style["background"]);
            Assert.AreEqual(@"###", ((ICssStyleRule)sheet.Rules[27]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[27]).Style["background"]);
            Assert.AreEqual(@"##.#.#", ((ICssStyleRule)sheet.Rules[28]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[28]).Style["background"]);
            Assert.AreEqual(@"#_", ((ICssStyleRule)sheet.Rules[29]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[29]).Style["background"]);
            Assert.AreEqual(@"#.fake-class", ((ICssStyleRule)sheet.Rules[30]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[30]).Style["background"]);
            Assert.AreEqual(@"#foo.bar", ((ICssStyleRule)sheet.Rules[31]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[31]).Style["background"]);
            Assert.AreEqual(@"#:hover", ((ICssStyleRule)sheet.Rules[32]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[32]).Style["background"]);
            Assert.AreEqual(@"#:hover:focus:active", ((ICssStyleRule)sheet.Rules[33]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[33]).Style["background"]);
            Assert.AreEqual(@"#[attr=value]", ((ICssStyleRule)sheet.Rules[34]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[34]).Style["background"]);
            Assert.AreEqual(@"#f/o/o", ((ICssStyleRule)sheet.Rules[35]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[35]).Style["background"]);
            Assert.AreEqual(@"#f\o\o", ((ICssStyleRule)sheet.Rules[36]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[36]).Style["background"]);
            Assert.AreEqual(@"#f*o*o", ((ICssStyleRule)sheet.Rules[37]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[37]).Style["background"]);
            Assert.AreEqual(@"#f!o!o", ((ICssStyleRule)sheet.Rules[38]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[38]).Style["background"]);
            Assert.AreEqual(@"#f'o'o", ((ICssStyleRule)sheet.Rules[39]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[39]).Style["background"]);
            Assert.AreEqual(@"#f~o~o", ((ICssStyleRule)sheet.Rules[40]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[40]).Style["background"]);
            Assert.AreEqual(@"#f+o+o", ((ICssStyleRule)sheet.Rules[41]).SelectorText);
            Assert.AreEqual(@"lime", ((ICssStyleRule)sheet.Rules[41]).Style["background"]);
		}

		[Test]
        public void StyleSheetFontFaceLinebreak()
		{
			var sheet = ParseSheet(@"@font-face

       {
  font-family: ""Bitstream Vera Serif Bold"";
  src: url(""http://developer.mozilla.org/@api/deki/files/2934/=VeraSeBd.ttf"");
}

body {
  font-family: ""Bitstream Vera Serif Bold"", serif;
}");
			Assert.AreEqual(2, sheet.Rules.Length);

            Assert.AreEqual(@"body", ((ICssStyleRule)sheet.Rules[1]).SelectorText);
            Assert.AreEqual(@"""Bitstream Vera Serif Bold"", serif", ((ICssStyleRule)sheet.Rules[1]).Style["font-family"]);
		}

		[Test]
        public void StyleSheetFontFace()
		{
			var sheet = ParseSheet(@"@font-face {
  font-family: ""Bitstream Vera Serif Bold"";
  src: url(""http://developer.mozilla.org/@api/deki/files/2934/=VeraSeBd.ttf"");
}

body {
  font-family: ""Bitstream Vera Serif Bold"", serif;
}");
			Assert.AreEqual(2, sheet.Rules.Length);

            Assert.AreEqual(@"body", ((ICssStyleRule)sheet.Rules[1]).SelectorText);
            Assert.AreEqual(@"""Bitstream Vera Serif Bold"", serif", ((ICssStyleRule)sheet.Rules[1]).Style["font-family"]);
		}

		[Test]
        public void StyleSheetHostLinebreak()
		{
			var sheet = ParseSheet(@"@host
    {
        :scope { color: white; }
    }");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetHost()
		{
			var sheet = ParseSheet(@"@host {
  :scope {
    display: block;
  }
}");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetImportLinebreak()
		{
			var sheet = ParseSheet(@"@import
    url(test.css)
    screen
    ;");
			Assert.AreEqual(1, sheet.Rules.Length);

            Assert.AreEqual(@"test.css", ((ICssImportRule)sheet.Rules[0]).Href);
		}

		[Test]
        public void StyleSheetImportMessed()
		{
			var sheet = ParseSheet(@"
   @import url(""fineprint.css"") print;
  @import url(""bluish.css"") projection, tv;
      @import 'custom.css';
  @import ""common.css"" screen, projection  ;

  @import url('landscape.css') screen and (orientation:landscape);");
			Assert.AreEqual(5, sheet.Rules.Length);

            Assert.AreEqual(@"fineprint.css", ((ICssImportRule)sheet.Rules[0]).Href);
            Assert.AreEqual(@"print", ((ICssImportRule)sheet.Rules[0]).Media.MediaText);

            Assert.AreEqual(@"bluish.css", ((ICssImportRule)sheet.Rules[1]).Href);
            Assert.AreEqual(@"projection, tv", ((ICssImportRule)sheet.Rules[1]).Media.MediaText);

            Assert.AreEqual(@"custom.css", ((ICssImportRule)sheet.Rules[2]).Href);
            Assert.AreEqual(@"", ((ICssImportRule)sheet.Rules[2]).Media.MediaText);

            Assert.AreEqual(@"common.css", ((ICssImportRule)sheet.Rules[3]).Href);
            Assert.AreEqual(@"screen, projection", ((ICssImportRule)sheet.Rules[3]).Media.MediaText);

            Assert.AreEqual(@"landscape.css", ((ICssImportRule)sheet.Rules[4]).Href);
            Assert.AreEqual(@"screen and (orientation: landscape)", ((ICssImportRule)sheet.Rules[4]).Media.MediaText);
		}

		[Test]
        public void StyleSheetImport()
		{
			var sheet = ParseSheet(@"@import url(""fineprint.css"") print;
@import url(""bluish.css"") projection, tv;
@import 'custom.css';
@import ""common.css"" screen, projection;
@import url('landscape.css') screen and (orientation:landscape);");
			Assert.AreEqual(5, sheet.Rules.Length);

            Assert.AreEqual(@"fineprint.css", ((ICssImportRule)sheet.Rules[0]).Href);
            Assert.AreEqual(@"print", ((ICssImportRule)sheet.Rules[0]).Media.MediaText);

            Assert.AreEqual(@"bluish.css", ((ICssImportRule)sheet.Rules[1]).Href);
            Assert.AreEqual(@"projection, tv", ((ICssImportRule)sheet.Rules[1]).Media.MediaText);

            Assert.AreEqual(@"custom.css", ((ICssImportRule)sheet.Rules[2]).Href);
            Assert.AreEqual(@"", ((ICssImportRule)sheet.Rules[2]).Media.MediaText);

            Assert.AreEqual(@"common.css", ((ICssImportRule)sheet.Rules[3]).Href);
            Assert.AreEqual(@"screen, projection", ((ICssImportRule)sheet.Rules[3]).Media.MediaText);

            Assert.AreEqual(@"landscape.css", ((ICssImportRule)sheet.Rules[4]).Href);
            Assert.AreEqual(@"screen and (orientation: landscape)", ((ICssImportRule)sheet.Rules[4]).Media.MediaText);
		}

		[Test]
        public void StyleSheetKeyframesAdvanced()
		{
			var sheet = ParseSheet(@"@keyframes advanced {
  top {
    opacity[sqrt]: 0;
  }

  100 {
    opacity: 0.5;
  }

  bottom {
    opacity: 1;
  }
}");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetKeyframesComplex()
		{
			var sheet = ParseSheet(@"@keyframes foo {
  0% { top: 0; left: 0 }
  30.50% { top: 50px }
  .68% ,
  72%
      , 85% { left: 50px }
  100% { top: 100px; left: 100% }
}");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetKeyframesLinebreak()
		{
			var sheet = ParseSheet(@"@keyframes
    test
    {
        from { opacity: 1; }
        to { opacity: 0; }
    }
");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetKeyframesMessed()
		{
			var sheet = ParseSheet(@"@keyframes fade {from
  {opacity: 0;
     }
to
  {
     opacity: 1;}}");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetKeyframesVendor()
		{
			var sheet = ParseSheet(@"@-webkit-keyframes fade {
  from { opacity: 0 }
  to { opacity: 1 }
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetKeyframes()
		{
			var sheet = ParseSheet(@"@keyframes fade {
  /* from above */
  from {
    /* from inside */
    opacity: 0;
  }

  /* to above */
  to {
    /* to inside */
    opacity: 1;
  }
}");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetMediaLinebreak()
		{
			var sheet = ParseSheet(@"@media

(
    min-width: 300px
)
{
    .test { width: 100px; }
}");
			Assert.AreEqual(1, sheet.Rules.Length);
            var rule = (ICssMediaRule)sheet.Rules[0];

            Assert.AreEqual(@"(min-width: 300px)", rule.Media.MediaText);
            Assert.AreEqual(1, rule.Rules.Length);

            var subrule = rule.Rules[0];
            Assert.AreEqual(@".test", ((ICssStyleRule)subrule).SelectorText);
            Assert.AreEqual(@"100px", ((ICssStyleRule)subrule).Style["width"]);
		}

		[Test]
        public void StyleSheetMediaMessed()
		{
			var sheet = ParseSheet(@"@media screen, projection{ html

  {
background: #fffef0;
    color:#300;
  }
  body

{
    max-width: 35em;
    margin: 0 auto;


}
  }

@media print
{
              html {
              background: #fff;
              color: #000;
              }
              body {
              padding: 1in;
              border: 0.5pt solid #666;
              }
}");
			Assert.AreEqual(2, sheet.Rules.Length);

            {
                var rule = sheet.Rules[0];
                Assert.AreEqual(@"screen, projection", ((ICssMediaRule)rule).Media.MediaText);
                Assert.AreEqual(2, ((ICssMediaRule)rule).Rules.Length);

                {
                    var subrule = ((ICssMediaRule)rule).Rules[0];
                    Assert.AreEqual(@"html", ((ICssStyleRule)subrule).SelectorText);
                    Assert.AreEqual(@"#fffef0", ((ICssStyleRule)subrule).Style["background"]);
                    Assert.AreEqual(@"#300", ((ICssStyleRule)subrule).Style["color"]);
                }

                {
                    var subrule = ((ICssMediaRule)rule).Rules[1];
                    Assert.AreEqual(@"body", ((ICssStyleRule)subrule).SelectorText);
                    Assert.AreEqual(@"35em", ((ICssStyleRule)subrule).Style["max-width"]);
                    Assert.AreEqual(@"0 auto", ((ICssStyleRule)subrule).Style["margin"]);
                }
            }

            {
                var rule = sheet.Rules[1];
                Assert.AreEqual(@"print", ((ICssMediaRule)rule).Media.MediaText);
                Assert.AreEqual(2, ((ICssMediaRule)rule).Rules.Length);

                {
                    var subrule = ((ICssMediaRule)rule).Rules[0];
                    Assert.AreEqual(@"html", ((ICssStyleRule)subrule).SelectorText);
                    Assert.AreEqual(@"#fff", ((ICssStyleRule)subrule).Style["background"]);
                    Assert.AreEqual(@"#000", ((ICssStyleRule)subrule).Style["color"]);
                }

                {
                    var subrule = ((ICssMediaRule)rule).Rules[1];
                    Assert.AreEqual(@"body", ((ICssStyleRule)subrule).SelectorText);
                    Assert.AreEqual(@"1in", ((ICssStyleRule)subrule).Style["padding"]);
                    Assert.AreEqual(@"0.5pt solid #666", ((ICssStyleRule)subrule).Style["border"]);
                }
            }
		}

		[Test]
        public void StyleSheetMedia()
		{
			var sheet = ParseSheet(@"@media screen, projection {
  /* html above */
  html {
    /* html inside */
    background: #fffef0;
    color: #300;
  }

  /* body above */
  body {
    /* body inside */
    max-width: 35em;
    margin: 0 auto;
  }
}

@media print {
  html {
    background: #fff;
    color: #000;
  }
  body {
    padding: 1in;
    border: 0.5pt solid #666;
  }
}");
			Assert.AreEqual(2, sheet.Rules.Length);

            Assert.AreEqual(@"screen, projection", ((ICssMediaRule)sheet.Rules[0]).Media.MediaText);
            Assert.AreEqual(2, ((ICssMediaRule)sheet.Rules[0]).Rules.Length);

            Assert.AreEqual(@"html", ((ICssStyleRule)((ICssMediaRule)sheet.Rules[0]).Rules[0]).SelectorText);
            Assert.AreEqual(@"#fffef0", ((ICssStyleRule)((ICssMediaRule)sheet.Rules[0]).Rules[0]).Style["background"]);
            Assert.AreEqual(@"#300", ((ICssStyleRule)((ICssMediaRule)sheet.Rules[0]).Rules[0]).Style["color"]);

            Assert.AreEqual(@"body", ((ICssStyleRule)((ICssMediaRule)sheet.Rules[0]).Rules[1]).SelectorText);
            Assert.AreEqual(@"35em", ((ICssStyleRule)((ICssMediaRule)sheet.Rules[0]).Rules[1]).Style["max-width"]);
            Assert.AreEqual(@"0 auto", ((ICssStyleRule)((ICssMediaRule)sheet.Rules[0]).Rules[1]).Style["margin"]);

            Assert.AreEqual(@"print", ((ICssMediaRule)sheet.Rules[1]).Media.MediaText);
			Assert.AreEqual(2, ((ICssMediaRule)sheet.Rules[1]).Rules.Length);

            Assert.AreEqual(@"html", ((ICssStyleRule)((ICssMediaRule)sheet.Rules[1]).Rules[0]).SelectorText);
            Assert.AreEqual(@"#fff", ((ICssStyleRule)((ICssMediaRule)sheet.Rules[1]).Rules[0]).Style["background"]);
            Assert.AreEqual(@"#000", ((ICssStyleRule)((ICssMediaRule)sheet.Rules[1]).Rules[0]).Style["color"]);

            Assert.AreEqual(@"body", ((ICssStyleRule)((ICssMediaRule)sheet.Rules[1]).Rules[1]).SelectorText);
            Assert.AreEqual(@"1in", ((ICssStyleRule)((ICssMediaRule)sheet.Rules[1]).Rules[1]).Style["padding"]);
            Assert.AreEqual(@"0.5pt solid #666", ((ICssStyleRule)((ICssMediaRule)sheet.Rules[1]).Rules[1]).Style["border"]);
		}

		[Test]
        public void StyleSheetMessedUp()
		{
			var sheet = ParseSheet(@"body { foo
  :
  'bar' }

   body{foo:bar;bar:baz}
   body
   {
     foo
     :
     bar
     ;
     bar
     :
     baz
     }
");
			Assert.AreEqual(3, sheet.Rules.Length);

            Assert.AreEqual(@"body", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
            Assert.AreEqual(@"""bar""", ((ICssStyleRule)sheet.Rules[0]).Style["foo"]);

            Assert.AreEqual(@"body", ((ICssStyleRule)sheet.Rules[1]).SelectorText);
            Assert.AreEqual(@"bar", ((ICssStyleRule)sheet.Rules[1]).Style["foo"]);
            Assert.AreEqual(@"baz", ((ICssStyleRule)sheet.Rules[1]).Style["bar"]);

            Assert.AreEqual(@"body", ((ICssStyleRule)sheet.Rules[2]).SelectorText);
            Assert.AreEqual(@"bar", ((ICssStyleRule)sheet.Rules[2]).Style["foo"]);
            Assert.AreEqual(@"baz", ((ICssStyleRule)sheet.Rules[2]).Style["bar"]);
		}

		[Test]
        public void StyleSheetNamespaceLinebreak()
		{
			var sheet = ParseSheet(@"@namespace
    ""http://www.w3.org/1999/xhtml""
    ;");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetNamespace()
		{
			var sheet = ParseSheet(@"@namespace ""http://www.w3.org/1999/xhtml"";
@namespace svg ""http://www.w3.org/2000/svg"";");
			Assert.AreEqual(2, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetNoSemi()
		{
			var sheet = ParseSheet(@"
tobi loki jane {
  are: 'all';
  the-species: called ""ferrets""
}");
			Assert.AreEqual(1, sheet.Rules.Length);

			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"tobi loki jane", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"""all""", ((ICssStyleRule)rule).Style["are"]);
				Assert.AreEqual(@"called ""ferrets""", ((ICssStyleRule)rule).Style["the-species"]);
			}
		}

		[Test]
        public void StyleSheetPageLinebreak()
		{
			var sheet = ParseSheet(@"@page
    toc
    {
        color: black;
    }");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetPagedMedia()
		{
			var sheet = ParseSheet(@"/* toc above */
@page toc, index:blank {
  /* toc inside */
  color: green;
}

@page {
  font-size: 16pt;
  color: #f00;
}

@page :left {
  margin-left: 5cm;
}");
			Assert.AreEqual(3, sheet.Rules.Length);

            var page1 = sheet.Rules[0] as ICssPageRule;
            var page2 = sheet.Rules[1] as ICssPageRule;
            var page3 = sheet.Rules[2] as ICssPageRule;

            Assert.AreEqual(1, page1.Style.Length);
            Assert.AreEqual("green", page1.Style["color"]);

            Assert.AreEqual(2, page2.Style.Length);
            Assert.AreEqual("16pt", page2.Style["font-size"]);
            Assert.AreEqual("#f00", page2.Style["color"]);

            Assert.AreEqual(1, page3.Style.Length);
            Assert.AreEqual("5cm", page3.Style["margin-left"]);
		}

		[Test]
        public void StyleSheetProps()
		{
			var sheet = ParseSheet(@"
tobi loki jane {
  are: 'all';
  the-species: called ""ferrets"";
  *even: 'ie crap';
}");
			Assert.AreEqual(1, sheet.Rules.Length);

            Assert.AreEqual(@"tobi loki jane", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
            Assert.AreEqual(@"""all""", ((ICssStyleRule)sheet.Rules[0]).Style["are"]);
            Assert.AreEqual(@"called ""ferrets""", ((ICssStyleRule)sheet.Rules[0]).Style["the-species"]);
            Assert.AreEqual(@"""ie crap""", ((ICssStyleRule)sheet.Rules[0]).Style["*even"]);
		}

		[Test]
        public void StyleSheetQuoteEscape()
		{
			var sheet = ParseSheet(@"p[qwe=""a\"",b""] { color: red }
");
			Assert.AreEqual(1, sheet.Rules.Length);

            Assert.AreEqual(@"p[qwe=""a\"",b""]", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
            Assert.AreEqual(@"red", ((ICssStyleRule)sheet.Rules[0]).Style["color"]);
		}

		[Test]
        public void StyleSheetQuoted()
		{
			var sheet = ParseSheet(@"body {
  background: url('some;stuff;here') 50% 50% no-repeat;
}");
			Assert.AreEqual(1, sheet.Rules.Length);

            Assert.AreEqual(@"body", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
            Assert.AreEqual(@"url(""some;stuff;here"") 50% 50% no-repeat", ((ICssStyleRule)sheet.Rules[0]).Style["background"]);
		}

		[Test]
        public void StyleSheetRule()
		{
			var sheet = ParseSheet(@"foo {
  bar: 'baz';
}");
			Assert.AreEqual(1, sheet.Rules.Length);

            Assert.AreEqual(@"foo", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
            Assert.AreEqual(@"""baz""", ((ICssStyleRule)sheet.Rules[0]).Style["bar"]);
		}

		[Test]
        public void StyleSheetRules()
		{
			var sheet = ParseSheet(@"tobi {
  name: 'tobi';
  age: 2;
}

loki {
  name: 'loki';
  age: 1;
}");
			Assert.AreEqual(2, sheet.Rules.Length);

            Assert.AreEqual(@"tobi", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
            Assert.AreEqual(@"""tobi""", ((ICssStyleRule)sheet.Rules[0]).Style["name"]);
            Assert.AreEqual(@"2", ((ICssStyleRule)sheet.Rules[0]).Style["age"]);

            Assert.AreEqual(@"loki", ((ICssStyleRule)sheet.Rules[1]).SelectorText);
            Assert.AreEqual(@"""loki""", ((ICssStyleRule)sheet.Rules[1]).Style["name"]);
            Assert.AreEqual(@"1", ((ICssStyleRule)sheet.Rules[1]).Style["age"]);
		}

		[Test]
        public void StyleSheetSelectors()
		{
			var sheet = ParseSheet(@"foo,
bar,
baz {
  color: 'black';
}");
			Assert.AreEqual(1, sheet.Rules.Length);

            Assert.AreEqual(@"foo,bar,baz", ((ICssStyleRule)sheet.Rules[0]).SelectorText);
            Assert.AreEqual(@"""black""", ((ICssStyleRule)sheet.Rules[0]).Style["color"]);
		}

		[Test]
        public void StyleSheetSupportsLinebreak()
		{
			var sheet = ParseSheet(@"@supports
    (display: flex)
    {
        .test { display: flex; }
    }");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetSupports()
		{
			var sheet = ParseSheet(@"@supports (display: flex) or (display: box) {
  /* flex above */
  .flex {
    /* flex inside */
    display: box;
    display: flex;
  }

  div {
    something: else;
  }
}");
			Assert.AreEqual(1, sheet.Rules.Length);
		}

		[Test]
        public void StyleSheetWtf()
		{
			var sheet = ParseSheet(@".wtf {
  *overflow-x: hidden;
  //max-height: 110px;
  #height: 18px;
}");
			Assert.AreEqual(1, sheet.Rules.Length);

			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@".wtf", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"hidden", ((ICssStyleRule)rule).Style["*overflow-x"]);
				Assert.AreEqual(@"110px", ((ICssStyleRule)rule).Style["//max-height"]);
				Assert.AreEqual(@"18px", ((ICssStyleRule)rule).Style["#height"]);
			}
		}

        [Test]
        public void StyleSheetUnicodeEscapeLiteral()
        {
            var sheet = ParseSheet(@"h1 { background-color: \000062
lack; }");
            Assert.AreEqual(@"black", ((ICssStyleRule)sheet.Rules[0]).Style["background-color"]);
        }

        [Test]
        public void StyleSheetUnicodeEscapeVarious()
        {
            var sheet = ParseSheet("h1 { background-color: \\000062\r\nlack; color: \\000062\tlack; border-color: \\000062\nlack; outline-color: \\000062 lack }");
            Assert.AreEqual(@"black", ((ICssStyleRule)sheet.Rules[0]).Style["background-color"]);
            Assert.AreEqual(@"black", ((ICssStyleRule)sheet.Rules[0]).Style["color"]);
            Assert.AreEqual(@"black", ((ICssStyleRule)sheet.Rules[0]).Style["border-color"]);
            Assert.AreEqual(@"black", ((ICssStyleRule)sheet.Rules[0]).Style["outline-color"]);
        }

        [Test]
        public void StyleSheetUnicodeEscapeLeadingSingleCarriageReturn()
        {
            var sheet = ParseSheet("h1 { background-image: \\000075\r\r\nrl('foo') }");
            Assert.AreEqual("u\nrl(\"foo\")", ((ICssStyleRule)sheet.Rules[0]).Style["background-image"]);
        }

        [Test]
        public void StyleSheetWithInitialCommentShouldWorkWithTriviaActive()
        {
            var options = new CssParserOptions
            {
                IsStoringTrivia = true
            };
            var parser = new CssParser(options);
            var document = parser.ParseStylesheet(@"/* Comment at the start */ body { font-size: 10pt; }");
            var comment = document.Children.First();

            Assert.IsInstanceOf<ICssComment>(comment);
            Assert.AreEqual(" Comment at the start ", ((ICssComment)comment).Data);
        }
    }
}