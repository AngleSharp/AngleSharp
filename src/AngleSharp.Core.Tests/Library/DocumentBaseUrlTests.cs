namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    [TestFixture]
    public class DocumentBaseUrlTests
    {
        private static async Task<IDocument> OpenAsync(String html)
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
        public async Task InvalidBaseFreezesFallbackUntilRelevantMutation(String href)
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

        [Test]
        public async Task CloneRefreezesBaseAgainstClonedDocumentUrl()
        {
            var document = await OpenAsync("<base href='./assets/'>").ConfigureAwait(false);
            ((Document)document).DocumentUrl.Href = "https://example.test/moved/page";
            var clone = (IDocument)document.Clone(true);
            Assert.AreEqual("https://example.test/start/assets/", document.BaseUri);
            Assert.AreEqual("https://example.test/moved/assets/", clone.BaseUri);
        }

        [Test]
        public async Task ExplicitBaseUrlOverrideDoesNotBecomeDocumentFallback()
        {
            var document = await OpenAsync("<a href='item'>Item</a>").ConfigureAwait(false);
            ((Node)document).BaseUrl = new Url("https://example.test/assets/");
            var element = (IHtmlBaseElement)document.CreateElement("base");
            element.Href = "child/";
            document.Head.AppendChild(element);
            Assert.AreEqual("https://example.test/assets/", document.BaseUri);
            Assert.AreEqual("https://example.test/start/child/", element.Href);
            Assert.AreEqual("https://example.test/assets/item", ((IHtmlAnchorElement)document.QuerySelector("a")).Href);
        }

        [Test]
        public async Task SrcDocDocumentInheritsFallbackBaseUrlFromCreator()
        {
            var config = Configuration.Default.WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });
            var html = "<!doctype html><base href='https://example.test/assets/'>" +
                "<iframe id='frame' srcdoc='<!doctype html><html><head></head><body></body></html>'></iframe>";
            var document = await BrowsingContext.New(config).OpenAsync(response => response
                .Address("https://example.test/start/page").Content(html)).ConfigureAwait(false);
            var frame = document.QuerySelector<IHtmlInlineFrameElement>("#frame");
            var child = frame.ContentDocument;
            Assert.AreEqual("about:srcdoc", child.Url);
            Assert.AreEqual("https://example.test/assets/", child.BaseUri);
            var baseElement = (IHtmlBaseElement)child.CreateElement("base");
            baseElement.SetAttribute("href", "child/");
            child.Head.AppendChild(baseElement);
            var anchor = (IHtmlAnchorElement)child.CreateElement("a");
            anchor.SetAttribute("href", "item");
            child.Body.AppendChild(anchor);
            Assert.AreEqual("about:srcdoc", child.Url);
            Assert.AreEqual("https://example.test/assets/child/", child.BaseUri);
            Assert.AreEqual("https://example.test/assets/child/", baseElement.Href);
            Assert.AreEqual("https://example.test/assets/child/item", anchor.Href);
        }

        [Test]
        public async Task AdoptionUsesNewDocumentAndRemovalRestoresOldDocument()
        {
            var source = await OpenAsync("<div><base href='./assets/'></div>").ConfigureAwait(false);
            var target = await OpenAsync("<p>Target</p>").ConfigureAwait(false);
            ((Document)target).DocumentUrl.Href = "https://example.test/target/page";
            var subtree = source.QuerySelector("div");
            target.AdoptNode(subtree);
            target.Body.AppendChild(subtree);
            Assert.AreEqual("https://example.test/start/page", source.BaseUri);
            Assert.AreEqual("https://example.test/target/assets/", target.BaseUri);
            ((Document)target).DocumentUrl.Href = "https://example.test/later/page";
            Assert.AreEqual("https://example.test/target/assets/", target.BaseUri);
        }

        [Test]
        public async Task TemporaryEarlierBaseRefreezesOriginalButLaterChangesDoNot()
        {
            var document = await OpenAsync("<base href='./assets/'>").ConfigureAwait(false);
            var original = document.QuerySelector("base");
            ((Document)document).DocumentUrl.Href = "https://example.test/moved/page";
            var temporary = document.CreateElement("base");
            temporary.SetAttribute("href", "/temporary/");
            document.Head.InsertBefore(temporary, original);
            temporary.Remove();
            Assert.AreEqual("https://example.test/moved/assets/", document.BaseUri);
            ((Document)document).DocumentUrl.Href = "https://example.test/final/page";
            document.Head.AppendChild(temporary);
            temporary.SetAttribute("href", "/unused/");
            temporary.Remove();
            Assert.AreEqual("https://example.test/moved/assets/", document.BaseUri);
        }

        [Test]
        public async Task TemplateContentAndForeignBaseDoNotChooseDocumentBase()
        {
            var document = await OpenAsync("<template><base href='/template/'></template><svg><base href='/svg/'/></svg><base href='/active/'>").ConfigureAwait(false);
            Assert.AreEqual("https://example.test/active/", document.BaseUri);
            var clone = (IDocument)document.Clone(true);
            Assert.AreEqual(document.BaseUri, clone.BaseUri);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TemplateOnlyBaseNeverChangesFallback(Boolean replaceContent)
        {
            var document = await OpenAsync("<template><base href='/template/'></template><a href='item'>Item</a>").ConfigureAwait(false);

            if (replaceContent)
            {
                document.QuerySelector("template").InnerHtml = "<base href='/replacement/'>";
            }

            Assert.AreEqual("https://example.test/start/page", document.BaseUri);
            Assert.AreEqual("https://example.test/start/item", ((IHtmlAnchorElement)document.QuerySelector("a")).Href);
        }

        [Test]
        public async Task MovingTemplateContentIntoDocumentActivatesItsBase()
        {
            var document = await OpenAsync("<template><div><base href='/activated/'></div></template>").ConfigureAwait(false);
            var template = (IHtmlTemplateElement)document.QuerySelector("template");
            document.Body.AppendChild(template.Content);
            Assert.AreEqual("https://example.test/activated/", document.BaseUri);
        }

        [Test]
        public async Task OrdinaryDomChildrenOfTemplateAreNotTemplateContent()
        {
            var document = await OpenAsync("<template></template>").ConfigureAwait(false);
            var element = document.CreateElement("base");
            element.SetAttribute("href", "/ordinary/");
            document.QuerySelector("template").AppendChild(element);
            Assert.AreEqual("https://example.test/ordinary/", document.BaseUri);
        }

        [TestCase("<base href='/inert/'>")]
        [TestCase("<div><base href='/inert/'></div>")]
        public async Task ReplacingTemplateContentPreservesAnotherBasesFrozenUrl(String content)
        {
            var document = await OpenAsync("<template></template><base href='./assets/'>").ConfigureAwait(false);
            ((Document)document).DocumentUrl.Href = "https://example.test/moved/page";
            document.QuerySelector("template").InnerHtml = content;
            Assert.AreEqual("https://example.test/start/assets/", document.BaseUri);
        }

        [Test]
        public async Task DeepInnerHtmlSubtreeWithBaseDoesNotOverflowStack()
        {
            var document = await OpenAsync(String.Empty).ConfigureAwait(false);
            var root = document.CreateElement("div");
            var parent = (Node)root;

            for (var i = 0; i < 30_000; i++)
            {
                var child = (Node)document.CreateElement("div");
                parent.AddNode(child);
                parent = child;
            }

            var baseElement = document.CreateElement("base");
            baseElement.SetAttribute("href", "/deep/");
            parent.AddNode((Node)baseElement);
            document.Body.AppendChild(root);
            Assert.AreEqual("https://example.test/deep/", document.BaseUri);
            document.Body.InnerHtml = String.Empty;
            Assert.AreEqual("https://example.test/start/page", document.BaseUri);
        }
    }
}
