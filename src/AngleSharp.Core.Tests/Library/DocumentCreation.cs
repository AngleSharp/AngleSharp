namespace AngleSharp.Core.Tests.Library
{
    using Dom;
    using Dom.Html;
    using Dom.Svg;
    using Dom.Xml;
    using Network;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    [TestFixture]
    public class DocumentCreationTests
    {
        static readonly String XmlContent = @"<note>
<to>Tove</to>
<from>Jani</from>
<heading>Reminder</heading>
<body>Don't forget me this weekend!</body>
</note>";

        static readonly String SvgContent = @"
<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 100 100"">
  <path d=""M34,93l11,-29a15,15 0,1,1 9,0l11,29a45,45 0,1,0 -31,0z"" stroke=""#142"" stroke-width=""2"" fill=""#4a5"" />
</svg>";

        static readonly String HtmlContent = @"<html><head><title>Example</title></head>
<body>This is some test &amp; another sample content.</body></html>";

        static readonly String TextContent = @"Hi Mum & Dad! You know that 3<pi, right? Or is it that me > you?";

        [Test]
        public async Task GenerateDocumentFromXmlWithXmlContentType()
        {
            var document = await GenerateDocument(XmlContent, "text/xml");

            Assert.IsInstanceOf<XmlDocument>(document);
            Assert.AreEqual("note", document.DocumentElement.NodeName);
        }

        [Test]
        public async Task GenerateDocumentFromXmlWithXHtmlContentType()
        {
            var document = await GenerateDocument(XmlContent, "application/xhtml+xml");

            Assert.IsInstanceOf<HtmlDocument>(document);
            Assert.AreEqual("HTML", document.DocumentElement.NodeName);
            Assert.AreEqual("NOTE", document.Body.FirstElementChild.NodeName);
        }

        [Test]
        public async Task GenerateDocumentFromSvgWithSvgContentType()
        {
            var document = await GenerateDocument(SvgContent, "image/svg+xml");

            Assert.IsInstanceOf<SvgDocument>(document);
            Assert.AreEqual("svg", document.DocumentElement.NodeName);
            Assert.AreEqual("path", document.DocumentElement.FirstElementChild.NodeName);
        }

        [Test]
        public async Task GenerateDocumentFromSvgWithHtmlContentType()
        {
            var document = await GenerateDocument(SvgContent, "text/html");

            Assert.IsInstanceOf<HtmlDocument>(document);
            Assert.AreEqual("HTML", document.DocumentElement.NodeName);
            Assert.AreEqual("svg", document.Body.FirstElementChild.NodeName);
            Assert.AreEqual("path", document.Body.FirstElementChild.FirstElementChild.NodeName);
        }

        [Test]
        public async Task GenerateDocumentFromHtmlWithHtmlContentType()
        {
            var document = await GenerateDocument(HtmlContent, "text/html");

            Assert.IsInstanceOf<HtmlDocument>(document);
            Assert.AreEqual("This is some test & another sample content.", document.Body.TextContent);
        }

        [Test]
        public async Task GenerateDocumentFromHtmlWithNoContentType()
        {
            var document = await GenerateDocument(HtmlContent, null);

            Assert.IsInstanceOf<HtmlDocument>(document);
            Assert.AreEqual("This is some test & another sample content.", document.Body.TextContent);
        }

        [Test]
        public async Task GenerateDocumentFromHtmlWithTextContentType()
        {
            var document = await GenerateDocument(HtmlContent, "text/plain");

            Assert.IsInstanceOf<HtmlDocument>(document);
            Assert.AreEqual("PRE", document.Body.FirstElementChild.NodeName);
            Assert.AreEqual(HtmlContent, document.Body.FirstElementChild.TextContent);
        }

        [Test]
        public async Task GenerateDocumentFromTextWithHtmlContentType()
        {
            var document = await GenerateDocument(TextContent, "text/html");

            Assert.IsInstanceOf<HtmlDocument>(document);
            Assert.AreEqual("Hi Mum & Dad! You know that 3 you?", document.Body.TextContent);
        }

        [Test]
        public async Task GenerateDocumentFromTextWithTextContentType()
        {
            var document = await GenerateDocument(TextContent, "text/plain");

            Assert.IsInstanceOf<HtmlDocument>(document);
            Assert.AreEqual("PRE", document.Body.FirstElementChild.NodeName);
            Assert.AreEqual(TextContent, document.Body.FirstElementChild.TextContent);
        }

        static Task<IDocument> GenerateDocument(String content, String contentType)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            return context.OpenAsync(res =>
            {
                res.Content(content);

                if (!String.IsNullOrEmpty(contentType))
                {
                    res.Header(HeaderNames.ContentType, contentType);
                }
            });
        }
    }
}
