using AngleSharp.DOM.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Css
{
    [TestClass]
    public class CssBorderImagePropertyTests
    {
        [TestMethod]
        public void CssBorderImageSourceNoneLegal()
        {
            var snippet = "border-image-source: none    ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-source", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBorderImageSourceProperty));
            var concrete = (CSSBorderImageSourceProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderImageSourceUrlLegal()
        {
            var snippet = "border-image-source: url(image.jpg)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-source", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBorderImageSourceProperty));
            var concrete = (CSSBorderImageSourceProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url('image.jpg')", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderImageSourceLinearGradientLegal()
        {
            var snippet = "border-image-source: linear-gradient(to top, red, yellow)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-source", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBorderImageSourceProperty));
            var concrete = (CSSBorderImageSourceProperty)property;
            Assert.AreEqual(CssValueType.Custom, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("linear-gradient(0deg, rgba(255, 0, 0, 1) 0%, rgba(255, 255, 0, 1) 100%)", concrete.Value.CssText);
        }
    }
}
