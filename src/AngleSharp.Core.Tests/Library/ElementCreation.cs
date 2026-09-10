namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ElementCreationTests
    {
        // DOM createElementNS preserves the extracted local name in every document type.
        [Test]
        public void HtmlNamespacePreservesLocalName(
            [Values("I", "ABC", "DiV", "div", "title", "unknown", "X-Widget", "\u0130", "\u212A")] String name,
            [Values(false, true)] Boolean xml,
            [Values(null, "h")] String prefix)
        {
            var document = CreateDocument(xml);
            var qualifiedName = prefix is null ? name : prefix + ":" + name;
            var element = document.CreateElement(NamespaceNames.HtmlUri, qualifiedName);

            Assert.AreEqual(name, element.LocalName);
            Assert.AreEqual(prefix, element.Prefix);
            Assert.AreEqual(NamespaceNames.HtmlUri, element.NamespaceUri);
        }

        // HTML's element-interface lookup uses the exact local name.
        [TestCase("div", typeof(IHtmlDivElement))]
        [TestCase("input", typeof(IHtmlInputElement))]
        [TestCase("title", typeof(IHtmlTitleElement))]
        [TestCase("DiV", typeof(IHtmlUnknownElement))]
        [TestCase("INPUT", typeof(IHtmlUnknownElement))]
        [TestCase("TITLE", typeof(IHtmlUnknownElement))]
        [TestCase("ABC", typeof(IHtmlUnknownElement))]
        public void HtmlNamespaceSelectsInterfaceByExactName(String name, Type type)
        {
            var document = String.Empty.ToHtmlDocument();
            var element = document.CreateElement(NamespaceNames.HtmlUri, name);

            Assert.IsInstanceOf(type, element);
            Assert.AreEqual(name, element.LocalName);
        }

        [TestCase("DIV", "div", typeof(IHtmlDivElement))]
        [TestCase("TITLE", "title", typeof(IHtmlTitleElement))]
        [TestCase("ABC", "abc", typeof(IHtmlUnknownElement))]
        [TestCase("\u0130", "\u0130", typeof(IHtmlUnknownElement))]
        public void OrdinaryHtmlCreationStillUsesAsciiLowercase(String name, String expected, Type type)
        {
            var document = String.Empty.ToHtmlDocument();
            var element = document.CreateElement(name);

            Assert.AreEqual(expected, element.LocalName);
            Assert.IsInstanceOf(type, element);
        }

        [Test]
        public void ParserStillNormalizesHtmlNames()
        {
            var document = "<!doctype html><TITLE>t</TITLE><DIV><ABC></ABC></DIV>".ToHtmlDocument();

            Assert.IsInstanceOf<IHtmlTitleElement>(document.Head.FirstElementChild);
            Assert.IsInstanceOf<IHtmlDivElement>(document.Body.FirstElementChild);
            Assert.AreEqual("div", document.Body.FirstElementChild.LocalName);
            Assert.AreEqual("abc", document.Body.FirstElementChild.FirstElementChild.LocalName);
        }

        [Test]
        public void CloneAndImportPreserveHtmlNamespaceIdentity(
            [Values("I", "ABC", "DiV", "div", "title")] String name,
            [Values(false, true)] Boolean deep)
        {
            var document = String.Empty.ToHtmlDocument();
            var element = document.CreateElement(NamespaceNames.HtmlUri, "h:" + name);
            element.SetAttribute("data-test", "value");
            element.AppendChild(document.CreateTextNode("child"));
            var target = String.Empty.ToHtmlDocument();

            foreach (var copy in new[] { (IElement)element.Clone(deep), (IElement)target.Import(element, deep) })
            {
                Assert.AreEqual(name, copy.LocalName);
                Assert.AreEqual("h", copy.Prefix);
                Assert.AreEqual(NamespaceNames.HtmlUri, copy.NamespaceUri);
                Assert.AreEqual(element.GetType(), copy.GetType());
                Assert.AreEqual("value", copy.GetAttribute("data-test"));
                Assert.AreEqual(deep ? "child" : String.Empty, copy.TextContent);
            }
        }

        [Test]
        public void NamespaceCreationUsesConfiguredHtmlFactory()
        {
            var factory = new RecordingHtmlFactory();
            var config = Configuration.Default.WithOnly<IElementFactory<Document, HtmlElement>>(factory);
            var document = String.Empty.ToHtmlDocument(config);
            var element = document.CreateElement(NamespaceNames.HtmlUri, "h:DiV");

            Assert.AreEqual("DiV", factory.LocalName);
            Assert.AreEqual("h", factory.Prefix);
            Assert.AreSame(factory.Element, element);
        }

        private static IDocument CreateDocument(Boolean xml)
        {
            // Core's XML-content Document base is also used by the XML plugin.
            return xml ? new MarkdownDocument(BrowsingContext.New(), new TextSource(String.Empty)) : String.Empty.ToHtmlDocument();
        }

        private sealed class RecordingHtmlFactory : IElementFactory<Document, HtmlElement>
        {
            public String LocalName { get; private set; }
            public String Prefix { get; private set; }
            public HtmlElement Element { get; private set; }

            public HtmlElement Create(Document document, String localName, String prefix = null, NodeFlags flags = NodeFlags.None)
            {
                LocalName = localName;
                Prefix = prefix;
                Element = HtmlElementFactory.Instance.Create(document, localName, prefix, flags);
                return Element;
            }
        }
    }
}
