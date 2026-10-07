namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class RadioInitialCheckednessTests
    {
        [TestCase("document", "type=radio name=group checked")]
        [TestCase("document", "checked name=group type=radio")]
        [TestCase("document", "type=radio checked name=group")]
        [TestCase("form", "type=radio name=group checked")]
        [TestCase("form", "checked name=group type=radio")]
        [TestCase("form", "type=radio checked name=group")]
        [TestCase("template", "type=radio name=group checked")]
        [TestCase("template", "checked name=group type=radio")]
        [TestCase("template", "type=radio checked name=group")]
        public void LastDefaultCheckedRadioIsSelectedRegardlessOfAttributeOrder(String scope, String attributes)
        {
            var markup = "<input id=first " + attributes + "><input id=last " + attributes + ">";

            if (scope == "form")
            {
                markup = "<form>" + markup + "</form>";
            }
            else if (scope == "template")
            {
                markup = "<input id=outside type=radio name=group checked><template>" + markup + "</template>";
            }

            var document = markup.ToHtmlDocument();
            var root = scope == "template"
                ? (IParentNode)document.QuerySelector<IHtmlTemplateElement>("template").Content
                : document;
            var first = root.QuerySelector<IHtmlInputElement>("#first");
            var last = root.QuerySelector<IHtmlInputElement>("#last");

            Assert.IsFalse(first.IsChecked);
            Assert.IsTrue(last.IsChecked);
            Assert.IsTrue(first.IsDefaultChecked);
            Assert.IsTrue(last.IsDefaultChecked);

            if (scope == "template")
            {
                Assert.IsTrue(document.QuerySelector<IHtmlInputElement>("#outside").IsChecked);
            }
        }
    }
}
