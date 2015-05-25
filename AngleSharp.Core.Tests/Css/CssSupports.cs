using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;
using System;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class CssSupportsTests
    {
        [Test]
        public void SupportsEmptyRule()
        {
            var source = @"@supports () { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual(String.Empty, supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsBackgroundColorRedRule()
        {
            var source = @"@supports (background-color: red) { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(background-color: red)", supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsBackgroundColorRedAndColorBlueRule()
        {
            var source = @"@supports ((background-color: red) and (color: blue)) { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual("((background-color: red) and (color: blue))", supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsNotUnsupportedDeclarationRule()
        {
            var source = @"@supports (not (background-transparency: half)) { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(not (background-transparency: half))", supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsUnsupportedDeclarationRule()
        {
            var source = @"@supports ((background-transparency: zero)) { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual("((background-transparency: zero))", supports.ConditionText);
            Assert.IsFalse(supports.IsSupported);
        }

        [Test]
        public void SupportsBackgroundRedWithImportantRule()
        {
            var source = @"@supports (background: red !important) { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(background: red !important)", supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsPaddingTopOrPaddingLeftRule()
        {
            var source = @"@supports ((padding-TOP :  0) or (padding-left : 0)) { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual("((padding-top: 0) or (padding-left: 0))", supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsPaddingTopOrPaddingLeftAndPaddingBottomOrPaddingRightRule()
        {
            var source = @"@supports (((padding-top: 0)  or  (padding-left: 0))  and  ((padding-bottom:  0)  or  (padding-right: 0))) { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(((padding-top: 0) or (padding-left: 0)) and ((padding-bottom: 0) or (padding-right: 0)))", supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsDisplayFlexWithImportantRule()
        {
            var source = @"@supports (display: flex !important) { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(display: flex !important)", supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsBareDisplayFlexRule()
        {
            var source = @"@supports display: flex { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual(String.Empty, supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsDisplayFlexMultipleBracketsRule()
        {
            var source = @"@supports ((display: flex)) { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual("((display: flex))", supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsTransitionOrAnimationNameAndTransformFrontBracketRule()
        {
            var source = @"@supports ((transition-property: color) or
           (animation-name: foo)) and
          (transform: rotate(10deg)) { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual("((transition-property: color) or (animation-name: foo)) and (transform: rotate(10deg))", supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsTransitionOrAnimationNameAndTransformBackBracketRule()
        {
            var source = @"@supports (transition-property: color) or
           ((animation-name: foo) and
          (transform: rotate(10deg))) { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(transition-property: color) or ((animation-name: foo) and (transform: rotate(10deg)))", supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsShadowVendorPrefixesRule()
        {
            var source = @"@supports ( box-shadow: 0 0 2px black ) or
          ( -moz-box-shadow: 0 0 2px black ) or
          ( -webkit-box-shadow: 0 0 2px black ) or
          ( -o-box-shadow: 0 0 2px black ) { }";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual("(box-shadow: 0 0 2px black) or (-moz-box-shadow: 0 0 2px black) or (-webkit-box-shadow: 0 0 2px black) or (-o-box-shadow: 0 0 2px black)", supports.ConditionText);
            Assert.IsTrue(supports.IsSupported);
        }

        [Test]
        public void SupportsNegatedDisplayFlexRuleWithDeclarations()
        {
            var source = @"@supports not ( display: flex ) {
  body { width: 100%; height: 100%; background: white; color: black; }
  #navigation { width: 25%; }
  #article { width: 75%; }
}";
            var parser = new CssParser(source);
            parser.Parse();
            Assert.AreEqual(1, parser.Result.Rules.Length);
            Assert.IsInstanceOf<CssSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CssSupportsRule;
            Assert.AreEqual(3, supports.Rules.Length);
            Assert.AreEqual("not (display: flex)", supports.ConditionText);
            Assert.IsFalse(supports.IsSupported);
        }
    }
}
