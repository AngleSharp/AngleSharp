namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp;
    using AngleSharp.Browser;
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Html.Parser;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class BasicScriptingTests
    {
        [Test]
        public void DocumentWriteDynamicallyWithCustomScriptEngineAndText()
        {
            if (TestRuntime.UsePrefetchedTextSource)
            {
                Assert.Ignore("Prefetched text source is read only");
            }

            var scripting = new CallbackScriptEngine(options => options.Document.Write("<b>Dynamically written</b>"));
            var config = Configuration.Default.WithScripts(scripting);
            var source = "<title>Some title</title><body><script type='c-sharp'>//...</script>";
            var document = source.ToHtmlDocument(config);
            var bold = document.QuerySelector("b");

            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Body.TextContent);
            Assert.AreEqual("//...Dynamically written", document.Body.TextContent);
            Assert.AreEqual(1, document.QuerySelectorAll("b").Length);
            Assert.AreEqual("Dynamically written", bold.TextContent);
        }

        [Test]
        public void ChangeTitleDynamicallyWithCustomScriptEngineScriptElementInjectedLater()
        {
            var expectedTitle = "Other title";
            var scripting = new CallbackScriptEngine(options => options.Document.Title = expectedTitle);
            var config = Configuration.Default.WithScripts(scripting);
            var source = "<title>Original title</title>";
            var document = source.ToHtmlDocument(config);

            var script = document.CreateElement("script");
            script.SetAttribute("type", "c-sharp");
            script.TextContent = "// ...";
            document.Body.AppendChild(script);

            Assert.AreEqual(expectedTitle, document.Title);
        }

        [Test]
        public async Task DocumentWriteDynamicallyWithCustomScriptEngineAndSource()
        {
            var baseAddress = "http://www.example.com";
            var filename = "foo.cs";
            var hasFoo = false;
            var scripting = new CallbackScriptEngine(options => options.Document.Write("<b>Dynamically written</b>"));
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester(request => hasFoo = request.Address.Href == baseAddress + "/" + filename);
            var source = "<title>Some title</title><body><script type='c-sharp' src='" + filename + "'></script>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address(baseAddress));
            var bold = document.QuerySelector("b");

            Assert.IsTrue(hasFoo);
            Assert.AreEqual("Dynamically written", document.Body.TextContent);
            Assert.AreEqual(1, document.QuerySelectorAll("b").Length);
            Assert.AreEqual("Dynamically written", bold.TextContent);
        }

        [Test]
        public async Task DocumentWriteDynamicallyWithCustomScriptEngineAndSourceNested()
        {
            var index = 0;
            var content = new[]
            {
                "<script type='c-sharp' src='foo2.cs'></script>",
                "<b>Dynamically written</b>"
            };
            var scripting = new CallbackScriptEngine(options => options.Document.Write(content[index++]));
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>Some title</title><body><script type='c-sharp' src='foo.cs'></script>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));
            var bold = document.QuerySelector("b");

            Assert.AreEqual("Dynamically written", document.Body.TextContent);
            Assert.AreEqual(1, document.QuerySelectorAll("b").Length);
            Assert.AreEqual("Dynamically written", bold.TextContent);
            Assert.AreEqual(2, index);
        }

        [Test]
        public async Task DocumentWriteConsecutiveWithCustomScriptEngine()
        {
            var scripting = new CallbackScriptEngine(options =>
            {
                options.Document.Write("foo");
                options.Document.Write("bar");
            });
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>Some title</title><body><script type='c-sharp' src='foo.cs'></script>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));

            Assert.AreEqual("foobar", document.Body.TextContent);
        }

        [Test]
        public async Task DocumentWriteDynamicallyWithCustomScriptEngineAndSourceMultipleNested()
        {
            var index = 0;
            var content = new[]
            {
                "<script type='c-sharp' src='foo2.cs'></script>",
                "<script type='c-sharp' src='foo3.cs'></script>",
                "<script type='c-sharp' src='foo4.cs'></script>",
                "<script type='c-sharp' src='foo5.cs'></script><b>dynamically written</b>",
                "This is "
            };
            var scripting = new CallbackScriptEngine(options => options.Document.Write(content[index++]));
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>Some title</title><body><script type='c-sharp' src='foo.cs'></script>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));
            var bold = document.QuerySelector("b");

            Assert.AreEqual("This is dynamically written", document.Body.TextContent);
            Assert.AreEqual(1, document.QuerySelectorAll("b").Length);
            Assert.AreEqual("dynamically written", bold.TextContent);
            Assert.AreEqual(5, index);
        }

        [Test]
        public async Task CustomScriptEngineHookToDomContentLoadedFromWindow()
        {
            var scripting = new CallbackScriptEngine(options =>
            {
                options.Document.DefaultView.AddEventListener(EventNames.DomContentLoaded, (_, _) =>
                {
                    options.Document.Title = "B";
                });
            });
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>A</title><body><script type='c-sharp' src='foo.cs'></script>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));

            Assert.AreEqual("B", document.Title);
        }

        [Test]
        public async Task CustomScriptEngineHookToDomContentLoadedFromDocument()
        {
            var scripting = new CallbackScriptEngine(options =>
            {
                options.Document.AddEventListener(EventNames.DomContentLoaded, (_, _) =>
                {
                    options.Document.Title = "B";
                });
            });
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>A</title><body><script type='c-sharp' src='foo.cs'></script>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));

            Assert.AreEqual("B", document.Title);
        }

        [Test]
        public async Task CustomScriptEngineHookToLoadFromWindow()
        {
            var scripting = new CallbackScriptEngine(options =>
            {
                options.Document.DefaultView.AddEventListener(EventNames.Load, (_, _) =>
                {
                    options.Document.Title = "B";
                });
            });
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>A</title><body><script type='c-sharp' src='foo.cs'></script>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));

            Assert.AreEqual("B", document.Title);
        }

        [Test]
        public async Task CustomScriptEngineHookToLoadFromDocument()
        {
            var scripting = new CallbackScriptEngine(options =>
            {
                options.Document.AddEventListener(EventNames.Load, (_, _) =>
                {
                    options.Document.Title = "B";
                });
            });
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>A</title><body><script type='c-sharp' src='foo.cs'></script>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));

            Assert.AreEqual("B", document.Title);
        }

        [Test]
        public async Task DocumentLoadExternalJavaScriptJqueryFromDifferentDomain()
        {
            if (Helper.IsNetworkAvailable())
            {
                var source = "<!doctype html><html><script src='https://code.jquery.com/jquery-2.1.4.min.js'></script>";
                var engine = new ContentScriptEngine();
                var config = Configuration.Default.WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true }).WithScripts(engine);
                var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));

                Assert.AreEqual(1, engine.Requests.Count);
                Assert.IsTrue(engine.Requests[0].Item1.StartsWith("/*! jQuery v2.1.4 | (c) 2005, 2015 jQuery Foundation, Inc. | jquery.org/license */"));
                Assert.AreEqual(document, engine.Requests[0].Item2.Document);
                Assert.AreEqual(Encoding.UTF8.WebName, engine.Requests[0].Item2.Encoding.WebName);
            }
        }

        [Test]
        public async Task DynamicallyAddedScriptWithTextContentShouldBeExecutedAfterAppending()
        {
            var didRun = false;
            var scripting = new CallbackScriptEngine(_ => didRun = true);
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>Some title</title><body>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));

            var script = document.CreateElement("script");
            script.SetAttribute("type", scripting.Type);
            script.TextContent = "my C# script";

            Assert.IsFalse(didRun);

            document.Body.AppendChild(script);

            Assert.IsTrue(didRun);
        }

        [Test]
        public async Task DynamicallyAddedScriptWithSourceShouldBeExecutedAfterAppending()
        {
            var didRun = false;
            var scripting = new CallbackScriptEngine(_ => didRun = true);
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>Some title</title><body>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));

            var script = document.CreateElement("script");
            script.SetAttribute("type", scripting.Type);
            script.SetAttribute("src", "foo.cs");

            Assert.IsFalse(didRun);

            document.Body.AppendChild(script);

            Assert.IsTrue(didRun);
        }

        [Test]
        public void DocumentOpenEncodingSwitchFailureShouldNotMutateDocument_Issue1276()
        {
            if (TestRuntime.UsePrefetchedTextSource)
            {
                Assert.Ignore("Prefetched text source is read only");
            }

            var scripting = new CallbackScriptEngine(_ => { });
            var config = Configuration.Default
                .WithCulture("en-US")
                .WithLocaleBasedEncoding()
                .WithScripts(scripting);

            var beforeLegacyByte = Encoding.ASCII.GetBytes("<!doctype html><html><head><title>caf");
            var afterLegacyByte = Encoding.ASCII.GetBytes("</title><script type='c-sharp'>x</script></head><body><p>tail</p></body></html>");
            var payload = new Byte[beforeLegacyByte.Length + 1 + afterLegacyByte.Length];
            Buffer.BlockCopy(beforeLegacyByte, 0, payload, 0, beforeLegacyByte.Length);
            payload[beforeLegacyByte.Length] = 0xe9;
            Buffer.BlockCopy(afterLegacyByte, 0, payload, beforeLegacyByte.Length + 1, afterLegacyByte.Length);

            var parent = BrowsingContext.New(config);
            var child = parent.CreateChild("issue-1276", Sandboxes.None);
            var document = child.OpenAsync(req => req.Content(Helper.StreamFromBytes(payload))).GetAwaiter().GetResult();
            var originalTitle = document.Title;
            var originalBody = document.Body?.TextContent;

            var source = document.GetType().GetProperty("Source", BindingFlags.Instance | BindingFlags.Public)?.GetValue(document);
            Assert.IsNotNull(source);

            var readOnlySourceField = source.GetType().GetField("_readOnlyTextSource", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(readOnlySourceField);
            var writable = readOnlySourceField.GetValue(source);
            Assert.IsNotNull(writable);

            var confidenceField = writable.GetType().GetField("_confidence", BindingFlags.Instance | BindingFlags.NonPublic);
            var encodingField = writable.GetType().GetField("_encoding", BindingFlags.Instance | BindingFlags.NonPublic);
            var decoderField = writable.GetType().GetField("_decoder", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(confidenceField);
            Assert.IsNotNull(encodingField);
            Assert.IsNotNull(decoderField);

            // Recreate the mismatch scenario from #1276 in a deterministic way.
            confidenceField.SetValue(writable, Enum.Parse(confidenceField.FieldType, "Tentative"));
            encodingField.SetValue(writable, TextEncoding.Resolve("windows-1252"));
            decoderField.SetValue(writable, TextEncoding.Resolve("windows-1252").GetDecoder());

            Assert.Throws<NotSupportedException>(() => document.Open());
            Assert.AreEqual(originalTitle, document.Title);
            Assert.AreEqual(originalBody, document.Body?.TextContent);
            Assert.AreEqual("café", document.Title);
            Assert.AreEqual("tail", document.Body?.TextContent);
        }

        [Test]
        public void CurrentScriptIsTheClassicScriptBeingRun()
        {
            var currentScript = default(IHtmlScriptElement);
            var scripting = new CallbackScriptEngine(options => currentScript = options.Document.CurrentScript);
            var config = Configuration.Default.WithScripts(scripting);
            var source = "<title>Some title</title><body><script type='c-sharp'>//...</script>";
            var document = source.ToHtmlDocument(config);
            var script = document.Scripts[0];

            Assert.AreSame(script, currentScript);
            Assert.IsNull(document.CurrentScript);
        }

        [Test]
        public async Task CurrentScriptIsTheExternalClassicScriptBeingRun()
        {
            var currentScript = default(IHtmlScriptElement);
            var scripting = new CallbackScriptEngine(options => currentScript = options.Document.CurrentScript);
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>Some title</title><body><script type='c-sharp' src='foo.cs'></script>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));
            var script = document.Scripts[0];

            Assert.AreSame(script, currentScript);
            Assert.IsNull(document.CurrentScript);
        }

        [Test]
        public async Task CurrentScriptIsTheDeferredScriptBeingRun()
        {
            var seen = new List<IHtmlScriptElement>();
            var scripting = new CallbackScriptEngine(options => seen.Add(options.Document.CurrentScript));
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>Some title</title><body><script type='c-sharp' defer src='first.cs'></script><script type='c-sharp' defer src='second.cs'></script>";
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));

            Assert.AreEqual(2, seen.Count);
            Assert.AreSame(document.Scripts[0], seen[0]);
            Assert.AreSame(document.Scripts[1], seen[1]);
            Assert.IsNull(document.CurrentScript);
        }

        [Test]
        public void CurrentScriptIsNullWhileRunningAModuleScript()
        {
            var didRun = false;
            var currentScript = default(IHtmlScriptElement);
            var scripting = new CallbackScriptEngine(options =>
            {
                didRun = true;
                currentScript = options.Document.CurrentScript;
            }, "module");
            var config = Configuration.Default.WithScripts(scripting);
            var source = "<title>Some title</title><body><script type='module'>//...</script>";
            var document = source.ToHtmlDocument(config);

            Assert.IsTrue(didRun);
            Assert.IsNull(currentScript);
            Assert.IsNull(document.CurrentScript);
        }

        [Test]
        public async Task ParsingSynchronouslyTracksExceptionEscapingHostEventListener_Issue1309()
        {
            var scripting = new CallbackScriptEngine(_ => { });
            var context = BrowsingContext.New(Configuration.Default.WithScripts(scripting));
            var tracked = TrackErrorAsync(context);
            var parser = context.GetService<IHtmlParser>();
            ThrowFromListener(parser, EventNames.BeforeScriptExecute, "from the host");

            var document = parser.ParseDocument("<body><script type='c-sharp'>//...</script>");
            var error = await AwaitTrackedErrorAsync(tracked);

            Assert.IsNotNull(document);
            Assert.IsInstanceOf<InvalidOperationException>(error);
            Assert.AreEqual("from the host", error.Message);
        }

        [Test]
        public async Task ParsingAsynchronouslyTracksExceptionEscapingHostEventListener_Issue1309()
        {
            var scripting = new CallbackScriptEngine(_ => { });
            var context = BrowsingContext.New(Configuration.Default.WithScripts(scripting));
            var tracked = TrackErrorAsync(context);
            var parser = context.GetService<IHtmlParser>();
            ThrowFromListener(parser, EventNames.BeforeScriptExecute, "from the host");

            var document = await parser.ParseDocumentAsync("<body><script type='c-sharp'>//...</script>");
            var error = await AwaitTrackedErrorAsync(tracked);

            Assert.IsNotNull(document);
            Assert.IsInstanceOf<InvalidOperationException>(error);
            Assert.AreEqual("from the host", error.Message);
        }

        [Test]
        public async Task ParsingSynchronouslyTracksExceptionEscapingDomContentLoadedListener_Issue1309()
        {
            var context = BrowsingContext.New(Configuration.Default);
            var tracked = TrackErrorAsync(context);
            var parser = context.GetService<IHtmlParser>();
            ThrowFromListener(parser, EventNames.DomContentLoaded, "from the host");

            var document = parser.ParseDocument("<body><p>text</p>");
            var error = await AwaitTrackedErrorAsync(tracked);

            Assert.IsNotNull(document);
            Assert.IsInstanceOf<InvalidOperationException>(error);
            Assert.AreEqual("from the host", error.Message);
        }

        [Test]
        public async Task ParsingAsynchronouslyTracksExceptionEscapingDomContentLoadedListener_Issue1309()
        {
            var context = BrowsingContext.New(Configuration.Default);
            var tracked = TrackErrorAsync(context);
            var parser = context.GetService<IHtmlParser>();
            ThrowFromListener(parser, EventNames.DomContentLoaded, "from the host");

            var document = await parser.ParseDocumentAsync("<body><p>text</p>");
            var error = await AwaitTrackedErrorAsync(tracked);

            Assert.IsNotNull(document);
            Assert.IsInstanceOf<InvalidOperationException>(error);
            Assert.AreEqual("from the host", error.Message);
        }

        [Test]
        public async Task FailingScriptingServiceIsTrackedInsteadOfFaultingTheParse_Issue1309()
        {
            var scripting = new FailingScriptEngine("from the scripting service");
            var context = BrowsingContext.New(Configuration.Default.WithScripts(scripting));
            var tracked = TrackErrorAsync(context);
            var parser = context.GetService<IHtmlParser>();

            var document = await parser.ParseDocumentAsync("<body><script type='c-sharp'>//...</script>");
            var error = await AwaitTrackedErrorAsync(tracked);

            Assert.IsNotNull(document);
            Assert.IsInstanceOf<InvalidOperationException>(error);
            Assert.AreEqual("from the scripting service", error.Message);
        }

        [Test]
        public async Task ReadyStateBecomesInteractiveBeforeDomContentLoaded_Issue1309()
        {
            var scripting = new CallbackScriptEngine(_ => { });
            var context = BrowsingContext.New(Configuration.Default.WithScripts(scripting));
            var parser = context.GetService<IHtmlParser>();
            var observed = new List<String>();

            parser.Parsing += (_, ev) =>
            {
                var parsed = ((HtmlParseEvent)ev).Document;
                parsed.ReadyStateChanged += (_, _) => observed.Add(parsed.ReadyState.ToString());
                parsed.AddEventListener(EventNames.DomContentLoaded, (_, _) => observed.Add("DOMContentLoaded"));
                parsed.AddEventListener(EventNames.Load, (_, _) => observed.Add("load"));
            };

            var document = await parser.ParseDocumentAsync("<body><script type='c-sharp'>//...</script><p>text</p>");

            Assert.AreEqual(DocumentReadyState.Complete, document.ReadyState);
            Assert.AreEqual("Interactive, DOMContentLoaded, Complete, load", String.Join(", ", observed));
        }

        /// <summary>
        /// Listens for the exceptions the browsing context tracks, i.e. what a host sees of a
        /// failure the parser handled instead of throwing.
        /// </summary>
        private static Task<Exception> TrackErrorAsync(IBrowsingContext context)
        {
            var tcs = new TaskCompletionSource<Exception>();
            context.AddEventListener(EventNames.Error, (_, ev) =>
                tcs.TrySetResult(((AngleSharp.Browser.Dom.Events.TrackEvent)ev).Error));
            return tcs.Task;
        }

        private static async Task<Exception> AwaitTrackedErrorAsync(Task<Exception> tracked)
        {
            var completed = await Task.WhenAny(tracked, Task.Delay(5000));
            Assert.AreSame(tracked, completed, "The browsing context did not track any error.");
            return await tracked;
        }

        private static void ThrowFromListener(IHtmlParser parser, String eventName, String message)
        {
            // Captured, as beforescriptexecute is fired at the script element without bubbling.
            parser.Parsing += (_, ev) => ((HtmlParseEvent)ev).Document.AddEventListener(
                eventName, (_, _) => throw new InvalidOperationException(message), capture: true);
        }
    }
}
