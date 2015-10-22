namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    [TestFixture]
    public class PageImportTests
    {
        [Test]
        public async Task ImportPageFromVirtualRequest()
        {
            var requester = new MockRequester();
            var receivedRequest = new TaskCompletionSource<String>();
            requester.OnRequest = request => receivedRequest.SetResult(request.Address.Href);
            var config = Configuration.Default.WithDefaultLoader(setup => setup.IsResourceLoadingEnabled = true, new[] { requester });

            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content("<!doctype html><link rel=import href=http://example.com/test.html>"));
            var link = document.QuerySelector<IHtmlLinkElement>("link");
            var result = await receivedRequest.Task;

            Assert.AreEqual("import", link.Relation);
            Assert.IsNotNull(link.Import);
            Assert.AreEqual("http://example.com/test.html", result);
        }

        [Test]
        public async Task ImportPageFromDataRequest()
        {
            var receivedRequest = new TaskCompletionSource<Boolean>();
            var config = Configuration.Default.WithDefaultLoader(setup =>
            {
                setup.IsResourceLoadingEnabled = true;
                setup.Filter = request =>
                {
                    receivedRequest.SetResult(true);
                    return true;
                };
            });

            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content("<!doctype html><link rel=import href='data:text/html,<div>foo</div>'>"));
            var link = document.QuerySelector<IHtmlLinkElement>("link");
            var finished = await receivedRequest.Task;

            Assert.AreEqual("import", link.Relation);
            Assert.IsNotNull(link.Import);
            Assert.AreEqual("foo", link.Import.QuerySelector("div").TextContent);
        }
    }
}
