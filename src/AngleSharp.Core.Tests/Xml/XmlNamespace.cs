namespace AngleSharp.Core.Tests.Xml
{
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class XmlNamespaceTests
    {
        [Test]
        public async Task XmlWithoutNamespaceUriShouldBeStandardNamespaceUri()
        {
            var document = await BrowsingContext.New().OpenAsync(req =>
                req.Content(@"<a t='42'><b/><c>TEXT</c></a>")
                   .Header("Content-Type", "text/xml"));
            var root = document.DocumentElement;
            Assert.AreEqual(null, root.NamespaceUri);
        }

        [Test]
        public async Task XmlWithNewNamespaceShouldContainRightNamespaceUri()
        {
            var document = await BrowsingContext.New().OpenAsync(req =>
              req.Content(@"<a xmlns=""http://www.w3.org/1999/xhtml"" t=""42""><b/><c>TEXT</c></a>")
                 .Header("Content-Type", "text/xml"));
            var root = document.DocumentElement;
            Assert.AreEqual("http://www.w3.org/1999/xhtml", root.NamespaceUri);
        }

        [Test]
        public async Task XmlWithCustomNamespaceShouldExposeThatNamespaceUri()
        {
            var document = await BrowsingContext.New().OpenAsync(req =>
                req.Content(@"<x:a xmlns:x=""http://initd.org/ns/tesseract-1.0"" t=""42""><b/><c>TEXT</c></x:a>")
                   .Header("Content-Type", "text/xml"));
            var root = document.DocumentElement;
            Assert.AreEqual("http://initd.org/ns/tesseract-1.0", root.NamespaceUri);
        }

        [Test]
        public async Task XmlSubElementsGetTheRightDefaultNamespace()
        {
            var document = await BrowsingContext.New().OpenAsync(req =>
                req.Content(@"<xml xmlns=""http://test.com""><child attr=""1""/></xml>")
                   .Header("Content-Type", "text/xml"));
            var root = document.DocumentElement;
            Assert.AreEqual("http://test.com", root.NamespaceUri);
            Assert.AreEqual("http://test.com", root.LookupNamespaceUri(""));
            Assert.AreEqual("http://test.com", root.FirstElementChild.NamespaceUri);
            Assert.AreEqual("http://test.com", root.FirstElementChild.LookupNamespaceUri(""));
            Assert.AreEqual(null, root.FirstElementChild.Attributes["attr"].NamespaceUri);
        }

        [Test]
        public async Task XmlPrefixRefersToDefinedNamespace()
        {
            var document = await BrowsingContext.New().OpenAsync(req =>
                req.Content(@"<xml xmlns:p1=""http://p1.com"" xmlns:p2=""http://p2.com""><p1:child a=""1"" p1:attr=""1"" b=""2""/><p2:child/></xml>")
                   .Header("Content-Type", "text/xml"));
            var root = document.DocumentElement;
            Assert.AreEqual("http://p1.com", root.FirstElementChild.NamespaceUri);
            Assert.AreEqual("http://p1.com", root.LookupNamespaceUri("p1"));
            Assert.AreEqual(null, root.FirstElementChild.GetAttribute("attr"));
            Assert.AreEqual("http://p1.com", root.FirstElementChild.Attributes["p1:attr"].NamespaceUri);
            Assert.AreEqual("http://p2.com", root.FirstElementChild.NextElementSibling.NamespaceUri);
            Assert.AreEqual("http://p2.com", root.FirstElementChild.NextElementSibling.LookupNamespaceUri("p2"));
        }

        [Test]
        public async Task XmlRedefinitionOfPrefixedNamespace()
        {
            var document = await BrowsingContext.New().OpenAsync(req =>
                req.Content(@"<xml xmlns:p=""http://test.com""><p:child xmlns:p=""http://p.com""/><p:child/></xml>")
                   .Header("Content-Type", "text/xml"));
            var root = document.DocumentElement;
            Assert.AreEqual("http://p.com", root.FirstElementChild.NamespaceUri);
            Assert.AreEqual("http://test.com", root.LastElementChild.NamespaceUri);
            Assert.AreEqual("http://test.com", root.FirstElementChild.NextElementSibling.LookupNamespaceUri("p"));
        }
    }
}
