namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Dom.Html;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class CookieHandlingTests
    {
        [Test]
        public async Task SettingSingleCookieInRequestAppearsInDocument()
        {
            var config = Configuration.Default.WithCookies();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res => 
                res.Content("<div></div>").
                    Address("http://localhost/").
                    Header(HeaderNames.SetCookie, "UserID=Foo"));

            Assert.IsNotNull(document.QuerySelector("div"));
            Assert.AreEqual("UserID=Foo", document.Cookie);
        }

        [Test]
        public async Task SettingSingleCookieInDocumentAppearsInRequest()
        {
            var request = default(IRequest);
            var config = Configuration.Default.WithCookies().WithMockRequester(req => request = req);
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res =>
                res.Content("<a href=mockpage.html></a>").
                    Address("http://localhost/"));

            document.Cookie = "UserID=Foo";
            await document.QuerySelector<IHtmlAnchorElement>("a").NavigateAsync();

            Assert.IsNotNull(request);
            Assert.IsTrue(request.Headers.ContainsKey(HeaderNames.Cookie));
            Assert.AreEqual("UserID=Foo", request.Headers[HeaderNames.Cookie]);
        }
    }
}
