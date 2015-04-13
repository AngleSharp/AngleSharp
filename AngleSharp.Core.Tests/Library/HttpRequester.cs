namespace AngleSharp.Core.Tests.Library
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp;
    using AngleSharp.Network;
    using AngleSharp.Network.Default;
    using NUnit.Framework;

    [TestFixture]
    public class HttpRequesterTests
    {
        [Test]
        public async Task SimpleHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new HttpRequester();
                var request = new Request();
                request.Address = new Url("http://httpbin.org/robots.txt");
                request.Method = HttpMethod.Get;

                using (var response = await http.RequestAsync(request, CancellationToken.None))
                {
                    Assert.IsNotNull(response);
                    Assert.AreEqual(200, (int)response.StatusCode);
                    Assert.IsTrue(response.Content.CanRead);
                    Assert.IsTrue(response.Headers.Count > 0);

                    var content = new StreamReader(response.Content);
                    Assert.AreEqual("User-agent: *\nDisallow: /deny\n", content.ReadToEnd());
                }
            }
        }

        [Test]
        public async Task StatusCode500OfHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new HttpRequester();
                var request = new Request();
                request.Address = new Url("http://httpbin.org/status/500");
                request.Method = HttpMethod.Get;

                using (var response = await http.RequestAsync(request, CancellationToken.None))
                {
                    Assert.IsNotNull(response);
                    Assert.AreEqual(500, (int)response.StatusCode);
                    Assert.IsTrue(response.Content.CanRead);
                    Assert.IsTrue(response.Headers.Count > 0);
                }
            }
        }

        [Test]
        public async Task StatusCode400OfHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new HttpRequester();
                var request = new Request();
                request.Address = new Url("http://httpbin.org/status/400");
                request.Method = HttpMethod.Get;

                using (var response = await http.RequestAsync(request, CancellationToken.None))
                {
                    Assert.IsNotNull(response);
                    Assert.AreEqual(400, (int)response.StatusCode);
                    Assert.IsTrue(response.Content.CanRead);
                    Assert.IsTrue(response.Headers.Count > 0);
                }
            }
        }

        [Test]
        public async Task StatusCode403OfHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new HttpRequester();
                var request = new Request();
                request.Address = new Url("http://httpbin.org/status/403");
                request.Method = HttpMethod.Get;

                using (var response = await http.RequestAsync(request, CancellationToken.None))
                {
                    Assert.IsNotNull(response);
                    Assert.AreEqual(403, (int)response.StatusCode);
                    Assert.IsTrue(response.Content.CanRead);
                    Assert.IsTrue(response.Headers.Count > 0);
                }
            }
        }

        [Test]
        public async Task StatusCode404OfHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new HttpRequester();
                var request = new Request();
                request.Address = new Url("http://httpbin.org/status/404");
                request.Method = HttpMethod.Get;

                using (var response = await http.RequestAsync(request, CancellationToken.None))
                {
                    Assert.IsNotNull(response);
                    Assert.AreEqual(404, (int)response.StatusCode);
                    Assert.IsTrue(response.Content.CanRead);
                    Assert.IsTrue(response.Headers.Count > 0);
                }
            }
        }

        [Test]
        public async Task SimpleHttpPostRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new HttpRequester();
                var request = new Request();
                request.Address = new Url("http://httpbin.org/post");
                request.Method = HttpMethod.Post;
                request.Content = Helper.StreamFromString("Hello world");

                using (var response = await http.RequestAsync(request, CancellationToken.None))
                {
                    Assert.IsNotNull(response);
                    Assert.AreEqual(200, (int)response.StatusCode);
                    Assert.IsTrue(response.Content.CanRead);
                    Assert.IsTrue(response.Headers.Count > 0);

                    var stream = new StreamReader(response.Content);
                    Assert.IsNotNull(stream);

                    var content = stream.ReadToEnd();
                    Assert.IsTrue(content.Length > 0);
                    Assert.IsTrue(content.Contains("\"data\": \"Hello world\""));
                }
            }
        }

        [Test]
        public async Task SimpleHttpPutRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new HttpRequester();
                var request = new Request();
                request.Address = new Url("http://httpbin.org/put");
                request.Method = HttpMethod.Put;
                request.Content = Helper.StreamFromString("PUT THIS THING BACK");

                using (var response = await http.RequestAsync(request, CancellationToken.None))
                {
                    Assert.IsNotNull(response);
                    Assert.AreEqual(200, (int)response.StatusCode);
                    Assert.IsTrue(response.Content.CanRead);
                    Assert.IsTrue(response.Headers.Count > 0);

                    var stream = new StreamReader(response.Content);
                    Assert.IsNotNull(stream);

                    var content = stream.ReadToEnd();
                    Assert.IsTrue(content.Length > 0);
                    Assert.IsTrue(content.Contains("\"data\": \"PUT THIS THING BACK\""));
                }
            }
        }

        [Test]
        public async Task SimpleHttpDeleteRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new HttpRequester();
                var request = new Request();
                request.Address = new Url("http://httpbin.org/delete");
                request.Method = HttpMethod.Delete;
                request.Content = Helper.StreamFromString("Should be ignored");

                using (var response = await http.RequestAsync(request, CancellationToken.None))
                {
                    Assert.IsNotNull(response);
                    Assert.AreEqual(200, (int)response.StatusCode);
                    Assert.IsTrue(response.Content.CanRead);
                    Assert.IsTrue(response.Headers.Count > 0);
                }
            }
        }

        [Test]
        public async Task MethodNotAllowedOnHttpDelete()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new HttpRequester();
                var request = new Request();
                request.Address = new Url("http://httpbin.org/get");
                request.Method = HttpMethod.Delete;
                request.Content = Helper.StreamFromString("Should be ignored");

                using (var response = await http.RequestAsync(request, CancellationToken.None))
                {
                    Assert.IsNotNull(response);
                    Assert.AreEqual(405, (int)response.StatusCode);
                    Assert.IsTrue(response.Content.CanRead);
                    Assert.IsTrue(response.Headers.Count > 0);
                }
            }
        }

        [Test]
        public async Task MethodNotAllowedOnHttpPut()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new HttpRequester();
                var request = new Request();
                request.Address = new Url("http://httpbin.org/get");
                request.Method = HttpMethod.Put;
                request.Content = Helper.StreamFromString("Should be ignored");

                using (var response = await http.RequestAsync(request, CancellationToken.None))
                {
                    Assert.IsNotNull(response);
                    Assert.AreEqual(405, (int)response.StatusCode);
                    Assert.IsTrue(response.Content.CanRead);
                    Assert.IsTrue(response.Headers.Count > 0);
                }
            }
        }

        [Test]
        public async Task RequestUserAgentString()
        {
            if (Helper.IsNetworkAvailable())
            {
                var agent = "MyAgent";
                var http = new HttpRequester(agent);
                var request = new Request();
                request.Address = new Url("http://httpbin.org/user-agent");
                request.Method = HttpMethod.Get;

                using (var response = await http.RequestAsync(request, CancellationToken.None))
                {
                    Assert.IsNotNull(response);
                    Assert.AreEqual(200, (int)response.StatusCode);
                    Assert.IsTrue(response.Content.CanRead);
                    Assert.IsTrue(response.Headers.Count > 0);

                    var stream = new StreamReader(response.Content);
                    Assert.IsNotNull(stream);

                    var content = stream.ReadToEnd();
                    Assert.IsTrue(content.Length > 0);
                    Assert.AreEqual("{\n  \"user-agent\": \"" + agent + "\"\n}\n", content);
                }
            }
        }

        [Test]
        public async Task AsyncHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new HttpRequester();
                var request = new Request();
                request.Address = new Url("http://httpbin.org/robots.txt");
                request.Method = HttpMethod.Get;

                using (var response = http.RequestAsync(request, CancellationToken.None))
                {
                    Assert.IsNotNull(response);
                    Assert.IsFalse(response.IsCompleted);

                    var result = await response;

                    Assert.IsTrue(response.IsCompleted);
                    Assert.IsTrue(result.Content.CanRead);
                    Assert.IsTrue(result.Headers.Count > 0);

                    var content = new StreamReader(result.Content);
                    Assert.AreEqual("User-agent: *\nDisallow: /deny\n", content.ReadToEnd());
                }
            }
        }
    }
}
