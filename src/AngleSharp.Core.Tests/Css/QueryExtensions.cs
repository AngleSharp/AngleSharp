namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class QueryExtensionsTests
    {
        private static IDocument GetTestDocument()
        {
            var content = "<!doctype html><ul><li>First entry<li>Second entry<li>Third entry<li>4<li>Fifth<li>Last</ul>";
            return content.ToHtmlDocument();
        }

        [Test]
        public void QueryOnEmptyNodeListShouldYieldEmptyResult()
        {
            var document = GetTestDocument();
            var result = document.Head.QuerySelectorAll("a");
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void InvalidQueryOnEmptyNodeListShouldThrowException()
        {
            var document = GetTestDocument();
            Assert.Catch<DomException>(() => document.Head.QuerySelectorAll("<invalid>"));
        }

        [Test]
        public void QueryOnNonEmptyNodeListShouldYieldEmptyResult()
        {
            var document = GetTestDocument();
            var result = document.Body.QuerySelectorAll("a");
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void InvalidQueryOnNonEmptyNodeListShouldThrowException()
        {
            var document = GetTestDocument();
            Assert.Catch<DomException>(() => document.Body.QuerySelectorAll("<invalid>"));
        }

        [Test]
        public void QueryEqValidIndexShouldYieldEntry()
        {
            var document = GetTestDocument();
            var item = document.QuerySelectorAll("li").Eq(3);
            Assert.IsNotNull(item);
            Assert.AreEqual("4", item.TextContent);
        }

        [Test]
        public void InvalidQueryPseudoClassSelectorShouldYieldException()
        {
            var document = GetTestDocument();
            Assert.Catch<DomException>(() => document.QuerySelectorAll(":foo > p"));
        }

        [Test]
        public void InvalidQueryPseudoClassFunctionSelectorShouldYieldException()
        {
            var document = GetTestDocument();
            Assert.Catch<DomException>(() => document.QuerySelectorAll(":bar(baz) > p"));
        }

        [Test]
        public void InvalidQueryPseudoElementSelectorShouldYieldException()
        {
            var document = GetTestDocument();
            Assert.Catch<DomException>(() => document.QuerySelectorAll("::test > p"));
        }

        [Test]
        public void QueryEqInvalidIndexShouldYieldNull()
        {
            var document = GetTestDocument();
            var item = document.QuerySelectorAll("li").Eq(6);
            Assert.IsNull(item);
        }

        [Test]
        public void QueryLtShouldLimitEntries()
        {
            var document = GetTestDocument();
            var items = document.QuerySelectorAll("li").Lt(3);
            Assert.AreEqual(3, items.Count());
            Assert.AreEqual("First entry", items.Skip(0).First().TextContent);
            Assert.AreEqual("Second entry", items.Skip(1).First().TextContent);
            Assert.AreEqual("Third entry", items.Skip(2).First().TextContent);
        }

        [Test]
        public void QueryLtZeroShouldYieldNoEntries()
        {
            var document = GetTestDocument();
            var items = document.QuerySelectorAll("li").Lt(0);
            Assert.AreEqual(0, items.Count());
        }

        [Test]
        public void QueryGtShouldLimitEntries()
        {
            var document = GetTestDocument();
            var items = document.QuerySelectorAll("li").Gt(3);
            Assert.AreEqual(2, items.Count());
            Assert.AreEqual("Fifth", items.Skip(0).First().TextContent);
            Assert.AreEqual("Last", items.Skip(1).First().TextContent);
        }

        [Test]
        public void QueryGtZeroShouldYieldNoEntries()
        {
            var document = GetTestDocument();
            var items = document.QuerySelectorAll("li").Gt(6);
            Assert.AreEqual(0, items.Count());
        }

        [Test]
        public void QueryEvenShouldYieldOnlyEvenEntries()
        {
            var document = GetTestDocument();
            var items = document.QuerySelectorAll("li").Even();
            Assert.AreEqual(3, items.Count());
            Assert.AreEqual("First entry", items.Skip(0).First().TextContent);
            Assert.AreEqual("Third entry", items.Skip(1).First().TextContent);
            Assert.AreEqual("Fifth", items.Skip(2).First().TextContent);
        }

        [Test]
        public void QueryOddShouldYieldOnlyOddEntries()
        {
            var document = GetTestDocument();
            var items = document.QuerySelectorAll("li").Odd();
            Assert.AreEqual(3, items.Count());
            Assert.AreEqual("Second entry", items.Skip(0).First().TextContent);
            Assert.AreEqual("4", items.Skip(1).First().TextContent);
            Assert.AreEqual("Last", items.Skip(2).First().TextContent);
        }
    }
}
