namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The loader follows redirects itself, so a 3xx response is read for its headers and replaced
    /// by the next one; nothing else holds it afterwards. A requester that streams keeps the
    /// connection of a response until it is disposed.
    /// </summary>
    [TestFixture]
    public class RedirectHandling
    {
        [Test]
        public async Task FollowedRedirectsAreDisposedAndTheFinalResponseIsRead()
        {
            var found = Redirect(HttpStatusCode.Redirect, "http://a.example/start", "http://a.example/middle");
            var moved = Redirect(HttpStatusCode.MovedPermanently, "http://a.example/middle", "http://a.example/end");
            var ok = Page("http://a.example/end", "<p>end</p>");
            var requester = new ScriptedRequester("http", found, moved, ok);
            var context = BrowsingContext.New(Configuration.Default.With(requester).WithDefaultLoader());

            var document = await context.OpenAsync("http://a.example/start");

            Assert.AreEqual("end", document.QuerySelector("p").TextContent);
            Assert.AreEqual("http://a.example/end", document.Url);
            Assert.IsTrue(found.IsDisposed);
            Assert.IsTrue(moved.IsDisposed);
            Assert.IsTrue(ok.IsDisposed);
        }

        [Test]
        public async Task RedirectToAnUnsupportedSchemeIsReturnedUndisposed()
        {
            var found = Redirect(HttpStatusCode.Redirect, "http://a.example/start", "ftp://a.example/file");
            found.Headers[HeaderNames.SetCookie] = "k=v";
            var requester = new ScriptedRequester("http", found);
            var cookies = new CountingCookies();
            var context = BrowsingContext.New(Configuration.Default.With(requester).With(cookies));
            var loader = new DefaultDocumentLoader(context);

            var response = await loader.FetchAsync(new DocumentRequest(Url.Create("http://a.example/start"))).Task;

            Assert.AreSame(found, response);
            Assert.IsFalse(found.IsDisposed);
            Assert.AreEqual(1, requester.Requests);
            // Its cookie is stored once, not once for every round up to MaxRedirects
            Assert.AreEqual(1, cookies.Stored);
        }

        [Test]
        public void RedirectIsDisposedWhenTheNextRequestFails()
        {
            var found = Redirect(HttpStatusCode.Redirect, "http://a.example/start", "http://a.example/end");
            var requester = new ScriptedRequester("http", found, null);
            var context = BrowsingContext.New(Configuration.Default.With(requester));
            var loader = new DefaultDocumentLoader(context);
            var download = loader.FetchAsync(new DocumentRequest(Url.Create("http://a.example/start")));

            Assert.ThrowsAsync<InvalidOperationException>(() => download.Task);
            Assert.IsTrue(found.IsDisposed);
            Assert.AreEqual(2, requester.Requests);
        }

        private static RecordingResponse Redirect(HttpStatusCode status, String address, String location)
        {
            var response = new RecordingResponse(status, address, String.Empty);
            response.Headers[HeaderNames.Location] = location;
            return response;
        }

        private static RecordingResponse Page(String address, String html)
        {
            var response = new RecordingResponse(HttpStatusCode.OK, address, html);
            response.Headers[HeaderNames.ContentType] = "text/html";
            return response;
        }

        private sealed class RecordingResponse : IResponse
        {
            public RecordingResponse(HttpStatusCode status, String address, String content)
            {
                StatusCode = status;
                Address = Url.Create(address);
                Content = new MemoryStream(Encoding.UTF8.GetBytes(content));
            }

            public HttpStatusCode StatusCode { get; }

            public Url Address { get; }

            public IDictionary<String, String> Headers { get; } = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);

            public Stream Content { get; }

            public Boolean IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
                Content.Dispose();
            }
        }

        private sealed class CountingCookies : ICookieProvider
        {
            public Int32 Stored { get; private set; }

            public String GetCookie(Url url) => String.Empty;

            public void SetCookie(Url url, String value) => Stored++;
        }

        /// <summary>
        /// Answers with the given responses in order, for one scheme only; a null answer fails
        /// the request.
        /// </summary>
        private sealed class ScriptedRequester : BaseRequester
        {
            private readonly String _scheme;
            private readonly Queue<RecordingResponse> _answers;

            public ScriptedRequester(String scheme, params RecordingResponse[] answers)
            {
                _scheme = scheme;
                _answers = new Queue<RecordingResponse>(answers);
            }

            public Int32 Requests { get; private set; }

            public override Boolean SupportsProtocol(String protocol) => protocol.Equals(_scheme, StringComparison.OrdinalIgnoreCase);

            protected override Task<IResponse> PerformRequestAsync(Request request, CancellationToken cancel)
            {
                Requests++;
                var answer = _answers.Dequeue();

                if (answer is null)
                {
                    throw new InvalidOperationException("The request failed.");
                }

                return Task.FromResult<IResponse>(answer);
            }
        }
    }
}
