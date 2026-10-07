namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class SelectSelectionTests
    {
        // https://html.spec.whatwg.org/multipage/form-elements.html#selectedness-setting-algorithm
        [TestCase("<option value='a'>A</option><option value='b'>B</option>", "", "a")]
        [TestCase("<option disabled value='a'>A</option><option value='b'>B</option>", "", "b")]
        [TestCase("<optgroup disabled><option value='a'>A</option></optgroup><option value='b'>B</option>", "", "b")]
        [TestCase("<option selected value='a'>A</option><option selected value='b'>B</option>", "", "b")]
        [TestCase("<option value='a'>A</option><option value='b'>B</option>", "size='2'", "")]
        [TestCase("<option value='a'>A</option><option value='b'>B</option>", "multiple", "")]
        [TestCase("<option selected value='a'>A</option><option selected value='b'>B</option>", "multiple", "a,b")]
        [TestCase("", "", "")]
        [TestCase("<option disabled value='a'>A</option>", "", "")]
        public void ParsedSelectInitializesSelectedness(String options, String attributes, String selectedValues)
        {
            var document = ("<select " + attributes + ">" + options + "</select>").ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            Assert.AreEqual(selectedValues, String.Join(",", select.SelectedOptions.Select(option => option.Value)));
            Assert.AreEqual(selectedValues.Split(',')[0], select.Value ?? String.Empty);
            Assert.AreEqual(selectedValues.Length == 0 ? -1 : select.Options.ToList().FindIndex(option => option.IsSelected), select.SelectedIndex);
        }
    }
}
