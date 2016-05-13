namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    [TestFixture]
    public class CookieHandlingTests
    {
        [Test]
        public async Task SettingSimpleSingleCookieInRequestAppearsInDocument()
        {
            var cookie = await LoadDocumentWithCookie("UserID=Foo");
            Assert.AreEqual("UserID=Foo", cookie);
        }

        [Test]
        public async Task SettingTwoSimpleCookiesInRequestAppearsInDocument()
        {
            var cookie = await LoadDocumentWithCookie("UserID=Foo, Auth=bar");
            Assert.AreEqual("UserID=Foo; Auth=bar", cookie);
        }

        [Test]
        public async Task SettingSingleCookieWithMaxAgeInRequestAppearsInDocument()
        {
            var cookie = await LoadDocumentWithCookie("cookie=two; Max-Age=36001");
            Assert.AreEqual("cookie=two", cookie);
        }

        [Test]
        public async Task SettingSingleExpiredCookieInRequestDoesNotAppearInDocument()
        {
            var cookie = await LoadDocumentWithCookie("cookie=expiring; Expires=Tue, 10 Nov 2009 23:00:00 GMT");
            Assert.AreEqual("", cookie);
        }

        [Test]
        public async Task SettingMultipleExpiredCookieInRequestDoNotAppearInDocument()
        {
            var cookie = await LoadDocumentWithCookie("cookie=expiring; Expires=Tue, 10 Nov 2009 23:00:00 GMT, foo=bar; Expires=Tue, 10 Nov 2009 23:00:00 GMT");
            Assert.AreEqual("", cookie);
        }

        [Test]
        public async Task SettingOneExpiredCookieAndAFutureCookieInRequestDoAppearInDocument()
        {
            var cookie = await LoadDocumentWithCookie("cookie=expiring; Expires=Tue, 10 Nov 2009 23:00:00 GMT, foo=bar; Expires=Tue, 28 Jan 2025 13:37:00 GMT");
            Assert.AreEqual("foo=bar", cookie);
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

        [Test]
        public async Task SettingOneCookiesInOneRequestAppearsInDocument()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = "https://httpbin.org/cookies/set?k1=v1";
                var config = Configuration.Default.WithCookies().WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(url);

                Assert.AreEqual("k1=v1", document.Cookie);
            }
        }

        [Test]
        public async Task SettingTwoCookiesInOneRequestAppearsInDocument()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = "https://httpbin.org/cookies/set?k2=v2&k1=v1";
                var config = Configuration.Default.WithCookies().WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(url);

                Assert.AreEqual("k2=v2; k1=v1", document.Cookie);
            }
        }

        [Test]
        public async Task SettingThreeCookiesInOneRequestAppearsInDocument()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = "https://httpbin.org/cookies/set?test=baz&k2=v2&k1=v1&foo=bar";
                var config = Configuration.Default.WithCookies().WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(url);

                Assert.AreEqual("test=baz; k2=v2; k1=v1; foo=bar", document.Cookie);
            }
        }

        static async Task<String> LoadDocumentWithCookie(String cookieValue)
        {
            var config = Configuration.Default.WithCookies();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res =>
                res.Content("<div></div>").
                    Address("http://localhost/").
                    Header(HeaderNames.SetCookie, cookieValue));

            return document.Cookie;
        }
    }
}
