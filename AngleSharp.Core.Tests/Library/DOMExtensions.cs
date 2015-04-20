namespace AngleSharp.Core.Tests.Library
{
    using System;
    using System.Linq;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class DOMExtensionsTests
    {
        static IDocument Html(String code)
        {
            var config = new Configuration().WithCss();
            return code.ToHtmlDocument(config);
        }

        [Test]
        public void ExtensionAttrWithEmptyList()
        {
            var document = Html("");
            var elements = document.QuerySelectorAll("li").Attr("test", "test");
            Assert.AreEqual(0, elements.Count());
        }

        [Test]
        public void ExtensionBeforeWithSimpleElements()
        {
            var document = Html(@"<div class='container'>
  <h2>Greetings</h2>
  <div class='inner'>Hello</div>
  <div class='inner'>Goodbye</div>
</div>");
            var container = document.QuerySelector(".container");
            Assert.AreEqual(3, container.ChildElementCount);
            var inner = document.QuerySelectorAll(".inner");
            inner.Before("<p>Test</p>");
            Assert.AreEqual(5, container.ChildElementCount);
            Assert.AreEqual("p", inner[0].PreviousElementSibling.GetTagName());
            Assert.AreEqual("p", inner[1].PreviousElementSibling.GetTagName());
        }

        [Test]
        public void ExtensionAfterWithSimpleElements()
        {
            var document = Html(@"<div class='container'>
  <h2>Greetings</h2>
  <div class='inner'>Hello</div>
  <div class='inner'>Goodbye</div>
</div>");
            var container = document.QuerySelector(".container");
            Assert.AreEqual(3, container.ChildElementCount);
            var inner = document.QuerySelectorAll(".inner");
            inner.After("<p>Test</p>");
            Assert.AreEqual(5, container.ChildElementCount);
            Assert.AreEqual("p", inner[0].NextElementSibling.GetTagName());
            Assert.AreEqual("p", inner[1].NextElementSibling.GetTagName());
        }

        [Test]
        public void ExtensionAppendWithSimpleElements()
        {
            var document = Html(@"<div class='container'>
  <h2>Greetings</h2>
  <div class='inner'>Hello</div>
  <div class='inner'>Goodbye</div>
</div>");
            var container = document.QuerySelector(".container");
            Assert.AreEqual(3, container.ChildElementCount);
            var inner = document.QuerySelectorAll(".inner");
            Assert.AreEqual(0, inner[0].ChildElementCount);
            Assert.AreEqual(0, inner[1].ChildElementCount);
            Assert.AreEqual(1, inner[0].ChildNodes.Length);
            Assert.AreEqual(1, inner[1].ChildNodes.Length);
            inner.Append("<p>Test</p>");
            Assert.AreEqual(3, container.ChildElementCount);
            Assert.AreEqual(1, inner[0].ChildElementCount);
            Assert.AreEqual(1, inner[1].ChildElementCount);
            Assert.AreEqual(2, inner[0].ChildNodes.Length);
            Assert.AreEqual(2, inner[1].ChildNodes.Length);
            Assert.AreEqual("p", inner[0].ChildNodes[1].GetTagName());
            Assert.AreEqual("p", inner[1].ChildNodes[1].GetTagName());
        }

        [Test]
        public void ExtensionPrependWithSimpleElements()
        {
            var document = Html(@"<div class='container'>
  <h2>Greetings</h2>
  <div class='inner'>Hello</div>
  <div class='inner'>Goodbye</div>
</div>");
            var container = document.QuerySelector(".container");
            Assert.AreEqual(3, container.ChildElementCount);
            var inner = document.QuerySelectorAll(".inner");
            Assert.AreEqual(0, inner[0].ChildElementCount);
            Assert.AreEqual(0, inner[1].ChildElementCount);
            Assert.AreEqual(1, inner[0].ChildNodes.Length);
            Assert.AreEqual(1, inner[1].ChildNodes.Length);
            inner.Prepend("<p>Test</p>");
            Assert.AreEqual(3, container.ChildElementCount);
            Assert.AreEqual(1, inner[0].ChildElementCount);
            Assert.AreEqual(1, inner[1].ChildElementCount);
            Assert.AreEqual(2, inner[0].ChildNodes.Length);
            Assert.AreEqual(2, inner[1].ChildNodes.Length);
            Assert.AreEqual("p", inner[0].ChildNodes[0].GetTagName());
            Assert.AreEqual("p", inner[1].ChildNodes[0].GetTagName());
        }

        [Test]
        public void ExtensionWrapWithSimpleElements()
        {
            var document = Html(@"<div class='container'>
  <div class='inner'>Hello</div>
  <div class='inner'>Goodbye</div>
</div>");
            var container = document.QuerySelector(".container");
            Assert.AreEqual(2, container.ChildElementCount);
            var inner = document.QuerySelectorAll(".inner");
            inner.Wrap("<div class='new'></div>");
            Assert.AreEqual(2, container.ChildElementCount);
            Assert.AreEqual("div", container.Children[0].GetTagName());
            Assert.AreEqual("new", container.Children[0].ClassName);
            Assert.AreEqual("div", container.Children[1].GetTagName());
            Assert.AreEqual("new", container.Children[1].ClassName);
            Assert.AreEqual(1, container.Children[0].ChildElementCount);
            Assert.AreEqual("Hello", container.Children[0].FirstElementChild.TextContent);
            Assert.AreEqual(1, container.Children[1].ChildElementCount);
            Assert.AreEqual("Goodbye", container.Children[1].FirstElementChild.TextContent);
        }

        [Test]
        public void ExtensionWrapWithSimpleText()
        {
            var document = Html(@"<p>Hello</p>
<p>cruel</p>
<p>World</p>");
            var body = document.Body;
            Assert.AreEqual(3, body.ChildElementCount);
            var p = document.QuerySelectorAll("p");
            Assert.AreEqual("p", body.Children[0].GetTagName());
            Assert.AreEqual("p", body.Children[1].GetTagName());
            Assert.AreEqual("p", body.Children[2].GetTagName());
            p.Wrap("<div></div>");
            Assert.AreEqual(3, body.ChildElementCount);
            Assert.AreEqual("div", body.Children[0].GetTagName());
            Assert.AreEqual("div", body.Children[1].GetTagName());
            Assert.AreEqual("div", body.Children[2].GetTagName());
            Assert.AreEqual(1, body.Children[0].ChildElementCount);
            Assert.AreEqual(1, body.Children[1].ChildElementCount);
            Assert.AreEqual(1, body.Children[2].ChildElementCount);
            Assert.AreEqual("Hello", body.Children[0].FirstElementChild.TextContent);
            Assert.AreEqual("cruel", body.Children[1].FirstElementChild.TextContent);
            Assert.AreEqual("World", body.Children[2].FirstElementChild.TextContent);
        }

        [Test]
        public void ExtensionWrapWithComplexElements()
        {
            var document = Html(@"<span>Span Text</span>
<strong>What about me?</strong>
<span>Another One</span>");
            var body = document.Body;
            Assert.AreEqual(3, body.ChildElementCount);
            var span = document.QuerySelectorAll("span");
            Assert.AreEqual("span", body.Children[0].GetTagName());
            Assert.AreEqual("strong", body.Children[1].GetTagName());
            Assert.AreEqual("span", body.Children[2].GetTagName());
            span.Wrap("<div><div><p><em><b></b></em></p></div></div>");
            Assert.AreEqual(3, body.ChildElementCount);
            Assert.AreEqual("div", body.Children[0].GetTagName());
            Assert.AreEqual("strong", body.Children[1].GetTagName());
            Assert.AreEqual("div", body.Children[2].GetTagName());
            Assert.AreEqual(1, body.Children[0].ChildElementCount);
            Assert.AreEqual(0, body.Children[1].ChildElementCount);
            Assert.AreEqual(1, body.Children[2].ChildElementCount);
            var bold = document.QuerySelectorAll("b");
            Assert.AreEqual(2, bold.Length);
            Assert.AreEqual(1, bold[0].ChildElementCount);
            Assert.AreEqual(1, bold[1].ChildElementCount);
            Assert.AreEqual("span", bold[0].FirstElementChild.GetTagName());
            Assert.AreEqual("span", bold[1].FirstElementChild.GetTagName());
            Assert.AreEqual("Span Text", bold[0].FirstElementChild.TextContent);
            Assert.AreEqual("Another One", bold[1].FirstElementChild.TextContent);
        }

        [Test]
        public void ExtensionWrapInnerWithSimpleElements()
        {
            var document = Html(@"<div class='container'>
  <div class='inner'>Hello</div>
  <div class='inner'>Goodbye</div>
</div>");
            var container = document.QuerySelector(".container");
            Assert.AreEqual(2, container.ChildElementCount);
            var inner = document.QuerySelectorAll(".inner");
            inner.WrapInner("<div class='new'></div>");
            Assert.AreEqual(2, container.ChildElementCount);
            Assert.AreEqual("div", container.Children[0].GetTagName());
            Assert.AreEqual("inner", container.Children[0].ClassName);
            Assert.AreEqual(1, container.Children[0].ChildElementCount);
            Assert.AreEqual("div", container.Children[0].FirstElementChild.GetTagName());
            Assert.AreEqual("new", container.Children[0].FirstElementChild.ClassName);
            Assert.AreEqual("Hello", container.Children[0].FirstElementChild.TextContent);
            Assert.AreEqual("div", container.Children[1].GetTagName());
            Assert.AreEqual("inner", container.Children[1].ClassName);
            Assert.AreEqual(1, container.Children[1].ChildElementCount);
            Assert.AreEqual("div", container.Children[1].FirstElementChild.GetTagName());
            Assert.AreEqual("new", container.Children[1].FirstElementChild.ClassName);
            Assert.AreEqual("Goodbye", container.Children[1].FirstElementChild.TextContent);
        }

        [Test]
        public void ExtensionWrapInnerWithSimpleText()
        {
            var document = Html(@"<p>Hello</p>
<p>cruel</p>
<p>World</p>");
            var body = document.Body;
            Assert.AreEqual(3, body.ChildElementCount);
            var p = document.QuerySelectorAll("p");
            Assert.AreEqual("p", body.Children[0].GetTagName());
            Assert.AreEqual("p", body.Children[1].GetTagName());
            Assert.AreEqual("p", body.Children[2].GetTagName());
            p.WrapInner("<b></b>");
            Assert.AreEqual(3, body.ChildElementCount);

            Assert.AreEqual("p", body.Children[0].GetTagName());
            Assert.AreEqual(1, body.Children[0].ChildElementCount);
            Assert.AreEqual("p", body.Children[1].GetTagName());
            Assert.AreEqual(1, body.Children[1].ChildElementCount);
            Assert.AreEqual("p", body.Children[2].GetTagName());
            Assert.AreEqual(1, body.Children[2].ChildElementCount);

            Assert.AreEqual("b", body.Children[0].FirstElementChild.GetTagName());
            Assert.AreEqual(0, body.Children[0].FirstElementChild.ChildElementCount);
            Assert.AreEqual("Hello", body.Children[0].FirstElementChild.TextContent);
            Assert.AreEqual("b", body.Children[1].FirstElementChild.GetTagName());
            Assert.AreEqual(0, body.Children[1].FirstElementChild.ChildElementCount);
            Assert.AreEqual("cruel", body.Children[1].FirstElementChild.TextContent);
            Assert.AreEqual("b", body.Children[2].FirstElementChild.GetTagName());
            Assert.AreEqual(0, body.Children[2].FirstElementChild.ChildElementCount);
            Assert.AreEqual("World", body.Children[2].FirstElementChild.TextContent);
        }

        [Test]
        public void ExtensionWrapAllWithSimpleElements()
        {
            var document = Html(@"<div class='container'>
  <div class='inner'>Hello</div>
  <div class='inner'>Goodbye</div>
</div>");
            var container = document.QuerySelector(".container");
            Assert.AreEqual(2, container.ChildElementCount);
            var inner = document.QuerySelectorAll(".inner");
            inner.WrapAll("<div class='new' />");
            Assert.AreEqual(1, container.ChildElementCount);
            Assert.AreEqual("div", container.FirstElementChild.GetTagName());
            Assert.AreEqual("new", container.FirstElementChild.ClassName);
            Assert.AreEqual(2, container.FirstElementChild.ChildElementCount);
            Assert.AreEqual("Hello", container.FirstElementChild.Children[0].TextContent);
            Assert.AreEqual("Goodbye", container.FirstElementChild.Children[1].TextContent);
        }

        [Test]
        public void ExtensionWrapAllWithComplexElements()
        {
            var document = Html(@"<span>Span Text</span>
<strong>What about me?</strong>
<span>Another One</span>");
            Assert.AreEqual(3, document.Body.ChildElementCount);
            var span = document.QuerySelectorAll("span");
            span.WrapAll("<div><div><p><em><b></b></em></p></div></div>");
            Assert.AreEqual(2, document.Body.ChildElementCount);
            Assert.AreEqual("div", document.Body.FirstElementChild.GetTagName());
            var bold = document.QuerySelector("b");
            Assert.IsNotNull(bold);
            Assert.AreEqual(2, bold.ChildElementCount);
            Assert.AreEqual("Span Text", bold.Children[0].TextContent);
            Assert.AreEqual("Another One", bold.Children[1].TextContent);
        }

        [Test]
        public void ExtensionAttrWithOneElement()
        {
            var document = Html("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Attr("test", "test");
            Assert.AreEqual(1, elements.Count());

            var attr = elements[0].Attributes;
            Assert.AreEqual(1, attr.Count());

            var test = attr.First();
            Assert.AreEqual("test", test.Name);
            Assert.AreEqual("test", test.Value);
        }

        [Test]
        public void ExtensionAttrWithOneElementButMultipleAttributes()
        {
            var document = Html("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Attr(new
            {
                test1 = "test",
                test2 = "test",
                test3 = string.Empty,
                test4 = 9,
                test5 = true
            });
            Assert.AreEqual(1, elements.Count());

            var attr = elements[0].Attributes;
            Assert.AreEqual(5, attr.Count());

            var element = elements[0];
            Assert.AreEqual("test", element.GetAttribute("test1"));
            Assert.AreEqual("test", element.GetAttribute("test2"));
            Assert.AreEqual("", element.GetAttribute("test3"));
            Assert.AreEqual("9", element.GetAttribute("test4"));
            Assert.AreEqual("True", element.GetAttribute("test5"));
        }

        [Test]
        public void ExtensionAttrWithMultipleElements()
        {
            var document = Html("<ul><li>First element<li>Second element<li>third<li class=bla>Last");
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

        [Test]
        public void ExtensionCssWithEmptyListAndEmptyDeclaration()
        {
            var document = Html("");
            var elements = document.QuerySelectorAll("li").Css(new { });
            Assert.AreEqual(0, elements.Count());
        }

        [Test]
        public void ExtensionCssWithEmptyListOnly()
        {
            var document = Html("");
            var elements = document.QuerySelectorAll("li").Css("color", "red");
            Assert.AreEqual(0, elements.Count());
        }

        [Test]
        public void ExtensionCssWithOneElement()
        {
            var document = Html("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Css("color", "red");
            Assert.AreEqual(1, elements.Count());

            var style = (elements[0] as IHtmlElement).Style;
            Assert.AreEqual(1, style.Count());

            Assert.AreEqual("color", style[0]);
            Assert.AreEqual("red", style.Color);
        }

        [Test]
        public void ExtensionCssWithOneElementButMultipleCssRules()
        {
            var document = Html("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Css(new
            {
                color = "red",
                background = "green",
                font = "10px 'Tahoma'",
                opacity = "0.5"
            });
            Assert.AreEqual(1, elements.Count());

            var style = (elements[0] as IHtmlElement).Style;

            Assert.AreEqual("red", style.Color);
            Assert.AreEqual("green", style.BackgroundColor);
            Assert.AreEqual("\"Tahoma\"", style.FontFamily);
            Assert.AreEqual("10px", style.FontSize);
            Assert.AreEqual("0.5", style.Opacity);
        }

        [Test]
        public void ExtensionCssWithMultipleElements()
        {
            var document = Html("<ul><li>First element<li>Second element<li>third<li style='background-color:blue'>Last");
            var elements = document.QuerySelectorAll("li").Css("color", "red");
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
            Assert.AreEqual("background-color", background);
            Assert.AreEqual("blue", style4.GetPropertyValue(background));

            var color = style4[1];
            Assert.AreEqual("color", color);
            Assert.AreEqual("red", style4.GetPropertyValue(color));
        }

        [Test]
        public void ExtensionTextWithEmptyList()
        {
            var document = Html("");
            var elements = document.QuerySelectorAll("li").Text("test");
            Assert.AreEqual(0, elements.Count());
        }

        [Test]
        public void ExtensionTextWithOneElement()
        {
            var document = Html("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Text("test");
            Assert.AreEqual(1, elements.Count());

            var text = elements[0].TextContent;
            Assert.AreEqual(1, elements[0].ChildNodes.Length);
            Assert.AreEqual("test", text);
        }

        [Test]
        public void ExtensionTextWithMultipleElements()
        {
            var document = Html("<ul><li>First element<li>Second element<li>third<li class=bla>Last");
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

        [Test]
        public void ExtensionHtmlWithEmptyList()
        {
            var document = Html("");
            var elements = document.QuerySelectorAll("li").Html("<p>Some paragraph</p>");
            Assert.AreEqual(0, elements.Count());
        }

        [Test]
        public void ExtensionHtmlWithOneElement()
        {
            var document = Html("<ul><li>First element");
            var elements = document.QuerySelectorAll("li").Html("<b><i>Text</i></b>");
            Assert.AreEqual(1, elements.Count());

            var childs = elements[0].ChildNodes;
            Assert.AreEqual(1, childs.Length);

            var bold = childs[0];
            Assert.AreEqual(NodeType.Element, bold.NodeType);
            Assert.AreEqual("b", bold.GetTagName());
            Assert.AreEqual(1, bold.ChildNodes.Length);

            var italic = bold.ChildNodes[0];
            Assert.AreEqual(NodeType.Element, italic.NodeType);
            Assert.AreEqual("i", italic.GetTagName());
            Assert.AreEqual(1, italic.ChildNodes.Length);

            var text = italic.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("Text", text.TextContent);
        }

        [Test]
        public void ExtensionHtmlWithMultipleElements()
        {
            var document = Html("<ul><li>First element<li>Second element<li>third<li class=bla>Last");
            var elements = document.QuerySelectorAll("li").Html("<b><i>Text</i></b>");
            Assert.AreEqual(4, elements.Count());

            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(1, elements[i].ChildNodes.Length);

                var bold = elements[i].ChildNodes[0];
                Assert.AreEqual(NodeType.Element, bold.NodeType);
                Assert.AreEqual("b", bold.GetTagName());
                Assert.AreEqual(1, bold.ChildNodes.Length);

                var italic = bold.ChildNodes[0];
                Assert.AreEqual(NodeType.Element, italic.NodeType);
                Assert.AreEqual("i", italic.GetTagName());
                Assert.AreEqual(1, italic.ChildNodes.Length);

                var text = italic.ChildNodes[0];
                Assert.AreEqual(NodeType.Text, text.NodeType);
                Assert.AreEqual("Text", text.TextContent);
            }
        }

        [Test]
        public void ExtensionHtmlWithMultipleNestedElements()
        {
            var document = Html("<ul><li>First element</li><li>Second element</li><li>third</li><li class=bla><ul><li>First nested</li><li>Second nested</li><li><ul><li>Last nesting level</li></ul></li></ul></li>");
            var elements = document.QuerySelectorAll("li").Html("<b><i>Text</i></b>");
            Assert.AreEqual(8, elements.Count());

            for (int i = 0; i < elements.Count(); i++)
            {
                Assert.AreEqual(1, elements[i].ChildNodes.Length);

                var bold = elements[i].ChildNodes[0];
                Assert.AreEqual(NodeType.Element, bold.NodeType);
                Assert.AreEqual("b", bold.GetTagName());
                Assert.AreEqual(1, bold.ChildNodes.Length);

                var italic = bold.ChildNodes[0];
                Assert.AreEqual(NodeType.Element, italic.NodeType);
                Assert.AreEqual("i", italic.GetTagName());
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
                Assert.AreEqual("b", bold.GetTagName());
                Assert.AreEqual(1, bold.ChildNodes.Length);

                var italic = bold.ChildNodes[0];
                Assert.AreEqual(NodeType.Element, italic.NodeType);
                Assert.AreEqual("i", italic.GetTagName());
                Assert.AreEqual(1, italic.ChildNodes.Length);

                var text = italic.ChildNodes[0];
                Assert.AreEqual(NodeType.Text, text.NodeType);
                Assert.AreEqual("Text", text.TextContent);
            }
        }
    }
}
