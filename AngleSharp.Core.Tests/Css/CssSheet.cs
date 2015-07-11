using AngleSharp;
using AngleSharp.Css;
using AngleSharp.Dom.Css;
using AngleSharp.Network.Default;
using AngleSharp.Parser.Css;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AngleSharp.Core.Tests
{
    [TestFixture]
    public class CssSheetTests
    {
        static ICssStyleSheet ParseStyleSheet(String source)
        {
            var parser = new CssParser(source);
            return parser.Parse();
        }

        static CssStyleDeclaration ParseDeclarations(String declarations)
        {
            var style = new CssStyleDeclaration();
            CssParser.AppendDeclarations(style, declarations);
            return style;
        }

        [Test]
        public void CssSheetOnEofDuringRuleWithoutSemicolon()
        {
            var sheet = ParseStyleSheet(@"
h1 {
 color: red;
 font-weight: bold");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var h1 = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("h1", h1.SelectorText);
            Assert.AreEqual("rgb(255, 0, 0)", h1.Style.Color);
            Assert.AreEqual("bold", h1.Style.FontWeight);
        }

        [Test]
        public void CssSheet1WithDoubleMarkedCommentFromIssue93()
        {
            var sheet = ParseStyleSheet(@"
            /**special css**/
            .dis-none { display: none;}
            .dis { display: block; }
            /*common css*/
            .dis2 { display: block; }
            ");
            var css = sheet.CssText;
            Assert.AreEqual(3, sheet.Rules.Length);
            Assert.AreEqual(".dis-none { display: none; }", sheet.Rules[0].CssText);
            Assert.AreEqual(".dis { display: block; }", sheet.Rules[1].CssText);
            Assert.AreEqual(".dis2 { display: block; }", sheet.Rules[2].CssText);
        }

        [Test]
        public void CssSheet2WithDoubleMarkedCommentFromIssue93()
        {
            var sheet = ParseStyleSheet(@"
            /**special css**/
            .dis-none { display: none;}
            .dis { display: block; }
            ");
            var css = sheet.CssText;
            Assert.AreEqual(2, sheet.Rules.Length);
            Assert.AreEqual(".dis-none { display: none; }", sheet.Rules[0].CssText);
            Assert.AreEqual(".dis { display: block; }", sheet.Rules[1].CssText);
        }

        [Test]
        public void CssSheetSerializeListStyleNone()
        {
            var cssSrc = ".T1 {list-style:NONE}";
            var expected = ".T1 { list-style: none; }";
            var stylesheet = ParseStyleSheet(cssSrc);
            var cssText = stylesheet.CssText;
            Assert.AreEqual(expected, cssText);
        }

        [Test]
        public void CssSheetSerializeBorder1pxOutset()
        {
            var cssSrc = ".T2 { border:1px  outset }";
            var expected = ".T2 { border: 1px outset; }";
            var stylesheet = ParseStyleSheet(cssSrc);
            var cssText = stylesheet.CssText;
            Assert.AreEqual(expected, cssText);
        }

        [Test]
        public void CssSheetSerializeBorder1pxSolidWithColor()
        {
            var cssSrc = "#rule1 { border: 1px solid #BBCCEB; border-top: none }";
            var expected = "#rule1 { border-right: 1px solid rgb(187, 204, 235); border-bottom: 1px solid rgb(187, 204, 235); border-left: 1px solid rgb(187, 204, 235); border-top: none; }";
            var stylesheet = ParseStyleSheet(cssSrc);
            var cssText = stylesheet.CssText;
            Assert.AreEqual(expected, cssText);
        }

        [Test]
        public void CssSheetSerializeBackgroundWithUrlPositionRepeatX()
        {
            var cssSrc = "#rule2 { background:url(/_static/img/bx_tile.gif) top left repeat-x; }";
            var expected = "#rule2 { background: url(\"/_static/img/bx_tile.gif\") top left repeat-x; }";
            var stylesheet = ParseStyleSheet(cssSrc);
            var cssText = stylesheet.CssText;
            Assert.AreEqual(expected, cssText);
        }

        [Test]
        public void CssSheetIgnoreVendorPrefixes()
        {
            var css = @".something { 
  -o-border-radius: 5px;
  -webkit-border-radius: 5px;
  border-radius: 5px;
  display: -webkit-box;
  display: -webkit-flex;
  display: -ms-flexbox;
  display: flex;
  background: -webkit-linear-gradient(red, green);
  background: linear-gradient(red, green);
}";
            var stylesheet = ParseStyleSheet(css);
            Assert.AreEqual(1, stylesheet.Rules.Length);
            var style = stylesheet.Rules[0] as ICssStyleRule;
            Assert.IsNotNull(style);
            Assert.AreEqual(13, style.Style.Length);
        }

        [Test]
        public void CssSheetSimpleStyleRuleStringification()
        {
            var css = @"html { font-family: sans-serif; }";
            var stylesheet = ParseStyleSheet(css);
            Assert.AreEqual(1, stylesheet.Rules.Length);
            var rule = stylesheet.Rules[0];
            Assert.IsInstanceOf<CssStyleRule>(rule);
            Assert.AreEqual(css, rule.CssText);
        }

        [Test]
        public void CssSheetCloseStringsEndOfLine()
        {
            var sheet = ParseStyleSheet(@"p {
        color: green;
        font-family: 'Courier New Times
        color: red;
        color: green;
      }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("rgb(0, 128, 0)", p.Style.Color);
        }

        [Test]
        public void CssSheetOnEofDuringRuleWithinString()
        {
            var sheet = ParseStyleSheet(@"
#something {
 content: 'hi there");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var id = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("#something", id.SelectorText);
            Assert.AreEqual("\"hi there\"", id.Style.Content);
        }

        [Test]
        public void CssSheetOnEofDuringAtMediaRuleWithinString()
        {
            var sheet = ParseStyleSheet(@"  @media screen {
    p:before { content: 'Hello");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssMediaRule>(sheet.Rules[0]);
            var media = sheet.Rules[0] as CssMediaRule;
            Assert.AreEqual("screen", media.Media.MediaText);
            Assert.AreEqual(1, media.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(media.Rules[0]);
            var p = media.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p::before", p.SelectorText);
            Assert.AreEqual("\"Hello\"", p.Style.Content);
        }

        [Test]
        public void CssSheetIgnoreUnknownProperty()
        {
            var sheet = ParseStyleSheet(@"h1 { color: red; rotation: 70minutes }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var h1 = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("h1", h1.SelectorText);
            Assert.AreEqual(1, h1.Style.Length);
            Assert.AreEqual("color", h1.Style[0]);
            Assert.AreEqual("rgb(255, 0, 0)", h1.Style.Color);
        }

        [Test]
        public void CssSheetInvalidStatementRulesetUnexpectedAtKeyword()
        {
            var sheet = ParseStyleSheet(@"p @here {color: red}");
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void CssSheetInvalidStatementAtRuleUnexpectedAtKeyword()
        {
            var sheet = ParseStyleSheet(@"@foo @bar;");
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void CssSheetInvalidStatementRulesetUnexpectedRightBrace()
        {
            var sheet = ParseStyleSheet(@"}} {{ - }}");
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void CssSheetInvalidStatementRulesetUnexpectedRightBraceWithValidQualifiedRule()
        {
            var sheet = ParseStyleSheet(@"}} {{ - }}
#hi { color: green; }");
            Assert.AreEqual(1, sheet.Rules.Length);
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.NotNull(style);
            Assert.AreEqual("#hi", style.SelectorText);
            Assert.AreEqual(1, style.Style.Length);
            Assert.AreEqual("rgb(0, 128, 0)", style.Style.Color);
        }

        [Test]
        public void CssSheetInvalidStatementRulesetUnexpectedRightParenthesis()
        {
            var sheet = ParseStyleSheet(@") ( {} ) p {color: red }");
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void CssSheetInvalidStatementRulesetUnexpectedRightParenthesisWithValidQualifiedRule()
        {
            var sheet = ParseStyleSheet(@") {} p {color: green }");
            Assert.AreEqual(1, sheet.Rules.Length);
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.NotNull(style);
            Assert.AreEqual("p", style.SelectorText);
            Assert.AreEqual(1, style.Style.Length);
            Assert.AreEqual("rgb(0, 128, 0)", style.Style.Color);
        }

        [Test]
        public void CssSheetIgnoreUnknownAtRule()
        {
            var sheet = ParseStyleSheet(@"@three-dee {
  @background-lighting {
    azimuth: 30deg;
    elevation: 190deg;
  }
  h1 { color: red }
}
h1 { color: blue }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var h1 = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("h1", h1.SelectorText);
            Assert.AreEqual(1, h1.Style.Length);
            Assert.AreEqual("color", h1.Style[0]);
            Assert.AreEqual("rgb(0, 0, 255)", h1.Style.Color);
        }

        [Test]
        public void CssSheetKeepValidValueFloat()
        {
            var sheet = ParseStyleSheet(@"img { float: left }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var img = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("img", img.SelectorText);
            Assert.AreEqual(1, img.Style.Length);
            Assert.AreEqual("float", img.Style[0]);
            Assert.AreEqual("left", img.Style.Float);
        }

        [Test]
        public void CssSheetIgnoreInvalidValueFloat()
        {
            var sheet = ParseStyleSheet(@"img { float: left here }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var img = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("img", img.SelectorText);
            Assert.AreEqual(0, img.Style.Length);
        }

        [Test]
        public void CssSheetIgnoreInvalidValueBackground()
        {
            var sheet = ParseStyleSheet(@"img { background: ""red"" }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var img = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("img", img.SelectorText);
            Assert.AreEqual(0, img.Style.Length);
        }

        [Test]
        public void CssSheetIgnoreInvalidValueBorderWidth()
        {
            var sheet = ParseStyleSheet(@"img { border-width: 3 }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var img = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("img", img.SelectorText);
            Assert.AreEqual(0, img.Style.Length);
        }

        [Test]
        public void CssSheetWellformedDeclaration()
        {
            var sheet = ParseStyleSheet(@"p { color:green; }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("rgb(0, 128, 0)", p.Style.Color);
        }

        [Test]
        public void CssSheetMalformedDeclarationMissingColon()
        {
            var sheet = ParseStyleSheet(@"p { color:green; color }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("rgb(0, 128, 0)", p.Style.Color);
        }

        [Test]
        public void CssSheetMalformedDeclarationMissingColonWithRecovery()
        {
            var sheet = ParseStyleSheet(@"p { color:red;   color; color:green }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("rgb(0, 128, 0)", p.Style.Color);
        }

        [Test]
        public void CssSheetMalformedDeclarationMissingValue()
        {
            var sheet = ParseStyleSheet(@"p { color:green; color: }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("rgb(0, 128, 0)", p.Style.Color);
        }

        [Test]
        public void CssSheetMalformedDeclarationUnexpectedTokens()
        {
            var sheet = ParseStyleSheet(@"p { color:green; color{;color:maroon} }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("rgb(0, 128, 0)", p.Style.Color);
        }

        [Test]
        public void CssSheetMalformedDeclarationUnexpectedTokensWithRecovery()
        {
            var sheet = ParseStyleSheet(@"p { color:red;   color{;color:maroon}; color:green }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssStyleRule>(sheet.Rules[0]);
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("rgb(0, 128, 0)", p.Style.Color);
        }

        [Test]
        public void CssCreateValueListConformal()
        {
            var valueString = "24px 12px 6px";
            var list = CssParser.ParseValue(valueString);
            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(list[0].ToValue(), "24px");
            Assert.AreEqual(list[1].ToValue(), " ");
            Assert.AreEqual(list[2].ToValue(), "12px");
            Assert.AreEqual(list[3].ToValue(), " ");
            Assert.AreEqual(list[4].ToValue(), "6px");
        }

        [Test]
        public void CssCreateValueListNonConformal()
        {
            var valueString = "  24px  12px 6px  13px ";
            var list = CssParser.ParseValue(valueString);
            Assert.AreEqual(7, list.Count);
            Assert.AreEqual(list[0].ToValue(), "24px");
            Assert.AreEqual(list[1].ToValue(), " ");
            Assert.AreEqual(list[2].ToValue(), "12px");
            Assert.AreEqual(list[3].ToValue(), " ");
            Assert.AreEqual(list[4].ToValue(), "6px");
            Assert.AreEqual(list[5].ToValue(), " ");
            Assert.AreEqual(list[6].ToValue(), "13px");
        }

        [Test]
        public void CssCreateValueListEmpty()
        {
            var valueString = "";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNull(value);
        }

        [Test]
        public void CssCreateValueListSpaces()
        {
            var valueString = "  ";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNull(value);
        }

        [Test]
        public void CssCreateValueListIllegal()
        {
            var valueString = " , ";
            var list = CssParser.ParseValue(valueString);
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void CssCreateMultipleValues()
        {
            var valueString = "Arial, Verdana, Helvetica, Sans-Serif";
            var list = CssParser.ParseValue(valueString);
            Assert.AreEqual(10, list.Count);
            Assert.AreEqual("Arial", list[0].Data);
            Assert.AreEqual("Verdana", list[3].Data);
            Assert.AreEqual("Helvetica", list[6].Data);
            Assert.AreEqual("Sans-Serif", list[9].Data);
        }

        [Test]
        public void CssCreateMultipleValueLists()
        {
            var valueString = "Arial 10pt bold, Verdana 12pt italic";
            var list = CssParser.ParseValue(valueString);
            Assert.AreEqual(12, list.Count);
            Assert.AreEqual("Arial", list[0].ToValue());
            Assert.AreEqual("Verdana", list[7].ToValue());
            Assert.AreEqual("10pt", list[2].ToValue());
            Assert.AreEqual("12pt", list[9].ToValue());
            Assert.AreEqual("bold", list[4].ToValue());
            Assert.AreEqual("italic", list[11].ToValue());
        }

        [Test]
        public void CssCreateMultipleValuesNonConformal()
        {
            var valueString = "  Arial  ,  Verdana  ,Helvetica,Sans-Serif   ";
            var list = CssParser.ParseValue(valueString);
            Assert.AreEqual(10, list.Count);
            Assert.AreEqual("Arial", list[0].ToValue());
            Assert.AreEqual("Verdana", list[3].ToValue());
            Assert.AreEqual("Helvetica", list[6].ToValue());
            Assert.AreEqual("Sans-Serif", list[9].ToValue());
        }

        [Test]
        public void CssColorBlack()
        {
            var valueString = "#000000";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssColorRed()
        {
            var valueString = "#FF0000";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssColorMixedShort()
        {
            var valueString = "#07C";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssColorGreenShort()
        {
            var valueString = "#00F";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssColorRedShort()
        {
            var valueString = "#F00";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
        }

        [Test]
        public void CssRgbaFunction()
        {
            var names = new[] { "border-top-color", "border-right-color", "border-bottom-color", "border-left-color" };
            var decls = ParseDeclarations("border-color: rgba(82, 168, 236, 0.8)");
            Assert.IsNotNull(decls);
            Assert.AreEqual(4, decls.Length);

            for (int i = 0; i < decls.Length; i++)
            {
                var propertyName = decls[i];
                var decl = decls.GetProperty(propertyName);
                Assert.AreEqual(names[i], decl.Name);
                Assert.AreEqual(propertyName, decl.Name);
                Assert.IsFalse(decl.IsImportant);

                //var property = (CssBorderPartColorProperty)decl;
                //var color = property.Color;
                //Assert.AreEqual(new Color(82, 168, 236, 0.8f), color);
            }
        }

        [Test]
        public void CssMarginAll()
        {
            var names = new[] { "margin-top", "margin-right", "margin-bottom", "margin-left" };
            var decls = ParseDeclarations("margin: 20px;");
            Assert.IsNotNull(decls);
            Assert.AreEqual(4, decls.Length);

            for (int i = 0; i < decls.Length; i++)
            {
                var propertyName = decls[i];
                var decl = decls.GetProperty(propertyName);
                Assert.AreEqual(names[i], decl.Name);
                Assert.AreEqual(propertyName, decl.Name);
                Assert.IsFalse(decl.IsImportant);
                Assert.AreEqual("20px", decl.Value);   
            }
        }

        [Test]
        public void CssSeveralFontFamily()
        {
            var prop = CssParser.ParseDeclaration("font-family: \"Helvetica Neue\", Helvetica, Arial, sans-serif");
            Assert.AreEqual("font-family", prop.Name);
            Assert.IsFalse(prop.IsImportant);
            Assert.AreEqual("\"Helvetica Neue\", Helvetica, Arial, sans-serif", prop.Value);
        }

        [Test]
        public void CssFontWithSlashAndContent()
        {
            var decl = ParseDeclarations("font: bold 1em/2em monospace; content: \" (\" attr(href) \")\"");
            Assert.IsNotNull(decl);
            Assert.AreEqual(8, decl.Length);

            Assert.AreEqual("bold 1em / 2em monospace", decl.GetPropertyValue("font"));

            var content = decl.GetProperty("content");
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.IsImportant);
            Assert.AreEqual("\" (\" attr(href) \")\"", content.Value);
        }

        [Test]
        public void CssBackgroundWebkitGradient()
        {
            var background = CssParser.ParseDeclaration("background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #FFA84C), color-stop(100%, #FF7B0D))");
            Assert.IsNotNull(background);
            Assert.AreEqual("background", background.Name);
            Assert.IsFalse(background.IsImportant);
            Assert.IsFalse(background.HasValue);
        }

        [Test]
        public void CssBackgroundColorRgba()
        {
            var background = CssParser.ParseDeclaration("background-color: rgba(255, 123, 13, 1)");
            Assert.AreEqual("background-color", background.Name);
            Assert.IsFalse(background.IsImportant);
            Assert.AreEqual("rgba(255, 123, 13, 1)", background.Value);
        }

        [Test]
        public void CssFontWithFraction()
        {
            var font = CssParser.ParseDeclaration("font:bold 40px/1.13 'PT Sans Narrow', sans-serif");
            Assert.AreEqual("font", font.Name);
            Assert.IsFalse(font.IsImportant);
        }

        [Test]
        public void CssTextShadow()
        {
            var textShadow = CssParser.ParseDeclaration("text-shadow: 0 0 10px #000");
            Assert.AreEqual("text-shadow", textShadow.Name);
            Assert.IsFalse(textShadow.IsImportant);
        }

        [Test]
        public void CssBackgroundWithImage()
        {
            var background = CssParser.ParseDeclaration("background:url(../images/ribbon.svg) no-repeat");
            Assert.AreEqual("background", background.Name);
            Assert.IsFalse(background.IsImportant);
        }

        [Test]
        public void CssContentWithCounter()
        {
            var content = CssParser.ParseDeclaration("content:counter(paging, decimal-leading-zero)");
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.IsImportant);
        }

        [Test]
        public void CssBackgroundColorRgb()
        {
            var backgroundColor = CssParser.ParseDeclaration("background-color: rgb(245, 0, 111)");
            Assert.AreEqual("background-color", backgroundColor.Name);
            Assert.IsFalse(backgroundColor.IsImportant);
        }

        [Test]
        public void CssImportSheet()
        {
            var rule = "@import url(fonts.css);";
            var decl = CssParser.ParseRule(rule);
            Assert.IsNotNull(decl);
            Assert.IsInstanceOf<CssImportRule>(decl);
            var importRule = (CssImportRule)decl;
            Assert.AreEqual("fonts.css", importRule.Href);
        }

        [Test]
        public void CssContentEscaped()
        {
            var content = CssParser.ParseDeclaration("content:'\005E'");
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.IsImportant);
        }

        [Test]
        public void CssContentCounter()
        {
            var content = CssParser.ParseDeclaration("content:counter(list)'.'");
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.IsImportant);
            //Assert.AreEqual(CssValueType.List, content.Value.Type);
        }

        [Test]
        public void CssTransformTranslate()
        {
            var transform = CssParser.ParseDeclaration("transform:translateY(-50%)");
            Assert.AreEqual("transform", transform.Name);
            Assert.IsFalse(transform.IsImportant);
        }

        [Test]
        public void CssBoxShadowMultiline()
        {
            var boxShadow = CssParser.ParseDeclaration(@"
        box-shadow:
			0 0 0 10px rgba(60, 61, 64, 0.6),
			0 0 50px #3C3D40;");
            Assert.AreEqual("box-shadow", boxShadow.Name);
            Assert.IsFalse(boxShadow.IsImportant);
        }

        [Test]
        public void CssDisplayBlock()
        {
            var display = CssParser.ParseDeclaration("display:block");
            Assert.AreEqual("display", display.Name);
            Assert.IsFalse(display.IsImportant);
            Assert.AreEqual("block", display.Value);
        }

        [Test]
        public void CssSheetWithDataUrlAsBackgroundImage()
        {
            var sheet = ParseStyleSheet(".App_Header_ .logo { background-image: url(\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEcAAAAcCAMAAAAEJ1IZAAAABGdBTUEAALGPC/xhBQAAVAI/VAI/VAI/VAI/VAI/VAI/VAAAA////AI/VRZ0U8AAAAFJ0Uk5TYNV4S2UbgT/Gk6uQt585w2wGXS0zJO2lhGttJK6j4YqZSobH1AAAAAElFTkSuQmCC\"); background-size: 71px 28px; background-position: 0 19px; width: 71px; }");
            Assert.IsNotNull(sheet);
            Assert.AreEqual(1, sheet.Rules.Length);
            var rule = sheet.Rules[0] as CssStyleRule;
            Assert.IsNotNull(rule);
            Assert.AreEqual(4, rule.Style.Length);
            Assert.AreEqual(".App_Header_ .logo", rule.SelectorText);
            var decl = rule.Style as ICssStyleDeclaration;
            Assert.AreEqual("url(\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEcAAAAcCAMAAAAEJ1IZAAAABGdBTUEAALGPC/xhBQAAVAI/VAI/VAI/VAI/VAI/VAI/VAAAA////AI/VRZ0U8AAAAFJ0Uk5TYNV4S2UbgT/Gk6uQt585w2wGXS0zJO2lhGttJK6j4YqZSobH1AAAAAElFTkSuQmCC\")", decl.BackgroundImage);
            Assert.AreEqual("71px 28px", decl.BackgroundSize);
            Assert.AreEqual("0 19px", decl.BackgroundPosition);
            Assert.AreEqual("71px", decl.Width);
        }

        [Test]
        public void CssSheetFromStreamWeirdBytesLeadingToInfiniteLoop()
        {
            var bs = new Byte[8];
            bs[0] = 239;
            bs[1] = 187;
            bs[2] = 191;
            bs[3] = 117;
            bs[4] = 43;
            bs[5] = 63;
            bs[6] = 63;
            bs[7] = 63;

            using (var memoryStream = new MemoryStream(bs, false))
            {
                var sheet = memoryStream.ToCssStylesheet();
            }
        }

        [Test]
        public void CssSheetFromStreamOnlyZerosAvailable()
        {
            var bs = new Byte[7180];

            using (var memoryStream = new MemoryStream(bs, false))
            {
                var sheet = memoryStream.ToCssStylesheet();
                Assert.IsNotNull(sheet);
                Assert.AreEqual(0, sheet.Rules.Length);
            }
        }

        [Test]
        public void CssSheetFromStringWithQuestionMarksLeadingToInfiniteLoop()
        {
            var sheet = "U+???\0".ToCssStylesheet();
            Assert.IsNotNull(sheet);
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void CssDefaultSheetSupportsRoundTripping()
        {
            var originalSourceCode = @"p.info {
	font-family: arial, sans-serif;
	line-height: 150%;
	margin-left: 2em;
	padding: 1em;
	border: 3px solid red;
	background-color: #f89;
	display: inline-block;
}
p.info span {
	font-weight: bold;
}
p.info span::after {
	content: ': ';
}";
            var initialSheet = originalSourceCode.ToCssStylesheet();
            var initialSourceCode = initialSheet.CssText;
            var finalSheet = initialSourceCode.ToCssStylesheet();
            var finalSourceCode = finalSheet.CssText;
            Assert.AreEqual(initialSourceCode, finalSourceCode);
            Assert.AreEqual(initialSheet.Rules.Length, finalSheet.Rules.Length);
        }

        [Test]
        public void CssParseSheetWithStyleMediaAndStyleRule()
        {
            var sheet = ParseStyleSheet(@".mobile,.tablet{display:none;} @media only screen and(max-width:51.875em){.tablet{display:block;}} .disp {display:block;}");
            Assert.AreEqual(3, sheet.Rules.Length);
            Assert.AreEqual(CssRuleType.Style, sheet.Rules[0].Type);
            Assert.AreEqual(CssRuleType.Media, sheet.Rules[1].Type);
            Assert.AreEqual(CssRuleType.Style, sheet.Rules[2].Type);
        }

        [Test]
        public void CssParseSheetWithMediaAndTwoStyleRules()
        {
            var sheet = ParseStyleSheet(@"@media only screen and(max-width:51.875em){.tablet{display:block;}} .mobile,.tablet{display:none;} .disp {display:block;}");
            Assert.AreEqual(3, sheet.Rules.Length);
            Assert.AreEqual(CssRuleType.Media, sheet.Rules[0].Type);
            Assert.AreEqual(CssRuleType.Style, sheet.Rules[1].Type);
            Assert.AreEqual(CssRuleType.Style, sheet.Rules[2].Type);
        }

        [Test]
        public void CssParseSheetWithTwoStyleAndMediaRule()
        {
            var sheet = ParseStyleSheet(@".mobile,.tablet{display:none;} .disp {display:block;} @media only screen and(max-width:51.875em){.tablet{display:block;}}");
            Assert.AreEqual(3, sheet.Rules.Length);
            Assert.AreEqual(CssRuleType.Style, sheet.Rules[0].Type);
            Assert.AreEqual(CssRuleType.Style, sheet.Rules[1].Type);
            Assert.AreEqual(CssRuleType.Media, sheet.Rules[2].Type);
        }

        [Test]
        public void CssParseImportStatementWithNoMediaTextFollowedByStyle()
        {
            var src = "@import url(import3.css); p { color : #f00; }";
            var sheet = ParseStyleSheet(src);
            Assert.AreEqual(2, sheet.Rules.Length);
            var import = sheet.Rules[0] as ICssImportRule;
            var style = sheet.Rules[1] as ICssStyleRule;
            Assert.IsNotNull(import);
            Assert.IsNotNull(style);
            Assert.AreEqual(0, import.Media.Length);
            Assert.AreEqual("", import.Media.MediaText);
            Assert.AreEqual("import3.css", import.Href);
            Assert.AreEqual("p", style.Selector.Text);
            Assert.AreEqual(1, style.Style.Length);
        }

        [Test]
        public void CssParseMediaRuleWithInvalidMediumEntities()
        {
            var src = "@media only screen and (min--moz-device-pixel-ratio:1.5),only screen and (-o-min-device-pixel-ratio:3/2),only screen and (-webkit-min-device-pixel-ratio:1.5),only screen and (min-device-pixel-ratio:1.5){.favicon{background-image:url('../img/favicons-sprite32.png?v=1b9547cf9cee3350a5b4875951e3e552');background-size:16px 5634px}}";
            var sheet = ParseStyleSheet(src);
            Assert.AreEqual(1, sheet.Rules.Length);
            var media = sheet.Rules[0] as ICssMediaRule;
            Assert.IsNotNull(media);
            Assert.AreEqual(1, media.Media.Length);
            Assert.AreEqual(1, media.Rules.Length);
            Assert.AreEqual("only screen and (min-device-pixel-ratio: 1.5)", media.ConditionText);
        }

        [Test]
        public void CssParseStyleWithInvalidSurrogatePair()
        {
            var src = @"span.berschrift2Zchn
{mso-style-name:""\00DCberschrift 2 Zchn"";
mso-style-priority:9;
mso-style-link:""\00DCberschrift 2"";
font-family:""Cambria"",""serif"";
color:#4F81BD;
font-weight:bold;}";
            var sheet = ParseStyleSheet(src);
            Assert.AreEqual(1, sheet.Rules.Length);
            var style = sheet.Rules[0] as ICssStyleRule;
            Assert.IsNotNull(style);
            Assert.AreEqual("span.berschrift2Zchn", style.SelectorText);
            Assert.AreEqual(3, style.Style.Length);
        }
    }
}
