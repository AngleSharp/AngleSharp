namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;

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
    }
}
