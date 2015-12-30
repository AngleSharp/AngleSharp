namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CssSupportsTests : CssConstructionFunctions
    {
        [Test]
        public void SupportsEmptyRule()
        {
            var source = @"@supports () { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual(String.Empty, supports.ConditionText);
            Assert.IsTrue(supports.Condition.Check());
        }

        [Test]
        public void SupportsBackgroundColorRedRule()
        {
            var source = @"@supports (background-color: red) { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(background-color: red)", supports.ConditionText);
            Assert.IsTrue(supports.Condition.Check());
        }

        [Test]
        public void SupportsBackgroundColorRedAndColorBlueRule()
        {
            var source = @"@supports ((background-color: red) and (color: blue)) { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual("((background-color: red) and (color: blue))", supports.ConditionText);
            Assert.IsTrue(supports.Condition.Check());
        }

        [Test]
        public void SupportsNotUnsupportedDeclarationRule()
        {
            var source = @"@supports (not (background-transparency: half)) { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(not (background-transparency: half))", supports.ConditionText);
            Assert.IsTrue(supports.Condition.Check());
        }

        [Test]
        public void SupportsUnsupportedDeclarationRule()
        {
            var source = @"@supports ((background-transparency: zero)) { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual("((background-transparency: zero))", supports.ConditionText);
            Assert.IsFalse(supports.Condition.Check());
        }

        [Test]
        public void SupportsBackgroundRedWithImportantRule()
        {
            var source = @"@supports (background: red !important) { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(background: red !important)", supports.ConditionText);
            Assert.IsTrue(supports.Condition.Check());
        }

        [Test]
        public void SupportsPaddingTopOrPaddingLeftRule()
        {
            var source = @"@supports ((padding-TOP :  0) or (padding-left : 0)) { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual("((padding-top: 0) or (padding-left: 0))", supports.ConditionText);
            Assert.IsTrue(supports.Condition.Check());
        }

        [Test]
        public void SupportsPaddingTopOrPaddingLeftAndPaddingBottomOrPaddingRightRule()
        {
            var source = @"@supports (((padding-top: 0)  or  (padding-left: 0))  and  ((padding-bottom:  0)  or  (padding-right: 0))) { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(((padding-top: 0) or (padding-left: 0)) and ((padding-bottom: 0) or (padding-right: 0)))", supports.ConditionText);
            Assert.IsTrue(supports.Condition.Check());
        }

        [Test]
        public void SupportsDisplayFlexWithImportantRule()
        {
            var source = @"@supports (display: flex !important) { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(display: flex !important)", supports.ConditionText);
            Assert.IsTrue(supports.Condition.Check());
        }

        [Test]
        public void SupportsBareDisplayFlexRule()
        {
            var source = @"@supports display: flex { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.Rules.Length);
        }

        [Test]
        public void SupportsDisplayFlexMultipleBracketsRule()
        {
            var source = @"@supports ((display: flex)) { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual("((display: flex))", supports.ConditionText);
            Assert.IsTrue(supports.Condition.Check());
        }

        [Test]
        public void SupportsTransitionOrAnimationNameAndTransformFrontBracketRule()
        {
            var source = @"@supports ((transition-property: color) or
           (animation-name: foo)) and
          (transform: rotate(10deg)) { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual("((transition-property: color) or (animation-name: foo)) and (transform: rotate(10deg))", supports.ConditionText);
            Assert.IsTrue(supports.Condition.Check());
        }

        [Test]
        public void SupportsTransitionOrAnimationNameAndTransformBackBracketRule()
        {
            var source = @"@supports (transition-property: color) or
           ((animation-name: foo) and
          (transform: rotate(10deg))) { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(transition-property: color) or ((animation-name: foo) and (transform: rotate(10deg)))", supports.ConditionText);
            Assert.IsTrue(supports.Condition.Check());
        }

        [Test]
        public void SupportsShadowVendorPrefixesRule()
        {
            var source = @"@supports ( box-shadow: 0 0 2px black ) or
          ( -moz-box-shadow: 0 0 2px black ) or
          ( -webkit-box-shadow: 0 0 2px black ) or
          ( -o-box-shadow: 0 0 2px black ) { }";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(box-shadow: 0 0 2px black) or (-moz-box-shadow: 0 0 2px black) or (-webkit-box-shadow: 0 0 2px black) or (-o-box-shadow: 0 0 2px black)", supports.ConditionText);
            Assert.IsTrue(supports.Condition.Check());
        }

        [Test]
        public void SupportsNegatedDisplayFlexRuleWithDeclarations()
        {
            var source = @"@supports not ( display: flex ) {
  body { width: 100%; height: 100%; background: white; color: black; }
  #navigation { width: 25%; }
  #article { width: 75%; }
}";
            var sheet = ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(sheet.Rules[0]);
            var supports = sheet.Rules[0] as CssSupportsRule;
            Assert.AreEqual(3, supports.Rules.Length);
            Assert.AreEqual("not (display: flex)", supports.ConditionText);
            Assert.IsFalse(supports.Condition.Check());
        }
    }
}
