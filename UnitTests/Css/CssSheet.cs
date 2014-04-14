using AngleSharp.DOM;
using AngleSharp.DOM.Css;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CssSheetTests
    {
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
            Assert.AreEqual("Arial", list[0].CssText);
            Assert.AreEqual("Verdana", list[1].CssText);
            Assert.AreEqual("Helvetica", list[2].CssText);
            Assert.AreEqual("Sans-Serif", list[3].CssText);
        }

        [TestMethod]
        public void CssCreateMultipleValueLists()
        {
            var valueString = "Arial 10pt bold, Verdana 12pt italic";
            var list = CssParser.ParseMultipleValues(valueString);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("Arial 10pt bold", list[0].CssText);
            Assert.AreEqual("Verdana 12pt italic", list[1].CssText);
            Assert.AreEqual(CssValueType.ValueList, list[0].CssValueType);
            Assert.AreEqual(CssValueType.ValueList, list[1].CssValueType);
            Assert.AreEqual(3, ((CSSValueList)list[0]).Length);
            Assert.AreEqual(3, ((CSSValueList)list[1]).Length);
        }

        [TestMethod]
        public void CssCreateMultipleValuesNonConformal()
        {
            var valueString = "  Arial  ,  Verdana  ,Helvetica,Sans-Serif   ";
            var list = CssParser.ParseMultipleValues(valueString);
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("Arial", list[0].CssText);
            Assert.AreEqual("Verdana", list[1].CssText);
            Assert.AreEqual("Helvetica", list[2].CssText);
            Assert.AreEqual("Sans-Serif", list[3].CssText);
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
            Assert.AreEqual(CssValueType.PrimitiveValue, value.CssValueType);
            var color = ((CSSPrimitiveValue<Color>)value).Value;
            Assert.AreEqual(new Color(0, 0, 0), color);
        }

        [TestMethod]
        public void CssColorRed()
        {
            var valueString = "#FF0000";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.PrimitiveValue, value.CssValueType);
            var color = ((CSSPrimitiveValue<Color>)value).Value;
            Assert.AreEqual(new Color(255, 0, 0), color);
        }

        [TestMethod]
        public void CssColorMixedShort()
        {
            var valueString = "#07C";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.PrimitiveValue, value.CssValueType);
            var color = ((CSSPrimitiveValue<Color>)value).Value;
            Assert.AreEqual(new Color(0, 119, 204), color);
        }

        [TestMethod]
        public void CssColorGreenShort()
        {
            var valueString = "#00F";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.PrimitiveValue, value.CssValueType);
            var color = ((CSSPrimitiveValue<Color>)value).Value;
            Assert.AreEqual(new Color(0, 0, 255), color);
        }

        [TestMethod]
        public void CssColorRedShort()
        {
            var valueString = "#F00";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.PrimitiveValue, value.CssValueType);
            var color = ((CSSPrimitiveValue<Color>)value).Value;
            Assert.AreEqual(new Color(255, 0, 0), color);
        }

        [TestMethod]
        public void CssRgbaFunction()
        {
            var decl = CssParser.ParseDeclarations("border-color: rgba(82, 168, 236, 0.8)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var prop = decl.Get(0);
            Assert.AreEqual("border-color", prop.Name);
            Assert.IsFalse(prop.Important);
            Assert.AreEqual(CssValueType.PrimitiveValue, prop.Value.CssValueType);

            var color = ((CSSPrimitiveValue<Color>)prop.Value).Value;
            Assert.AreEqual(new Color(82, 168, 236, 0.8f), color);
        }

        [TestMethod]
        public void CssMarginAll()
        {
            var decl = CssParser.ParseDeclarations("margin: 20px;");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var prop = decl.Get(0);
            Assert.AreEqual("margin", prop.Name);
            Assert.IsFalse(prop.Important);
            Assert.AreEqual(CssValueType.PrimitiveValue, prop.Value.CssValueType);
            Assert.AreEqual("20px", prop.Value.ToCss());
        }

        [TestMethod]
        public void CssSeveralFontFamily()
        {
            var decl = CssParser.ParseDeclarations("font-family: \"Helvetica Neue\", Helvetica, Arial, sans-serif");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var prop = decl.Get(0);
            Assert.AreEqual("font-family", prop.Name);
            Assert.IsFalse(prop.Important);
            Assert.AreEqual(CssValueType.ValueList, prop.Value.CssValueType);

            var value = prop.Value as CSSValueList;
            Assert.AreEqual(7, value.Length);
            Assert.AreEqual("'Helvetica Neue', Helvetica, Arial, sans-serif", value.ToCss());
        }

        [TestMethod]
        public void CssFontWithSlashAndContent()
        {
            var decl = CssParser.ParseDeclarations("font: bold 1em/2em monospace; content: \" (\" attr(href) \")\"");
            Assert.IsNotNull(decl);
            Assert.AreEqual(2, decl.Length);

            var font = decl.Get(0);
            Assert.AreEqual("font", font.Name);
            Assert.IsFalse(font.Important);
            Assert.AreEqual(CssValueType.ValueList, font.Value.CssValueType);
            Assert.AreEqual("bold 1em / 2em monospace", font.Value.ToCss());

            var content = decl.Get(1);
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.Important);
            Assert.AreEqual(CssValueType.ValueList, content.Value.CssValueType);
            Assert.AreEqual("' (' attr(href) ')'", content.Value.ToCss());
        }

        [TestMethod]
        public void CssBackgroundWebkitGradient()
        {
            var decl = CssParser.ParseDeclarations("background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #FFA84C), color-stop(100%, #FF7B0D))");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var background = decl.Get(0);
            Assert.AreEqual("background", background.Name);
            Assert.IsFalse(background.Important);
            Assert.AreEqual(CssValueType.Inherit, background.Value.CssValueType);
            Assert.IsFalse(background.HasValue);
        }

        [TestMethod]
        public void CssBackgroundColorRgba()
        {
            var decl = CssParser.ParseDeclarations("background: rgba(255, 123, 13, 1)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var background = decl.Get(0);
            Assert.AreEqual("background", background.Name);
            Assert.IsFalse(background.Important);
            Assert.AreEqual(CssValueType.PrimitiveValue, background.Value.CssValueType);
            Assert.AreEqual("rgba(255, 123, 13, 1)", background.Value.ToCss());
        }

        [TestMethod]
        public void CssFontWithFraction()
        {
            var decl = CssParser.ParseDeclarations("font:bold 40px/1.13 'PT Sans Narrow', sans-serif");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var font = decl.Get(0);
            Assert.AreEqual("font", font.Name);
            Assert.IsFalse(font.Important);
            Assert.AreEqual(CssValueType.ValueList, font.Value.CssValueType);
        }

        [TestMethod]
        public void CssTextShadow()
        {
            var decl = CssParser.ParseDeclarations("text-shadow: 0 0 10px #000");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var textShadow = decl.Get(0);
            Assert.AreEqual("text-shadow", textShadow.Name);
            Assert.IsFalse(textShadow.Important);
            Assert.AreEqual(CssValueType.ValueList, textShadow.Value.CssValueType);
        }

        [TestMethod]
        public void CssBackgroundWithImage()
        {
            var decl = CssParser.ParseDeclarations("background:url(../images/ribbon.svg) no-repeat");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var background = decl.Get(0);
            Assert.AreEqual("background", background.Name);
            Assert.IsFalse(background.Important);
            Assert.AreEqual(CssValueType.ValueList, background.Value.CssValueType);
        }

        [TestMethod]
        public void CssContentWithCounter()
        {
            var decl = CssParser.ParseDeclarations("content:counter(paging, decimal-leading-zero)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var content = decl.Get(0);
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.Important);
            Assert.AreEqual(CssValueType.Custom, content.Value.CssValueType);
        }

        [TestMethod]
        public void CssBackgroundColorRgb()
        {
            var decl = CssParser.ParseDeclarations("background-color: rgb(245, 0, 111)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var backgroundColor = decl.Get(0);
            Assert.AreEqual("background-color", backgroundColor.Name);
            Assert.IsFalse(backgroundColor.Important);
            Assert.AreEqual(CssValueType.PrimitiveValue, backgroundColor.Value.CssValueType);
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

            var content = decl.Get(0);
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.Important);
            Assert.AreEqual(CssValueType.PrimitiveValue, content.Value.CssValueType);
        }

        [TestMethod]
        public void CssContentCounter()
        {
            var decl = CssParser.ParseDeclarations("content:counter(list)'.'");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var content = decl.Get(0);
            Assert.AreEqual("content", content.Name);
            Assert.IsFalse(content.Important);
            Assert.AreEqual(CssValueType.ValueList, content.Value.CssValueType);
        }

        [TestMethod]
        public void CssTransformTranslate()
        {
            var decl = CssParser.ParseDeclarations("transform:translateY(-50%)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var transform = decl.Get(0);
            Assert.AreEqual("transform", transform.Name);
            Assert.IsFalse(transform.Important);
            Assert.AreEqual(CssValueType.Custom, transform.Value.CssValueType);
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

            var boxShadow = decl.Get(0);
            Assert.AreEqual("box-shadow", boxShadow.Name);
            Assert.IsFalse(boxShadow.Important);
            Assert.AreEqual(CssValueType.ValueList, boxShadow.Value.CssValueType);
        }

        [TestMethod]
        public void CssDisplayBlock()
        {
            var decl = CssParser.ParseDeclarations("display:block");
            Assert.IsNotNull(decl);
            Assert.AreEqual(1, decl.Length);

            var display = decl.Get(0);
            Assert.AreEqual("display", display.Name);
            Assert.IsFalse(display.Important);
            Assert.AreEqual(CssValueType.PrimitiveValue, display.Value.CssValueType);
            Assert.AreEqual("block", display.Value.CssText);
        }
    }
}
