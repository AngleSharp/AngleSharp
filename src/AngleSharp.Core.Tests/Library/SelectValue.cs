namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class SelectValueTests
    {
        [TestCase("")]
        [TestCase("multiple")]
        public void ValueSelectsOnlyTheFirstExactMatch(String attributes)
        {
            var document = ("<select " + attributes + "><option value=A>Upper</option>" +
                "<option value=a>First</option><option value=a selected>Second</option></select>").ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            select.Value = "a";

            Assert.AreEqual(1, select.SelectedIndex);
            Assert.AreEqual(1, select.SelectedOptions.Length);
            Assert.AreEqual("a", select.Value);
            Assert.IsTrue(select.Options[2].IsDefaultSelected);
        }

        [TestCase("")]
        [TestCase("multiple")]
        public void ValueWithNoExactMatchClearsSelection(String attributes)
        {
            var document = ("<select " + attributes + "><option value=a selected>Lower</option></select>").ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            select.Value = "A";

            Assert.AreEqual(-1, select.SelectedIndex);
            Assert.AreEqual(0, select.SelectedOptions.Length);
        }

        [TestCase("")]
        [TestCase("multiple")]
        public void ValueCanSelectTheFirstMatchInsideADisabledGroup(String attributes)
        {
            var document = ("<select " + attributes + "><optgroup disabled><option value=a>First</option></optgroup>" +
                "<option value=a selected>Second</option></select>").ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            select.Value = "a";

            Assert.AreEqual(0, select.SelectedIndex);
            Assert.AreEqual(1, select.SelectedOptions.Length);
            Assert.IsTrue(select.Options[0].IsSelected);
            Assert.IsFalse(select.Options[1].IsSelected);
        }
    }
}
