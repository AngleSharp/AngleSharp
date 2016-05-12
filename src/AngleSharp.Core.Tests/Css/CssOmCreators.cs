namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CssOmCreatorsTests : CssConstructionFunctions
    {
        [Test]
        public void CssCreateStyleRuleViaHelper()
        {
            var sheet = ParseStyleSheet(String.Empty);
            var rule = sheet.AddNewRule(CssRuleType.Style);

            Assert.IsNotNull(rule);
            Assert.IsInstanceOf<ICssStyleRule>(rule);
        }

        [Test]
        public void CssCreateStyleRuleViaExtension()
        {
            var sheet = ParseStyleSheet(String.Empty);
            var rule = sheet.AddNewRule<ICssStyleRule>();

            Assert.IsNotNull(rule);
            Assert.IsInstanceOf<ICssStyleRule>(rule);
        }

        [Test]
        public void CssCreateStyleRuleDirectlyViaExtension()
        {
            var sheet = ParseStyleSheet(String.Empty);
            var rule = sheet.AddNewStyle("a > b");

            Assert.IsNotNull(rule);
            Assert.IsInstanceOf<ICssStyleRule>(rule);
            Assert.AreEqual("a>b", rule.SelectorText);
            Assert.AreEqual(0, rule.Style.Length);
        }

        [Test]
        public void CssCreateStyleRuleWithAnonymousObjectViaExtension()
        {
            var sheet = ParseStyleSheet(String.Empty);
            var rule = sheet.AddNewStyle("h1", new
            {
                margin = "5px",
                color = "green"
            });

            Assert.IsNotNull(rule);
            Assert.IsInstanceOf<ICssStyleRule>(rule);
            Assert.AreEqual("h1", rule.SelectorText);
            Assert.AreEqual(5, rule.Style.Length);
            Assert.AreEqual("5px", rule.Style.MarginTop);
            Assert.AreEqual("5px", rule.Style.MarginBottom);
            Assert.AreEqual("5px", rule.Style.MarginRight);
            Assert.AreEqual("5px", rule.Style.MarginLeft);
            Assert.AreEqual("rgb(0, 128, 0)", rule.Style.Color);
        }
    }
}
