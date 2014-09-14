using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp.Parser.Css;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Css.Properties;

namespace UnitTests.Css
{
    [TestClass]
    public class CssPaddingPropertyTests
    {
        [TestMethod]
        public void CssPaddingLeftLengthLegal()
        {
            var snippet = "padding-left: 15px ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding-left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSPaddingLeftProperty));
            var concrete = (CSSPaddingLeftProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssPaddingRightLengthImportantLegal()
        {
            var snippet = "padding-right: 3em!important";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding-right", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSPaddingRightProperty));
            var concrete = (CSSPaddingRightProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3em", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssPaddingTopPercentLegal()
        {
            var snippet = "padding-top: 4% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding-top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSPaddingTopProperty));
            var concrete = (CSSPaddingTopProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("4%", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssPaddingBottomZeroLegal()
        {
            var snippet = "padding-bottom: 0 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSPaddingBottomProperty));
            var concrete = (CSSPaddingBottomProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssPaddingAllZeroLegal()
        {
            var snippet = "padding: 0 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSPaddingProperty));
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssPaddingAllPercentLegal()
        {
            var snippet = "padding: 25% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSPaddingProperty));
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("25%", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssPaddingSidesLengthLegal()
        {
            var snippet = "padding: 10px 3em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSPaddingProperty));
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 3em", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssPaddingAutoIllegal()
        {
            var snippet = "padding: auto ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSPaddingProperty));
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssPaddingThreeValuesLegal()
        {
            var snippet = "padding: 10px 3em 5px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSPaddingProperty));
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 3em 5px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssPaddingAllValuesWithPercentLegal()
        {
            var snippet = "padding: 10px 5% 8px 2% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSPaddingProperty));
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 5% 8px 2%", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssPaddingTooManyValuesIllegal()
        {
            var snippet = "padding: 10px 5% 8px 2% 3px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSPaddingProperty));
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }
    }
}
