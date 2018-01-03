namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using Dom;
    using Mocks;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
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
        public async Task SettingSingleCookieChangesValue()
        {
            var document = await LoadDocumentAloneWithCookie("cookie=two; Max-Age=36001");
            Assert.AreEqual("cookie=two", document.Cookie);
            document.Cookie = "cookie=one";
            Assert.AreEqual("cookie=one", document.Cookie);
        }

        [Test]
        public async Task SettingOtherCookieAddsCookie()
        {
            var document = await LoadDocumentAloneWithCookie("cookie=two; Max-Age=36001");
            Assert.AreEqual("cookie=two", document.Cookie);
            document.Cookie = "foo=bar";
            Assert.AreEqual("cookie=two; foo=bar", document.Cookie);
        }

        [Test]
        public async Task InvalidatingCookieRemovesTheCookie()
        {
            var document = await LoadDocumentAloneWithCookie("cookie=two; Max-Age=36001, foo=bar");
            Assert.AreEqual("cookie=two; foo=bar", document.Cookie);
            document.Cookie = "cookie=expiring; Expires=Tue, 10 Nov 2009 23:00:00 GMT";
            Assert.AreEqual("foo=bar", document.Cookie);
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

                var cookies = document.Cookie.Split(new[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
                CollectionAssert.AreEquivalent(new[] { "k2=v2", "k1=v1" }, cookies);
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

                var cookies = document.Cookie.Split(new[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
                CollectionAssert.AreEquivalent(new[] { "test=baz", "k2=v2", "k1=v1", "foo=bar" }, cookies);
            }
        }

        [Test]
        public async Task SettingThreeCookiesInOneRequestAreTransportedToNextRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var baseUrl = "https://httpbin.org/cookies";
                var url = baseUrl + "/set?test=baz&k2=v2&k1=v1&foo=bar";
                var config = Configuration.Default.WithCookies().WithDefaultLoader();
                var context = BrowsingContext.New(config);
                await context.OpenAsync(url);
                var document = await context.OpenAsync(baseUrl);

                Assert.AreEqual(@"{
  ""cookies"": {
    ""foo"": ""bar"", 
    ""k1"": ""v1"", 
    ""k2"": ""v2"", 
    ""test"": ""baz""
  }
}
".Replace(Environment.NewLine, "\n"), document.Body.TextContent);
            }
        }

        [Test]
        public async Task SettingCookieIsPreservedViaRedirect()
        {
            if (Helper.IsNetworkAvailable())
            {
                var cookieUrl = "https://httpbin.org/cookies/set?test=baz";
                var redirectUrl = "http://httpbin.org/redirect-to?url=http%3A%2F%2Fhttpbin.org%2Fcookies";
                var config = Configuration.Default.WithCookies().WithDefaultLoader();
                var context = BrowsingContext.New(config);
                await context.OpenAsync(cookieUrl);
                var document = await context.OpenAsync(redirectUrl);

                Assert.AreEqual(@"{
  ""cookies"": {
    ""test"": ""baz""
  }
}
".Replace(Environment.NewLine, "\n"), document.Body.TextContent);
            }
        }

        [Test]
        public async Task SendingRequestToLocalResourceContainsLocalCookie()
        {
            var content = "<!doctype html><img src=foo.png />";
            var cookieValue = "test=true";
            var requestCount = 0;
            var imgCookie = String.Empty;
            var initial = VirtualResponse.Create(m => m.Content(content).Address("http://www.local.com").Header(HeaderNames.SetCookie, cookieValue));
            var document = await LoadDocumentWithFakeRequesterAndCookie(initial, req =>
            {
                var res = VirtualResponse.Create(m => m.Content(String.Empty).Address(req.Address));
                imgCookie = req.Headers.GetOrDefault(HeaderNames.Cookie, String.Empty);
                requestCount++;
                return res;
            });

            Assert.AreEqual(1, requestCount);
            Assert.AreEqual(cookieValue, imgCookie);
        }

        [Test]
        public async Task SendingRequestToOtherResourceOmitsLocalCookie()
        {
            var content = "<!doctype html><img src=http://www.other.com/foo.png />";
            var cookieValue = "test=true";
            var requestCount = 0;
            var imgCookie = String.Empty;
            var initial = VirtualResponse.Create(m => m.Content(content).Address("http://www.local.com").Header(HeaderNames.SetCookie, cookieValue));
            var document = await LoadDocumentWithFakeRequesterAndCookie(initial, req =>
            {
                var res = VirtualResponse.Create(m => m.Content(String.Empty).Address(req.Address));
                imgCookie = req.Headers.GetOrDefault(HeaderNames.Cookie, String.Empty);
                requestCount++;
                return res;
            });

            Assert.AreEqual(1, requestCount);
            Assert.AreEqual(String.Empty, imgCookie);
        }

        [Test]
        public async Task CookieWithUTCTimeStampVariant1()
        {
            var content = "<!doctype html>";
            var cookieValue = "fm=0; Expires=Wed, 03 Jan 2018 10:54:24 UTC; Path=/; Domain=.twitter.com; Secure; HTTPOnly";
            var requestCount = 0;
            var initial = VirtualResponse.Create(m => m.Content(content).Address("http://www.twitter.com").Header(HeaderNames.SetCookie, cookieValue));
            var document = await LoadDocumentWithFakeRequesterAndCookie(initial, req =>
            {
                var res = VirtualResponse.Create(m => m.Content(String.Empty).Address(req.Address));
                requestCount++;
                return res;
            });

            Assert.AreEqual(0, requestCount);
        }

        [Test]
        public async Task CookieWithUTCTimeStampVariant2()
        {
            var content = "<!doctype html>";
            var cookieValue = "ct0=cf2c3d61837dc0513fe9dfa8019a3af8; Expires=Wed, 03 Jan 2018 16:54:34 UTC; Path=/; Domain=.twitter.com; Secure";
            var requestCount = 0;
            var initial = VirtualResponse.Create(m => m.Content(content).Address("http://www.twitter.com").Header(HeaderNames.SetCookie, cookieValue));
            var document = await LoadDocumentWithFakeRequesterAndCookie(initial, req =>
            {
                var res = VirtualResponse.Create(m => m.Content(String.Empty).Address(req.Address));
                requestCount++;
                return res;
            });

            Assert.AreEqual(0, requestCount);
        }

        [Test]
        public async Task SendingRequestToLocalResourceSendsLocalCookie()
        {
            var content = "<!doctype html><img src=http://www.local.com/foo.png />";
            var cookieValue = "test=true";
            var requestCount = 0;
            var imgCookie = String.Empty;
            var initial = VirtualResponse.Create(m => m.Content(content).Address("http://www.local.com").Header(HeaderNames.SetCookie, cookieValue));
            var document = await LoadDocumentWithFakeRequesterAndCookie(initial, req =>
            {
                var res = VirtualResponse.Create(m => m.Content(String.Empty).Address(req.Address));
                imgCookie = req.Headers.GetOrDefault(HeaderNames.Cookie, String.Empty);
                requestCount++;
                return res;
            });

            Assert.AreEqual(1, requestCount);
            Assert.AreEqual(cookieValue, imgCookie);
        }

        private static Task<IDocument> LoadDocumentWithFakeRequesterAndCookie(IResponse initialResponse, Func<IRequest, IResponse> onRequest)
        {
            var requester = new MockRequester();
            requester.BuildResponse(onRequest);
            var config = Configuration.Default.WithDefaultLoader(setup => setup.IsResourceLoadingEnabled = true, new[] { requester }).WithCookies();
            return BrowsingContext.New(config).OpenAsync(initialResponse, System.Threading.CancellationToken.None);
        }

        private static async Task<IDocument> LoadDocumentAloneWithCookie(String cookieValue)
        {
            var config = Configuration.Default.WithCookies();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res =>
                res.Content("<div></div>").
                    Address("http://localhost/").
                    Header(HeaderNames.SetCookie, cookieValue));

            return document;
        }

        private static async Task<String> LoadDocumentWithCookie(String cookieValue)
        {
            var document = await LoadDocumentAloneWithCookie(cookieValue);
            return document.Cookie;
        }
    }
}
