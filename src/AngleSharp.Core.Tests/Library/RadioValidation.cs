namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class RadioValidationTests
    {
        [TestCase("required", "checked", false)]
        [TestCase("required", "", true)]
        [TestCase("required disabled", "", true)]
        [TestCase("required disabled", "checked", false)]
        [TestCase("required", "checked disabled", false)]
        public void RequiredAndCheckedStateApplyToEveryGroupMember(String first, String second, Boolean missing)
        {
            var document = ("<form><input id=a type=radio name=choice " + first +
                "><input id=b type=radio name=choice " + second + "></form>").ToHtmlDocument();

            Assert.AreEqual(missing, Input(document, "a").Validity.IsValueMissing);
            Assert.AreEqual(missing, Input(document, "b").Validity.IsValueMissing);
            Assert.AreEqual(!missing, document.Forms[0].CheckValidity());
        }

        [TestCase("name=other", "type=radio")]
        [TestCase("name=CHOICE", "type=radio")]
        [TestCase("name=''", "type=radio")]
        [TestCase("", "type=radio")]
        [TestCase("name=choice", "type=checkbox")]
        public void CheckedUnrelatedControlDoesNotSatisfyRequiredRadio(String name, String type)
        {
            var document = ("<input id=a type=radio name=choice required>" +
                "<input id=b checked " + name + " " + type + ">").ToHtmlDocument();

            Assert.IsTrue(Input(document, "a").Validity.IsValueMissing);
            Assert.IsFalse(Input(document, "b").Validity.IsValueMissing);
        }

        [Test]
        public void FormOwnerIncludesExternalControlsButSeparatesOtherForms()
        {
            var document = ("<form id=first><input id=a type=radio name=choice required></form>" +
                "<form id=second><input id=other type=radio name=choice checked></form>" +
                "<input id=b form=first type=radio name=choice>").ToHtmlDocument();
            var a = Input(document, "a");
            var b = Input(document, "b");

            Assert.IsTrue(a.Validity.IsValueMissing);
            Assert.IsTrue(b.Validity.IsValueMissing);
            Assert.IsFalse(Input(document, "other").Validity.IsValueMissing);
            b.IsChecked = true;
            Assert.IsFalse(a.Validity.IsValueMissing);
            Assert.IsTrue(document.Forms[0].CheckValidity());
        }

        [Test]
        public void GroupValidityUsesLiveNamesRequiredStateAndCheckedness()
        {
            var document = "<input id=a type=radio name=choice required><input id=b type=radio name=choice>".ToHtmlDocument();
            var a = Input(document, "a");
            var b = Input(document, "b");
            Assert.IsTrue(b.Validity.IsValueMissing);
            a.IsRequired = false;
            Assert.IsFalse(b.Validity.IsValueMissing);
            a.IsRequired = true;
            b.IsChecked = true;
            Assert.IsFalse(a.Validity.IsValueMissing);
            b.Name = "other";
            Assert.IsTrue(a.Validity.IsValueMissing);
        }

        [Test]
        public void DetachedRadioUsesItsOwnTree()
        {
            var document = "<input type=radio name=choice checked>".ToHtmlDocument();
            var detached = document.CreateElement<IHtmlInputElement>();
            detached.Type = "radio";
            detached.Name = "choice";
            detached.IsRequired = true;

            Assert.IsTrue(detached.Validity.IsValueMissing);
        }

        [Test]
        public void DetachedFormRadiosUseTheirOwnTree()
        {
            var document = String.Empty.ToHtmlDocument();
            var form = document.CreateElement<IHtmlFormElement>();
            var required = document.CreateElement<IHtmlInputElement>();
            required.Type = "radio";
            required.Name = "choice";
            required.IsRequired = true;
            form.AppendChild(required);

            var other = document.CreateElement<IHtmlInputElement>();
            other.Type = "radio";
            other.Name = "choice";
            form.AppendChild(other);

            Assert.IsTrue(other.Validity.IsValueMissing);
            other.IsChecked = true;
            Assert.IsFalse(required.Validity.IsValueMissing);
        }

        [Test]
        public void RequiredCheckboxUsesItsOwnCheckedness()
        {
            var document = "<input id=a type=checkbox name=choice required><input id=b type=checkbox name=choice checked>".ToHtmlDocument();
            Assert.IsTrue(Input(document, "a").Validity.IsValueMissing);
            Assert.IsFalse(Input(document, "b").Validity.IsValueMissing);
        }

        private static IHtmlInputElement Input(IDocument document, String id) =>
            (IHtmlInputElement)document.GetElementById(id);
    }
}
