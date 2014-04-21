using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;
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
                var form = html.Forms[0];
                var name = form.Elements["Name"] as HTMLInputElement;
                var number = form.Elements["Number"] as HTMLInputElement;
                var isactive = form.Elements["IsActive"] as HTMLInputElement;
                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.AreEqual(HTMLInputElement.InputType.Text, name.Type);
                Assert.AreEqual(HTMLInputElement.InputType.Number, number.Type);
                Assert.AreEqual(HTMLInputElement.InputType.Checkbox, isactive.Type);
                name.Value = "Test";
                number.Value = "1";
                isactive.Checked = true;
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
                var form = html.Forms[0];
                var name = form.Elements["Name"] as HTMLInputElement;
                var number = form.Elements["Number"] as HTMLInputElement;
                var isactive = form.Elements["IsActive"] as HTMLInputElement;
                var file = form.Elements["File"] as HTMLInputElement;
                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.IsNotNull(file);
                Assert.AreEqual(HTMLInputElement.InputType.Text, name.Type);
                Assert.AreEqual(HTMLInputElement.InputType.Number, number.Type);
                Assert.AreEqual(HTMLInputElement.InputType.Checkbox, isactive.Type);
                Assert.AreEqual(HTMLInputElement.InputType.File, file.Type);
                name.Value = "Test";
                number.Value = "1";
                isactive.Checked = true;
                file.Files.Add(GenerateFile());
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
                var form = html.Forms[0];
                var name = form.Elements["Name"] as HTMLInputElement;
                var number = form.Elements["Number"] as HTMLInputElement;
                var isactive = form.Elements["IsActive"] as HTMLInputElement;
                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.AreEqual(HTMLInputElement.InputType.Text, name.Type);
                Assert.AreEqual(HTMLInputElement.InputType.Number, number.Type);
                Assert.AreEqual(HTMLInputElement.InputType.Checkbox, isactive.Type);
                name.Value = "Test";
                number.Value = "1";
                isactive.Checked = true;
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
                var form = html.Forms[0];
                var name = form.Elements["Name"] as HTMLInputElement;
                var number = form.Elements["Number"] as HTMLInputElement;
                var isactive = form.Elements["IsActive"] as HTMLInputElement;
                var file = form.Elements["File"] as HTMLInputElement;
                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.IsNotNull(file);
                Assert.AreEqual(HTMLInputElement.InputType.Text, name.Type);
                Assert.AreEqual(HTMLInputElement.InputType.Number, number.Type);
                Assert.AreEqual(HTMLInputElement.InputType.Checkbox, isactive.Type);
                Assert.AreEqual(HTMLInputElement.InputType.File, file.Type);
                name.Value = "Test";
                number.Value = "1";
                isactive.Checked = true;
                file.Files.Add(GenerateFile());
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
                var form = html.Forms[0];
                var name = form.Elements["Name"] as HTMLInputElement;
                var number = form.Elements["Number"] as HTMLInputElement;
                var isactive = form.Elements["IsActive"] as HTMLInputElement;
                var files = form.Elements["Files"] as HTMLInputElement;
                Assert.IsNotNull(name);
                Assert.IsNotNull(number);
                Assert.IsNotNull(isactive);
                Assert.IsNotNull(files);
                Assert.AreEqual(HTMLInputElement.InputType.Text, name.Type);
                Assert.AreEqual(HTMLInputElement.InputType.Number, number.Type);
                Assert.AreEqual(HTMLInputElement.InputType.Checkbox, isactive.Type);
                Assert.AreEqual(HTMLInputElement.InputType.File, files.Type);
                Assert.IsTrue(files.Multiple);
                name.Value = "Test";
                number.Value = "1";
                isactive.Checked = true;

                for (int i = 0; i < 5; i++)
                    files.Files.Add(GenerateFile(i));

                form.Submit();
                form.PlannedNavigation.Wait();
                Assert.AreEqual("okay", html.Body.TextContent);
            }
        }
    }
}
