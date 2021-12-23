using AngleSharp.Xhtml;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class XhtmlFormatter
    {
        [Test]
        public void XhtmlMarkupFormatter_DoesNotFormatEmptyElementsToSelfClosing__WhenEmptyTagsToSelfClosingIsFalse()
        {
            var formatter = new XhtmlMarkupFormatter(false);
            var input = "<html>" +
                            "<head></head>" +
                            "<body>" +
                                "<div>test</div>" +
                                "<div></div>" +
                                "<div class=\"test\"></div>" +
                            "</body>" +
                        "</html>";
            var doc = input.ToHtmlDocument();

            var res = doc.ToHtml(formatter);

            Assert.AreEqual(input, res);
        }

        [Test]
        public void XhtmlMarkupFormatter_FormatsEmptyElementsToSelfClosing_WhenEmptyTagsToSelfClosingIsTrue()
        {
            var formatter = new XhtmlMarkupFormatter(true);
            var input = "<html>" +
                                "<head></head>" +
                                "<body>" +
                                    "<div>test</div>" +
                                    "<div></div>" +
                                    "<div class=\"test\"></div>" +
                                "</body>" +
                            "</html>";
            var expected = "<html>" +
                                   "<head />" +
                                   "<body>" +
                                       "<div>test</div>" +
                                       "<div />" +
                                       "<div class=\"test\" />" +
                                   "</body>" +
                               "</html>";
            var doc = input.ToHtmlDocument();

            var res = doc.ToHtml(formatter);

            Assert.AreEqual(expected, res);
        }
    }
}
