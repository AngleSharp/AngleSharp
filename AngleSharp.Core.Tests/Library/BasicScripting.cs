namespace AngleSharp.Core.Tests.Library
{
    using System;
    using AngleSharp;
    using AngleSharp.Network;
    using AngleSharp.Scripting;
    using AngleSharp.Services;
    using NUnit.Framework;

    [TestFixture]
    public class BasicScriptingTests
    {
        Configuration configuration;
        TestScriptEngine scripting;

        [SetUp]
        public void CreateConfig()
        {
            scripting = new TestScriptEngine();
            configuration = Configuration.Default.With(new TestScriptService(scripting));
        }

        [TearDown]
        public void DestroyConfig()
        {
            configuration = null;
            scripting = null;
        }

        [Test]
        public void DocumentWriteDynamicallyWithCustomScriptEngine()
        {
            scripting.Callback = options =>
            {
                options.Document.Write("<b>Dynamically written</b>");
            };
            var source = "<title>Some title</title><body><script type='c-sharp'>//...</script>";
            var doc = source.ToHtmlDocument(configuration);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Body.TextContent);
            Assert.AreEqual("//...Dynamically written", doc.Body.TextContent);
            Assert.AreEqual(1, doc.QuerySelectorAll("b").Length);
            var bold = doc.QuerySelector("b");
            Assert.AreEqual("Dynamically written", bold.TextContent);
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
            public String Type
            {
                get { return "c-sharp"; }
            }

            public Action<ScriptOptions> Callback
            {
                get;
                set;
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
