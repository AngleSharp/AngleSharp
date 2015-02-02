namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class BasicScriptingTests
    {
        Configuration configuration;
        TestScriptEngine scripting;

        [SetUp]
        public void CreateConfig()
        {
            configuration = new Configuration();
            scripting = new TestScriptEngine();
            configuration.IsScripting = true;
            configuration.Register(scripting);
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
            var doc = DocumentBuilder.Html("<title>Some title</title><body><script type='c-sharp'>//...</script>", configuration);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Body.TextContent);
            Assert.AreEqual("//...Dynamically written", doc.Body.TextContent);
            Assert.AreEqual(1, doc.QuerySelectorAll("b").Length);
            var bold = doc.QuerySelector("b");
            Assert.AreEqual("Dynamically written", bold.TextContent);
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
