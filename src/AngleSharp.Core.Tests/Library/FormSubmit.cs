namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using AngleSharp.Io.Dom;
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
        private const String BaseUrl = "http://anglesharp.azurewebsites.net/";

        private static Task<IDocument> LoadDocumentAsync(String url)
        {
            var config = new Configuration().WithDefaultLoader();
            return BrowsingContext.New(config).OpenAsync(Url.Create(url));
        }

        private static Task<IDocument> PostDocumentAsync(Dictionary<String, String> fields, String encType = null)
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

        private static Task<IDocument> PostDocumentAsync(String content, String encType = null, Boolean fromButton = false)
        {
            return PostDocumentAsync((document, form) =>
            {
                if (encType != null)
                    form.Enctype = encType;

                form.InnerHtml = content;
            }, fromButton);
        }

        private static async Task<IDocument> PostDocumentAsync(Action<IDocument, IHtmlFormElement> fill, Boolean fromButton = false)
        {
            var config = new Configuration().WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenNewAsync();
            var form = document.Body.AppendElement(document.CreateElement<IHtmlFormElement>());
            form.Method = "POST";
            form.Action = BaseUrl + "echo";
            fill(document, form);

            if (fromButton)
            {
                var submitter = form.QuerySelector<IHtmlElement>("button") ??
                    form.QuerySelector<IHtmlElement>("input[type=submit]") ??
                    form.QuerySelector<IHtmlElement>("input[type=image]");
                return await form.SubmitAsync(submitter);
            }

            return await form.SubmitAsync();
        }

        private static Task<IDocument> LoadWithMockAsync(String content, String url, Action<Request> onRequest = null)
        {
            var config = Configuration.Default.With(new MockRequester { OnRequest = onRequest }).WithDefaultLoader();
            return BrowsingContext.New(config).OpenAsync(m => m.Content(content).Address(url));
        }

        private static FileEntry GenerateFile()
        {
            var content = Enumerable.Range(0, 32).Select(m => (Byte)m).ToArray();
            var body = new MemoryStream(content);
            return new FileEntry("Filename.txt", body);
        }

        private static FileEntry GenerateFile(Int32 index)
        {
            var content = Enumerable.Range(0, index * 5 + 10).Select(m => (Byte)m).ToArray();
            var body = new MemoryStream(content);
            return new FileEntry(String.Format("Filename{0}.txt", index + 1), body);
        }

        private static string Utf8StreamToString(Stream s)
        {
            byte[] data = new byte[s.Length];
            s.Read(data, 0, data.Length);
            return Encoding.UTF8.GetString(data);
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
            var result = await form.SubmitAsync();
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
            var result = await form.SubmitAsync();
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
                var response = await form.SubmitAsync();
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
                var response = await form.SubmitAsync();
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
                var response = await form.SubmitAsync();
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
                var response = await form.SubmitAsync();
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

                var response = await form.SubmitAsync();
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
                var result = await PostDocumentAsync(fields, MimeTypeNames.Plain);
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
                var result = await PostDocumentAsync(fields, MimeTypeNames.MultipartForm);
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
                var result = await PostDocumentAsync(source, encType: MimeTypeNames.MultipartForm);
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
                    form.Enctype = MimeTypeNames.MultipartForm;
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
                    form.Enctype = MimeTypeNames.MultipartForm;
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

        [Test]
        public async Task PostStandardTypeWithNamesButMissingValueShouldOmitRedundantAmpersand()
        {
            if (Helper.IsNetworkAvailable())
            {
                var content = "<input name=foo value><input name=nothing><input name=bar value>";
                var result = await PostDocumentAsync(content);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(3, rows.Length);

                Assert.AreEqual("foo", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("", rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("nothing", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual("", rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("bar", rows[2].QuerySelector("th").TextContent);
                Assert.AreEqual("", rows[2].QuerySelector("td").TextContent);

                Assert.AreEqual("\nfoo=&nothing=&bar=\n", raw);
            }
        }

        [Test]
        public async Task PostStandardTypeWithoutNamesShouldOmitRedundantAmpersand()
        {
            if (Helper.IsNetworkAvailable())
            {
                var content = "<input name=foo><input><input name=bar>";
                var result = await PostDocumentAsync(content);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(2, rows.Length);

                Assert.AreEqual("foo", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("", rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("bar", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual("", rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("\nfoo=&bar=\n", raw);
            }
        }

        [Test]
        public async Task PostStandardTypeWithoutFileShouldSkipRedundantAmpersand()
        {
            if (Helper.IsNetworkAvailable())
            {
                var content = "<input type=hidden name=status1 value=1><input type=file name=photo><input type=hidden name=status2 value=1>";
                var result = await PostDocumentAsync(content);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(2, rows.Length);

                Assert.AreEqual("status1", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("1", rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("status2", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual("1", rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("\nstatus1=1&status2=1\n", raw);
            }
        }

        [Test]
        public async Task PostStandardTypeWithImageTypeNotPressedShouldSupressEverything()
        {
            if (Helper.IsNetworkAvailable())
            {
                var content = "<input type=image name=foo value=bar>";
                var result = await PostDocumentAsync(content);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(0, rows.Length);

                Assert.AreEqual("\n\n", raw);
            }
        }

        [Test]
        public async Task PostStandardTypeWithImageTypePressedShouldShowEverything()
        {
            if (Helper.IsNetworkAvailable())
            {
                var content = "<input type=image name=foo value=bar>";
                var result = await PostDocumentAsync(content, fromButton: true);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(3, rows.Length);

                Assert.AreEqual("\nfoo.x=0&foo.y=0&foo=bar\n", raw);
            }
        }

        [Test]
        public async Task PostStandardTypeWithImageTypeWithoutValuePressedShouldShowXy()
        {
            if (Helper.IsNetworkAvailable())
            {
                var content = "<input type=image name=foo>";
                var result = await PostDocumentAsync(content, fromButton: true);
                var rows = result.QuerySelectorAll("tr");
                var raw = result.QuerySelector("#input").TextContent;

                Assert.AreEqual(2, rows.Length);

                Assert.AreEqual("\nfoo.x=0&foo.y=0\n", raw);
            }
        }

        [Test]
        public async Task AsJsonExample1BasicKeys()
        {
            var request = default(Request);
            var onRequest = new Action<Request>(r => request = r);
            var url = "http://localhost/";

            var document = await LoadWithMockAsync(@"<form enctype='application/json' method='post'>
  <input name='name' value='Bender'>
  <select name='hind'>
    <option selected>Bitable</option>
    <option>Kickable</option>
  </select>
  <input type='checkbox' name='shiny' checked>
</form>", url, onRequest);

            var form = document.Forms[0] as HtmlFormElement;
            await form.SubmitAsync();
            Assert.IsNotNull(request);
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual("{\"name\":\"Bender\",\"hind\":\"Bitable\",\"shiny\":true}", Utf8StreamToString(request.Content));
        }

        [Test]
        public async Task AsJsonExample2MultipleValues()
        {
            var request = default(Request);
            var onRequest = new Action<Request>(r => request = r);
            var url = "http://localhost/";

            var document = await LoadWithMockAsync(@"<form enctype='application/json' method='post'>
  <input type='number' name='bottle-on-wall' value='1'>
  <input type='number' name='bottle-on-wall' value='2'>
  <input type='number' name='bottle-on-wall' value='3'>
</form>", url, onRequest);

            var form = document.Forms[0] as HtmlFormElement;
            await form.SubmitAsync();
            Assert.IsNotNull(request);
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual("{\"bottle-on-wall\":[1,2,3]}", Utf8StreamToString(request.Content));
        }

        [Test]
        public async Task AsJsonExample3DeeperStructure()
        {
            var request = default(Request);
            var onRequest = new Action<Request>(r => request = r);
            var url = "http://localhost/";

            var document = await LoadWithMockAsync(@"<form enctype='application/json' method='post'>
  <input name='pet[species]' value='Dahut'>
  <input name='pet[name]' value='Hypatia'>
  <input name='kids[1]' value='Thelma'>
  <input name='kids[0]' value='Ashley'>
</form>", url, onRequest);

            var form = document.Forms[0] as HtmlFormElement;
            await form.SubmitAsync();
            Assert.IsNotNull(request);
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual("{\"pet\":{\"species\":\"Dahut\",\"name\":\"Hypatia\"},\"kids\":[\"Ashley\",\"Thelma\"]}", Utf8StreamToString(request.Content));
        }

        [Test]
        public async Task AsJsonExample4SparseArrays()
        {
            var request = default(Request);
            var onRequest = new Action<Request>(r => request = r);
            var url = "http://localhost/";

            var document = await LoadWithMockAsync(@"<form enctype='application/json' method='post'>
  <input name='hearbeat[0]' value='thunk'>
  <input name='hearbeat[2]' value='thunk'>
</form>", url, onRequest);

            var form = document.Forms[0] as HtmlFormElement;
            await form.SubmitAsync();
            Assert.IsNotNull(request);
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual("{\"hearbeat\":[\"thunk\",null,\"thunk\"]}", Utf8StreamToString(request.Content));
        }

        [Test]
        public async Task AsJsonExample5EvenDeeper()
        {
            var request = default(Request);
            var onRequest = new Action<Request>(r => request = r);
            var url = "http://localhost/";

            var document = await LoadWithMockAsync(@"<form enctype='application/json' method='post'>
  <input name='pet[0][species]' value='Dahut'>
  <input name='pet[0][name]' value='Hypatia'>
  <input name='pet[1][species]' value='Felis Stultus'>
  <input name='pet[1][name]' value='Billie'>
</form>", url, onRequest);

            var form = document.Forms[0] as HtmlFormElement;
            await form.SubmitAsync();
            Assert.IsNotNull(request);
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual("{\"pet\":[{\"species\":\"Dahut\",\"name\":\"Hypatia\"},{\"species\":\"Felis Stultus\",\"name\":\"Billie\"}]}", Utf8StreamToString(request.Content));
        }

        [Test]
        public async Task AsJsonExample6SuchDeep()
        {
            var request = default(Request);
            var onRequest = new Action<Request>(r => request = r);
            var url = "http://localhost/";

            var document = await LoadWithMockAsync(@"<form enctype='application/json' method='post'>
  <input name='wow[such][deep][3][much][power][!]' value='Amaze'>
</form>", url, onRequest);

            var form = document.Forms[0] as HtmlFormElement;
            await form.SubmitAsync();
            Assert.IsNotNull(request);
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual("{\"wow\":{\"such\":{\"deep\":[null,null,null,{\"much\":{\"power\":{\"!\":\"Amaze\"}}}]}}}", Utf8StreamToString(request.Content));
        }

        [Test]
        public async Task AsJsonExample7MergeBehaviour()
        {
            var request = default(Request);
            var onRequest = new Action<Request>(r => request = r);
            var url = "http://localhost/";

            var document = await LoadWithMockAsync(@"<form enctype='application/json' method='post'>
  <input name='mix' value='scalar'>
  <input name='mix[0]' value='array 1'>
  <input name='mix[2]' value='array 2'>
  <input name='mix[key]' value='key key'>
  <input name='mix[car]' value='car key'>
</form>", url, onRequest);

            var form = document.Forms[0] as HtmlFormElement;
            await form.SubmitAsync();
            Assert.IsNotNull(request);
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual("{\"mix\":{\"\":\"scalar\",\"0\":\"array 1\",\"2\":\"array 2\",\"key\":\"key key\",\"car\":\"car key\"}}", Utf8StreamToString(request.Content));
        }

        [Test]
        public async Task AsJsonExample8Append()
        {
            var request = default(Request);
            var onRequest = new Action<Request>(r => request = r);
            var url = "http://localhost/";

            var document = await LoadWithMockAsync(@"<form enctype='application/json' method='post'>
  <input name='highlander[]' value='one'>
</form>", url, onRequest);

            var form = document.Forms[0] as HtmlFormElement;
            await form.SubmitAsync();
            Assert.IsNotNull(request);
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual("{\"highlander\":[\"one\"]}", Utf8StreamToString(request.Content));
        }

        [Test]
        public async Task AsJsonExample9Files()
        {
            var request = default(Request);
            var onRequest = new Action<Request>(r => request = r);
            var url = "http://localhost/";

            var document = await LoadWithMockAsync(@"<form enctype='application/json' method='post'>
  <input type='file' name='file' multiple>
</form>", url, onRequest);

            var form = document.Forms[0] as HtmlFormElement;
            var file = form.Elements["file"] as HtmlInputElement;
            Assert.IsNotNull(file);
            Assert.AreEqual("file", file.Type);

            var files = file.Files as FileList;
            files.Add(new FileEntry("dahut.txt", new MemoryStream(Convert.FromBase64String("REFBQUFBQUFIVVVVVVVVVVVVVCEhIQo="))));
            files.Add(new FileEntry("litany.txt", new MemoryStream(Convert.FromBase64String("SSBtdXN0IG5vdCBmZWFyLlxuRmVhciBpcyB0aGUgbWluZC1raWxsZXIuCg=="))));

            await form.SubmitAsync();
            Assert.IsNotNull(request);
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual("{\"file\":[{\"type\":\"text/plain\",\"name\":\"dahut.txt\",\"body\":\"REFBQUFBQUFIVVVVVVVVVVVVVCEhIQo=\"},{\"type\":\"text/plain\",\"name\":\"litany.txt\",\"body\":\"SSBtdXN0IG5vdCBmZWFyLlxuRmVhciBpcyB0aGUgbWluZC1raWxsZXIuCg==\"}]}", Utf8StreamToString(request.Content));
        }

        [Test]
        public async Task AsJsonExample10BadInput()
        {
            var request = default(Request);
            var onRequest = new Action<Request>(r => request = r);
            var url = "http://localhost/";

            var document = await LoadWithMockAsync(@"<form enctype='application/json' method='post'>
  <input name='error[good]' value='BOOM!'>
  <input name='error[bad' value='BOOM BOOM!'>
</form>", url, onRequest);

            var form = document.Forms[0] as HtmlFormElement;
            await form.SubmitAsync();
            Assert.IsNotNull(request);
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual("{\"error\":{\"good\":\"BOOM!\"},\"error[bad\":\"BOOM BOOM!\"}", Utf8StreamToString(request.Content));
        }

        [Test]
        public async Task PostStandardTypeFromButtonViaExtensionMethodWithoutFields()
        {
            if (Helper.IsNetworkAvailable())
            {
                var target = BaseUrl + "echo";
                var source = "<form method=POST action='" + target + "'><input type=text name=username value='foo'><input type=password name=password value='bar'><button type=submit name=login value=Login></form>";
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(res => res.Content(source).Address(target));
                var result = await document.QuerySelector<IHtmlButtonElement>("button").SubmitAsync();

                var rows = result.QuerySelectorAll("tr");
                Assert.AreEqual(3, rows.Length);

                Assert.AreEqual("username", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("foo", rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("password", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual("bar", rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("login", rows[2].QuerySelector("th").TextContent);
                Assert.AreEqual("Login", rows[2].QuerySelector("td").TextContent);
            }
        }

        [Test]
        public async Task PostStandardTypeFromButtonViaExtensionMethodWithFields()
        {
            if (Helper.IsNetworkAvailable())
            {
                var target = BaseUrl + "echo";
                var source = "<form method=POST action='" + target + "'><input type=text name=username value='foo'><input type=password name=password value='bar'><button type=submit name=login value=Login></form>";
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(res => res.Content(source).Address(target));
                var result = await document.QuerySelector<IHtmlButtonElement>("button").SubmitAsync(new
                {
                    username = "Test",
                    password = "Baz"
                });

                var rows = result.QuerySelectorAll("tr");
                Assert.AreEqual(3, rows.Length);

                Assert.AreEqual("username", rows[0].QuerySelector("th").TextContent);
                Assert.AreEqual("Test", rows[0].QuerySelector("td").TextContent);

                Assert.AreEqual("password", rows[1].QuerySelector("th").TextContent);
                Assert.AreEqual("Baz", rows[1].QuerySelector("td").TextContent);

                Assert.AreEqual("login", rows[2].QuerySelector("th").TextContent);
                Assert.AreEqual("Login", rows[2].QuerySelector("td").TextContent);
            }
        }
    }
}
