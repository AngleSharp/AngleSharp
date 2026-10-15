namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Runtime.CompilerServices;

    [TestFixture]
    public class SelectStateMutationTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public void PopulatingAnEmptySelectUpdatesSelectionAndSubmission(Boolean markup)
        {
            var document = "<form action='https://example.com/'><select name=choice></select></form>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            if (markup)
            {
                select.InnerHtml = "<option value=a>A</option>";
            }
            else
            {
                var option = (IHtmlOptionElement)document.CreateElement("option");
                option.Value = "a";
                select.AppendChild(option);
            }

            Assert.AreEqual("a", select.Value);
            Assert.AreEqual(0, select.SelectedIndex);
            Assert.AreEqual(1, select.SelectedOptions.Length);
            Assert.AreEqual("choice=a", document.Forms[0].GetSubmission().Target.Query);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void CleanImplicitlyDeselectedOptionsStillFollowDefaults(Boolean normalizeWhileParsing)
        {
            var document = ("<select size=2><option value=a selected>A</option>" +
                "<option value=b" + (normalizeWhileParsing ? " selected" : "") + ">B</option></select>").ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var first = select.Options[0];

            if (!normalizeWhileParsing)
            {
                select.Options[1].IsSelected = true;
            }

            first.IsDefaultSelected = false;
            first.IsDefaultSelected = true;

            Assert.AreEqual("a", select.Value);
            Assert.IsTrue(first.IsSelected);
            Assert.IsFalse(select.Options[1].IsSelected);
        }

        [Test]
        public void ExplicitSelectionStaysDirtyUntilFormReset()
        {
            var document = "<form><select size=2><option selected>A</option><option>B</option></select></form>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var option = select.Options[0];

            option.IsSelected = false;
            option.IsDefaultSelected = false;
            option.IsDefaultSelected = true;
            Assert.IsFalse(option.IsSelected);

            document.Forms[0].Reset();
            Assert.IsTrue(option.IsSelected);
            option.IsDefaultSelected = false;
            Assert.IsFalse(option.IsSelected);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void ValueAndIndexDirtyOnlyTheirSelectedTarget(Boolean useIndex)
        {
            var document = "<select size=2><option value=a selected>A</option><option value=b>B</option></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");

            if (useIndex)
            {
                select.Options.SelectedIndex = 1;
            }
            else
            {
                select.Value = "b";
            }

            Assert.AreEqual("b", select.Value);
            select.Options[0].IsDefaultSelected = false;
            select.Options[0].IsDefaultSelected = true;
            Assert.AreEqual("a", select.Value);
            select.Options[1].IsDefaultSelected = true;
            Assert.IsFalse(select.Options[1].IsSelected);
        }

        [Test]
        public void GroupMovesAndSelectedFragmentsUpdateTheirOwners()
        {
            var document = ("<select id=source><option value=a>A</option><optgroup><option value=x>X</option><option value=b selected>B</option></optgroup></select>" +
                "<select id=target><option value=c>C</option></select>").ToHtmlDocument();
            var source = document.QuerySelector<IHtmlSelectElement>("#source");
            var target = document.QuerySelector<IHtmlSelectElement>("#target");
            var group = document.QuerySelector<IHtmlOptionsGroupElement>("optgroup");

            target.InsertBefore(group, target.FirstChild);
            Assert.AreEqual("a", source.Value);
            Assert.AreEqual("b", target.Value);
            Assert.AreEqual(1, target.SelectedOptions.Length);
            target.RemoveChild(group);
            Assert.AreEqual("c", target.Value);

            var fragment = document.CreateDocumentFragment();
            var first = (IHtmlOptionElement)document.CreateElement("option");
            var last = (IHtmlOptionElement)document.CreateElement("option");
            first.Value = "x";
            last.Value = "y";
            first.IsDefaultSelected = true;
            last.IsDefaultSelected = true;
            fragment.AppendChild(first);
            fragment.AppendChild(last);
            target.InsertBefore(fragment, target.FirstChild);

            Assert.AreEqual("y", target.Value);
            Assert.AreEqual(1, target.SelectedIndex);
            Assert.IsFalse(first.IsSelected);
            Assert.IsFalse(target.Options[2].IsSelected);
        }

        [Test]
        public void MovingASelectedOptionKeepsTheTwoSelectsIndependent()
        {
            var document = ("<select id=source><option value=a>A</option><option value=b selected>B</option></select>" +
                "<select id=target><option value=c>C</option></select>").ToHtmlDocument();
            var source = document.QuerySelector<IHtmlSelectElement>("#source");
            var target = document.QuerySelector<IHtmlSelectElement>("#target");

            target.AppendChild(source.Options[1]);
            Assert.AreEqual("a", source.Value);
            Assert.AreEqual("b", target.Value);

            source.Options[0].IsSelected = true;
            Assert.AreEqual("b", target.Value);
            target.Options[0].IsSelected = true;
            Assert.AreEqual("a", source.Value);
            Assert.AreEqual("c", target.Value);

            target.RemoveChild(target.Options[0]);
            Assert.AreEqual("b", target.Value);
            Assert.AreEqual(1, target.SelectedOptions.Length);
        }

        [Test]
        public void ClonesPreserveCurrentSelectednessAndDirtiness()
        {
            var document = ("<form><select id=list size=2><option value=a selected>A</option><option value=b>B</option></select></form>" +
                "<select id=dropdown><option value=c>C</option></select>").ToHtmlDocument();
            var list = document.QuerySelector<IHtmlSelectElement>("#list");
            var first = list.Options[0];
            first.IsSelected = false;
            var dirty = (IHtmlOptionElement)first.Clone();
            dirty.IsDefaultSelected = false;
            dirty.IsDefaultSelected = true;
            Assert.IsFalse(dirty.IsSelected);

            document.Forms[0].Reset();
            list.Options[1].IsSelected = true;
            var clean = (IHtmlOptionElement)first.Clone();
            Assert.IsFalse(clean.IsSelected);
            clean.IsDefaultSelected = false;
            clean.IsDefaultSelected = true;
            Assert.IsTrue(clean.IsSelected);

            var dropdown = document.QuerySelector<IHtmlSelectElement>("#dropdown");
            dropdown.Value = "missing";
            var clone = (IHtmlSelectElement)dropdown.Clone(true);
            Assert.AreEqual(-1, clone.SelectedIndex);
            clone.Options[0].IsDefaultSelected = true;
            Assert.AreEqual("c", clone.Value);
            Assert.AreEqual(-1, dropdown.SelectedIndex);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void ScriptsObserveSelectionBeforeTheSelectCloses(Boolean grouped)
        {
            var index = -2;
            String value = null;
            var scripting = new CallbackScriptEngine(options =>
            {
                var select = options.Document.QuerySelector<IHtmlSelectElement>("select");
                index = select.SelectedIndex;
                value = select.Value;
            });
            var option = "<option value=a>A</option>";
            var markup = "<select>" + (grouped ? "<optgroup>" + option + "</optgroup>" : option) +
                "<script type='c-sharp'>// observe</script></select>";
            markup.ToHtmlDocument(Configuration.Default.WithScripts(scripting));
            Assert.AreEqual(0, index);
            Assert.AreEqual("a", value);
        }

        [Test]
        public void ClonedMultipleSelectDoesNotRetainRemovedGroups()
        {
            var removed = RemoveClonedGroup(out var clone);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Assert.IsFalse(removed.IsAlive);
            GC.KeepAlive(clone);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static WeakReference RemoveClonedGroup(out IHtmlSelectElement clone)
        {
            var document = "<select multiple><optgroup><option selected>A</option></optgroup></select>".ToHtmlDocument();
            clone = (IHtmlSelectElement)document.QuerySelector<IHtmlSelectElement>("select").Clone();
            var group = clone.QuerySelector<IHtmlOptionsGroupElement>("optgroup");
            clone.RemoveChild(group);
            return new WeakReference(group);
        }

        [Test]
        public void ClonedGroupsChangeSelectionWithoutChangingTheSource()
        {
            var document = "<select><optgroup><option value=a>A</option><option value=b>B</option></optgroup><option value=c>C</option></select>".ToHtmlDocument();
            var source = document.QuerySelector<IHtmlSelectElement>("select");
            var clone = (IHtmlSelectElement)source.Clone();
            var group = clone.QuerySelector<IHtmlOptionsGroupElement>("optgroup");

            clone.Options[1].IsSelected = true;
            Assert.IsFalse(clone.Options[0].IsSelected);
            Assert.AreEqual("b", clone.Value);
            clone.RemoveChild(group);
            Assert.AreEqual("c", clone.Value);
            clone.InsertBefore(group, clone.FirstChild);
            Assert.AreEqual("b", clone.Value);
            Assert.AreEqual(1, clone.SelectedOptions.Length);
            Assert.AreEqual("a", source.Value);
            Assert.AreEqual(0, ((IHtmlSelectElement)source.Clone(false)).Length);
        }

        [Test]
        public void NonOptionMutationsPreserveAnExplicitClear()
        {
            var document = "<select><optgroup><option>A</option></optgroup></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var group = select.QuerySelector<IHtmlOptionsGroupElement>("optgroup");
            select.Value = "missing";
            var text = document.CreateTextNode("label");
            group.AppendChild(text);
            group.RemoveChild(text);
            select.AppendChild(text);
            select.RemoveChild(text);
            var empty = document.CreateElement("optgroup");
            select.AppendChild(empty);
            select.RemoveChild(empty);
            Assert.AreEqual(-1, select.SelectedIndex);
        }

        [Test]
        public void SizeAndMultipleChangesNormalizeWithoutDirtyingOptions()
        {
            var document = "<select size=2><option value=a>A</option><option value=b>B</option></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            Assert.AreEqual(-1, select.SelectedIndex);
            select.Size = 1;
            Assert.AreEqual("a", select.Value);

            select.IsMultiple = true;
            select.Options[1].IsDefaultSelected = true;
            Assert.AreEqual(2, select.SelectedOptions.Length);
            select.IsMultiple = false;
            Assert.AreEqual("a", select.Value);

            select.Options[1].IsDefaultSelected = false;
            select.Options[1].IsDefaultSelected = true;
            Assert.AreEqual("b", select.Value);
        }

        [Test]
        public void ExplicitClearsAndDropdownOptionSettersKeepDistinctBehavior()
        {
            var document = "<form action='https://example.com/'><select name=choice><option value=a>A</option></select></form>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            select.Value = "missing";
            Assert.IsNull(select.Value);
            Assert.AreEqual(-1, select.SelectedIndex);
            Assert.AreEqual(0, select.SelectedOptions.Length);
            Assert.AreEqual(String.Empty, document.Forms[0].GetSubmission().Target.Query);

            select.Options[0].IsSelected = false;
            Assert.AreEqual(0, select.SelectedIndex);
            Assert.AreEqual("a", select.Value);
        }
    }
}
