using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;

namespace UnitTests
{
    [TestClass]
    public class DOMExtensionsTests
    {
        [TestMethod]
        public void ExtensionAttrWithEmptyList()
        {
            var document = DocumentBuilder.Html("");
            var elements = document.QuerySelectorAll("li").Attr("test", "test");
            Assert.AreEqual(0, elements.Count());
        }

        [TestMethod]
        public void ExtensionAttrWithOneElement()
        {
            var document = DocumentBuilder.Html("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Attr("test", "test");
            Assert.AreEqual(1, elements.Count());

            var attr = elements[0].Attributes;
            Assert.AreEqual(1, attr.Count());

            var test = attr.First();
            Assert.AreEqual("test", test.Name);
            Assert.AreEqual("test", test.Value);
        }

        [TestMethod]
        public void ExtensionAttrWithMultipleElements()
        {
            var document = DocumentBuilder.Html("<ul><li>First element<li>Second element<li>third<li class=bla>Last");
            var elements = document.QuerySelectorAll("li").Attr("test", "test");
            Assert.AreEqual(4, elements.Count());

            var attr1 = elements[0].Attributes;
            Assert.AreEqual(1, attr1.Count());

            var test1 = attr1.First();
            Assert.AreEqual("test", test1.Name);
            Assert.AreEqual("test", test1.Value);

            var attr2 = elements[1].Attributes;
            Assert.AreEqual(1, attr2.Count());

            var test2 = attr2.First();
            Assert.AreEqual("test", test2.Name);
            Assert.AreEqual("test", test2.Value);

            var attr3 = elements[2].Attributes;
            Assert.AreEqual(1, attr3.Count());

            var test3 = attr3.First();
            Assert.AreEqual("test", test3.Name);
            Assert.AreEqual("test", test3.Value);

            var attr4 = elements[3].Attributes;
            Assert.AreEqual(2, attr4.Count());

            var cls = attr4.First();
            Assert.AreEqual("class", cls.Name);
            Assert.AreEqual("bla", cls.Value);

            var test4 = attr4.Skip(1).First();
            Assert.AreEqual("test", test4.Name);
            Assert.AreEqual("test", test4.Value);
        }

        [TestMethod]
        public void ExtensionCssWithEmptyListAndEmptyDeclaration()
        {
            var document = DocumentBuilder.Html("");
            var elements = document.QuerySelectorAll("li").Css("");
            Assert.AreEqual(0, elements.Count());
        }

        [TestMethod]
        public void ExtensionCssWithEmptyListOnly()
        {
            var document = DocumentBuilder.Html("");
            var elements = document.QuerySelectorAll("li").Css("color:red");
            Assert.AreEqual(0, elements.Count());
        }

        [TestMethod]
        public void ExtensionCssWithOneElement()
        {
            var document = DocumentBuilder.Html("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Css("color:red");
            Assert.AreEqual(1, elements.Count());

            var style = (elements[0] as IHtmlElement).Style;
            Assert.AreEqual(1, style.Count());

            var prop = style[0];
            Assert.AreEqual("color", prop);
            Assert.AreEqual("red", style.GetPropertyValue(prop));
        }

        [TestMethod]
        public void ExtensionCssWithMultipleElements()
        {
            var document = DocumentBuilder.Html("<ul><li>First element<li>Second element<li>third<li style='background:blue'>Last");
            var elements = document.QuerySelectorAll("li").Css("color:red");
            Assert.AreEqual(4, elements.Count());

            var style1 = (elements[0] as IHtmlElement).Style;
            Assert.AreEqual(1, style1.Count());

            var test1 = style1[0];
            Assert.AreEqual("color", test1);
            Assert.AreEqual("red", style1.GetPropertyValue(test1));

            var style2 = (elements[1] as IHtmlElement).Style;
            Assert.AreEqual(1, style2.Count());

            var test2 = style2[0];
            Assert.AreEqual("color", test2);
            Assert.AreEqual("red", style2.GetPropertyValue(test2));

            var style3 = (elements[2] as IHtmlElement).Style;
            Assert.AreEqual(1, style3.Count());

            var test3 = style3[0];
            Assert.AreEqual("color", test3);
            Assert.AreEqual("red", style3.GetPropertyValue(test3));

            var style4 = (elements[3] as IHtmlElement).Style;
            Assert.AreEqual(2, style4.Count());

            var background = style4[0];
            Assert.AreEqual("background", background);
            Assert.AreEqual("blue", style4.GetPropertyValue(background));

            var color = style4[1];
            Assert.AreEqual("color", color);
            Assert.AreEqual("red", style4.GetPropertyValue(color));
        }

        [TestMethod]
        public void ExtensionTextWithEmptyList()
        {
            var document = DocumentBuilder.Html("");
            var elements = document.QuerySelectorAll("li").Text("test");
            Assert.AreEqual(0, elements.Count());
        }

        [TestMethod]
        public void ExtensionTextWithOneElement()
        {
            var document = DocumentBuilder.Html("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Text("test");
            Assert.AreEqual(1, elements.Count());

            var text = elements[0].TextContent;
            Assert.AreEqual(1, elements[0].ChildNodes.Length);
            Assert.AreEqual("test", text);
        }

        [TestMethod]
        public void ExtensionTextWithMultipleElements()
        {
            var document = DocumentBuilder.Html("<ul><li>First element<li>Second element<li>third<li class=bla>Last");
            var elements = document.QuerySelectorAll("li").Text("test");
            Assert.AreEqual(4, elements.Count());

            var text1 = elements[0].ChildNodes;
            Assert.AreEqual(1, text1.Length);

            var test1 = text1[0];
            Assert.AreEqual("test", test1.TextContent);

            var text2 = elements[1].ChildNodes;
            Assert.AreEqual(1, text2.Length);

            var test2 = text2[0];
            Assert.AreEqual("test", test2.TextContent);

            var text3 = elements[2].ChildNodes;
            Assert.AreEqual(1, text3.Length);

            var test3 = text3[0];
            Assert.AreEqual("test", test3.TextContent);

            var text4 = elements[3].ChildNodes;
            Assert.AreEqual(1, text4.Length);

            var test4 = text4[0];
            Assert.AreEqual("test", test4.TextContent);
        }

        [TestMethod]
        public void ExtensionHtmlWithEmptyList()
        {
            var document = DocumentBuilder.Html("");
            var elements = document.QuerySelectorAll("li").Html("<p>Some paragraph</p>");
            Assert.AreEqual(0, elements.Count());
        }

        [TestMethod]
        public void ExtensionHtmlWithOneElement()
        {
            var document = DocumentBuilder.Html("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Html("<b><i>Text</i></b>");
            Assert.AreEqual(1, elements.Count());

            var childs = elements[0].ChildNodes;
            Assert.AreEqual(1, childs.Length);

            var bold = childs[0];
            Assert.AreEqual(NodeType.Element, bold.NodeType);
            Assert.AreEqual("b", bold.NodeName);
            Assert.AreEqual(1, bold.ChildNodes.Length);

            var italic = bold.ChildNodes[0];
            Assert.AreEqual(NodeType.Element, italic.NodeType);
            Assert.AreEqual("i", italic.NodeName);
            Assert.AreEqual(1, italic.ChildNodes.Length);

            var text = italic.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("Text", text.TextContent);
        }

        [TestMethod]
        public void ExtensionHtmlWithMultipleElements()
        {
            var document = DocumentBuilder.Html("<ul><li>First element<li>Second element<li>third<li class=bla>Last");
            var elements = document.QuerySelectorAll("li").Html("<b><i>Text</i></b>");
            Assert.AreEqual(4, elements.Count());

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(1, elements[i].ChildNodes.Length);

                var bold = elements[i].ChildNodes[0];
                Assert.AreEqual(NodeType.Element, bold.NodeType);
                Assert.AreEqual("b", bold.NodeName);
                Assert.AreEqual(1, bold.ChildNodes.Length);

                var italic = bold.ChildNodes[0];
                Assert.AreEqual(NodeType.Element, italic.NodeType);
                Assert.AreEqual("i", italic.NodeName);
                Assert.AreEqual(1, italic.ChildNodes.Length);

                var text = italic.ChildNodes[0];
                Assert.AreEqual(NodeType.Text, text.NodeType);
                Assert.AreEqual("Text", text.TextContent);
            }
        }

        [TestMethod]
        public void ExtensionHtmlWithMultipleNestedElements()
        {
            var document = DocumentBuilder.Html("<ul><li>First element</li><li>Second element</li><li>third</li><li class=bla><ul><li>First nested</li><li>Second nested</li><li><ul><li>Last nesting level</li></ul></li></ul></li>");
            var elements = document.QuerySelectorAll("li").Html("<b><i>Text</i></b>");
            Assert.AreEqual(8, elements.Count());

            for (int i = 0; i < elements.Count(); i++)
            {
                Assert.AreEqual(1, elements[i].ChildNodes.Length);

                var bold = elements[i].ChildNodes[0];
                Assert.AreEqual(NodeType.Element, bold.NodeType);
                Assert.AreEqual("b", bold.NodeName);
                Assert.AreEqual(1, bold.ChildNodes.Length);

                var italic = bold.ChildNodes[0];
                Assert.AreEqual(NodeType.Element, italic.NodeType);
                Assert.AreEqual("i", italic.NodeName);
                Assert.AreEqual(1, italic.ChildNodes.Length);

                var text = italic.ChildNodes[0];
                Assert.AreEqual(NodeType.Text, text.NodeType);
                Assert.AreEqual("Text", text.TextContent);
            }

            var elementsInDocument = document.QuerySelectorAll("li");
            Assert.AreEqual(4, elementsInDocument.Count());

            for (int i = 0; i < elements.Count(); i++)
            {
                Assert.AreEqual(1, elements[i].ChildNodes.Length);

                var bold = elements[i].ChildNodes[0];
                Assert.AreEqual(NodeType.Element, bold.NodeType);
                Assert.AreEqual("b", bold.NodeName);
                Assert.AreEqual(1, bold.ChildNodes.Length);

                var italic = bold.ChildNodes[0];
                Assert.AreEqual(NodeType.Element, italic.NodeType);
                Assert.AreEqual("i", italic.NodeName);
                Assert.AreEqual(1, italic.ChildNodes.Length);

                var text = italic.ChildNodes[0];
                Assert.AreEqual(NodeType.Text, text.NodeType);
                Assert.AreEqual("Text", text.TextContent);
            }
        }
    }
}
