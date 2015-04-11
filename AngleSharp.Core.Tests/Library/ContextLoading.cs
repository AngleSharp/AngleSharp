using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class ContextLoadingTests
    {
        [Test]
        public void ContextLoadEmptyDocumentWithoutUrl()
        {
            var document = BrowsingContext.New().OpenNewAsync().Result;
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
            var document = BrowsingContext.New().OpenNewAsync(url: "http://localhost:8081").Result;
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
            if (Helper.IsNetworkAvailable())
            {
                var url = "http://anglesharp.azurewebsites.net/PostUrlEncodeNormal";
                var config = new Configuration().WithDefaultLoader();
                var task = BrowsingContext.New(config).OpenAsync(Url.Create(url), CancellationToken.None).ContinueWith(t =>
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
                    return true;
                });
                var result = task.Result;
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void ContextFormSubmission()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = "http://anglesharp.azurewebsites.net/PostUrlEncodeNormal";
                var config = new Configuration().WithDefaultLoader();
                var context = BrowsingContext.New(config);
                Func<Task<IDocument>, IDocument> loadForm = t =>
                {
                    var document = t.Result;
                    Assert.AreEqual(1, document.Forms.Length);
                    var form = document.Forms[0];
                    var name = form.Elements["Name"] as IHtmlInputElement;
                    var number = form.Elements["Number"] as IHtmlInputElement;
                    var isactive = form.Elements["IsActive"] as IHtmlInputElement;
                    Assert.IsNotNull(name);
                    Assert.IsNotNull(number);
                    Assert.IsNotNull(isactive);
                    Assert.AreEqual("text", name.Type);
                    Assert.AreEqual("number", number.Type);
                    Assert.AreEqual("checkbox", isactive.Type);
                    name.Value = "Test";
                    number.Value = "1";
                    isactive.IsChecked = true;
                    return form.Submit().Result;
                };

                var task = context.OpenAsync(Url.Create(url), CancellationToken.None).ContinueWith(loadForm);
                var result = task.Result;

                Assert.IsNotNull(result);
                Assert.AreEqual(result, context.Active);
                Assert.AreEqual("okay", context.Active.Body.TextContent);
            }
        }
    }
}
