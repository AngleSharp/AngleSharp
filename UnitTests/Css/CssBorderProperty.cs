using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp.Parser.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.DOM.Css;

namespace UnitTests.Css
{
    [TestClass]
    public class CssBorderPropertyTest
    {
        [TestMethod]
        public void CssBorderBottomLeftRadiusPxPxLegal()
        {
            var snippet = "border-bottom-left-radius: 40px  40px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBorderBottomLeftRadiusProperty));
            var concrete = (CSSBorderBottomLeftRadiusProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("40px 40px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderBottomLeftRadiusPxEmLegal()
        {
            var snippet = "border-bottom-left-radius  : 40px 20em";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBorderBottomLeftRadiusProperty));
            var concrete = (CSSBorderBottomLeftRadiusProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("40px 20em", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderBottomLeftRadiusPxPercentLegal()
        {
            var snippet = "border-bottom-left-radius: 10px 5%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBorderBottomLeftRadiusProperty));
            var concrete = (CSSBorderBottomLeftRadiusProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 5%", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderBottomLeftRadiusPercentLegal()
        {
            var snippet = "border-bottom-left-radius: 10%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBorderBottomLeftRadiusProperty));
            var concrete = (CSSBorderBottomLeftRadiusProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10%", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderBottomRightRadiusZeroLegal()
        {
            var snippet = "border-bottom-right-radius: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-right-radius", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBorderBottomRightRadiusProperty));
            var concrete = (CSSBorderBottomRightRadiusProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderBottomRightRadiusPxLegal()
        {
            var snippet = "border-bottom-right-radius: 20px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-right-radius", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBorderBottomRightRadiusProperty));
            var concrete = (CSSBorderBottomRightRadiusProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("20px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderTopLeftRadiusCmLegal()
        {
            var snippet = "border-top-left-radius: 3.5cm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-top-left-radius", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBorderTopLeftRadiusProperty));
            var concrete = (CSSBorderTopLeftRadiusProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3.5cm", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderTopRightRadiusPercentPercentLegal()
        {
            var snippet = "border-top-right-radius: 15% 3.5%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-top-right-radius", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBorderTopRightRadiusProperty));
            var concrete = (CSSBorderTopRightRadiusProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15% 3.5%", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderRadiusPercentPercentLegal()
        {
            var snippet = "border-radius: 15% 3.5%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBorderRadiusProperty));
            var concrete = (CSSBorderRadiusProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15% 3.5%", concrete.Value.CssText);
        }
    }
}
