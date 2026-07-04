namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class NodeIteratorTests
    {
        [Test]
        public void NodeIteratorJavaScriptKitDivision()
        {
            var source = @"<div id=contentarea>
<p>Some <span>text</span></p>
<b>Bold text</b>
</div>";
            var doc = source.ToHtmlDocument();
            var rootnode = doc.GetElementById("contentarea");
            var iterator = doc.CreateNodeIterator(rootnode, FilterSettings.Element);

            Assert.AreEqual(rootnode, iterator.Root);
            Assert.IsTrue(iterator.IsBeforeReference);

            var results = new List<INode>();

            while (iterator.Next() != null)
            {
                results.Add(iterator.Reference);
            }

            Assert.IsFalse(iterator.IsBeforeReference);
            Assert.AreEqual(4, results.Count);
            Assert.IsInstanceOf<HtmlDivElement>(results[0]);
            Assert.IsInstanceOf<HtmlParagraphElement>(results[1]);
            Assert.IsInstanceOf<HtmlSpanElement>(results[2]);
            Assert.IsInstanceOf<HtmlBoldElement>(results[3]);

            do
            {
                results.Remove(iterator.Reference);
            }
            while (iterator.Previous() != null);

            Assert.IsTrue(iterator.IsBeforeReference);
        }

        [Test]
        public void NodeIteratorJavaScriptKitParagraph()
        {
            var source = @"<p id=essay>George<span> loves </span><b>JavaScript!</b></p>";
            var doc = source.ToHtmlDocument();
            Assert.IsNotNull(doc);

            var rootnode = doc.GetElementById("essay");
            Assert.IsNotNull(rootnode);

            var iterator = doc.CreateNodeIterator(rootnode, FilterSettings.Text);
            Assert.IsNotNull(iterator);
            Assert.AreEqual(rootnode, iterator.Root);
            Assert.IsTrue(iterator.IsBeforeReference);

            Assert.AreEqual("George", iterator.Next().TextContent);

            var paratext = iterator.Reference.TextContent;

            while (iterator.Next() != null)
            {
                paratext += iterator.Reference.TextContent;
            }

            Assert.AreEqual("George loves JavaScript!", paratext);
        }

        [Test]
        public void NodeIteratorJavaScriptKitList()
        {
            var source = @"<ul id=mylist>
<li class='item'>List 1</li>
<li class='item'>List 2</li>
<li>List 3</li>
</ul>";
            var doc = source.ToHtmlDocument();
            Assert.IsNotNull(doc);

            var rootnode = doc.GetElementById("mylist");
            Assert.IsNotNull(rootnode);

            var iterator = doc.CreateNodeIterator(rootnode, FilterSettings.Element, node =>
            {

                if (node is IHtmlListItemElement element && element.ClassList.Contains("item"))
                {
                    return FilterResult.Accept;
                }

                return FilterResult.Reject;
            });

            Assert.IsNotNull(iterator);
            Assert.AreEqual(rootnode, iterator.Root);

            var results = new List<INode>();

            while (iterator.Next() != null)
            {
                results.Add(iterator.Reference);
            }

            Assert.AreEqual(7, rootnode.ChildNodes.Length);
            Assert.AreEqual(3, rootnode.Children.Length);
            Assert.AreEqual(2, results.Count);

            var item1 = results[0] as IHtmlListItemElement;
            var item2 = results[1] as IHtmlListItemElement;

            Assert.IsNotNull(item1);
            Assert.IsNotNull(item2);

            Assert.AreEqual("item", item1.ClassName);
            Assert.AreEqual("item", item2.ClassName);
        }

        [Test]
        public void NodeIteratorDotteroSpans()
        {
            var source = @"<div id=""content"">
        <span>
            <b>1. Section</b><br />
            <span>
                <b>1.1. Subsection</b><br />
            </span>
        </span>
        <span>
            <b>2.Section</b><br />
        </span>
    </div>";
            var doc = source.ToHtmlDocument();
            Assert.IsNotNull(doc);

            var rootnode = doc.GetElementById("content");
            Assert.IsNotNull(rootnode);

            var iterator = doc.CreateNodeIterator(rootnode, FilterSettings.Element,
                m => m.GetTagName() == "span" ? FilterResult.Accept : FilterResult.Skip);
            Assert.IsNotNull(iterator);
            Assert.AreEqual(rootnode, iterator.Root);

            var node = iterator.Next();
            var sections = 0;
            Assert.IsNotNull(node);

            while (node != null)
            {
                Assert.AreEqual("span", node.GetTagName());
                sections++;
                node = iterator.Next();
            }

            Assert.AreEqual(3, sections);
        }

        [Test]
        public void NodeIteratorFromDocumentDoesNotThrowException()
        {
            var doc = "<div></div>".ToHtmlDocument();
            var ni = doc.CreateNodeIterator(doc, FilterSettings.All);
            Assert.AreEqual(doc, ni.Root);
            Assert.AreEqual(doc, ni.Next());
            Assert.AreEqual(doc.DocumentElement, ni.Next());
            Assert.AreEqual(doc.Head, ni.Next());
            Assert.AreEqual(doc.Body, ni.Next());
            Assert.AreEqual(doc.Body.FirstChild, ni.Next());
            Assert.AreEqual(null, ni.Next());
        }

        [Test]
        public void NodeIteratorFromEmptyElementDoesNotThrowException()
        {
            var doc = "<div></div>".ToHtmlDocument();
            var div = doc.QuerySelector("div");
            var ni = doc.CreateNodeIterator(div, FilterSettings.All);
            Assert.AreEqual(div, ni.Root);
            Assert.AreEqual(div, ni.Next());
            Assert.AreEqual(null, ni.Next());
            Assert.AreEqual(div, ni.Previous());
            Assert.AreEqual(null, ni.Previous());
        }

        [Test]
        public void NodeIteratorUsingPreviousWorksAsExpected()
        {
            var doc = "<div><span>foo</span></div>".ToHtmlDocument();
            var div = doc.QuerySelector("div");
            var ni = doc.CreateNodeIterator(div, FilterSettings.Element);
            Assert.AreEqual(div, ni.Root);
            Assert.AreEqual(div, ni.Next());
            Assert.AreNotEqual(null, ni.Next());
            Assert.AreEqual(null, ni.Next());
            Assert.AreNotEqual(null, ni.Previous());
            Assert.AreEqual(div, ni.Previous());
            Assert.AreEqual(null, ni.Previous());
            Assert.AreEqual(div, ni.Next());
            Assert.AreEqual(div, ni.Previous());
            Assert.AreEqual(null, ni.Previous());
        }

        [Test]
        public void NodeIteratorUsingCommentsWithNoCommentsOnlyYieldsNull()
        {
            var doc = "<div><span>foo</span></div>".ToHtmlDocument();
            var div = doc.QuerySelector("div");
            var ni = doc.CreateNodeIterator(div, FilterSettings.Comment);
            Assert.AreEqual(div, ni.Root);
            Assert.AreEqual(null, ni.Next());
            Assert.AreEqual(null, ni.Next());
            Assert.AreEqual(null, ni.Previous());
            Assert.AreEqual(null, ni.Previous());
            Assert.AreEqual(null, ni.Next());
            Assert.AreEqual(null, ni.Previous());
        }

        [Test]
        public void NodeIteratorShouldDealWithNodeRemoval_Issue1222()
        {
            var doc = "<div id='outer'><div id='inner1'></div><div id='inner2'></div></div>".ToHtmlDocument();
            var outer = doc.GetElementById("outer");
            var inner1 = doc.GetElementById("inner1");
            var inner2 = doc.GetElementById("inner2");
            var iterator = doc.CreateNodeIterator(outer, FilterSettings.Element);
            var node1 = iterator.Next();
            var node2 = iterator.Next();
            node2.Parent.RemoveChild(node2);
            var node3 = iterator.Next();

            Assert.AreEqual(outer, node1);
            Assert.AreEqual(inner1, node2);
            Assert.AreEqual(inner2, node3);
        }

        [Test]
        public void NodeIteratorShouldMoveReferenceToFollowingNodeOnRemoval()
        {
            var doc = "<div id='root'><div id='a'></div><div id='b'><div id='c'></div></div><div id='d'></div></div>".ToHtmlDocument();
            var root = doc.GetElementById("root");
            var b = doc.GetElementById("b");
            var c = doc.GetElementById("c");
            var d = doc.GetElementById("d");
            var iterator = doc.CreateNodeIterator(root, FilterSettings.Element);

            iterator.Next(); // root
            iterator.Next(); // a
            iterator.Next(); // b
            iterator.Next(); // c
            Assert.AreEqual(c, iterator.Previous());
            Assert.IsTrue(iterator.IsBeforeReference);

            b.Parent.RemoveChild(b);

            Assert.AreEqual(d, iterator.Reference);
            Assert.AreEqual(d, iterator.Next());
        }

        [Test]
        public void NodeIteratorShouldNotRevisitNodesWhenReferenceParentIsRemoved()
        {
            var doc = "<div id='root'><div id='first'></div><div id='second'><div id='third'></div></div></div>".ToHtmlDocument();
            var root = doc.GetElementById("root");
            var first = doc.GetElementById("first");
            var second = doc.GetElementById("second");
            var third = doc.GetElementById("third");
            var iterator = doc.CreateNodeIterator(root, FilterSettings.Element);

            Assert.AreEqual(root, iterator.Next());
            Assert.AreEqual(first, iterator.Next());
            Assert.AreEqual(second, iterator.Next());
            Assert.AreEqual(third, iterator.Next());
            Assert.AreEqual(third, iterator.Previous());

            second.Parent.RemoveChild(second);

            Assert.AreEqual(first, iterator.Reference);
            Assert.IsFalse(iterator.IsBeforeReference);
            Assert.AreEqual(null, iterator.Next());
            Assert.AreEqual(first, iterator.Previous());
            Assert.AreEqual(root, iterator.Previous());
        }
    }
}
