namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Dom.Io;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class FormSubmitTests
    {
        const String BaseUrl = "http://anglesharp.azurewebsites.net/";

        static Task<IDocument> LoadDocumentAsync(String url)
        {
            var config = new Configuration().WithDefaultLoader();
            return BrowsingContext.New(config).OpenAsync(Url.Create(url));
        }

        static Task<IDocument> PostDocumentAsync(Dictionary<String, String> fields, String encType = null)
        {
            return PostDocumentAsync((document, form) =>
            {
                if (encType != null)
                    form.Enctype = encType;

                foreach (var field in fields)
                {
                    var input = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    input.Name = field.Key;
                    input.Value = field.Value;
                }
            });
        }

        static Task<IDocument> PostDocumentAsync(String content, String encType = null, Boolean fromButton = false)
        {
            return PostDocumentAsync((document, form) =>
            {
                if (encType != null)
                    form.Enctype = encType;

                form.InnerHtml = content;
            }, fromButton);
        }

        static async Task<IDocument> PostDocumentAsync(Action<IDocument, IHtmlFormElement> fill, Boolean fromButton = false)
        {
            var config = new Configuration().WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenNewAsync();
            var form = document.Body.AppendElement(document.CreateElement<IHtmlFormElement>());
            form.Method = "POST";
            form.Action = BaseUrl + "echo";
            fill(document, form);

            if (fromButton)
                return await form.Submit(form.QuerySelector<IHtmlElement>("button") ?? form.QuerySelector<IHtmlElement>("input[type=submit]"));

            return await form.Submit();
        }

        static Task<IDocument> LoadWithMockAsync(String content, String url)
        {
            var config = Configuration.Default.WithDefaultLoader(requesters: new[] { new MockRequester() });
            return BrowsingContext.New(config).OpenAsync(m => m.Content(content).Address(url));
        }

        static FileEntry GenerateFile()
        {
            var content = Enumerable.Range(0, 32).Select(m => (Byte)m).ToArray();
            var body = new MemoryStream(content);
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
            var document = await LoadWithMockAsync(@"<form method=get>
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
            var document = await LoadWithMockAsync(@"
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
                var document = await LoadDocumentAsync(url);
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
                var document = await LoadDocumentAsync(url);
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
                var document = await LoadDocumentAsync(url);
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
                var document = await LoadDocumentAsync(url);
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
                var document = await LoadDocumentAsync(url);
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

        [Test]
        public async Task PostStandardTypeShouldEchoAllValuesCorrectly()
        {
            if (Helper.IsNetworkAvailable())
            {
                var fields = new Dictionary<String, String>
                {
                    { "myname", "foo" },
                    { "bar", "this is some longer text" },
                    { "yeti", "0" },
                };
                var result = await PostDocumentAsync(fields);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(3, rows.Length);

                Assert.AreEqual("myname", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual(fields["myname"], rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("bar", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual(fields["bar"], rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("yeti", rows[2].QuerySelector("th").TextContent);
                Assert.AreEqual(fields["yeti"], rows[2].QuerySelector("td").TextContent);

                Assert.AreEqual("\nmyname=foo&bar=this+is+some+longer+text&yeti=0\n", raw);
            }
        }

        [Test]
        public async Task PostTextPlainShouldEchoAllValuesCorrectly()
        {
            if (Helper.IsNetworkAvailable())
            {
                var fields = new Dictionary<String, String>
                {
                    { "myname", "foo" },
                    { "bar", "this is some longer text" },
                    { "yeti", "0" },
                };
                var result = await PostDocumentAsync(fields, MimeTypes.Plain);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(0, rows.Length);
                Assert.AreEqual("\nmyname=foo\nbar=this is some longer text\nyeti=0\n", raw);
            }
        }

        [Test]
        public async Task PostMulipartFormdataShouldEchoAllValuesCorrectly()
        {
            if (Helper.IsNetworkAvailable())
            {
                var fields = new Dictionary<String, String>
                {
                    { "myname", "foo" },
                    { "bar", "this is some longer text" },
                    { "yeti", "0" },
                };
                var result = await PostDocumentAsync(fields, MimeTypes.MultipartForm);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(3, rows.Length);

                Assert.AreEqual("myname", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual(fields["myname"], rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("bar", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual(fields["bar"], rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("yeti", rows[2].QuerySelector("th").TextContent);
                Assert.AreEqual(fields["yeti"], rows[2].QuerySelector("td").TextContent);

                var lines = raw.Split('\n');

                Assert.AreEqual(15, lines.Length);

                var emptyLines = new[] { 0, 3, 7, 11, 14 };
                var sameLines = new[] { 1, 5, 9 };
                var nameLines = new[] { 2, 6, 10 };
                var valueLines = new[] { 4, 8, 12 };

                foreach (var emptyLine in emptyLines)
                    Assert.AreEqual(String.Empty, lines[emptyLine]);

                for (int i = 1; i < sameLines.Length; i++)
                    Assert.AreEqual(lines[sameLines[0]], lines[sameLines[i]]);

                Assert.AreEqual(lines[sameLines[0]] + "--", lines[lines.Length - 2]);

                for (int i = 0; i < nameLines.Length; i++)
                {
                    var field = fields.Skip(i).First();
                    Assert.AreEqual("Content-Disposition: form-data; name=\"" + field.Key + "\"", lines[nameLines[i]]);
                    Assert.AreEqual(field.Value, lines[valueLines[i]]);
                }
            }
        }

        [Test]
        public async Task PostStandardTypeWithModifiedButtonValueShouldNotEchoTheButtonValue()
        {
            if (Helper.IsNetworkAvailable())
            {
                var result = await PostDocumentAsync((document, form) =>
                {
                    var user = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    var pass = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    var btn = form.AppendElement(document.CreateElement<IHtmlButtonElement>());
                    user.Type = "text";
                    user.Name = "username";
                    user.Value = "foo";
                    pass.Type = "password";
                    pass.Name = "password";
                    pass.Value = "bar";
                    btn.Name = "login";
                    btn.Value = "Login";
                }, fromButton: false);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(2, rows.Length);

                Assert.AreEqual("username", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("foo", rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("password", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual("bar", rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("\nusername=foo&password=bar\n", raw);
            }
        }

        [Test]
        public async Task PostStandardTypeWithInitialButtonValueShouldNotEchoTheButtonValue()
        {
            if (Helper.IsNetworkAvailable())
            {
                var source = "<input type=text name=username value='foo'><input type=password name=password value='bar'><button type=submit name=login value=Login>";
                var result = await PostDocumentAsync(source, fromButton: false);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(2, rows.Length);

                Assert.AreEqual("username", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("foo", rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("password", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual("bar", rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("\nusername=foo&password=bar\n", raw);
            }
        }

        [Test]
        public async Task PostStandardTypeFromButtonWithModifiedValueShouldEchoTheButtonValue()
        {
            if (Helper.IsNetworkAvailable())
            {
                var result = await PostDocumentAsync((document, form) =>
                {
                    var user = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    var pass = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    var btn = form.AppendElement(document.CreateElement<IHtmlButtonElement>());
                    user.Type = "text";
                    user.Name = "username";
                    user.Value = "foo";
                    pass.Type = "password";
                    pass.Name = "password";
                    pass.Value = "bar";
                    btn.Name = "login";
                    btn.Value = "Login";
                }, fromButton: true);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(3, rows.Length);

                Assert.AreEqual("username", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("foo", rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("password", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual("bar", rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("login", rows[2].QuerySelector("th").TextContent);
                Assert.AreEqual("Login", rows[2].QuerySelector("td").TextContent);

                Assert.AreEqual("\nusername=foo&password=bar&login=Login\n", raw);
            }
        }

        [Test]
        public async Task PostStandardTypeFromButtonWithInitialValueShouldEchoTheButtonValue()
        {
            if (Helper.IsNetworkAvailable())
            {
                var source = "<input type=text name=username value='foo'><input type=password name=password value='bar'><button type=submit name=login value=Login>";
                var result = await PostDocumentAsync(source, fromButton: true);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(3, rows.Length);

                Assert.AreEqual("username", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("foo", rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("password", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual("bar", rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("login", rows[2].QuerySelector("th").TextContent);
                Assert.AreEqual("Login", rows[2].QuerySelector("td").TextContent);

                Assert.AreEqual("\nusername=foo&password=bar&login=Login\n", raw);
            }
        }

        [Test]
        public async Task PostFormWithCheckboxArrayAndDefaultRadioValueShouldYieldStandardValues()
        {
            if (Helper.IsNetworkAvailable())
            {
                var source = @"<input name=name size=15 type=text value=abc /><TEXTAREA NAME=Address ROWS=3 COLS=30 >
</TEXTAREA><select multiple=multiple name=colors>
<option> RED </option>
<option selected> GREEN </option>
<option> YELLOW </option>
<option> BLUE </option>
<option> ORANGE </option>
</select><input name=""color[]"" type=checkbox value=green checked /> green
<input name=""color[]"" type=checkbox value=red checked /> red
<input name=""color[]"" type=checkbox value=blue checked /> blue<input checked=checked name=answer type=radio /> True
<input name=answer type=radio value=off /> False";
                var result = await PostDocumentAsync(source);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(5, rows.Length);

                Assert.AreEqual("name", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("abc", rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("Address", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual("", rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("colors", rows[2].QuerySelector("th").TextContent);
                Assert.AreEqual("GREEN", rows[2].QuerySelector("td").TextContent);

                Assert.AreEqual("color[]", rows[3].QuerySelector("th").TextContent);
                Assert.AreEqual("green,red,blue", rows[3].QuerySelector("td").TextContent);

                Assert.AreEqual("answer", rows[4].QuerySelector("th").TextContent);
                Assert.AreEqual("on", rows[4].QuerySelector("td").TextContent);
            }
        }

        [Test]
        public async Task PostFormWithEmptyRadioElementValueShouldYieldEmptyValue()
        {
            if (Helper.IsNetworkAvailable())
            {
                var source = @"<input name=answer type=radio /> On
<input checked=checked name=answer type=radio value='' /> Nothing
<input name=answer type=radio value=false /> False";
                var result = await PostDocumentAsync(source);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(1, rows.Length);

                Assert.AreEqual("answer", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("", rows[0].QuerySelector("td").TextContent);
            }
        }

        [Test]
        public async Task PostFormWithEmptyCheckboxElementValueShouldYieldEmptyValue()
        {
            if (Helper.IsNetworkAvailable())
            {
                var source = @"<input checked name=answer type=checkbox /> On
<input checked name=answer type=checkbox value='' /> Nothing
<input name=answer type=checkbox value=false /> False";
                var result = await PostDocumentAsync(source);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(1, rows.Length);

                Assert.AreEqual("answer", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("on,", rows[0].QuerySelector("td").TextContent);
            }
        }

        [Test]
        public async Task PostFormWithNoFileShouldSendInputEmptyFileName()
        {
            if (Helper.IsNetworkAvailable())
            {
                var source = @"<input type=file name=image>";
                var result = await PostDocumentAsync(source, encType: MimeTypes.MultipartForm);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(0, rows.Length);

                var lines = raw.Split('\n');

                Assert.AreEqual(8, lines.Length);

                var emptyLines = new[] { 0, 4, 5, 7 };

                foreach (var emptyLine in emptyLines)
                    Assert.AreEqual(String.Empty, lines[emptyLine]);

                Assert.AreEqual(lines[1] + "--", lines[lines.Length - 2]);
                Assert.AreEqual("Content-Disposition: form-data; name=\"image\"; filename=\"\"", lines[2]);
                Assert.AreEqual("Content-Type: application/octet-stream", lines[3]);
            }
        }

        [Test]
        public async Task PostFormWithSimpleFileShouldSendFileContent()
        {
            if (Helper.IsNetworkAvailable())
            {
                var content = Encoding.UTF8.GetBytes("test");
                var result = await PostDocumentAsync((document, form) =>
                {
                    var input = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    form.Enctype = MimeTypes.MultipartForm;
                    input.Name = "image";
                    input.Type = "file";
                    input.Files.Add(new FileEntry("test.txt", new MemoryStream(content)));
                });
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(0, rows.Length);

                var lines = raw.Split('\n');

                Assert.AreEqual(8, lines.Length);

                var emptyLines = new[] { 0, 4, 7 };

                foreach (var emptyLine in emptyLines)
                    Assert.AreEqual(String.Empty, lines[emptyLine]);

                Assert.AreEqual(lines[1] + "--", lines[lines.Length - 2]);
                Assert.AreEqual("Content-Disposition: form-data; name=\"image\"; filename=\"test.txt\"", lines[2]);
                Assert.AreEqual("Content-Type: text/plain", lines[3]);
                Assert.AreEqual("test", lines[5]);
            }
        }

        [Test]
        public async Task PostFormWithFileFieldWithoutNameShouldNotSendAnything()
        {
            if (Helper.IsNetworkAvailable())
            {
                var content = Encoding.UTF8.GetBytes("test");
                var result = await PostDocumentAsync((document, form) =>
                {
                    var input = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    form.Enctype = MimeTypes.MultipartForm;
                    input.Type = "file";
                    input.Files.Add(new FileEntry("test.txt", new MemoryStream(content)));
                });
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(0, rows.Length);

                var lines = raw.Split('\n');

                Assert.AreEqual(3, lines.Length);

                var emptyLines = new[] { 0, 2 };

                foreach (var emptyLine in emptyLines)
                    Assert.AreEqual(String.Empty, lines[emptyLine]);
            }
        }

        [Test]
        public async Task PostStandardTypeWithInitialInputSubmitShouldNotEchoTheInputValue()
        {
            if (Helper.IsNetworkAvailable())
            {
                var source = "<input type=text name=username value='foo'><input type=password name=password value='bar'><input type=submit name=login value=Login>";
                var result = await PostDocumentAsync(source, fromButton: false);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(2, rows.Length);

                Assert.AreEqual("username", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("foo", rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("password", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual("bar", rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("\nusername=foo&password=bar\n", raw);
            }
        }

        [Test]
        public async Task PostStandardTypeFromInputSubmitWithModifiedValueShouldEchoTheInputValue()
        {
            if (Helper.IsNetworkAvailable())
            {
                var result = await PostDocumentAsync((document, form) =>
                {
                    var user = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    var pass = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    var btn = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    user.Type = "text";
                    user.Name = "username";
                    user.Value = "foo";
                    pass.Type = "password";
                    pass.Name = "password";
                    pass.Value = "bar";
                    btn.Name = "login";
                    btn.Type = "submit";
                    btn.Value = "Login";
                }, fromButton: true);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(3, rows.Length);

                Assert.AreEqual("username", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("foo", rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("password", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual("bar", rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("login", rows[2].QuerySelector("th").TextContent);
                Assert.AreEqual("Login", rows[2].QuerySelector("td").TextContent);

                Assert.AreEqual("\nusername=foo&password=bar&login=Login\n", raw);
            }
        }

        [Test]
        public async Task PostStandardTypeWithAttributeEmptyCheckboxValueShouldSendEmptyValue()
        {
            if (Helper.IsNetworkAvailable())
            {
                var result = await PostDocumentAsync((document, form) =>
                {
                    var check = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    check.Type = "checkbox";
                    check.Name = "test";
                    check.SetAttribute("checked", "");
                    check.SetAttribute("value", "");
                });
                var rows = result.QuerySelectorAll("tr");

                Assert.AreEqual(1, rows.Length);

                Assert.AreEqual("test", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("", rows[0].QuerySelector("td").TextContent);
            }
        }

        [Test]
        public async Task PostStandardTypeWithSetNonEmptyCheckboxValueShouldSendNonEmptyValue()
        {
            if (Helper.IsNetworkAvailable())
            {
                var result = await PostDocumentAsync((document, form) =>
                {
                    var check = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    check.Type = "checkbox";
                    check.Name = "test";
                    check.SetAttribute("checked", "");
                    check.Value = "foo";
                });
                var rows = result.QuerySelectorAll("tr");

                Assert.AreEqual(1, rows.Length);

                Assert.AreEqual("test", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("foo", rows[0].QuerySelector("td").TextContent);
            }
        }

        [Test]
        public async Task PostStandardTypeWithSetEmptyCheckboxValueShouldSendEmptyValue()
        {
            if (Helper.IsNetworkAvailable())
            {
                var result = await PostDocumentAsync((document, form) =>
                {
                    var check = form.AppendElement(document.CreateElement<IHtmlInputElement>());
                    check.Type = "checkbox";
                    check.Name = "test";
                    check.IsChecked = true;
                    check.SetAttribute("value", "foo");
                    check.Value = "";
                });
                var rows = result.QuerySelectorAll("tr");

                Assert.AreEqual(1, rows.Length);

                Assert.AreEqual("test", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("", rows[0].QuerySelector("td").TextContent);
            }
        }
    }
}
