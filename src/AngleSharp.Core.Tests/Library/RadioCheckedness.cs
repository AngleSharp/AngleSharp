namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [TestFixture]
    public class RadioCheckednessTests
    {
        [TestCase("<input id=first type=radio name=group checked><input id=second type=radio name=group>", false)]
        [TestCase("<form id=owner><input id=first type=radio name=group checked></form><input id=second type=radio name=group form=owner>", false)]
        [TestCase("<input id=first type=radio name=Group checked><input id=second type=radio name=group>", true)]
        [TestCase("<form><input id=first type=radio name=group checked></form><form><input id=second type=radio name=group></form>", true)]
        [TestCase("<input id=first type=radio checked><input id=second type=radio>", true)]
        [TestCase("<input id=first type=checkbox name=group checked><input id=second type=radio name=group>", true)]
        public void CheckingRadioOnlyUnchecksMembersOfItsGroup(String markup, Boolean firstRemainsChecked)
        {
            var document = markup.ToHtmlDocument();
            var first = document.QuerySelector<IHtmlInputElement>("#first");
            var second = document.QuerySelector<IHtmlInputElement>("#second");

            second.IsChecked = true;

            Assert.AreEqual(firstRemainsChecked, first.IsChecked);
            Assert.IsTrue(second.IsChecked);
            Assert.IsTrue(first.IsDefaultChecked);
        }

        [Test]
        public void CheckingRadioPreservesDefaultStateOfAnUncheckedPeer()
        {
            var document = "<input type=radio name=group><input type=radio name=group>".ToHtmlDocument();
            var radios = document.QuerySelectorAll<IHtmlInputElement>("input").ToArray();
            var first = radios[0];
            var second = radios[1];

            second.IsChecked = true;
            second.IsChecked = false;
            first.IsDefaultChecked = true;

            Assert.IsTrue(first.IsChecked);
            Assert.IsFalse(second.IsChecked);
        }

        [Test]
        public void CheckingDetachedRadioDoesNotUncheckAnotherTree()
        {
            var document = "<input type=radio name=group checked>".ToHtmlDocument();
            var connected = document.QuerySelector<IHtmlInputElement>("input");
            var detached = (IHtmlInputElement)document.CreateElement("input");
            detached.Type = "radio";
            detached.Name = "group";

            detached.IsChecked = true;

            Assert.IsTrue(connected.IsChecked);
            Assert.IsTrue(detached.IsChecked);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void ImplicitlyUncheckedPeerStillReflectsDefaultChanges(Boolean useProperty)
        {
            var document = "<input id=first type=radio name=group checked><input id=second type=radio name=group>".ToHtmlDocument();
            var first = document.QuerySelector<IHtmlInputElement>("#first");
            var second = document.QuerySelector<IHtmlInputElement>("#second");
            second.IsChecked = true;
            Assert.IsFalse(first.IsChecked);

            if (useProperty)
            {
                first.IsDefaultChecked = false;
                first.IsDefaultChecked = true;
            }
            else
            {
                first.RemoveAttribute("checked");
                first.SetAttribute("checked", String.Empty);
            }

            Assert.IsTrue(first.IsChecked);
            Assert.IsTrue(first.IsDefaultChecked);
            Assert.IsFalse(second.IsChecked);
        }

        [Test]
        public void CloningAndResettingPreserveImplicitCheckedness()
        {
            var document = "<form><input id=first type=radio name=group checked><input id=second type=radio name=group></form>".ToHtmlDocument();
            var first = document.QuerySelector<IHtmlInputElement>("#first");
            document.QuerySelector<IHtmlInputElement>("#second").IsChecked = true;
            var clone = (IHtmlInputElement)first.Clone();

            Assert.IsFalse(clone.IsChecked);
            Assert.IsTrue(clone.IsDefaultChecked);
            document.QuerySelector<IHtmlFormElement>("form").Reset();
            Assert.IsTrue(first.IsChecked);
            Assert.IsFalse(document.QuerySelector<IHtmlInputElement>("#second").IsChecked);
            Assert.IsFalse(clone.IsChecked);
        }

        [Test]
        public void CheckingRadiosAfterRepeatedAdoptionUsesTheirCurrentDocument()
        {
            var source = "<form><input id=first type=radio name=group checked><input id=second type=radio name=group></form>".ToHtmlDocument();
            var target = "<input id=outside type=radio name=group checked>".ToHtmlDocument();
            var form = source.QuerySelector("form");
            var first = source.QuerySelector<IHtmlInputElement>("#first");
            var second = source.QuerySelector<IHtmlInputElement>("#second");
            target.AdoptNode(form);
            source.AdoptNode(form);
            target.Body.AppendChild(form);

            second.IsChecked = true;
            Assert.IsFalse(first.IsChecked);
            Assert.IsTrue(target.QuerySelector<IHtmlInputElement>("#outside").IsChecked);
            first.IsChecked = true;
            Assert.IsFalse(second.IsChecked);
        }

        [Test]
        public void DetachedInputsCanBeCollectedWhileTheirDocumentIsAlive()
        {
            var document = "<p>Kept alive</p>".ToHtmlDocument();
            var input = CreateDetachedInput(document);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Assert.IsFalse(input.IsAlive);
            GC.KeepAlive(document);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static WeakReference CreateDetachedInput(IDocument document) =>
            new WeakReference(document.CreateElement("input"));

        [Test]
        public void InputTrackingDoesNotKeepItsDocumentAlive()
        {
            var document = CreateInputDocument();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Assert.IsFalse(document.IsAlive);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static WeakReference CreateInputDocument() =>
            new WeakReference("<input type=radio name=group checked>".ToHtmlDocument());

        [Test]
        public void CheckedAttributesInTemplatesKeepTheirGroupsSeparate()
        {
            var document = "<input id=outside type=radio name=group checked><template id=outer><input id=first type=radio name=group checked><input id=last type=radio name=group checked><template id=inner><input type=radio name=group checked></template></template>".ToHtmlDocument();
            var outer = document.QuerySelector<IHtmlTemplateElement>("#outer");
            var inner = outer.Content.QuerySelector<IHtmlTemplateElement>("#inner");
            var first = outer.Content.QuerySelector<IHtmlInputElement>("#first");
            var last = outer.Content.QuerySelector<IHtmlInputElement>("#last");

            Assert.IsTrue(document.QuerySelector<IHtmlInputElement>("#outside").IsChecked);
            Assert.IsFalse(first.IsChecked);
            Assert.IsTrue(last.IsChecked);
            Assert.IsTrue(inner.Content.QuerySelector<IHtmlInputElement>("input").IsChecked);
            first.IsChecked = true;
            Assert.IsFalse(last.IsChecked);
            Assert.IsTrue(document.QuerySelector<IHtmlInputElement>("#outside").IsChecked);
        }
    }
}
