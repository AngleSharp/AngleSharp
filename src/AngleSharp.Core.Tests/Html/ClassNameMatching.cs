namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class ClassNameMatchingTests
    {
        private const String LimitedQuirks = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";

        // https://dom.spec.whatwg.org/#concept-getelementsbyclassname
        // https://github.com/web-platform-tests/wpt/blob/master/dom/nodes/getElementsByClassName-14.htm
        [TestCase("", QuirksMode.On, "BackCompat", 2)]
        [TestCase("<!doctype html>", QuirksMode.Off, "CSS1Compat", 1)]
        [TestCase(LimitedQuirks, QuirksMode.Limited, "CSS1Compat", 1)]
        public void ClassNamesRespectDocumentMode(String doctype, QuirksMode mode, String compatMode, Int32 count)
        {
            var document = (doctype + "<html class='a A'><body class='a a'></body></html>").ToHtmlDocument();
            var matches = document.GetElementsByClassName("A a");

            Assert.AreEqual(mode, ((Document)document).QuirksMode);
            Assert.AreEqual(compatMode, document.CompatMode);
            Assert.AreEqual(count, matches.Length);
            Assert.AreSame(document.DocumentElement, matches[0]);
            Assert.AreEqual(count - 1, document.DocumentElement.GetElementsByClassName("A a").Length);

            if (count == 2)
            {
                Assert.AreSame(document.Body, matches[1]);
                Assert.AreSame(document.Body, document.DocumentElement.GetElementsByClassName("A a")[0]);
            }

            Assert.IsFalse(document.Body.ClassList.Contains("A"));
        }

        [TestCase("", "K", "upper,lower")]
        [TestCase("", "k", "upper,lower")]
        [TestCase("<!doctype html>", "K", "upper")]
        [TestCase(LimitedQuirks, "K", "upper")]
        [TestCase("", "K", "kelvin")]
        [TestCase("", "Ä", "nonAsciiUpper")]
        [TestCase("", "ä", "nonAsciiLower")]
        [TestCase("", "Äb", "mixed")]
        [TestCase("", "", "")]
        [TestCase("", " \t\n\r\f ", "")]
        [TestCase("", "K missing", "")]
        [TestCase("", " K\tK\nk ", "upper,lower")]
        public void ClassNamesFoldOnlyAscii(String doctype, String query, String expected)
        {
            var document = (doctype + "<body>" +
                "<i id='upper' class='K'></i><i id='lower' class='k'></i>" +
                "<i id='kelvin' class='K'></i><i id='nonAsciiUpper' class='Ä'></i>" +
                "<i id='nonAsciiLower' class='ä'></i><i id='mixed' class='ÄB'></i></body>").ToHtmlDocument();

            Assert.AreEqual(expected, String.Join(",", document.GetElementsByClassName(query).Select(m => m.Id)));
            Assert.AreEqual(expected, String.Join(",", document.Body.GetElementsByClassName(query).Select(m => m.Id)));
        }

        [Test]
        public void DetachedElementUsesOwnerDocumentMode()
        {
            var document = "<html>".ToHtmlDocument();
            var root = document.CreateElement("div");
            root.ClassName = "A";
            root.InnerHtml = "<span class='a'></span>";

            Assert.AreEqual(1, root.GetElementsByClassName("A").Length);
            Assert.AreSame(root.FirstElementChild, root.GetElementsByClassName("A")[0]);
        }
    }
}
