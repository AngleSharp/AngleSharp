using System.Threading;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class ContextLoadingTests
    {
        [Test]
        public void ContextLoadEmptyDocumentWithoutUrl()
        {
            var document = default(IBrowsingContext).OpenNew();
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.DocumentElement);
            Assert.IsNotNull(document.Body);
            Assert.IsNotNull(document.Head);
            Assert.AreEqual("", document.DocumentUri);
            Assert.AreEqual(2, document.DocumentElement.ChildElementCount);
            Assert.AreEqual(0, document.Body.ChildElementCount);
            Assert.AreEqual(0, document.Head.ChildElementCount);
        }

        [Test]
        public void ContextLoadEmptyDocumentWithUrl()
        {
            var document = default(IBrowsingContext).OpenNew(url: "http://localhost:8081");
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.DocumentElement);
            Assert.IsNotNull(document.Body);
            Assert.IsNotNull(document.Head);
            Assert.AreEqual("http://localhost:8081/", document.DocumentUri);
            Assert.AreEqual(2, document.DocumentElement.ChildElementCount);
            Assert.AreEqual(0, document.Body.ChildElementCount);
            Assert.AreEqual(0, document.Head.ChildElementCount);
        }

        [Test]
        public void ContextLoadFromUrl()
        {
            var url = "http://anglesharp.azurewebsites.net/PostUrlEncodeNormal";
            default(IBrowsingContext).OpenAsync(Url.Create(url), CancellationToken.None).ContinueWith(t =>
            {
                var document = t.Result;
                var h1 = document.QuerySelector("h1");
                Assert.IsNotNull(document);
                Assert.IsNotNull(document.DocumentElement);
                Assert.IsNotNull(document.Body);
                Assert.IsNotNull(document.Head);
                Assert.AreEqual(url, document.DocumentUri);
                Assert.AreEqual("PostUrlencodeNormal - My ASP.NET Application", document.Title);
                Assert.AreEqual("PostUrlencodeNormal", h1.TextContent);
            }).Wait();
        }
    }
}
