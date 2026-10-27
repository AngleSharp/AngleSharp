namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests_innerHTML_1.dat,
    /// tree-construction/tests4.dat
    /// </summary>
    [TestFixture]
    public class HtmlFragmentTests
    {
        static INodeList HtmlFragment(String code, IElement context = null)
        {
            return code.ToHtmlFragment(context);
        }

        static IElement Create(String tagName)
        {
            var doc = "".ToHtmlDocument();
            return doc.CreateElement(tagName);
        }

        [Test]
        public void FragmentBodyContextDoubleBodyAndSpanElement()
        {
            var doc = (@"<body><span>").ToHtmlFragment("body");

            var docspan0 = doc[0] as Element;
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Length);
            Assert.AreEqual("span", docspan0.GetTagName());
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [Test]
        public void FragmentBodyContextSpanAndDoubleBodyElement()
        {
            var doc = (@"<span><body>").ToHtmlFragment("body");

            var docspan0 = doc[0] as Element;
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Length);
            Assert.AreEqual("span", docspan0.GetTagName());
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [Test]
        public void FragmentDivContextSpanAndDoubleBodyElement()
        {
            var doc = (@"<span><body>").ToHtmlFragment("div");

            var docspan0 = doc[0] as Element;
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Length);
            Assert.AreEqual("span", docspan0.GetTagName());
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [Test]
        public void FragmentHtmlContextBodyAndSpanElement()
        {
            var doc = (@"<body><span>").ToHtmlFragment("html");

            var dochead0 = doc[0] as Element;
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Length);
            Assert.AreEqual("head", dochead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docbody1 = doc[1] as Element;
            Assert.AreEqual(1, docbody1.ChildNodes.Length);
            Assert.AreEqual(0, docbody1.Attributes.Length);
            Assert.AreEqual("body", docbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, docbody1.NodeType);

            var docbody1span0 = docbody1.ChildNodes[0] as Element;
            Assert.AreEqual(0, docbody1span0.ChildNodes.Length);
            Assert.AreEqual(0, docbody1span0.Attributes.Length);
            Assert.AreEqual("span", docbody1span0.GetTagName());
            Assert.AreEqual(NodeType.Element, docbody1span0.NodeType);
        }

        [Test]
        public void FragmentBodyContextFramesetAndSpanElement()
        {
            var doc = (@"<frameset><span>").ToHtmlFragment("body");

            var docspan0 = doc[0] as Element;
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Length);
            Assert.AreEqual("span", docspan0.GetTagName());
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);

        }

        [Test]
        public void FragmentBodyContextSpanAndFramesetElement()
        {
            var doc = (@"<span><frameset>").ToHtmlFragment("body");

            var docspan0 = doc[0] as Element;
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Length);
            Assert.AreEqual("span", docspan0.GetTagName());
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [Test]
        public void FragmentDivContextSpanAndFramesetElement()
        {
            var doc = (@"<span><frameset>").ToHtmlFragment("div");

            var docspan0 = doc[0] as Element;
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Length);
            Assert.AreEqual("span", docspan0.GetTagName());
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [Test]
        public void FragmentHtmlContextEmpty()
        {
            var doc = (@"").ToHtmlFragment("html");
            var dochead0 = doc[0] as Element;
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Length);
            Assert.AreEqual("head", dochead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docbody1 = doc[1] as Element;
            Assert.AreEqual(0, docbody1.ChildNodes.Length);
            Assert.AreEqual(0, docbody1.Attributes.Length);
            Assert.AreEqual("body", docbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, docbody1.NodeType);
        }

        [Test]
        public void FragmentHtmlContextFramesetAndSpanElement()
        {
            var doc = (@"<frameset><span>").ToHtmlFragment("html");

            var dochead0 = doc[0] as Element;
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Length);
            Assert.AreEqual("head", dochead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docframeset1 = doc[1] as Element;
            Assert.AreEqual(0, docframeset1.ChildNodes.Length);
            Assert.AreEqual(0, docframeset1.Attributes.Length);
            Assert.AreEqual("frameset", docframeset1.GetTagName());
            Assert.AreEqual(NodeType.Element, docframeset1.NodeType);
        }

        [Test]
        public void FragmentTableContextOpeningTableAndTrElement()
        {
            var doc = (@"<table><tr>").ToHtmlFragment("table");

            var doctbody0 = doc[0] as Element;
            Assert.AreEqual(1, doctbody0.ChildNodes.Length);
            Assert.AreEqual(0, doctbody0.Attributes.Length);
            Assert.AreEqual("tbody", doctbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctbody0.NodeType);

            var doctbody0tr0 = doctbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, doctbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, doctbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", doctbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctbody0tr0.NodeType);
        }

        [Test]
        public void FragmentTableContextClosingTableAndTrElement()
        {
            var doc = (@"</table><tr>").ToHtmlFragment("table");

            var doctbody0 = doc[0] as Element;
            Assert.AreEqual(1, doctbody0.ChildNodes.Length);
            Assert.AreEqual(0, doctbody0.Attributes.Length);
            Assert.AreEqual("tbody", doctbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctbody0.NodeType);

            var doctbody0tr0 = doctbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, doctbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, doctbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", doctbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctbody0tr0.NodeType);
        }

        [Test]
        public void FragmentFramesetContextClosingFramesetAndFrameElement()
        {
            var doc = (@"</frameset><frame>").ToHtmlFragment("frameset");

            var docframe0 = doc[0] as Element;
            Assert.AreEqual(0, docframe0.ChildNodes.Length);
            Assert.AreEqual(0, docframe0.Attributes.Length);
            Assert.AreEqual("frame", docframe0.GetTagName());
            Assert.AreEqual(NodeType.Element, docframe0.NodeType);
        }

        [Test]
        public void FragmentSelectContextClosingSelectAndOptionElement()
        {
            var doc = (@"</select><option>").ToHtmlFragment("select");
            var docoption0 = doc[0] as Element;
            Assert.AreEqual(0, docoption0.ChildNodes.Length);
            Assert.AreEqual(0, docoption0.Attributes.Length);
            Assert.AreEqual("option", docoption0.GetTagName());
            Assert.AreEqual(NodeType.Element, docoption0.NodeType);
        }

        [Test]
        public void FragmentSelectContextInputAndOptionElement()
        {
            var doc = (@"<input><option>").ToHtmlFragment("select");

            var docoption0 = doc[0] as Element;
            Assert.AreEqual(0, docoption0.ChildNodes.Length);
            Assert.AreEqual(0, docoption0.Attributes.Length);
            Assert.AreEqual("option", docoption0.GetTagName());
            Assert.AreEqual(NodeType.Element, docoption0.NodeType);
        }

        [Test]
        public void FragmentTdContextTableAndDoubleTdElement()
        {
            var doc = (@"<table><td><td>").ToHtmlFragment("td");

            var doctable0 = doc[0] as Element;
            Assert.AreEqual(1, doctable0.ChildNodes.Length);
            Assert.AreEqual(0, doctable0.Attributes.Length);
            Assert.AreEqual("table", doctable0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctable0.NodeType);

            var doctable0tbody0 = doctable0.ChildNodes[0] as Element;
            Assert.AreEqual(1, doctable0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, doctable0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", doctable0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctable0tbody0.NodeType);

            var doctable0tbody0tr0 = doctable0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(2, doctable0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, doctable0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", doctable0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctable0tbody0tr0.NodeType);

            var doctable0tbody0tr0td0 = doctable0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, doctable0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, doctable0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", doctable0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctable0tbody0tr0td0.NodeType);

            var doctable0tbody0tr0td1 = doctable0tbody0tr0.ChildNodes[1] as Element;
            Assert.AreEqual(0, doctable0tbody0tr0td1.ChildNodes.Length);
            Assert.AreEqual(0, doctable0tbody0tr0td1.Attributes.Length);
            Assert.AreEqual("td", doctable0tbody0tr0td1.GetTagName());
            Assert.AreEqual(NodeType.Element, doctable0tbody0tr0td1.NodeType);

        }

        [Test]
        public void FragmentTdContextTfootAndAnchorElement()
        {
            var doc = (@"<tfoot><a>").ToHtmlFragment("td");

            var doca0 = doc[0] as Element;
            Assert.AreEqual(0, doca0.ChildNodes.Length);
            Assert.AreEqual(0, doca0.Attributes.Length);
            Assert.AreEqual("a", doca0.GetTagName());
            Assert.AreEqual(NodeType.Element, doca0.NodeType);
        }

        [Test]
        public void FragmentTrContextTdAndFinishedTableAndTdElement()
        {
            var doc = (@"<td><table></table><td>").ToHtmlFragment("tr");

            var doctd0 = doc[0] as Element;
            Assert.AreEqual(1, doctd0.ChildNodes.Length);
            Assert.AreEqual(0, doctd0.Attributes.Length);
            Assert.AreEqual("td", doctd0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctd0.NodeType);

            var doctd0table0 = doctd0.ChildNodes[0] as Element;
            Assert.AreEqual(0, doctd0table0.ChildNodes.Length);
            Assert.AreEqual(0, doctd0table0.Attributes.Length);
            Assert.AreEqual("table", doctd0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctd0table0.NodeType);

            var doctd1 = doc[1] as Element;
            Assert.AreEqual(0, doctd1.ChildNodes.Length);
            Assert.AreEqual(0, doctd1.Attributes.Length);
            Assert.AreEqual("td", doctd1.GetTagName());
            Assert.AreEqual(NodeType.Element, doctd1.NodeType);
        }

        [Test]
        public void FragmentTbodyContextTdAndTableAndTbodyAndMisplacedAnchorAndTrElement()
        {
            var doc = (@"<td><table><tbody><a><tr>").ToHtmlFragment("tbody");

            var doctr0 = doc[0] as Element;
            Assert.AreEqual(1, doctr0.ChildNodes.Length);
            Assert.AreEqual(0, doctr0.Attributes.Length);
            Assert.AreEqual("tr", doctr0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctr0.NodeType);

            var doctr0td0 = doctr0.ChildNodes[0] as Element;
            Assert.AreEqual(2, doctr0td0.ChildNodes.Length);
            Assert.AreEqual(0, doctr0td0.Attributes.Length);
            Assert.AreEqual("td", doctr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctr0td0.NodeType);

            var doctr0td0a0 = doctr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(0, doctr0td0a0.ChildNodes.Length);
            Assert.AreEqual(0, doctr0td0a0.Attributes.Length);
            Assert.AreEqual("a", doctr0td0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctr0td0a0.NodeType);

            var doctr0td0table1 = doctr0td0.ChildNodes[1] as Element;
            Assert.AreEqual(1, doctr0td0table1.ChildNodes.Length);
            Assert.AreEqual(0, doctr0td0table1.Attributes.Length);
            Assert.AreEqual("table", doctr0td0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, doctr0td0table1.NodeType);

            var doctr0td0table1tbody0 = doctr0td0table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, doctr0td0table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, doctr0td0table1tbody0.Attributes.Length);
            Assert.AreEqual("tbody", doctr0td0table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctr0td0table1tbody0.NodeType);

            var doctr0td0table1tbody0tr0 = doctr0td0table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, doctr0td0table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, doctr0td0table1tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", doctr0td0table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctr0td0table1tbody0tr0.NodeType);

        }

        [Test]
        public void FragmentTbodyContextMisplacedTheadAndAnchorElement()
        {
            var doc = (@"<thead><a>").ToHtmlFragment("tbody");

            var doca0 = doc[0] as Element;
            Assert.AreEqual(0, doca0.ChildNodes.Length);
            Assert.AreEqual(0, doca0.Attributes.Length);
            Assert.AreEqual("a", doca0.GetTagName());
            Assert.AreEqual(NodeType.Element, doca0.NodeType);
        }

        [Test]
        public void FragmentColgroupContextClosingColgroupAndColElement()
        {
            var doc = (@"</colgroup><col>").ToHtmlFragment("colgroup");

            var doccol0 = doc[0] as Element;
            Assert.AreEqual(0, doccol0.ChildNodes.Length);
            Assert.AreEqual(0, doccol0.Attributes.Length);
            Assert.AreEqual("col", doccol0.GetTagName());
            Assert.AreEqual(NodeType.Element, doccol0.NodeType);
        }

        [Test]
        public void FragmentDivContextWithText()
        {
            var doc = (@"direct div content").ToHtmlFragment("div");

            var docText0 = doc[0];
            Assert.AreEqual(NodeType.Text, docText0.NodeType);
            Assert.AreEqual("direct div content", docText0.TextContent);
        }

        [Test]
        public void FragmentTextareaContextWithText()
        {
            var doc = (@"direct textarea content").ToHtmlFragment("textarea");
            var docText0 = doc[0];
            Assert.AreEqual(NodeType.Text, docText0.NodeType);
            Assert.AreEqual("direct textarea content", docText0.TextContent);
        }

        [Test]
        public void FragmentTextAreaContextWithTextAndMarkup()
        {
            var doc = (@"textarea content with <em>pseudo</em> <foo>markup").ToHtmlFragment("textarea");
            var docText0 = doc[0];
            Assert.AreEqual(NodeType.Text, docText0.NodeType);
            Assert.AreEqual("textarea content with <em>pseudo</em> <foo>markup", docText0.TextContent);
        }

        [Test]
        public void FragmentStyleContextWithText()
        {
            var doc = (@"this is &#x0043;DATA inside a <style> element").ToHtmlFragment("style");

            var docText0 = doc[0];
            Assert.AreEqual(NodeType.Text, docText0.NodeType);
            Assert.AreEqual("this is &#x0043;DATA inside a <style> element", docText0.TextContent);
        }

        [Test]
        public void FragmentPlaintextContext()
        {
            var doc = (@"</plaintext>").ToHtmlFragment("plaintext");

            var docText0 = doc[0];
            Assert.AreEqual(NodeType.Text, docText0.NodeType);
            Assert.AreEqual("</plaintext>", docText0.TextContent);
        }

        [Test]
        public void FragmentHtmlContextWithText()
        {
            var doc = (@"setting html's innerHTML").ToHtmlFragment("html");

            var dochead0 = doc[0] as Element;
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Length);
            Assert.AreEqual("head", dochead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docbody1 = doc[1] as Element;
            Assert.AreEqual(1, docbody1.ChildNodes.Length);
            Assert.AreEqual(0, docbody1.Attributes.Length);
            Assert.AreEqual("body", docbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, docbody1.NodeType);

            var docbody1Text0 = docbody1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, docbody1Text0.NodeType);
            Assert.AreEqual("setting html's innerHTML", docbody1Text0.TextContent);
        }

        [Test]
        public void FragmentHeadContextWithTextInTitle()
        {
            var doc = (@"<title>setting head's innerHTML</title>").ToHtmlFragment("head");

            var doctitle0 = doc[0] as Element;
            Assert.AreEqual(1, doctitle0.ChildNodes.Length);
            Assert.AreEqual(0, doctitle0.Attributes.Length);
            Assert.AreEqual("title", doctitle0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctitle0.NodeType);

            var doctitle0Text0 = doctitle0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, doctitle0Text0.NodeType);
            Assert.AreEqual("setting head's innerHTML", doctitle0Text0.TextContent);
        }

        [Test]
        public void FosterFragmentDoubleClosedBody()
        {
            var doc = (@"<body>X</body></body>").ToHtmlFragment("html");

            var dochead0 = doc[0] as Element;
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Length);
            Assert.AreEqual("head", dochead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docbody1 = doc[1] as Element;
            Assert.AreEqual(1, docbody1.ChildNodes.Length);
            Assert.AreEqual(0, docbody1.Attributes.Length);
            Assert.AreEqual("body", docbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, docbody1.NodeType);

            var docbody1Text0 = docbody1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, docbody1Text0.NodeType);
            Assert.AreEqual("X", docbody1Text0.TextContent);
        }

        [Test]
        public void FragmentButtonWithText()
        {
            var doc = ("<button>Boo!</button>").ToHtmlFragment();
            var buttonElement = doc.QuerySelector("button") as IHtmlButtonElement;

            Assert.IsNotNull(buttonElement);
            Assert.AreEqual("Boo!", buttonElement.TextContent);
        }

        [Test]
        public void FragmentButtonWithTextAndAttribute()
        {
            var doc = ("<button type=SEARCH>Boo!</button>").ToHtmlFragment();
            var buttonElement = doc.QuerySelector("button") as IHtmlButtonElement;

            Assert.IsNotNull(buttonElement);
            Assert.AreEqual("Boo!", buttonElement.TextContent);
            Assert.AreEqual("search", buttonElement.Type);
            Assert.AreEqual("SEARCH", buttonElement.GetAttribute("type"));
        }

        [Test]
        public void FragmentButtonDefaultSubmitType()
        {
            var doc = ("<button>Boo!</button>").ToHtmlFragment();
            var buttonElement = doc.QuerySelector("button") as IHtmlButtonElement;

            Assert.IsNotNull(buttonElement);
            Assert.AreEqual("Boo!", buttonElement.TextContent);
            Assert.AreEqual("submit", buttonElement.Type);
            Assert.IsFalse(buttonElement.HasAttribute("type"));
        }
        
        [Test]
        public void FragmentClassNameCaseNumbered()
        {
            var dom = ("<div class=\"class1 CLASS2 claSS3\" x=\"y\" />").ToHtmlFragment();
            var el = dom.QuerySelector("div");

            Assert.IsNotNull(el);
            Assert.AreEqual(3, el.ClassList.Length);

            CollectionAssert.AreEqual(new List<String>(new [] { "class1", "CLASS2", "claSS3" }), new List<String>(el.ClassList));

            Assert.AreEqual(0, dom.QuerySelectorAll(".class2").Length);
            Assert.AreEqual(1, dom.QuerySelectorAll(".CLASS2").Length);
        }

        [Test]
        public void FragmentClassNameOnlyCase()
        {
            var dom = ("<div class=\"class CLASS\" />").ToHtmlFragment();
            var el = dom.QuerySelector("div");

            Assert.IsNotNull(el);
            Assert.AreEqual(2, el.ClassList.Length);

            CollectionAssert.AreEqual(new List<String>(new[] { "class", "CLASS" }), new List<String>(el.ClassList));
        }

        [Test]
        public void FragmentUnquotedAttributeHandling()
        {
            var doc = ("<div custattribute=10/23/2012 id=\"tableSample\"><span>sample text</span></div>").ToHtmlFragment();
            var obj = doc.QuerySelector("#tableSample");

            Assert.AreEqual("10/23/2012", obj.GetAttribute("custattribute"));
        }

        [Test]
        public void FragmentCaretsInAttributes()
        {
            var doc = ("<div><img src=\"test.png\" alt=\">\" /></div>").ToHtmlFragment();
            var div = doc.QuerySelector("div");

            Assert.IsNotNull(div);
            Assert.AreEqual(">", div.QuerySelector("img").GetAttribute("alt"));
            Assert.AreEqual("<div><img src=\"test.png\" alt=\"&gt;\"></div>", div.OuterHtml);
        }

        [Test]
        public void FragmentUnwrapWithoutParent()
        {
            var s = "This is <b> a big</b> text";
            var f = s.ToHtmlFragment();
            var t = f.QuerySelector("b");

            Assert.AreEqual("<b> a big</b>", t.OuterHtml);
        }

        [Test]
        public void FragmentRoundtripEncoding()
        {
            var html = "<span>Test &nbsp; nbsp</span>";
            var dom = (html).ToHtmlFragment();

            var body = dom.QuerySelector("body");
            Assert.IsNotNull(body);

            var output = body.InnerHtml.Replace("" + (Char)160, "&nbsp;");
            Assert.AreEqual(html, output);
        }

        [Test]
        public void FragmentClassAndStyleAsBoolean()
        {
            var html = @"<span class="""" style="""">Test </span><div class style><br /></div>";
            var dom = (html).ToHtmlFragment();

            var body = dom.QuerySelector("body");
            Assert.IsNotNull(body);

            var output = body.InnerHtml.Replace("" + (char)160, "&nbsp;");
            Assert.AreEqual(@"<span class="""" style="""">Test </span><div class="""" style=""""><br></div>", output);
        }

        [Test]
        public void FragmentUtf8HighValuesConversion()
        {
            var html = @"<span>&#55449;&#56580;</span>";
            var dom = (html).ToHtmlFragment();
            var span = dom.QuerySelector("span");

            Assert.IsNotNull(span);
            Assert.AreEqual("\uFFFD\uFFFD", span.InnerHtml);
        }

        [Test]
        public void FragmentTableShouldBeAlright()
        {
            var fragment = "<table></table>";
            var document = "<!DOCTYPE html><div id=outputPanel></div>".ToHtmlDocument();
            var element = document.GetElementById("outputPanel");
            element.InnerHtml = fragment;

            Assert.AreEqual(fragment, element.InnerHtml);
        }

        [Test]
        public void FragmentTailShouldBeConsidered()
        {
            var fragment = "<script>alert('foo');</script><p>Test</p>";
            var document = "<!DOCTYPE html><div id=outputPanel></div>".ToHtmlDocument();
            var nodes = fragment.ToHtmlFragment(document.Body);

            Assert.AreEqual(2, nodes.Length);
            Assert.AreEqual("script", nodes[0].GetTagName());
            Assert.AreEqual("p", nodes[1].GetTagName());
        }

        [Test]
        public void InnerHtmlShouldConsiderAllElementsOfFragmentsContainingScriptElements()
        {
            var fragment = "<p>Foo</p><script>alert('foo');</script><p>Test</p>";
            var document = "<!DOCTYPE html><div id=outputPanel></div>".ToHtmlDocument();
            document.Body.InnerHtml = fragment;
            var nodes = document.Body.ChildNodes;

            Assert.IsNull(document.QuerySelector("#outputPanel"));
            Assert.AreEqual(3, nodes.Length);
            Assert.AreEqual("p", nodes[0].GetTagName());
            Assert.AreEqual("Foo", nodes[0].TextContent);
            Assert.AreEqual("script", nodes[1].GetTagName());
            Assert.AreEqual("p", nodes[2].GetTagName());
            Assert.AreEqual("Test", nodes[2].TextContent);
        }

        [TestCase("<svg><p>y</p></svg>", "svg", "p", "y", null)]
        [TestCase("<svg><b>x</b>z</svg>", "svg", "b", "x", "z")]
        [TestCase("<math><div>x</div></math>", "math", "div", "x", null)]
        public void FragmentBreakoutTagLeavesForeignContent(String source, String foreign, String html, String inner, String tail)
        {
            var nodes = source.ToHtmlFragment("body");

            Assert.AreEqual(tail is null ? 2 : 3, nodes.Length);
            Assert.AreEqual(foreign, nodes[0].GetTagName());
            Assert.AreEqual(0, nodes[0].ChildNodes.Length);
            Assert.AreEqual(html, nodes[1].GetTagName());
            Assert.AreEqual(NamespaceNames.HtmlUri, ((IElement)nodes[1]).NamespaceUri);
            Assert.AreEqual(inner, nodes[1].TextContent);

            if (tail is not null)
            {
                Assert.AreEqual(tail, nodes[2].TextContent);
            }
        }

        [Test]
        public void FragmentBreakoutTagMatchesDocumentParse()
        {
            var source = "<svg><g><p>y</p></g></svg>";
            var document = ("<body>" + source).ToHtmlDocument();
            var holder = document.CreateElement("div");

            foreach (var node in source.ToHtmlFragment(document.Body).ToArray())
            {
                holder.AppendChild(node);
            }

            Assert.AreEqual(document.Body.InnerHtml, holder.InnerHtml);
            Assert.AreEqual(NamespaceNames.HtmlUri, holder.QuerySelector("p").NamespaceUri);
            Assert.AreEqual(NamespaceNames.HtmlUri, document.Body.QuerySelector("p").NamespaceUri);
        }

        [Test]
        public void FragmentBreakoutTagInsideIntegrationPointStaysHtml()
        {
            var nodes = "<svg><desc><p>ok</p></desc></svg>".ToHtmlFragment("body");

            Assert.AreEqual(1, nodes.Length);
            var p = ((IElement)nodes[0]).QuerySelector("p");
            Assert.AreEqual(NamespaceNames.HtmlUri, p.NamespaceUri);
            Assert.AreEqual("desc", p.Parent.GetTagName());
        }

        [TestCase("<svg></svg>", "svg", "http://www.w3.org/2000/svg")]
        [TestCase("<math></math>", "math", "http://www.w3.org/1998/Math/MathML")]
        public void FragmentBreakoutTagWithForeignContextIsHtml(String host, String contextName, String namespaceUri)
        {
            var document = ("<body>" + host).ToHtmlDocument();
            var context = document.Body.QuerySelector(contextName);
            context.InnerHtml = "<p>a</p><circle/>";
            var nodes = context.ChildNodes;

            Assert.AreEqual(2, nodes.Length);
            Assert.AreEqual("p", nodes[0].GetTagName());
            Assert.AreEqual(NamespaceNames.HtmlUri, ((IElement)nodes[0]).NamespaceUri);
            Assert.AreEqual("a", nodes[0].TextContent);
            Assert.AreEqual(namespaceUri, ((IElement)nodes[1]).NamespaceUri);
        }

        [TestCase("<svg></svg>", "svg", "http://www.w3.org/2000/svg")]
        [TestCase("<math></math>", "math", "http://www.w3.org/1998/Math/MathML")]
        public void ParseFragmentKeepsForeignContextNamespace(String host, String contextName, String namespaceUri)
        {
            var document = ("<body>" + host).ToHtmlDocument();
            var context = document.Body.QuerySelector(contextName);
            var nodes = "<circle/><p>a</p>".ToHtmlFragment(context);

            Assert.AreEqual(2, nodes.Length);
            Assert.AreEqual(namespaceUri, ((IElement)nodes[0]).NamespaceUri);
            Assert.AreEqual(NamespaceNames.HtmlUri, ((IElement)nodes[1]).NamespaceUri);
            Assert.AreEqual(namespaceUri, context.NamespaceUri);
        }

        [TestCase("<math><annotation-xml encoding='text/html'></annotation-xml></math>", "http://www.w3.org/1999/xhtml")]
        [TestCase("<math><annotation-xml></annotation-xml></math>", "http://www.w3.org/1998/Math/MathML")]
        public void ParseFragmentKeepsAnnotationXmlIntegrationPoint(String host, String namespaceUri)
        {
            var document = ("<body>" + host).ToHtmlDocument();
            var context = document.Body.QuerySelector("annotation-xml");
            var nodes = "<circle/>".ToHtmlFragment(context);

            Assert.AreEqual(1, nodes.Length);
            Assert.AreEqual(namespaceUri, ((IElement)nodes[0]).NamespaceUri);

            context.InnerHtml = "<circle/>";
            Assert.AreEqual(namespaceUri, ((IElement)context.FirstChild).NamespaceUri);
        }

        [TestCase("<form><div id=c></div></form>", 1, "input")]
        [TestCase("<div id=c></div>", 1, "form")]
        public void ParseFragmentFindsFormAncestorOfContext(String host, Int32 length, String first)
        {
            var document = ("<body>" + host).ToHtmlDocument();
            var context = document.GetElementById("c");
            var nodes = "<form><input></form>".ToHtmlFragment(context);

            Assert.AreEqual(length, nodes.Length);
            Assert.AreEqual(first, nodes[0].GetTagName());

            context.InnerHtml = "<form><input></form>";
            Assert.AreEqual(length, context.ChildNodes.Length);
            Assert.AreEqual(first, context.FirstChild.GetTagName());
        }
    }
}
