using AngleSharp.Css;
using AngleSharp.DOM.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CssSheet
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
            Assert.AreEqual(4, list.Length);
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
            Assert.AreEqual(2, list.Length);
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
			Assert.AreEqual(4, list.Length);
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
			Assert.AreEqual(0, list.Length);
        }

        [TestMethod]
        public void CssCreateMultipleValuesSpaces()
        {
            var valueString = "  ";
            var list = CssParser.ParseMultipleValues(valueString);
			Assert.AreEqual(0, list.Length);
        }

        [TestMethod]
        public void CssCreateMultipleValuesIllegal()
        {
            var valueString = " , ";
            var list = CssParser.ParseMultipleValues(valueString);
			Assert.AreEqual(0, list.Length);
        }

        [TestMethod]
        public void CssColorBlack()
        {
            var valueString = "#000000";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.PrimitiveValue, value.CssValueType);
            var color = ((CSSPrimitiveValue)value).GetRGBColorValue();
            Assert.IsTrue(color.HasValue);
            Assert.AreEqual(new CSSColor(0, 0, 0), color.Value);
        }

        [TestMethod]
        public void CssColorRed()
        {
            var valueString = "#FF0000";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.PrimitiveValue, value.CssValueType);
            var color = ((CSSPrimitiveValue)value).GetRGBColorValue();
            Assert.IsTrue(color.HasValue);
            Assert.AreEqual(new CSSColor(255, 0, 0), color.Value);
        }

        [TestMethod]
        public void CssColorMixedShort()
        {
            var valueString = "#07C";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.PrimitiveValue, value.CssValueType);
            var color = ((CSSPrimitiveValue)value).GetRGBColorValue();
            Assert.IsTrue(color.HasValue);
            Assert.AreEqual(new CSSColor(0, 119, 204), color.Value);
        }

        [TestMethod]
        public void CssColorGreenShort()
        {
            var valueString = "#00F";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.PrimitiveValue, value.CssValueType);
            var color = ((CSSPrimitiveValue)value).GetRGBColorValue();
            Assert.IsTrue(color.HasValue);
            Assert.AreEqual(new CSSColor(0, 0, 255), color.Value);
        }

        [TestMethod]
        public void CssColorRedShort()
        {
            var valueString = "#F00";
            var value = CssParser.ParseValue(valueString);
            Assert.IsNotNull(value);
            Assert.AreEqual(CssValueType.PrimitiveValue, value.CssValueType);
            var color = ((CSSPrimitiveValue)value).GetRGBColorValue();
            Assert.IsTrue(color.HasValue);
            Assert.AreEqual(new CSSColor(255, 0, 0), color.Value);
        }

        [TestMethod]
        public void CssRgbaFunction()
        {
            var decl = CssParser.ParseDeclarations("border-color: rgba(82, 168, 236, 0.8)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(decl.List.Count, 1);

            var prop = decl.List[0];
            Assert.AreEqual("border-color", prop.Name);
            Assert.IsFalse(prop.Important);
            Assert.AreEqual(CssValueType.PrimitiveValue, prop.Value.CssValueType);

            var color = ((CSSPrimitiveValue)prop.Value).GetRGBColorValue();
            Assert.IsTrue(color.HasValue);
            Assert.AreEqual(new CSSColor(82, 168, 236, 0.8f), color.Value);
        }

        [TestMethod]
        public void CssMarginAll()
        {
            var decl = CssParser.ParseDeclarations("margin: 20px;");
            Assert.IsNotNull(decl);
            Assert.AreEqual(decl.List.Count, 1);

            var prop = decl.List[0];
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
            Assert.AreEqual(decl.List.Count, 1);

            var prop = decl.List[0];
            Assert.AreEqual("font-family", prop.Name);
            Assert.IsFalse(prop.Important);
            Assert.AreEqual(CssValueType.Custom, prop.Value.CssValueType);

            var value = prop.Value as CSSValuePool;
            Assert.AreEqual(4, value.Length);
            Assert.AreEqual("'Helvetica Neue',Helvetica,Arial,sans-serif", value.ToCss());
        }

        [TestMethod]
        public void CssFontWithSlashAndContent()
        {
            var decl = CssParser.ParseDeclarations("font: bold 1em/2em monospace; content: \" (\" attr(href) \")\"");
            Assert.IsNotNull(decl);
            Assert.AreEqual(decl.List.Count, 2);

            var font = decl.List[0];
            Assert.AreEqual("font", font.Name);
            Assert.IsFalse(font.Important);
            Assert.AreEqual(CssValueType.ValueList, font.Value.CssValueType);
            Assert.AreEqual("bold 1em/2em monospace", font.Value.ToCss());

            var content = decl.List[1];
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
            Assert.AreEqual(decl.List.Count, 1);

            var background = decl.List[0];
            Assert.AreEqual("background", background.Name);
            Assert.IsFalse(background.Important);
            Assert.AreEqual(CssValueType.Custom, background.Value.CssValueType);
            Assert.AreEqual("-webkit-gradient(linear, left top, left bottom, color-stop(0%, #FFA84C), color-stop(100%, #FF7B0D))", background.Value.ToCss());
        }

        [TestMethod]
        public void CssFontWithFraction()
        {
            var decl = CssParser.ParseDeclarations("font:bold 40px/1.13 'PT Sans Narrow', sans-serif");
            Assert.IsNotNull(decl);
            Assert.AreEqual(decl.List.Count, 1);

            var font = decl.List[0];
            Assert.AreEqual("font", font.Name);
        }

        [TestMethod]
        public void CssTextShadow()
        {
            var decl = CssParser.ParseDeclarations("text-shadow: 0 0 10px #000");
            Assert.IsNotNull(decl);
            Assert.AreEqual(decl.List.Count, 1);

            var textShadow = decl.List[0];
            Assert.AreEqual("text-shadow", textShadow.Name);
        }

        [TestMethod]
        public void CssBackgroundWithImage()
        {
            var decl = CssParser.ParseDeclarations("background:url(../images/ribbon.svg) no-repeat");
            Assert.IsNotNull(decl);
            Assert.AreEqual(decl.List.Count, 1);

            var background = decl.List[0];
            Assert.AreEqual("background", background.Name);
        }

        [TestMethod]
        public void CssContentWithCounter()
        {
            var decl = CssParser.ParseDeclarations("content:counter(paging, decimal-leading-zero)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(decl.List.Count, 1);

            var content = decl.List[0];
            Assert.AreEqual("content", content.Name);
        }

        [TestMethod]
        public void CssBackgroundColor()
        {
            var decl = CssParser.ParseDeclarations("background-color: rgb(245, 0, 111)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(decl.List.Count, 1);

            var backgroundColor = decl.List[0];
            Assert.AreEqual("background-color", backgroundColor.Name);
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
            Assert.AreEqual(decl.List.Count, 1);

            var content = decl.List[0];
            Assert.AreEqual("content", content.Name);
        }

        [TestMethod]
        public void CssContentCounter()
        {
            var decl = CssParser.ParseDeclarations("content:counter(list)'.'");
            Assert.IsNotNull(decl);
            Assert.AreEqual(decl.List.Count, 1);

            var content = decl.List[0];
            Assert.AreEqual("content", content.Name);
        }

        [TestMethod]
        public void CssTransformTranslate()
        {
            var decl = CssParser.ParseDeclarations("transform:translateY(-50%)");
            Assert.IsNotNull(decl);
            Assert.AreEqual(decl.List.Count, 1);

            var transform = decl.List[0];
            Assert.AreEqual("transform", transform.Name);
        }

        [TestMethod]
        public void CssBoxShadowMultiline()
        {
            var decl = CssParser.ParseDeclarations(@"
        box-shadow:
			0 0 0 10px rgba(60, 61, 64, 0.6),
			0 0 50px #3C3D40;");
            Assert.IsNotNull(decl);
            Assert.AreEqual(decl.List.Count, 1);

            var boxShadow = decl.List[0];
            Assert.AreEqual("box-shadow", boxShadow.Name);
        }
    }
}
