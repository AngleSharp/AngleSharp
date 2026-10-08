namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CssSelectorDocumentModeTests
    {
        [TestCase("", true)]
        [TestCase("<!doctype html>", false)]
        [TestCase("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">", false)]
        public void IdAndClassSelectorsUseAsciiCaseInsensitivityOnlyInQuirksMode(String doctype, Boolean quirks)
        {
            var document = new HtmlParser().ParseDocument(doctype + "<div id='SAMPLEÅ' class='SAMPLEÅ'>Target</div>");
            var target = document.QuerySelector("div")!;

            Assert.AreEqual(quirks, target.Matches("#sampleÅ"));
            Assert.AreEqual(quirks, target.Matches(".sampleÅ"));
            Assert.AreEqual(quirks, document.QuerySelector("#sampleÅ") is not null);
            Assert.AreEqual(quirks, document.QuerySelector(".sampleÅ") is not null);
            Assert.IsFalse(target.Matches("#sampleå"));
            Assert.IsFalse(target.Matches(".sampleå"));
            Assert.IsFalse(target.Matches("[id='sampleÅ']"));
            Assert.IsFalse(target.Matches("[class='sampleÅ']"));
            Assert.AreSame(target, document.QuerySelector("[id='SAMPLEÅ']"));
        }

        [Test]
        public void DetachedSelectorsRetainTheirOwnerDocumentMode()
        {
            var document = new HtmlParser().ParseDocument("<section><p id='TARGET' class='CARD'></p></section>");
            var section = document.QuerySelector("section")!;
            var target = section.QuerySelector("p")!;
            section.Remove();

            Assert.IsNull(document.QuerySelector("#target"));
            Assert.AreSame(target, section.QuerySelector("#target.card"));
            Assert.IsTrue(target.Matches("section > #target.card"));
        }

        [TestCase("", true)]
        [TestCase("<!doctype html>", false)]
        [TestCase("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">", false)]
        public void ClassSelectorsInspectEveryTokenAndReflectTokenListChanges(String doctype, Boolean quirks)
        {
            var document = new HtmlParser().ParseDocument(doctype + "<div class='OTHER CARD LAST'></div>");
            var target = document.QuerySelector("div")!;

            Assert.AreEqual(quirks, target.Matches(".card"));
            Assert.AreEqual(quirks, target.Matches(".last"));
            Assert.IsFalse(target.Matches(".car"));
            Assert.IsFalse(target.Matches(".missing"));
            target.ClassList.Remove("CARD");

            Assert.IsFalse(target.Matches(".CARD"));
            target.ClassList.Add("card");

            Assert.IsTrue(target.Matches(".card"));
            target.ClassList.Remove("OTHER", "card", "LAST");

            Assert.IsFalse(target.Matches(".card"));
        }
    }
}
