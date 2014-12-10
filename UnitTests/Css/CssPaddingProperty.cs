using AngleSharp.DOM.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;

namespace UnitTests.Css
{
    [TestFixture]
    public class CssPaddingPropertyTests
    {
        [Test]
        public void CssPaddingLeftLengthLegal()
        {
            var snippet = "padding-left: 15px ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding-left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPaddingLeftProperty>(property);
            var concrete = (CSSPaddingLeftProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15px", concrete.Value.CssText);
        }

        [Test]
        public void CssPaddingRightLengthImportantLegal()
        {
            var snippet = "padding-right: 3em!important";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding-right", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CSSPaddingRightProperty>(property);
            var concrete = (CSSPaddingRightProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3em", concrete.Value.CssText);
        }

        [Test]
        public void CssPaddingTopPercentLegal()
        {
            var snippet = "padding-top: 4% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding-top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPaddingTopProperty>(property);
            var concrete = (CSSPaddingTopProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("4%", concrete.Value.CssText);
        }

        [Test]
        public void CssPaddingBottomZeroLegal()
        {
            var snippet = "padding-bottom: 0 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPaddingBottomProperty>(property);
            var concrete = (CSSPaddingBottomProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [Test]
        public void CssPaddingAllZeroLegal()
        {
            var snippet = "padding: 0 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPaddingProperty>(property);
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [Test]
        public void CssPaddingAllPercentLegal()
        {
            var snippet = "padding: 25% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPaddingProperty>(property);
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("25%", concrete.Value.CssText);
        }

        [Test]
        public void CssPaddingSidesLengthLegal()
        {
            var snippet = "padding: 10px 3em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPaddingProperty>(property);
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 3em", concrete.Value.CssText);
        }

        [Test]
        public void CssPaddingAutoIllegal()
        {
            var snippet = "padding: auto ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPaddingProperty>(property);
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssPaddingThreeValuesLegal()
        {
            var snippet = "padding: 10px 3em 5px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPaddingProperty>(property);
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 3em 5px", concrete.Value.CssText);
        }

        [Test]
        public void CssPaddingAllValuesWithPercentLegal()
        {
            var snippet = "padding: 10px 5% 8px 2% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPaddingProperty>(property);
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 5% 8px 2%", concrete.Value.CssText);
        }

        [Test]
        public void CssPaddingTooManyValuesIllegal()
        {
            var snippet = "padding: 10px 5% 8px 2% 3px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPaddingProperty>(property);
            var concrete = (CSSPaddingProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }
    }
}
