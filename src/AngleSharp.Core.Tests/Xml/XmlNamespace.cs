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
    }
}
