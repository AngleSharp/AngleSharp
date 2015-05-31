using AngleSharp;
using AngleSharp.Css;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;
using System;
using System.IO;

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
            Assert.AreEqual("red", h1.Style.Color);
            Assert.AreEqual("bold", h1.Style.FontWeight);
        }

        [Test]
        public void CssSheetSerializeListStyleNone()
        {
            var cssSrc = ".T1 {list-style:NONE}";
            var expected = ".T1 { list-style: NONE; }";
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
            var expected = "#rule1 { border-top: none; border-right: 1px solid rgb(187, 204, 235); border-bottom: 1px solid rgb(187, 204, 235); border-left: 1px solid rgb(187, 204, 235); }";
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
            Assert.AreEqual("green", p.Style.Color);
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
            Assert.AreEqual(2, h1.Style.Length);
            Assert.AreEqual("color", h1.Style[0]);
            Assert.AreEqual("red", h1.Style.Color);
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
            Assert.AreEqual("green", style.Style.Color);
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
            Assert.AreEqual("green", style.Style.Color);
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
            Assert.AreEqual("blue", h1.Style.Color);
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
            Assert.AreEqual("green", p.Style.Color);
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
            Assert.AreEqual("green", p.Style.Color);
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
            Assert.AreEqual("green", p.Style.Color);
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
            Assert.AreEqual("green", p.Style.Color);
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
            Assert.AreEqual("green", p.Style.Color);
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
            Assert.AreEqual("green", p.Style.Color);
        }

        [Test]
        public void CssCreateValueListConformal()
        {
            var valueString = "24px 12px 6px";
            var list = CssParser.ParseValue(valueString) as CssValue;
            Assert.AreEqual(CssValueType.List, list.Type);
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
            var list = CssParser.ParseValue(valueString) as CssValue;
            Assert.AreEqual(CssValueType.List, list.Type);
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
            var list = CssParser.ParseValue(valueString) as CssValue;
            Assert.AreEqual(CssValueType.List, list.Type);
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void CssCreateMultipleValues()
        {
            var valueString = "Arial, Verdana, Helvetica, Sans-Serif";
            var list = CssParser.ParseValue(valueString) as CssValue;
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
            var list = CssParser.ParseValue(valueString) as CssValue;
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
            var list = CssParser.ParseValue(valueString) as CssValue;
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
            Assert.AreEqual(CssValueType.Primitive, value.Type);
        }

        [Test]
        public void CssColorRed()
        {
            var valueString = "#FF0000";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.Primitive, value.Type);
        }

        [Test]
        public void CssColorMixedShort()
        {
            var valueString = "#07C";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.Primitive, value.Type);
        }

        [Test]
        public void CssColorGreenShort()
        {
            var valueString = "#00F";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.Primitive, value.Type);
        }

        [Test]
        public void CssColorRedShort()
        {
            var valueString = "#F00";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.Primitive, value.Type);
        }

        [Test]
        public void CssRgbaFunction()
        {
            var names = new[] { "border-top-color", "border-right-color", "border-bottom-color", "border-left-color" };
            var decls = CssParser.ParseDeclarations("border-color: rgba(82, 168, 236, 0.8)");
            Assert.IsNotNull(decls);
            Assert.AreEqual(4, decls.Length);

            for (int i = 0; i < decls.Length; i++)
            {
                var propertyName = decls[i];
                var decl = decls.GetProperty(propertyName);
                Assert.AreEqual(names[i], decl.Name);
                Assert.AreEqual(propertyName, decl.Name);
                Assert.IsFalse(decl.IsImportant);
                Assert.AreEqual(CssValueType.Primitive, decl.Value.Type);

                //var property = (CssBorderPartColorProperty)decl;
                //var color = property.Color;
                //Assert.AreEqual(new Color(82, 168, 236, 0.8f), color);
            }
        }

        [Test]
        public void CssMarginAll()
        {
            var names = new[] { "margin-top", "margin-right", "margin-bottom", "margin-left" };
            var decls = CssParser.ParseDeclarations("margin: 20px;");
            Assert.IsNotNull(decls);
            Assert.AreEqual(4, decls.Length);

            for (int i = 0; i < decls.Length; i++)
            {
                var propertyName = decls[i];
                var decl = decls.GetProperty(propertyName);
                Assert.AreEqual(names[i], decl.Name);
                Assert.AreEqual(propertyName, decl.Name);
                Assert.IsFalse(decl.IsImportant);
                Assert.AreEqual(CssValueType.Primitive, decl.Value.Type);
                Assert.AreEqual("20px", decl.Value.CssText);   
            }
        }

        [Test]
        public void CssSeveralFontFamily()
        {
            var decl = CssParser.ParseDeclarations("font-family: \"Helvetica Neue\", Helvetica, Arial, sans-serif");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var prop = decl.GetProperty("font-family");
            Assert.AreEqual("font-family", prop.Name);
            Assert.IsFalse(prop.IsImportant);
            Assert.AreEqual(CssValueType.List, prop.Value.Type);
            Assert.AreEqual("\"Helvetica Neue\", Helvetica, Arial, sans-serif", prop.Value.CssText);
        }

        [Test]
        public void CssFontWithSlashAndContent()
        {
            var decl = CssParser.ParseDeclarations("font: bold 1em/2em monospace; content: \" (\" attr(href) \")\"");
            Assert.IsNotNull(decl);
            Assert.AreEqual(8, decl.Length);

            Assert.AreEqual("bold 1em/2em monospace", decl.GetPropertyValue("font"));

            var content = decl.GetProperty("content");
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.IsImportant);
            Assert.AreEqual(CssValueType.List, content.Value.Type);
            Assert.AreEqual("\" (\" attr(href) \")\"", content.Value.CssText);
        }

        [Test]
        public void CssBackgroundWebkitGradient()
        {
            var background = CssParser.ParseDeclaration("background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #FFA84C), color-stop(100%, #FF7B0D))");
            Assert.IsNotNull(background);
            Assert.AreEqual("background", background.Name);
            Assert.IsFalse(background.IsImportant);
            Assert.AreEqual(CssValueType.Initial, background.Value.Type);
            Assert.IsFalse(background.HasValue);
        }

        [Test]
        public void CssBackgroundColorRgba()
        {
            var decl = CssParser.ParseDeclarations("background-color: rgba(255, 123, 13, 1)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var background = decl.GetProperty("background-color");
            Assert.AreEqual("background-color", background.Name);
            Assert.IsFalse(background.IsImportant);
            Assert.AreEqual(CssValueType.Primitive, background.Value.Type);
            Assert.AreEqual("rgba(255, 123, 13, 1)", background.Value.CssText);
        }

        [Test]
        public void CssFontWithFraction()
        {
            var font = CssParser.ParseDeclaration("font:bold 40px/1.13 'PT Sans Narrow', sans-serif");
            Assert.AreEqual("font", font.Name);
            Assert.IsFalse(font.IsImportant);
            Assert.AreEqual(CssValueType.List, font.Value.Type);
        }

        [Test]
        public void CssTextShadow()
        {
            var decl = CssParser.ParseDeclarations("text-shadow: 0 0 10px #000");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var textShadow = decl.GetProperty("text-shadow");
            Assert.AreEqual("text-shadow", textShadow.Name);
            Assert.IsFalse(textShadow.IsImportant);
            Assert.AreEqual(CssValueType.List, textShadow.Value.Type);
        }

        [Test]
        public void CssBackgroundWithImage()
        {
            var background = CssParser.ParseDeclaration("background:url(../images/ribbon.svg) no-repeat");
            Assert.AreEqual("background", background.Name);
            Assert.IsFalse(background.IsImportant);
            Assert.AreEqual(CssValueType.List, background.Value.Type);
        }

        [Test]
        public void CssContentWithCounter()
        {
            var decl = CssParser.ParseDeclarations("content:counter(paging, decimal-leading-zero)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var content = decl.GetProperty("content");
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.IsImportant);
            Assert.AreEqual(CssValueType.Primitive, content.Value.Type);
        }

        [Test]
        public void CssBackgroundColorRgb()
        {
            var decl = CssParser.ParseDeclarations("background-color: rgb(245, 0, 111)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var backgroundColor = decl.GetProperty("background-color");
            Assert.AreEqual("background-color", backgroundColor.Name);
            Assert.IsFalse(backgroundColor.IsImportant);
            Assert.AreEqual(CssValueType.Primitive, backgroundColor.Value.Type);
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
            var decl = CssParser.ParseDeclarations("content:'\005E'");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var content = decl.GetProperty("content");
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.IsImportant);
            Assert.AreEqual(CssValueType.Primitive, content.Value.Type);
        }

        [Test]
        public void CssContentCounter()
        {
            var decl = CssParser.ParseDeclarations("content:counter(list)'.'");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var content = decl.GetProperty("content");
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.IsImportant);
            //Assert.AreEqual(CssValueType.List, content.Value.Type);
        }

        [Test]
        public void CssTransformTranslate()
        {
            var decl = CssParser.ParseDeclarations("transform:translateY(-50%)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var transform = decl.GetProperty("transform");
            Assert.AreEqual("transform", transform.Name);
            Assert.IsFalse(transform.IsImportant);
            Assert.AreEqual(CssValueType.Primitive, transform.Value.Type);
        }

        [Test]
        public void CssBoxShadowMultiline()
        {
            var decl = CssParser.ParseDeclarations(@"
        box-shadow:
			0 0 0 10px rgba(60, 61, 64, 0.6),
			0 0 50px #3C3D40;");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var boxShadow = decl.GetProperty("box-shadow");
            Assert.AreEqual("box-shadow", boxShadow.Name);
            Assert.IsFalse(boxShadow.IsImportant);
            Assert.AreEqual(CssValueType.List, boxShadow.Value.Type);
        }

        [Test]
        public void CssDisplayBlock()
        {
            var decl = CssParser.ParseDeclarations("display:block");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var display = decl.GetProperty("display");
            Assert.AreEqual("display", display.Name);
            Assert.IsFalse(display.IsImportant);
            Assert.AreEqual(CssValueType.Primitive, display.Value.Type);
            Assert.AreEqual("block", display.Value.CssText);
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
    }
}
