using AngleSharp;
using AngleSharp.DOM.Html;
using AngleSharp.DOM.Io;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace UnitTests.Library
{
    [TestClass]
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

        [TestMethod]
        public void PostUrlencodeNormal()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostUrlencodeNormal";
                var html = DocumentBuilder.Html(new Uri(url), new Configuration { AllowRequests = true });
                Assert.AreEqual(1, html.Forms.Length);
                var form = html.Forms[0] as HTMLFormElement;
                var name = form.Elements["Name"] as HTMLInputElement;
                var number = form.Elements["Number"] as HTMLInputElement;
                var isactive = form.Elements["IsActive"] as HTMLInputElement;
                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.AreEqual("text", name.Type);
                Assert.AreEqual("number", number.Type);
                Assert.AreEqual("checkbox", isactive.Type);
                name.Value = "Test";
                number.Value = "1";
                isactive.IsChecked = true;
                form.Submit();
                form.PlannedNavigation.Wait();
                Assert.AreEqual("okay", html.Body.TextContent);
            }
        }

        [TestMethod]
        public void PostUrlencodeFile()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostUrlencodeFile";
                var html = DocumentBuilder.Html(new Uri(url), new Configuration { AllowRequests = true });
                Assert.AreEqual(1, html.Forms.Length);
                var form = html.Forms[0] as HTMLFormElement;
                var name = form.Elements["Name"] as HTMLInputElement;
                var number = form.Elements["Number"] as HTMLInputElement;
                var isactive = form.Elements["IsActive"] as HTMLInputElement;
                var file = form.Elements["File"] as HTMLInputElement;
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
                form.Submit();
                form.PlannedNavigation.Wait();
                Assert.AreEqual("okay", html.Body.TextContent);
            }
        }

        [TestMethod]
        public void PostMultipartNormal()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostMultipartNormal";
                var html = DocumentBuilder.Html(new Uri(url), new Configuration { AllowRequests = true });
                Assert.AreEqual(1, html.Forms.Length);
                var form = html.Forms[0] as HTMLFormElement;
                var name = form.Elements["Name"] as HTMLInputElement;
                var number = form.Elements["Number"] as HTMLInputElement;
                var isactive = form.Elements["IsActive"] as HTMLInputElement;
                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.AreEqual("text", name.Type);
                Assert.AreEqual("number", number.Type);
                Assert.AreEqual("checkbox", isactive.Type);
                name.Value = "Test";
                number.Value = "1";
                isactive.IsChecked = true;
                form.Submit();
                form.PlannedNavigation.Wait();
                Assert.AreEqual("okay", html.Body.TextContent);
            }
        }

        [TestMethod]
        public void PostMultipartFile()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostMultipartFile";
                var html = DocumentBuilder.Html(new Uri(url), new Configuration { AllowRequests = true });
                Assert.AreEqual(1, html.Forms.Length);
                var form = html.Forms[0] as HTMLFormElement;
                var name = form.Elements["Name"] as HTMLInputElement;
                var number = form.Elements["Number"] as HTMLInputElement;
                var isactive = form.Elements["IsActive"] as HTMLInputElement;
                var file = form.Elements["File"] as HTMLInputElement;
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
                form.Submit();
                form.PlannedNavigation.Wait();
                Assert.AreEqual("okay", html.Body.TextContent);
            }
        }

        [TestMethod]
        public void PostMultipartFiles()
        {
            if (Helper.IsNetworkAvailable())
            {
                var url = BaseUrl + "PostMultipartFiles";
                var html = DocumentBuilder.Html(new Uri(url), new Configuration { AllowRequests = true });
                Assert.AreEqual(1, html.Forms.Length);
                var form = html.Forms[0] as HTMLFormElement;
                var name = form.Elements["Name"] as HTMLInputElement;
                var number = form.Elements["Number"] as HTMLInputElement;
                var isactive = form.Elements["IsActive"] as HTMLInputElement;
                var files = form.Elements["Files"] as HTMLInputElement;
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

                form.Submit();
                form.PlannedNavigation.Wait();
                Assert.AreEqual("okay", html.Body.TextContent);
            }
        }
    }
}
