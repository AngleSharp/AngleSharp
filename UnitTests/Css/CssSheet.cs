using AngleSharp;
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
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(list[0].CssText, "Arial");
            Assert.AreEqual(list[1].CssText, "Verdana");
            Assert.AreEqual(list[2].CssText, "Helvetica");
            Assert.AreEqual(list[3].CssText, "Sans-Serif");
        }

        [TestMethod]
        public void CssCreateMultipleValueLists()
        {
            var valueString = "Arial 10pt bold, Verdana 12pt italic";
            var list = CssParser.ParseMultipleValues(valueString);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(list[0].CssText, "Arial 10pt bold");
            Assert.AreEqual(list[1].CssText, "Verdana 12pt italic");
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
            Assert.AreEqual(list[0].CssText, "Arial");
            Assert.AreEqual(list[1].CssText, "Verdana");
            Assert.AreEqual(list[2].CssText, "Helvetica");
            Assert.AreEqual(list[3].CssText, "Sans-Serif");
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
    }
}
