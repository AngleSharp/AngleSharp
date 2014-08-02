using AngleSharp;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Css
{
    [TestClass]
    public class CssTextPropertyTests
    {

        [TestMethod]
        public void CssWordSpacingZeroLengthLegal()
        {
            var snippet = "word-spacing: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWordSpacingProperty));
            var concrete = (CSSWordSpacingProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssWordSpacingLengthFloatRemLegal()
        {
            var snippet = "word-spacing: .3rem ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWordSpacingProperty));
            var concrete = (CSSWordSpacingProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3rem", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssWordSpacingLengthFloatEmLegal()
        {
            var snippet = "word-spacing: 0.3em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWordSpacingProperty));
            var concrete = (CSSWordSpacingProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3em", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssWordSpacingNormalLegal()
        {
            var snippet = "word-spacing: normal ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWordSpacingProperty));
            var concrete = (CSSWordSpacingProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTextAlignLegalJustify()
        {
            var snippet = "text-align:justify";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-align", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextAlignProperty));
            var concrete = (CSSTextAlignProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("justify", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
        }

        [TestMethod]
        public void CssTextAlignLegalJustifyChangedToLeftAndThenIllegal()
        {
            var snippet = "text-align:justify";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-align", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextAlignProperty));
            var concrete = (CSSTextAlignProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("justify", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
            concrete.Value = new CSSIdentifierValue("left");
            value = concrete.Value;
            Assert.AreEqual("left", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
            concrete.Value = new CSSIdentifierValue("whatever");
            Assert.AreEqual(value, concrete.Value);
        }

        [TestMethod]
        public void CssTextIndentLegalLength()
        {
            var snippet = "text-indent:3em";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextIndentProperty));
            var concrete = (CSSTextIndentProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("3em", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue<Length>));
        }

        [TestMethod]
        public void CssTextIndentLegalZero()
        {
            var snippet = "text-indent:0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextIndentProperty));
            var concrete = (CSSTextIndentProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("0", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue<Number>));
        }

        [TestMethod]
        public void CssTextIndentLegalPercent()
        {
            var snippet = "text-indent:10%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextIndentProperty));
            var concrete = (CSSTextIndentProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("10%", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue<Percent>));
        }

        [TestMethod]
        public void CssTextIndentIllegalNone()
        {
            var snippet = "text-indent:none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextIndentProperty));
            var concrete = (CSSTextIndentProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
        }
    }
}
