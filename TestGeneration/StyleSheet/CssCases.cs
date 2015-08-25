using AngleSharp.Dom.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Css
{
	[TestFixture]
	public class CssCasesTests : CssConstructionFunctions
	{
		[Test]
		public void AtNamespace()
		{
			var sheet = ParseStyleSheet(@"@namespace svg ""http://www.w3.org/2000/svg"";
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void CharsetLinebreak()
		{
			var sheet = ParseStyleSheet(@"@charset
    ""UTF-8""
    ;
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"""UTF-8""", ((ICssCharsetRule)rule).CharacterSet);
			}
		}
	
		[Test]
		public void Charset()
		{
			var sheet = ParseStyleSheet(@"@charset ""UTF-8"";       /* Set the encoding of the style sheet to Unicode UTF-8 */
@charset 'iso-8859-15'; /* Set the encoding of the style sheet to Latin-9 (Western European languages, with euro sign) */
");
			Assert.AreEqual(4, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"""UTF-8""", ((ICssCharsetRule)rule).CharacterSet);
				Assert.AreEqual(@"'iso-8859-15'", ((ICssCharsetRule)rule).CharacterSet);
			}
		}
	
		[Test]
		public void ColonSpace()
		{
			var sheet = ParseStyleSheet(@"a {
    margin  : auto;
    padding : 0;
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"a", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"auto", ((ICssStyleRule)rule).Style["margin"]);
				Assert.AreEqual(@"0", ((ICssStyleRule)rule).Style["padding"]);
			}
		}
	
		[Test]
		public void CommaAttribute()
		{
			var sheet = ParseStyleSheet(@".foo[bar=""baz,quz""] {
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
}
");
			Assert.AreEqual(5, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@".foo[bar=""baz,quz""]", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"123", ((ICssStyleRule)rule).Style["foobar"]);
				Assert.AreEqual(@".bar, #bar[baz=""qux,foo""], #qux", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"456", ((ICssStyleRule)rule).Style["foobar"]);
				Assert.AreEqual(@".baz[qux="",foo""], .baz[qux=""foo,""], .baz[qux=""foo,bar,baz""], .baz[qux="",foo,bar,baz,""], .baz[qux="" , foo , bar , baz , ""]", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"789", ((ICssStyleRule)rule).Style["foobar"]);
				Assert.AreEqual(@".qux[foo='bar,baz'], .qux[bar=""baz,foo""], #qux[foo=""foobar""], #qux[foo=',bar,baz, ']", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"012", ((ICssStyleRule)rule).Style["foobar"]);
				Assert.AreEqual(@"#foo[foo=""""], #foo[bar="" ""], #foo[bar="",""], #foo[bar="", ""], #foo[bar="" ,""], #foo[bar="" , ""], #foo[baz=''], #foo[qux=' '], #foo[qux=','], #foo[qux=', '], #foo[qux=' ,'], #foo[qux=' , ']", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"345", ((ICssStyleRule)rule).Style["foobar"]);
			}
		}
	
		[Test]
		public void CommaSelectorFunction()
		{
			var sheet = ParseStyleSheet(@".foo:matches(.bar,.baz),
.foo:matches(.bar, .baz),
.foo:matches(.bar , .baz),
.foo:matches(.bar ,.baz) {
  prop: value;
}

.foo:matches(.bar,.baz,.foobar),
.foo:matches(.bar, .baz,),
.foo:matches(,.bar , .baz) {
  anotherprop: anothervalue;
}
");
			Assert.AreEqual(2, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@".foo:matches(.bar,.baz), .foo:matches(.bar, .baz), .foo:matches(.bar , .baz), .foo:matches(.bar ,.baz)", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"value", ((ICssStyleRule)rule).Style["prop"]);
				Assert.AreEqual(@".foo:matches(.bar,.baz,.foobar), .foo:matches(.bar, .baz,), .foo:matches(,.bar , .baz)", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"anothervalue", ((ICssStyleRule)rule).Style["anotherprop"]);
			}
		}
	
		[Test]
		public void CommentIn()
		{
			var sheet = ParseStyleSheet(@"a {
    color/**/: 12px;
    padding/*4815162342*/: 1px /**/ 2px /*13*/ 3px;
    border/*\**/: solid; border-top/*\**/: none\9;
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"a", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"12px", ((ICssStyleRule)rule).Style["color"]);
				Assert.AreEqual(@"1px  2px  3px", ((ICssStyleRule)rule).Style["padding"]);
				Assert.AreEqual(@"solid", ((ICssStyleRule)rule).Style["border"]);
				Assert.AreEqual(@"none\9", ((ICssStyleRule)rule).Style["border-top"]);
			}
		}
	
		[Test]
		public void CommentUrl()
		{
			var sheet = ParseStyleSheet(@"/* http://foo.com/bar/baz.html */
/**/

foo { /*/*/
  /* something */
  bar: baz; /* http://foo.com/bar/baz.html */
}
");
			Assert.AreEqual(3, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"foo", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"", ((ICssStyleRule)rule).Style["undefined"]);
				Assert.AreEqual(@"", ((ICssStyleRule)rule).Style["undefined"]);
				Assert.AreEqual(@"baz", ((ICssStyleRule)rule).Style["bar"]);
				Assert.AreEqual(@"", ((ICssStyleRule)rule).Style["undefined"]);
			}
		}
	
		[Test]
		public void Comment()
		{
			var sheet = ParseStyleSheet(@"/* 1 */

head, /* footer, */body/*, nav */ { /* 2 */
  /* 3 */
  /**/foo: 'bar';
  /* 4 */
} /* 5 */

/* 6 */
");
			Assert.AreEqual(4, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"head, body", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"", ((ICssStyleRule)rule).Style["undefined"]);
				Assert.AreEqual(@"", ((ICssStyleRule)rule).Style["undefined"]);
				Assert.AreEqual(@"", ((ICssStyleRule)rule).Style["undefined"]);
				Assert.AreEqual(@"'bar'", ((ICssStyleRule)rule).Style["foo"]);
				Assert.AreEqual(@"", ((ICssStyleRule)rule).Style["undefined"]);
			}
		}
	
		[Test]
		public void CustomMediaLinebreak()
		{
			var sheet = ParseStyleSheet(@"@custom-media
    --test
    (min-width: 200px)
;
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void CustomMedia()
		{
			var sheet = ParseStyleSheet(@"@custom-media --narrow-window (max-width: 30em);
@custom-media --wide-window screen and (min-width: 40em);
");
			Assert.AreEqual(2, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void DocumentLinebreak()
		{
			var sheet = ParseStyleSheet(@"@document
    url-prefix()
    {

        .test {
            color: blue;
        }

    }
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void Document()
		{
			var sheet = ParseStyleSheet(@"@-moz-document url-prefix() {
  /* ui above */
  .ui-select .ui-btn select {
    /* ui inside */
    opacity:.0001
  }

  .icon-spin {
    height: .9em;
  }
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void Empty()
		{
			var sheet = ParseStyleSheet(@"
");
			Assert.AreEqual(0, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void Escapes()
		{
			var sheet = ParseStyleSheet(@"/* tests compressed for easy testing */
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
/*#\{\}{background:lime;}*/
");
			Assert.AreEqual(53, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@".\3A \`\(", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@".\31 a2b3c", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"#\#fake-id", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"#\---", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"#-a-b-c-", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"#©", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"html", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"1.2em/1.6 Arial", ((ICssStyleRule)rule).Style["font"]);
				Assert.AreEqual(@"code", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"Consolas", ((ICssStyleRule)rule).Style["font-family"]);
				Assert.AreEqual(@"li code", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"rgba(255, 255, 255, .5)", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@".3em", ((ICssStyleRule)rule).Style["padding"]);
				Assert.AreEqual(@"li", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"orange", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#♥", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#©", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#“‘’”", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#☺☃", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#⌘⌥", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#𝄞♪♩♫♬", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\?", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\@", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\.", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\3A \)", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\3A \`\(", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\31 23", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\31 a2b3c", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\<p\>", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\<\>\<\<\<\>\>\<\>", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\+\+\+\+\+\+\+\+\+\+\[\>\+\+\+\+\+\+\+\>\+\+\+\+\+\+\+\+\+\+\>\+\+\+\>\+\<\<\<\<\-\]\>\+\+\.\>\+\.\+\+\+\+\+\+\+\.\.\+\+\+\.\>\+\+\.\<\<\+\+\+\+\+\+\+\+\+\+\+\+\+\+\+\.\>\.\+\+\+\.\-\-\-\-\-\-\.\-\-\-\-\-\-\-\-\.\>\+\.\>\.", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\#", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\#\#", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\#\.\#\.\#", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\_", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\.fake\-class", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#foo\.bar", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\3A hover", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\3A hover\3A focus\3A active", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#\[attr\=value\]", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#f\/o\/o", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#f\\o\\o", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#f\*o\*o", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#f\!o\!o", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#f\'o\'o", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#f\~o\~o", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#f\+o\+o", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"lime", ((ICssStyleRule)rule).Style["background"]);
			}
		}
	
		[Test]
		public void FontFaceLinebreak()
		{
			var sheet = ParseStyleSheet(@"@font-face
  
       {
  font-family: ""Bitstream Vera Serif Bold"";
  src: url(""http://developer.mozilla.org/@api/deki/files/2934/=VeraSeBd.ttf"");
}

body {
  font-family: ""Bitstream Vera Serif Bold"", serif;
}
");
			Assert.AreEqual(2, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"body", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"""Bitstream Vera Serif Bold"", serif", ((ICssStyleRule)rule).Style["font-family"]);
			}
		}
	
		[Test]
		public void FontFace()
		{
			var sheet = ParseStyleSheet(@"@font-face {
  font-family: ""Bitstream Vera Serif Bold"";
  src: url(""http://developer.mozilla.org/@api/deki/files/2934/=VeraSeBd.ttf"");
}

body {
  font-family: ""Bitstream Vera Serif Bold"", serif;
}
");
			Assert.AreEqual(2, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"body", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"""Bitstream Vera Serif Bold"", serif", ((ICssStyleRule)rule).Style["font-family"]);
			}
		}
	
		[Test]
		public void HoseLinebreak()
		{
			var sheet = ParseStyleSheet(@"@host
    {
        :scope { color: white; }
    }
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void Host()
		{
			var sheet = ParseStyleSheet(@"@host {
  :scope {
    display: block;
  }
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void ImportLinebreak()
		{
			var sheet = ParseStyleSheet(@"@import
    url(test.css)
    screen
    ;
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"url(test.css)
    screen", ((ICssImportRule)rule).Href);
			}
		}
	
		[Test]
		public void ImportMessed()
		{
			var sheet = ParseStyleSheet(@"
   @import url(""fineprint.css"") print;
  @import url(""bluish.css"") projection, tv;
      @import 'custom.css';
  @import ""common.css"" screen, projection  ;

  @import url('landscape.css') screen and (orientation:landscape);
");
			Assert.AreEqual(5, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"url(""fineprint.css"") print", ((ICssImportRule)rule).Href);
				Assert.AreEqual(@"url(""bluish.css"") projection, tv", ((ICssImportRule)rule).Href);
				Assert.AreEqual(@"'custom.css'", ((ICssImportRule)rule).Href);
				Assert.AreEqual(@"""common.css"" screen, projection", ((ICssImportRule)rule).Href);
				Assert.AreEqual(@"url('landscape.css') screen and (orientation:landscape)", ((ICssImportRule)rule).Href);
			}
		}
	
		[Test]
		public void Import()
		{
			var sheet = ParseStyleSheet(@"@import url(""fineprint.css"") print;
@import url(""bluish.css"") projection, tv;
@import 'custom.css';
@import ""common.css"" screen, projection;
@import url('landscape.css') screen and (orientation:landscape);
");
			Assert.AreEqual(5, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"url(""fineprint.css"") print", ((ICssImportRule)rule).Href);
				Assert.AreEqual(@"url(""bluish.css"") projection, tv", ((ICssImportRule)rule).Href);
				Assert.AreEqual(@"'custom.css'", ((ICssImportRule)rule).Href);
				Assert.AreEqual(@"""common.css"" screen, projection", ((ICssImportRule)rule).Href);
				Assert.AreEqual(@"url('landscape.css') screen and (orientation:landscape)", ((ICssImportRule)rule).Href);
			}
		}
	
		[Test]
		public void KeyframesAdvanced()
		{
			var sheet = ParseStyleSheet(@"@keyframes advanced {
  top {
    opacity[sqrt]: 0;
  }

  100 {
    opacity: 0.5;
  }

  bottom {
    opacity: 1;
  }
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void KeyframesComplex()
		{
			var sheet = ParseStyleSheet(@"@keyframes foo {
  0% { top: 0; left: 0 }
  30.50% { top: 50px }
  .68% ,
  72%
      , 85% { left: 50px }
  100% { top: 100px; left: 100% }
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void KeyframesLinebreak()
		{
			var sheet = ParseStyleSheet(@"@keyframes
    test
    {
        from { opacity: 1; }
        to { opacity: 0; }
    }
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void KeyframesMessed()
		{
			var sheet = ParseStyleSheet(@"@keyframes fade {from
  {opacity: 0;
     }
to
  {
     opacity: 1;}}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void KeyframesVendor()
		{
			var sheet = ParseStyleSheet(@"@-webkit-keyframes fade {
  from { opacity: 0 }
  to { opacity: 1 }
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void Keyframes()
		{
			var sheet = ParseStyleSheet(@"@keyframes fade {
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
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void MediaLinebreak()
		{
			var sheet = ParseStyleSheet(@"@media

(
    min-width: 300px
)
{
    .test { width: 100px; }
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"(
    min-width: 300px
)", ((ICssMediaRule)rule).MediaText);
			Assert.AreEqual(1, rule.Rules.Length);
			
			foreach (var rule in rule.Rules)
			{
				Assert.AreEqual(@".test", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"100px", ((ICssStyleRule)rule).Style["width"]);
			}
			}
		}
	
		[Test]
		public void MediaMessed()
		{
			var sheet = ParseStyleSheet(@"@media screen, projection{ html
  
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
}
");
			Assert.AreEqual(2, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"screen, projection", ((ICssMediaRule)rule).MediaText);
			Assert.AreEqual(2, rule.Rules.Length);
			
			foreach (var rule in rule.Rules)
			{
				Assert.AreEqual(@"html", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"#fffef0", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#300", ((ICssStyleRule)rule).Style["color"]);
				Assert.AreEqual(@"body", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"35em", ((ICssStyleRule)rule).Style["max-width"]);
				Assert.AreEqual(@"0 auto", ((ICssStyleRule)rule).Style["margin"]);
			}
				Assert.AreEqual(@"print", ((ICssMediaRule)rule).MediaText);
			Assert.AreEqual(2, rule.Rules.Length);
			
			foreach (var rule in rule.Rules)
			{
				Assert.AreEqual(@"html", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"#fff", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#000", ((ICssStyleRule)rule).Style["color"]);
				Assert.AreEqual(@"body", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"1in", ((ICssStyleRule)rule).Style["padding"]);
				Assert.AreEqual(@"0.5pt solid #666", ((ICssStyleRule)rule).Style["border"]);
			}
			}
		}
	
		[Test]
		public void Media()
		{
			var sheet = ParseStyleSheet(@"@media screen, projection {
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
}
");
			Assert.AreEqual(2, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"screen, projection", ((ICssMediaRule)rule).MediaText);
			Assert.AreEqual(4, rule.Rules.Length);
			
			foreach (var rule in rule.Rules)
			{
				Assert.AreEqual(@"html", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"", ((ICssStyleRule)rule).Style["undefined"]);
				Assert.AreEqual(@"#fffef0", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#300", ((ICssStyleRule)rule).Style["color"]);
				Assert.AreEqual(@"body", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"", ((ICssStyleRule)rule).Style["undefined"]);
				Assert.AreEqual(@"35em", ((ICssStyleRule)rule).Style["max-width"]);
				Assert.AreEqual(@"0 auto", ((ICssStyleRule)rule).Style["margin"]);
			}
				Assert.AreEqual(@"print", ((ICssMediaRule)rule).MediaText);
			Assert.AreEqual(2, rule.Rules.Length);
			
			foreach (var rule in rule.Rules)
			{
				Assert.AreEqual(@"html", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"#fff", ((ICssStyleRule)rule).Style["background"]);
				Assert.AreEqual(@"#000", ((ICssStyleRule)rule).Style["color"]);
				Assert.AreEqual(@"body", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"1in", ((ICssStyleRule)rule).Style["padding"]);
				Assert.AreEqual(@"0.5pt solid #666", ((ICssStyleRule)rule).Style["border"]);
			}
			}
		}
	
		[Test]
		public void MessedUp()
		{
			var sheet = ParseStyleSheet(@"body { foo
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
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"body", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"'bar'", ((ICssStyleRule)rule).Style["foo"]);
				Assert.AreEqual(@"body", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"bar", ((ICssStyleRule)rule).Style["foo"]);
				Assert.AreEqual(@"baz", ((ICssStyleRule)rule).Style["bar"]);
				Assert.AreEqual(@"body", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"bar", ((ICssStyleRule)rule).Style["foo"]);
				Assert.AreEqual(@"baz", ((ICssStyleRule)rule).Style["bar"]);
			}
		}
	
		[Test]
		public void NamespaceLinebreak()
		{
			var sheet = ParseStyleSheet(@"@namespace
    ""http://www.w3.org/1999/xhtml""
    ;
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void Namespace()
		{
			var sheet = ParseStyleSheet(@"@namespace ""http://www.w3.org/1999/xhtml"";
@namespace svg ""http://www.w3.org/2000/svg"";
");
			Assert.AreEqual(2, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void NoSemi()
		{
			var sheet = ParseStyleSheet(@"
tobi loki jane {
  are: 'all';
  the-species: called ""ferrets""
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"tobi loki jane", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"'all'", ((ICssStyleRule)rule).Style["are"]);
				Assert.AreEqual(@"called ""ferrets""", ((ICssStyleRule)rule).Style["the-species"]);
			}
		}
	
		[Test]
		public void PageLinebreak()
		{
			var sheet = ParseStyleSheet(@"@page
    toc
    {
        color: black;
    }
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void PagedMedia()
		{
			var sheet = ParseStyleSheet(@"/* toc above */
@page toc, index:blank {
  /* toc inside */
  color: green;
}

@page {
  font-size: 16pt;
}

@page :left {
  margin-left: 5cm;
}
");
			Assert.AreEqual(4, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void Props()
		{
			var sheet = ParseStyleSheet(@"
tobi loki jane {
  are: 'all';
  the-species: called ""ferrets"";
  *even: 'ie crap';
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"tobi loki jane", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"'all'", ((ICssStyleRule)rule).Style["are"]);
				Assert.AreEqual(@"called ""ferrets""", ((ICssStyleRule)rule).Style["the-species"]);
				Assert.AreEqual(@"'ie crap'", ((ICssStyleRule)rule).Style["*even"]);
			}
		}
	
		[Test]
		public void QuoteEscape()
		{
			var sheet = ParseStyleSheet(@"p[qwe=""a\"",b""] { color: red }
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"p[qwe=""a\"",b""]", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"red", ((ICssStyleRule)rule).Style["color"]);
			}
		}
	
		[Test]
		public void Quoted()
		{
			var sheet = ParseStyleSheet(@"body {
  background: url('some;stuff;here') 50% 50% no-repeat;
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"body", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"url('some;stuff;here') 50% 50% no-repeat", ((ICssStyleRule)rule).Style["background"]);
			}
		}
	
		[Test]
		public void Rule()
		{
			var sheet = ParseStyleSheet(@"foo {
  bar: 'baz';
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"foo", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"'baz'", ((ICssStyleRule)rule).Style["bar"]);
			}
		}
	
		[Test]
		public void Rules()
		{
			var sheet = ParseStyleSheet(@"tobi {
  name: 'tobi';
  age: 2;
}

loki {
  name: 'loki';
  age: 1;
}
");
			Assert.AreEqual(2, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"tobi", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"'tobi'", ((ICssStyleRule)rule).Style["name"]);
				Assert.AreEqual(@"2", ((ICssStyleRule)rule).Style["age"]);
				Assert.AreEqual(@"loki", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"'loki'", ((ICssStyleRule)rule).Style["name"]);
				Assert.AreEqual(@"1", ((ICssStyleRule)rule).Style["age"]);
			}
		}
	
		[Test]
		public void Selectors()
		{
			var sheet = ParseStyleSheet(@"foo,
bar,
baz {
  color: 'black';
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@"foo, bar, baz", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"'black'", ((ICssStyleRule)rule).Style["color"]);
			}
		}
	
		[Test]
		public void SupportsLinebreak()
		{
			var sheet = ParseStyleSheet(@"@supports
    (display: flex)
    {
        .test { display: flex; }
    }
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void Supports()
		{
			var sheet = ParseStyleSheet(@"@supports (display: flex) or (display: box) {
  /* flex above */
  .flex {
    /* flex inside */
    display: box;
    display: flex;
  }

  div {
    something: else;
  }
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
			}
		}
	
		[Test]
		public void Wtf()
		{
			var sheet = ParseStyleSheet(@".wtf {
  *overflow-x: hidden;
  //max-height: 110px;
  #height: 18px;
}
");
			Assert.AreEqual(1, sheet.Rules.Length);
			
			foreach (var rule in sheet.Rules)
			{
				Assert.AreEqual(@".wtf", ((ICssStyleRule)rule).SelectorText);
				Assert.AreEqual(@"hidden", ((ICssStyleRule)rule).Style["*overflow-x"]);
				Assert.AreEqual(@"110px", ((ICssStyleRule)rule).Style["//max-height"]);
				Assert.AreEqual(@"18px", ((ICssStyleRule)rule).Style["#height"]);
			}
		}
	}
}