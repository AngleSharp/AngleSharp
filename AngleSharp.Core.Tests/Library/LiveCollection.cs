namespace AngleSharp.Core.Tests.Library
{
    using System;
    using System.Linq;
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using NUnit.Framework;

    [TestFixture]
    public class LiveCollectionTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void HtmlLiveCollectionUpdates()
        {
            var document = Html("<ul><li>A<li>B<li>C<li>D</ul>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(1, body.ChildNodes.Length);

            var ul = body.ChildNodes[0];
            Assert.AreEqual("ul", ul.GetTagName());
            Assert.AreEqual(NodeType.Element, ul.NodeType);

            var live = ((Element)ul).Children;
            Assert.AreEqual(4, live.Length);
            Assert.AreEqual("A", live[0].TextContent);
            Assert.AreEqual("B", live[1].TextContent);
            Assert.AreEqual("C", live[2].TextContent);
            Assert.AreEqual("D", live[3].TextContent);

            var newElement = document.CreateElement(Tags.Li);
            newElement.TextContent = "E";
            ul.AppendChild(newElement);

            Assert.AreEqual(5, live.Length);
            Assert.AreEqual("E", live[4].TextContent);
        }

        [Test]
        public void HtmlLiveCollectionCompleteDOMRebuildWithInnerHtml()
        {
            var document = Html("<p><p><p><p><p>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(5, body.ChildNodes.Length);

            var live = body.Children;
            Assert.AreEqual(5, live.Length);

            foreach (var child in live)
            {
                Assert.AreEqual("p", child.GetTagName());
                Assert.AreEqual(0, child.ChildNodes.Length);
                Assert.AreEqual(0, child.Attributes.Count());
                Assert.AreEqual(NodeType.Element, child.NodeType);
                Assert.AreEqual("", child.TextContent);
            }

            body.InnerHtml = "<p>First<p>Second<p>Third";
            Assert.AreEqual(3, body.ChildNodes.Length);
            Assert.AreEqual(3, live.Length);

            var i = 0;
            var str = new[] { "First", "Second", "Third" };

            foreach (var child in live)
            {
                Assert.AreEqual("p", child.GetTagName());
                Assert.AreEqual(1, child.ChildNodes.Length);
                Assert.AreEqual(0, child.Attributes.Count());
                Assert.AreEqual(NodeType.Element, child.NodeType);
                Assert.AreEqual(str[i++], child.TextContent);
            }
        }

        [Test]
        public void HtmlLiveCollectionCompleteDOMRebuildWithText()
        {
            var document = Html("<p><p><p><p><p>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(5, body.ChildNodes.Length);

            var live = body.Children;
            Assert.AreEqual(5, live.Length);

            foreach (var child in live)
            {
                Assert.AreEqual("p", child.GetTagName());
                Assert.AreEqual(0, child.ChildNodes.Length);
                Assert.AreEqual(0, child.Attributes.Count());
                Assert.AreEqual(NodeType.Element, child.NodeType);
                Assert.AreEqual("", child.TextContent);
            }

            body.InnerHtml = "This is pure text!";
            Assert.AreEqual(1, body.ChildNodes.Length);
            Assert.AreEqual(0, live.Length);

            body.InnerHtml = "<b>Proof that we still have live view</b>";
            Assert.AreEqual(1, body.ChildNodes.Length);
            Assert.AreEqual(1, live.Length);
        }

        [Test]
        public void HtmlLiveCollectionWithAttr()
        {
            var document = Html("<a name=first>some name</a><a name=second>more</a><div><a name=third>last</a><a id=change>not really an anchor</a></div>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(3, body.ChildNodes.Length);

            var live = document.Anchors;
            Assert.AreEqual(3, live.Length);

            foreach (var child in live)
            {
                Assert.AreEqual("a", child.GetTagName());
                Assert.AreEqual(1, child.Attributes.Count());
                Assert.AreEqual(NodeType.Element, child.NodeType);
                Assert.IsNotNull(child.GetAttribute("name"));
            }

            var a = document.QuerySelector("#change");
            Assert.IsNotNull(a);

            a.SetAttribute("name", "changed");
            Assert.AreEqual(4, live.Length);
        }

        [Test]
        public void HtmlLiveCollectionMultiple()
        {
            var document = Html("<embed></embed><div><object></object><applet></applet>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(2, body.ChildNodes.Length);

            var live = document.Plugins;
            Assert.AreEqual(1, live.Length);

            var div = document.QuerySelector(Tags.Div);
            Assert.IsNotNull(div);

            var embed = document.CreateElement(Tags.Embed);
            div.AppendChild(embed);

            Assert.AreEqual(2, live.Length);
        }

        [Test]
        public void HtmlLiveCollectionMultipleWithAttr()
        {
            var document = Html("<a href='http://127.0.0.1'></a><div class='container'><area href='#'>my area</area>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(2, body.ChildNodes.Length);

            var live = document.Links;
            Assert.AreEqual(2, live.Length);

            var div = document.QuerySelector("body > div.container");
            Assert.IsNotNull(div);

            var a = document.CreateElement(Tags.A);
            div.AppendChild(a);

            Assert.AreEqual(2, live.Length);

            a.SetAttribute("href", "http://localhost");
            Assert.AreEqual(3, live.Length);

            foreach (var element in live)
            {
                Assert.IsNotNull(element.GetAttribute("href"));
                Assert.IsTrue(element.GetTagName() == "a" || element.GetTagName() == "area");
            }
        }
    }
}
