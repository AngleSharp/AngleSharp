namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Css.Parser;
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
        public void QuerySelectorAllResultsShouldAlsoBeNodeLists()
        {
            var document = GetTestDocument();
            var selector = new CssSelectorParser().ParseSelector("li");
            var textResult = document.QuerySelectorAll("li");
            var objectResult = document.ChildNodes.QuerySelectorAll(selector);

            Assert.IsInstanceOf<INodeList>(textResult);
            Assert.IsInstanceOf<INodeList>(objectResult);
            Assert.AreEqual(textResult.Length, ((INodeList)textResult).Length);
            Assert.AreSame(textResult[0], ((INodeList)textResult)[0]);
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

        [Test]
        public void QuerySelectorAllWithCompoundNthChildShouldYieldExpectedMatches()
        {
            var document = "<div><p>a</p><span>x</span><p>b</p><p>c</p><p>d</p></div>".ToHtmlDocument();
            var items = document.QuerySelectorAll("p:nth-child(2n+1)");

            Assert.AreEqual(3, items.Length);
            Assert.AreEqual("a", items[0].TextContent);
            Assert.AreEqual("b", items[1].TextContent);
            Assert.AreEqual("d", items[2].TextContent);
        }

        [Test]
        public void QuerySelectorAllWithDuplicateIdsShouldReturnAllMatches()
        {
            var document = "<div id='dup'>one</div><section><div id='dup'>two</div></section>".ToHtmlDocument();
            var items = document.QuerySelectorAll("#dup");

            Assert.AreEqual(2, items.Length);
            Assert.AreEqual("one", items[0].TextContent);
            Assert.AreEqual("two", items[1].TextContent);
        }
    }
}
