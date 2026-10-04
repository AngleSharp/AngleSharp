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

        [TestCase(false)]
        [TestCase(true)]
        public void RewritingAPresentSelectedAttributePreservesTheCurrentValue(Boolean useProperty)
        {
            var document = "<select><option selected value='a'>A</option><option value='b'>B</option></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            select.Value = "b";

            if (useProperty)
            {
                select.Options[0].IsDefaultSelected = true;
            }
            else
            {
                select.Options[0].SetAttribute("selected", "rewritten");
            }

            Assert.AreEqual("b", select.Value);
        }

        [Test]
        public void ChangingADirtyDefaultStillNormalizesAnEmptySingleSelect()
        {
            var document = "<select><option value='a'>A</option><option value='b'>B</option></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            select.Options[0].IsSelected = false;
            select.Options.SelectedIndex = -1;

            select.Options[0].IsDefaultSelected = true;

            Assert.AreEqual("a", select.Value);
            Assert.AreEqual(0, select.SelectedIndex);
        }

        [TestCase("add", "a")]
        [TestCase("remove", "b")]
        [TestCase("replace", "c")]
        [TestCase("group-remove", "b")]
        [TestCase("group-add", "a")]
        public void OptionMutationsPreserveDefaultSelectionAndSubmission(String operation, String expected)
        {
            var options = operation == "add" || operation == "group-add"
                ? String.Empty
                : "<option value='a'>A</option><option value='b'>B</option>";
            if (operation.StartsWith("group", StringComparison.Ordinal))
            {
                options = "<optgroup>" + options + "</optgroup>";
            }
            var document = ("<form action='https://example.invalid/'><select name='choice'>" + options + "</select></form>").ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var form = document.QuerySelector<IHtmlFormElement>("form");
            var added = (IHtmlOptionElement)document.CreateElement("option");
            added.Value = "a";

            if (operation == "add") select.AddOption(added);
            else if (operation == "remove") select.RemoveOptionAt(0);
            else if (operation == "replace") select.InnerHtml = "<option value='c'>C</option>";
            else if (operation == "group-remove") select.Options[0].Remove();
            else select.QuerySelector("optgroup").AppendChild(added);

            Assert.AreEqual(expected, select.Value);
            Assert.AreEqual("https://example.invalid/?choice=" + expected, form.GetSubmission().Target.Href);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void InsertingAnEarlierSelectedOptionReplacesTheExistingSelection(Boolean inGroup)
        {
            var document = "<select><option selected value='b'>B</option></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var added = (IHtmlOptionElement)document.CreateElement("option");
            added.Value = "a";
            added.IsSelected = true;
            INode inserted = added;
            if (inGroup)
            {
                inserted = document.CreateElement("optgroup");
                inserted.AppendChild(added);
            }

            select.InsertBefore(inserted, select.FirstChild);

            Assert.AreEqual("a", select.Value);
            Assert.AreEqual(1, select.SelectedOptions.Length);
        }

        [Test]
        public void InsertingAFragmentUsesTheLastSelectedOption()
        {
            var document = "<select></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var fragment = document.CreateDocumentFragment();
            foreach (var value in new[] { "a", "b" })
            {
                var option = (IHtmlOptionElement)document.CreateElement("option");
                option.Value = value;
                option.IsSelected = true;
                fragment.AppendChild(option);
            }

            select.AppendChild(fragment);

            Assert.AreEqual("b", select.Value);
            Assert.AreEqual(1, select.SelectedOptions.Length);
        }

        [Test]
        public void InsertingAnUnselectedOptionKeepsMultipleSelectionEmpty()
        {
            var document = "<select multiple></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            select.AddOption((IHtmlOptionElement)document.CreateElement("option"));

            Assert.AreEqual(-1, select.SelectedIndex);
            Assert.AreEqual(0, select.SelectedOptions.Length);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void MutatingNonOptionsDoesNotRestoreAClearedSelection(Boolean inGroup)
        {
            var document = "<select><option value='a'>A</option><optgroup></optgroup></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var parent = inGroup ? select.QuerySelector("optgroup") : select;
            select.Options.SelectedIndex = -1;
            var text = document.CreateTextNode(" ");

            parent.AppendChild(text);
            Assert.AreEqual(-1, select.SelectedIndex);
            parent.RemoveChild(text);
            Assert.AreEqual(-1, select.SelectedIndex);
        }
    }
}
