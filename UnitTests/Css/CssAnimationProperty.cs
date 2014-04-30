using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp.Parser.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.DOM.Css;

namespace UnitTests.Css
{
    [TestClass]
    public class CssAnimationPropertyTests
    {
        [TestMethod]
        public void CssAnimationDurationMillisecondsLegal()
        {
            var snippet = "animation-duration : 60ms";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-duration", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDurationProperty));
            var concrete = (CSSAnimationDurationProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("60ms", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationDurationMultipleSecondsLegal()
        {
            var snippet = "animation-duration : 1s  , 2s  , 3s  , 4s";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-duration", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDurationProperty));
            var concrete = (CSSAnimationDurationProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1s, 2s, 3s, 4s", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationDelayMillisecondsLegal()
        {
            var snippet = "animation-delay : 0ms";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-delay", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDelayProperty));
            var concrete = (CSSAnimationDelayProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0ms", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationDelayZeroZeroSecondMillisecondsLegal()
        {
            var snippet = "animation-delay : 0s  , 0s  , 1s  , 20ms";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-delay", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDelayProperty));
            var concrete = (CSSAnimationDelayProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0s, 0s, 1s, 20ms", concrete.Value.CssText);
        }
    }
}
