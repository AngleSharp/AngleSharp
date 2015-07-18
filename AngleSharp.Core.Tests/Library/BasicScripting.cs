namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using AngleSharp.Services.Scripting;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    [TestFixture]
    public class BasicScriptingTests
    {
        [Test]
        public void DocumentWriteDynamicallyWithCustomScriptEngineAndText()
        {
            var scripting = new TestScriptEngine(options => options.Document.Write("<b>Dynamically written</b>"));
            var config = Configuration.Default.With(new TestScriptService(scripting));
            var source = "<title>Some title</title><body><script type='c-sharp'>//...</script>";
            var doc = source.ToHtmlDocument(config);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Body.TextContent);
            Assert.AreEqual("//...Dynamically written", doc.Body.TextContent);
            Assert.AreEqual(1, doc.QuerySelectorAll("b").Length);
            var bold = doc.QuerySelector("b");
            Assert.AreEqual("Dynamically written", bold.TextContent);
        }

        [Test]
        public async Task DocumentWriteDynamicallyWithCustomScriptEngineAndSource()
        {
            var baseAddress = "http://www.example.com";
            var filename = "foo.cs";
            var hasFoo = false;
            var scripting = new TestScriptEngine(options => options.Document.Write("<b>Dynamically written</b>"));
            var config = Configuration.Default.With(new TestScriptService(scripting))
                                      .WithMockRequester(request => hasFoo = request.Address.Href == baseAddress + "/" + filename);
            var source = "<title>Some title</title><body><script type='c-sharp' src='" + filename + "'></script>";
            var doc = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address(baseAddress));
            Assert.IsTrue(hasFoo);
            Assert.AreEqual("Dynamically written", doc.Body.TextContent);
            Assert.AreEqual(1, doc.QuerySelectorAll("b").Length);
            var bold = doc.QuerySelector("b");
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
            var scripting = new TestScriptEngine(options => options.Document.Write(content[index++]));
            var config = Configuration.Default.With(new TestScriptService(scripting)).WithMockRequester();
            var source = "<title>Some title</title><body><script type='c-sharp' src='foo.cs'></script>";
            var doc = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));
            Assert.AreEqual("Dynamically written", doc.Body.TextContent);
            Assert.AreEqual(1, doc.QuerySelectorAll("b").Length);
            var bold = doc.QuerySelector("b");
            Assert.AreEqual("Dynamically written", bold.TextContent);
            Assert.AreEqual(2, index);
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
            var scripting = new TestScriptEngine(options => options.Document.Write(content[index++]));
            var config = Configuration.Default.With(new TestScriptService(scripting)).WithMockRequester();
            var source = "<title>Some title</title><body><script type='c-sharp' src='foo.cs'></script>";
            var doc = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));
            Assert.AreEqual("This is dynamically written", doc.Body.TextContent);
            Assert.AreEqual(1, doc.QuerySelectorAll("b").Length);
            var bold = doc.QuerySelector("b");
            Assert.AreEqual("dynamically written", bold.TextContent);
            Assert.AreEqual(5, index);
        }

        class TestScriptService : IScriptingService
        {
            readonly TestScriptEngine _engine;

            public TestScriptService(TestScriptEngine engine)
            {
                _engine = engine;
            }

            public IScriptEngine GetEngine(String mimeType)
            {
                return _engine;
            }
        }

        class TestScriptEngine : IScriptEngine
        {
            public TestScriptEngine(Action<ScriptOptions> callback)
            {
                Callback = callback;
            }

            public String Type
            {
                get { return "c-sharp"; }
            }

            public Action<ScriptOptions> Callback
            {
                get;
                private set;
            }

            public void Evaluate(String source, ScriptOptions options)
            {
                if (Callback != null)
                    Callback(options);
            }

            public void Evaluate(IResponse response, ScriptOptions options)
            {
                if (Callback != null)
                    Callback(options);
            }
        }
    }
}
