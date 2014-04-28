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

        [TestMethod]
        public void CssTransitionTimingFunctionEaseLegal()
        {
            var snippet = "transition-timing-function : ease";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionTimingFunctionProperty));
            var concrete = (CSSTransitionTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("ease", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionTimingFunctionEaseInLegal()
        {
            var snippet = "transition-timing-function : ease-IN";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionTimingFunctionProperty));
            var concrete = (CSSTransitionTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("ease-IN", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionTimingFunctionStepStartLegal()
        {
            var snippet = "transition-timing-function : step-start";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionTimingFunctionProperty));
            var concrete = (CSSTransitionTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-start", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionTimingFunctionStepStartStepEndLegal()
        {
            var snippet = "transition-timing-function : step-start  , step-end";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionTimingFunctionProperty));
            var concrete = (CSSTransitionTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-start, step-end", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionTimingFunctionStepStartStepEndLinearEaseInOutLegal()
        {
            var snippet = "transition-timing-function : step-start  , step-end,linear,ease-IN-OUT";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionTimingFunctionProperty));
            var concrete = (CSSTransitionTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-start, step-end, linear, ease-IN-OUT", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionTimingFunctionCubicBezierLegal()
        {
            var snippet = "transition-timing-function : cubic-bezier(0, 1, 0.5, 1)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionTimingFunctionProperty));
            var concrete = (CSSTransitionTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.Custom, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("cubic-bezier(0, 1, 0.5, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionTimingFunctionStepsStartLegal()
        {
            var snippet = "transition-timing-function : steps(10, start)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionTimingFunctionProperty));
            var concrete = (CSSTransitionTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.Custom, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("steps(10, start)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionTimingFunctionStepsEndLegal()
        {
            var snippet = "transition-timing-function : steps(25, end)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionTimingFunctionProperty));
            var concrete = (CSSTransitionTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.Custom, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("steps(25, end)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTransitionTimingFunctionStepsLinearCubicBezierLegal()
        {
            var snippet = "transition-timing-function : steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionTimingFunctionProperty));
            var concrete = (CSSTransitionTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("steps(25, end), linear, cubic-bezier(0.25, 1, 0.5, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssDurationSecondsLegal()
        {
            var snippet = "transition-duration : 6s";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-duration", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionDurationProperty));
            var concrete = (CSSTransitionDurationProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("6s", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssDurationMillisecondsLegal()
        {
            var snippet = "transition-duration : 60ms";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-duration", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionDurationProperty));
            var concrete = (CSSTransitionDurationProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("60ms", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssDurationMillisecondsSecondsSecondsLegal()
        {
            var snippet = "transition-duration : 60ms, 1s, 2s";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("transition-duration", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTransitionDurationProperty));
            var concrete = (CSSTransitionDurationProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("60ms, 1s, 2s", concrete.Value.CssText);
        }
    }
}
