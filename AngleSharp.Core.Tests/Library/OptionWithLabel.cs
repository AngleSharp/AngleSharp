using AngleSharp.Dom.Html;
using NUnit.Framework;
using System;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class OptionWithLabelTests
    {
        static readonly String[] spaces = new[] { "\u0020", "\u0009", "\u000A", "\u000C", "\u000D" };

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

        [Test]
        public void OptionNoChildrenEmptyValue()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.SetAttribute("value", "");
            Assert.AreEqual("", option.Value);
        }

        [Test]
        public void OptionNoChildrenValue()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.SetAttribute("value", "value");
            Assert.AreEqual("value", option.Value);
        }

        [Test]
        public void OptionNoChildrenNamespacedValue()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.SetAttribute("http://www.example.com/", "value", "");
            Assert.AreEqual("", option.Value);
        }

        [Test]
        public void OptionSingleChildNoValue()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            Assert.AreEqual("child", option.Value);
        }

        [Test]
        public void OptionSingleChildEmptyValue()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.SetAttribute("value", "");
            Assert.AreEqual("", option.Value);
        }

        [Test]
        public void OptionSingleChildValue()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.SetAttribute("value", "value");
            Assert.AreEqual("value", option.Value);
        }

        [Test]
        public void OptionSingleChildNamespacedValue()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.SetAttribute("http://www.example.com/", "value", "value");
            Assert.AreEqual("child", option.Value);
        }

        [Test]
        public void OptionTwoChildrenNoValue()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.AppendChild(document.CreateTextNode(" node "));
            Assert.AreEqual("child node", option.Value);
        }

        [Test]
        public void OptionTwoChildrenEmptyValue()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.AppendChild(document.CreateTextNode(" node "));
            option.SetAttribute("value", "");
            Assert.AreEqual("", option.Value);
        }

        [Test]
        public void OptionTwoChildrenValue()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.AppendChild(document.CreateTextNode(" node "));
            option.SetAttribute("value", "value");
            Assert.AreEqual("value", option.Value);
        }

        [Test]
        public void OptionTwoChildrenNamespacedValue()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.AppendChild(document.CreateTextNode(" child "));
            option.AppendChild(document.CreateTextNode(" node "));
            option.SetAttribute("http://www.example.com/", "value", "value");
            Assert.AreEqual("child node", option.Value);
        }

        [Test]
        public void OptionInOptGroup()
        {
            var testcase = DocumentBuilder.Html(@"<select id=""test"" name=""entry""><optgroup label=""Main menu""><option value=""1"">Exit</option></optgroup></select>");
            var select = testcase.GetElementById("test") as IHtmlSelectElement;
            Assert.IsNotNull(select);
            var option = select.QuerySelector("option") as IHtmlOptionElement;
            Assert.IsNotNull(option);
            Assert.AreEqual("1", option.Value);
            var parent = option.ParentElement as IHtmlOptionsGroupElement;
            Assert.IsNotNull(parent);
            Assert.AreEqual("Main menu", parent.Label);
            var sameOption = select.Options[0] as IHtmlOptionElement;
            Assert.IsNotNull(sameOption);
            Assert.AreEqual(option, sameOption);
        }

        [Test]
        public void OptionsInOptGroupMixedWithOptionsNoInOptGroup()
        {
            var testcase = DocumentBuilder.Html(@"<select><optgroup><option>1</option><option>2</option></optgroup><option>3</option><optgroup><option>4</option><option>5</option></optgroup></select>");
            var select = testcase.QuerySelector("select") as IHtmlSelectElement;
            Assert.IsNotNull(select);
            var options = select.Options;
            Assert.IsNotNull(options);
            Assert.AreEqual(5, options.Length);
            Assert.AreEqual("1", options.GetOptionAt(0).Value);
            Assert.AreEqual("2", options.GetOptionAt(1).Value);
            Assert.AreEqual("3", options.GetOptionAt(2).Value);
            Assert.AreEqual("4", options.GetOptionAt(3).Value);
            Assert.AreEqual("5", options.GetOptionAt(4).Value);
        }

        [Test]
        public void OptionWithNonEmptyLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.SetAttribute("label", "label");
            option.TextContent = "text";
            Assert.AreEqual("text", option.Text);
        }

        [Test]
        public void OptionWithEmptyLabel()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.SetAttribute("label", "");
            option.TextContent = "text";
            Assert.AreEqual("text", option.Text);
        }

        [Test]
        public void OptionTextShouldLeaveNbspAlone()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.TextContent = "\u00a0text";
            Assert.AreEqual("\u00a0text", option.Text);
        }

        [Test]
        public void OptionTextShouldLeaveTrailingNbspAlone()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.TextContent = "text\u00a0";
            Assert.AreEqual("text\u00a0", option.Text);
        }

        [Test]
        public void OptionTextShouldLeaveASingleInternalNbspAlone()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.TextContent = "before\u00a0after";
            Assert.AreEqual("before\u00a0after", option.Text);
        }

        [Test]
        public void OptionTextShouldLeaveMultipleInternalNbspAlone()
        {
            var document = DocumentBuilder.Html("");
            var option = document.CreateElement<IHtmlOptionElement>();
            option.TextContent = "before\u00a0\u00a0after";
            Assert.AreEqual("before\u00a0\u00a0after", option.Text);
        }
    }
}
