namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp;
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    [TestFixture]
    public class HttpRequesterTests
    {
        [Test]
        public async Task SimpleHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new DefaultHttpRequester();
                var request = new Request
                {
                    Address = new Url("http://httpbin.org/robots.txt"),
                    Method = HttpMethod.Get
                };

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
                var http = new DefaultHttpRequester();
                var request = new Request
                {
                    Address = new Url("http://httpbin.org/status/500"),
                    Method = HttpMethod.Get
                };

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
                var http = new DefaultHttpRequester();
                var request = new Request
                {
                    Address = new Url("http://httpbin.org/status/400"),
                    Method = HttpMethod.Get
                };

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
                var http = new DefaultHttpRequester();
                var request = new Request
                {
                    Address = new Url("http://httpbin.org/status/403"),
                    Method = HttpMethod.Get
                };

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
                var http = new DefaultHttpRequester();
                var request = new Request
                {
                    Address = new Url("http://httpbin.org/status/404"),
                    Method = HttpMethod.Get
                };

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
                var http = new DefaultHttpRequester();
                var request = new Request
                {
                    Address = new Url("http://httpbin.org/post"),
                    Method = HttpMethod.Post,
                    Content = Helper.StreamFromString("Hello world")
                };

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
                var http = new DefaultHttpRequester();
                var request = new Request
                {
                    Address = new Url("http://httpbin.org/put"),
                    Method = HttpMethod.Put,
                    Content = Helper.StreamFromString("PUT THIS THING BACK")
                };

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
                var http = new DefaultHttpRequester();
                var request = new Request
                {
                    Address = new Url("http://httpbin.org/delete"),
                    Method = HttpMethod.Delete,
                    Content = Helper.StreamFromString("Should be ignored")
                };

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
                var http = new DefaultHttpRequester();
                var request = new Request
                {
                    Address = new Url("http://httpbin.org/get"),
                    Method = HttpMethod.Delete,
                    Content = Helper.StreamFromString("Should be ignored")
                };

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
                var http = new DefaultHttpRequester();
                var request = new Request
                {
                    Address = new Url("http://httpbin.org/get"),
                    Method = HttpMethod.Put,
                    Content = Helper.StreamFromString("Should be ignored")
                };

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
                var http = new DefaultHttpRequester(agent);
                var request = new Request
                {
                    Address = new Url("http://httpbin.org/user-agent"),
                    Method = HttpMethod.Get
                };

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
                var http = new DefaultHttpRequester();
                var request = new Request
                {
                    Address = new Url("http://httpbin.org/robots.txt"),
                    Method = HttpMethod.Get
                };

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

        [Test]
        public async Task FilteringRequestsWork()
        {
            var requester = new MockRequester();
            var requests = new List<Request>();
            var filtered = new List<Request>();
            requester.OnRequest = request => requests.Add(request);
            var content = "<!doctype><html><div><img src=foo.jpg><iframe src=test.html></iframe></div>";
            var config = Configuration.Default.With(requester).WithDefaultLoader(new LoaderOptions
            {
                IsResourceLoadingEnabled = true,
                Filter = request =>
                {
                    lock (filtered)
                    {
                        filtered.Add(request);
                    }

                    return !request.Address.Href.EndsWith(".jpg");
                }
            });

            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(m => m.Content(content).Address("http://localhost"));
            Assert.IsNotNull(document);
            Assert.AreEqual(1, requests.Count);
            Assert.AreEqual(2, filtered.Count);
            Assert.AreEqual("test.html", requests[0].Address.Path);
        }

        [Test]
        public async Task HttpRequesterShouldNotHang()
        {
            if (Helper.IsNetworkAvailable())
            {
                var address = "https://serverspace.ae";
                var cts = new CancellationTokenSource();
                var config = Configuration.Default.WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });

                var context = BrowsingContext.New(config);
                var url = Url.Create(address);
                var document = await context.OpenAsync(url, cts.Token);
                Assert.IsNotNull(document);
            }
        }
    }
}
