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
        public void ParsedSelectUsesTheSelectednessSettingAlgorithm(String options, String attributes, String selectedValues)
        {
            var document = ("<select " + attributes + ">" + options + "</select>").ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            Assert.AreEqual(selectedValues, String.Join(",", select.SelectedOptions.Select(option => option.Value)));
            Assert.AreEqual(selectedValues.Split(',')[0], select.Value ?? String.Empty);
            Assert.AreEqual(selectedValues.Length == 0 ? -1 : select.Options.ToList().FindIndex(option => option.IsSelected), select.SelectedIndex);
        }

        [Test]
        public void DefaultSelectionAttributesRemainEffectiveUntilTheOptionIsDirty()
        {
            var document = "<select><option value='a'>A</option><option value='b'>B</option></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var second = select.Options[1];

            second.IsDefaultSelected = true;
            Assert.IsTrue(second.IsSelected);
            second.IsSelected = false;
            second.IsDefaultSelected = false;
            second.IsDefaultSelected = true;
            Assert.IsFalse(second.IsSelected);
        }

        [Test]
        public void ResetRestoresDefaultsWithoutMakingOptionsDirty()
        {
            var document = "<form><select><option value='a'>A</option><option selected value='b'>B</option></select></form>".ToHtmlDocument();
            var form = document.QuerySelector<IHtmlFormElement>("form");
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            select.Value = "a";

            form.Reset();
            Assert.AreEqual("b", select.Value);
            select.Options[1].IsDefaultSelected = false;
            Assert.AreEqual("a", select.Value);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SelectingAnEarlierOptionReplacesTheLaterSelection(Boolean changeDefault)
        {
            var document = "<select><option value='a'>A</option><option selected value='b'>B</option></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            if (changeDefault)
            {
                select.Options[0].IsDefaultSelected = true;
            }
            else
            {
                select.Options[0].IsSelected = true;
            }

            Assert.AreEqual("a", select.Value);
            Assert.AreEqual(0, select.SelectedIndex);
            Assert.AreEqual(1, select.SelectedOptions.Length);
            Assert.IsFalse(select.Options[1].IsSelected);
        }

        [TestCase("")]
        [TestCase("multiple")]
        public void SettingValueUsesTheFirstExactMatchAndCanClearSelection(String attributes)
        {
            var document = ("<select " + attributes + "><option value='A'>Upper</option><option value='a'>First</option><option value='a'>Second</option></select>").ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            select.Value = "a";
            Assert.AreEqual(1, select.SelectedIndex);
            Assert.AreEqual(1, select.SelectedOptions.Length);
            select.Value = "missing";
            Assert.AreEqual(-1, select.SelectedIndex);
            Assert.AreEqual(String.Empty, select.Value);
            Assert.AreEqual(0, select.SelectedOptions.Length);
        }

        [Test]
        public void SettingSelectedIndexCanSelectAndClearWithoutChoosingAFallback()
        {
            var document = "<select><option value='a'>A</option><option value='b'>B</option></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            select.Options.SelectedIndex = 1;
            Assert.AreEqual("b", select.Value);
            select.Options.SelectedIndex = -1;
            Assert.AreEqual(String.Empty, select.Value);
            Assert.AreEqual(-1, select.SelectedIndex);
            Assert.AreEqual(0, select.SelectedOptions.Length);
        }
    }
}
