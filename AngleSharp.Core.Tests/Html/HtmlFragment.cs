using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
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
            var doc = HtmlFragment(@"<body><span>", Create("body"));

            var docspan0 = doc[0] as Element;
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Count);
            Assert.AreEqual("span", docspan0.GetTagName());
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [Test]
        public void FragmentBodyContextSpanAndDoubleBodyElement()
        {
            var doc = HtmlFragment(@"<span><body>", Create("body"));

            var docspan0 = doc[0] as Element;
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Count);
            Assert.AreEqual("span", docspan0.GetTagName());
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [Test]
        public void FragmentDivContextSpanAndDoubleBodyElement()
        {
            var doc = HtmlFragment(@"<span><body>", Create("div"));

            var docspan0 = doc[0] as Element;
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Count);
            Assert.AreEqual("span", docspan0.GetTagName());
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [Test]
        public void FragmentHtmlContextBodyAndSpanElement()
        {
            var doc = HtmlFragment(@"<body><span>", Create("html"));

            var dochead0 = doc[0] as Element;
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Count);
            Assert.AreEqual("head", dochead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docbody1 = doc[1] as Element;
            Assert.AreEqual(1, docbody1.ChildNodes.Length);
            Assert.AreEqual(0, docbody1.Attributes.Count);
            Assert.AreEqual("body", docbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, docbody1.NodeType);

            var docbody1span0 = docbody1.ChildNodes[0] as Element;
            Assert.AreEqual(0, docbody1span0.ChildNodes.Length);
            Assert.AreEqual(0, docbody1span0.Attributes.Count);
            Assert.AreEqual("span", docbody1span0.GetTagName());
            Assert.AreEqual(NodeType.Element, docbody1span0.NodeType);
        }

        [Test]
        public void FragmentBodyContextFramesetAndSpanElement()
        {
            var doc = HtmlFragment(@"<frameset><span>", Create("body"));

            var docspan0 = doc[0] as Element;
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Count);
            Assert.AreEqual("span", docspan0.GetTagName());
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);

        }

        [Test]
        public void FragmentBodyContextSpanAndFramesetElement()
        {
            var doc = HtmlFragment(@"<span><frameset>", Create("body"));

            var docspan0 = doc[0] as Element;
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Count);
            Assert.AreEqual("span", docspan0.GetTagName());
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [Test]
        public void FragmentDivContextSpanAndFramesetElement()
        {
            var doc = HtmlFragment(@"<span><frameset>", Create("div"));

            var docspan0 = doc[0] as Element;
            Assert.AreEqual(0, docspan0.ChildNodes.Length);
            Assert.AreEqual(0, docspan0.Attributes.Count);
            Assert.AreEqual("span", docspan0.GetTagName());
            Assert.AreEqual(NodeType.Element, docspan0.NodeType);
        }

        [Test]
        public void FragmentHtmlContextEmpty()
        {
            var doc = HtmlFragment(@"", Create("html"));
            var dochead0 = doc[0] as Element;
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Count);
            Assert.AreEqual("head", dochead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docbody1 = doc[1] as Element;
            Assert.AreEqual(0, docbody1.ChildNodes.Length);
            Assert.AreEqual(0, docbody1.Attributes.Count);
            Assert.AreEqual("body", docbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, docbody1.NodeType);
        }

        [Test]
        public void FragmentHtmlContextFramesetAndSpanElement()
        {
            var doc = HtmlFragment(@"<frameset><span>", Create("html"));

            var dochead0 = doc[0] as Element;
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Count);
            Assert.AreEqual("head", dochead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docframeset1 = doc[1] as Element;
            Assert.AreEqual(0, docframeset1.ChildNodes.Length);
            Assert.AreEqual(0, docframeset1.Attributes.Count);
            Assert.AreEqual("frameset", docframeset1.GetTagName());
            Assert.AreEqual(NodeType.Element, docframeset1.NodeType);
        }

        [Test]
        public void FragmentTableContextOpeningTableAndTrElement()
        {
            var doc = HtmlFragment(@"<table><tr>", Create("table"));

            var doctbody0 = doc[0] as Element;
            Assert.AreEqual(1, doctbody0.ChildNodes.Length);
            Assert.AreEqual(0, doctbody0.Attributes.Count);
            Assert.AreEqual("tbody", doctbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctbody0.NodeType);

            var doctbody0tr0 = doctbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, doctbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, doctbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", doctbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctbody0tr0.NodeType);
        }

        [Test]
        public void FragmentTableContextClosingTableAndTrElement()
        {
            var doc = HtmlFragment(@"</table><tr>", Create("table"));

            var doctbody0 = doc[0] as Element;
            Assert.AreEqual(1, doctbody0.ChildNodes.Length);
            Assert.AreEqual(0, doctbody0.Attributes.Count);
            Assert.AreEqual("tbody", doctbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctbody0.NodeType);

            var doctbody0tr0 = doctbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, doctbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, doctbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", doctbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctbody0tr0.NodeType);
        }

        [Test]
        public void FragmentFramesetContextClosingFramesetAndFrameElement()
        {
            var doc = HtmlFragment(@"</frameset><frame>", Create("frameset"));

            var docframe0 = doc[0] as Element;
            Assert.AreEqual(0, docframe0.ChildNodes.Length);
            Assert.AreEqual(0, docframe0.Attributes.Count);
            Assert.AreEqual("frame", docframe0.GetTagName());
            Assert.AreEqual(NodeType.Element, docframe0.NodeType);
        }

        [Test]
        public void FragmentSelectContextClosingSelectAndOptionElement()
        {
            var doc = HtmlFragment(@"</select><option>", Create("select"));
            var docoption0 = doc[0] as Element;
            Assert.AreEqual(0, docoption0.ChildNodes.Length);
            Assert.AreEqual(0, docoption0.Attributes.Count);
            Assert.AreEqual("option", docoption0.GetTagName());
            Assert.AreEqual(NodeType.Element, docoption0.NodeType);
        }

        [Test]
        public void FragmentSelectContextInputAndOptionElement()
        {
            var doc = HtmlFragment(@"<input><option>", Create("select"));

            var docoption0 = doc[0] as Element;
            Assert.AreEqual(0, docoption0.ChildNodes.Length);
            Assert.AreEqual(0, docoption0.Attributes.Count);
            Assert.AreEqual("option", docoption0.GetTagName());
            Assert.AreEqual(NodeType.Element, docoption0.NodeType);
        }

        [Test]
        public void FragmentTdContextTableAndDoubleTdElement()
        {
            var doc = HtmlFragment(@"<table><td><td>", Create("td"));

            var doctable0 = doc[0] as Element;
            Assert.AreEqual(1, doctable0.ChildNodes.Length);
            Assert.AreEqual(0, doctable0.Attributes.Count);
            Assert.AreEqual("table", doctable0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctable0.NodeType);

            var doctable0tbody0 = doctable0.ChildNodes[0] as Element;
            Assert.AreEqual(1, doctable0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, doctable0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", doctable0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctable0tbody0.NodeType);

            var doctable0tbody0tr0 = doctable0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(2, doctable0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, doctable0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", doctable0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctable0tbody0tr0.NodeType);

            var doctable0tbody0tr0td0 = doctable0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, doctable0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, doctable0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", doctable0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctable0tbody0tr0td0.NodeType);

            var doctable0tbody0tr0td1 = doctable0tbody0tr0.ChildNodes[1] as Element;
            Assert.AreEqual(0, doctable0tbody0tr0td1.ChildNodes.Length);
            Assert.AreEqual(0, doctable0tbody0tr0td1.Attributes.Count);
            Assert.AreEqual("td", doctable0tbody0tr0td1.GetTagName());
            Assert.AreEqual(NodeType.Element, doctable0tbody0tr0td1.NodeType);

        }

        [Test]
        public void FragmentTdContextTfootAndAnchorElement()
        {
            var doc = HtmlFragment(@"<tfoot><a>", Create("td"));

            var doca0 = doc[0] as Element;
            Assert.AreEqual(0, doca0.ChildNodes.Length);
            Assert.AreEqual(0, doca0.Attributes.Count);
            Assert.AreEqual("a", doca0.GetTagName());
            Assert.AreEqual(NodeType.Element, doca0.NodeType);
        }

        [Test]
        public void FragmentTrContextTdAndFinishedTableAndTdElement()
        {
            var doc = HtmlFragment(@"<td><table></table><td>", Create("tr"));

            var doctd0 = doc[0] as Element;
            Assert.AreEqual(1, doctd0.ChildNodes.Length);
            Assert.AreEqual(0, doctd0.Attributes.Count);
            Assert.AreEqual("td", doctd0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctd0.NodeType);

            var doctd0table0 = doctd0.ChildNodes[0] as Element;
            Assert.AreEqual(0, doctd0table0.ChildNodes.Length);
            Assert.AreEqual(0, doctd0table0.Attributes.Count);
            Assert.AreEqual("table", doctd0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctd0table0.NodeType);

            var doctd1 = doc[1] as Element;
            Assert.AreEqual(0, doctd1.ChildNodes.Length);
            Assert.AreEqual(0, doctd1.Attributes.Count);
            Assert.AreEqual("td", doctd1.GetTagName());
            Assert.AreEqual(NodeType.Element, doctd1.NodeType);
        }

        [Test]
        public void FragmentTbodyContextTdAndTableAndTbodyAndMisplacedAnchorAndTrElement()
        {
            var doc = HtmlFragment(@"<td><table><tbody><a><tr>", Create("tbody"));

            var doctr0 = doc[0] as Element;
            Assert.AreEqual(1, doctr0.ChildNodes.Length);
            Assert.AreEqual(0, doctr0.Attributes.Count);
            Assert.AreEqual("tr", doctr0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctr0.NodeType);

            var doctr0td0 = doctr0.ChildNodes[0] as Element;
            Assert.AreEqual(2, doctr0td0.ChildNodes.Length);
            Assert.AreEqual(0, doctr0td0.Attributes.Count);
            Assert.AreEqual("td", doctr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctr0td0.NodeType);

            var doctr0td0a0 = doctr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(0, doctr0td0a0.ChildNodes.Length);
            Assert.AreEqual(0, doctr0td0a0.Attributes.Count);
            Assert.AreEqual("a", doctr0td0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctr0td0a0.NodeType);

            var doctr0td0table1 = doctr0td0.ChildNodes[1] as Element;
            Assert.AreEqual(1, doctr0td0table1.ChildNodes.Length);
            Assert.AreEqual(0, doctr0td0table1.Attributes.Count);
            Assert.AreEqual("table", doctr0td0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, doctr0td0table1.NodeType);

            var doctr0td0table1tbody0 = doctr0td0table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, doctr0td0table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, doctr0td0table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", doctr0td0table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctr0td0table1tbody0.NodeType);

            var doctr0td0table1tbody0tr0 = doctr0td0table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, doctr0td0table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, doctr0td0table1tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", doctr0td0table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctr0td0table1tbody0tr0.NodeType);

        }

        [Test]
        public void FragmentTbodyContextMisplacedTheadAndAnchorElement()
        {
            var doc = HtmlFragment(@"<thead><a>", Create("tbody"));

            var doca0 = doc[0] as Element;
            Assert.AreEqual(0, doca0.ChildNodes.Length);
            Assert.AreEqual(0, doca0.Attributes.Count);
            Assert.AreEqual("a", doca0.GetTagName());
            Assert.AreEqual(NodeType.Element, doca0.NodeType);
        }

        [Test]
        public void FragmentColgroupContextClosingColgroupAndColElement()
        {
            var doc = HtmlFragment(@"</colgroup><col>", Create("colgroup"));

            var doccol0 = doc[0] as Element;
            Assert.AreEqual(0, doccol0.ChildNodes.Length);
            Assert.AreEqual(0, doccol0.Attributes.Count);
            Assert.AreEqual("col", doccol0.GetTagName());
            Assert.AreEqual(NodeType.Element, doccol0.NodeType);
        }

        [Test]
        public void FragmentDivContextWithText()
        {
            var doc = HtmlFragment(@"direct div content", Create("div"));

            var docText0 = doc[0];
            Assert.AreEqual(NodeType.Text, docText0.NodeType);
            Assert.AreEqual("direct div content", docText0.TextContent);
        }

        [Test]
        public void FragmentTextareaContextWithText()
        {
            var doc = HtmlFragment(@"direct textarea content", Create("textarea"));
            var docText0 = doc[0];
            Assert.AreEqual(NodeType.Text, docText0.NodeType);
            Assert.AreEqual("direct textarea content", docText0.TextContent);
        }

        [Test]
        public void FragmentTextAreaContextWithTextAndMarkup()
        {
            var doc = HtmlFragment(@"textarea content with <em>pseudo</em> <foo>markup", Create("textarea"));
            var docText0 = doc[0];
            Assert.AreEqual(NodeType.Text, docText0.NodeType);
            Assert.AreEqual("textarea content with <em>pseudo</em> <foo>markup", docText0.TextContent);
        }

        [Test]
        public void FragmentStyleContextWithText()
        {
            var doc = HtmlFragment(@"this is &#x0043;DATA inside a <style> element", Create("style"));

            var docText0 = doc[0];
            Assert.AreEqual(NodeType.Text, docText0.NodeType);
            Assert.AreEqual("this is &#x0043;DATA inside a <style> element", docText0.TextContent);
        }

        [Test]
        public void FragmentPlaintextContext()
        {
            var doc = HtmlFragment(@"</plaintext>", Create("plaintext"));

            var docText0 = doc[0];
            Assert.AreEqual(NodeType.Text, docText0.NodeType);
            Assert.AreEqual("</plaintext>", docText0.TextContent);
        }

        [Test]
        public void FragmentHtmlContextWithText()
        {
            var doc = HtmlFragment(@"setting html's innerHTML", Create("html"));

            var dochead0 = doc[0] as Element;
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Count);
            Assert.AreEqual("head", dochead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docbody1 = doc[1] as Element;
            Assert.AreEqual(1, docbody1.ChildNodes.Length);
            Assert.AreEqual(0, docbody1.Attributes.Count);
            Assert.AreEqual("body", docbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, docbody1.NodeType);

            var docbody1Text0 = docbody1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, docbody1Text0.NodeType);
            Assert.AreEqual("setting html's innerHTML", docbody1Text0.TextContent);
        }

        [Test]
        public void FragmentHeadContextWithTextInTitle()
        {
            var doc = HtmlFragment(@"<title>setting head's innerHTML</title>", Create("head"));

            var doctitle0 = doc[0] as Element;
            Assert.AreEqual(1, doctitle0.ChildNodes.Length);
            Assert.AreEqual(0, doctitle0.Attributes.Count);
            Assert.AreEqual("title", doctitle0.GetTagName());
            Assert.AreEqual(NodeType.Element, doctitle0.NodeType);

            var doctitle0Text0 = doctitle0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, doctitle0Text0.NodeType);
            Assert.AreEqual("setting head's innerHTML", doctitle0Text0.TextContent);
        }

        [Test]
        public void FosterFragmentDoubleClosedBody()
        {
            var doc = HtmlFragment(@"<body>X</body></body>", Create("html"));

            var dochead0 = doc[0] as Element;
            Assert.AreEqual(0, dochead0.ChildNodes.Length);
            Assert.AreEqual(0, dochead0.Attributes.Count);
            Assert.AreEqual("head", dochead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochead0.NodeType);

            var docbody1 = doc[1] as Element;
            Assert.AreEqual(1, docbody1.ChildNodes.Length);
            Assert.AreEqual(0, docbody1.Attributes.Count);
            Assert.AreEqual("body", docbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, docbody1.NodeType);

            var docbody1Text0 = docbody1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, docbody1Text0.NodeType);
            Assert.AreEqual("X", docbody1Text0.TextContent);
        }

        [Test]
        public void FragmentButtonWithText()
        {
            var doc = HtmlFragment("<button>Boo!</button>");
            var buttonElement = doc.QuerySelector("button") as IHtmlButtonElement;

            Assert.IsNotNull(buttonElement);
            Assert.AreEqual("Boo!", buttonElement.TextContent);
        }

        [Test]
        public void FragmentButtonWithTextAndAttribute()
        {
            var doc = HtmlFragment("<button type=SEARCH>Boo!</button>");
            var buttonElement = doc.QuerySelector("button") as IHtmlButtonElement;

            Assert.IsNotNull(buttonElement);
            Assert.AreEqual("Boo!", buttonElement.TextContent);
            Assert.AreEqual("search", buttonElement.Type);
            Assert.AreEqual("SEARCH", buttonElement.GetAttribute("type"));
        }

        [Test]
        public void FragmentButtonDefaultSubmitType()
        {
            var doc = HtmlFragment("<button>Boo!</button>");
            var buttonElement = doc.QuerySelector("button") as IHtmlButtonElement;

            Assert.IsNotNull(buttonElement);
            Assert.AreEqual("Boo!", buttonElement.TextContent);
            Assert.AreEqual("submit", buttonElement.Type);
            Assert.IsFalse(buttonElement.HasAttribute("type"));
        }
        
        [Test]
        public void FragmentClassNameCaseNumbered()
        {
            var dom = HtmlFragment("<div class=\"class1 CLASS2 claSS3\" x=\"y\" />");
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
            var dom = HtmlFragment("<div class=\"class CLASS\" />");
            var el = dom.QuerySelector("div");

            Assert.IsNotNull(el);
            Assert.AreEqual(2, el.ClassList.Length);

            CollectionAssert.AreEqual(new List<String>(new[] { "class", "CLASS" }), new List<String>(el.ClassList));
        }

        [Test]
        public void FragmentUnquotedAttributeHandling()
        {
            var doc = HtmlFragment("<div custattribute=10/23/2012 id=\"tableSample\"><span>sample text</span></div>");
            var obj = doc.QuerySelector("#tableSample");

            Assert.AreEqual("10/23/2012", obj.GetAttribute("custattribute"));
        }

        [Test]
        public void FragmentCaretsInAttributes()
        {
            var doc = HtmlFragment("<div><img src=\"test.png\" alt=\">\" /></div>");
            var div = doc.QuerySelector("div");

            Assert.IsNotNull(div);
            Assert.AreEqual("<div><img src=\"test.png\" alt=\">\"></div>", div.OuterHtml);
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
            var dom = HtmlFragment(html);

            var body = dom.QuerySelector("body");
            Assert.IsNotNull(body);

            var output = body.InnerHtml.Replace("" + (Char)160, "&nbsp;");
            Assert.AreEqual(html, output);
        }

        [Test]
        public void FragmentClassAndStyleAsBoolean()
        {
            var html = @"<span class="""" style="""">Test </span><div class style><br /></div>";
            var dom = HtmlFragment(html);

            var body = dom.QuerySelector("body");
            Assert.IsNotNull(body);

            var output = body.InnerHtml.Replace("" + (char)160, "&nbsp;");
            Assert.AreEqual(@"<span class="""">Test </span><div class=""""><br></div>", output);
        }

        [Test]
        public void FragmentUtf8HighValuesConversion()
        {
            var html = @"<span>&#55449;&#56580;</span>";
            var dom = HtmlFragment(html);
            var span = dom.QuerySelector("span");

            Assert.IsNotNull(span);
            Assert.AreEqual("\uFFFD\uFFFD", span.InnerHtml);
        }
    }
}
