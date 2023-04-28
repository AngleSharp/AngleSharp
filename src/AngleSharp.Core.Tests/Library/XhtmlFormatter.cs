using AngleSharp.Xhtml;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Library
{
    using System;
    using Dom;

    [TestFixture]
    public class XhtmlFormatter
    {
        [Test]
        public void XhtmlMarkupFormatter_DoesNotFormatEmptyElementsToSelfClosing_WhenEmptyTagsToSelfClosingIsFalse()
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

        [Test]
        public void XhtmlMarkupFormatter_KeepsEntireNameOfXmlNamespacedAttributes()
        {
            var formatter = new XhtmlMarkupFormatter();
            var attributeName = String.Concat(NamespaceNames.XmlNsPrefix, ":", "pfx");
            var inputOnWhichToSetAttribute = "<html>" +
                                "<head></head>" +
                                 "<body>" +
                                    "<div>test</div>" +
                                "</body>" +
                            "</html>";
            var expected = "<html>" +
                                    "<head />" +
                                    "<body>" +
                                       "<div xmlns:pfx=\"http://www.foo.com\">test</div>" +
                                    "</body>" +
                                "</html>";
            var doc = inputOnWhichToSetAttribute.ToHtmlDocument();
            doc.Body.FirstElementChild.SetAttribute(NamespaceNames.XmlNsUri, attributeName, "http://www.foo.com");
            var res = doc.ToHtml(formatter);
            Assert.AreEqual(expected, res);
        }


        [Test]
        public void XhtmlMarkupFormatter_DoesNotDuplicatePrefixIfUnnecessary()
        {
            var formatter = new XhtmlMarkupFormatter();
            var attributeName = NamespaceNames.XmlNsPrefix;
            var inputOnWhichToSetAttribute = "<html>" +
                        "<head></head>" +
                        "<body>" +
                        "<div>test</div>" +
                        "</body>" +
                        "</html>";
            var expected = "<html>" +
                           "<head />" +
                           "<body>" +
                           "<div xmlns=\"http://www.foo.com\">test</div>" +
                           "</body>" +
                           "</html>";
            var doc = inputOnWhichToSetAttribute.ToHtmlDocument();

            doc.Body.FirstElementChild.SetAttribute(NamespaceNames.XmlNsUri, attributeName, "http://www.foo.com");
            var res = doc.ToHtml(formatter);
            Assert.AreEqual(expected, res);
        }
    }
}
