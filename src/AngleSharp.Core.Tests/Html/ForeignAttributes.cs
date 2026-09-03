namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ForeignAttributesTests
    {
        // Reference behaviour taken from Blink and WebKit; both agree on every row.
        [TestCase("<svg xml:lang[>", "xml:lang[", null, null, "")]
        [TestCase("<svg xml:base[>", "xml:base[", null, null, "")]
        [TestCase("<svg xml:space[>", "xml:space[", null, null, "")]
        [TestCase("<svg xml:lang<x=y>", "xml:lang<x", null, null, "y")]
        [TestCase("<svg xml:lang\"x=1>", "xml:lang\"x", null, null, "1")]
        [TestCase("<math xml:lang[>", "xml:lang[", null, null, "")]
        [TestCase("<svg xlink:href[>", "xlink:href[", null, null, "")]
        [TestCase("<svg xmlns:foo[>", "xmlns:foo[", null, null, "")]
        [TestCase("<svg xml:lang=\"en\">", "xml:lang", "http://www.w3.org/XML/1998/namespace", "xml", "en")]
        [TestCase("<svg xml:space=\"preserve\">", "xml:space", "http://www.w3.org/XML/1998/namespace", "xml", "preserve")]
        [TestCase("<svg xmlns=\"u\">", "xmlns", "http://www.w3.org/2000/xmlns/", null, "u")]
        [TestCase("<svg xmlns:xlink=\"u\">", "xmlns:xlink", "http://www.w3.org/2000/xmlns/", "xmlns", "u")]
        [TestCase("<svg xml:langue=\"1\">", "xml:langue", null, null, "1")]
        [TestCase("<svg xml:spaceship=\"1\">", "xml:spaceship", null, null, "1")]
        [TestCase("<svg xml:base=\"b\">", "xml:base", null, null, "b")]
        public void ForeignAttributeMatchesBrowserBehavior_Issue1294(String source, String name, String namespaceUri, String prefix, String value)
        {
            var doc = source.ToHtmlDocument();
            var element = doc.Body.FirstElementChild;
            Assert.AreEqual(1, element.Attributes.Length);

            var attr = element.Attributes.GetNamedItem(name);
            Assert.IsNotNull(attr);
            Assert.AreEqual(value, attr.Value);
            Assert.AreEqual(namespaceUri, attr.NamespaceUri);
            Assert.AreEqual(prefix, attr.Prefix);
        }

        // AngleSharp drops the prefix here, so the name deviates from the browsers - the
        // namespace and the local name still match.
        [TestCase("xlink:href", "href")]
        [TestCase("xlink:title", "title")]
        public void XLinkAttributeInAdjustmentTableIsNamespaced_Issue1294(String name, String localName)
        {
            var doc = ("<svg " + name + "=\"v\">").ToHtmlDocument();
            var attr = doc.Body.FirstElementChild.Attributes.GetNamedItem(localName);
            Assert.IsNotNull(attr);
            Assert.AreEqual("v", attr.Value);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr.NamespaceUri);
            Assert.AreEqual(null, attr.Prefix);
        }

        [TestCase("xlink:hrefs")]
        [TestCase("xlink:foo")]
        public void XLinkAttributeNotInAdjustmentTableIsNotNamespaced_Issue1294(String name)
        {
            var doc = ("<svg " + name + "=\"v\">").ToHtmlDocument();
            var attr = doc.Body.FirstElementChild.Attributes.GetNamedItem(name);
            Assert.IsNotNull(attr);
            Assert.AreEqual("v", attr.Value);
            Assert.AreEqual(null, attr.NamespaceUri);
            Assert.AreEqual(null, attr.Prefix);
        }
    }
}
