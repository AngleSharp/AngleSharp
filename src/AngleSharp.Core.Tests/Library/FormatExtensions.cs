namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Html;
    using NUnit.Framework;
    using System.IO;
    using System.Text;

    [TestFixture]
    public class FormatExtensions
    {
        [Test]
        public void ExtensionToHtml()
        {
            var document = ("<!DOCTYPE html><html><head></head><body></body></html>").ToHtmlDocument();
            var html = document.ToHtml();
            Assert.AreEqual("<!DOCTYPE html><html><head></head><body></body></html>", html);
        }

        [Test]
        public void ExtensionToHtmlWithFormatter()
        {
            var document = ("<!DOCTYPE html><html><head></head><body></body></html>").ToHtmlDocument();
            var html = document.ToHtml(HtmlMarkupFormatter.Instance);
            Assert.AreEqual("<!DOCTYPE html><html><head></head><body></body></html>", html);
        }

        [Test]
        public void ExtensionToHtmlWithTextWriter()
        {
            var builder = new StringBuilder();

            using (var writer = new StringWriter(builder))
            {
                var document = ("<!DOCTYPE html><html><head></head><body></body></html>").ToHtmlDocument();
                document.ToHtml(writer);
                Assert.AreEqual("<!DOCTYPE html><html><head></head><body></body></html>", builder.ToString());
            }
        }
    }
}
