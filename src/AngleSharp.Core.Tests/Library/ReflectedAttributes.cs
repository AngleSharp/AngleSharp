namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;

    [TestFixture]
    public class ReflectedAttributeTests
    {
        [Test]
        public void MissingIdAndClassNameReflectAsEmptyString()
        {
            var document = "<div></div>".ToHtmlDocument();
            var div = document.QuerySelector("div");

            Assert.IsFalse(div.HasAttribute("id"));
            Assert.IsFalse(div.HasAttribute("class"));
            Assert.AreEqual("", div.Id);
            Assert.AreEqual("", div.ClassName);
        }

        [Test]
        public void MissingSlotReflectsAsEmptyString()
        {
            var document = "<div></div>".ToHtmlDocument();
            var div = document.QuerySelector("div");

            Assert.IsFalse(div.HasAttribute("slot"));
            Assert.AreEqual("", div.Slot);
        }

        [Test]
        public void MissingTitleAndDirectionReflectAsEmptyString()
        {
            var document = "<div></div>".ToHtmlDocument();
            var div = (IHtmlElement)document.QuerySelector("div");

            Assert.IsFalse(div.HasAttribute("title"));
            Assert.IsFalse(div.HasAttribute("dir"));
            Assert.AreEqual("", div.Title);
            Assert.AreEqual("", div.Direction);
        }

        [Test]
        public void MissingSlotNameReflectsAsEmptyString()
        {
            var document = "<slot></slot>".ToHtmlDocument();
            var slot = (IHtmlSlotElement)document.QuerySelector("slot");

            Assert.IsFalse(slot.HasAttribute("name"));
            Assert.AreEqual("", slot.Name);
        }

        [Test]
        public void PresentAttributesAreStillReflected()
        {
            var document = "<div id=foo class=bar slot=baz title=qux dir=rtl></div>".ToHtmlDocument();
            var div = (IHtmlElement)document.QuerySelector("div");

            Assert.AreEqual("foo", div.Id);
            Assert.AreEqual("bar", div.ClassName);
            Assert.AreEqual("baz", div.Slot);
            Assert.AreEqual("qux", div.Title);
            Assert.AreEqual("rtl", div.Direction);
        }

        [Test]
        public void AnEmptyAttributeIsNotConfusedWithAMissingOne()
        {
            var document = "<div id=\"\"></div>".ToHtmlDocument();
            var div = document.QuerySelector("div");

            Assert.IsTrue(div.HasAttribute("id"));
            Assert.AreEqual("", div.Id);
        }

        [Test]
        public void SettingTheReflectedValueSetsTheAttribute()
        {
            var document = "<div></div>".ToHtmlDocument();
            var div = document.QuerySelector("div");

            div.Id = "foo";

            Assert.IsTrue(div.HasAttribute("id"));
            Assert.AreEqual("foo", div.GetAttribute("id"));
            Assert.AreEqual("foo", div.Id);

            div.Id = "";

            Assert.IsTrue(div.HasAttribute("id"));
            Assert.AreEqual("", div.Id);
        }
    }
}
