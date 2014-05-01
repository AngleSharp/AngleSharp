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

        [TestMethod]
        public void CssAnimationNameDashSpecificLegal()
        {
            var snippet = "animation-name : -specific";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationNameProperty));
            var concrete = (CSSAnimationNameProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("-specific", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationNameSlidingVerticallyLegal()
        {
            var snippet = "animation-name : sliding-vertically";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationNameProperty));
            var concrete = (CSSAnimationNameProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("sliding-vertically", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationNameTest05Legal()
        {
            var snippet = "animation-name : test_05";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationNameProperty));
            var concrete = (CSSAnimationNameProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("test_05", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationNameMyAnimationOtherAnimationLegal()
        {
            var snippet = "animation-name : my-animation, other-animation";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationNameProperty));
            var concrete = (CSSAnimationNameProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("my-animation, other-animation", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationIterationCountZeroLegal()
        {
            var snippet = "animation-iteration-count : 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationIterationCountProperty));
            var concrete = (CSSAnimationIterationCountProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationIterationCountInfiniteLegal()
        {
            var snippet = "animation-iteration-count : infinite";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationIterationCountProperty));
            var concrete = (CSSAnimationIterationCountProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("infinite", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationIterationCountInfiniteUppercaseLegal()
        {
            var snippet = "animation-iteration-count : INFINITE";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationIterationCountProperty));
            var concrete = (CSSAnimationIterationCountProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("INFINITE", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationIterationCountFloatLegal()
        {
            var snippet = "animation-iteration-count : 2.3";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationIterationCountProperty));
            var concrete = (CSSAnimationIterationCountProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2.3", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationIterationCountTwoZeroInfiniteLegal()
        {
            var snippet = "animation-iteration-count : 2, 0, infinite";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationIterationCountProperty));
            var concrete = (CSSAnimationIterationCountProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2, 0, infinite", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationIterationCountNegativeIllegal()
        {
            var snippet = "animation-iteration-count : -1";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-iteration-count", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationIterationCountProperty));
            var concrete = (CSSAnimationIterationCountProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssAnimationTimingFunctionEaseUppercaseLegal()
        {
            var snippet = "animation-timing-function : EASE";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationTimingFunctionProperty));
            var concrete = (CSSAnimationTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("EASE", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationTimingFunctionEaseInOutLegal()
        {
            var snippet = "animation-timing-function : ease-IN-out";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationTimingFunctionProperty));
            var concrete = (CSSAnimationTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("ease-IN-out", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationTimingFunctionStepEndLegal()
        {
            var snippet = "animation-timing-function : step-END";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationTimingFunctionProperty));
            var concrete = (CSSAnimationTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-END", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationTimingFunctionStepStartLinearLegal()
        {
            var snippet = "animation-timing-function : step-start  , LINeAr";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationTimingFunctionProperty));
            var concrete = (CSSAnimationTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-start, LINeAr", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationTimingFunctionStepStartCubicBezierLegal()
        {
            var snippet = "animation-timing-function : step-start  , cubic-bezier(0,1,1,1)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationTimingFunctionProperty));
            var concrete = (CSSAnimationTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-start, cubic-bezier(0, 1, 1, 1)", concrete.Value.CssText);
        }
    }
}
