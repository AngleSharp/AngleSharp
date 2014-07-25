using AngleSharp;
using AngleSharp.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTests.Library
{
    [TestClass]
    public class BasicScriptingTests
    {
        Configuration configuration;
        TestScriptEngine scripting;

        [TestInitialize]
        public void CreateConfig()
        {
            configuration = new Configuration();
            scripting = new TestScriptEngine();
            configuration.IsScripting = true;
            configuration.Register(scripting);
        }

        [TestMethod]
        public void DocumentWriteDynamicallyWithCustomScriptEngine()
        {
            scripting.Callback = window =>
            {
                window.Document.Write("<b>Dynamically written</b>");
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

            public Action<IWindow> Callback
            {
                get;
                set;
            }

            public void Evaluate(String source, IWindow context)
            {
                if (Callback != null)
                    Callback(context);
            }

            public void Evaluate(Stream source, IWindow context)
            {
                if (Callback != null)
                    Callback(context);
            }
        }
    }
}
