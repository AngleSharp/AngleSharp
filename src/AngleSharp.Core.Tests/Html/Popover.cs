namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class PopoverTests
    {
        private static IDocument Html(String markup) => new HtmlParser().ParseDocument(markup);

        [Test]
        public void PopoverAttributeReflectsItsNormalizedValue()
        {
            var popover = Html("<div></div>").QuerySelector<IHtmlElement>("div");

            Assert.IsNull(popover.GetPopover());

            popover.SetPopover(String.Empty);
            Assert.AreEqual("auto", popover.GetPopover());

            popover.SetPopover("HiNt");
            Assert.AreEqual("hint", popover.GetPopover());

            popover.SetPopover("invalid");
            Assert.AreEqual("manual", popover.GetPopover());

            popover.SetPopover(null);
            Assert.IsNull(popover.GetPopover());
        }

        [Test]
        public void ShowAndHidePopoverDispatchToggleEvents()
        {
            var document = Html("<div popover></div>");
            var popover = document.QuerySelector<IHtmlElement>("div");
            var states = new List<String>();

            popover.AddEventListener(EventNames.BeforeToggle, (_, ev) =>
            {
                var toggle = (ToggleEvent)ev;
                states.Add($"{toggle.OldState}>{toggle.NewState}:{toggle.IsCancelable}");
            });
            popover.AddEventListener(EventNames.Toggle, (_, ev) =>
            {
                var toggle = (ToggleEvent)ev;
                states.Add($"{toggle.OldState}>{toggle.NewState}:{toggle.IsCancelable}");
            });

            popover.ShowPopover();
            Assert.IsTrue(popover.IsPopoverOpen());
            popover.HidePopover();
            Assert.IsFalse(popover.IsPopoverOpen());

            CollectionAssert.AreEqual(new[]
            {
                "closed>open:True",
                "closed>open:False",
                "open>closed:False",
                "open>closed:False",
            }, states);
        }

        [Test]
        public void BeforeToggleCanCancelShowingPopover()
        {
            var popover = Html("<div popover></div>").QuerySelector<IHtmlElement>("div");
            popover.BeforeToggle += (_, ev) => ev.Cancel();

            popover.ShowPopover();

            Assert.IsFalse(popover.IsPopoverOpen());
        }

        [Test]
        public void ToggleEventDefaultsBothStatesToClosed()
        {
            var ev = new ToggleEvent("toggle");

            Assert.AreEqual("closed", ev.OldState);
            Assert.AreEqual("closed", ev.NewState);
        }

        [Test]
        public void AutoPopoverStackClosesUnrelatedPopoversAndKeepsAncestors()
        {
            var document = Html("<div id='outer' popover><div id='first' popover></div><div id='second' popover></div></div><div id='other' popover></div>");
            var outer = document.GetElementById("outer") as IHtmlElement;
            var first = document.GetElementById("first") as IHtmlElement;
            var second = document.GetElementById("second") as IHtmlElement;
            var other = document.GetElementById("other") as IHtmlElement;

            outer.ShowPopover();
            first.ShowPopover();
            second.ShowPopover();

            Assert.IsTrue(outer.IsPopoverOpen());
            Assert.IsFalse(first.IsPopoverOpen());
            Assert.IsTrue(second.IsPopoverOpen());

            other.ShowPopover();

            Assert.IsFalse(outer.IsPopoverOpen());
            Assert.IsFalse(second.IsPopoverOpen());
            Assert.IsTrue(other.IsPopoverOpen());
        }

        [Test]
        public void ManualPopoverIsNotClosedByAnotherAutoPopover()
        {
            var document = Html("<div id='manual' popover='manual'></div><div id='auto' popover></div>");
            var manual = document.GetElementById("manual") as IHtmlElement;
            var automatic = document.GetElementById("auto") as IHtmlElement;

            manual.ShowPopover();
            automatic.ShowPopover();

            Assert.IsTrue(manual.IsPopoverOpen());
            Assert.IsTrue(automatic.IsPopoverOpen());
        }

        [Test]
        public void PopoverOperationsRejectInvalidStates()
        {
            var document = Html("<div id='plain'></div><div id='popover' popover></div><div id='detached' popover></div>");
            var plain = document.GetElementById("plain") as IHtmlElement;
            var popover = document.GetElementById("popover") as IHtmlElement;
            var detached = document.GetElementById("detached") as IHtmlElement;
            detached.Parent.RemoveChild(detached);

            Assert.Throws<DomException>(() => plain.ShowPopover());
            Assert.Throws<DomException>(() => plain.HidePopover());
            Assert.Throws<DomException>(() => popover.HidePopover());
            Assert.Throws<DomException>(() => detached.ShowPopover());

            popover.ShowPopover();
            Assert.Throws<DomException>(() => popover.ShowPopover());
            Assert.IsTrue(popover.TogglePopover(true));
        }

        [Test]
        public void RemovingPopoverClosesIt()
        {
            var document = Html("<div id='container'><div id='popover' popover='manual'></div></div>");
            var container = document.GetElementById("container");
            var popover = document.GetElementById("popover") as IHtmlElement;

            popover.ShowPopover();
            container.RemoveChild(popover);

            Assert.IsFalse(popover.IsPopoverOpen());
        }

        [Test]
        public void RemovingPopoverAttributeClosesIt()
        {
            var popover = Html("<div popover='manual'></div>").QuerySelector<IHtmlElement>("div");
            popover.ShowPopover();

            popover.RemoveAttribute("popover");

            Assert.IsFalse(popover.IsPopoverOpen());
        }

        [Test]
        public void PopoverInvokerPropertiesReflectAttributes()
        {
            var document = Html("<button popovertarget='menu' popovertargetaction='show'></button><input id='menu' popover>");
            var button = document.QuerySelector<IHtmlButtonElement>("button");
            var menu = document.QuerySelector<IHtmlElement>("input");

            Assert.AreSame(menu, button.GetPopoverTargetElement());
            Assert.AreEqual("show", button.GetPopoverTargetAction());
            Assert.AreEqual("toggle", Html("<button popovertargetaction='invalid'></button>").QuerySelector<IHtmlButtonElement>("button").GetPopoverTargetAction());

            button.SetPopoverTargetAction("hide");
            Assert.AreEqual("hide", button.GetAttribute("popovertargetaction"));

            button.SetPopoverTargetElement(menu);
            Assert.AreSame(menu, button.GetPopoverTargetElement());

            var input = Html("<input>").QuerySelector<IHtmlInputElement>("input");
            input.SetPopoverTargetElement(menu);
            Assert.AreSame(menu, input.GetPopoverTargetElement());
            input.SetPopoverTargetAction("show");
            Assert.AreEqual("show", input.GetPopoverTargetAction());

            var unnumberedTarget = Html("<div popover></div>").QuerySelector<IHtmlElement>("div");
            input.SetPopoverTargetElement(unnumberedTarget);
            Assert.AreSame(unnumberedTarget, input.GetPopoverTargetElement());
        }
    }
}
