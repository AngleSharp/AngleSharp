namespace AngleSharp.Core.Tests.Library
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Dom.Io;
    using NUnit.Framework;

    [TestFixture]
    public class FormSubmitTests
    {
        const String BaseUrl = "http://anglesharp.azurewebsites.net/";

        static IDocument Load(String url)
        {
            var config = new Configuration().WithDefaultLoader();
            return BrowsingContext.New(config).OpenAsync(Url.Create(url)).Result;
        }

        static IDocument LoadWithMock(String content, String url)
        {
            var config = Configuration.Default.WithDefaultLoader(requesters: new[] { new MockRequester() });
            return BrowsingContext.New(config).OpenAsync(m => m.Content(content).Address(url)).Result;
        }

        static FileEntry GenerateFile()
        {
            var body = new MemoryStream(new Byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 });
            return new FileEntry("Filename.txt", body);
        }

        static FileEntry GenerateFile(Int32 index)
        {
            var content = Enumerable.Range(0, index * 5 + 10).Select(m => (Byte)m).ToArray();
            var body = new MemoryStream(content);
            return new FileEntry(String.Format("Filename{0}.txt", index + 1), body);
        }

        [Test]
        public async Task AsUrlEncodedProducesRightAmountOfAmpersands()
        {
            var url = "http://localhost/";
            var document = LoadWithMock(@"<form method=get>
<input type=button />
<input name=other type=text value=something /><input type=text value=something /><input name=another type=text value=test />
</form>", url);
            var form = document.Forms.OfType<IHtmlFormElement>().FirstOrDefault();
            var result = await form.Submit();
            Assert.IsNotNull(result);
            Assert.AreEqual(url + "?other=something&another=test", result.Url);
        }

        [Test]
        public async Task PostDoNotEncounterNullReferenceExceptionWithoutName()
        {
            var url = "http://localhost/";
            var document = LoadWithMock(@"
<form method=""post"">
<input type=""button"" />
</form>", url);
            var form = document.Forms.OfType<IHtmlFormElement>().FirstOrDefault();
            var result = await form.Submit();
            Assert.IsNotNull(result);
            Assert.AreEqual(url, result.Url);
        }

        [Test]
        public async Task PostUrlencodeNormal()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostUrlencodeNormal";
                var document = Load(url);
                Assert.AreEqual(1, document.Forms.Length);
                var form = document.Forms[0] as HtmlFormElement;
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
                var response = await form.Submit();
                Assert.IsNotNull(response);
                Assert.AreEqual("okay", response.Body.TextContent);
            }
        }

        [Test]
        public async Task PostUrlencodeFile()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostUrlencodeFile";
                var document = Load(url);
                Assert.AreEqual(1, document.Forms.Length);
                var form = document.Forms[0] as HtmlFormElement;
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
                var response = await form.Submit();
                Assert.IsNotNull(response);
                Assert.AreEqual("okay", response.Body.TextContent);
            }
        }

        [Test]
        public async Task PostMultipartNormal()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostMultipartNormal";
                var document = Load(url);
                Assert.AreEqual(1, document.Forms.Length);
                var form = document.Forms[0] as HtmlFormElement;
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
                var response = await form.Submit();
                Assert.IsNotNull(response);
                Assert.AreEqual("okay", response.Body.TextContent);
            }
        }

        [Test]
        public async Task PostMultipartFile()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostMultipartFile";
                var document = Load(url);
                Assert.AreEqual(1, document.Forms.Length);
                var form = document.Forms[0] as HtmlFormElement;
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
                var response = await form.Submit();
                Assert.IsNotNull(response);
                Assert.AreEqual("okay", response.Body.TextContent);
            }
        }

        [Test]
        public async Task PostMultipartFiles()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostMultipartFiles";
                var document = Load(url);
                Assert.AreEqual(1, document.Forms.Length);
                var form = document.Forms[0] as HtmlFormElement;
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

                var response = await form.Submit();
                Assert.IsNotNull(response);
                Assert.AreEqual("okay", response.Body.TextContent);
            }
        }
    }
}
