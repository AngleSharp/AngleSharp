namespace AngleSharp.Core.Tests.Xml
{
    using AngleSharp.Xml.Parser;
    using NUnit.Framework;

    [TestFixture]
    public class XmlParsing
    {
        [Test]
        public void ParseHtmlStructureInXmlShouldThrowError()
        {
            var source = @" <title>My Title</title>
 <p>My paragraph</p>";
            var parser = new XmlParser();
            Assert.Catch<XmlParseException>(() => parser.ParseDocument(source));
        }

        [Test]
        public void ParseHtmlStructureInXmlShouldNotThrowErrorIfSuppressed()
        {
            var source = @" <title>My Title</title>
 <p>My paragraph</p>";
            var parser = new XmlParser(new XmlParserOptions
            {
                IsSuppressingErrors = true
            });
            var document = parser.ParseDocument(source);
            Assert.AreEqual(1, document.Children.Length);
        }

        [Test]
        public void ParseHtmlEntityInXmlShouldThrowError()
        {
            var source = @" <title>&nbsp;</title>";
            var parser = new XmlParser();
            Assert.Catch<XmlParseException>(() => parser.ParseDocument(source));
        }

        [Test]
        public void ParseHtmlEntityInXmlShouldNotThrowErrorIfSuppressed()
        {
            var source = @" <title>&nbsp;</title>";
            var parser = new XmlParser(new XmlParserOptions
            {
                IsSuppressingErrors = true
            });
            var document = parser.ParseDocument(source);
            Assert.AreEqual(1, document.Children.Length);
        }

        [Test]
        public void ParseValidXmlEntityShouldBeRepresentedCorrectly()
        {
            var source = @" <title>&amp;</title>";
            var parser = new XmlParser(new XmlParserOptions
            {
                IsSuppressingErrors = true
            });
            var document = parser.ParseDocument(source);
            Assert.AreEqual("&", document.DocumentElement.TextContent);
        }

        [Test]
        public void ParseInvalidXmlEntityShouldBeSerialized()
        {
            var source = @" <title>&nbsp;</title>";
            var parser = new XmlParser(new XmlParserOptions
            {
                IsSuppressingErrors = true
            });
            var document = parser.ParseDocument(source);
            Assert.AreEqual("&nbsp;", document.DocumentElement.TextContent);
        }
    }
}
