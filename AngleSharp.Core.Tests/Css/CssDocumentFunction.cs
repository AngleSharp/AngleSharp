namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class CssDocumentFunctionTests : CssConstructionFunctions
    {
        [Test]
        public void CssDocumentRuleSingleUrlFunction()
        {
            var snippet = "@document url(http://www.w3.org/) { }";
            var rule = ParseRule(snippet) as ICssDocumentRule;
            Assert.IsNotNull(rule);
            Assert.AreEqual(CssRuleType.Document, rule.Type);
            Assert.AreEqual(1, rule.Conditions.Count());
            var condition = rule.Conditions.First();
            Assert.AreEqual("url", condition.Name);
            Assert.AreEqual("http://www.w3.org/", condition.Data);
            Assert.IsFalse(condition.Matches(Url.Create("https://www.w3.org/")));
            Assert.IsTrue(condition.Matches(Url.Create("http://www.w3.org")));
        }

        [Test]
        public void CssDocumentRuleSingleUrlPrefixFunction()
        {
            var snippet = "@document url-prefix(http://www.w3.org/Style/) { }";
            var rule = ParseRule(snippet) as ICssDocumentRule;
            Assert.IsNotNull(rule);
            Assert.AreEqual(CssRuleType.Document, rule.Type);
            Assert.AreEqual(1, rule.Conditions.Count());
            var condition = rule.Conditions.First();
            Assert.AreEqual("url-prefix", condition.Name);
            Assert.AreEqual("http://www.w3.org/Style/", condition.Data);
            Assert.IsFalse(condition.Matches(Url.Create("https://www.w3.org/Style/")));
            Assert.IsTrue(condition.Matches(Url.Create("http://www.w3.org/Style/foo/bar")));
        }

        [Test]
        public void CssDocumentRuleSingleDomainFunction()
        {
            var snippet = "@document domain('mozilla.org') { }";
            var rule = ParseRule(snippet) as ICssDocumentRule;
            Assert.IsNotNull(rule);
            Assert.AreEqual(CssRuleType.Document, rule.Type);
            Assert.AreEqual(1, rule.Conditions.Count());
            var condition = rule.Conditions.First();
            Assert.AreEqual("domain", condition.Name);
            Assert.AreEqual("mozilla.org", condition.Data);
            Assert.IsFalse(condition.Matches(Url.Create("https://www.w3.org/")));
            Assert.IsTrue(condition.Matches(Url.Create("http://mozilla.org")));
            Assert.IsTrue(condition.Matches(Url.Create("http://www.mozilla.org")));
            Assert.IsTrue(condition.Matches(Url.Create("http://foo.mozilla.org")));
        }

        [Test]
        public void CssDocumentRuleSingleRegexpFunction()
        {
            var snippet = "@document regexp(\"https:.*\") { }";
            var rule = ParseRule(snippet) as ICssDocumentRule;
            Assert.IsNotNull(rule);
            Assert.AreEqual(CssRuleType.Document, rule.Type);
            Assert.AreEqual(1, rule.Conditions.Count());
            var condition = rule.Conditions.First();
            Assert.AreEqual("regexp", condition.Name);
            Assert.AreEqual("https:.*", condition.Data);
            Assert.IsFalse(condition.Matches(Url.Create("http://www.w3.org")));
            Assert.IsTrue(condition.Matches(Url.Create("https://www.w3.org/")));
        }

        [Test]
        public void CssDocumentRuleMultipleFunctions()
        {
            var snippet = "@document url(http://www.w3.org/), url-prefix(http://www.w3.org/Style/), domain(mozilla.org), regexp(\"https:.*\") { }";
            var rule = ParseRule(snippet) as CssDocumentRule;
            Assert.IsNotNull(rule);
            Assert.AreEqual(CssRuleType.Document, rule.Type);
            Assert.AreEqual(4, rule.Conditions.Count());
            Assert.IsTrue(rule.IsValid(Url.Create("https://www.w3.org/")));
            Assert.IsTrue(rule.IsValid(Url.Create("http://www.w3.org/")));
            Assert.IsTrue(rule.IsValid(Url.Create("http://www.w3.org/Style/bar")));
            Assert.IsTrue(rule.IsValid(Url.Create("https://test.mozilla.org/foo")));
            Assert.IsFalse(rule.IsValid(Url.Create("http://localhost")));
        }
    }
}
