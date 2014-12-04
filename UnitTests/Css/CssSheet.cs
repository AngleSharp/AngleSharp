using AngleSharp;
using AngleSharp.Css;
using AngleSharp.DOM.Css;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class CssSheetTests
    {
        [TestMethod]
        public void CssSheetOnEofDuringRuleWithoutSemicolon()
        {
            var sheet = CssParser.ParseStyleSheet(@"
h1 {
 color: red;
 font-weight: bold");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var h1 = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("h1", h1.SelectorText);
            Assert.AreEqual("red", h1.Style.Color);
            Assert.AreEqual("bold", h1.Style.FontWeight);
        }

        [TestMethod]
        public void CssSheetSimpleStyleRuleStringification()
        {
            var css = @"html { font-family: sans-serif; }";
            var stylesheet = CssParser.ParseStyleSheet(css);
            Assert.AreEqual(1, stylesheet.Rules.Length);
            var rule = stylesheet.Rules[0];
            Assert.IsInstanceOfType(rule, typeof(CSSStyleRule));
            Assert.AreEqual(css, rule.CssText);
        }

        [TestMethod]
        public void CssSheetCloseStringsEndOfLine()
        {
            var sheet = CssParser.ParseStyleSheet(@"p {
        color: green;
        font-family: 'Courier New Times
        color: red;
        color: green;
      }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("green", p.Style.Color);
        }

        [TestMethod]
        public void CssSheetOnEofDuringRuleWithinString()
        {
            var sheet = CssParser.ParseStyleSheet(@"
#something {
 content: 'hi there");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var id = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("#something", id.SelectorText);
            Assert.AreEqual("\"hi there\"", id.Style.Content);
        }

        [TestMethod]
        public void CssSheetOnEofDuringAtMediaRuleWithinString()
        {
            var sheet = CssParser.ParseStyleSheet(@"  @media screen {
    p:before { content: 'Hello");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSMediaRule));
            var media = sheet.Rules[0] as CSSMediaRule;
            Assert.AreEqual("screen", media.Media.MediaText);
            Assert.AreEqual(1, media.Rules.Length);
            Assert.IsInstanceOfType(media.Rules[0], typeof(CSSStyleRule));
            var p = media.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p:before", p.SelectorText);
            Assert.AreEqual("\"Hello\"", p.Style.Content);
        }

        [TestMethod]
        public void CssSheetIgnoreUnknownProperty()
        {
            var sheet = CssParser.ParseStyleSheet(@"h1 { color: red; rotation: 70minutes }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var h1 = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("h1", h1.SelectorText);
            Assert.AreEqual(1, h1.Style.Length);
            Assert.AreEqual("color", h1.Style[0]);
            Assert.AreEqual("red", h1.Style.Color);
        }

        [TestMethod]
        public void CssSheetInvalidStatementRulesetUnexpectedAtKeyword()
        {
            var sheet = CssParser.ParseStyleSheet(@"p @here {color: red}");
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [TestMethod]
        public void CssSheetInvalidStatementAtRuleUnexpectedAtKeyword()
        {
            var sheet = CssParser.ParseStyleSheet(@"@foo @bar;");
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [TestMethod]
        public void CssSheetInvalidStatementRulesetUnexpectedRightBrace()
        {
            var sheet = CssParser.ParseStyleSheet(@"}} {{ - }}");
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [TestMethod]
        public void CssSheetInvalidStatementRulesetUnexpectedRightParenthesis()
        {
            var sheet = CssParser.ParseStyleSheet(@") ( {} ) p {color: red }");
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [TestMethod]
        public void CssSheetIgnoreUnknownAtRule()
        {
            var sheet = CssParser.ParseStyleSheet(@"@three-dee {
  @background-lighting {
    azimuth: 30deg;
    elevation: 190deg;
  }
  h1 { color: red }
}
h1 { color: blue }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var h1 = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("h1", h1.SelectorText);
            Assert.AreEqual(1, h1.Style.Length);
            Assert.AreEqual("color", h1.Style[0]);
            Assert.AreEqual("blue", h1.Style.Color);
        }

        [TestMethod]
        public void CssSheetKeepValidValueFloat()
        {
            var sheet = CssParser.ParseStyleSheet(@"img { float: left }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var img = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("img", img.SelectorText);
            Assert.AreEqual(1, img.Style.Length);
            Assert.AreEqual("float", img.Style[0]);
            Assert.AreEqual("left", img.Style.Float);
        }

        [TestMethod]
        public void CssSheetIgnoreInvalidValueFloat()
        {
            var sheet = CssParser.ParseStyleSheet(@"img { float: left here }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var img = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("img", img.SelectorText);
            Assert.AreEqual(0, img.Style.Length);
        }

        [TestMethod]
        public void CssSheetIgnoreInvalidValueBackground()
        {
            var sheet = CssParser.ParseStyleSheet(@"img { background: ""red"" }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var img = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("img", img.SelectorText);
            Assert.AreEqual(0, img.Style.Length);
        }

        [TestMethod]
        public void CssSheetIgnoreInvalidValueBorderWidth()
        {
            var sheet = CssParser.ParseStyleSheet(@"img { border-width: 3 }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var img = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("img", img.SelectorText);
            Assert.AreEqual(0, img.Style.Length);
        }

        [TestMethod]
        public void CssSheetWellformedDeclaration()
        {
            var sheet = CssParser.ParseStyleSheet(@"p { color:green; }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("green", p.Style.Color);
        }

        [TestMethod]
        public void CssSheetMalformedDeclarationMissingColon()
        {
            var sheet = CssParser.ParseStyleSheet(@"p { color:green; color }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("green", p.Style.Color);
        }

        [TestMethod]
        public void CssSheetMalformedDeclarationMissingColonWithRecovery()
        {
            var sheet = CssParser.ParseStyleSheet(@"p { color:red;   color; color:green }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("green", p.Style.Color);
        }

        [TestMethod]
        public void CssSheetMalformedDeclarationMissingValue()
        {
            var sheet = CssParser.ParseStyleSheet(@"p { color:green; color: }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("green", p.Style.Color);
        }

        [TestMethod]
        public void CssSheetMalformedDeclarationUnexpectedTokens()
        {
            var sheet = CssParser.ParseStyleSheet(@"p { color:green; color{;color:maroon} }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("green", p.Style.Color);
        }

        [TestMethod]
        public void CssSheetMalformedDeclarationUnexpectedTokensWithRecovery()
        {
            var sheet = CssParser.ParseStyleSheet(@"p { color:red;   color{;color:maroon}; color:green }");
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOfType(sheet.Rules[0], typeof(CSSStyleRule));
            var p = sheet.Rules[0] as ICssStyleRule;
            Assert.AreEqual("p", p.SelectorText);
            Assert.AreEqual(1, p.Style.Length);
            Assert.AreEqual("color", p.Style[0]);
            Assert.AreEqual("green", p.Style.Color);
        }

        [TestMethod]
        public void CssCreateValueListConformal()
        {
            var valueString = "24px 12px 6px";
            var list = CssParser.ParseValueList(valueString);
            Assert.AreEqual(3, list.Length);
            Assert.AreEqual(list[0].CssText, "24px");
            Assert.AreEqual(list[1].CssText, "12px");
            Assert.AreEqual(list[2].CssText, "6px");
        }

        [TestMethod]
        public void CssCreateValueListNonConformal()
        {
            var valueString = "  24px  12px 6px  13px ";
            var list = CssParser.ParseValueList(valueString);
            Assert.AreEqual(4, list.Length);
            Assert.AreEqual(list[0].CssText, "24px");
            Assert.AreEqual(list[1].CssText, "12px");
            Assert.AreEqual(list[2].CssText, "6px");
            Assert.AreEqual(list[3].CssText, "13px");
        }

        [TestMethod]
        public void CssCreateValueListEmpty()
        {
            var valueString = "";
            var list = CssParser.ParseValueList(valueString);
            Assert.AreEqual(0, list.Length);
        }

        [TestMethod]
        public void CssCreateValueListSpaces()
        {
            var valueString = "  ";
            var list = CssParser.ParseValueList(valueString);
            Assert.AreEqual(0, list.Length);
        }

        [TestMethod]
        public void CssCreateValueListIllegal()
        {
            var valueString = " , ";
            var list = CssParser.ParseValueList(valueString);
            Assert.AreEqual(0, list.Length);
        }

        [TestMethod]
        public void CssCreateMultipleValues()
        {
            var valueString = "Arial, Verdana, Helvetica, Sans-Serif";
            var list = CssParser.ParseMultipleValues(valueString);
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("arial", list[0].CssText);
            Assert.AreEqual("verdana", list[1].CssText);
            Assert.AreEqual("helvetica", list[2].CssText);
            Assert.AreEqual("sans-serif", list[3].CssText);
        }

        [TestMethod]
        public void CssCreateMultipleValueLists()
        {
            var valueString = "Arial 10pt bold, Verdana 12pt italic";
            var list = CssParser.ParseMultipleValues(valueString);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("arial 10pt bold", list[0].CssText);
            Assert.AreEqual("verdana 12pt italic", list[1].CssText);
            Assert.AreEqual(CssValueType.List, list[0].Type);
            Assert.AreEqual(CssValueType.List, list[1].Type);
            Assert.AreEqual(3, ((CssValueList)list[0]).Length);
            Assert.AreEqual(3, ((CssValueList)list[1]).Length);
        }

        [TestMethod]
        public void CssCreateMultipleValuesNonConformal()
        {
            var valueString = "  Arial  ,  Verdana  ,Helvetica,Sans-Serif   ";
            var list = CssParser.ParseMultipleValues(valueString);
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("arial", list[0].CssText);
            Assert.AreEqual("verdana", list[1].CssText);
            Assert.AreEqual("helvetica", list[2].CssText);
            Assert.AreEqual("sans-serif", list[3].CssText);
        }

        [TestMethod]
        public void CssCreateMultipleValuesEmpty()
        {
            var valueString = "";
            var list = CssParser.ParseMultipleValues(valueString);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void CssCreateMultipleValuesSpaces()
        {
            var valueString = "  ";
            var list = CssParser.ParseMultipleValues(valueString);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void CssCreateMultipleValuesIllegal()
        {
            var valueString = " , ";
            var list = CssParser.ParseMultipleValues(valueString);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void CssColorBlack()
        {
            var valueString = "#000000";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.Primitive, value.Type);
            var color = (Color)value;
            Assert.AreEqual(new Color(0, 0, 0), color);
        }

        [TestMethod]
        public void CssColorRed()
        {
            var valueString = "#FF0000";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.Primitive, value.Type);
            var color = (Color)value;
            Assert.AreEqual(new Color(255, 0, 0), color);
        }

        [TestMethod]
        public void CssColorMixedShort()
        {
            var valueString = "#07C";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.Primitive, value.Type);
            var color = (Color)value;
            Assert.AreEqual(new Color(0, 119, 204), color);
        }

        [TestMethod]
        public void CssColorGreenShort()
        {
            var valueString = "#00F";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.Primitive, value.Type);
            var color = (Color)value;
            Assert.AreEqual(new Color(0, 0, 255), color);
        }

        [TestMethod]
        public void CssColorRedShort()
        {
            var valueString = "#F00";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.Primitive, value.Type);
            var color = (Color)value;
            Assert.AreEqual(new Color(255, 0, 0), color);
        }

        [TestMethod]
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

                var property = (CSSBorderPartColorProperty)decl;
                var color = property.Color;
                Assert.AreEqual(new Color(82, 168, 236, 0.8f), color);
            }
        }

        [TestMethod]
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

        [TestMethod]
        public void CssSeveralFontFamily()
        {
            var decl = CssParser.ParseDeclarations("font-family: \"Helvetica Neue\", Helvetica, Arial, sans-serif");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var prop = decl.GetProperty("font-family");
            Assert.AreEqual("font-family", prop.Name);
            Assert.IsFalse(prop.IsImportant);
            Assert.AreEqual(CssValueType.List, prop.Value.Type);

            var value = prop.Value as CssValueList;
            Assert.AreEqual(7, value.Length);
            Assert.AreEqual("\"Helvetica Neue\", helvetica, arial, sans-serif", value.CssText);
        }

        [TestMethod]
        public void CssFontWithSlashAndContent()
        {
            var decl = CssParser.ParseDeclarations("font: bold 1em/2em monospace; content: \" (\" attr(href) \")\"");
            Assert.IsNotNull(decl);
            Assert.AreEqual(8, decl.Length);

            Assert.AreEqual("bold 1em / 2em monospace", decl.GetPropertyValue("font"));

            var content = decl.GetProperty("content");
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.IsImportant);
            Assert.AreEqual(CssValueType.List, content.Value.Type);
            Assert.AreEqual("\" (\" attr(href) \")\"", content.Value.CssText);
        }

        [TestMethod]
        public void CssBackgroundWebkitGradient()
        {
            var background = CssParser.ParseDeclaration("background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #FFA84C), color-stop(100%, #FF7B0D))");
            Assert.IsNotNull(background);
            Assert.AreEqual("background", background.Name);
            Assert.IsFalse(background.IsImportant);
            Assert.AreEqual(CssValueType.Initial, background.Value.Type);
            Assert.IsFalse(background.HasValue);
        }

        [TestMethod]
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

        [TestMethod]
        public void CssFontWithFraction()
        {
            var font = CssParser.ParseDeclaration("font:bold 40px/1.13 'PT Sans Narrow', sans-serif");
            Assert.AreEqual("font", font.Name);
            Assert.IsFalse(font.IsImportant);
            Assert.AreEqual(CssValueType.List, font.Value.Type);
        }

        [TestMethod]
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

        [TestMethod]
        public void CssBackgroundWithImage()
        {
            var background = CssParser.ParseDeclaration("background:url(../images/ribbon.svg) no-repeat");
            Assert.AreEqual("background", background.Name);
            Assert.IsFalse(background.IsImportant);
            Assert.AreEqual(CssValueType.List, background.Value.Type);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void CssImportSheet()
        {
            var rule = "@import url(fonts.css);";
            var decl = CssParser.ParseRule(rule);
            Assert.IsNotNull(decl);
            Assert.IsInstanceOfType(decl, typeof(CSSImportRule));
            var importRule = (CSSImportRule)decl;
            Assert.AreEqual("fonts.css", importRule.Href);
        }

        [TestMethod]
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

        [TestMethod]
        public void CssContentCounter()
        {
            var decl = CssParser.ParseDeclarations("content:counter(list)'.'");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var content = decl.GetProperty("content");
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.IsImportant);
            Assert.AreEqual(CssValueType.List, content.Value.Type);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void CssSheetWithDataUrlAsBackgroundImage()
        {
            var sheet = CssParser.ParseStyleSheet(".App_Header_ .logo { background-image: url(\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEcAAAAcCAMAAAAEJ1IZAAAABGdBTUEAALGPC/xhBQAAVAI/VAI/VAI/VAI/VAI/VAI/VAAAA////AI/VRZ0U8AAAAFJ0Uk5TYNV4S2UbgT/Gk6uQt585w2wGXS0zJO2lhGttJK6j4YqZSobH1AAAAAElFTkSuQmCC\"); background-size: 71px 28px; background-position: 0 19px; width: 71px; }");
            Assert.IsNotNull(sheet);
            Assert.AreEqual(1, sheet.Rules.Length);
            var rule = sheet.Rules[0] as CSSStyleRule;
            Assert.IsNotNull(rule);
            Assert.AreEqual(4, rule.Style.Length);
            Assert.AreEqual(".App_Header_ .logo", rule.SelectorText);
            var decl = rule.Style as ICssStyleDeclaration;
            Assert.AreEqual("url(\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEcAAAAcCAMAAAAEJ1IZAAAABGdBTUEAALGPC/xhBQAAVAI/VAI/VAI/VAI/VAI/VAI/VAAAA////AI/VRZ0U8AAAAFJ0Uk5TYNV4S2UbgT/Gk6uQt585w2wGXS0zJO2lhGttJK6j4YqZSobH1AAAAAElFTkSuQmCC\")", decl.BackgroundImage);
            Assert.AreEqual("71px 28px", decl.BackgroundSize);
            Assert.AreEqual("0 19px", decl.BackgroundPosition);
            Assert.AreEqual("71px", decl.Width);
        }

        [TestMethod]
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
                var sheet = DocumentBuilder.Css(memoryStream);
            }
        }

        [TestMethod]
        public void CssSheetFromStreamOnlyZerosAvailable()
        {
            var bs = new Byte[7180];

            using (var memoryStream = new MemoryStream(bs, false))
            {
                var sheet = DocumentBuilder.Css(memoryStream);
                Assert.IsNotNull(sheet);
                Assert.AreEqual(0, sheet.Rules.Length);
            }
        }

        [TestMethod]
        public void CssSheetFromStringWithQuestionMarksLeadingToInfiniteLoop()
        {
            var sheet = DocumentBuilder.Css("U+???\0");
            Assert.IsNotNull(sheet);
            Assert.AreEqual(0, sheet.Rules.Length);
        }
    }
}
