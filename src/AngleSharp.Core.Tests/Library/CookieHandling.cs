namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
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

        [Test]
        public async Task SettingNewCookieInSubsequentRequestDoesNotExpirePreviousCookies()
        {
            var config = Configuration.Default.
                WithCookies().
                WithVirtualRequester(req => VirtualResponse.Create(
                    res => res.Address("http://localhost/mockpage.html").
                               Content("<div></div>").
                               Header(HeaderNames.SetCookie, "Auth=Bar")));
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res =>
                res.Content("<a href=mockpage.html></a>").
                    Address("http://localhost/").
                    Header(HeaderNames.SetCookie, "UserID=Foo"));

            document = await document.QuerySelector<IHtmlAnchorElement>("a").NavigateAsync();

            Assert.AreEqual("UserID=Foo; Auth=Bar", document.Cookie);
        }

        [Test]
        public async Task SettingNoCookieInSubsequentRequestLeavesCookieSituationUnchanged()
        {
            var config = Configuration.Default.
                WithCookies().
                WithVirtualRequester(req => VirtualResponse.Create(
                    res => res.Address("http://localhost/mockpage.html").
                               Content("<div></div>")));
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res =>
                res.Content("<a href=mockpage.html></a>").
                    Address("http://localhost/").
                    Header(HeaderNames.SetCookie, "UserID=Foo"));

            document = await document.QuerySelector<IHtmlAnchorElement>("a").NavigateAsync();

            Assert.AreEqual("UserID=Foo", document.Cookie);
        }
    }
}
