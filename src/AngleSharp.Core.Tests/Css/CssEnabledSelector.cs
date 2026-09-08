namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class CssEnabledSelectorTests
    {
        // https://html.spec.whatwg.org/multipage/semantics-other.html#selector-enabled
        [Test]
        public void LinksMatchNeitherEnabledNorDisabled(
            [Values("a", "area", "link")] String tagName,
            [Values(null, "", "/next")] String href,
            [Values(false, true)] Boolean disabled)
        {
            var document = $"<!doctype html><{tagName} id='target'></{tagName}>".ToHtmlDocument();
            var element = document.QuerySelector("#target");

            if (href != null)
            {
                element.SetAttribute("href", href);
            }

            if (disabled)
            {
                element.SetAttribute("disabled", "disabled");
            }

            Assert.IsFalse(element.IsEnabled());
            Assert.IsFalse(element.IsDisabled());
            Assert.IsFalse(element.Matches(":enabled"));
            Assert.IsFalse(element.Matches(":disabled"));
            Assert.IsNull(document.QuerySelector(":enabled"));
            Assert.IsNull(document.QuerySelector(":disabled"));
            Assert.AreEqual(0, document.QuerySelectorAll(":enabled").Length);
            Assert.AreEqual(0, document.QuerySelectorAll(":disabled").Length);
        }

        [Test]
        public void FormControlsKeepTheirEnabledAndDisabledStates(
            [Values("button", "input", "select", "textarea", "option", "optgroup", "fieldset")] String tagName,
            [Values(false, true)] Boolean disabled)
        {
            var document = $"<!doctype html><{tagName} id='target'></{tagName}>".ToHtmlDocument();
            var element = document.QuerySelector("#target");

            if (disabled)
            {
                element.SetAttribute("disabled", "disabled");
            }

            Assert.AreEqual(!disabled, element.IsEnabled());
            Assert.AreEqual(disabled, element.IsDisabled());
            Assert.AreEqual(!disabled, element.Matches(":enabled"));
            Assert.AreEqual(disabled, element.Matches(":disabled"));
            Assert.AreEqual(disabled ? 0 : 1, document.QuerySelectorAll(":enabled").Length);
            Assert.AreEqual(disabled ? 1 : 0, document.QuerySelectorAll(":disabled").Length);
        }

        [Test]
        public void EnabledQuerySkipsLinksBeforeFormControls()
        {
            var document = ("<!doctype html><head><link id='link' href='/style.css'></head><body>" +
                "<a id='anchor' href='/next'></a><map><area id='area' href='/next'></map>" +
                "<button id='button'></button><input id='input'>" +
                "<button id='disabledButton' disabled></button><input id='disabledInput' disabled>" +
                "<div id='other'></div></body>").ToHtmlDocument();

            Assert.AreEqual("button", document.QuerySelector(":enabled").Id);
            CollectionAssert.AreEqual(new[] { "button", "input" },
                document.QuerySelectorAll(":enabled").Select(element => element.Id));
            CollectionAssert.AreEqual(new[] { "disabledButton", "disabledInput" },
                document.QuerySelectorAll(":disabled").Select(element => element.Id));
        }
    }
}
