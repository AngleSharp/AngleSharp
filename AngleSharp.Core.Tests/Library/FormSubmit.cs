using AngleSharp.Core.Tests.Mocks;
using AngleSharp.Dom.Html;
using AngleSharp.Dom.Io;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class FormSubmitTests
    {
        const String BaseUrl = "http://anglesharp.azurewebsites.net/";

        static FileEntry GenerateFile()
        {
            var body = new MemoryStream(new Byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 });
            return FileEntry.FromFile("Filename.txt", body);
        }

        static FileEntry GenerateFile(Int32 index)
        {
            var content = Enumerable.Range(0, index * 5 + 10).Select(m => (Byte)m).ToArray();
            var body = new MemoryStream(content);
            return FileEntry.FromFile(String.Format("Filename{0}.txt", index + 1), body);
        }

        [Test]
        public void AsUrlEncodedProducesRightAmountOfAmpersands()
        {
            var config = new Configuration().Register(new MockRequester());
            var url = "http://localhost/";
            var document = DocumentBuilder.Html(@"<form method=get>
<input type=button />
<input name=other type=text value=something /><input type=text value=something /><input name=another type=text value=test />
</form>", config, url);
            var form = document.Forms.OfType<IHtmlFormElement>().FirstOrDefault();
            var result = form.Submit().Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(url + "?other=something&another=test", result.Url);
        }

        [Test]
        public void PostDoNotEncounterNullReferenceExceptionWithoutName()
        {
            var config = new Configuration().Register(new MockRequester());
            var url = "http://localhost/";
            var document = DocumentBuilder.Html(@"
<form method=""post"">
<input type=""button"" />
</form>", config, url);
            var form = document.Forms.OfType<IHtmlFormElement>().FirstOrDefault();
            var result = form.Submit().Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(url, result.Url);
        }

        [Test]
        public void PostUrlencodeNormal()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostUrlencodeNormal";
                var html = DocumentBuilder.Html(new Uri(url), new Configuration().WithDefaultRequester());
                Assert.AreEqual(1, html.Forms.Length);
                var form = html.Forms[0] as HtmlFormElement;
                var name = form.Elements["Name"] as HtmlInputElement;
                var number = form.Elements["Number"] as HtmlInputElement;
                var isactive = form.Elements["IsActive"] as HtmlInputElement;
                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.AreEqual("text", name.Type);
                Assert.AreEqual("number", number.Type);
                Assert.AreEqual("checkbox", isactive.Type);
                name.Value = "Test";
                number.Value = "1";
                isactive.IsChecked = true;
                var newDoc = form.Submit().Result;
                Assert.IsNotNull(newDoc);
                Assert.AreEqual("okay", newDoc.Body.TextContent);
            }
        }

        [Test]
        public void PostUrlencodeFile()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostUrlencodeFile";
                var html = DocumentBuilder.Html(new Uri(url), new Configuration().WithDefaultRequester());
                Assert.AreEqual(1, html.Forms.Length);
                var form = html.Forms[0] as HtmlFormElement;
                var name = form.Elements["Name"] as HtmlInputElement;
                var number = form.Elements["Number"] as HtmlInputElement;
                var isactive = form.Elements["IsActive"] as HtmlInputElement;
                var file = form.Elements["File"] as HtmlInputElement;
                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.IsNotNull(file);
                Assert.AreEqual("text", name.Type);
                Assert.AreEqual("number", number.Type);
                Assert.AreEqual("checkbox", isactive.Type);
                Assert.AreEqual("file", file.Type);
                name.Value = "Test";
                number.Value = "1";
                isactive.IsChecked = true;
                (file.Files as FileList).Add(GenerateFile());
                var newDoc = form.Submit().Result;
                Assert.IsNotNull(newDoc);
                Assert.AreEqual("okay", newDoc.Body.TextContent);
            }
        }

        [Test]
        public void PostMultipartNormal()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostMultipartNormal";
                var html = DocumentBuilder.Html(new Uri(url), new Configuration().WithDefaultRequester());
                Assert.AreEqual(1, html.Forms.Length);
                var form = html.Forms[0] as HtmlFormElement;
                var name = form.Elements["Name"] as HtmlInputElement;
                var number = form.Elements["Number"] as HtmlInputElement;
                var isactive = form.Elements["IsActive"] as HtmlInputElement;
                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.AreEqual("text", name.Type);
                Assert.AreEqual("number", number.Type);
                Assert.AreEqual("checkbox", isactive.Type);
                name.Value = "Test";
                number.Value = "1";
                isactive.IsChecked = true;
                var newDoc = form.Submit().Result;
                Assert.IsNotNull(newDoc);
                Assert.AreEqual("okay", newDoc.Body.TextContent);
            }
        }

        [Test]
        public void PostMultipartFile()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostMultipartFile";
                var html = DocumentBuilder.Html(new Uri(url), new Configuration().WithDefaultRequester());
                Assert.AreEqual(1, html.Forms.Length);
                var form = html.Forms[0] as HtmlFormElement;
                var name = form.Elements["Name"] as HtmlInputElement;
                var number = form.Elements["Number"] as HtmlInputElement;
                var isactive = form.Elements["IsActive"] as HtmlInputElement;
                var file = form.Elements["File"] as HtmlInputElement;
                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.IsNotNull(file);
                Assert.AreEqual("text", name.Type);
                Assert.AreEqual("number", number.Type);
                Assert.AreEqual("checkbox", isactive.Type);
                Assert.AreEqual("file", file.Type);
                name.Value = "Test";
                number.Value = "1";
                isactive.IsChecked = true;
                (file.Files as FileList).Add(GenerateFile());
                var newDoc = form.Submit().Result;
                Assert.IsNotNull(newDoc);
                Assert.AreEqual("okay", newDoc.Body.TextContent);
            }
        }

        [Test]
        public void PostMultipartFiles()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostMultipartFiles";
                var html = DocumentBuilder.Html(new Uri(url), new Configuration().WithDefaultRequester());
                Assert.AreEqual(1, html.Forms.Length);
                var form = html.Forms[0] as HtmlFormElement;
                var name = form.Elements["Name"] as HtmlInputElement;
                var number = form.Elements["Number"] as HtmlInputElement;
                var isactive = form.Elements["IsActive"] as HtmlInputElement;
                var files = form.Elements["Files"] as HtmlInputElement;
                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.IsNotNull(files);
                Assert.AreEqual("text", name.Type);
                Assert.AreEqual("number", number.Type);
                Assert.AreEqual("checkbox", isactive.Type);
                Assert.AreEqual("file", files.Type);
                Assert.IsTrue(files.IsMultiple);
                name.Value = "Test";
                number.Value = "1";
                isactive.IsChecked = true;

                for (int i = 0; i < 5; i++)
                    (files.Files as FileList).Add(GenerateFile(i));

                var newDoc = form.Submit().Result;
                Assert.IsNotNull(newDoc);
                Assert.AreEqual("okay", newDoc.Body.TextContent);
            }
        }
    }
}
