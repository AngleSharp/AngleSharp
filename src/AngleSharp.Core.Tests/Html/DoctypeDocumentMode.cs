namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Html.Parser;
    using AngleSharp.Html.Parser.Tokens;
    using AngleSharp.Text;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DoctypeDocumentModeTests
    {
        private static readonly Object[] Cases =
        {
            new Object[] { "<!DOCTYPE html>", QuirksMode.Off },
            new Object[] { "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">", QuirksMode.Off },
            new Object[] { "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">", QuirksMode.Limited },
            new Object[] { "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Frameset//EN\" \"http://www.w3.org/TR/html4/frameset.dtd\">", QuirksMode.Limited },
            new Object[] { "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">", QuirksMode.On },
            new Object[] { "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Frameset//EN\">", QuirksMode.On },
            new Object[] { "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"\">", QuirksMode.On },
            new Object[] { "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Frameset//EN\" \"\">", QuirksMode.On },
            new Object[] { "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">", QuirksMode.Limited },
            new Object[] { "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Frameset//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd\">", QuirksMode.Limited },
        };

        [TestCaseSource(nameof(Cases))]
        public void ParsedDocumentUsesDoctypeMode(String doctype, QuirksMode mode)
        {
            var document = doctype.ToHtmlDocument();

            Assert.AreEqual(mode, ((Document)document).QuirksMode);
            Assert.AreEqual(mode == QuirksMode.On ? "BackCompat" : "CSS1Compat", document.CompatMode);
        }

        [TestCaseSource(nameof(Cases))]
        public void PublicDoctypeTokenUsesDoctypeMode(String doctype, QuirksMode mode)
        {
            var tokenizer = new HtmlTokenizer(new TextSource(doctype), HtmlEntityProvider.Resolver);
            var token = (HtmlDoctypeToken)tokenizer.Get();

            Assert.AreEqual(mode == QuirksMode.On, token.IsFullQuirks);
            Assert.AreEqual(mode == QuirksMode.Limited, token.IsLimitedQuirks);
        }
    }
}
