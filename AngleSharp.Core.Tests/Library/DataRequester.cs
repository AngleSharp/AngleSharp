namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Network.Default;
    using NUnit.Framework;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    [TestFixture]
    public class DataRequesterTests
    {
        [Test]
        public async Task DataGetRequestForPlainText()
        {
            var http = new DataRequester();
            var request = new Request();
            request.Address = new Url("data:,Hello+there");
            request.Method = HttpMethod.Get;

            using (var response = await http.RequestAsync(request, CancellationToken.None))
            {
                Assert.IsNotNull(response);
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);
                Assert.AreEqual("text/plain", response.Headers[HeaderNames.ContentType]);

                var content = new StreamReader(response.Content);
                Assert.AreEqual("Hello there", content.ReadToEnd());
            }
        }

        [Test]
        public async Task DataGetRequestForHtmlWithUtf8UrlEncoded()
        {
            var http = new DataRequester();
            var request = new Request();
            var source = "<!DOCTYPE html><html lang='en'><head><title>Embedded Window</title></head><body><h1>42</h1></body></html>";
            var content = TextEncoding.Utf8.GetBytes(source).UrlEncode();
            request.Address = new Url("data:text/html;charset=utf-8," + content);
            request.Method = HttpMethod.Get;

            using (var response = await http.RequestAsync(request, CancellationToken.None))
            {
                Assert.IsNotNull(response);
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);
                Assert.AreEqual("text/html;charset=utf-8", response.Headers[HeaderNames.ContentType]);

                var reader = new StreamReader(response.Content);
                Assert.AreEqual(source, reader.ReadToEnd());
            }
        }

        [Test]
        public async Task DataGetRequestForHtmlWithUtf8Base64Encoded()
        {
            var http = new DataRequester();
            var request = new Request();
            var source = "<!DOCTYPE html><html lang='en'><head><title>Embedded Window</title></head><body><h1>42</h1></body></html>";
            var content = Convert.ToBase64String(TextEncoding.Utf8.GetBytes(source));
            request.Address = new Url("data:text/html;charset=utf-8;base64," + content);
            request.Method = HttpMethod.Get;

            using (var response = await http.RequestAsync(request, CancellationToken.None))
            {
                Assert.IsNotNull(response);
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);
                Assert.AreEqual("text/html;charset=utf-8", response.Headers[HeaderNames.ContentType]);

                var reader = new StreamReader(response.Content);
                Assert.AreEqual(source, reader.ReadToEnd());
            }
        }

        [Test]
        public async Task DataGetRequestForMiddleImageBase64Encoded()
        {
            var http = new DataRequester();
            var request = new Request();
            var content = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQAQMAAAAlPW0iAAAABlBMVEUAAAD///+l2Z/dAAAAM0lEQVR4nGP4/5/h/1+G/58ZDrAz3D/McH8yw83NDDeNGe4Ug9C9zwz3gVLMDA/A6P9/AFGGFyjOXZtQAAAAAElFTkSuQmCC";
            request.Address = new Url(content);
            request.Method = HttpMethod.Get;

            using (var response = await http.RequestAsync(request, CancellationToken.None))
            {
                Assert.IsNotNull(response);
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);
                Assert.AreEqual("image/png", response.Headers[HeaderNames.ContentType]);
            }
        }

        [Test]
        public async Task DataGetRequestForSmallImageBase64Encoded()
        {
            var http = new DataRequester();
            var request = new Request();
            var content = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==";
            request.Address = new Url(content);
            request.Method = HttpMethod.Get;

            using (var response = await http.RequestAsync(request, CancellationToken.None))
            {
                Assert.IsNotNull(response);
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.IsTrue(response.Content.CanRead);
                Assert.IsTrue(response.Headers.Count > 0);
                Assert.AreEqual("image/png", response.Headers[HeaderNames.ContentType]);
            }
        }
    }
}
