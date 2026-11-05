namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Browser;
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// document.open starts over, and what is written then has to end up in the tree.
    /// </summary>
    [TestFixture]
    public class DocumentOpenTests
    {
        private static Task<IDocument> OpenAsync(String html) => BrowsingContext.New().OpenAsync(m => m.Content(html));

        [Test]
        public async Task OpenWriteCloseReplacesTheDocumentOfATopLevelContext()
        {
            var document = await OpenAsync("<title>Old</title><p id=old>Old</p>");

            var result = document.Open();
            document.Write("<title>New</title><p id=new>New</p>");
            document.Close();

            Assert.AreSame(document, result);
            Assert.AreEqual("New", document.Title);
            Assert.IsNull(document.GetElementById("old"));
            Assert.AreEqual("New", document.GetElementById("new").TextContent);
            Assert.AreEqual(DocumentReadyState.Complete, document.ReadyState);
        }

        [Test]
        public async Task OpenWorksInAChildContextWithoutALoader()
        {
            var child = BrowsingContext.New().CreateChild(null, Sandboxes.None);
            var document = await child.OpenAsync(m => m.Content("<p>Old</p>"));

            document.Open();
            document.Write("<i>New</i>");
            document.Close();

            Assert.AreEqual("New", document.QuerySelector("i").TextContent);
            Assert.IsNull(document.QuerySelector("p"));
        }

        [Test]
        public async Task OpenWorksInASrcdocFrameOfAPage()
        {
            var parent = BrowsingContext.New();
            await parent.OpenAsync(m => m.Address("http://example.com/page").Content("<iframe></iframe>"));
            var child = parent.CreateChild(null, Sandboxes.None);
            var document = await child.OpenAsync(m => m.Address("about:srcdoc").Content("<p>Old</p>"));

            document.Open();
            document.Write("<i>New</i>");
            document.Close();

            Assert.IsNotNull(document.QuerySelector("i"));
        }

        [Test]
        public async Task OpenIsRefusedForAFrameOfAnotherOrigin()
        {
            var parent = BrowsingContext.New();
            await parent.OpenAsync(m => m.Address("http://example.com/page").Content("<iframe></iframe>"));
            var child = parent.CreateChild(null, Sandboxes.None);
            var document = await child.OpenAsync(m => m.Address("http://other.example/").Content("<p>Old</p>"));

            Assert.Throws<DomException>(() => document.Open());
            Assert.IsNotNull(document.QuerySelector("p"));
        }

        [Test]
        public async Task FrameWithoutASourceHasADocumentToWriteTo()
        {
            var page = await BrowsingContext.New().OpenAsync(m => m.Address("http://example.com/dir/page").Content("<iframe id=f></iframe>"));
            var frame = (IHtmlInlineFrameElement)page.GetElementById("f");

            var document = frame.ContentDocument;

            Assert.IsNotNull(document);
            Assert.AreSame(document, frame.ContentDocument);
            Assert.AreSame(document, frame.ContentWindow.Document);
            Assert.AreEqual("about:blank", document.Url);
            Assert.AreEqual(DocumentReadyState.Complete, document.ReadyState);
            Assert.IsNotNull(document.Body);
            Assert.IsNotNull(document.Head);

            document.Open();
            document.Write("<a id=x href=other>New</a>");
            document.Close();

            Assert.AreEqual("http://example.com/dir/other", ((IHtmlAnchorElement)document.GetElementById("x")).Href);
        }

        [TestCase("")]
        [TestCase("about:blank")]
        [TestCase("ABOUT:BLANK")]
        public async Task FrameWithAnEmptySourceHasADocument(String source)
        {
            var page = await OpenAsync($"<iframe id=f src='{source}'></iframe>");

            Assert.AreEqual("about:blank", ((IHtmlInlineFrameElement)page.GetElementById("f")).ContentDocument.Url);
        }

        [Test]
        public async Task FrameNotInADocumentHasNoDocument()
        {
            var page = await OpenAsync("");
            var frame = (IHtmlInlineFrameElement)page.CreateElement("iframe");

            Assert.IsNull(frame.ContentDocument);
            Assert.IsNull(frame.ContentWindow);

            page.Body.AppendChild(frame);
            Assert.IsNotNull(frame.ContentDocument);
        }

        [Test]
        public async Task SandboxOfAFrameAppliesToItsInitialDocument()
        {
            var page = await OpenAsync("<iframe id=f sandbox></iframe>");
            var frame = (IHtmlInlineFrameElement)page.GetElementById("f");

            Assert.AreNotEqual(Sandboxes.None, frame.ContentDocument.Context.Security);
        }

        [Test]
        public async Task FrameWithASourceDoesNotGetAnInitialDocument()
        {
            var page = await OpenAsync("<iframe id=f src='http://example.com/'></iframe>");

            Assert.IsNull(((IHtmlInlineFrameElement)page.GetElementById("f")).ContentDocument);
        }

        [Test]
        public async Task SourceReplacesTheInitialDocument()
        {
            var page = await OpenAsync("<iframe id=f></iframe>");
            var frame = (IHtmlInlineFrameElement)page.GetElementById("f");
            var initial = frame.ContentDocument;

            frame.ContentHtml = "<b>srcdoc</b>";
            await Task.WhenAll(page.GetDownloads().Select(m => m.Task));

            for (var i = 0; i < 200 && ReferenceEquals(frame.ContentDocument, initial); i++)
            {
                await Task.Delay(10);
            }

            Assert.IsNotNull(frame.ContentDocument.QuerySelector("b"));
        }

        [Test]
        public async Task WritesMayEndAnywhereInTheMarkup()
        {
            var document = await OpenAsync("");

            document.Open();
            document.Write("<di");
            document.Write("v id=a>x");
            document.WriteLine("y</div>");
            document.Close();

            Assert.AreEqual("xy", document.GetElementById("a").TextContent);
        }

        [Test]
        public async Task OpenWithoutWritingLeavesAnEmptyDocument()
        {
            var document = await OpenAsync("<p>Old</p>");

            document.Open();

            Assert.AreEqual(0, document.ChildNodes.Length);
            document.Close();
            Assert.AreEqual(DocumentReadyState.Complete, document.ReadyState);
        }

        [Test]
        public async Task ANewLoadEventFiresAfterClosing()
        {
            var document = await OpenAsync("");
            var fired = 0;

            document.Open();
            document.Loaded += (_, _) => fired++;
            document.Write("<p></p>");
            document.Close();

            Assert.AreEqual(1, fired);
        }

        [Test]
        public async Task OpeningAgainAfterClosingStartsOver()
        {
            var document = await OpenAsync("");

            document.Open();
            document.Write("<a></a>");
            document.Close();
            document.Open();
            document.Write("<b></b>");
            document.Close();

            Assert.IsNull(document.QuerySelector("a"));
            Assert.IsNotNull(document.QuerySelector("b"));
        }

        [Test]
        public async Task WriteAfterLoadingStartsOverAndWaitsForTheEndOfTheTask()
        {
            var document = await OpenAsync("<p>Old</p>");

            document.Write("<i>New</i>");

            Assert.AreEqual(DocumentReadyState.Loading, document.ReadyState);
            Assert.IsNull(document.QuerySelector("p"));

            // there's no event loop here to end the task, which AngleSharp.Js has
            document.Close();
            Assert.AreEqual("New", document.QuerySelector("i").TextContent);
        }

        [Test]
        public async Task ScriptsInWhatIsWrittenRunAndMayWriteThemselves()
        {
            var calls = 0;
            IDocument document = null;
            var scripting = new CallbackScriptEngine(options =>
            {
                if (++calls == 2)
                {
                    options.Document.Write("<u>nested</u>");
                }
            });
            var config = Configuration.Default.WithScripts(scripting);
            document = await BrowsingContext.New(config).OpenAsync(m => m.Content("<script type='c-sharp'>//</script>"));

            document.Open();
            document.Write("<script type='c-sharp'>//</script><b></b>");
            document.Close();

            Assert.AreEqual(2, calls);
            Assert.IsNotNull(document.QuerySelector("u"));
            Assert.IsNotNull(document.QuerySelector("b"));
        }

        [Test]
        public async Task OpenDuringTheParseOfTheDocumentDoesNothing()
        {
            var scripting = new CallbackScriptEngine(options =>
            {
                Assert.AreSame(options.Document, options.Document.Open());
                options.Document.Write("<u>written</u>");
            });
            var config = Configuration.Default.WithScripts(scripting);

            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content("<p>Before</p><script type='c-sharp'>//</script><p>After</p>"));

            Assert.AreEqual(2, document.QuerySelectorAll("p").Length);
            Assert.IsNotNull(document.QuerySelector("u"));
        }

        [Test]
        public async Task ADocumentOfNoContextCanBeWrittenTo()
        {
            var document = await OpenAsync("<p>Page</p>");
            var other = document.Implementation.CreateHtmlDocument("x");

            other.Open();
            other.Write("<i>Other</i>");
            other.Close();

            Assert.IsNotNull(other.QuerySelector("i"));
            Assert.IsNotNull(document.QuerySelector("p"));
        }

        [Test]
        public void ADocumentParsedWithoutAContextCanBeWrittenTo()
        {
            var document = new HtmlParser().ParseDocument("<p>Old</p>");

            document.Open();
            document.Write("<i>New</i>");
            document.Close();

            Assert.IsNotNull(document.QuerySelector("i"));
            Assert.IsNull(document.QuerySelector("p"));
        }

        [Test]
        public async Task TheParseOfWhatWasWrittenCountsAsOneChangeOfTheVersion()
        {
            var document = (AngleSharp.Dom.Document)await OpenAsync("<p></p>");
            var before = document.MutationVersion;

            document.Open();
            Assert.AreEqual(before, document.MutationVersion);
            document.Write("<a></a><b></b><i></i><u></u>");
            ((IDocument)document).Close();

            Assert.AreEqual(before + 1, document.MutationVersion);
        }
    }
}
