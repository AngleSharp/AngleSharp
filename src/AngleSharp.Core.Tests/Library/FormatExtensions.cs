using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Extensions;
using AngleSharp.Dom;
using NUnit.Framework;
using AngleSharp.Html;
using System.IO;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class FormatExtensions
    {
        static IDocument Html(String code)
        {
            var config = new Configuration().WithCss();
            return code.ToHtmlDocument(config);
        }

        [Test]
        public void ExtensionToHtml()
        {
            var document = Html("<!DOCTYPE html><html><head></head><body></body></html>");

            var html = document.ToHtml();

            Assert.AreEqual("<!DOCTYPE html><html><head></head><body></body></html>", html);
        }

        [Test]
        public void ExtensionToHtmlWithFormatter()
        {
            var document = Html("<!DOCTYPE html><html><head></head><body></body></html>");

            var html = document.ToHtml(HtmlMarkupFormatter.Instance);

            Assert.AreEqual("<!DOCTYPE html><html><head></head><body></body></html>", html);
        }

        [Test]
        public void ExtensionToHtmlWithTextWriter()
        {
            var builder = new StringBuilder();
            using (var writer = new StringWriter(builder))
            {
                var document = Html("<!DOCTYPE html><html><head></head><body></body></html>");

                document.ToHtml(writer);

                Assert.AreEqual("<!DOCTYPE html><html><head></head><body></body></html>", builder.ToString());
            }
        }
    }
}
