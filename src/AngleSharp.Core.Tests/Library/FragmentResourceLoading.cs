namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestFixture]
    public class FragmentResourceLoadingTests
    {
        private static async Task<(IDocument Document, List<String> Requests)> CreateDocumentAsync()
        {
            var requests = new List<String>();
            var requester = new MockRequester { OnRequest = request => requests.Add(request.Address.Href) };
            var config = Configuration.Default.With(requester).WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content("<!doctype html><body></body>").Address("https://example.com/page/"));
            return (document, requests);
        }

        [Test]
        public async Task ImageOfInnerHtmlLoadsAgainstTheDocumentAddress()
        {
            var (document, requests) = await CreateDocumentAsync();

            document.Body.InnerHtml = "<p><img src=images/logo.png></p>";
            await Task.Delay(50);

            Assert.That(requests, Is.EqualTo(new[] { "https://example.com/page/images/logo.png" }));
        }

        [Test]
        public async Task ImageOfFragmentLoadsOnceAdopted()
        {
            var (document, requests) = await CreateDocumentAsync();
            var scratch = document.CreateElement("div");

            scratch.InnerHtml = "<img src=/images/a.png><img srcset=\"/images/b.png 1x\">";
            await Task.Delay(50);

            Assert.That(requests, Is.EqualTo(new[] { "https://example.com/images/a.png", "https://example.com/images/b.png" }));
            Assert.That(document.QuerySelectorAll("img").Length, Is.EqualTo(0));
        }

        [Test]
        public async Task AdoptingAnImageResolvesItsSourceAgain()
        {
            var (document, requests) = await CreateDocumentAsync();
            var other = await BrowsingContext.New(Configuration.Default).OpenAsync(m => m.Content("<img src=x.png>").Address("https://other.example/"));
            var image = other.QuerySelector("img");

            document.Body.AppendChild(image);
            await Task.Delay(50);

            Assert.That(requests, Does.Contain("https://example.com/page/x.png"));
        }

        [TestCase("<iframe src=frame.html></iframe>", "https://example.com/page/frame.html")]
        [TestCase("<object data=movie.swf></object>", "https://example.com/page/movie.swf")]
        [TestCase("<embed src=movie.swf>", "https://example.com/page/movie.swf")]
        [TestCase("<video src=clip.mp4></video>", "https://example.com/page/clip.mp4")]
        public async Task ResourceOfInnerHtmlLoadsAgainstTheDocumentAddress(String html, String expected)
        {
            var (document, requests) = await CreateDocumentAsync();

            document.Body.InnerHtml = "<div>" + html + "</div>";
            await Task.Delay(100);

            Assert.That(requests, Is.EqualTo(new[] { expected }));
        }

        [TestCase("<iframe src=frame.html></iframe>", "https://example.com/page/frame.html")]
        [TestCase("<object data=movie.swf></object>", "https://example.com/page/movie.swf")]
        [TestCase("<embed src=movie.swf>", "https://example.com/page/movie.swf")]
        public async Task FrameOrObjectOfDetachedFragmentLoadsOnceInTheDocument(String html, String expected)
        {
            var (document, requests) = await CreateDocumentAsync();
            var scratch = document.CreateElement("div");

            scratch.InnerHtml = "<p>" + html + "</p>";
            await Task.Delay(100);
            Assert.That(requests, Is.Empty);

            document.Body.AppendChild(scratch);
            await Task.Delay(100);
            Assert.That(requests, Is.EqualTo(new[] { expected }));
        }

        [Test]
        public async Task FrameMadeByScriptLoadsOnceAppended()
        {
            var (document, requests) = await CreateDocumentAsync();
            var frame = document.CreateElement("iframe");

            frame.SetAttribute("src", "frame.html");
            await Task.Delay(100);
            Assert.That(requests, Is.Empty);

            document.Body.AppendChild(frame);
            await Task.Delay(100);
            Assert.That(requests, Is.EqualTo(new[] { "https://example.com/page/frame.html" }));
        }

        [Test]
        public async Task FrameOfParsedDocumentLoadsOnce()
        {
            var requests = new List<String>();
            var requester = new MockRequester { OnRequest = request => requests.Add(request.Address.Href) };
            var config = Configuration.Default.With(requester).WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });

            await BrowsingContext.New(config).OpenAsync(m => m.Content("<!doctype html><body><div><iframe src=frame.html></iframe></div>").Address("https://example.com/page/"));
            await Task.Delay(100);

            Assert.That(requests, Is.EqualTo(new[] { "https://example.com/page/frame.html" }));
        }
    }
}
