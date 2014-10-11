using AngleSharp.DOM.Css;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDurationProperty));
            var concrete = (CSSAnimationDurationProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDurationProperty));
            var concrete = (CSSAnimationDurationProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDelayProperty));
            var concrete = (CSSAnimationDelayProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0ms", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationDelayZeroIllegal()
        {
            var snippet = "animation-delay : 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-delay", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDelayProperty));
            var concrete = (CSSAnimationDelayProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssAnimationDelayZeroZeroSecondMillisecondsLegal()
        {
            var snippet = "animation-delay : 0s  , 0s  , 1s  , 20ms";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-delay", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDelayProperty));
            var concrete = (CSSAnimationDelayProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationNameProperty));
            var concrete = (CSSAnimationNameProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationNameProperty));
            var concrete = (CSSAnimationNameProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationNameProperty));
            var concrete = (CSSAnimationNameProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("test_05", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationNameNumberIllegal()
        {
            var snippet = "animation-name : 42";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationNameProperty));
            var concrete = (CSSAnimationNameProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssAnimationNameMyAnimationOtherAnimationLegal()
        {
            var snippet = "animation-name : my-animation, other-animation";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-name", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationNameProperty));
            var concrete = (CSSAnimationNameProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationIterationCountProperty));
            var concrete = (CSSAnimationIterationCountProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationIterationCountProperty));
            var concrete = (CSSAnimationIterationCountProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationIterationCountProperty));
            var concrete = (CSSAnimationIterationCountProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationIterationCountProperty));
            var concrete = (CSSAnimationIterationCountProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationIterationCountProperty));
            var concrete = (CSSAnimationIterationCountProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationIterationCountProperty));
            var concrete = (CSSAnimationIterationCountProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssAnimationTimingFunctionEaseUppercaseLegal()
        {
            var snippet = "animation-timing-function : EASE";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationTimingFunctionProperty));
            var concrete = (CSSAnimationTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("EASE", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationTimingFunctionNoneIllegal()
        {
            var snippet = "animation-timing-function : none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationTimingFunctionProperty));
            var concrete = (CSSAnimationTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssAnimationTimingFunctionEaseInOutLegal()
        {
            var snippet = "animation-timing-function : ease-IN-out";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationTimingFunctionProperty));
            var concrete = (CSSAnimationTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationTimingFunctionProperty));
            var concrete = (CSSAnimationTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationTimingFunctionProperty));
            var concrete = (CSSAnimationTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
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
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationTimingFunctionProperty));
            var concrete = (CSSAnimationTimingFunctionProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-start, cubic-bezier(0, 1, 1, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationPlayStateRunningLegal()
        {
            var snippet = "animation-play-state: running";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-play-state", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationPlayStateProperty));
            var concrete = (CSSAnimationPlayStateProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("running", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationPlayStatePausedUppercaseLegal()
        {
            var snippet = "animation-play-state: PAUSED";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-play-state", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationPlayStateProperty));
            var concrete = (CSSAnimationPlayStateProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("PAUSED", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationPlayStatePausedRunningPausedLegal()
        {
            var snippet = "animation-play-state: paused, Running, paused";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-play-state", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationPlayStateProperty));
            var concrete = (CSSAnimationPlayStateProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("paused, Running, paused", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationFillModeNoneLegal()
        {
            var snippet = "animation-fill-mode: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationFillModeProperty));
            var concrete = (CSSAnimationFillModeProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationFillModeZeroIllegal()
        {
            var snippet = "animation-fill-mode: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationFillModeProperty));
            var concrete = (CSSAnimationFillModeProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssAnimationFillModeBackwardsLegal()
        {
            var snippet = "animation-fill-mode: backwards !important";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationFillModeProperty));
            var concrete = (CSSAnimationFillModeProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("backwards", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationFillModeForwardsUppercaseLegal()
        {
            var snippet = "animation-fill-mode: FORWARDS";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationFillModeProperty));
            var concrete = (CSSAnimationFillModeProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("FORWARDS", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationFillModeBothBackwardsForwardsNoneLegal()
        {
            var snippet = "animation-fill-mode: both , backwards ,  forwards  ,NONE";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-fill-mode", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationFillModeProperty));
            var concrete = (CSSAnimationFillModeProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("both, backwards, forwards, NONE", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationDirectionNormalLegal()
        {
            var snippet = "animation-direction: normal";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDirectionProperty));
            var concrete = (CSSAnimationDirectionProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationDirectionReverseLegal()
        {
            var snippet = "animation-direction  : reverse";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDirectionProperty));
            var concrete = (CSSAnimationDirectionProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("reverse", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationDirectionNoneIllegal()
        {
            var snippet = "animation-direction  : none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDirectionProperty));
            var concrete = (CSSAnimationDirectionProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssAnimationDirectionAlternateReverseUppercaseLegal()
        {
            var snippet = "animation-direction : alternate-REVERSE";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDirectionProperty));
            var concrete = (CSSAnimationDirectionProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("alternate-REVERSE", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationDirectionNormalAlternateReverseAlternateReverseLegal()
        {
            var snippet = "animation-direction: normal,alternate  , reverse   ,ALTERNATE-reverse !important";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation-direction", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationDirectionProperty));
            var concrete = (CSSAnimationDirectionProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal, alternate, reverse, ALTERNATE-reverse", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationIterationCountLegal()
        {
            var snippet = "animation : 5";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationProperty));
            var concrete = (CSSAnimationProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationNameLegal()
        {
            var snippet = "animation : my-animation";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationProperty));
            var concrete = (CSSAnimationProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("my-animation", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationNameDurationDelayLegal()
        {
            var snippet = "animation : my-animation 2s 0.5s";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationProperty));
            var concrete = (CSSAnimationProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("my-animation 2s 0.5s", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationNameDurationDelayEaseLegal()
        {
            var snippet = "animation : my-animation  200ms 0.5s    ease";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationProperty));
            var concrete = (CSSAnimationProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("my-animation 200ms 0.5s ease", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationCountDoubleIllegal()
        {
            var snippet = "animation : 10 20";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationProperty));
            var concrete = (CSSAnimationProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssAnimationNameDurationCountEaseInOutLegal()
        {
            var snippet = "animation : my-animation  200ms 2.5   ease-in-out";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationProperty));
            var concrete = (CSSAnimationProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("my-animation 200ms 2.5 ease-in-out", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssAnimationMultipleLegal()
        {
            var snippet = "animation : my-animation 0s 10 ease,   other-animation  5 linear,yet-another 0s 1s  10 step-start !important";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("animation", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSAnimationProperty));
            var concrete = (CSSAnimationProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("my-animation 0s 10 ease, other-animation 5 linear, yet-another 0s 1s 10 step-start", concrete.Value.CssText);
        }
    }
}
