namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class GroupedOptionsMutationTests
    {
        [Test]
        public void RemovingAGroupedOptionUpdatesDomAndCollectionMembership()
        {
            var document = "<select size=2><optgroup><option>A</option><option>B</option></optgroup></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var group = document.QuerySelector<IHtmlOptionsGroupElement>("optgroup");
            var removed = select.Options[0];
            var remaining = select.Options[1];

            select.RemoveOptionAt(0);

            Assert.IsNull(removed.Parent);
            Assert.AreSame(group, remaining.Parent);
            Assert.AreEqual(1, group.Children.Length);
            Assert.AreEqual(1, select.Length);
            Assert.AreSame(remaining, select.Options[0]);
        }

        [Test]
        public void ReplacingAGroupedOptionUpdatesDomAndCollectionMembership()
        {
            var document = "<select size=2><optgroup><option>A</option><option>B</option></optgroup></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var group = document.QuerySelector<IHtmlOptionsGroupElement>("optgroup");
            var replaced = select.Options[0];
            var remaining = select.Options[1];
            var replacement = (IHtmlOptionElement)document.CreateElement("option");

            select[0] = replacement;

            Assert.IsNull(replaced.Parent);
            Assert.AreSame(group, replacement.Parent);
            Assert.AreSame(remaining, replacement.NextSibling);
            Assert.AreEqual(2, group.Children.Length);
            Assert.AreEqual(2, select.Length);
            Assert.AreSame(replacement, select.Options[0]);
            Assert.AreSame(remaining, select.Options[1]);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void AddingBeforeAGroupedOptionUsesItsParentAndKeepsCollectionMembership(Boolean addGroup)
        {
            var document = "<select size=2><optgroup><option>A</option><option>B</option></optgroup></select>".ToHtmlDocument();
            var select = document.QuerySelector<IHtmlSelectElement>("select");
            var parent = document.QuerySelector<IHtmlOptionsGroupElement>("optgroup");
            var reference = select.Options[0];
            IHtmlElement inserted;

            if (addGroup)
            {
                var group = (IHtmlOptionsGroupElement)document.CreateElement("optgroup");
                select.AddOption(group, reference);
                inserted = group;
            }
            else
            {
                var option = (IHtmlOptionElement)document.CreateElement("option");
                select.AddOption(option, reference);
                inserted = option;
            }

            Assert.AreSame(parent, inserted.Parent);
            Assert.AreSame(reference, inserted.NextSibling);
            Assert.AreSame(inserted, parent.Children[0]);
            Assert.AreEqual(addGroup ? 2 : 3, select.Length);
            Assert.AreSame(addGroup ? reference : inserted, select.Options[0]);

            if (!addGroup)
            {
                Assert.AreSame(reference, select.Options[1]);
            }
        }
    }
}
