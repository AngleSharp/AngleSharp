using AngleSharp;
using AngleSharp.DOM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTests
{
    /// <summary>
    /// Tests taken (and ported) from
    /// https://github.com/google/gumbo-parser/blob/master/tests/parser.cc
    /// </summary>
    [TestClass]
    public class GumboParserTests
    {
        [TestMethod]
        public void GumboDoubleBody()
        {
            var html = DocumentBuilder.Html("<body class=first><body class=second id=merged>Text</body></body>");
            var root = html.Body;
            Assert.AreEqual(1, root.Childs.Length);
            Assert.AreEqual(2, root.Attributes.Count());

            var cls = root.ClassName;
            Assert.AreEqual("first", cls);

            var id = root.Id;
            Assert.AreEqual("merged", id);

            var txt = root.Childs[0];
            Assert.AreEqual(NodeType.Text, txt.NodeType);
            Assert.AreEqual("Text", txt.TextContent);
        }

        [TestMethod]
        public void GumboMisnestedHeading()
        {
            var html = DocumentBuilder.Html(@"<h1>  <section>    <h2>      <dl><dt>List    </h1>  </section>  Heading1<h3>Heading3</h4>After</h3> text");

            var root = html.Body;
            Assert.AreEqual(3, root.Childs.Length);

            var h1 = root.Childs[0];
            Assert.AreEqual(NodeType.Element, h1.NodeType);
            Assert.AreEqual("h1", h1.NodeName);
            Assert.AreEqual(3, h1.Childs.Length);

            var section = h1.Childs[1];
            Assert.AreEqual(NodeType.Element, section.NodeType);
            Assert.AreEqual("section", section.NodeName);
            Assert.AreEqual(3, section.Childs.Length);

            var h2 = section.Childs[1];
            Assert.AreEqual(NodeType.Element, h2.NodeType);
            Assert.AreEqual("h2", h2.NodeName);
            Assert.AreEqual(2, h2.Childs.Length);

            var dl = h2.Childs[1];
            Assert.AreEqual(NodeType.Element, dl.NodeType);
            Assert.AreEqual("dl", dl.NodeName);
            Assert.AreEqual(1, dl.Childs.Length);

            var dt = dl.Childs[0];
            Assert.AreEqual(NodeType.Element, dt.NodeType);
            Assert.AreEqual("dt", dt.NodeName);
            Assert.AreEqual(1, dt.Childs.Length);

            var text1 = dt.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("List    ", text1.TextContent);

            var text2 = h1.Childs[2];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual("  Heading1", text2.TextContent);

            var h3 = root.Childs[1];
            Assert.AreEqual(NodeType.Element, h3.NodeType);
            Assert.AreEqual("h3", h3.NodeName);
            Assert.AreEqual(1, h3.Childs.Length);

            var text3 = h3.Childs[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("Heading3", text3.TextContent);

            var text4 = root.Childs[2];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual("After text", text4.TextContent);
        }

        [TestMethod]
        public void GumboLinkifiedHeading()
        {
            var html = DocumentBuilder.Html(@"<li><h3><a href=#foo>Text</a></h3><div>Summary</div>");

            var root = html.Body;
            Assert.AreEqual(1, root.Childs.Length);

            var li = root.Childs[0];
            Assert.AreEqual(NodeType.Element, li.NodeType);
            Assert.AreEqual("li", li.NodeName);
            Assert.AreEqual(2, li.Childs.Length);

            var h3 = li.Childs[0];
            Assert.AreEqual(NodeType.Element, h3.NodeType);
            Assert.AreEqual("h3", h3.NodeName);
            Assert.AreEqual(1, h3.Childs.Length);

            var anchor = h3.Childs[0];
            Assert.AreEqual(NodeType.Element, anchor.NodeType);
            Assert.AreEqual("a", anchor.NodeName);
            Assert.AreEqual(1, anchor.Childs.Length);

            var div = li.Childs[1];
            Assert.AreEqual(NodeType.Element, div.NodeType);
            Assert.AreEqual("div", div.NodeName);
            Assert.AreEqual(1, div.Childs.Length);
        }

        [TestMethod]
        public void GumboFormattingTagsInHeading()
        {
            var html = DocumentBuilder.Html(@"<h2>This is <b>old</h2>text");

            var root = html.Body;
            Assert.AreEqual(2, root.Childs.Length);

            var h2 = root.Childs[0];
            Assert.AreEqual(NodeType.Element, h2.NodeType);
            Assert.AreEqual("h2", h2.NodeName);
            Assert.AreEqual(2, h2.Childs.Length);

            var text1 = h2.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("This is ", text1.TextContent);

            var b = h2.Childs[1];
            Assert.AreEqual(NodeType.Element, b.NodeType);
            Assert.AreEqual("b", b.NodeName);
            Assert.AreEqual(1, b.Childs.Length);

            var text2 = b.Childs[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual("old", text2.TextContent);

            var bimpl = root.Childs[1];
            Assert.AreEqual(NodeType.Element, bimpl.NodeType);
            Assert.AreEqual("b", bimpl.NodeName);
            Assert.AreEqual(1, bimpl.Childs.Length);

            var text3 = bimpl.Childs[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("text", text3.TextContent);
        }

        [TestMethod]
        public void GumboImplicitlyCloseLists()
        {
            var html = DocumentBuilder.Html(@"<ul>
  <li>First
  <li>Second
</ul>");

            var root = html.Body;
            Assert.AreEqual(1, root.Childs.Length);

            var ul = root.Childs[0];
            Assert.AreEqual(NodeType.Element, ul.NodeType);
            Assert.AreEqual("ul", ul.NodeName);
            Assert.AreEqual(3, ul.Childs.Length);

            var text = ul.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("\n  ", text.TextContent);

            var li1 = ul.Childs[1];
            Assert.AreEqual(NodeType.Element, li1.NodeType);
            Assert.AreEqual("li", li1.NodeName);
            Assert.AreEqual(1, li1.Childs.Length);

            var li2 = ul.Childs[2];
            Assert.AreEqual(NodeType.Element, li2.NodeType);
            Assert.AreEqual("li", li2.NodeName);
            Assert.AreEqual(1, li2.Childs.Length);
        }

        /// <summary>
        /// See http://www.whatwg.org/specs/web-apps/current-work/multipage/the-end.html#misnested-tags:-b-i-/b-/i
        /// </summary>
        [TestMethod]
        public void GumboAdoptionAgency1()
        {
            var html = DocumentBuilder.Html(@"<p>1<b>2<i>3</b>4</i>5</p>");

            var root = html.Body;
            Assert.AreEqual(1, root.Childs.Length);

            var p = root.Childs[0];
            Assert.AreEqual(NodeType.Element, p.NodeType);
            Assert.AreEqual("p", p.NodeName);
            Assert.AreEqual(4, p.Childs.Length);

            var text1 = p.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("1", text1.TextContent);

            var b = p.Childs[1];
            Assert.AreEqual(NodeType.Element, b.NodeType);
            Assert.AreEqual("b", b.NodeName);
            Assert.AreEqual(2, b.Childs.Length);

            var text2 = b.Childs[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual("2", text2.TextContent);

            var i = b.Childs[1];
            Assert.AreEqual(NodeType.Element, i.NodeType);
            Assert.AreEqual("i", i.NodeName);
            Assert.AreEqual(1, i.Childs.Length);

            var text3 = i.Childs[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("3", text3.TextContent);

            var iadopt = p.Childs[2];
            Assert.AreEqual(NodeType.Element, i.NodeType);
            Assert.AreEqual("i", iadopt.NodeName);
            Assert.AreEqual(1, iadopt.Childs.Length);

            var text4 = iadopt.Childs[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual("4", text4.TextContent);

            var text5 = p.Childs[3];
            Assert.AreEqual(NodeType.Text, text5.NodeType);
            Assert.AreEqual("5", text5.TextContent);
        }

        /// <summary>
        /// See http://www.whatwg.org/specs/web-apps/current-work/multipage/the-end.html#misnested-tags:-b-p-/b-/p
        /// </summary>
        [TestMethod]
        public void GumboAdoptionAgency2()
        {
            var html = DocumentBuilder.Html(@"<b>1<p>2</b>3</p>");

            var root = html.Body;
            Assert.AreEqual(2, root.Childs.Length);

            var b = root.Childs[0];
            Assert.AreEqual(NodeType.Element, b.NodeType);
            Assert.AreEqual("b", b.NodeName);
            Assert.AreEqual(1, b.Childs.Length);

            var text1 = b.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("1", text1.TextContent);

            var p = root.Childs[1];
            Assert.AreEqual(NodeType.Element, p.NodeType);
            Assert.AreEqual("p", p.NodeName);
            Assert.AreEqual(2, p.Childs.Length);

            var badopt = p.Childs[0];
            Assert.AreEqual(NodeType.Element, badopt.NodeType);
            Assert.AreEqual("b", badopt.NodeName);
            Assert.AreEqual(1, badopt.Childs.Length);

            var text2 = badopt.Childs[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual("2", text2.TextContent);

            var text3 = p.Childs[1];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("3", text3.TextContent);
        }

        [TestMethod]
        public void GumboMetaBeforeHead()
        {
            var html = DocumentBuilder.Html(@"<html><meta http-equiv='content-type' content='text/html; charset=UTF-8' /><head></head>");

            var root = html.Body;
            Assert.IsNotNull(root);
        }

        [TestMethod]
        public void GumboNoahsArkClause()
        {
            var html = DocumentBuilder.Html(@"<p><font size=4><font color=red><font size=4><font size=4><font size=4><font size=4><font size=4><font color=red><p>X");

            var root = html.Body;
            Assert.AreEqual(2, root.Childs.Length);

            var p1 = root.Childs[0];
            Assert.AreEqual(NodeType.Element, p1.NodeType);
            Assert.AreEqual("p", p1.NodeName);
            Assert.AreEqual(1, p1.Childs.Length);

            var size1 = p1.Childs[0];
            var red1 = size1.Childs[0] as Element;
            Assert.AreEqual(NodeType.Element, red1.NodeType);
            Assert.AreEqual("font", red1.NodeName);
            Assert.AreEqual(1, red1.Attributes.Count());
            Assert.IsNotNull(red1.GetAttribute("color"));
            Assert.AreEqual("red", red1.GetAttribute("color"));
            Assert.AreEqual(1, red1.Childs.Length);

            var p2 = root.Childs[1];
            Assert.AreEqual(NodeType.Element, p2.NodeType);
            Assert.AreEqual("p", p2.NodeName);
            Assert.AreEqual(1, p2.Childs.Length);

            var red2 = p2.Childs[0] as Element;
            Assert.AreEqual(NodeType.Element, red2.NodeType);
            Assert.AreEqual("font", red2.NodeName);
            Assert.AreEqual(1, red2.Attributes.Count());
            Assert.IsNotNull(red2.GetAttribute("color"));
            Assert.AreEqual("red", red2.GetAttribute("color"));
            Assert.AreEqual(1, red2.Childs.Length);
        }

        [TestMethod]
        public void GumboRawtextInBody()
        {
            var html = DocumentBuilder.Html(@"<body><noembed jsif=false></noembed>");

            var root = html.Body;
            Assert.AreEqual(1, root.Childs.Length);

            var noembed = root.Childs[0] as Element;
            Assert.AreEqual(NodeType.Element, noembed.NodeType);
            Assert.AreEqual("noembed", noembed.NodeName);
            Assert.AreEqual(1, noembed.Attributes.Count());
        }

        [TestMethod]
        public void GumboNestedRawtextTags()
        {
            var html = DocumentBuilder.Html(@"<noscript><noscript jstag=false><style>div{text-align:center}</style></noscript>");

            Assert.AreEqual(2, html.DocumentElement.Childs.Length);

            var head = html.Head;
            Assert.AreEqual(1, head.Childs.Length);

            var noscript = head.Childs[0];
            Assert.AreEqual(NodeType.Element, noscript.NodeType);
            Assert.AreEqual("noscript", noscript.NodeName);
            Assert.AreEqual(1, noscript.Childs.Length);

            var style = noscript.Childs[0];
            Assert.AreEqual(NodeType.Element, style.NodeType);
            Assert.AreEqual("style", style.NodeName);
            Assert.AreEqual(1, style.Childs.Length);

            var text = style.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("div{text-align:center}", text.TextContent);
        }

        [TestMethod]
        public void GumboIsIndex()
        {
            var html = DocumentBuilder.Html(@"<isindex id=form1 action='/action' prompt='Secret Message'>");

            var body = html.Body;
            Assert.AreEqual(1, body.Childs.Length);

            var form = body.Childs[0] as Element;
            Assert.AreEqual(NodeType.Element, form.NodeType);
            Assert.AreEqual("form", form.NodeName);
            Assert.AreEqual(3, form.Childs.Length);

            var action = form.GetAttribute("action");
            Assert.IsNotNull(action);
            Assert.AreEqual("/action", action);

            var hr1 = form.Childs[0];
            Assert.AreEqual(NodeType.Element, hr1.NodeType);
            Assert.AreEqual("hr", hr1.NodeName);
            Assert.AreEqual(0, hr1.Childs.Length);

            var label = form.Childs[1];
            Assert.AreEqual(NodeType.Element, label.NodeType);
            Assert.AreEqual("label", label.NodeName);
            Assert.AreEqual(2, label.Childs.Length);

            var text = label.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("Secret Message", text.TextContent);

            var input = label.Childs[1] as Element;
            Assert.AreEqual(NodeType.Element, input.NodeType);
            Assert.AreEqual("input", input.NodeName);
            Assert.AreEqual(0, input.Childs.Length);
            Assert.AreEqual(2, input.Attributes.Count());

            var id = input.GetAttribute("id");
            Assert.IsNotNull(id);
            Assert.AreEqual("form1", id);

            var name = input.GetAttribute("name");
            Assert.IsNotNull(name);
            Assert.AreEqual("isindex", name);

            var hr2 = form.Childs[2];
            Assert.AreEqual(NodeType.Element, hr2.NodeType);
            Assert.AreEqual("hr", hr2.NodeName);
            Assert.AreEqual(0, hr2.Childs.Length);
        }

        [TestMethod]
        public void GumboForm()
        {
            var html = DocumentBuilder.Html(@"<form><input type=hidden /><isindex /></form>After form");

            var body = html.Body;
            Assert.AreEqual(2, body.Childs.Length);

            var form = body.Childs[0];
            Assert.AreEqual(NodeType.Element, form.NodeType);
            Assert.AreEqual("form", form.NodeName);
            Assert.AreEqual(1, form.Childs.Length);

            var input = form.Childs[0];
            Assert.AreEqual(NodeType.Element, input.NodeType);
            Assert.AreEqual("input", input.NodeName);
            Assert.AreEqual(0, input.Childs.Length);

            var text = body.Childs[1];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("After form", text.TextContent);
        }

        [TestMethod]
        public void GumboNestedForm()
        {
            var html = DocumentBuilder.Html(@"<form><label>Label</label><form><input id=input2></form>After form");

            var body = html.Body;
            Assert.AreEqual(2, body.Childs.Length);

            var form = body.Childs[0];
            Assert.AreEqual(NodeType.Element, form.NodeType);
            Assert.AreEqual("form", form.NodeName);
            Assert.AreEqual(2, form.Childs.Length);

            var label = form.Childs[0];
            Assert.AreEqual(NodeType.Element, label.NodeType);
            Assert.AreEqual("label", label.NodeName);
            Assert.AreEqual(1, label.Childs.Length);

            var input = form.Childs[1];
            Assert.AreEqual(NodeType.Element, input.NodeType);
            Assert.AreEqual("input", input.NodeName);
            Assert.AreEqual(0, input.Childs.Length);

            var text = body.Childs[1];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("After form", text.TextContent);
        }

        [TestMethod]
        public void GumboMisnestedFormInTable()
        {
            var html = DocumentBuilder.Html(@"<table><tr><td><form><table><tr><td></td></tr></form><form></tr></table></form></td></tr></table>");

            var body = html.Body;
            Assert.AreEqual(1, body.Childs.Length);

            var table1 = body.Childs[0];
            Assert.AreEqual(NodeType.Element, table1.NodeType);
            Assert.AreEqual("table", table1.NodeName);
            Assert.AreEqual(1, table1.Childs.Length);

            var tbody1 = table1.Childs[0];
            Assert.AreEqual(NodeType.Element, tbody1.NodeType);
            Assert.AreEqual("tbody", tbody1.NodeName);
            Assert.AreEqual(1, tbody1.Childs.Length);

            var tr1 = tbody1.Childs[0];
            Assert.AreEqual(NodeType.Element, tr1.NodeType);
            Assert.AreEqual("tr", tr1.NodeName);
            Assert.AreEqual(1, tr1.Childs.Length);

            var td1 = tr1.Childs[0];
            Assert.AreEqual(NodeType.Element, td1.NodeType);
            Assert.AreEqual("td", td1.NodeName);
            Assert.AreEqual(1, td1.Childs.Length);

            var form1 = td1.Childs[0];
            Assert.AreEqual(NodeType.Element, form1.NodeType);
            Assert.AreEqual("form", form1.NodeName);
            Assert.AreEqual(1, form1.Childs.Length);

            var table2 = form1.Childs[0];
            Assert.AreEqual(NodeType.Element, table2.NodeType);
            Assert.AreEqual("table", table2.NodeName);
            Assert.AreEqual(1, table2.Childs.Length);

            var tbody2 = table2.Childs[0];
            Assert.AreEqual(NodeType.Element, tbody2.NodeType);
            Assert.AreEqual("tbody", tbody2.NodeName);
            Assert.AreEqual(2, tbody2.Childs.Length);

            var tr2 = tbody2.Childs[0];
            Assert.AreEqual(NodeType.Element, tr2.NodeType);
            Assert.AreEqual("tr", tr2.NodeName);
            Assert.AreEqual(1, tr2.Childs.Length);

            var form2 = tbody2.Childs[1];
            Assert.AreEqual(NodeType.Element, form2.NodeType);
            Assert.AreEqual("form", form2.NodeName);
            Assert.AreEqual(0, form2.Childs.Length);
        }

        [TestMethod]
        public void GumboImplicitColgroup()
        {
            var html = DocumentBuilder.Html(@"<table><col /><col /></table>");

            var body = html.Body;
            Assert.AreEqual(1, body.Childs.Length);

            var table = body.Childs[0];
            Assert.AreEqual(NodeType.Element, table.NodeType);
            Assert.AreEqual("table", table.NodeName);
            Assert.AreEqual(1, table.Childs.Length);

            var colgroup = table.Childs[0];
            Assert.AreEqual(NodeType.Element, colgroup.NodeType);
            Assert.AreEqual("colgroup", colgroup.NodeName);
            Assert.AreEqual(2, colgroup.Childs.Length);

            var col1 = colgroup.Childs[0];
            Assert.AreEqual(NodeType.Element, col1.NodeType);
            Assert.AreEqual("col", col1.NodeName);
            Assert.AreEqual(0, col1.Childs.Length);

            var col2 = colgroup.Childs[1];
            Assert.AreEqual(NodeType.Element, col2.NodeType);
            Assert.AreEqual("col", col2.NodeName);
            Assert.AreEqual(0, col2.Childs.Length);
        }

        [TestMethod]
        public void GumboSelectInTable()
        {
            var html = DocumentBuilder.Html(@"<table><td><select><option value=1></table>");

            var body = html.Body;
            Assert.AreEqual(1, body.Childs.Length);

            var table = body.Childs[0];
            Assert.AreEqual(NodeType.Element, table.NodeType);
            Assert.AreEqual("table", table.NodeName);
            Assert.AreEqual(1, table.Childs.Length);

            var tbody = table.Childs[0];
            Assert.AreEqual(NodeType.Element, tbody.NodeType);
            Assert.AreEqual("tbody", tbody.NodeName);
            Assert.AreEqual(1, tbody.Childs.Length);

            var tr = tbody.Childs[0];
            Assert.AreEqual(NodeType.Element, tr.NodeType);
            Assert.AreEqual("tr", tr.NodeName);
            Assert.AreEqual(1, tr.Childs.Length);

            var td = tr.Childs[0];
            Assert.AreEqual(NodeType.Element, td.NodeType);
            Assert.AreEqual("td", td.NodeName);
            Assert.AreEqual(1, td.Childs.Length);

            var select = td.Childs[0];
            Assert.AreEqual(NodeType.Element, select.NodeType);
            Assert.AreEqual("select", select.NodeName);
            Assert.AreEqual(1, select.Childs.Length);

            var option = select.Childs[0];
            Assert.AreEqual(NodeType.Element, option.NodeType);
            Assert.AreEqual("option", option.NodeName);
            Assert.AreEqual(0, option.Childs.Length);
        }

        [TestMethod]
        public void GumboComplicatedSelect()
        {
            var html = DocumentBuilder.Html(@"<select><div class=foo></div><optgroup><option>Option</option><input></optgroup></select>");

            var body = html.Body;
            Assert.AreEqual(2, body.Childs.Length);

            var select = body.Childs[0];
            Assert.AreEqual(NodeType.Element, select.NodeType);
            Assert.AreEqual("select", select.NodeName);
            Assert.AreEqual(1, select.Childs.Length);

            var optgroup = select.Childs[0];
            Assert.AreEqual(NodeType.Element, optgroup.NodeType);
            Assert.AreEqual("optgroup", optgroup.NodeName);
            Assert.AreEqual(1, optgroup.Childs.Length);

            var option = optgroup.Childs[0];
            Assert.AreEqual(NodeType.Element, option.NodeType);
            Assert.AreEqual("option", option.NodeName);
            Assert.AreEqual(1, option.Childs.Length);

            var text = option.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("Option", text.TextContent);

            var input = body.Childs[1];
            Assert.AreEqual(NodeType.Element, input.NodeType);
            Assert.AreEqual("input", input.NodeName);
            Assert.AreEqual(0, input.Childs.Length);
        }

        [TestMethod]
        public void GumboDoubleSelect()
        {
            var html = DocumentBuilder.Html(@"<select><select><div></div>");

            var body = html.Body;
            Assert.AreEqual(2, body.Childs.Length);

            var select = body.Childs[0];
            Assert.AreEqual(NodeType.Element, select.NodeType);
            Assert.AreEqual("select", select.NodeName);
            Assert.AreEqual(0, select.Childs.Length);

            var div = body.Childs[1];
            Assert.AreEqual(NodeType.Element, div.NodeType);
            Assert.AreEqual("div", div.NodeName);
            Assert.AreEqual(0, div.Childs.Length);
        }

        [TestMethod]
        public void GumboInputInSelect()
        {
            var html = DocumentBuilder.Html(@"<select><input /><div></div>");

            var body = html.Body;
            Assert.AreEqual(3, body.Childs.Length);

            var select = body.Childs[0];
            Assert.AreEqual(NodeType.Element, select.NodeType);
            Assert.AreEqual("select", select.NodeName);
            Assert.AreEqual(0, select.Childs.Length);

            var input = body.Childs[1];
            Assert.AreEqual(NodeType.Element, input.NodeType);
            Assert.AreEqual("input", input.NodeName);
            Assert.AreEqual(0, input.Childs.Length);

            var div = body.Childs[2];
            Assert.AreEqual(NodeType.Element, div.NodeType);
            Assert.AreEqual("div", div.NodeName);
            Assert.AreEqual(0, div.Childs.Length);
        }

        [TestMethod]
        public void GumboNullDocument()
        {
            var html = DocumentBuilder.Html(@"");
            Assert.IsNotNull(html);
            var body = html.Body;
            Assert.IsNotNull(body);
        }

        [TestMethod]
        public void GumboOneChar()
        {
            var html = DocumentBuilder.Html(@"T");
            Assert.AreEqual(1, html.Childs.Length);

            var root = html.DocumentElement;
            Assert.AreEqual("html", root.NodeName);
            Assert.AreEqual(NodeType.Element, root.NodeType);
            Assert.AreEqual(2, root.Childs.Length);

            var head = root.Childs[0];
            Assert.AreEqual("head", head.NodeName);
            Assert.AreEqual(NodeType.Element, head.NodeType);
            Assert.AreEqual(0, head.Childs.Length);

            var body = root.Childs[1];
            Assert.AreEqual("body", body.NodeName);
            Assert.AreEqual(NodeType.Element, body.NodeType);
            Assert.AreEqual(1, body.Childs.Length);

            var text = body.Childs[0];
            Assert.AreEqual("T", text.TextContent);
            Assert.AreEqual(NodeType.Text, text.NodeType);
        }

        [TestMethod]
        public void GumboTextOnly()
        {
            var html = DocumentBuilder.Html(@"Test");
            Assert.AreEqual(1, html.Childs.Length);

            var root = html.DocumentElement;
            Assert.AreEqual("html", root.NodeName);
            Assert.AreEqual(NodeType.Element, root.NodeType);
            Assert.AreEqual(2, root.Childs.Length);

            var head = root.Childs[0];
            Assert.AreEqual("head", head.NodeName);
            Assert.AreEqual(NodeType.Element, head.NodeType);
            Assert.AreEqual(0, head.Childs.Length);

            var body = root.Childs[1];
            Assert.AreEqual("body", body.NodeName);
            Assert.AreEqual(NodeType.Element, body.NodeType);
            Assert.AreEqual(1, body.Childs.Length);

            var text = body.Childs[0];
            Assert.AreEqual("Test", text.TextContent);
            Assert.AreEqual(NodeType.Text, text.NodeType);
        }

        [TestMethod]
        public void GumboUnexpectedEndBreak()
        {
            var html = DocumentBuilder.Html(@"</br><div></div>");

            var body = html.Body;
            Assert.AreEqual(2, body.Childs.Length);

            var br = body.Childs[0];
            Assert.AreEqual("br", br.NodeName);
            Assert.AreEqual(NodeType.Element, br.NodeType);
            Assert.AreEqual(0, br.Childs.Length);

            var div = body.Childs[1];
            Assert.AreEqual("div", div.NodeName);
            Assert.AreEqual(NodeType.Element, div.NodeType);
            Assert.AreEqual(0, div.Childs.Length);
        }

        [TestMethod]
        public void GumboCaseSensitiveAttributesCamelCase()
        {
            var html = DocumentBuilder.Html(@"<div class=camelCase>");

            var body = html.Body;
            Assert.AreEqual(1, body.Childs.Length);

            var div = body.Childs[0] as Element;
            Assert.AreEqual("div", div.NodeName);
            Assert.AreEqual(NodeType.Element, div.NodeType);
            Assert.AreEqual(0, div.Childs.Length);
            Assert.AreEqual(1, div.Attributes.Count());
            Assert.AreEqual("camelCase", div.GetAttribute("class"));
        }

        [TestMethod]
        public void GumboCaseSensitiveAttributesPascalCase()
        {
            var html = DocumentBuilder.Html(@"<div class=PascalCase>");

            var body = html.Body;
            Assert.AreEqual(1, body.Childs.Length);

            var div = body.Childs[0] as Element;
            Assert.AreEqual("div", div.NodeName);
            Assert.AreEqual(NodeType.Element, div.NodeType);
            Assert.AreEqual(0, div.Childs.Length);
            Assert.AreEqual(1, div.Attributes.Count());
            Assert.AreEqual("PascalCase", div.GetAttribute("class"));
        }

        [TestMethod]
        public void GumboExplicitHtmlStructure()
        {
            var html = DocumentBuilder.Html(@"<!doctype html>
<html><head><title>Foo</title></head>
<body><div class=bar>Test</div></body></html>");

            Assert.AreEqual(2, html.Childs.Length);

            var root = html.Childs[1];
            Assert.AreEqual(NodeType.Element, root.NodeType);
            Assert.AreEqual("html", root.NodeName);
            Assert.AreEqual(3, root.Childs.Length);

            var head = root.Childs[0];
            Assert.AreEqual(NodeType.Element, head.NodeType);
            Assert.AreEqual("head", head.NodeName);
            Assert.AreEqual(root, head.ParentElement);
            Assert.AreEqual(1, head.Childs.Length);

            var body = root.Childs[2];
            Assert.AreEqual(NodeType.Element, body.NodeType);
            Assert.AreEqual("body", body.NodeName);
            Assert.AreEqual(root, body.ParentElement);
            Assert.AreEqual(1, body.Childs.Length);

            var div = body.Childs[0] as Element;
            Assert.AreEqual(NodeType.Element, div.NodeType);
            Assert.AreEqual("div", div.NodeName);
            Assert.AreEqual(body, div.ParentElement);
            Assert.AreEqual(1, div.Childs.Length);
            Assert.AreEqual(1, div.Attributes.Count());

            var clas = div.Attributes.First();
            Assert.AreEqual("class", clas.Name);
            Assert.AreEqual("bar", clas.Value);

            var text = div.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("Test", text.TextContent);
        }

        [TestMethod]
        public void GumboDuplicateAttributes()
        {
            var html = DocumentBuilder.Html(@"<input checked=""false"" checked id=foo id='bar'>");

            var body = html.Body;
            Assert.AreEqual(NodeType.Element, body.NodeType);
            Assert.AreEqual("body", body.NodeName);
            Assert.AreEqual(1, body.Childs.Length);

            var input = body.Childs[0] as Element;
            Assert.AreEqual(NodeType.Element, input.NodeType);
            Assert.AreEqual("input", input.NodeName);
            Assert.AreEqual(0, input.Childs.Length);
            Assert.AreEqual(2, input.Attributes.Count());

            var chked = input.GetAttribute("checked");
            Assert.AreEqual("false", chked);

            var id = input.GetAttribute("id");
            Assert.AreEqual("foo", id);
        }

        [TestMethod]
        public void GumboLinkTagsInHead()
        {
            var html = DocumentBuilder.Html(@"<html>
  <head>
    <title>Sample title></title>

    <link rel=stylesheet>
    <link rel=author>
  </head>
  <body>Foo</body>");

            var root = html.DocumentElement;
            Assert.AreEqual(3, root.Childs.Length);

            var head = html.Head;
            Assert.AreEqual(NodeType.Element, head.NodeType);
            Assert.AreEqual("head", head.NodeName);
            Assert.AreEqual(7, head.Childs.Length);

            var text1 = head.Childs[2];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("\n\n    ", text1.TextContent);

            var link1 = head.Childs[3];
            Assert.AreEqual(NodeType.Element, link1.NodeType);
            Assert.AreEqual("link", link1.NodeName);
            Assert.AreEqual(0, link1.Childs.Length);

            var text2 = head.Childs[4];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual("\n    ", text2.TextContent);

            var link2 = head.Childs[5];
            Assert.AreEqual(NodeType.Element, link2.NodeType);
            Assert.AreEqual("link", link2.NodeName);
            Assert.AreEqual(0, link2.Childs.Length);

            var text3 = head.Childs[6];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("\n  ", text3.TextContent);

            var body = html.Body;
            Assert.AreEqual(NodeType.Element, body.NodeType);
            Assert.AreEqual("body", body.NodeName);
            Assert.AreEqual(1, body.Childs.Length);
        }

        [TestMethod]
        public void GumboTextAfterHtml()
        {
            var html = DocumentBuilder.Html(@"<html>Test</html> after doc");

            var body = html.Body;
            Assert.AreEqual(1, body.Childs.Length);

            var text = body.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("Test after doc", text.TextContent);
        }

        [TestMethod]
        public void GumboWhitespaceInHead()
        {
            var html = DocumentBuilder.Html(@"<html>  Test</html>");

            var root = html.DocumentElement;
            Assert.AreEqual(2, root.Childs.Length);

            var head = html.Head;
            Assert.AreEqual(0, head.Childs.Length);

            var body = html.Body;
            Assert.AreEqual(1, body.Childs.Length);

            var text = body.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("Test", text.TextContent);
        }

        [TestMethod]
        public void GumboDoctype()
        {
            var html = DocumentBuilder.Html(@"<!doctype html>Test");
            Assert.AreEqual(AngleSharp.DOM.QuirksMode.Off, html.QuirksMode);
            Assert.AreEqual(2, html.Childs.Length);

            var doctype = html.Doctype;
            Assert.AreEqual("html", doctype.Name);
            Assert.AreEqual(String.Empty, doctype.PublicId);
            Assert.AreEqual(String.Empty, doctype.SystemId);
        }

        [TestMethod]
        public void GumboInvalidDoctype()
        {
            var html = DocumentBuilder.Html(@"Test<!doctype root_element SYSTEM ""DTD_location"">");
            Assert.AreEqual(AngleSharp.DOM.QuirksMode.On, html.QuirksMode);
            Assert.AreEqual(1, html.Childs.Length);

            Assert.IsNull(html.Doctype);

            var body = html.Body;
            Assert.AreEqual(1, body.Childs.Length);

            var text = body.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("Test", text.TextContent);
        }

        [TestMethod]
        public void GumboCommentInVerbatimMode()
        {
            var doc = DocumentBuilder.Html(@"<body> <div id='onegoogle'>Text</div>  </body><!-- comment 

-->");
            var html = doc.DocumentElement;
            Assert.AreEqual(NodeType.Element, html.NodeType);
            Assert.AreEqual("html", html.NodeName);
            Assert.AreEqual(3, html.Childs.Length);

            var body = html.Childs[1];
            Assert.AreEqual(NodeType.Element, body.NodeType);
            Assert.AreEqual("body", body.NodeName);
            Assert.AreEqual(3, body.Childs.Length);

            var comment = html.Childs[2];
            Assert.AreEqual(NodeType.Comment, comment.NodeType);
            Assert.AreEqual(" comment \n\n", comment.TextContent);
        }

        [TestMethod]
        public void GumboCommentInText()
        {
            var doc = DocumentBuilder.Html(@"Start <!-- comment --> end");
            var body = doc.Body;
            Assert.AreEqual(3, body.Childs.Length);

            var start = body.Childs[0];
            Assert.AreEqual(NodeType.Text, start.NodeType);
            Assert.AreEqual("Start ", start.TextContent);

            var comment = body.Childs[1];
            Assert.AreEqual(NodeType.Comment, comment.NodeType);
            Assert.AreEqual(body, comment.ParentElement);
            Assert.AreEqual(" comment ", comment.TextContent);

            var end = body.Childs[2];
            Assert.AreEqual(NodeType.Text, end.NodeType);
            Assert.AreEqual(" end", end.TextContent);
        }

        [TestMethod]
        public void GumboUnknownTag1()
        {
            var doc = DocumentBuilder.Html(@"<foo>1<p>2</FOO>");
            var body = doc.Body;
            Assert.AreEqual(1, body.Childs.Length);

            var foo = body.Childs[0];
            Assert.AreEqual(NodeType.Element, foo.NodeType);
            Assert.AreEqual("foo", foo.NodeName);
            Assert.AreEqual(typeof(AngleSharp.DOM.Html.HTMLUnknownElement), foo.GetType());
        }

        [TestMethod]
        public void GumboUnknownTag2()
        {
            var doc = DocumentBuilder.Html(@"<div><sarcasm><div></div></sarcasm></div>");
            var body = doc.Body;
            Assert.AreEqual(1, body.Childs.Length); 
            
            var div = body.Childs[0];
            Assert.AreEqual(1, div.Childs.Length);

            var sarcasm = div.Childs[0];
            Assert.AreEqual(NodeType.Element, sarcasm.NodeType);
            Assert.AreEqual("sarcasm", sarcasm.NodeName);
            Assert.AreEqual(typeof(AngleSharp.DOM.Html.HTMLUnknownElement), sarcasm.GetType());
        }

        [TestMethod]
        public void GumboInvalidEndTag()
        {
            var doc = DocumentBuilder.Html(@"<a><img src=foo.jpg></img></a>");
            var body = doc.Body;
            Assert.AreEqual(1, body.Childs.Length);

            var a = body.Childs[0];
            Assert.AreEqual(NodeType.Element, a.NodeType);
            Assert.AreEqual("a", a.NodeName);
            Assert.AreEqual(1, a.Childs.Length);

            var img = a.Childs[0];
            Assert.AreEqual(NodeType.Element, img.NodeType);
            Assert.AreEqual("img", img.NodeName);
            Assert.AreEqual(0, img.Childs.Length);
        }

        [TestMethod]
        public void GumboTables()
        {
            var doc = DocumentBuilder.Html(@"<html><table>
  <tr><br /></invalid-tag>
    <th>One</th>
    <td>Two</td>
  </tr>
  <iframe></iframe>
</table><tr></tr><div></div></html>");
            var body = doc.Body;
            Assert.AreEqual(4, body.Childs.Length);

            var br = body.Childs[0];
            Assert.AreEqual(NodeType.Element, br.NodeType);
            Assert.AreEqual("br", br.NodeName);
            Assert.AreEqual(body, br.ParentElement);
            Assert.AreEqual(0, br.Childs.Length);

            var iframe = body.Childs[1];
            Assert.AreEqual(NodeType.Element, iframe.NodeType);
            Assert.AreEqual("iframe", iframe.NodeName);
            Assert.AreEqual(0, iframe.Childs.Length);

            var table = body.Childs[2];
            Assert.AreEqual(NodeType.Element, table.NodeType);
            Assert.AreEqual("table", table.NodeName);
            Assert.AreEqual(body, table.ParentElement);
            Assert.AreEqual(2, table.Childs.Length);

            var table_text = table.Childs[0];
            Assert.AreEqual(NodeType.Text, table_text.NodeType);
            Assert.AreEqual("\n  ", table_text.TextContent);

            var tbody = table.Childs[1];
            Assert.AreEqual(NodeType.Element, tbody.NodeType);
            Assert.AreEqual("tbody", tbody.NodeName);
            Assert.AreEqual(2, tbody.Childs.Length);

            var tr = tbody.Childs[0];
            Assert.AreEqual(NodeType.Element, tr.NodeType);
            Assert.AreEqual("tr", tr.NodeName);
            Assert.AreEqual(5, tr.Childs.Length);

            var tr_text = tr.Childs[0];
            Assert.AreEqual(NodeType.Text, tr_text.NodeType);
            Assert.AreEqual(tr, tr_text.ParentElement);
            Assert.AreEqual("\n    ", tr_text.TextContent);

            var th = tr.Childs[1];
            Assert.AreEqual(NodeType.Element, th.NodeType);
            Assert.AreEqual("th", th.NodeName);
            Assert.AreEqual(tr, th.ParentElement);
            Assert.AreEqual(1, th.Childs.Length);

            var th_text = th.Childs[0];
            Assert.AreEqual(NodeType.Text, th_text.NodeType);
            Assert.AreEqual("One", th_text.TextContent);

            var td = tr.Childs[3];
            Assert.AreEqual(NodeType.Element, td.NodeType);
            Assert.AreEqual("td", td.NodeName);
            Assert.AreEqual(1, td.Childs.Length);

            var td_text = td.Childs[0];
            Assert.AreEqual(NodeType.Text, td_text.NodeType);
            Assert.AreEqual("Two", td_text.TextContent);

            var div = body.Childs[3];
            Assert.AreEqual(NodeType.Element, div.NodeType);
            Assert.AreEqual("div", div.NodeName);
            Assert.AreEqual(0, div.Childs.Length);
        }

        [TestMethod]
        public void GumboStartParagraphInTable()
        {
            var doc = DocumentBuilder.Html(@"<table><P></tr></td>foo</table>");
            var body = doc.Body;
            Assert.AreEqual(2, body.Childs.Length);

            var paragraph = body.Childs[0];
            Assert.AreEqual(NodeType.Element, paragraph.NodeType);
            Assert.AreEqual("p", paragraph.NodeName);
            Assert.AreEqual(body, paragraph.ParentElement);
            Assert.AreEqual(1, paragraph.Childs.Length);

            var text = paragraph.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("foo", text.TextContent);

            var table = body.Childs[1];
            Assert.AreEqual(NodeType.Element, table.NodeType);
            Assert.AreEqual("table", table.NodeName);
            Assert.AreEqual(body, table.ParentElement);
            Assert.AreEqual(0, table.Childs.Length);
        }

        [TestMethod]
        public void GumboEndParagraphInTable()
        {
            var doc = DocumentBuilder.Html(@"<table></p></table>");
            var body = doc.Body;
            Assert.AreEqual(2, body.Childs.Length);

            var paragraph = body.Childs[0];
            Assert.AreEqual(NodeType.Element, paragraph.NodeType);
            Assert.AreEqual("p", paragraph.NodeName);
            Assert.AreEqual(body, paragraph.ParentElement);
            Assert.AreEqual(0, paragraph.Childs.Length);

            var table = body.Childs[1];
            Assert.AreEqual(NodeType.Element, table.NodeType);
            Assert.AreEqual("table", table.NodeName);
            Assert.AreEqual(body, table.ParentElement);
            Assert.AreEqual(0, table.Childs.Length);
        }

        [TestMethod]
        public void GumboUnclosedTableTags()
        {
            var doc = DocumentBuilder.Html(@"<html><table>
  <tr>
    <td>One
    <td>Two
  <tr><td>Row2
  <tr><td>Row3
</table>
</html>");
            var body = doc.Body;
            Assert.AreEqual(2, body.Childs.Length);

            var table = body.Childs[0];
            Assert.AreEqual(NodeType.Element, table.NodeType);
            Assert.AreEqual("table", table.NodeName);
            Assert.AreEqual(body, table.ParentElement);
            Assert.AreEqual(2, table.Childs.Length);

            var table_text = table.Childs[0];
            Assert.AreEqual(NodeType.Text, table_text.NodeType);
            Assert.AreEqual("\n  ", table_text.TextContent);

            var tbody = table.Childs[1];
            Assert.AreEqual(NodeType.Element, tbody.NodeType);
            Assert.AreEqual("tbody", tbody.NodeName);
            Assert.AreEqual(3, tbody.Childs.Length);

            var tr = tbody.Childs[0];
            Assert.AreEqual(NodeType.Element, tr.NodeType);
            Assert.AreEqual("tr", tr.NodeName);
            Assert.AreEqual(3, tr.Childs.Length);

            var tr_text = tr.Childs[0];
            Assert.AreEqual(NodeType.Text, tr_text.NodeType);
            Assert.AreEqual("\n    ", tr_text.TextContent);

            var td1 = tr.Childs[1];
            Assert.AreEqual(NodeType.Element, td1.NodeType);
            Assert.AreEqual("td", td1.NodeName);
            Assert.AreEqual(1, td1.Childs.Length);

            var td2 = tr.Childs[2];
            Assert.AreEqual(NodeType.Element, td1.NodeType);
            Assert.AreEqual("td", td1.NodeName);
            Assert.AreEqual(1, td1.Childs.Length);

            var td1_text = td1.Childs[0];
            Assert.AreEqual(NodeType.Text, td1_text.NodeType);
            Assert.AreEqual("One\n    ", td1_text.TextContent);

            var td2_text = td2.Childs[0];
            Assert.AreEqual(NodeType.Text, td2_text.NodeType);
            Assert.AreEqual("Two\n  ", td2_text.TextContent);

            var tr3 = tbody.Childs[2];
            Assert.AreEqual(NodeType.Element, tr3.NodeType);
            Assert.AreEqual("tr", tr3.NodeName);
            Assert.AreEqual(1, tr3.Childs.Length);

            var body_text = body.Childs[1];
            Assert.AreEqual(NodeType.Text, body_text.NodeType);
            Assert.AreEqual("\n", body_text.TextContent);
        }

        [TestMethod]
        public void GumboMisnestedTable1()
        {
            var doc = DocumentBuilder.Html(@"<table><tr><div><td></div></table>");
            var body = doc.Body;
            Assert.AreEqual(2, body.Childs.Length);

            var div = body.Childs[0];
            Assert.AreEqual(NodeType.Element, div.NodeType);
            Assert.AreEqual("div", div.NodeName);
            Assert.AreEqual(0, div.Childs.Length);

            var table = body.Childs[1];
            Assert.AreEqual(NodeType.Element, table.NodeType);
            Assert.AreEqual("table", table.NodeName);
            Assert.AreEqual(body, table.ParentElement);
            Assert.AreEqual(1, table.Childs.Length);

            var tbody = table.Childs[0];
            Assert.AreEqual(NodeType.Element, tbody.NodeType);
            Assert.AreEqual("tbody", tbody.NodeName);
            Assert.AreEqual(1, tbody.Childs.Length);

            var tr = tbody.Childs[0];
            Assert.AreEqual(NodeType.Element, tr.NodeType);
            Assert.AreEqual("tr", tr.NodeName);
            Assert.AreEqual(1, tr.Childs.Length);

            var td = tr.Childs[0];
            Assert.AreEqual(NodeType.Element, td.NodeType);
            Assert.AreEqual("td", td.NodeName);
            Assert.AreEqual(0, td.Childs.Length);
        }

        [TestMethod]
        public void GumboMisnestedTable2()
        {
            var doc = DocumentBuilder.Html(@"<table><td>Cell1<table><th>Cell2<tr>Cell3</table>");
            var body = doc.Body;
            Assert.AreEqual(1, body.Childs.Length);

            var table1 = body.Childs[0];
            Assert.AreEqual(NodeType.Element, table1.NodeType);
            Assert.AreEqual("table", table1.NodeName);
            Assert.AreEqual(body, table1.ParentElement);
            Assert.AreEqual(1, table1.Childs.Length);

            var tbody1 = table1.Childs[0];
            Assert.AreEqual(NodeType.Element, tbody1.NodeType);
            Assert.AreEqual("tbody", tbody1.NodeName);
            Assert.AreEqual(1, tbody1.Childs.Length);

            var tr1 = tbody1.Childs[0];
            Assert.AreEqual(NodeType.Element, tr1.NodeType);
            Assert.AreEqual("tr", tr1.NodeName);
            Assert.AreEqual(1, tr1.Childs.Length);

            var td1 = tr1.Childs[0];
            Assert.AreEqual(NodeType.Element, td1.NodeType);
            Assert.AreEqual("td", td1.NodeName);
            Assert.AreEqual(2, td1.Childs.Length);

            var cell1 = td1.Childs[0];
            Assert.AreEqual(NodeType.Text, cell1.NodeType);
            Assert.AreEqual("Cell1Cell3", cell1.TextContent);

            var table2 = td1.Childs[1];
            Assert.AreEqual(NodeType.Element, table2.NodeType);
            Assert.AreEqual("table", table2.NodeName);
            Assert.AreEqual(1, table2.Childs.Length);

            var tbody2 = table2.Childs[0];
            Assert.AreEqual(NodeType.Element, tbody2.NodeType);
            Assert.AreEqual("tbody", tbody2.NodeName);
            Assert.AreEqual(2, tbody2.Childs.Length);

            var tr2 = tbody2.Childs[0];
            Assert.AreEqual(NodeType.Element, tr2.NodeType);
            Assert.AreEqual("tr", tr2.NodeName);
            Assert.AreEqual(1, tr2.Childs.Length);

            var th1 = tr2.Childs[0];
            Assert.AreEqual(NodeType.Element, th1.NodeType);
            Assert.AreEqual("th", th1.NodeName);
            Assert.AreEqual(1, th1.Childs.Length);

            var cell2 = th1.Childs[0];
            Assert.AreEqual(NodeType.Text, cell2.NodeType);
            Assert.AreEqual("Cell2", cell2.TextContent);

            var tr3 = tbody2.Childs[1];
            Assert.AreEqual(NodeType.Element, tr3.NodeType);
            Assert.AreEqual("tr", tr3.NodeName);
            Assert.AreEqual(0, tr3.Childs.Length);
        }
    }
}
