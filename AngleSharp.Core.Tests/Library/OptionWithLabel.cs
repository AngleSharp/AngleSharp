using AngleSharp.Dom.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class OptionWithLabelTests
    {
        [Test]
        public void OptionNoChildrenEmptyLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.SetAttribute("label", "");
            Assert.AreEqual("", option.Label);
        }

        [Test]
        public void OptionNoChildrenLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.SetAttribute("label", "label");
            Assert.AreEqual("label", option.Label);
        }

        [Test]
        public void OptionNoChildrenNamespacedLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.SetAttribute("http://www.example.com/", "label", "");
            Assert.AreEqual("", option.Label);
        }

        [Test]
        public void OptionSingleChildNoLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            Assert.AreEqual("child", option.Label);
        }

        [Test]
        public void OptionSingleChildEmptyLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.SetAttribute("label", "");
            Assert.AreEqual("", option.Label);
        }

        [Test]
        public void OptionSingleChildLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.SetAttribute("label", "label");
            Assert.AreEqual("label", option.Label);
        }

        [Test]
        public void OptionSingleChildNamespacedLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.SetAttribute("http://www.example.com/", "label", "label");
            Assert.AreEqual("child", option.Label);
        }

        [Test]
        public void OptionTwoChildrenNoLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.AppendChild(document.CreateTextNode(" node "));
            Assert.AreEqual("child node", option.Label);
        }

        [Test]
        public void OptionTwoChildrenEmptyLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.AppendChild(document.CreateTextNode(" node "));
            option.SetAttribute("label", "");
            Assert.AreEqual("", option.Label);
        }

        [Test]
        public void OptionTwoChildrenLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.AppendChild(document.CreateTextNode(" node "));
            option.SetAttribute("label", "label");
            Assert.AreEqual("label", option.Label);
        }

        [Test]
        public void OptionTwoChildrenNamespacedLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.AppendChild(document.CreateTextNode(" node "));
            option.SetAttribute("http://www.example.com/", "label", "label");
            Assert.AreEqual("child node", option.Label);
        }
    }
}
