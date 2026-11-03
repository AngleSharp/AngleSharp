namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    [TestFixture]
    public class HyperlinkStringifierTests
    {
        private static Task<IDocument> OpenAsync(String content) =>
            BrowsingContext.New(Configuration.Default).OpenAsync(m => m.Address("https://links.example/dir/page").Content(content));

        [TestCase("a")]
        [TestCase("area")]
        public async Task HyperlinksGiveTheirResolvedUrl(String tag)
        {
            var document = await OpenAsync($"<!doctype html><{tag} id=link href=other?q=1#top></{tag}>");

            var element = document.GetElementById("link");

            Assert.AreEqual("https://links.example/dir/other?q=1#top", element.ToString());
        }

        [TestCase("a")]
        [TestCase("area")]
        public async Task HyperlinksWithoutHrefGiveAnEmptyString(String tag)
        {
            var document = await OpenAsync($"<!doctype html><{tag} id=link></{tag}>");

            Assert.AreEqual(String.Empty, document.GetElementById("link").ToString());
        }

        [Test]
        public async Task TheStringFollowsTheHref()
        {
            var document = await OpenAsync("<!doctype html><a id=link href=first></a>");
            var anchor = (IHtmlAnchorElement)document.GetElementById("link");

            anchor.Href = "https://other.example/second";

            Assert.AreEqual("https://other.example/second", anchor.ToString());
        }
    }
}
