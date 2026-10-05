namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class OptionSelectednessTests
    {
        [TestCase("", false)]
        [TestCase("multiple", true)]
        public void SelectingOptionPreservesMultipleSelectBehavior(String attributes, Boolean firstRemainsSelected)
        {
            var document = ("<select " + attributes + "><option selected>First</option>" +
                "<optgroup><option>Second</option></optgroup></select>").ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            select.Options[1].IsSelected = true;

            Assert.AreEqual(firstRemainsSelected, select.Options[0].IsSelected);
            Assert.IsTrue(select.Options[1].IsSelected);
            Assert.IsTrue(select.Options[0].IsDefaultSelected);
            Assert.AreEqual(firstRemainsSelected ? 2 : 1, select.SelectedOptions.Length);
        }

        [Test]
        public void ClearingListBoxOptionDoesNotSelectAFallback()
        {
            var document = "<select size=2><option selected>First</option><option>Second</option></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            select.Options[0].IsSelected = false;

            Assert.AreEqual(-1, select.SelectedIndex);
            Assert.AreEqual(0, select.SelectedOptions.Length);
        }

        [Test]
        public void SelectingOptionPreservesDefaultStateOfAnUnselectedPeer()
        {
            var document = "<select size=2><option>First</option><option>Second</option></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            select.Options[1].IsSelected = true;
            select.Options[1].IsSelected = false;
            select.Options[0].IsDefaultSelected = true;

            Assert.IsTrue(select.Options[0].IsSelected);
            Assert.IsFalse(select.Options[1].IsSelected);
        }

        [Test]
        public void SelectingOptionDoesNotChangeAnotherSelect()
        {
            var document = "<select><option selected>First</option></select><select><option>Second</option></select>".ToHtmlDocument();
            var selects = document.QuerySelectorAll<IHtmlSelectElement>("select").ToArray();

            selects[1].Options[0].IsSelected = true;

            Assert.IsTrue(selects[0].Options[0].IsSelected);
            Assert.IsTrue(selects[1].Options[0].IsSelected);
        }
    }
}
