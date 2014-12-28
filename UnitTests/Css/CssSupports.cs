using AngleSharp.DOM.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;
using System;

namespace UnitTests.Css
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
            Assert.IsInstanceOf<CSSSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CSSSupportsRule;
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
            Assert.IsInstanceOf<CSSSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CSSSupportsRule;
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
            Assert.IsInstanceOf<CSSSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CSSSupportsRule;
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
            Assert.IsInstanceOf<CSSSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CSSSupportsRule;
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
            Assert.IsInstanceOf<CSSSupportsRule>(parser.Result.Rules[0]);
            var supports = parser.Result.Rules[0] as CSSSupportsRule;
            Assert.AreEqual("(background-transparency: zero)", supports.ConditionText);
            Assert.IsFalse(supports.IsSupported);
        }
    }
}
