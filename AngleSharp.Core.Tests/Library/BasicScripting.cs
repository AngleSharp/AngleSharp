namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp;
    using AngleSharp.Core.Tests.Mocks;
    using NUnit.Framework;
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
            var scripting = new CallbackScriptEngine(options => options.Document.Write("<b>Dynamically written</b>"));
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester(request => hasFoo = request.Address.Href == baseAddress + "/" + filename);
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
            var scripting = new CallbackScriptEngine(options => options.Document.Write(content[index++]));
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
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
            var scripting = new CallbackScriptEngine(options => options.Document.Write(content[index++]));
            var config = Configuration.Default.WithScripts(scripting).WithMockRequester();
            var source = "<title>Some title</title><body><script type='c-sharp' src='foo.cs'></script>";
            var doc = await BrowsingContext.New(config).OpenAsync(m => m.Content(source).Address("http://www.example.com"));
            Assert.AreEqual("This is dynamically written", doc.Body.TextContent);
            Assert.AreEqual(1, doc.QuerySelectorAll("b").Length);
            var bold = doc.QuerySelector("b");
            Assert.AreEqual("dynamically written", bold.TextContent);
            Assert.AreEqual(5, index);
        }


    }
}
