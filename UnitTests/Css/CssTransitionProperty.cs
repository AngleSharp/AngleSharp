using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp.Parser.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.DOM.Css;

namespace UnitTests.Css
{
    [TestClass]
    public class CssTransitionPropertyTests
    {
        [TestMethod]
        public void CssTransitionPropertyNoneLegal()
        {
            var snippet = "transition-property : none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionPropertyProperty));
            var concrete = (CSSTransitionPropertyProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionPropertyAllLegal()
        {
            var snippet = "transition-property : ALL";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionPropertyProperty));
            var concrete = (CSSTransitionPropertyProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("ALL", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionPropertyWidthHeightLegal()
        {
            var snippet = "transition-property : width   , height";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionPropertyProperty));
            var concrete = (CSSTransitionPropertyProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("width, height", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionPropertyDashSpecificLegal()
        {
            var snippet = "transition-property : -specific";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionPropertyProperty));
            var concrete = (CSSTransitionPropertyProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("-specific", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionPropertySlidingVerticallyLegal()
        {
            var snippet = "transition-property : sliding-vertically";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionPropertyProperty));
            var concrete = (CSSTransitionPropertyProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("sliding-vertically", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionPropertyTest05Legal()
        {
            var snippet = "transition-property : test_05";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionPropertyProperty));
            var concrete = (CSSTransitionPropertyProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("test_05", concrete.Value.CssText);
        }
    }
}
