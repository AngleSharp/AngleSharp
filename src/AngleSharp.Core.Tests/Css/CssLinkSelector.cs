namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class CssLinkSelectorTests
    {
        // https://html.spec.whatwg.org/multipage/semantics-other.html#selector-link
        [Test]
        public void HtmlHyperlinksDependOnHrefPresence(
            [Values("a", "area", "link", "div", "base")] String tagName,
            [Values(null, "", " ", "/next")] String href)
        {
            var document = $"<!doctype html><{tagName} id='target'></{tagName}>".ToHtmlDocument();
            var element = document.QuerySelector("#target");

            if (href != null)
            {
                element.SetAttribute("href", href);
            }

            var expected = (tagName == "a" || tagName == "area") && href != null;

            AssertLinkState(element, expected);
            Assert.AreEqual(expected ? element : null, document.QuerySelector(":any-link"));
            Assert.AreEqual(expected ? element : null, document.QuerySelector(":link"));
            Assert.AreEqual(expected ? 1 : 0, document.QuerySelectorAll(":any-link").Length);
            Assert.AreEqual(expected ? 1 : 0, document.QuerySelectorAll(":link").Length);
            Assert.AreEqual(0, document.QuerySelectorAll(":visited").Length);
        }

        [TestCase("a", true)]
        [TestCase("area", true)]
        [TestCase("link", false)]
        public void LinkStateTracksHrefMutation(String tagName, Boolean hyperlink)
        {
            var document = $"<!doctype html><{tagName} id='target'></{tagName}>".ToHtmlDocument();
            var element = document.QuerySelector("#target");

            AssertLinkState(element, false);
            element.SetAttribute("href", String.Empty);
            AssertLinkState(element, hyperlink);
            element.SetAttribute("href", "/next");
            AssertLinkState(element, hyperlink);
            element.RemoveAttribute("href");
            AssertLinkState(element, false);
        }

        [TestCase("a", true)]
        [TestCase("area", true)]
        [TestCase("link", false)]
        public void HtmlHyperlinksRequireNoNamespaceHref(String tagName, Boolean hyperlink)
        {
            var document = $"<!doctype html><{tagName} id='target'></{tagName}>".ToHtmlDocument();
            var element = document.QuerySelector("#target");

            element.SetAttribute("urn:example", "href", "/namespaced");
            AssertLinkState(element, false);
            element.SetAttribute("href", String.Empty);
            AssertLinkState(element, hyperlink);
            element.RemoveAttribute(null, "href");
            AssertLinkState(element, false);
        }

        // https://github.com/web-platform-tests/wpt/blob/master/dom/nodes/selectors.js
        [Test]
        public void LinkQueriesPartitionHtmlHyperlinksAndExcludeHeadLinks()
        {
            var document = ("<!doctype html><head><link id='stylesheet' href='/style.css'></head><body>" +
                "<a id='a-empty' href=''></a><a id='a-full' href='/next'></a><a id='a-absent'></a>" +
                "<map><area id='area-empty' href=''><area id='area-full' href='/map'><area id='area-absent'></map>" +
                "<div href='/other'></div></body>").ToHtmlDocument();
            var expected = new[] { "a-empty", "a-full", "area-empty", "area-full" };

            foreach (var selector in new[] { ":any-link", ":link", ":link, :visited" })
            {
                CollectionAssert.AreEqual(expected, document.QuerySelectorAll(selector).Select(element => element.Id));
                Assert.AreEqual("a-empty", document.QuerySelector(selector).Id);
            }

            Assert.AreEqual(0, document.QuerySelectorAll("head :any-link, head :link, head :visited").Length);
            Assert.AreEqual(0, document.QuerySelectorAll(":link:visited").Length);
            Assert.AreEqual(0, document.QuerySelectorAll(":visited").Length);
        }

        private static void AssertLinkState(IElement element, Boolean expected)
        {
            Assert.AreEqual(expected, element.IsLink());
            Assert.AreEqual(expected, element.Matches(":link"));
            Assert.AreEqual(expected, element.Matches(":any-link"));
            Assert.IsFalse(element.IsVisited());
            Assert.IsFalse(element.Matches(":visited"));
        }
    }
}
