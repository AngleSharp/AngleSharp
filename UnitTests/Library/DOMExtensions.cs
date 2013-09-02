using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;

namespace UnitTests
{
    [TestClass]
    public class DOMExtensions
    {
        [TestMethod]
        public void ExtensionAttrWithEmptyList()
        {
            var document = DocumentBuilder.Html("");
            var elements = document.QuerySelectorAll("li").Attr("test", "test");
            Assert.AreEqual(0, elements.Length);
        }

        [TestMethod]
        public void ExtensionAttrWithOneElement()
        {
            var document = DocumentBuilder.Html("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Attr("test", "test");
            Assert.AreEqual(1, elements.Length);

            var attr = elements[0].Attributes;
            Assert.AreEqual(1, attr.Length);

            var test = attr[0];
            Assert.AreEqual("test", test.Name);
            Assert.AreEqual("test", test.Value);
        }

        [TestMethod]
        public void ExtensionAttrWithMultipleElements()
        {
            var document = DocumentBuilder.Html("<ul><li>First element<li>Second element<li>third<li class=bla>Last");
            var elements = document.QuerySelectorAll("li").Attr("test", "test");
            Assert.AreEqual(4, elements.Length);

            var attr1 = elements[0].Attributes;
            Assert.AreEqual(1, attr1.Length);

            var test1 = attr1[0];
            Assert.AreEqual("test", test1.Name);
            Assert.AreEqual("test", test1.Value);

            var attr2 = elements[1].Attributes;
            Assert.AreEqual(1, attr2.Length);

            var test2 = attr2[0];
            Assert.AreEqual("test", test2.Name);
            Assert.AreEqual("test", test2.Value);

            var attr3 = elements[2].Attributes;
            Assert.AreEqual(1, attr3.Length);

            var test3 = attr3[0];
            Assert.AreEqual("test", test3.Name);
            Assert.AreEqual("test", test3.Value);

            var attr4 = elements[3].Attributes;
            Assert.AreEqual(2, attr4.Length);

            var cls = attr4[0];
            Assert.AreEqual("class", cls.Name);
            Assert.AreEqual("bla", cls.Value);

            var test4 = attr4[1];
            Assert.AreEqual("test", test4.Name);
            Assert.AreEqual("test", test4.Value);
        }

        [TestMethod]
        public void ExtensionCssWithEmptyListAndEmptyDeclaration()
        {
            var document = DocumentBuilder.Html("");
            var elements = document.QuerySelectorAll("li").Css("");
            Assert.AreEqual(0, elements.Length);
        }

        [TestMethod]
        public void ExtensionCssWithEmptyListOnly()
        {
            var document = DocumentBuilder.Html("");
            var elements = document.QuerySelectorAll("li").Css("color:red");
            Assert.AreEqual(0, elements.Length);
        }

        [TestMethod]
        public void ExtensionCssWithOneElement()
        {
            var document = DocumentBuilder.Html("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Css("color:red");
            Assert.AreEqual(1, elements.Length);

            var style = elements[0].Style;
            Assert.AreEqual(1, style.Length);

            var prop = style[0];
            Assert.AreEqual("color", prop);
            Assert.AreEqual("red", style.GetPropertyValue(prop));
        }

        [TestMethod]
        public void ExtensionCssWithMultipleElements()
        {
            var document = DocumentBuilder.Html("<ul><li>First element<li>Second element<li>third<li style='background:blue'>Last");
            var elements = document.QuerySelectorAll("li").Css("color:red");
            Assert.AreEqual(4, elements.Length);

            var style1 = elements[0].Style;
            Assert.AreEqual(1, style1.Length);

            var test1 = style1[0];
            Assert.AreEqual("color", test1);
            Assert.AreEqual("red", style1.GetPropertyValue(test1));

            var style2 = elements[1].Style;
            Assert.AreEqual(1, style2.Length);

            var test2 = style2[0];
            Assert.AreEqual("color", test2);
            Assert.AreEqual("red", style2.GetPropertyValue(test2));

            var style3 = elements[2].Style;
            Assert.AreEqual(1, style3.Length);

            var test3 = style3[0];
            Assert.AreEqual("color", test3);
            Assert.AreEqual("red", style3.GetPropertyValue(test3));

            var style4 = elements[3].Style;
            Assert.AreEqual(2, style4.Length);

            var background = style4[0];
            Assert.AreEqual("background", background);
            Assert.AreEqual("blue", style4.GetPropertyValue(background));

            var color = style4[1];
            Assert.AreEqual("color", color);
            Assert.AreEqual("red", style4.GetPropertyValue(color));
        }
    }
}
