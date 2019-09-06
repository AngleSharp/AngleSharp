namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    [TestFixture]
    public class CharacterReferenceTests
    {
        [Test]
        public void ParseNormalCharacterReference()
        {
            var source = "<div>&lt;</div>";
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsNotConsumingCharacterReferences = false,
            });
            var document = parser.ParseDocument(source);
            var content = document.QuerySelector("div").TextContent;
            Assert.AreEqual("<", content);
        }

        [Test]
        public void ParseAvoidedCharacterReference()
        {
            var source = "<div>&lt;</div>";
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsNotConsumingCharacterReferences = true,
            });
            var document = parser.ParseDocument(source);
            var content = document.QuerySelector("div").TextContent;
            Assert.AreEqual("&lt;", content);
        }
    }
}
