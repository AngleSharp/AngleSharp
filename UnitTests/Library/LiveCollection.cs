using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;

namespace UnitTests.Library
{
    [TestClass]
    public class LiveCollectionTests
    {
        [TestMethod]
        public void HtmlLiveCollectionUpdates()
        {
            var document = DocumentBuilder.Html("<ul><li>A<li>B<li>C<li>D</ul>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(1, body.ChildNodes.Length);

            var ul = body.ChildNodes[0];
            Assert.AreEqual("ul", ul.NodeName);
            Assert.AreEqual(NodeType.Element, ul.NodeType);

            var live = ((Element)ul).Children;
            Assert.AreEqual(4, live.Length);
            Assert.AreEqual("A", live[0].TextContent);
            Assert.AreEqual("B", live[1].TextContent);
            Assert.AreEqual("C", live[2].TextContent);
            Assert.AreEqual("D", live[3].TextContent);

            var newElement = document.CreateElement("li");
            newElement.TextContent = "E";
            ul.AppendChild(newElement);

            Assert.AreEqual(5, live.Length);
            Assert.AreEqual("E", live[4].TextContent);
        }

        [TestMethod]
        public void HtmlLiveCollectionCompleteDOMRebuildWithInnerHtml()
        {
            var document = DocumentBuilder.Html("<p><p><p><p><p>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(5, body.ChildNodes.Length);

            var live = body.Children;
            Assert.AreEqual(5, live.Length);

            foreach (var child in live)
            {
                Assert.AreEqual("p", child.NodeName);
                Assert.AreEqual(0, child.ChildNodes.Length);
                Assert.AreEqual(0, child.Attributes.Length);
                Assert.AreEqual(NodeType.Element, child.NodeType);
                Assert.AreEqual("", child.TextContent);
            }

            body.InnerHTML = "<p>First<p>Second<p>Third";
            Assert.AreEqual(3, body.ChildNodes.Length);
            Assert.AreEqual(3, live.Length);

            var i = 0;
            var str = new[] { "First", "Second", "Third" };

            foreach (var child in live)
            {
                Assert.AreEqual("p", child.NodeName);
                Assert.AreEqual(1, child.ChildNodes.Length);
                Assert.AreEqual(0, child.Attributes.Length);
                Assert.AreEqual(NodeType.Element, child.NodeType);
                Assert.AreEqual(str[i++], child.TextContent);
            }
        }

        [TestMethod]
        public void HtmlLiveCollectionCompleteDOMRebuildWithText()
        {
            var document = DocumentBuilder.Html("<p><p><p><p><p>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(5, body.ChildNodes.Length);

            var live = body.Children;
            Assert.AreEqual(5, live.Length);

            foreach (var child in live)
            {
                Assert.AreEqual("p", child.NodeName);
                Assert.AreEqual(0, child.ChildNodes.Length);
                Assert.AreEqual(0, child.Attributes.Length);
                Assert.AreEqual(NodeType.Element, child.NodeType);
                Assert.AreEqual("", child.TextContent);
            }

            body.InnerHTML = "This is pure text!";
            Assert.AreEqual(1, body.ChildNodes.Length);
            Assert.AreEqual(0, live.Length);

            body.InnerHTML = "<b>Proof that we still have live view</b>";
            Assert.AreEqual(1, body.ChildNodes.Length);
            Assert.AreEqual(1, live.Length);
        }

        [TestMethod]
        public void HtmlLiveCollectionWithAttr()
        {
            var document = DocumentBuilder.Html("<a name=first>some name</a><a name=second>more</a><div><a name=third>last</a><a id=change>not really an anchor</a></div>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(3, body.ChildNodes.Length);

            var live = document.Anchors;
            Assert.AreEqual(3, live.Length);

            foreach (var child in live)
            {
                Assert.AreEqual("a", child.NodeName);
                Assert.AreEqual(1, child.Attributes.Length);
                Assert.AreEqual(NodeType.Element, child.NodeType);
                Assert.IsNotNull(child.Attributes["name"]);
            }

            var a = document.QuerySelector("#change");
            Assert.IsNotNull(a);

            a.SetAttribute("name", "changed");
            Assert.AreEqual(4, live.Length);
        }

        [TestMethod]
        public void HtmlLiveCollectionMultiple()
        {
            var document = DocumentBuilder.Html("<embed></embed><div><object></object><applet></applet>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(2, body.ChildNodes.Length);

            var live = document.Embeds;
            Assert.AreEqual(3, live.Length);

            var div = document.QuerySelector("div");
            Assert.IsNotNull(div);

            var embed = document.CreateElement("embed");
            div.AppendChild(embed);

            Assert.AreEqual(4, live.Length);
        }

        [TestMethod]
        public void HtmlLiveCollectionMultipleWithAttr()
        {
            var document = DocumentBuilder.Html("<a href='http://127.0.0.1'></a><div class='container'><area href='#'>my area</area>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(2, body.ChildNodes.Length);

            var live = document.Links;
            Assert.AreEqual(2, live.Length);

            var div = document.QuerySelector("body > div.container");
            Assert.IsNotNull(div);

            var a = document.CreateElement("a");
            div.AppendChild(a);

            Assert.AreEqual(2, live.Length);

            a.SetAttribute("href", "http://localhost");
            Assert.AreEqual(3, live.Length);

            foreach (var element in live)
            {
                Assert.IsNotNull(element.Attributes["href"]);
                Assert.IsTrue(element.NodeName == "a" || element.NodeName == "area");
            }
        }
    }
}
