namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.IO;
    using System.Text;

    [TestFixture]
    public class HtmlFragmentDocumentModeTests
    {
        [TestCase("", QuirksMode.On, false)]
        [TestCase("", QuirksMode.On, true)]
        [TestCase("<!doctype html>", QuirksMode.Off, false)]
        [TestCase("<!doctype html>", QuirksMode.Off, true)]
        [TestCase("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">", QuirksMode.Limited, false)]
        [TestCase("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">", QuirksMode.Limited, true)]
        public void FragmentRetainsContextDocumentModeAndTableParsing(String doctype, QuirksMode mode, Boolean stream)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(doctype + "<div id=context></div>");
            var context = document.QuerySelector("div");

            Assert.AreEqual(mode, ((Document)document).QuirksMode);

            var source = "<p id=TARGET class=CARD>Paragraph<table id=TABLE><tr><td>Cell</td></tr></table>";
            using var bytes = new MemoryStream(Encoding.UTF8.GetBytes(source));
            var fragment = stream ? parser.ParseFragment(bytes, context) : parser.ParseFragment(source, context);
            var paragraph = fragment.QuerySelector("#TARGET");
            var table = fragment.QuerySelector("#TABLE");

            Assert.AreEqual(mode, ((Document)paragraph.Owner).QuirksMode);
            Assert.AreNotSame(document, paragraph.Owner);
            Assert.AreSame(paragraph.Owner, table.Owner);
            Assert.AreEqual(mode == QuirksMode.On ? "p" : "div", table.ParentElement.LocalName);
            Assert.AreEqual(1, fragment.QuerySelectorAll(".CARD").Length);
            Assert.AreEqual(1, fragment.QuerySelectorAll("#TARGET").Length);
            Assert.AreEqual(mode, ((Document)context.Owner).QuirksMode);
        }
    }
}
