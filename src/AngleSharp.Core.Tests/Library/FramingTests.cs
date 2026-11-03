namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Browser;
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class FramingTests
    {
        private static IBrowsingContext CreateContext(Func<String, (String Html, Dictionary<String, String> Headers)> site) =>
            BrowsingContext.New(Configuration.Default
                .With(new VirtualRequester(request =>
                {
                    var (html, headers) = site(request.Address.Href);
                    return new DefaultResponse
                    {
                        Address = request.Address,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new MemoryStream(Encoding.UTF8.GetBytes(html)),
                        Headers = headers,
                    };
                }))
                .WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true }));

        private static async Task<IDocument> LoadFrameAsync(String pageAddress, String frameAddress, String header, String value, String attributes = "")
        {
            var context = CreateContext(address =>
            {
                var headers = new Dictionary<String, String> { ["Content-Type"] = "text/html" };

                if (address == pageAddress)
                {
                    return ($"<!doctype html><iframe id=f {attributes} src=\"{frameAddress}\"></iframe>", headers);
                }

                if (header is not null)
                {
                    headers[header] = value;
                }

                return ("<!doctype html><title>framed</title>", headers);
            });
            var page = await context.OpenAsync(pageAddress);
            return ((IHtmlInlineFrameElement)page.GetElementById("f")).ContentDocument;
        }

        [TestCase(null, null, true)]
        [TestCase("X-Frame-Options", "DENY", false)]
        [TestCase("x-frame-options", "deny", false)]
        [TestCase("X-Frame-Options", "SAMEORIGIN", false)]
        [TestCase("X-Frame-Options", "ALLOW-FROM https://a.test", true)]
        [TestCase("X-Frame-Options", "nonsense", true)]
        [TestCase("X-Frame-Options", "SAMEORIGIN, DENY", false)]
        [TestCase("Content-Security-Policy", "frame-ancestors 'none'", false)]
        [TestCase("Content-Security-Policy", "frame-ancestors 'self'", false)]
        [TestCase("Content-Security-Policy", "frame-ancestors *", true)]
        [TestCase("Content-Security-Policy", "frame-ancestors https:", true)]
        [TestCase("Content-Security-Policy", "frame-ancestors http:", false)]
        [TestCase("Content-Security-Policy", "frame-ancestors https://a.test", true)]
        [TestCase("Content-Security-Policy", "frame-ancestors a.test", true)]
        [TestCase("Content-Security-Policy", "frame-ancestors https://c.test", false)]
        [TestCase("Content-Security-Policy", "frame-ancestors *.test", true)]
        [TestCase("Content-Security-Policy", "frame-ancestors *.other", false)]
        [TestCase("Content-Security-Policy", "frame-ancestors https://a.test:8443", false)]
        [TestCase("Content-Security-Policy", "frame-ancestors https://c.test https://a.test", true)]
        [TestCase("Content-Security-Policy", "default-src 'self'; frame-ancestors 'none'", false)]
        [TestCase("Content-Security-Policy", "default-src 'none'", true)]
        [TestCase("Content-Security-Policy", "frame-ancestors *, frame-ancestors 'none'", false)]
        [TestCase("Content-Security-Policy-Report-Only", "frame-ancestors 'none'", true)]
        public async Task FrameOfAnotherOriginFollowsWhatItsResponseSays(String header, String value, Boolean isShown)
        {
            var frame = await LoadFrameAsync("https://a.test/", "https://b.test/f", header, value);

            Assert.AreEqual(isShown, frame is not null);
        }

        [TestCase("X-Frame-Options", "SAMEORIGIN", true)]
        [TestCase("Content-Security-Policy", "frame-ancestors 'self'", true)]
        [TestCase("X-Frame-Options", "DENY", false)]
        [TestCase("Content-Security-Policy", "frame-ancestors 'none'", false)]
        public async Task FrameOfTheSameOriginFollowsWhatItsResponseSays(String header, String value, Boolean isShown)
        {
            var frame = await LoadFrameAsync("https://a.test/", "https://a.test/f", header, value);

            Assert.AreEqual(isShown, frame is not null);
        }

        [TestCase("X-Frame-Options", "SAMEORIGIN")]
        [TestCase("Content-Security-Policy", "frame-ancestors 'self'")]
        public async Task PageInBetweenCannotGetAroundIt(String header, String value)
        {
            var context = CreateContext(address =>
            {
                var headers = new Dictionary<String, String> { ["Content-Type"] = "text/html" };

                switch (address)
                {
                    case "https://a.test/":
                        return ("<!doctype html><iframe id=f src=\"https://b.test/\"></iframe>", headers);
                    case "https://b.test/":
                        return ("<!doctype html><iframe id=g src=\"https://b.test/inner\"></iframe>", headers);
                    default:
                        headers[header] = value;
                        return ("<!doctype html><title>inner</title>", headers);
                }
            });

            var page = await context.OpenAsync("https://a.test/");
            var middle = ((IHtmlInlineFrameElement)page.GetElementById("f")).ContentDocument;
            var inner = ((IHtmlInlineFrameElement)middle.GetElementById("g")).ContentDocument;

            Assert.IsNull(inner);
        }

        [Test]
        public async Task PolicyTakesPrecedenceOverFrameOptions()
        {
            var context = CreateContext(address =>
            {
                var headers = new Dictionary<String, String> { ["Content-Type"] = "text/html" };

                if (address == "https://a.test/")
                {
                    return ("<!doctype html><iframe id=f src=\"https://b.test/\"></iframe>", headers);
                }

                headers["X-Frame-Options"] = "DENY";
                headers["Content-Security-Policy"] = "frame-ancestors https://a.test";
                return ("<!doctype html>", headers);
            });

            var page = await context.OpenAsync("https://a.test/");

            Assert.IsNotNull(((IHtmlInlineFrameElement)page.GetElementById("f")).ContentDocument);
        }

        [TestCase("", Sandboxes.Scripts, true)]
        [TestCase("", Sandboxes.Origin, true)]
        [TestCase("", Sandboxes.Forms, true)]
        [TestCase("", Sandboxes.TopLevelNavigation, true)]
        [TestCase("allow-scripts", Sandboxes.Scripts, false)]
        [TestCase("allow-same-origin", Sandboxes.Origin, false)]
        [TestCase("allow-forms", Sandboxes.Forms, false)]
        [TestCase("allow-top-navigation", Sandboxes.TopLevelNavigation, false)]
        [TestCase("allow-popups", Sandboxes.AuxiliaryNavigation, false)]
        [TestCase("allow-scripts", Sandboxes.Origin, true)]
        public async Task SandboxAppliesToTheFrame(String tokens, Sandboxes flag, Boolean isSet)
        {
            var frame = await LoadFrameAsync("https://a.test/", "https://b.test/f", null, null, $"sandbox=\"{tokens}\"");

            Assert.AreEqual(isSet, frame.Context.Security.HasFlag(flag));
        }

        [Test]
        public async Task FrameWithoutSandboxHasNoFlags()
        {
            var frame = await LoadFrameAsync("https://a.test/", "https://b.test/f", null, null);

            Assert.AreEqual(Sandboxes.None, frame.Context.Security);
        }

        [Test]
        public async Task SandboxOfTheFrameAroundStillApplies()
        {
            var context = CreateContext(address =>
            {
                var headers = new Dictionary<String, String> { ["Content-Type"] = "text/html" };
                return address switch
                {
                    "https://a.test/" => ("<!doctype html><iframe id=f sandbox=allow-forms src=\"https://b.test/\"></iframe>", headers),
                    "https://b.test/" => ("<!doctype html><iframe id=g src=\"https://b.test/inner\"></iframe>", headers),
                    _ => ("<!doctype html>", headers),
                };
            });

            var page = await context.OpenAsync("https://a.test/");
            var middle = ((IHtmlInlineFrameElement)page.GetElementById("f")).ContentDocument;
            var inner = ((IHtmlInlineFrameElement)middle.GetElementById("g")).ContentDocument;

            Assert.IsTrue(inner.Context.Security.HasFlag(Sandboxes.Scripts));
        }

        [Test]
        public async Task SrcdocOfASandboxedFrameIsSandboxed()
        {
            var context = CreateContext(_ => ("<!doctype html><iframe id=f sandbox srcdoc=\"x\"></iframe>", new Dictionary<String, String> { ["Content-Type"] = "text/html" }));

            var page = await context.OpenAsync("https://a.test/");
            var frame = ((IHtmlInlineFrameElement)page.GetElementById("f")).ContentDocument;

            Assert.IsTrue(frame.Context.Security.HasFlag(Sandboxes.Scripts));
        }

        [Test]
        public async Task DocumentOfAFrameIsNeverLost()
        {
            for (var i = 0; i < 50; i++)
            {
                var frame = await LoadFrameAsync("https://a.test/", "https://b.test/f", null, null);
                Assert.IsNotNull(frame, $"Lost in round {i}.");
            }
        }
    }
}
