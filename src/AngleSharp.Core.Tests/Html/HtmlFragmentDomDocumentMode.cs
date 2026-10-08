namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class HtmlFragmentDomDocumentModeTests
    {
        private const String Source = "<p id=paragraph>Paragraph<table id=table><tr><td>Cell</td></tr></table>";

        private static readonly Object[] DocumentModes =
        {
            new Object[] { "", QuirksMode.On },
            new Object[] { "<!doctype html>", QuirksMode.Off },
            new Object[] { "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">", QuirksMode.Limited },
        };

        private static IEnumerable<TestCaseData> DomCases
        {
            get
            {
                foreach (Object[] mode in DocumentModes)
                {
                    foreach (var operation in new[] { "innerHTML", "outerHTML", "insertInside", "insertOutside" })
                    {
                        yield return new TestCaseData(mode[0], mode[1], operation);
                    }
                }
            }
        }

        [TestCaseSource(nameof(DocumentModes))]
        public void PublicSubtreeRetainsContextDocumentMode(String doctype, QuirksMode mode)
        {
            var document = (doctype + "<div id=context></div>").ToHtmlDocument();
            var context = (Element)document.QuerySelector("#context");

            Assert.AreEqual(mode, ((Document)document).QuirksMode);

            var root = context.ParseSubtree(Source);
            var table = root.QuerySelector("#table");

            Assert.AreEqual(mode, ((Document)table.Owner).QuirksMode);
            Assert.AreNotSame(document, table.Owner);
            Assert.AreEqual(mode == QuirksMode.On ? "p" : "html", table.ParentElement.LocalName);
            Assert.AreEqual(mode, ((Document)document).QuirksMode);
        }

        [TestCaseSource(nameof(DomCases))]
        public void DomMarkupParsingUsesContextModeBeforeAdoption(String doctype, QuirksMode mode, String operation)
        {
            var document = (doctype + "<div id=context><span></span></div>").ToHtmlDocument();
            var context = document.QuerySelector("#context");

            Assert.AreEqual(mode, ((Document)document).QuirksMode);

            switch (operation)
            {
                case "innerHTML":
                    context.InnerHtml = Source;
                    break;
                case "outerHTML":
                    context.FirstElementChild.OuterHtml = Source;
                    break;
                case "insertInside":
                    context.Insert(AdjacentPosition.AfterBegin, Source);
                    break;
                case "insertOutside":
                    context.FirstElementChild.Insert(AdjacentPosition.AfterEnd, Source);
                    break;
            }

            var table = context.QuerySelector("#table");

            Assert.AreSame(document, table.Owner);
            Assert.AreEqual(mode == QuirksMode.On ? "p" : "div", table.ParentElement.LocalName);
            Assert.AreEqual(mode, ((Document)document).QuirksMode);
        }
    }
}
