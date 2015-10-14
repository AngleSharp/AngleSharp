namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom.Html;
    using AngleSharp.Events;
    using AngleSharp.Extensions;
    using NUnit.Framework;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class ContextLoadingTests
    {
        [Test]
        public async Task ContextLoadEmptyDocumentWithoutUrl()
        {
            var document = await BrowsingContext.New().OpenNewAsync();
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.DocumentElement);
            Assert.IsNotNull(document.Body);
            Assert.IsNotNull(document.Head);
            Assert.AreEqual("", document.DocumentUri);
            Assert.AreEqual(2, document.DocumentElement.ChildElementCount);
            Assert.AreEqual(0, document.Body.ChildElementCount);
            Assert.AreEqual(0, document.Head.ChildElementCount);
        }

        [Test]
        public async Task ContextLoadEmptyDocumentFromVirtualResponse()
        {
            var document = await BrowsingContext.New().OpenAsync(m => m.Address("http://anglesharp.github.io").Content("<h1>AngleSharp rocks</h1>"));
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.DocumentElement);
            Assert.IsNotNull(document.Body);
            Assert.IsNotNull(document.Head);
            Assert.AreEqual("http://anglesharp.github.io/", document.DocumentUri);
            Assert.AreEqual(2, document.DocumentElement.ChildElementCount);
            Assert.AreEqual(1, document.Body.ChildElementCount);
            Assert.AreEqual(0, document.Head.ChildElementCount);
            Assert.AreEqual("AngleSharp rocks", document.QuerySelector("h1").TextContent);
        }

        [Test]
        public async Task ContextLoadEmptyDocumentWithUrl()
        {
            var document = await BrowsingContext.New().OpenNewAsync(url: "http://localhost:8081");
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.DocumentElement);
            Assert.IsNotNull(document.Body);
            Assert.IsNotNull(document.Head);
            Assert.AreEqual("http://localhost:8081/", document.DocumentUri);
            Assert.AreEqual(2, document.DocumentElement.ChildElementCount);
            Assert.AreEqual(0, document.Body.ChildElementCount);
            Assert.AreEqual(0, document.Head.ChildElementCount);
        }

        [Test]
        public async Task ContextLoadFromUrl()
        {
            if (Helper.IsNetworkAvailable())
            {
                var title = "PostUrlencodeNormal";
                var address = "http://anglesharp.azurewebsites.net/PostUrlEncodeNormal";
                var config = new Configuration().WithDefaultLoader();
                var document = await BrowsingContext.New(config).OpenAsync(address);
                var h1 = document.QuerySelector("h1");

                Assert.IsNotNull(document);
                Assert.IsNotNull(document.DocumentElement);
                Assert.IsNotNull(document.Body);
                Assert.IsNotNull(document.Head);
                Assert.AreEqual(address, document.DocumentUri);
                Assert.AreEqual(title, document.Title);
                Assert.AreEqual(title, h1.TextContent);
            }
        }

        [Test]
        public async Task ContextFormSubmission()
        {
            if (Helper.IsNetworkAvailable())
            {
                var address = "http://anglesharp.azurewebsites.net/PostUrlEncodeNormal";
                var config = new Configuration().WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);

                Assert.AreEqual(1, document.Forms.Length);

                var form = document.Forms[0];
                var name = form.Elements["Name"] as IHtmlInputElement;
                var number = form.Elements["Number"] as IHtmlInputElement;
                var isactive = form.Elements["IsActive"] as IHtmlInputElement;

                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.AreEqual("text", name.Type);
                Assert.AreEqual("number", number.Type);
                Assert.AreEqual("checkbox", isactive.Type);

                name.Value = "Test";
                number.Value = "1";
                isactive.IsChecked = true;
                var result = await form.Submit();

                Assert.IsNotNull(result);
                Assert.AreEqual(result, context.Active);
                Assert.AreEqual("okay", context.Active.Body.TextContent);
            }
        }

        [Test]
        public async Task ContextNavigateFromLinkRefererShouldBeOriginalUrl()
        {
            if (Helper.IsNetworkAvailable())
            {
                var address = "http://anglesharp.azurewebsites.net/";
                var config = new Configuration().WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);
                var anchors = document.QuerySelectorAll<IHtmlAnchorElement>("ul a");
                var anchor = anchors.Where(m => m.TextContent == "Header").FirstOrDefault();
                var result = await context.OpenAsync(Url.Create(anchor.Href));

                Assert.IsNotNull(result);
                Assert.AreEqual(result, context.Active);
                Assert.AreEqual(address, context.Active.Body.TextContent);
            }
        }

        [Test]
        public async Task ContextNavigateFromLinkToPage()
        {
            if (Helper.IsNetworkAvailable())
            {
                var title = "PostUrlencodeNormal";
                var address = "http://anglesharp.azurewebsites.net/";
                var config = new Configuration().WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);
                var anchors = document.QuerySelectorAll<IHtmlAnchorElement>("ul a");
                var anchor = anchors.Where(m => m.TextContent == title).FirstOrDefault();
                var result = await anchor.Navigate();

                Assert.IsNotNull(result);
                Assert.AreEqual(result, context.Active);
                Assert.AreEqual(title, context.Active.Title);
            }
        }

        [Test]
        public async Task ContextLoadAmazonWithCss()
        {
            var address = "http://www.amazon.com";
            var config = new Configuration().WithPageRequester().WithCss();
            var document = await BrowsingContext.New(config).OpenAsync(address);
            await Task.WhenAll(document.Requests);
            Assert.IsNotNull(document);
            Assert.AreNotEqual(0, document.Body.ChildElementCount);
        }

        [Test]
        public async Task ContextLoadExternalResources()
        {
            var delayRequester = new DelayRequester(100);
            var config = new Configuration().WithDefaultLoader(m => m.IsResourceLoadingEnabled = true, new[] { delayRequester });
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(m => m.Content("<img src=whatever.jpg>"));
            Assert.AreEqual(0, document.Requests.Count());
        }

        [Test]
        public async Task ContextNoLoadExternalResources()
        {
            var delayRequester = new DelayRequester(100);
            var config = new Configuration().WithDefaultLoader(requesters: new[] { delayRequester });
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(m => m.Content("<img src=whatever.jpg>"));
            Assert.AreEqual(0, document.Requests.Count());
        }

        [Test]
        public async Task ContextLoadPageWithCssAndNoLoaders()
        {
            var url = "http://localhost";
            var source = "<!doctype html><link rel=stylesheet href=http://localhost/beispiel.css type=text/css />";
            var memory = new MemoryStream(Encoding.UTF8.GetBytes(source));
            var config = new Configuration(null, null, null).WithCss();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(m => m.Content(memory).Address(url));
            var links = document.QuerySelectorAll("link");
            Assert.AreEqual(1, links.Length);
            var link = links[0] as IHtmlLinkElement;
            Assert.NotNull(link);
            Assert.AreEqual("http://localhost/beispiel.css", link.Href);
        }

        [Test]
        public async Task CheckIfAllStyleSheetsAreProcessed()
        {
            var html = @"<html>
  <head>
     <title>test title</title>
     <link href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css' rel='stylesheet'>
    </head>
    <body>
    </body>
</html>";

            var config = new Configuration().WithPageRequester(enableResourceLoading: true).WithCss();
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(html));
            Assert.AreEqual(1, document.StyleSheets.Length);
        }

        [Test]
        public async Task LoadContextFromStreamLoadedShouldNotFaceBufferTooSmall()
        {
            var address = "http://kommersant.ru/rss-list";
            var config = Configuration.Default.WithPageRequester();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);

            Assert.IsNotNull(document);
            Assert.AreNotEqual(0, document.All.Length);
        }

        [Test]
        public async Task LoadContextFromStreamLoadedShouldNotBeStuckForever()
        {
            if (Helper.IsNetworkAvailable())
            {
                var address = "http://eurobelarus.info/";
                var config = Configuration.Default.WithPageRequester();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);

                Assert.IsNotNull(document);
                Assert.AreNotEqual(0, document.All.Length);
            }
        }

        [Test]
        public async Task LoadContextFromStreamChunked()
        {
            if (Helper.IsNetworkAvailable())
            {
                var address = "http://anglesharp.azurewebsites.net/Chunked";
                var events = new EventReceiver<HtmlParseStartEvent>();
                var config = new Configuration(events: events).WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var start = DateTime.Now;
                events.OnReceived = rec => start = DateTime.Now;
                var document = await context.OpenAsync(address);
                var end = DateTime.Now;
                Assert.Greater(end - start, TimeSpan.FromSeconds(1));
            }
        }

        [Test]
        public async Task ProxyShouldBeAvailableDuringLoading()
        {
            var windowIsNotNull = false;
            var scripting = new CallbackScriptEngine(options => windowIsNotNull = options.Context.Proxy != null);
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>Some title</title><body><script type='c-sharp' src='foo.cs'></script>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));
            Assert.IsTrue(windowIsNotNull);
        }

        [Test]
        public async Task LoadTaobaoWithAllSubresources()
        {
            if (Helper.IsNetworkAvailable())
            {
                var address = "https://meadjohnson.world.tmall.com/search.htm?search=y&orderType=defaultSort&scene=taobao_shop";
                var config = Configuration.Default.WithDefaultLoader(setup => setup.IsResourceLoadingEnabled = true);
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);
                Assert.IsNotNull(document);
            }
        }
    }
}
