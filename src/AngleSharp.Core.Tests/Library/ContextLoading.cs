namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Html.Parser;
    using AngleSharp.Io;
    using AngleSharp.Media;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
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
            Assert.AreEqual("http://localhost/", document.DocumentUri);
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
                var address = "https://anglesharp-tests.anglevisions.com/PostUrlEncodeNormal";
                var config = Configuration.Default.WithDefaultLoader();
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
                var address = "https://anglesharp-tests.anglevisions.com/PostUrlEncodeNormal";
                var config = Configuration.Default.WithDefaultLoader();
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
                var result = await form.SubmitAsync();

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
                var address = "https://anglesharp-tests.anglevisions.com/";
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);
                var anchors = document.QuerySelectorAll<IHtmlAnchorElement>("ul a");
                var anchor = anchors.FirstOrDefault(m => m.TextContent == "Header");
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
                var address = "https://anglesharp-tests.anglevisions.com/";
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);
                var anchors = document.QuerySelectorAll<IHtmlAnchorElement>("ul a");
                var anchor = anchors.FirstOrDefault(m => m.TextContent == title);
                var result = await anchor.NavigateAsync();

                Assert.IsNotNull(result);
                Assert.AreEqual(result, context.Active);
                Assert.AreEqual(title, context.Active.Title);
            }
        }

        [Test]
        public async Task ContextLoadExternalResources()
        {
            var delayRequester = new DelayRequester(100);
            var imageService = new ResourceService<IImageInfo>("image/jpeg", response => new MockImageInfo { Source = response.Address });
            var config = Configuration.Default.With(delayRequester).WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true }).With(imageService);
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(m => m.Content("<img src=whatever.jpg>"));
            var img = document.QuerySelector<IHtmlImageElement>("img");

            // an image requests nothing by itself, what shows it loads it
            Assert.AreEqual(0, delayRequester.RequestCount);
            Assert.IsFalse(img.IsCompleted);

            await img.LoadAsync(new Url(img.Source));
            Assert.AreEqual(1, delayRequester.RequestCount);
            Assert.IsTrue(img.IsCompleted);
        }

        [Test]
        public async Task ContextNoLoadExternalResources()
        {
            var delayRequester = new DelayRequester(100);
            var config = Configuration.Default.With(delayRequester).WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(m => m.Content("<img src=whatever.jpg>"));
            var img = document.QuerySelector<IHtmlImageElement>("img");
            Assert.AreEqual(0, delayRequester.RequestCount);
            Assert.IsFalse(img.IsCompleted);
        }

        [Test]
        public async Task GzipEncoding_Issue1122()
        {
            if (Helper.IsNetworkAvailable())
            {
                var address = "https://anglesharp-tests.anglevisions.com/test-cases/powerball";
                var config = Configuration.Default.WithLocaleBasedEncoding().WithPageRequester();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);

                Assert.IsNotNull(document);
                // Any value goes - it should just be readable and make sense
                Assert.AreEqual("Home | Powerball", document.Title);
                // Sure, this can break fast if they change something; but we need an indicator that we can trust
                Assert.IsNotNull(document.QuerySelector("a.u-text-transform-uppercase.c-skipnav"));
            }
        }

        [Test]
        public async Task LoadContextFromStreamLoadedShouldNotFaceBufferTooSmall()
        {
            if (Helper.IsNetworkAvailable())
            {
                var address = "https://anglesharp-tests.anglevisions.com/test-cases/kommersant";
                var config = Configuration.Default.WithPageRequester();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);

                Assert.IsNotNull(document);
                Assert.AreNotEqual(0, document.All.Length);
            }
        }

        [Test]
        public async Task LoadContextFromStreamLoadedShouldNotBeStuckForever()
        {
            if (Helper.IsNetworkAvailable())
            {
                var address = "https://anglesharp-tests.anglevisions.com/test-cases/eurobelarus";
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
            // Warning: This test might fail under certain conditions, e.g.,
            // * client uses a proxy or
            // * client is connected to VPN (at least with the VPN client of Windows 10).
            if (Helper.IsNetworkAvailable())
            {
                var address = "https://anglesharp-tests.anglevisions.com/Chunked";
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var events = new EventReceiver<HtmlParseEvent>(handler => context.GetService<IHtmlParser>().Parsing += handler);
                var start = DateTime.Now;
                events.OnReceived = _ => start = DateTime.Now;
                await context.OpenAsync(address);
                var end = DateTime.Now;
                Assert.Greater(end - start, TimeSpan.FromSeconds(1));
            }
        }

        [Test]
        public async Task ProxyShouldBeAvailableDuringLoading()
        {
            var windowIsNotNull = false;
            var scripting = new CallbackScriptEngine(options => windowIsNotNull = options.Document.DefaultView.Proxy != null);
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>Some title</title><body><script type='c-sharp' src='foo.cs'></script>";
            await BrowsingContext.New(config).OpenAsync(m =>
                m.Content(source).Address("http://www.example.com"));
            Assert.IsTrue(windowIsNotNull);
        }

        [Test]
        public async Task LoadTestPageWithAllSubresources()
        {
            if (Helper.IsNetworkAvailable())
            {
                //Formerly used the following url:
                //https://meadjohnson.world.tmall.com/search.htm?search=y&orderType=defaultSort&scene=taobao_shop
                //However: The connection to taobao is usually very bad and the
                //page takes ~10-30s (or longer!) to load. Replaced with another
                //solution taken directly from the AngleSharp infrastructure.
                var address = "https://anglesharp-tests.anglevisions.com/Page";
                var config = Configuration.Default.WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);
                Assert.IsNotNull(document);
            }
        }

        [Test]
        public async Task LoadingPdfDocumentInsteadOfHtmlShouldWork()
        {
            if (Helper.IsNetworkAvailable())
            {
                var address = "https://anglesharp-tests.anglevisions.com/test-cases/europarl";
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);
                Assert.IsNotNull(document);
            }
        }

        [Test]
        public async Task GetDownloadsOfEmptyDocumentShouldBeZero()
        {
            var config = Configuration.Default.WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });
            var document = await BrowsingContext.New(config).OpenNewAsync();
            var downloads = document.GetDownloads().ToArray();

            Assert.AreEqual(0, downloads.Length);
        }

        [Test]
        public async Task LoadEventWaitsForImagesLoadedWhileTheDocumentIsLoading()
        {
            // e.g. the images a renderer shows before the document has loaded
            var delayRequester = new DelayRequester(100);
            var imageService = new ResourceService<IImageInfo>("image/jpeg", response => new MockImageInfo { Source = response.Address });
            var config = Configuration.Default.With(delayRequester).WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true }).With(imageService)
                .WithOnly<IHtmlParser>(context =>
                {
                    var parser = new HtmlParser(new HtmlParserOptions(), context);
                    parser.Parsing += (_, ev) =>
                    {
                        var document = ((HtmlParseEvent)ev).Document;
                        document.AddEventListener(EventNames.DomContentLoaded, (_, _) =>
                        {
                            var img = document.QuerySelector<IHtmlImageElement>("img");
                            _ = img.LoadAsync(new Url(img.Source));
                        });
                    };
                    return parser;
                });
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content("<img src=whatever.jpg>"));
            var img = document.QuerySelector<IHtmlImageElement>("img");

            Assert.AreEqual(DocumentReadyState.Complete, document.ReadyState);
            Assert.AreEqual(1, delayRequester.RequestCount);
            Assert.IsTrue(img.IsCompleted);
        }

        [TestCase(1.0, 800, 600)]
        [TestCase(2.0, 400, 300)]
        [TestCase(0.5, 1600, 1200)]
        public async Task NaturalSizeOfAnImageIsDividedByItsDensity(Double density, Int32 width, Int32 height)
        {
            var imageService = new ResourceService<IImageInfo>("image/jpeg", response => new MockImageInfo { Source = response.Address, Width = 800, Height = 600 });
            var config = Configuration.Default.With(new DelayRequester(0)).WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true }).With(imageService);
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content("<img srcset=\"whatever.jpg 2x\">").Address("http://localhost/"));
            var img = document.QuerySelector<IHtmlImageElement>("img");

            await img.LoadAsync(new Url("http://localhost/whatever.jpg"), density);

            Assert.AreEqual(width, img.OriginalWidth);
            Assert.AreEqual(height, img.OriginalHeight);
        }

        [Test]
        public async Task LoadingAnImageAgainWaitsForTheLoadInFlight()
        {
            var processing = new TaskCompletionSource<IImageInfo>();
            var imageService = new SlowImageService(processing.Task);
            var config = Configuration.Default.With(new DelayRequester(0)).WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true }).With(imageService);
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content("<img>").Address("http://localhost/"));
            var img = document.QuerySelector<IHtmlImageElement>("img");
            var source = new Url("http://localhost/whatever.jpg");

            var first = img.LoadAsync(source);
            await ((ILoadableElement)img).CurrentDownload.Task;
            var second = img.LoadAsync(source);

            Assert.IsFalse(second.IsCompleted);
            processing.SetResult(new MockImageInfo { Source = source, Width = 20, Height = 10 });
            await second;
            Assert.AreEqual(20, img.OriginalWidth);
            await first;
        }

        private sealed class SlowImageService : IResourceService<IImageInfo>
        {
            private readonly Task<IImageInfo> _result;

            public SlowImageService(Task<IImageInfo> result) => _result = result;

            public Boolean SupportsType(String mimeType) => true;

            public Task<IImageInfo> CreateAsync(IResponse response, CancellationToken cancel) => _result;
        }

        [Test]
        public async Task LoadEventDoesNotWaitForImagesLoadedAfterwards()
        {
            var delayRequester = new DelayRequester(200);
            var imageService = new ResourceService<IImageInfo>("image/jpeg", response => new MockImageInfo { Source = response.Address });
            var config = Configuration.Default.With(delayRequester).WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true }).With(imageService);
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content("<img src=whatever.jpg>"));
            var img = document.QuerySelector<IHtmlImageElement>("img");
            var loaded = false;
            img.AddEventListener(EventNames.Load, (_, _) => loaded = true);

            var task = img.LoadAsync(new Url(img.Source));

            Assert.AreEqual(DocumentReadyState.Complete, document.ReadyState);
            Assert.IsFalse(img.IsCompleted);
            await task;
            Assert.IsTrue(img.IsCompleted);

            for (var i = 0; i < 50 && !loaded; i++)
            {
                await Task.Delay(20);
            }

            Assert.IsTrue(loaded);
        }

        [Test]
        public async Task GetDownloadsOfExampleDocumentWithoutCssAndJsShouldYieldPartialResources()
        {
            var config = Configuration.Default.WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });
            var content = @"<link rel=stylesheet type=text/css href=bootstraph.css>
<link rel=stylesheet type=text/css href=fontawesome.css>
<link rel=stylesheet type=text/css href=style.css>
<body>
    <img src=foo.png>
    <iframe src=foo.html></iframe>
    <script>alert('Hello World!');</script>
    <script src=test.js></script>
</body>";
            var document = await BrowsingContext.New(config).OpenAsync(res => res.Content(content).Address("http://localhost"));
            var downloads = document.GetDownloads().ToArray();

            // the image is loaded by what shows it
            Assert.AreEqual(1, downloads.Length);

            foreach (var download in downloads)
            {
                Assert.IsTrue(download.IsCompleted);
                Assert.IsNotNull(download.Source);
            }

            Assert.AreEqual(document.QuerySelector("iframe"), downloads[0].Source);
        }

        [Test]
        public async Task JavaScriptWithIntegrityAndCorsShouldBeCheckedButNotParsedIfDeclined()
        {
            var hasBeenChecked = false;
            var hasBeenParsed = false;
            var scripting = new CallbackScriptEngine(_ =>
            {
                hasBeenParsed = true;
            }, MimeTypeNames.DefaultJavaScript);
            var provider = new MockIntegrityProvider((_, _) =>
            {
                hasBeenChecked = true;
                return false;
            });
            var config = Configuration.Default.WithScripts(scripting).With(provider).WithMockRequester();
            var content = @"<body>
<script src=""https://code.jquery.com/jquery-2.2.4.js"" integrity=""sha256-iT6Q9iMJYuQiMWNd9lDyBUStIq/8PuOW33aOqmvFpqI="" crossorigin=""anonymous""></script>
</body>";
            await BrowsingContext.New(config).OpenAsync(res => res.Content(content).Address("http://localhost"));

            Assert.IsTrue(hasBeenChecked);
            Assert.IsFalse(hasBeenParsed);
        }

        [Test]
        public async Task JavaScriptWithIntegrityAndCorsShouldBeCheckedAndParsed()
        {
            var hasBeenChecked = false;
            var hasBeenParsed = false;
            var scripting = new CallbackScriptEngine(_ =>
            {
                hasBeenParsed = true;
            }, MimeTypeNames.DefaultJavaScript);
            var provider = new MockIntegrityProvider((_, _) =>
            {
                hasBeenChecked = true;
                return true;
            });
            var config = Configuration.Default.WithScripts(scripting).With(provider).WithMockRequester();
            var content = @"<body>
<script src=""https://code.jquery.com/jquery-2.2.4.js"" integrity=""sha256-iT6Q9iMJYuQiMWNd9lDyBUStIq/8PuOW33aOqmvFpqI="" crossorigin=""anonymous""></script>
</body>";
            await BrowsingContext.New(config).OpenAsync(res => res.Content(content).Address("http://localhost"));

            Assert.IsTrue(hasBeenChecked);
            Assert.IsTrue(hasBeenParsed);
        }

        [Test]
        public async Task JavaScriptWithIntegrityButNoCorsShouldNotBeChecked()
        {
            var hasBeenChecked = false;
            var hasBeenParsed = false;
            var scripting = new CallbackScriptEngine(_ =>
            {
                hasBeenParsed = true;
            }, MimeTypeNames.DefaultJavaScript);
            var provider = new MockIntegrityProvider((_, _) =>
            {
                hasBeenChecked = true;
                return false;
            });
            var config = Configuration.Default.WithScripts(scripting).With(provider).WithMockRequester();
            var content = @"<body>
<script src=""https://code.jquery.com/jquery-2.2.4.js"" integrity=""sha256-iT6Q9iMJYuQiMWNd9lDyBUStIq/8PuOW33aOqmvFpqI=""></script>
</body>";
            await BrowsingContext.New(config).OpenAsync(res => res.Content(content).Address("http://localhost"));

            Assert.IsFalse(hasBeenChecked);
            Assert.IsTrue(hasBeenParsed);
        }

        [Test]
        public async Task LoadCustomDocumentWithRegisteredHandler()
        {
            var type = "text/markdown";
            var context = BrowsingContext.New();
            var documentFactory = context.GetFactory<IDocumentFactory>() as DefaultDocumentFactory;
            documentFactory.Register(type, (ctx, options, _) => Task.FromResult<IDocument>(new MarkdownDocument(ctx, options.Source)));
            var document = await context.OpenAsync(res => res.Content("").Header(HeaderNames.ContentType, type));
            Assert.IsInstanceOf<MarkdownDocument>(document);
        }

        [Test]
        public async Task LoadCustomDocumentWithoutUnregisteredHandler()
        {
            var type = "text/markdown";
            var context = BrowsingContext.New();
            var documentFactory = context.GetFactory<IDocumentFactory>() as DefaultDocumentFactory;
            documentFactory.Register(type, (ctx, options, _) => Task.FromResult<IDocument>(new MarkdownDocument(ctx, options.Source)));
            var handler = documentFactory.Unregister(type);
            var document = await context.OpenAsync(res => res.Content("").Header(HeaderNames.ContentType, type));
            Assert.IsNotNull(handler);
            Assert.IsInstanceOf<HtmlDocument>(document);
        }

        [Test]
        public async Task LoadCustomDocumentWithoutAnyHandler()
        {
            var config = new Configuration();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res => res.Content("").Header(HeaderNames.ContentType, "text/markdown"));
            Assert.IsInstanceOf<HtmlDocument>(document);
        }

        [Test]
        public async Task FailingToCreateADocumentPropagatesWithoutAnErrorHandler()
        {
            var config = Configuration.Default.WithOnly<IDocumentFactory>(CreateFailingFactory());
            var context = BrowsingContext.New(config);

            try
            {
                await context.OpenAsync(res => res.Content("").Header(HeaderNames.ContentType, "text/markdown"));
                Assert.Fail("The error should propagate.");
            }
            catch (FormatException)
            {
            }
        }

        [Test]
        public async Task FailingToCreateADocumentIsGivenToTheErrorHandler()
        {
            var handler = new ErrorDocumentHandler();
            var config = Configuration.Default.WithOnly<IDocumentFactory>(CreateFailingFactory()).With(handler);
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res => res.Content("").Address("http://localhost/doc.md").Header(HeaderNames.ContentType, "text/markdown"));
            Assert.IsInstanceOf<FormatException>(handler.Error);
            Assert.AreEqual("http://localhost/doc.md", handler.Address);
            Assert.AreEqual("not markdown", document.Body.TextContent);
        }

        [Test]
        public async Task FailingToCreateTheDocumentOfAFrameIsGivenToTheErrorHandler()
        {
            var handler = new ErrorDocumentHandler();
            var config = Configuration.Default
                .WithOnly<IDocumentFactory>(CreateFailingFactory())
                .With(handler)
                .WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true })
                .With(new MarkdownRequester());
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res => res.Content("<iframe src='http://localhost/doc.md'></iframe>").Address("http://localhost/"));
            var iframe = document.QuerySelector<IHtmlInlineFrameElement>("iframe");
            await iframe.CurrentDownload.Task;
            Assert.AreEqual("http://localhost/doc.md", handler.Address);
            Assert.AreEqual("not markdown", iframe.ContentDocument.Body.TextContent);
        }

        [Test]
        public async Task ErrorHandlerNotHandlingTheErrorLetsItPropagate()
        {
            var config = Configuration.Default.WithOnly<IDocumentFactory>(CreateFailingFactory()).With(new ErrorDocumentHandler { IsHandling = false });
            var context = BrowsingContext.New(config);

            try
            {
                await context.OpenAsync(res => res.Content("").Header(HeaderNames.ContentType, "text/markdown"));
                Assert.Fail("The error should propagate.");
            }
            catch (FormatException)
            {
            }
        }

        private static DefaultDocumentFactory CreateFailingFactory()
        {
            var factory = new DefaultDocumentFactory();
            factory.Register("text/markdown", (ctx, options, _) => throw new FormatException("not markdown"));
            return factory;
        }

        private sealed class ErrorDocumentHandler : IDocumentErrorHandler
        {
            public Boolean IsHandling { get; set; } = true;

            public Exception Error { get; private set; }

            public String Address { get; private set; }

            public async Task<IDocument> HandleAsync(IBrowsingContext context, CreateDocumentOptions options, Exception error, System.Threading.CancellationToken cancellationToken)
            {
                Error = error;
                Address = options.Response.Address.Href;

                if (!IsHandling)
                {
                    return null;
                }

                var factory = context.GetFactory<IDocumentFactory>();
                var response = VirtualResponse.Create(res => res.Address(options.Response.Address).Content(error.Message));
                return await factory.CreateAsync(context, new CreateDocumentOptions(response, null, options.ImportAncestor), cancellationToken);
            }
        }

        private sealed class MarkdownRequester : BaseRequester
        {
            public override Boolean SupportsProtocol(String protocol) => protocol == ProtocolNames.Http;

            protected override Task<IResponse> PerformRequestAsync(Request request, System.Threading.CancellationToken cancel)
            {
                var response = VirtualResponse.Create(res => res.Address(request.Address).Content("# title").Header(HeaderNames.ContentType, "text/markdown"));
                return Task.FromResult(response);
            }
        }

        [Test]
        public async Task LoadingResourcesFromAStreamShouldNotFail()
        {
            if (Helper.IsNetworkAvailable())
            {
                var uri = "http://florian-rappl.de";
                var config = Configuration.Default.WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });

                var req = new HttpClient();
                var message = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, uri);

                using var response = await req.SendAsync(message);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(stream).Address(uri));
                    Assert.IsNotNull(document);
                }
            }
        }

        [Test]
        public async Task LoadingIframeWithEmptySourceIsLikeLoadingWithout()
        {
            var config = new Configuration().WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });
            var context = BrowsingContext.New(config);
            var source = @"<iframe src="""" class=""updates-iframe""></iframe>";
            var document = await context.OpenAsync(res => res.Content(source));
            var iframe = document.QuerySelector<HtmlIFrameElement>("iframe");
            Assert.AreEqual("about:blank", iframe.ContentDocument.Url);
        }
    }
}
