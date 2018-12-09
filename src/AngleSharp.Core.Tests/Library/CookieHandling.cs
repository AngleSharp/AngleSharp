using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Common;
using AngleSharp.Core.Tests.Mocks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Library
{
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
        public async Task AccessLloydsBankWithMixedPathAndDomainCookiesShouldWork()
        {
            if (Helper.IsNetworkAvailable())
            {
                var configuration = Configuration.Default.WithDefaultLoader().WithCookies();
                var context = BrowsingContext.New(configuration);
                await context.OpenAsync("https://online.lloydsbank.co.uk/personal/logon/login.jsp");
            }
        }

        [Test]
        public async Task PlainVersion1CookieIsCorrectlyTransformed()
        {
            var cookie = await LoadDocumentWithCookie(
                "FGTServer=04E2E1A642B2BB49C6FE0115DE3976CB377263F3278BD6C8E2F8A24EE4DF7562F089BFAC5C0102; Version=1");
            Assert.AreEqual(
                "$Version=1; FGTServer=04E2E1A642B2BB49C6FE0115DE3976CB377263F3278BD6C8E2F8A24EE4DF7562F089BFAC5C0102",
                cookie);
        }

        [Test]
        public async Task Version1CookieIsAlreadyTransformed()
        {
            var cookie = await LoadDocumentWithCookie("Customer=\"WILE_E_COYOTE\"; Version=\"1\"");
            Assert.AreEqual("$Version=\"1\"; Customer=\"WILE_E_COYOTE\"", cookie);
        }

        [Test]
        public async Task Version1CookieWithSingleEntryAlreadyTransformedCorrectly()
        {
            var cookie = await LoadDocumentWithCookie("Shipping=FedEx; Version=\"1\"");
            Assert.AreEqual("$Version=\"1\"; Shipping=FedEx", cookie);
        }

        [Test]
        public async Task CookieExpiresInFuture()
        {
            var year = DateTime.Today.Year + 1;
            var dayOfWeek = new DateTime(year, 8, 3).ToString("ddd", CultureInfo.InvariantCulture);
            var cookie = $"ppkcookie2=another test; expires={dayOfWeek}, 3 Aug {year} 20:47:11 GMT; path=/";
            var document = await LoadDocumentAloneWithCookie("");
            document.Cookie = cookie;
            Assert.AreEqual("ppkcookie2=another test", document.Cookie);
        }

        [Test]
        public async Task CookieExpiredAlready()
        {
            var year = DateTime.Today.Year - 1;
            var dayOfWeek = new DateTime(year, 8, 3).ToString("ddd", CultureInfo.InvariantCulture);
            var document = await LoadDocumentAloneWithCookie("");
            document.Cookie = $"ppkcookie2=yet another test; expires={dayOfWeek}, 3 Aug {year} 20:47:11 GMT; path=/";
            Assert.AreEqual("", document.Cookie);
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
            var cookie = await LoadDocumentWithCookie(
                "cookie=expiring; Expires=Tue, 10 Nov 2009 23:00:00 GMT, foo=bar; Expires=Tue, 10 Nov 2009 23:00:00 GMT");
            Assert.AreEqual("", cookie);
        }

        [Test]
        public async Task SettingOneExpiredCookieAndAFutureCookieInRequestDoAppearInDocument()
        {
            var cookie = await LoadDocumentWithCookie(
                "cookie=expiring; Expires=Tue, 10 Nov 2009 23:00:00 GMT, foo=bar; Expires=Tue, 28 Jan 2025 13:37:00 GMT");
            Assert.AreEqual("foo=bar", cookie);
        }

        [Test]
        public async Task SettingSingleCookieInDocumentAppearsInRequest()
        {
            var request = default(Request);
            var config = Configuration.Default.WithCookies().WithMockRequester(req => request = req);
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res =>
                res.Content("<a href=mockpage.html></a>").Address("http://localhost/"));

            document.Cookie = "UserID=Foo";
            await document.QuerySelector<IHtmlAnchorElement>("a").NavigateAsync();

            Assert.IsNotNull(request);
            Assert.IsTrue(request.Headers.ContainsKey(HeaderNames.Cookie));
            Assert.AreEqual("UserID=Foo", request.Headers[HeaderNames.Cookie]);
        }

        [Test]
        public async Task SettingNewCookieInSubsequentRequestDoesNotExpirePreviousCookies()
        {
            var config = Configuration.Default.WithCookies().WithVirtualRequester(req => VirtualResponse.Create(
                res => res.Address("http://localhost/mockpage.html").Content("<div></div>")
                    .Cookie("Auth=Bar; Path=/")));
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res =>
                res.Content("<a href=mockpage.html></a>").Address("http://localhost/").Cookie("UserID=Foo; Path=/"));

            document = await document.QuerySelector<IHtmlAnchorElement>("a").NavigateAsync();

            Assert.AreEqual("UserID=Foo; Auth=Bar", document.Cookie);
        }

        [Test]
        public async Task SettingNoCookieInSubsequentRequestLeavesCookieSituationUnchanged()
        {
            var config = Configuration.Default.WithCookies().WithVirtualRequester(req => VirtualResponse.Create(
                res => res.Address("http://localhost/mockpage.html").Content("<div></div>")));
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res =>
                res.Content("<a href=mockpage.html></a>").Address("http://localhost/").Cookie("UserID=Foo"));

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
                var cookies = document.Cookie.Split(';').Select(m => m.Trim());

                CollectionAssert.AreEquivalent(new[] {"k1=v1", "k2=v2"}, cookies);
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
                var cookies = document.Cookie.Split(';').Select(m => m.Trim());

                CollectionAssert.AreEquivalent(new[] {"k1=v1", "k2=v2", "foo=bar", "test=baz"}, cookies);
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

                var expected = JObject.Parse(@"{
  ""cookies"": {
    ""foo"": ""bar"",
    ""k1"": ""v1"",
    ""k2"": ""v2"",
    ""test"": ""baz""
  }
}
");

                Assert.AreEqual(expected.ToString(), JObject.Parse(document.Body.TextContent).ToString());
            }
        }

        [Test]
        public async Task SettingCookieIsPreservedViaRedirect()
        {
            if (Helper.IsNetworkAvailable())
            {
                var cookieUrl = "https://httpbin.org/cookies/set?test=baz";
                var redirectUrl = "https://httpbin.org/redirect-to?url=https%3A%2F%2Fhttpbin.org%2Fcookies";
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
            var initial =
                VirtualResponse.Create(m => m.Content(content).Address("http://www.local.com").Cookie(cookieValue));
            await LoadDocumentWithFakeRequesterAndCookie(initial, req =>
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
            var initial =
                VirtualResponse.Create(m => m.Content(content).Address("http://www.local.com").Cookie(cookieValue));
            await LoadDocumentWithFakeRequesterAndCookie(initial, req =>
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
            var cookieValue =
                "fm=0; Expires=Wed, 03 Jan 2018 10:54:24 UTC; Path=/; Domain=.twitter.com; Secure; HTTPOnly";
            var requestCount = 0;
            var initial = VirtualResponse.Create(m =>
                m.Content(content).Address("http://www.twitter.com").Header(HeaderNames.SetCookie, cookieValue));
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
            var cookieValue =
                "ct0=cf2c3d61837dc0513fe9dfa8019a3af8; Expires=Wed, 03 Jan 2018 16:54:34 UTC; Path=/; Domain=.twitter.com; Secure";
            var requestCount = 0;
            var initial = VirtualResponse.Create(m =>
                m.Content(content).Address("http://www.twitter.com").Header(HeaderNames.SetCookie, cookieValue));
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
            var initial =
                VirtualResponse.Create(m => m.Content(content).Address("http://www.local.com").Cookie(cookieValue));
            await LoadDocumentWithFakeRequesterAndCookie(initial, req =>
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
        public async Task DefaultHttpRequesterWorksWithVersion1Cookies()
        {
            var config = Configuration.Default.WithDefaultLoader().WithCookies();
            var context = BrowsingContext.New(config);
            var cookieValue =
                "FGTServer=04E2E1A642B2BB49C6FE0115DE3976CB377263F3278BD6C8E2F8A24EE4DF7562F089BFAC5C0102; Version=1";

            var document = await context.OpenAsync(res => res.Content("<div></div>")
                .Address("http://localhost/")
                .Header(HeaderNames.SetCookie, cookieValue));

            await context.OpenAsync("http://localhost/");
        }

        [Test]
        public void DateTimeShouldBeAccepted_Issue663()
        {
            var mcp = new MemoryCookieProvider();
            var url = Url.Create("http://www.example.com");
            mcp.SetCookie(url,
                "c-s=expires=1531601411~access=/clientimg/richmond/*!/content/richmond/*~md5=c56447496f01a9cd01bbec1b3a293357; path=/; secure");
        }

        [Test]
        public void DataUrlShouldNotDeliverAnyCookie_Issue702()
        {
            var mcp = new MemoryCookieProvider();
            var url = Url.Create("data:image/gif;base64,R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs=");
            var cookie = mcp.GetCookie(url);
            Assert.IsEmpty(cookie);
        }

        [Test]
        public void NoOriginShouldNotDeliverAnyCookie_Issue702()
        {
            var mcp = new MemoryCookieProvider();
            var url = new Url("");
            var cookie = mcp.GetCookie(url);
            Assert.IsEmpty(cookie);
        }

        [Test]
        public void ShouldChangeSelectedCookiesOnRedirect_Issue548()
        {
            var requester = new MockRequester();
            var mcp = new MemoryCookieProvider();
            var config = Configuration.Default.With(mcp).With(requester).WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var receivedCookieHeader = "THECOOKIE=value1";
            var url = new Url("http://example.com/path1");
            //request 1: /path1, set a cookie THECOOKIE=value1
            requester.BuildResponse(req => VirtualResponse.Create(r => r
                .Address("http://example.com/path1")
                .Content("")
                .Header(HeaderNames.SetCookie, receivedCookieHeader)));
            context.OpenAsync("http://example.com/path1");
            //request 2: /path1/somefile.jsp redirects to /path2/file2.jsp
            requester.BuildResponses(new Func<Request, IResponse>[] {
                req => VirtualResponse.Create(r => r
                    .Address("http://example.com/path1/somefile.jsp")
                    .Content("")
                    .Status(System.Net.HttpStatusCode.Redirect)
                    .Header(HeaderNames.Location, "http://example.com/path2/file2.jsp")),
                req => {
                    receivedCookieHeader = req.Headers.GetOrDefault(HeaderNames.Cookie, String.Empty);
                    return VirtualResponse.Create(r => r
                    .Address("http://example.com/path2/file2.jsp")
                    .Content(""));
                }
            });
            context.OpenAsync("http://example.com/path1/somefile.jsp");
            Assert.AreEqual(String.Empty, receivedCookieHeader);
        }

        private static Task<IDocument> LoadDocumentWithFakeRequesterAndCookie(IResponse initialResponse,
            Func<Request, IResponse> onRequest)
        {
            var requester = new MockRequester();
            requester.BuildResponse(onRequest);
            var config = Configuration.Default.With(requester)
                .WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true }).WithCookies();
            return BrowsingContext.New(config).OpenAsync(initialResponse, CancellationToken.None);
        }

        private static async Task<IDocument> LoadDocumentAloneWithCookie(String cookieValue)
        {
            var config = Configuration.Default.WithCookies();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res =>
                res.Content("<div></div>").Address("http://localhost/").Header(HeaderNames.SetCookie, cookieValue));

            return document;
        }

        private static async Task<String> LoadDocumentWithCookie(String cookieValue)
        {
            var document = await LoadDocumentAloneWithCookie(cookieValue);
            return document.Cookie;
        }
    }
}