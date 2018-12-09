namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp;
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class BasicScriptingTests
    {
        [Test]
        public void DocumentWriteDynamicallyWithCustomScriptEngineAndText()
        {
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
            var scripting = new CallbackScriptEngine(options => didRun = true);
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
            var scripting = new CallbackScriptEngine(options => didRun = true);
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
    }
}
