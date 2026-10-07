namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class OptionIndexTests
    {
        [TestCase("a", 0)]
        [TestCase("b", 1)]
        [TestCase("c", 2)]
        [TestCase("d", 3)]
        [TestCase("e", 4)]
        public void IndexCountsOptionsAcrossTheSelect(String id, Int32 expected)
        {
            var document = ("<select><option id=a>A</option><option id=b>B</option>" +
                "<optgroup label=Group>\n<option id=c>C</option>\n<!--gap-->\n<option id=d>D</option>\n</optgroup>" +
                "<option id=e>E</option></select>").ToHtmlDocument();

            Assert.AreEqual(expected, document.QuerySelector<IHtmlOptionElement>("#" + id).Index);
        }

        [TestCase("<option id=target>A</option>")]
        [TestCase("<optgroup><option>A</option><option id=target>B</option></optgroup>")]
        public void IndexWithoutAnOwningSelectIsZero(String html)
        {
            var document = html.ToHtmlDocument();

            Assert.AreEqual(0, document.QuerySelector<IHtmlOptionElement>("#target").Index);
        }

        [Test]
        public void IndexReflectsTheCurrentOptionOrder()
        {
            var document = "<select><option id=a>A</option><option id=b>B</option></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var a = document.QuerySelector<IHtmlOptionElement>("#a");
            var b = document.QuerySelector<IHtmlOptionElement>("#b");

            Assert.AreEqual(1, b.Index);
            select.InsertBefore(b, a);
            Assert.AreEqual(0, b.Index);
            Assert.AreEqual(1, a.Index);
            b.Remove();
            Assert.AreEqual(0, b.Index);
            Assert.AreEqual(0, a.Index);
        }
    }
}
