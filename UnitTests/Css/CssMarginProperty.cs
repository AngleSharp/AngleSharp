using AngleSharp.DOM.Css;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Css
{
    [TestClass]
    public class CssMarginPropertyTests
    {
        [TestMethod]
        public void CssMarginLeftLengthLegal()
        {
            var snippet = "margin-left: 15px ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin-left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginLeftProperty));
            var concrete = (CSSMarginLeftProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginLeftInitialLegal()
        {
            var snippet = "margin-left: initial ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin-left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginLeftProperty));
            var concrete = (CSSMarginLeftProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
            Assert.AreEqual("initial", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginRightLengthImportantLegal()
        {
            var snippet = "margin-right: 3em!important";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin-right", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginRightProperty));
            var concrete = (CSSMarginRightProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3em", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginRightPercentLegal()
        {
            var snippet = "margin-right: 10%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin-right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginRightProperty));
            var concrete = (CSSMarginRightProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10%", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginTopPercentLegal()
        {
            var snippet = "margin-top: 4% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin-top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginTopProperty));
            var concrete = (CSSMarginTopProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("4%", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginBottomZeroLegal()
        {
            var snippet = "margin-bottom: 0 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginBottomProperty));
            var concrete = (CSSMarginBottomProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginBottomNegativeLegal()
        {
            var snippet = "margin-bottom: -3px ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginBottomProperty));
            var concrete = (CSSMarginBottomProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("-3px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginBottomAutoLegal()
        {
            var snippet = "margin-bottom: auto ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginBottomProperty));
            var concrete = (CSSMarginBottomProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginAllZeroLegal()
        {
            var snippet = "margin: 0 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginProperty));
            var concrete = (CSSMarginProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginAllPercentLegal()
        {
            var snippet = "margin: 25% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginProperty));
            var concrete = (CSSMarginProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("25%", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginSidesLengthLegal()
        {
            var snippet = "margin: 10px 3em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginProperty));
            var concrete = (CSSMarginProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 3em", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginSidesLengthAndAutoLegal()
        {
            var snippet = "margin: 10px auto ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginProperty));
            var concrete = (CSSMarginProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px auto", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginAutoLegal()
        {
            var snippet = "margin: auto ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginProperty));
            var concrete = (CSSMarginProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginThreeValuesLegal()
        {
            var snippet = "margin: 10px 3em 5px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginProperty));
            var concrete = (CSSMarginProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 3em 5px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginAllValuesWithPercentAndAutoLegal()
        {
            var snippet = "margin: 10px 5% auto 2% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginProperty));
            var concrete = (CSSMarginProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 5% auto 2%", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMarginTooManyValuesIllegal()
        {
            var snippet = "margin: 10px 5% 8px 2% 3px auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMarginProperty));
            var concrete = (CSSMarginProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }
    }
}
