namespace AngleSharp.Core.Tests.Library
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using AngleSharp.Io;
    using NUnit.Framework;

    [TestFixture]
    public class FormSubmission
    {
        [Test]
        public async Task GetSubmissionOfSimpleForm()
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var content = "<form><input name=a value=foo><input name=b value=bar></form>";
            var document = await context.OpenAsync(req => req.Content(content).Address("http://example.com"));
            var form = document.Forms[0];
            var documentRequest = form.GetSubmission();

            Assert.AreEqual(HttpMethod.Get, documentRequest.Method);
            Assert.AreEqual("?a=foo&b=bar", documentRequest.Target.Href.Substring(document.Url.Length));
            Assert.AreEqual("", GetText(documentRequest.Body));
        }

        [Test]
        public async Task GetSubmissionOfPostForm()
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var content = "<form method=post><input name=a value=foo><input name=b value=bar></form>";
            var document = await context.OpenAsync(req => req.Content(content).Address("http://example.com"));
            var form = document.Forms[0];
            var documentRequest = form.GetSubmission();

            Assert.AreEqual(HttpMethod.Post, documentRequest.Method);
            Assert.AreEqual("", documentRequest.Target.Href.Substring(document.Url.Length));
            Assert.AreEqual("a=foo&b=bar", GetText(documentRequest.Body));
        }

        private static string GetText(Stream body)
        {
            using var ms = new MemoryStream();
            body.CopyTo(ms);
            var content = ms.ToArray();
            return Encoding.UTF8.GetString(content);
        }

        [Test]
        public async Task SelectBasic()
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var content = @"<form method=post>
  <select name=sex>
    <option value=>--</option>
    <option value=male selected>Man</option>
    <option value=female>Woman</option>
  </select>
</form>";
            var document = await context.OpenAsync(req => req.Content(content).Address("http://example.com"));
            var form = document.Forms[0];
            var documentRequest = form.GetSubmission();

            Assert.AreEqual(HttpMethod.Post, documentRequest.Method);
            Assert.AreEqual("", documentRequest.Target.Href.Substring(document.Url.Length));
            Assert.AreEqual("sex=male", GetText(documentRequest.Body));
        }

        [Test]
        public async Task SelectDefaultOptionIfNoOptionsSelected()
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var content = @"<form method=post>
  <select name=sex>
    <option value=>--</option>
    <option value=male>Man</option>
    <option value=female>Woman</option>
  </select>
</form>";
            var document = await context.OpenAsync(req => req.Content(content).Address("http://example.com"));
            var form = document.Forms[0];
            var documentRequest = form.GetSubmission();

            Assert.AreEqual(HttpMethod.Post, documentRequest.Method);
            Assert.AreEqual("", documentRequest.Target.Href.Substring(document.Url.Length));
            Assert.AreEqual("sex=", GetText(documentRequest.Body));
        }

        [Test]
        public async Task RadioMultipleSameNameEntries()
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var content = @"<form method=post>
  <input type=hidden name=color value=red3>
  <label><input type=radio name=color value=blue>Blue</label>
  <label><input type=radio name=color value=red checked>Red</label>
  <label><input type=radio name=color value=red2 checked>Red</label>
  <label><input type=radio name=color value=yellow>Yellow</label>
  <input type=hidden name=color value=red4>
</form>";
            var document = await context.OpenAsync(req => req.Content(content).Address("http://example.com"));
            var form = document.Forms[0];
            var documentRequest = form.GetSubmission();

            Assert.AreEqual(HttpMethod.Post, documentRequest.Method);
            Assert.AreEqual("", documentRequest.Target.Href.Substring(document.Url.Length));
            Assert.AreEqual("color=red3&color=red2&color=red4", GetText(documentRequest.Body));
        }
    }
}
