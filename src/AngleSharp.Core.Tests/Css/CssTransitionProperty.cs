namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;

    [TestFixture]
    public class CssTransitionPropertyTests : CssConstructionFunctions
    {
        [Test]
        public void CssTransitionPropertyNoneLegal()
        {
            var snippet = "transition-property : none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionPropertyProperty>(property);
            var concrete = (CssTransitionPropertyProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssTransitionPropertyAllLegal()
        {
            var snippet = "transition-property : ALL";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionPropertyProperty>(property);
            var concrete = (CssTransitionPropertyProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("all", concrete.Value);
        }

        [Test]
        public void CssTransitionPropertyWidthHeightLegal()
        {
            var snippet = "transition-property : width   , height";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionPropertyProperty>(property);
            var concrete = (CssTransitionPropertyProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("width, height", concrete.Value);
        }

        [Test]
        public void CssTransitionPropertyDashSpecificIllegal()
        {
            var snippet = "transition-property : -specific";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionPropertyProperty>(property);
            var concrete = (CssTransitionPropertyProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssTransitionPropertySlidingVerticallyIllegal()
        {
            var snippet = "transition-property : sliding-vertically";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionPropertyProperty>(property);
            var concrete = (CssTransitionPropertyProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssTransitionPropertyTest05Illegal()
        {
            var snippet = "transition-property : test_05";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-property", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionPropertyProperty>(property);
            var concrete = (CssTransitionPropertyProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssTransitionTimingFunctionEaseLegal()
        {
            var snippet = "transition-timing-function : ease";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionTimingFunctionProperty>(property);
            var concrete = (CssTransitionTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("ease", concrete.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionEaseInLegal()
        {
            var snippet = "transition-timing-function : ease-IN";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionTimingFunctionProperty>(property);
            var concrete = (CssTransitionTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("ease-in", concrete.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionStepStartLegal()
        {
            var snippet = "transition-timing-function : step-start";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionTimingFunctionProperty>(property);
            var concrete = (CssTransitionTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-start", concrete.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionStepStartStepEndLegal()
        {
            var snippet = "transition-timing-function : step-start  , step-end";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionTimingFunctionProperty>(property);
            var concrete = (CssTransitionTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-start, step-end", concrete.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionStepStartStepEndLinearEaseInOutLegal()
        {
            var snippet = "transition-timing-function : step-start  , step-end,linear,ease-IN-OUT";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionTimingFunctionProperty>(property);
            var concrete = (CssTransitionTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("step-start, step-end, linear, ease-in-out", concrete.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionCubicBezierLegal()
        {
            var snippet = "transition-timing-function : cubic-bezier(0, 1, 0.5, 1)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionTimingFunctionProperty>(property);
            var concrete = (CssTransitionTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("cubic-bezier(0, 1, 0.5, 1)", concrete.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionStepsStartLegal()
        {
            var snippet = "transition-timing-function : steps(10, start)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionTimingFunctionProperty>(property);
            var concrete = (CssTransitionTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("steps(10, start)", concrete.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionStepsEndLegal()
        {
            var snippet = "transition-timing-function : steps(25, end)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionTimingFunctionProperty>(property);
            var concrete = (CssTransitionTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("steps(25, end)", concrete.Value);
        }

        [Test]
        public void CssTransitionTimingFunctionStepsLinearCubicBezierLegal()
        {
            var snippet = "transition-timing-function : steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-timing-function", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionTimingFunctionProperty>(property);
            var concrete = (CssTransitionTimingFunctionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)", concrete.Value);
        }

        [Test]
        public void CssTransitionDurationSecondsLegal()
        {
            var snippet = "transition-duration : 6s";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-duration", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionDurationProperty>(property);
            var concrete = (CssTransitionDurationProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("6s", concrete.Value);
        }

        [Test]
        public void CssTransitionDurationMillisecondsLegal()
        {
            var snippet = "transition-duration : 60ms";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-duration", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionDurationProperty>(property);
            var concrete = (CssTransitionDurationProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("60ms", concrete.Value);
        }

        [Test]
        public void CssTransitionDurationMillisecondsSecondsSecondsLegal()
        {
            var snippet = "transition-duration : 60ms, 1s, 2s";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-duration", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionDurationProperty>(property);
            var concrete = (CssTransitionDurationProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("60ms, 1s, 2s", concrete.Value);
        }

        [Test]
        public void CssTransitionDelayMillisecondsLegal()
        {
            var snippet = "transition-delay : 60ms";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-delay", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionDelayProperty>(property);
            var concrete = (CssTransitionDelayProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("60ms", concrete.Value);
        }

        [Test]
        public void CssTransitionDelayMillisecondsSecondsSecondsLegal()
        {
            var snippet = "transition-delay : 60ms, 1s, 2s";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition-delay", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionDelayProperty>(property);
            var concrete = (CssTransitionDelayProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("60ms, 1s, 2s", concrete.Value);
        }

        [Test]
        public void CssTransitionMillisecondsSecondsSecondsLegal()
        {
            var snippet = "transition : 60ms, 1s, 2s";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionProperty>(property);
            var concrete = (CssTransitionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("60ms, 1s, 2s", concrete.Value);
        }

        [Test]
        public void CssTransitionStepsLinearCubicBezierLegal()
        {
            var snippet = "transition : steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionProperty>(property);
            var concrete = (CssTransitionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("steps(25), linear, cubic-bezier(0.25, 1, 0.5, 1)", concrete.Value);
        }

        [Test]
        public void CssTransitionWidthHeightLegal()
        {
            var snippet = "transition : width   , height";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionProperty>(property);
            var concrete = (CssTransitionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("width, height", concrete.Value);
        }

        [Test]
        public void CssTransitionEaseLegal()
        {
            var snippet = "transition : ease";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionProperty>(property);
            var concrete = (CssTransitionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("ease", concrete.Value);
        }

        [Test]
        public void CssTransitionSecondsEaseAllLegal()
        {
            var snippet = "transition : all 1s ease";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionProperty>(property);
            var concrete = (CssTransitionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("all 1s ease", concrete.Value);
        }

        [Test]
        public void CssTransitionSecondsEaseAllHeightMsStepsLegal()
        {
            var snippet = "transition : all 1s ease, height steps(5) 50ms";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionProperty>(property);
            var concrete = (CssTransitionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("all 1s ease, height 50ms steps(5)", concrete.Value);
        }

        [Test]
        public void CssTransitionSecondsEaseAllHeightMsStepsWidthCubicBezierLegal()
        {
            var snippet = "transition : all 1s ease, height step-start 50ms,width,cubic-bezier(0.2,0.5 , 1  ,  1)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transition", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransitionProperty>(property);
            var concrete = (CssTransitionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("all 1s ease, height 50ms step-start, width, cubic-bezier(0.2, 0.5, 1, 1)", concrete.Value);
        }
    }
}
