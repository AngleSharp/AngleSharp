using AngleSharp;
using AngleSharp.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTests.Library
{
    [TestClass]
    public class HttpRequesterTests
    {
        [TestMethod]
        public void DefaultGetHttpRequester()
        {
            Assert.IsFalse(Configuration.Default.AllowRequests);
            Assert.IsInstanceOfType(Configuration.Default.GetRequester(), typeof(DefaultRequester));
        }

        [TestMethod]
        public void SimpleHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new DefaultRequester(new DefaultInfo());
                var request = new DefaultRequest();
                request.Address = new Url("http://httpbin.org/robots.txt");
                request.Method = HttpMethod.Get;

                var response = http.Request(request);
                Assert.IsNotNull(response);
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);

                var content = new StreamReader(response.Content);
                Assert.AreEqual("User-agent: *\nDisallow: /deny\n", content.ReadToEnd());
            }
        }

        [TestMethod]
        public void StatusCode500OfHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new DefaultRequester(new DefaultInfo());
                var request = new DefaultRequest();
                request.Address = new Url("http://httpbin.org/status/500");
                request.Method = HttpMethod.Get;

                var response = http.Request(request);
                Assert.IsNotNull(response);
                Assert.AreEqual(500, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);
            }
        }

        [TestMethod]
        public void StatusCode400OfHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new DefaultRequester(new DefaultInfo());
                var request = new DefaultRequest();
                request.Address = new Url("http://httpbin.org/status/400");
                request.Method = HttpMethod.Get;

                var response = http.Request(request);
                Assert.IsNotNull(response);
                Assert.AreEqual(400, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);
            }
        }

        [TestMethod]
        public void StatusCode403OfHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new DefaultRequester(new DefaultInfo());
                var request = new DefaultRequest();
                request.Address = new Url("http://httpbin.org/status/403");
                request.Method = HttpMethod.Get;

                var response = http.Request(request);
                Assert.IsNotNull(response);
                Assert.AreEqual(403, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);
            }
        }

        [TestMethod]
        public void StatusCode404OfHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new DefaultRequester(new DefaultInfo());
                var request = new DefaultRequest();
                request.Address = new Url("http://httpbin.org/status/404");
                request.Method = HttpMethod.Get;

                var response = http.Request(request);
                Assert.IsNotNull(response);
                Assert.AreEqual(404, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);
            }
        }

        [TestMethod]
        public void SimpleHttpPostRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new DefaultRequester(new DefaultInfo());
                var request = new DefaultRequest();
                request.Address = new Url("http://httpbin.org/post");
                request.Method = HttpMethod.Post;
                request.Content = Helper.StreamFromString("Hello world");

                var response = http.Request(request);
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

        [TestMethod]
        public void SimpleHttpPutRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new DefaultRequester(new DefaultInfo());
                var request = new DefaultRequest();
                request.Address = new Url("http://httpbin.org/put");
                request.Method = HttpMethod.Put;
                request.Content = Helper.StreamFromString("PUT THIS THING BACK");

                var response = http.Request(request);
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

        [TestMethod]
        public void SimpleHttpDeleteRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new DefaultRequester(new DefaultInfo());
                var request = new DefaultRequest();
                request.Address = new Url("http://httpbin.org/delete");
                request.Method = HttpMethod.Delete;
                request.Content = Helper.StreamFromString("Should be ignored");

                var response = http.Request(request);
                Assert.IsNotNull(response);
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);
            }
        }

        [TestMethod]
        public void MethodNotAllowedOnHttpDelete()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new DefaultRequester(new DefaultInfo());
                var request = new DefaultRequest();
                request.Address = new Url("http://httpbin.org/get");
                request.Method = HttpMethod.Delete;
                request.Content = Helper.StreamFromString("Should be ignored");

                var response = http.Request(request);
                Assert.IsNotNull(response);
                Assert.AreEqual(405, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);
            }
        }

        [TestMethod]
        public void MethodNotAllowedOnHttpPut()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new DefaultRequester(new DefaultInfo());
                var request = new DefaultRequest();
                request.Address = new Url("http://httpbin.org/get");
                request.Method = HttpMethod.Put;
                request.Content = Helper.StreamFromString("Should be ignored");

                var response = http.Request(request);
                Assert.IsNotNull(response);
                Assert.AreEqual(405, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);
            }
        }

        [TestMethod]
        public void RequestUserAgentString()
        {
            if (Helper.IsNetworkAvailable())
            {
                var info = new DefaultInfo();
                var http = new DefaultRequester(info);
                var request = new DefaultRequest();
                request.Address = new Url("http://httpbin.org/user-agent");
                request.Method = HttpMethod.Get;

                var response = http.Request(request);
                Assert.IsNotNull(response);
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);

                var stream = new StreamReader(response.Content);
                Assert.IsNotNull(stream);

                var content = stream.ReadToEnd();
                Assert.IsTrue(content.Length > 0);
                Assert.AreEqual("{\n  \"user-agent\": \"" + info.Agent + "\"\n}", content);
            }
        }

        [TestMethod]
        public void AsyncHttpGetRequest()
        {
            if (Helper.IsNetworkAvailable())
            {
                var http = new DefaultRequester(new DefaultInfo());
                var request = new DefaultRequest();
                request.Address = new Url("http://httpbin.org/robots.txt");
                request.Method = HttpMethod.Get;

                var response = http.RequestAsync(request);
                Assert.IsNotNull(response);
                Assert.IsFalse(response.IsCompleted);

                var result = response.Result;

                Assert.IsTrue(response.IsCompleted);
                Assert.IsTrue(result.Content.CanRead);
                Assert.IsTrue(result.Headers.Count > 0);

                var content = new StreamReader(result.Content);
                Assert.AreEqual("User-agent: *\nDisallow: /deny\n", content.ReadToEnd());
            }
        }
    }
}
