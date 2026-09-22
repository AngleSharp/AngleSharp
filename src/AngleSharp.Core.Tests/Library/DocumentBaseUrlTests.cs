namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class DocumentBaseUrlTests
    {
        private static async Task<IDocument> OpenAsync(string html)
        {
            return await BrowsingContext.New().OpenAsync(response => response
                .Address("https://example.test/start/page").Content(html)).ConfigureAwait(false);
        }

        [Test]
        public async Task FirstConnectedBaseWinsAndDetachedWritesDoNotChangeIt()
        {
            var document = await OpenAsync("<base href='./first/'><base href='./second/'><a href='item'>Item</a>").ConfigureAwait(false);
            var detached = (IHtmlBaseElement)document.CreateElement("base");
            detached.Href = "/detached/";
            Assert.AreEqual("https://example.test/start/first/", document.BaseUri);
            Assert.AreEqual("https://example.test/start/first/item", ((IHtmlAnchorElement)document.QuerySelector("a")).Href);
            document.QuerySelector("base").Remove();
            Assert.AreEqual("https://example.test/start/second/", document.BaseUri);
        }

        [TestCase("data:text/plain,ignored")]
        [TestCase("javascript:ignored")]
        [TestCase("https://[")]
        public async Task InvalidBaseFreezesFallbackUntilRelevantMutation(string href)
        {
            var document = await OpenAsync("<base><a href='item'>Item</a>").ConfigureAwait(false);
            document.QuerySelector("base").SetAttribute("href", href);
            ((Document)document).DocumentUrl.Href = "https://example.test/moved/page";
            Assert.AreEqual("https://example.test/start/page", document.BaseUri);
            document.QuerySelector("base").SetAttribute("href", href);
            Assert.AreEqual("https://example.test/moved/page", document.BaseUri);
        }

        [Test]
        public async Task BaseActivationIsFrozenBeforeTheNextUrlRead()
        {
            var document = await OpenAsync("<base href='./first/'>").ConfigureAwait(false);
            ((Document)document).DocumentUrl.Href = "https://example.test/middle/page";
            document.QuerySelector("base").SetAttribute("href", "./next/");
            ((Document)document).DocumentUrl.Href = "https://example.test/final/page";
            Assert.AreEqual("https://example.test/middle/next/", document.BaseUri);
        }

        [Test]
        public async Task RemovingAndReinsertingContainingSubtreeRefreezesBase()
        {
            var document = await OpenAsync("<div><base href='./first/'></div>").ConfigureAwait(false);
            ((Document)document).DocumentUrl.Href = "https://example.test/moved/page";
            var container = document.QuerySelector("div");
            container.Remove();
            Assert.AreEqual("https://example.test/moved/page", document.BaseUri);
            document.Body.AppendChild(container);
            Assert.AreEqual("https://example.test/moved/first/", document.BaseUri);
        }

        [Test]
        public async Task BaseHrefUsesDocumentFallbackAndPreservesInvalidInput()
        {
            var document = await OpenAsync("<base href='./first/'>").ConfigureAwait(false);
            var element = (IHtmlBaseElement)document.QuerySelector("base");
            Assert.AreEqual("https://example.test/start/first/", element.Href);
            element.Href = "https://[";
            Assert.AreEqual("https://[", element.Href);
        }
    }
}
