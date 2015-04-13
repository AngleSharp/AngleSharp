namespace AngleSharp.Core.Tests.Library
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Dom.Html;
    using NUnit.Framework;

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
                var url = "http://anglesharp.azurewebsites.net/PostUrlEncodeNormal";
                var config = new Configuration().WithDefaultLoader();
                var document = await BrowsingContext.New(config).OpenAsync(Url.Create(url), CancellationToken.None);
                var h1 = document.QuerySelector("h1");

                Assert.IsNotNull(document);
                Assert.IsNotNull(document.DocumentElement);
                Assert.IsNotNull(document.Body);
                Assert.IsNotNull(document.Head);
                Assert.AreEqual(url, document.DocumentUri);
                Assert.AreEqual(title, document.Title);
                Assert.AreEqual(title, h1.TextContent);
            }
        }

        [Test]
        public async Task ContextFormSubmission()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = "http://anglesharp.azurewebsites.net/PostUrlEncodeNormal";
                var config = new Configuration().WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(Url.Create(url), CancellationToken.None);

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
                var url = "http://anglesharp.azurewebsites.net/";
                var config = new Configuration().WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(Url.Create(url), CancellationToken.None);
                var anchors = document.QuerySelectorAll<IHtmlAnchorElement>("ul a");
                var anchor = anchors.Where(m => m.TextContent == "Header").FirstOrDefault();
                var result = await context.OpenAsync(Url.Create(anchor.Href), CancellationToken.None);

                Assert.IsNotNull(result);
                Assert.AreEqual(result, context.Active);
                Assert.AreEqual(url, context.Active.Body.TextContent);
            }
        }
    }
}
