namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class TreeWalkerTests
    {
        private static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        private static System.String Walk(System.Func<INode> move)
        {
            var names = new System.Collections.Generic.List<System.String>();

            for (var node = move(); node != null && names.Count < 20; node = move())
            {
                names.Add(node.NodeName.ToLowerInvariant());
            }

            return System.String.Join(",", names);
        }

        [Test]
        public void TreeWalkerPreviousNodeGoesBackThroughTheTree()
        {
            var doc = Html("<div id=r><a><b></b><i></i></a><u></u><s><em></em></s></div>");
            var walker = doc.CreateTreeWalker(doc.GetElementById("r"), FilterSettings.Element);
            walker.Current = doc.QuerySelector("em");

            Assert.AreEqual("s,u,i,b,a,div", Walk(walker.ToPrevious));
            Assert.AreEqual("div", walker.Current.NodeName.ToLowerInvariant());
        }

        [Test]
        public void TreeWalkerPreviousNodeStopsAtTheRootWithoutASibling()
        {
            var doc = Html("<p></p><div id=r></div>");
            var walker = doc.CreateTreeWalker(doc.GetElementById("r"), FilterSettings.Element);

            Assert.IsNull(walker.ToPrevious());
        }

        [Test]
        public void TreeWalkerPreviousNodeSkipsAndRejectsAsFiltered()
        {
            var doc = Html("<div id=r><a><b></b></a><u><em></em></u><s></s></div>");
            var walker = doc.CreateTreeWalker(doc.GetElementById("r"), FilterSettings.Element, node =>
                node.NodeName == "U" ? FilterResult.Reject : node.NodeName == "A" ? FilterResult.Skip : FilterResult.Accept);
            walker.Current = doc.QuerySelector("s");

            Assert.AreEqual("b,div", Walk(walker.ToPrevious));
        }

        [Test]
        public void TreeWalkerGoesToTheLastAndBackAsAFocusZoneDoes()
        {
            var doc = Html("<div id=r><button>1</button><span><button>2</button></span><button>3</button></div>");
            var walker = doc.CreateTreeWalker(doc.GetElementById("r"), FilterSettings.Element, node =>
                node.NodeName == "BUTTON" ? FilterResult.Accept : FilterResult.Skip);
            INode node = null;

            for (var next = walker.ToLast(); next != null; next = walker.ToLast())
            {
                node = next;
            }

            Assert.AreEqual("3", node.TextContent);
            Assert.AreEqual("button,button", Walk(walker.ToPrevious));
        }

        [Test]
        public void TreeWalkerJavaScriptKitDivision()
        {
            var source = @"<div id=contentarea>
<p>Some <span>text</span></p>
<b>Bold text</b>
</div>";
            var doc = Html(source);
            Assert.IsNotNull(doc);

            var rootnode = doc.GetElementById("contentarea");
            Assert.IsNotNull(rootnode);

            var walker = doc.CreateTreeWalker(rootnode, FilterSettings.Element);
            Assert.IsNotNull(walker);
            Assert.AreEqual(rootnode, walker.Current);

            var results = new List<INode>();

            while (walker.ToNext() != null)
            {
                results.Add(walker.Current);
            }

            Assert.AreEqual(3, results.Count);
            Assert.IsInstanceOf<HtmlParagraphElement>(results[0]);
            Assert.IsInstanceOf<HtmlSpanElement>(results[1]);
            Assert.IsInstanceOf<HtmlBoldElement>(results[2]);

            walker.Current = rootnode;
            Assert.IsInstanceOf<HtmlParagraphElement>(walker.ToFirst());
        }

        [Test]
        public void TreeWalkerJavaScriptKitParagraph()
        {
            var source = @"<p id=essay>George<span> loves </span><b>JavaScript!</b></p>";
            var doc = Html(source);
            Assert.IsNotNull(doc);

            var rootnode = doc.GetElementById("essay");
            Assert.IsNotNull(rootnode);

            var walker = doc.CreateTreeWalker(rootnode, FilterSettings.Text);
            Assert.IsNotNull(walker);
            Assert.AreEqual(rootnode, walker.Current);

            Assert.AreEqual("George", walker.ToFirst().TextContent);

            var paratext = walker.Current.TextContent;

            while (walker.ToNextSibling() != null)
            {
                paratext += walker.Current.TextContent;
            }

            Assert.AreEqual("George loves JavaScript!", paratext);
        }

        [Test]
        public void TreeWalkerJavaScriptKitList()
        {
            var source = @"<ul id=mylist>
<li class='item'>List 1</li>
<li class='item'>List 2</li>
<li>List 3</li>
</ul>";
            var doc = Html(source);
            Assert.IsNotNull(doc);

            var rootnode = doc.GetElementById("mylist");
            Assert.IsNotNull(rootnode);

            var walker = doc.CreateTreeWalker(rootnode, FilterSettings.Element, node =>
            {

                if (node is IHtmlListItemElement element && element.ClassList.Contains("item"))
                {
                    return FilterResult.Accept;
                }

                return FilterResult.Reject;
            });

            Assert.IsNotNull(walker);
            Assert.AreEqual(rootnode, walker.Current);

            var results = new List<INode>();

            while (walker.ToNext() != null)
            {
                results.Add(walker.Current);
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
        public void TreeWalkerDotteroSpans()
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
            var doc = Html(source);
            Assert.IsNotNull(doc);

            var rootnode = doc.GetElementById("content");
            Assert.IsNotNull(rootnode);

            var walker = doc.CreateTreeWalker(rootnode, FilterSettings.Element, 
                m => m.GetTagName() == "span" ? FilterResult.Accept : FilterResult.Skip);
            Assert.IsNotNull(walker);
            Assert.AreEqual(rootnode, walker.Current);

            var node = walker.ToFirst();
            var sections = 0;
            Assert.IsNotNull(node);

            while (node != null)
            {
                Assert.AreEqual("span", node.GetTagName());
                sections++;
                node = walker.ToNextSibling();
            }

            Assert.AreEqual(2, sections);
        }
    }
}
