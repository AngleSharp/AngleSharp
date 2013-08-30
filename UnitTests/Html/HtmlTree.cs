using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using AngleSharp.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class HtmlTree
    {
        [TestMethod]
        public void TreeHasOneBangComment()
        {
            var doc = DocumentBuilder.Html("<!-- BANG IT --!>");
            Assert.AreEqual(2, doc.ChildNodes.Length);
        }

        [TestMethod]
        public void TreeNonConformingTable()
        {
            //8.2.5.4.7 The "in body" insertion mode - "In the non-conforming ..."
            var doc = DocumentBuilder.Html(@"<a href=""a"">a<table><a href=""b"">b</table>x");

            var tree = new HTMLHtmlElement()
                .AppendChild(new HTMLHeadElement()).ParentNode
                .AppendChild(new HTMLBodyElement())
                    .AppendChild(new HTMLAnchorElement().SetAttribute("href", "a"))
                        .AppendChild(new TextNode("a")).ParentNode
                        .AppendChild(new HTMLAnchorElement().SetAttribute("href", "b"))
                            .AppendChild(new TextNode("b")).ParentNode.ParentNode
                        .AppendChild(new HTMLTableElement()).ParentNode.ParentNode
                    .AppendChild(new HTMLAnchorElement().SetAttribute("href", "b"))
                        .AppendChild(new TextNode("x")).ParentNode.ParentNode.ParentNode;

            Assert.AreEqual(tree.ToHtml(), doc.DocumentElement.ToHtml());
        }

        [TestMethod]
        public void TreeOneTextNodeTableBeforeABCD()
        {
            //One Text node before the table, containing "ABCD"
            var doc = DocumentBuilder.Html(@"A<table>B<tr>C</tr>D</table>");

            var tree = new HTMLHtmlElement()
                .AppendChild(new HTMLHeadElement()).ParentNode
                .AppendChild(new HTMLBodyElement())
                    .AppendChild(new TextNode("ABCD")).ParentNode
                    .AppendChild(new HTMLTableElement())
                        .AppendChild(new HTMLTableSectionElement())
                            .AppendChild(new HTMLTableRowElement()).ParentNode.ParentNode.ParentNode.ParentNode;

            Assert.AreEqual(tree.ToHtml(), doc.DocumentElement.ToHtml());
        }

        [TestMethod]
        public void TreeOneTextNodeTableBeforeAspaceBspaceC()
        {
            //One Text node before the table, containing "A B C" (A-space-B-space-C).
            var doc = DocumentBuilder.Html(@"A<table><tr> B</tr> C</table>");

            var tree = new HTMLHtmlElement()
                .AppendChild(new HTMLHeadElement()).ParentNode
                .AppendChild(new HTMLBodyElement())
                    .AppendChild(new TextNode("A B C")).ParentNode
                    .AppendChild(new HTMLTableElement())
                        .AppendChild(new HTMLTableSectionElement())
                            .AppendChild(new HTMLTableRowElement()).ParentNode.ParentNode.ParentNode.ParentNode;

            Assert.AreEqual(tree.ToHtml(), doc.DocumentElement.ToHtml());
        }

        [TestMethod]
        public void TreeOneTextNodeTableBeforeAspaceBC()
        {
            //One Text node before the table, containing "A BC" (A-space-B-C), and one Text node inside the table (as a child of a tbody) with a single space character.
            var doc = DocumentBuilder.Html(@"A<table><tr> B</tr> </em>C</table>");

            var tree = new HTMLHtmlElement()
                .AppendChild(new HTMLHeadElement()).ParentNode
                .AppendChild(new HTMLBodyElement())
                    .AppendChild(new TextNode("A BC")).ParentNode
                    .AppendChild(new HTMLTableElement())
                        .AppendChild(new HTMLTableSectionElement())
                            .AppendChild(new HTMLTableRowElement()).ParentNode
                            .AppendChild(new TextNode(" ")).ParentNode.ParentNode.ParentNode.ParentNode;

            Assert.AreEqual(tree.ToHtml(), doc.DocumentElement.ToHtml());
        }

        [TestMethod]
        public void TreeUnexpectedTableMarkup()
        {
            //8.2.8.3 Unexpected markup in tables
            var doc = DocumentBuilder.Html(@"<table><b><tr><td>aaa</td></tr>bbb</table>ccc");

            var tree = new HTMLHtmlElement()
                .AppendChild(new HTMLHeadElement()).ParentNode
                .AppendChild(new HTMLBodyElement())
                    .AppendChild(new HTMLElement() { NodeName = "b" }).ParentNode
                    .AppendChild(new HTMLElement() { NodeName = "b" })
                        .AppendChild(new TextNode("bbb")).ParentNode.ParentNode
                    .AppendChild(new HTMLTableElement())
                        .AppendChild(new HTMLTableSectionElement())
                            .AppendChild(new HTMLTableRowElement())
                                .AppendChild(new HTMLTableCellElement())
                                    .AppendChild(new TextNode("aaa")).ParentNode.ParentNode.ParentNode.ParentNode.ParentNode
                    .AppendChild(new HTMLElement() { NodeName = "b" })
                        .AppendChild(new TextNode("ccc")).ParentNode.ParentNode.ParentNode;

            Assert.AreEqual(tree.ToHtml(), doc.DocumentElement.ToHtml());
        }

        [TestMethod]
        public void TreeMisnestedTagsHeisenbergNoFurthest()
        {
            //8.2.8.1 Misnested tags: <b><i></b></i>
            var doc = DocumentBuilder.Html(@"<p>1<b>2<i>3</b>4</i>5</p>");

            var tree = new HTMLHtmlElement()
                .AppendChild(new HTMLHeadElement()).ParentNode
                .AppendChild(new HTMLBodyElement())
                    .AppendChild(new HTMLParagraphElement())
                        .AppendChild(new TextNode("1")).ParentNode
                        .AppendChild(new HTMLElement() { NodeName = "b" })
                            .AppendChild(new TextNode("2")).ParentNode
                            .AppendChild(new HTMLElement() { NodeName = "i" })
                                .AppendChild(new TextNode("3")).ParentNode.ParentNode.ParentNode
                        .AppendChild(new HTMLElement() { NodeName = "i" })
                            .AppendChild(new TextNode("4")).ParentNode.ParentNode
                        .AppendChild(new TextNode("5")).ParentNode.ParentNode.ParentNode;

            Assert.AreEqual(tree.ToHtml(), doc.DocumentElement.ToHtml());
        }

        [TestMethod]
        public void TreeMisnestedTagsHeisenbergWithFurthest()
        {
            //8.2.8.2 Misnested tags: <b><p></b></p>
            var doc = DocumentBuilder.Html(@"<b>1<p>2</b>3</p>");

            var tree = new HTMLHtmlElement()
                .AppendChild(new HTMLHeadElement()).ParentNode
                .AppendChild(new HTMLBodyElement())
                    .AppendChild(new HTMLElement() { NodeName = "b" })
                        .AppendChild(new TextNode("1")).ParentNode.ParentNode
                    .AppendChild(new HTMLParagraphElement())
                        .AppendChild(new HTMLElement() { NodeName = "b" })
                            .AppendChild(new TextNode("2")).ParentNode.ParentNode
                        .AppendChild(new TextNode("3")).ParentNode.ParentNode.ParentNode;

            Assert.AreEqual(tree.ToHtml(), doc.DocumentElement.ToHtml());
        }

        [TestMethod]
        public void TreeUnclosedFormattingElements()
        {
            //8.2.8.6 Unclosed formatting elements
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html>
<p><b class=x><b class=x><b><b class=x><b class=x><b>X<p>X<p><b><b class=x><b>X<p></b></b></b></b></b></b>X");

            var tree = new HTMLHtmlElement()
                .AppendChild(new HTMLHeadElement()).ParentNode
                .AppendChild(new HTMLBodyElement())
                    .AppendChild(new HTMLParagraphElement())
                        .AppendChild(new HTMLElement() { NodeName = "b" }.SetAttribute("class", "x"))
                            .AppendChild(new HTMLElement() { NodeName = "b" }.SetAttribute("class", "x"))
                                .AppendChild(new HTMLElement() { NodeName = "b" })
                                    .AppendChild(new HTMLElement() { NodeName = "b" }.SetAttribute("class", "x"))
                                        .AppendChild(new HTMLElement() { NodeName = "b" }.SetAttribute("class", "x"))
                                            .AppendChild(new HTMLElement() { NodeName = "b" })
                                                .AppendChild(new TextNode("X")).ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode
                    .AppendChild(new HTMLParagraphElement())
                        .AppendChild(new HTMLElement() { NodeName = "b" }.SetAttribute("class", "x"))
                            .AppendChild(new HTMLElement() { NodeName = "b" })
                                .AppendChild(new HTMLElement() { NodeName = "b" }.SetAttribute("class", "x"))
                                    .AppendChild(new HTMLElement() { NodeName = "b" }.SetAttribute("class", "x"))
                                        .AppendChild(new HTMLElement() { NodeName = "b" })
                                            .AppendChild(new TextNode("X")).ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode
                    .AppendChild(new HTMLParagraphElement())
                        .AppendChild(new HTMLElement() { NodeName = "b" }.SetAttribute("class", "x"))
                            .AppendChild(new HTMLElement() { NodeName = "b" })
                                .AppendChild(new HTMLElement() { NodeName = "b" }.SetAttribute("class", "x"))
                                    .AppendChild(new HTMLElement() { NodeName = "b" }.SetAttribute("class", "x"))
                                        .AppendChild(new HTMLElement() { NodeName = "b" })
                                            .AppendChild(new HTMLElement() { NodeName = "b" })
                                                .AppendChild(new HTMLElement() { NodeName = "b" }.SetAttribute("class", "x"))
                                                    .AppendChild(new HTMLElement() { NodeName = "b" })
                                                        .AppendChild(new TextNode("X")).ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode
                    .AppendChild(new HTMLParagraphElement())
                        .AppendChild(new TextNode("X")).ParentNode.ParentNode.ParentNode;

            Assert.AreEqual(tree.ToHtml(), doc.DocumentElement.ToHtml());
        }

        [TestMethod]
        public void HeisenbergAlgorithmStrong()
        {
            var doc = DocumentBuilder.Html(@"<p>1<s id=""A"">2<b id=""B"">3</p>4</s>5</b>");
            var body = doc.Body;
            Assert.AreEqual(3, body.ChildNodes.Length);

            var p = body.ChildNodes[0];
            Assert.AreEqual(NodeType.Element, p.NodeType);
            Assert.AreEqual(2, p.ChildNodes.Length);

            var pt = p.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, pt.NodeType);
            Assert.AreEqual("1", pt.TextContent);

            var ps = p.ChildNodes[1];
            Assert.AreEqual("A", ps.Attributes["id"].Value);
            Assert.AreEqual(NodeType.Element, ps.NodeType);
            Assert.AreEqual(2, ps.ChildNodes.Length);

            var pst = ps.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, pst.NodeType);
            Assert.AreEqual("2", pst.TextContent);

            var psb = ps.ChildNodes[1];
            Assert.AreEqual(NodeType.Element, psb.NodeType);
            Assert.AreEqual(1, psb.ChildNodes.Length);
            Assert.AreEqual("B", psb.Attributes["id"].Value);

            var psbt = psb.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, psbt.NodeType);
            Assert.AreEqual("3", psbt.TextContent);

            var s = body.ChildNodes[1];
            Assert.AreEqual("A", s.Attributes["id"].Value);
            Assert.AreEqual(NodeType.Element, s.NodeType);
            Assert.AreEqual(1, s.ChildNodes.Length);

            var sb = s.ChildNodes[0];
            Assert.AreEqual(NodeType.Element, sb.NodeType);
            Assert.AreEqual(1, sb.ChildNodes.Length);
            Assert.AreEqual("B", sb.Attributes["id"].Value);

            var sbt = sb.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, sbt.NodeType);
            Assert.AreEqual("4", sbt.TextContent);

            var b = body.ChildNodes[2];
            Assert.AreEqual(NodeType.Element, b.NodeType);
            Assert.AreEqual(1, b.ChildNodes.Length);
            Assert.AreEqual("B", b.Attributes["id"].Value);

            var bt = b.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, bt.NodeType);
            Assert.AreEqual("5", bt.TextContent);
        }

        [TestMethod]
        public void OpenButtonWrongClosingTag()
        {
            var doc = DocumentBuilder.Html(@"<button>1</foo>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1button0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1button0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1button0.Attributes.Length);
            Assert.AreEqual("button", dochtml0body1button0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1button0.NodeType);

            var dochtml0body1button0Text0 = dochtml0body1button0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1button0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1button0Text0.TextContent);
        }

        [TestMethod]
        public void UnknownTagWithParagraphChild()
        {
            var doc = DocumentBuilder.Html(@"<foo>1<p>2</foo>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1foo0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1foo0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1foo0.Attributes.Length);
            Assert.AreEqual("foo", dochtml0body1foo0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0.NodeType);

            var dochtml0body1foo0Text0 = dochtml0body1foo0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1foo0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1foo0Text0.TextContent);

            var dochtml0body1foo0p1 = dochtml0body1foo0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1foo0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1foo0p1.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1foo0p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0p1.NodeType);

            var dochtml0body1foo0p1Text0 = dochtml0body1foo0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1foo0p1Text0.NodeType);
            Assert.AreEqual("2", dochtml0body1foo0p1Text0.TextContent);
        }

        [TestMethod]
        public void OpenDefinitionWrongClosingTag()
        {
            var doc = DocumentBuilder.Html(@"<dd>1</foo>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1dd0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1dd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dd0.Attributes.Length);
            Assert.AreEqual("dd", dochtml0body1dd0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1dd0.NodeType);

            var dochtml0body1dd0Text0 = dochtml0body1dd0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1dd0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1dd0Text0.TextContent);
        }

        [TestMethod]
        public void UnknownTagWithDefinitionChild()
        {
            var doc = DocumentBuilder.Html(@"<foo>1<dd>2</foo>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1foo0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1foo0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1foo0.Attributes.Length);
            Assert.AreEqual("foo", dochtml0body1foo0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0.NodeType);

            var dochtml0body1foo0Text0 = dochtml0body1foo0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1foo0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1foo0Text0.TextContent);

            var dochtml0body1foo0dd1 = dochtml0body1foo0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1foo0dd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1foo0dd1.Attributes.Length);
            Assert.AreEqual("dd", dochtml0body1foo0dd1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0dd1.NodeType);

            var dochtml0body1foo0dd1Text0 = dochtml0body1foo0dd1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1foo0dd1Text0.NodeType);
            Assert.AreEqual("2", dochtml0body1foo0dd1Text0.TextContent);
        }

        [TestMethod]
        public void IsIndexStandalone()
        {
            var doc = DocumentBuilder.Html(@"<isindex>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1form0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1form0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0.Attributes.Length);
            Assert.AreEqual("form", dochtml0body1form0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1form0.NodeType);

            var dochtml0body1form0hr0 = dochtml0body1form0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1form0hr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0hr0.Attributes.Length);
            Assert.AreEqual("hr", dochtml0body1form0hr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1form0hr0.NodeType);

            var dochtml0body1form0label1 = dochtml0body1form0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1form0label1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0label1.Attributes.Length);
            Assert.AreEqual("label", dochtml0body1form0label1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1form0label1.NodeType);

            var dochtml0body1form0label1Text0 = dochtml0body1form0label1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1form0label1Text0.NodeType);
            Assert.AreEqual("This is a searchable index. Enter search keywords: ", dochtml0body1form0label1Text0.TextContent);

            var dochtml0body1form0label1input1 = dochtml0body1form0label1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1form0label1input1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1form0label1input1.Attributes.Length);
            Assert.AreEqual("input", dochtml0body1form0label1input1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1form0label1input1.NodeType);
            Assert.AreEqual("isindex", dochtml0body1form0label1input1.Attributes["name"].Value);

            var dochtml0body1form0hr2 = dochtml0body1form0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1form0hr2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0hr2.Attributes.Length);
            Assert.AreEqual("hr", dochtml0body1form0hr2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1form0hr2.NodeType);
        }

        [TestMethod]
        public void IsIndexWithAttributes()
        {
            var doc = DocumentBuilder.Html(@"<isindex name=""A"" action=""B"" prompt=""C"" foo=""D"">");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1form0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1form0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1form0.Attributes.Length);
            Assert.AreEqual("form", dochtml0body1form0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1form0.NodeType);
            Assert.AreEqual("B", dochtml0body1form0.Attributes["action"].Value);

            var dochtml0body1form0hr0 = dochtml0body1form0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1form0hr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0hr0.Attributes.Length);
            Assert.AreEqual("hr", dochtml0body1form0hr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1form0hr0.NodeType);

            var dochtml0body1form0label1 = dochtml0body1form0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1form0label1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0label1.Attributes.Length);
            Assert.AreEqual("label", dochtml0body1form0label1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1form0label1.NodeType);

            var dochtml0body1form0label1Text0 = dochtml0body1form0label1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1form0label1Text0.NodeType);
            Assert.AreEqual("C", dochtml0body1form0label1Text0.TextContent);

            var dochtml0body1form0label1input1 = dochtml0body1form0label1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1form0label1input1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1form0label1input1.Attributes.Length);
            Assert.AreEqual("input", dochtml0body1form0label1input1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1form0label1input1.NodeType);
            Assert.AreEqual("D", dochtml0body1form0label1input1.Attributes["foo"].Value);
            Assert.AreEqual("isindex", dochtml0body1form0label1input1.Attributes["name"].Value);

            var dochtml0body1form0hr2 = dochtml0body1form0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1form0hr2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0hr2.Attributes.Length);
            Assert.AreEqual("hr", dochtml0body1form0hr2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1form0hr2.NodeType);
        }

        [TestMethod]
        public void IsIndexWithinForm()
        {
            var doc = DocumentBuilder.Html(@"<form><isindex>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1form0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1form0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0.Attributes.Length);
            Assert.AreEqual("form", dochtml0body1form0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1form0.NodeType);
        }

        [TestMethod]
        public void TreeSingleTextNode()
        {
            var doc = DocumentBuilder.Html(@"Test");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("Test", dochtml0body1Text0.TextContent);
        }

        [TestMethod]
        public void TreeSingleDivElement()
        {
            var doc = DocumentBuilder.Html(@"<div></div>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);
        }

        [TestMethod]
        public void TreeSingleDivWithTextNode()
        {
            var doc = DocumentBuilder.Html(@"<div>Test</div>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("Test", dochtml0body1div0Text0.TextContent);
        }

        [TestMethod]
        public void TreeTagStartedUnexpectedEof()
        {
            var doc = DocumentBuilder.Html(@"<di");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [TestMethod]
        public void TreeDivsWithContentAndScript()
        {
            var doc = DocumentBuilder.Html(@"<div>Hello</div>
<script>
console.log(""PASS"");
</script>
<div>Bye</div>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(5, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("Hello", dochtml0body1div0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("\n", dochtml0body1Text1.TextContent);

            var dochtml0body1script2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1script2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script2.Attributes.Length);
            Assert.AreEqual("script", dochtml0body1script2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script2.NodeType);

            var dochtml0body1script2Text0 = dochtml0body1script2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script2Text0.NodeType);
            Assert.AreEqual("\nconsole.log(\"PASS\");\n", dochtml0body1script2Text0.TextContent);

            var dochtml0body1div3 = dochtml0body1.ChildNodes[4];
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div3Text0 = dochtml0body1div3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div3Text0.NodeType);
            Assert.AreEqual("Bye", dochtml0body1div3Text0.TextContent);
        }

        [TestMethod]
        public void TreeDivWithAttributeAndTextNode()
        {
            var doc = DocumentBuilder.Html(@"<div foo=""bar"">Hello</div>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);
            Assert.AreEqual("bar", dochtml0body1div0.Attributes["foo"].Value);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("Hello", dochtml0body1div0Text0.TextContent);
        }

        [TestMethod]
        public void TreeScriptElementWithTagInside()
        {
            var doc = DocumentBuilder.Html(@"<div>Hello</div>
<script>
console.log(""FOO<span>BAR</span>BAZ"");
</script>
<div>Bye</div>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(5, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("Hello", dochtml0body1div0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("\n", dochtml0body1Text1.TextContent);

            var dochtml0body1script2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1script2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script2.Attributes.Length);
            Assert.AreEqual("script", dochtml0body1script2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1script2.NodeType);

            var dochtml0body1script2Text0 = dochtml0body1script2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script2Text0.NodeType);
            Assert.AreEqual("\nconsole.log(\"FOO<span>BAR</span>BAZ\");\n", dochtml0body1script2Text0.TextContent);

            var dochtml0body1div3 = dochtml0body1.ChildNodes[4];
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div3Text0 = dochtml0body1div3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div3Text0.NodeType);
            Assert.AreEqual("Bye", dochtml0body1div3Text0.TextContent);
        }

        [TestMethod]
        public void TreeUnknownElementsInContent()
        {
            var doc = DocumentBuilder.Html(@"<foo bar=""baz""></foo><potato quack=""duck""></potato>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1foo0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1foo0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1foo0.Attributes.Length);
            Assert.AreEqual("foo", dochtml0body1foo0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0.NodeType);
            Assert.AreEqual("baz", dochtml0body1foo0.Attributes["bar"].Value);

            var dochtml0body1potato1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1potato1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1potato1.Attributes.Length);
            Assert.AreEqual("potato", dochtml0body1potato1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1potato1.NodeType);
            Assert.AreEqual("duck", dochtml0body1potato1.Attributes["quack"].Value);
        }

        [TestMethod]
        public void TreeUnknownElementsSurrounding()
        {
            var doc = DocumentBuilder.Html(@"<foo bar=""baz""><potato quack=""duck""></potato></foo>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1foo0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1foo0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1foo0.Attributes.Length);
            Assert.AreEqual("foo", dochtml0body1foo0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0.NodeType);
            Assert.AreEqual("baz", dochtml0body1foo0.Attributes["bar"].Value);

            var dochtml0body1foo0potato0 = dochtml0body1foo0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1foo0potato0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1foo0potato0.Attributes.Length);
            Assert.AreEqual("potato", dochtml0body1foo0potato0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0potato0.NodeType);
            Assert.AreEqual("duck", dochtml0body1foo0potato0.Attributes["quack"].Value);
        }

        [TestMethod]
        public void TreeUnknownElementsWithAttributesInClosing()
        {
            var doc = DocumentBuilder.Html(@"<foo></foo bar=""baz""><potato></potato quack=""duck"">");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1foo0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1foo0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1foo0.Attributes.Length);
            Assert.AreEqual("foo", dochtml0body1foo0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0.NodeType);

            var dochtml0body1potato1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1potato1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1potato1.Attributes.Length);
            Assert.AreEqual("potato", dochtml0body1potato1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1potato1.NodeType);
        }

        [TestMethod]
        public void TreeInvalidClosingTag()
        {
            var doc = DocumentBuilder.Html(@"</ tttt>");

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@" tttt", docComment0.TextContent);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [TestMethod]
        public void TreeDivWithAttributeAndImages()
        {
            var doc = DocumentBuilder.Html(@"<div FOO ><img><img></div>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);
            Assert.AreEqual("", dochtml0body1div0.Attributes["foo"].Value);

            var dochtml0body1div0img0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0img0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0img0.Attributes.Length);
            Assert.AreEqual("img", dochtml0body1div0img0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0img0.NodeType);

            var dochtml0body1div0img1 = dochtml0body1div0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div0img1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0img1.Attributes.Length);
            Assert.AreEqual("img", dochtml0body1div0img1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0img1.NodeType);
        }

        [TestMethod]
        public void TreeParagraphsWithTypo()
        {
            var doc = DocumentBuilder.Html(@"<p>Test</p<p>Test2</p>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0Text0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0Text0.NodeType);
            Assert.AreEqual("TestTest2", dochtml0body1p0Text0.TextContent);
        }

        [TestMethod]
        public void TreeInvalidStartTag()
        {
            var doc = DocumentBuilder.Html(@"<rdar://problem/6869687>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1rdar0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1rdar0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1rdar0.Attributes.Length);
            Assert.AreEqual("rdar:", dochtml0body1rdar0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1rdar0.NodeType);
            Assert.AreEqual("", dochtml0body1rdar0.Attributes["6869687"].Value);
            Assert.AreEqual("", dochtml0body1rdar0.Attributes["problem"].Value);

        }

        [TestMethod]
        public void TreeAnchorTagWrongClosing()
        {
            var doc = DocumentBuilder.Html(@"<A>test< /A>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("test< /A>", dochtml0body1a0Text0.TextContent);
        }

        [TestMethod]
        public void TreeSingleEntity()
        {
            var doc = DocumentBuilder.Html(@"&lt;");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("<", dochtml0body1Text0.TextContent);
        }

        [TestMethod]
        public void TreeDoubleBodiesWithAttributes()
        {
            var doc = DocumentBuilder.Html(@"<body foo='bar'><body foo='baz' yo='mama'>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
            Assert.AreEqual("bar", dochtml0body1.Attributes["foo"].Value);
            Assert.AreEqual("mama", dochtml0body1.Attributes["yo"].Value);
        }

        [TestMethod]
        public void TreeClosingBrWithAttribute()
        {
            var doc = DocumentBuilder.Html(@"<body></br foo=""bar""></body>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1br0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1br0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1br0.Attributes.Length);
            Assert.AreEqual("br", dochtml0body1br0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1br0.NodeType);
        }

        [TestMethod]
        public void TreeBodyTypoWithBrAttributes()
        {
            var doc = DocumentBuilder.Html(@"<bdy><br foo=""bar""></body>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1bdy0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1bdy0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1bdy0.Attributes.Length);
            Assert.AreEqual("bdy", dochtml0body1bdy0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1bdy0.NodeType);

            var dochtml0body1bdy0br0 = dochtml0body1bdy0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1bdy0br0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1bdy0br0.Attributes.Length);
            Assert.AreEqual("br", dochtml0body1bdy0br0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1bdy0br0.NodeType);
            Assert.AreEqual("bar", dochtml0body1bdy0br0.Attributes["foo"].Value);
        }

        [TestMethod]
        public void TreeBrClosingWithAttributesOutsideBodyTag()
        {
            var doc = DocumentBuilder.Html(@"<body></body></br foo=""bar"">");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1br0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1br0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1br0.Attributes.Length);
            Assert.AreEqual("br", dochtml0body1br0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1br0.NodeType);
        }

        [TestMethod]
        public void TreeBodyTpyoWithBrOutside()
        {
            var doc = DocumentBuilder.Html(@"<bdy></body><br foo=""bar"">");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1bdy0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1bdy0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1bdy0.Attributes.Length);
            Assert.AreEqual("bdy", dochtml0body1bdy0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1bdy0.NodeType);

            var dochtml0body1bdy0br0 = dochtml0body1bdy0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1bdy0br0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1bdy0br0.Attributes.Length);
            Assert.AreEqual("br", dochtml0body1bdy0br0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1bdy0br0.NodeType);
            Assert.AreEqual("bar", dochtml0body1bdy0br0.Attributes["foo"].Value);
        }

        [TestMethod]
        public void TreeCommentAfterDocumentRoot()
        {
            var doc = DocumentBuilder.Html(@"<html><body></body></html><!-- Hi there -->");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var docComment1 = doc.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, docComment1.NodeType);
            Assert.AreEqual(@" Hi there ", docComment1.TextContent);
        }

        [TestMethod]
        public void TreeTextAndCommentAfterDocumentRoot()
        {
            var doc = DocumentBuilder.Html(@"<html><body></body></html>x<!-- Hi there -->");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@" Hi there ", dochtml0body1Comment1.TextContent);
        }

        [TestMethod]
        public void TreeTextAndCommentAfterDocumentRootTwice()
        {
            var doc = DocumentBuilder.Html(@"<html><body></body></html>x<!-- Hi there --></html><!-- Again -->");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@" Hi there ", dochtml0body1Comment1.TextContent);

            var docComment1 = doc.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, docComment1.NodeType);
            Assert.AreEqual(@" Again ", docComment1.TextContent);
        }

        [TestMethod]
        public void TreeTextAndCommentAfterDocumentRootWithRedundantClosingTags()
        {
            var doc = DocumentBuilder.Html(@"<html><body></body></html>x<!-- Hi there --></body></html><!-- Again -->");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@" Hi there ", dochtml0body1Comment1.TextContent);

            var docComment1 = doc.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, docComment1.NodeType);
            Assert.AreEqual(@" Again ", docComment1.TextContent);
        }

        [TestMethod]
        public void TreeRubyWithDivs()
        {
            var doc = DocumentBuilder.Html(@"<html><body><ruby><div><rp>xx</rp></div></ruby></body></html>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1ruby0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ruby0.Attributes.Length);
            Assert.AreEqual("ruby", dochtml0body1ruby0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0.NodeType);

            var dochtml0body1ruby0div0 = dochtml0body1ruby0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ruby0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ruby0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1ruby0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0div0.NodeType);

            var dochtml0body1ruby0div0rp0 = dochtml0body1ruby0div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ruby0div0rp0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ruby0div0rp0.Attributes.Length);
            Assert.AreEqual("rp", dochtml0body1ruby0div0rp0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0div0rp0.NodeType);

            var dochtml0body1ruby0div0rp0Text0 = dochtml0body1ruby0div0rp0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0div0rp0Text0.NodeType);
            Assert.AreEqual("xx", dochtml0body1ruby0div0rp0Text0.TextContent);
        }

        [TestMethod]
        public void TreeRubyAndRtElements()
        {
            var doc = DocumentBuilder.Html(@"<html><body><ruby><div><rt>xx</rt></div></ruby></body></html>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1ruby0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ruby0.Attributes.Length);
            Assert.AreEqual("ruby", dochtml0body1ruby0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0.NodeType);

            var dochtml0body1ruby0div0 = dochtml0body1ruby0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ruby0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ruby0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1ruby0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0div0.NodeType);

            var dochtml0body1ruby0div0rt0 = dochtml0body1ruby0div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ruby0div0rt0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ruby0div0rt0.Attributes.Length);
            Assert.AreEqual("rt", dochtml0body1ruby0div0rt0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0div0rt0.NodeType);

            var dochtml0body1ruby0div0rt0Text0 = dochtml0body1ruby0div0rt0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0div0rt0Text0.NodeType);
            Assert.AreEqual("xx", dochtml0body1ruby0div0rt0Text0.TextContent);
        }

        [TestMethod]
        public void TreeFramesetAndNoframesElements()
        {
            var doc = DocumentBuilder.Html(@"<html><frameset><!--1--><noframes>A</noframes><!--2--></frameset><!--3--><noframes>B</noframes><!--4--></html><!--5--><noframes>C</noframes><!--6-->");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(6, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0frameset1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(3, dochtml0frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0frameset1.Attributes.Length);
            Assert.AreEqual("frameset", dochtml0frameset1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0frameset1.NodeType);

            var dochtml0frameset1Comment0 = dochtml0frameset1.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml0frameset1Comment0.NodeType);
            Assert.AreEqual(@"1", dochtml0frameset1Comment0.TextContent);

            var dochtml0frameset1noframes1 = dochtml0frameset1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0frameset1noframes1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0frameset1noframes1.Attributes.Length);
            Assert.AreEqual("noframes", dochtml0frameset1noframes1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0frameset1noframes1.NodeType);

            var dochtml0frameset1noframes1Text0 = dochtml0frameset1noframes1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0frameset1noframes1Text0.NodeType);
            Assert.AreEqual("A", dochtml0frameset1noframes1Text0.TextContent);

            var dochtml0frameset1Comment2 = dochtml0frameset1.ChildNodes[2];
            Assert.AreEqual(NodeType.Comment, dochtml0frameset1Comment2.NodeType);
            Assert.AreEqual(@"2", dochtml0frameset1Comment2.TextContent);

            var dochtml0Comment2 = dochtml0.ChildNodes[2];
            Assert.AreEqual(NodeType.Comment, dochtml0Comment2.NodeType);
            Assert.AreEqual(@"3", dochtml0Comment2.TextContent);

            var dochtml0noframes3 = dochtml0.ChildNodes[3];
            Assert.AreEqual(1, dochtml0noframes3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0noframes3.Attributes.Length);
            Assert.AreEqual("noframes", dochtml0noframes3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0noframes3.NodeType);

            var dochtml0noframes3Text0 = dochtml0noframes3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0noframes3Text0.NodeType);
            Assert.AreEqual("B", dochtml0noframes3Text0.TextContent);

            var dochtml0Comment4 = dochtml0.ChildNodes[4];
            Assert.AreEqual(NodeType.Comment, dochtml0Comment4.NodeType);
            Assert.AreEqual(@"4", dochtml0Comment4.TextContent);

            var dochtml0noframes5 = dochtml0.ChildNodes[5];
            Assert.AreEqual(1, dochtml0noframes5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0noframes5.Attributes.Length);
            Assert.AreEqual("noframes", dochtml0noframes5.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0noframes5.NodeType);

            var dochtml0noframes5Text0 = dochtml0noframes5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0noframes5Text0.NodeType);
            Assert.AreEqual("C", dochtml0noframes5Text0.TextContent);

            var docComment1 = doc.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, docComment1.NodeType);
            Assert.AreEqual(@"5", docComment1.TextContent);

            var docComment2 = doc.ChildNodes[2];
            Assert.AreEqual(NodeType.Comment, docComment2.NodeType);
            Assert.AreEqual(@"6", docComment2.TextContent);
        }

        [TestMethod]
        public void TreeSelectOptions()
        {
            var doc = DocumentBuilder.Html(@"<select><option>A<select><option>B<select><option>C<select><option>D<select><option>E<select><option>F<select><option>G<select>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(4, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Length);
            Assert.AreEqual("select", dochtml0body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0option0 = dochtml0body1select0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0option0.Attributes.Length);
            Assert.AreEqual("option", dochtml0body1select0option0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option0.NodeType);

            var dochtml0body1select0option0Text0 = dochtml0body1select0option0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1select0option0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1select0option0Text0.TextContent);

            var dochtml0body1option1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1option1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option1.Attributes.Length);
            Assert.AreEqual("option", dochtml0body1option1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1option1.NodeType);

            var dochtml0body1option1Text0 = dochtml0body1option1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option1Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1option1Text0.TextContent);

            var dochtml0body1option1select1 = dochtml0body1option1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1option1select1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option1select1.Attributes.Length);
            Assert.AreEqual("select", dochtml0body1option1select1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1option1select1.NodeType);

            var dochtml0body1option1select1option0 = dochtml0body1option1select1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1option1select1option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option1select1option0.Attributes.Length);
            Assert.AreEqual("option", dochtml0body1option1select1option0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1option1select1option0.NodeType);

            var dochtml0body1option1select1option0Text0 = dochtml0body1option1select1option0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option1select1option0Text0.NodeType);
            Assert.AreEqual("C", dochtml0body1option1select1option0Text0.TextContent);

            var dochtml0body1option2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(2, dochtml0body1option2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option2.Attributes.Length);
            Assert.AreEqual("option", dochtml0body1option2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1option2.NodeType);

            var dochtml0body1option2Text0 = dochtml0body1option2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option2Text0.NodeType);
            Assert.AreEqual("D", dochtml0body1option2Text0.TextContent);

            var dochtml0body1option2select1 = dochtml0body1option2.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1option2select1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option2select1.Attributes.Length);
            Assert.AreEqual("select", dochtml0body1option2select1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1option2select1.NodeType);

            var dochtml0body1option2select1option0 = dochtml0body1option2select1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1option2select1option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option2select1option0.Attributes.Length);
            Assert.AreEqual("option", dochtml0body1option2select1option0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1option2select1option0.NodeType);

            var dochtml0body1option2select1option0Text0 = dochtml0body1option2select1option0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option2select1option0Text0.NodeType);
            Assert.AreEqual("E", dochtml0body1option2select1option0Text0.TextContent);

            var dochtml0body1option3 = dochtml0body1.ChildNodes[3];
            Assert.AreEqual(2, dochtml0body1option3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option3.Attributes.Length);
            Assert.AreEqual("option", dochtml0body1option3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1option3.NodeType);

            var dochtml0body1option3Text0 = dochtml0body1option3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option3Text0.NodeType);
            Assert.AreEqual("F", dochtml0body1option3Text0.TextContent);

            var dochtml0body1option3select1 = dochtml0body1option3.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1option3select1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option3select1.Attributes.Length);
            Assert.AreEqual("select", dochtml0body1option3select1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1option3select1.NodeType);

            var dochtml0body1option3select1option0 = dochtml0body1option3select1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1option3select1option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option3select1option0.Attributes.Length);
            Assert.AreEqual("option", dochtml0body1option3select1option0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1option3select1option0.NodeType);

            var dochtml0body1option3select1option0Text0 = dochtml0body1option3select1option0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option3select1option0Text0.NodeType);
            Assert.AreEqual("G", dochtml0body1option3select1option0Text0.TextContent);
        }

        [TestMethod]
        public void TreeDefinitionList()
        {
            var doc = DocumentBuilder.Html(@"<dd><dd><dt><dt><dd><li><li>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(5, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
            var dochtml0body1dd0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1dd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dd0.Attributes.Length);
            Assert.AreEqual("dd", dochtml0body1dd0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1dd0.NodeType);

            var dochtml0body1dd1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1dd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dd1.Attributes.Length);
            Assert.AreEqual("dd", dochtml0body1dd1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1dd1.NodeType);

            var dochtml0body1dt2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1dt2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dt2.Attributes.Length);
            Assert.AreEqual("dt", dochtml0body1dt2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1dt2.NodeType);

            var dochtml0body1dt3 = dochtml0body1.ChildNodes[3];
            Assert.AreEqual(0, dochtml0body1dt3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dt3.Attributes.Length);
            Assert.AreEqual("dt", dochtml0body1dt3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1dt3.NodeType);

            var dochtml0body1dd4 = dochtml0body1.ChildNodes[4];
            Assert.AreEqual(2, dochtml0body1dd4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dd4.Attributes.Length);
            Assert.AreEqual("dd", dochtml0body1dd4.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1dd4.NodeType);

            var dochtml0body1dd4li0 = dochtml0body1dd4.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1dd4li0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dd4li0.Attributes.Length);
            Assert.AreEqual("li", dochtml0body1dd4li0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1dd4li0.NodeType);

            var dochtml0body1dd4li1 = dochtml0body1dd4.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1dd4li1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dd4li1.Attributes.Length);
            Assert.AreEqual("li", dochtml0body1dd4li1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1dd4li1.NodeType);
        }

        [TestMethod]
        public void TreeDivsAndFormatting()
        {
            var doc = DocumentBuilder.Html(@"<div><b></div><div><nobr>a<nobr>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0b0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div0b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b0.NodeType);

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

            var dochtml0body1div1b0 = dochtml0body1div1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1div1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1b0.Attributes.Length);
            Assert.AreEqual("b", dochtml0body1div1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0.NodeType);

            var dochtml0body1div1b0nobr0 = dochtml0body1div1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1b0nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml0body1div1b0nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0nobr0.NodeType);

            var dochtml0body1div1b0nobr0Text0 = dochtml0body1div1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div1b0nobr0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1div1b0nobr0Text0.TextContent);

            var dochtml0body1div1b0nobr1 = dochtml0body1div1b0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1b0nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml0body1div1b0nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0nobr1.NodeType);
        }

        [TestMethod]
        public void TreeStandardStructureWithoutRoot()
        {
            var doc = DocumentBuilder.Html(@"<head></head>
<body></body>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(3, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0Text1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0Text1.NodeType);
            Assert.AreEqual("\n", dochtml0Text1.TextContent);

            var dochtml0body2 = dochtml0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body2.Attributes.Length);
            Assert.AreEqual("body", dochtml0body2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body2.NodeType);
        }

        [TestMethod]
        public void TreeStyleTagOutsideHead()
        {
            var doc = DocumentBuilder.Html(@"<head></head> <style></style>ddd");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(3, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0style0.Attributes.Length);
            Assert.AreEqual("style", dochtml0head0style0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0Text1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0Text1.NodeType);
            Assert.AreEqual(" ", dochtml0Text1.TextContent);

            var dochtml0body2 = dochtml0.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body2.Attributes.Length);
            Assert.AreEqual("body", dochtml0body2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body2.NodeType);

            var dochtml0body2Text0 = dochtml0body2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body2Text0.NodeType);
            Assert.AreEqual("ddd", dochtml0body2Text0.TextContent);
        }

        [TestMethod]
        public void TreeTableElementMisnestedWithUnknownElement()
        {
            var doc = DocumentBuilder.Html(@"<kbd><table></kbd><col><select><tr>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1kbd0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1kbd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0.Attributes.Length);
            Assert.AreEqual("kbd", dochtml0body1kbd0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0.NodeType);

            var dochtml0body1kbd0select0 = dochtml0body1kbd0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1kbd0select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0select0.Attributes.Length);
            Assert.AreEqual("select", dochtml0body1kbd0select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0select0.NodeType);

            var dochtml0body1kbd0table1 = dochtml0body1kbd0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1kbd0table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1kbd0table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1.NodeType);

            var dochtml0body1kbd0table1colgroup0 = dochtml0body1kbd0table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1kbd0table1colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1colgroup0.Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1kbd0table1colgroup0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1colgroup0.NodeType);

            var dochtml0body1kbd0table1colgroup0col0 = dochtml0body1kbd0table1colgroup0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1kbd0table1colgroup0col0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1colgroup0col0.Attributes.Length);
            Assert.AreEqual("col", dochtml0body1kbd0table1colgroup0col0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1colgroup0col0.NodeType);

            var dochtml0body1kbd0table1tbody1 = dochtml0body1kbd0table1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1kbd0table1tbody1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1tbody1.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1kbd0table1tbody1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1tbody1.NodeType);

            var dochtml0body1kbd0table1tbody1tr0 = dochtml0body1kbd0table1tbody1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1kbd0table1tbody1tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1tbody1tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1kbd0table1tbody1tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1tbody1tr0.NodeType);
        }

        [TestMethod]
        public void TreeTableAndSelectElementMistnestedInUnknownElement()
        {
            var doc = DocumentBuilder.Html(@"<kbd><table></kbd><col><select><tr></table><div>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1kbd0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1kbd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0.Attributes.Length);
            Assert.AreEqual("kbd", dochtml0body1kbd0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0.NodeType);

            var dochtml0body1kbd0select0 = dochtml0body1kbd0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1kbd0select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0select0.Attributes.Length);
            Assert.AreEqual("select", dochtml0body1kbd0select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0select0.NodeType);

            var dochtml0body1kbd0table1 = dochtml0body1kbd0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1kbd0table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1kbd0table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1.NodeType);

            var dochtml0body1kbd0table1colgroup0 = dochtml0body1kbd0table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1kbd0table1colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1colgroup0.Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1kbd0table1colgroup0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1colgroup0.NodeType);

            var dochtml0body1kbd0table1colgroup0col0 = dochtml0body1kbd0table1colgroup0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1kbd0table1colgroup0col0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1colgroup0col0.Attributes.Length);
            Assert.AreEqual("col", dochtml0body1kbd0table1colgroup0col0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1colgroup0col0.NodeType);

            var dochtml0body1kbd0table1tbody1 = dochtml0body1kbd0table1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1kbd0table1tbody1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1tbody1.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1kbd0table1tbody1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1tbody1.NodeType);

            var dochtml0body1kbd0table1tbody1tr0 = dochtml0body1kbd0table1tbody1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1kbd0table1tbody1tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1tbody1tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1kbd0table1tbody1tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1tbody1tr0.NodeType);

            var dochtml0body1kbd0div2 = dochtml0body1kbd0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1kbd0div2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0div2.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1kbd0div2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0div2.NodeType);
        }

        [TestMethod]
        public void TreeVariousTagsInsideAnchorElement()
        {
            var doc = DocumentBuilder.Html(@"<a><li><style></style><title></title></a>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1li1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1li1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1li1.Attributes.Length);
            Assert.AreEqual("li", dochtml0body1li1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1li1.NodeType);

            var dochtml0body1li1a0 = dochtml0body1li1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1li1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1li1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1li1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1li1a0.NodeType);

            var dochtml0body1li1a0style0 = dochtml0body1li1a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1li1a0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1li1a0style0.Attributes.Length);
            Assert.AreEqual("style", dochtml0body1li1a0style0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1li1a0style0.NodeType);

            var dochtml0body1li1a0title1 = dochtml0body1li1a0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1li1a0title1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1li1a0title1.Attributes.Length);
            Assert.AreEqual("title", dochtml0body1li1a0title1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1li1a0title1.NodeType);
        }

        [TestMethod]
        public void TreeVariousTagsInsideFontElement()
        {
            var doc = DocumentBuilder.Html(@"<font></p><p><meta><title></title></font>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1font0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1font0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1font0.NodeType);

            var dochtml0body1font0p0 = dochtml0body1font0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1font0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1font0p0.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1font0p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1font0p0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1font0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1p1font0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1font0.Attributes.Length);
            Assert.AreEqual("font", dochtml0body1p1font0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0.NodeType);

            var dochtml0body1p1font0meta0 = dochtml0body1p1font0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p1font0meta0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1font0meta0.Attributes.Length);
            Assert.AreEqual("meta", dochtml0body1p1font0meta0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0meta0.NodeType);

            var dochtml0body1p1font0title1 = dochtml0body1p1font0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1p1font0title1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1font0title1.Attributes.Length);
            Assert.AreEqual("title", dochtml0body1p1font0title1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0title1.NodeType);
        }

        [TestMethod]
        public void TreeCenterTitleElementInAnchorElement()
        {
            var doc = DocumentBuilder.Html(@"<a><center><title></title><a>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1center1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1center1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1center1.Attributes.Length);
            Assert.AreEqual("center", dochtml0body1center1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1center1.NodeType);

            var dochtml0body1center1a0 = dochtml0body1center1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1center1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1center1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1center1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1center1a0.NodeType);

            var dochtml0body1center1a0title0 = dochtml0body1center1a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1center1a0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1center1a0title0.Attributes.Length);
            Assert.AreEqual("title", dochtml0body1center1a0title0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1center1a0title0.NodeType);

            var dochtml0body1center1a1 = dochtml0body1center1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1center1a1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1center1a1.Attributes.Length);
            Assert.AreEqual("a", dochtml0body1center1a1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1center1a1.NodeType);
        }

        [TestMethod]
        public void TreeSvgElementWithTitleAndDiv()
        {
            var doc = DocumentBuilder.Html(@"<svg><title><div>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0title0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0.Attributes.Length);
            Assert.AreEqual("title", dochtml0body1svg0title0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0.NodeType);

            var dochtml0body1svg0title0div0 = dochtml0body1svg0title0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1svg0title0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1svg0title0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0div0.NodeType);
        }

        [TestMethod]
        public void TreeSvgElementWithTitleAndRectAndDiv()
        {
            var doc = DocumentBuilder.Html(@"<svg><title><rect><div>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0title0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0.Attributes.Length);
            Assert.AreEqual("title", dochtml0body1svg0title0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0.NodeType);

            var dochtml0body1svg0title0rect0 = dochtml0body1svg0title0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0title0rect0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0rect0.Attributes.Length);
            Assert.AreEqual("rect", dochtml0body1svg0title0rect0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0rect0.NodeType);

            var dochtml0body1svg0title0rect0div0 = dochtml0body1svg0title0rect0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1svg0title0rect0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0rect0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1svg0title0rect0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0rect0div0.NodeType);
        }

        [TestMethod]
        public void TreeSvgElementWithTitleRepeated()
        {
            var doc = DocumentBuilder.Html(@"<svg><title><svg><div>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0title0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1svg0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0.Attributes.Length);
            Assert.AreEqual("title", dochtml0body1svg0title0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0.NodeType);

            var dochtml0body1svg0title0svg0 = dochtml0body1svg0title0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1svg0title0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0title0svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0svg0.NodeType);

            var dochtml0body1svg0title0div1 = dochtml0body1svg0title0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1svg0title0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1svg0title0div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0div1.NodeType);
        }

        [TestMethod]
        public void TreeImageWithFailedContent()
        {
            var doc = DocumentBuilder.Html(@"<img <="""" FAIL>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1img0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1img0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1img0.Attributes.Length);
            Assert.AreEqual("img", dochtml0body1img0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1img0.NodeType);
            Assert.AreEqual("", dochtml0body1img0.Attributes["fail"].Value);
            Assert.AreEqual("", dochtml0body1img0.Attributes["<"].Value);
        }

        [TestMethod]
        public void TreeUnorderedListWithDivElements()
        {
            var doc = DocumentBuilder.Html(@"<ul><li><div id='foo'/>A</li><li>B<div>C</div></li></ul>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1ul0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1ul0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ul0.Attributes.Length);
            Assert.AreEqual("ul", dochtml0body1ul0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0.NodeType);

            var dochtml0body1ul0li0 = dochtml0body1ul0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ul0li0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ul0li0.Attributes.Length);
            Assert.AreEqual("li", dochtml0body1ul0li0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li0.NodeType);

            var dochtml0body1ul0li0div0 = dochtml0body1ul0li0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1ul0li0div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1ul0li0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1ul0li0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li0div0.NodeType);
            Assert.AreEqual("foo", dochtml0body1ul0li0div0.Attributes["id"].Value);

            var dochtml0body1ul0li0div0Text0 = dochtml0body1ul0li0div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ul0li0div0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1ul0li0div0Text0.TextContent);

            var dochtml0body1ul0li1 = dochtml0body1ul0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1ul0li1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ul0li1.Attributes.Length);
            Assert.AreEqual("li", dochtml0body1ul0li1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li1.NodeType);

            var dochtml0body1ul0li1Text0 = dochtml0body1ul0li1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ul0li1Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1ul0li1Text0.TextContent);

            var dochtml0body1ul0li1div1 = dochtml0body1ul0li1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1ul0li1div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ul0li1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1ul0li1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li1div1.NodeType);

            var dochtml0body1ul0li1div1Text0 = dochtml0body1ul0li1div1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ul0li1div1Text0.NodeType);
            Assert.AreEqual("C", dochtml0body1ul0li1div1Text0.TextContent);
        }

        [TestMethod]
        public void TreeSvgWithEmAndDescElements()
        {
            var doc = DocumentBuilder.Html(@"<svg><em><desc></em>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1em1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1em1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1em1.Attributes.Length);
            Assert.AreEqual("em", dochtml0body1em1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1em1.NodeType);

            var dochtml0body1em1desc0 = dochtml0body1em1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1em1desc0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1em1desc0.Attributes.Length);
            Assert.AreEqual("desc", dochtml0body1em1desc0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1em1desc0.NodeType);
        }

        [TestMethod]
        public void TreeSvgWithTfootAndClosingMiElement()
        {
            var doc = DocumentBuilder.Html(@"<svg><tfoot></mi><td>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0tfoot0 = dochtml0body1svg0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1svg0tfoot0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0tfoot0.Attributes.Length);
            Assert.AreEqual("tfoot", dochtml0body1svg0tfoot0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0tfoot0.NodeType);

            var dochtml0body1svg0tfoot0td0 = dochtml0body1svg0tfoot0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1svg0tfoot0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0tfoot0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1svg0tfoot0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0tfoot0td0.NodeType);
        }

        [TestMethod]
        public void TreeMathWithMrowsAndOtherElements()
        {
            var doc = DocumentBuilder.Html(@"<math><mrow><mrow><mn>1</mn></mrow><mi>a</mi></mrow></math>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mrow0 = dochtml0body1math0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1math0mrow0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mrow0.Attributes.Length);
            Assert.AreEqual("mrow", dochtml0body1math0mrow0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mrow0.NodeType);

            var dochtml0body1math0mrow0mrow0 = dochtml0body1math0mrow0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0mrow0mrow0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mrow0mrow0.Attributes.Length);
            Assert.AreEqual("mrow", dochtml0body1math0mrow0mrow0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mrow0mrow0.NodeType);

            var dochtml0body1math0mrow0mrow0mn0 = dochtml0body1math0mrow0mrow0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0mrow0mrow0mn0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mrow0mrow0mn0.Attributes.Length);
            Assert.AreEqual("mn", dochtml0body1math0mrow0mrow0mn0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mrow0mrow0mn0.NodeType);

            var dochtml0body1math0mrow0mrow0mn0Text0 = dochtml0body1math0mrow0mrow0mn0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1math0mrow0mrow0mn0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1math0mrow0mrow0mn0Text0.TextContent);

            var dochtml0body1math0mrow0mi1 = dochtml0body1math0mrow0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1math0mrow0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mrow0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml0body1math0mrow0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mrow0mi1.NodeType);

            var dochtml0body1math0mrow0mi1Text0 = dochtml0body1math0mrow0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1math0mrow0mi1Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1math0mrow0mi1Text0.TextContent);
        }

        [TestMethod]
        public void TreeDocTypeWithInputHiddenAndFrameset()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><input type=""hidden""><frameset>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1frameset1.Attributes.Length);
            Assert.AreEqual("frameset", dochtml1frameset1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);
        }

        [TestMethod]
        public void TreeDocTypeWithInputButtonAndFrameset()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><input type=""button""><frameset>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1input0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1input0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1input0.Attributes.Length);
            Assert.AreEqual("input", dochtml1body1input0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1input0.NodeType);
            Assert.AreEqual("button", dochtml1body1input0.Attributes["type"].Value);
        }

        [TestMethod]
        public void TreeUnknownTagSelfClosing()
        {
            var doc = DocumentBuilder.Html(@"<foo bar=qux/>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1foo0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1foo0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1foo0.Attributes.Length);
            Assert.AreEqual("foo", dochtml0body1foo0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0.NodeType);
            Assert.AreEqual("qux/", dochtml0body1foo0.Attributes["bar"].Value);
        }

        [TestMethod]
        public void TreeParagraphWithTightAttributesAndNoScriptTagScriptingEnabled()
        {
            var doc = new HTMLDocument();
            doc.IsScripting = true;

            var parser = new HtmlParser(doc, @"<p id=""status""><noscript><strong>A</strong></noscript><span>B</span></p>");
            parser.Parse();

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);
            Assert.AreEqual("status", dochtml0body1p0.Attributes["id"].Value);

            var dochtml0body1p0noscript0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0noscript0.Attributes.Length);
            Assert.AreEqual("noscript", dochtml0body1p0noscript0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0noscript0.NodeType);

            var dochtml0body1p0noscript0Text0 = dochtml0body1p0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0noscript0Text0.NodeType);
            Assert.AreEqual("<strong>A</strong>", dochtml0body1p0noscript0Text0.TextContent);

            var dochtml0body1p0span1 = dochtml0body1p0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p0span1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0span1.Attributes.Length);
            Assert.AreEqual("span", dochtml0body1p0span1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0span1.NodeType);

            var dochtml0body1p0span1Text0 = dochtml0body1p0span1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0span1Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1p0span1Text0.TextContent);
        }

        [TestMethod]
        public void TreeParagraphWithTightAttributesAndNoScriptTagScriptingDisabled()
        {
            //Scripting is disabled by default
            var doc = DocumentBuilder.Html(@"<p id=""status""><noscript><strong>A</strong></noscript><span>B</span></p>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);
            Assert.AreEqual("status", dochtml0body1p0.Attributes["id"].Value);

            var dochtml0body1p0noscript0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0noscript0.Attributes.Length);
            Assert.AreEqual("noscript", dochtml0body1p0noscript0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0noscript0.NodeType);

            var dochtml0body1p0noscript0Strong0 = dochtml0body1p0noscript0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0noscript0Strong0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0noscript0Strong0.Attributes.Length);
            Assert.AreEqual("strong", dochtml0body1p0noscript0Strong0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0noscript0Strong0.NodeType);

            var dochtml0body1p0noscript0Strong0Text = dochtml0body1p0noscript0Strong0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0noscript0Strong0Text.NodeType);
            Assert.AreEqual("A", dochtml0body1p0noscript0Strong0Text.TextContent);

            var dochtml0body1p0span1 = dochtml0body1p0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1p0span1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0span1.Attributes.Length);
            Assert.AreEqual("span", dochtml0body1p0span1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0span1.NodeType);

            var dochtml0body1p0span1Text0 = dochtml0body1p0span1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0span1Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1p0span1Text0.TextContent);
        }

        [TestMethod]
        public void TreeSarcasmTagUsed()
        {
            var doc = DocumentBuilder.Html(@"<div><sarcasm><div></div></sarcasm></div>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0sarcasm0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1div0sarcasm0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0sarcasm0.Attributes.Length);
            Assert.AreEqual("sarcasm", dochtml0body1div0sarcasm0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0sarcasm0.NodeType);

            var dochtml0body1div0sarcasm0div0 = dochtml0body1div0sarcasm0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1div0sarcasm0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0sarcasm0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0sarcasm0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1div0sarcasm0div0.NodeType);
        }

        [TestMethod]
        public void TreeImageWithOpeningDoubleQuotesAltAttribute()
        {
            var doc = DocumentBuilder.Html(@"<html><body><img src="""" border=""0"" alt=""><div>A</div></body></html>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [TestMethod]
        public void TreeWithMisnestedClosingTableBodySection()
        {
            var doc = DocumentBuilder.Html(@"<table><td></tbody>A");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table1tbody0 = dochtml0body1table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table1tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0.NodeType);

            var dochtml0body1table1tbody0tr0 = dochtml0body1table1tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table1tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0.NodeType);

            var dochtml0body1table1tbody0tr0td0 = dochtml0body1table1tbody0tr0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table1tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0td0.NodeType);
        }

        [TestMethod]
        public void TreeWithMisnestedClosingTableHeadSection()
        {
            var doc = DocumentBuilder.Html(@"<table><td></thead>A");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0Text0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1table0tbody0tr0td0Text0.TextContent);
        }

        [TestMethod]
        public void TreeWithMisnestedClosingTableFootSection()
        {
            var doc = DocumentBuilder.Html(@"<table><td></tfoot>A");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0Text0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1table0tbody0tr0td0Text0.TextContent);
        }

        [TestMethod]
        public void TreeWithTableHeadSectionAndMisnestedClosingTableBodySection()
        {
            var doc = DocumentBuilder.Html(@"<table><thead><td></tbody>A");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0thead0 = dochtml0body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0thead0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0.Attributes.Length);
            Assert.AreEqual("thead", dochtml0body1table0thead0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0.NodeType);

            var dochtml0body1table0thead0tr0 = dochtml0body1table0thead0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0thead0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0thead0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0tr0.NodeType);

            var dochtml0body1table0thead0tr0td0 = dochtml0body1table0thead0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1table0thead0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0thead0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0tr0td0.NodeType);

            var dochtml0body1table0thead0tr0td0Text0 = dochtml0body1table0thead0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0thead0tr0td0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1table0thead0tr0td0Text0.TextContent);
        }

        [TestMethod]
        public void TreeNobrTagsInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><a href='#1'><nobr>1<nobr></a><br><a href='#2'><nobr>2<nobr></a><br><a href='#3'><nobr>3<nobr></a>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(5, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1a0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1a0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1a0.Attributes.Length);
            Assert.AreEqual("a", dochtml1body1a0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1a0.NodeType);
            Assert.IsNotNull(dochtml1body1a0.Attributes["href"]);
            Assert.AreEqual("href", dochtml1body1a0.Attributes["href"].Name);
            Assert.AreEqual("#1", dochtml1body1a0.Attributes["href"].Value);

            var dochtml1body1a0nobr0 = dochtml1body1a0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1a0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1a0nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1a0nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1a0nobr0.NodeType);

            var dochtml1body1a0nobr0Text0 = dochtml1body1a0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1a0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1a0nobr0Text0.TextContent);

            var dochtml1body1a0nobr1 = dochtml1body1a0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1a0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1a0nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1a0nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1a0nobr1.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1br0 = dochtml1body1nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1nobr1br0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1br0.Attributes.Length);
            Assert.AreEqual("br", dochtml1body1nobr1br0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1br0.NodeType);

            var dochtml1body1nobr1a1 = dochtml1body1nobr1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1nobr1a1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1nobr1a1.Attributes.Length);
            Assert.AreEqual("a", dochtml1body1nobr1a1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1a1.NodeType);
            Assert.IsNotNull(dochtml1body1nobr1a1.Attributes["href"]);
            Assert.AreEqual("href", dochtml1body1nobr1a1.Attributes["href"].Name);
            Assert.AreEqual("#2", dochtml1body1nobr1a1.Attributes["href"].Value);

            var dochtml1body1a2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(2, dochtml1body1a2.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1a2.Attributes.Length);
            Assert.AreEqual("a", dochtml1body1a2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1a2.NodeType);
            Assert.IsNotNull(dochtml1body1a2.Attributes["href"]);
            Assert.AreEqual("href", dochtml1body1a2.Attributes["href"].Name);
            Assert.AreEqual("#2", dochtml1body1a2.Attributes["href"].Value);

            var dochtml1body1a2nobr0 = dochtml1body1a2.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1a2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1a2nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1a2nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1a2nobr0.NodeType);

            var dochtml1body1a2nobr0Text0 = dochtml1body1a2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1a2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1a2nobr0Text0.TextContent);

            var dochtml1body1a2nobr1 = dochtml1body1a2.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1a2nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1a2nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1a2nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1a2nobr1.NodeType);

            var dochtml1body1nobr3 = dochtml1body1.ChildNodes[3];
            Assert.AreEqual(2, dochtml1body1nobr3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr3.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1nobr3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr3.NodeType);

            var dochtml1body1nobr3br0 = dochtml1body1nobr3.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1nobr3br0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr3br0.Attributes.Length);
            Assert.AreEqual("br", dochtml1body1nobr3br0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr3br0.NodeType);

            var dochtml1body1nobr3a1 = dochtml1body1nobr3.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1nobr3a1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1nobr3a1.Attributes.Length);
            Assert.AreEqual("a", dochtml1body1nobr3a1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr3a1.NodeType);
            Assert.IsNotNull(dochtml1body1nobr3a1.Attributes["href"]);
            Assert.AreEqual("href", dochtml1body1nobr3a1.Attributes["href"].Name);
            Assert.AreEqual("#3", dochtml1body1nobr3a1.Attributes["href"].Value);

            var dochtml1body1a4 = dochtml1body1.ChildNodes[4];
            Assert.AreEqual(2, dochtml1body1a4.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1a4.Attributes.Length);
            Assert.AreEqual("a", dochtml1body1a4.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1a4.NodeType);
            Assert.IsNotNull(dochtml1body1a4.Attributes["href"]);
            Assert.AreEqual("href", dochtml1body1a4.Attributes["href"].Name);
            Assert.AreEqual("#3", dochtml1body1a4.Attributes["href"].Value);

            var dochtml1body1a4nobr0 = dochtml1body1a4.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1a4nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1a4nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1a4nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1a4nobr0.NodeType);

            var dochtml1body1a4nobr0Text0 = dochtml1body1a4nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1a4nobr0Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1a4nobr0Text0.TextContent);

            var dochtml1body1a4nobr1 = dochtml1body1a4.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1a4nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1a4nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1a4nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1a4nobr1.NodeType);
        }

        [TestMethod]
        public void TreeNobrAndFormattingTagsInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><b><nobr>1<nobr></b><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(4, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Length);
            Assert.AreEqual("b", dochtml1body1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1i0 = dochtml1body1nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1i0.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1nobr1i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1i0.NodeType);

            var dochtml1body1i2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(2, dochtml1body1i2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1i2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1i2.NodeType);

            var dochtml1body1i2nobr0 = dochtml1body1i2.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1i2nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1i2nobr0.NodeType);

            var dochtml1body1i2nobr0Text0 = dochtml1body1i2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1i2nobr0Text0.TextContent);

            var dochtml1body1i2nobr1 = dochtml1body1i2.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1i2nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1i2nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1i2nobr1.NodeType);

            var dochtml1body1nobr3 = dochtml1body1.ChildNodes[3];
            Assert.AreEqual(1, dochtml1body1nobr3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr3.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1nobr3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr3.NodeType);

            var dochtml1body1nobr3Text0 = dochtml1body1nobr3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1nobr3Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1nobr3Text0.TextContent);
        }

        [TestMethod]
        public void TreeNobrAndTableTagsInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><b><nobr>1<table><nobr></b><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Length);
            Assert.AreEqual("b", dochtml1body1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(5, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr0nobr1 = dochtml1body1b0nobr0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1b0nobr0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0nobr1.NodeType);

            var dochtml1body1b0nobr0nobr1i0 = dochtml1body1b0nobr0nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1b0nobr0nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0nobr1i0.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1b0nobr0nobr1i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0nobr1i0.NodeType);

            var dochtml1body1b0nobr0i2 = dochtml1body1b0nobr0.ChildNodes[2];
            Assert.AreEqual(2, dochtml1body1b0nobr0i2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0i2.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1b0nobr0i2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0i2.NodeType);

            var dochtml1body1b0nobr0i2nobr0 = dochtml1body1b0nobr0i2.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0i2nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0i2nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0i2nobr0.NodeType);

            var dochtml1body1b0nobr0i2nobr0Text0 = dochtml1body1b0nobr0i2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0i2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1b0nobr0i2nobr0Text0.TextContent);

            var dochtml1body1b0nobr0i2nobr1 = dochtml1body1b0nobr0i2.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr0i2nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0i2nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0i2nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0i2nobr1.NodeType);

            var dochtml1body1b0nobr0nobr3 = dochtml1body1b0nobr0.ChildNodes[3];
            Assert.AreEqual(1, dochtml1body1b0nobr0nobr3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0nobr3.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0nobr3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0nobr3.NodeType);

            var dochtml1body1b0nobr0nobr3Text0 = dochtml1body1b0nobr0nobr3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0nobr3Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1b0nobr0nobr3Text0.TextContent);

            var dochtml1body1b0nobr0table4 = dochtml1body1b0nobr0.ChildNodes[4];
            Assert.AreEqual(0, dochtml1body1b0nobr0table4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table4.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1b0nobr0table4.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table4.NodeType);
        }

        [TestMethod]
        public void TreeNoBrAndTableCellTagsInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><b><nobr>1<table><tr><td><nobr></b><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Length);
            Assert.AreEqual("b", dochtml1body1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr0table1 = dochtml1body1b0nobr0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1b0nobr0table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1b0nobr0table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1.NodeType);

            var dochtml1body1b0nobr0table1tbody0 = dochtml1body1b0nobr0table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1b0nobr0table1tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0 = dochtml1body1b0nobr0table1tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml1body1b0nobr0table1tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0 = dochtml1body1b0nobr0table1tbody0tr0.ChildNodes[0];
            Assert.AreEqual(3, dochtml1body1b0nobr0table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml1body1b0nobr0table1tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0nobr0 = dochtml1body1b0nobr0table1tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0tr0td0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0table1tbody0tr0td0nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0nobr0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0 = dochtml1body1b0nobr0table1tbody0tr0td0nobr0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0i1 = dochtml1body1b0nobr0table1tbody0tr0td0.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1b0nobr0table1tbody0tr0td0i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0i1.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1b0nobr0table1tbody0tr0td0i1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0i1.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0 = dochtml1body1b0nobr0table1tbody0tr0td0i1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0Text0 = dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0Text0.TextContent);

            var dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1 = dochtml1body1b0nobr0table1tbody0tr0td0i1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0nobr2 = dochtml1body1b0nobr0table1tbody0tr0td0.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0tr0td0nobr2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0nobr2.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0table1tbody0tr0td0nobr2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0nobr2.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0nobr2Text0 = dochtml1body1b0nobr0table1tbody0tr0td0nobr2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0table1tbody0tr0td0nobr2Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1b0nobr0table1tbody0tr0td0nobr2Text0.TextContent);
        }

        [TestMethod]
        public void TreeNobrAndDivTagsInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><b><nobr>1<div><nobr></b><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Length);
            Assert.AreEqual("b", dochtml1body1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1div1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(4, dochtml1body1div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml1body1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1.NodeType);

            var dochtml1body1div1b0 = dochtml1body1div1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1b0.Attributes.Length);
            Assert.AreEqual("b", dochtml1body1div1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1b0.NodeType);

            var dochtml1body1div1b0nobr0 = dochtml1body1div1b0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1div1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1b0nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1div1b0nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1b0nobr0.NodeType);

            var dochtml1body1div1b0nobr1 = dochtml1body1div1b0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1div1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1b0nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1div1b0nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1b0nobr1.NodeType);

            var dochtml1body1div1nobr1 = dochtml1body1div1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1div1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1div1nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr1.NodeType);

            var dochtml1body1div1nobr1i0 = dochtml1body1div1nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1div1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1nobr1i0.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1div1nobr1i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr1i0.NodeType);

            var dochtml1body1div1i2 = dochtml1body1div1.ChildNodes[2];
            Assert.AreEqual(2, dochtml1body1div1i2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1i2.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1div1i2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i2.NodeType);

            var dochtml1body1div1i2nobr0 = dochtml1body1div1i2.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1div1i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1i2nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1div1i2nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i2nobr0.NodeType);

            var dochtml1body1div1i2nobr0Text0 = dochtml1body1div1i2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div1i2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1div1i2nobr0Text0.TextContent);

            var dochtml1body1div1i2nobr1 = dochtml1body1div1i2.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1div1i2nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1i2nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1div1i2nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i2nobr1.NodeType);

            var dochtml1body1div1nobr3 = dochtml1body1div1.ChildNodes[3];
            Assert.AreEqual(1, dochtml1body1div1nobr3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1nobr3.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1div1nobr3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr3.NodeType);

            var dochtml1body1div1nobr3Text0 = dochtml1body1div1nobr3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div1nobr3Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1div1nobr3Text0.TextContent);
        }

        [TestMethod]
        public void TreeNobrAndBoldAndDivTagsInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><b><nobr>1<nobr></b><div><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Length);
            Assert.AreEqual("b", dochtml1body1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1div1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(3, dochtml1body1div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1.Attributes.Length);
            Assert.AreEqual("div", dochtml1body1div1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1.NodeType);

            var dochtml1body1div1nobr0 = dochtml1body1div1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1div1nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1div1nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr0.NodeType);

            var dochtml1body1div1nobr0i0 = dochtml1body1div1nobr0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1div1nobr0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1nobr0i0.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1div1nobr0i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr0i0.NodeType);

            var dochtml1body1div1i1 = dochtml1body1div1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1div1i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1i1.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1div1i1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i1.NodeType);

            var dochtml1body1div1i1nobr0 = dochtml1body1div1i1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1div1i1nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1i1nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1div1i1nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i1nobr0.NodeType);

            var dochtml1body1div1i1nobr0Text0 = dochtml1body1div1i1nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div1i1nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1div1i1nobr0Text0.TextContent);

            var dochtml1body1div1i1nobr1 = dochtml1body1div1i1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1div1i1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1i1nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1div1i1nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i1nobr1.NodeType);

            var dochtml1body1div1nobr2 = dochtml1body1div1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1div1nobr2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1nobr2.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1div1nobr2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr2.NodeType);

            var dochtml1body1div1nobr2Text0 = dochtml1body1div1nobr2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div1nobr2Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1div1nobr2Text0.TextContent);
        }

        [TestMethod]
        public void TreeNobrAndInsTagInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><b><nobr>1<nobr><ins></b><i><nobr>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Length);
            Assert.AreEqual("b", dochtml1body1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1b0nobr1ins0 = dochtml1body1b0nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1b0nobr1ins0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr1ins0.Attributes.Length);
            Assert.AreEqual("ins", dochtml1body1b0nobr1ins0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1ins0.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1i0 = dochtml1body1nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1i0.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1nobr1i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1i0.NodeType);

            var dochtml1body1i2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1i2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1i2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1i2.NodeType);

            var dochtml1body1i2nobr0 = dochtml1body1i2.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1i2nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1i2nobr0.NodeType);
        }

        [TestMethod]
        public void TreeNobrAndInsTagWithBoldInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><b><nobr>1<ins><nobr></b><i>2");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Length);
            Assert.AreEqual("b", dochtml1body1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr0ins1 = dochtml1body1b0nobr0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr0ins1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0ins1.Attributes.Length);
            Assert.AreEqual("ins", dochtml1body1b0nobr0ins1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0ins1.NodeType);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1i0 = dochtml1body1nobr1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1i0.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1nobr1i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1i0.NodeType);

            var dochtml1body1nobr1i0Text0 = dochtml1body1nobr1i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1nobr1i0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1nobr1i0Text0.TextContent);
        }

        [TestMethod]
        public void TreeNobrAndItalicTagsInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><b>1<nobr></b><i><nobr>2</i>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Length);
            Assert.AreEqual("b", dochtml1body1b0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0Text0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0Text0.TextContent);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1nobr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1i0 = dochtml1body1nobr1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1i0.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1nobr1i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1i0.NodeType);

            var dochtml1body1i2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1i2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1i2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1i2.NodeType);

            var dochtml1body1i2nobr0 = dochtml1body1i2.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2nobr0.Attributes.Length);
            Assert.AreEqual("nobr", dochtml1body1i2nobr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1i2nobr0.NodeType);

            var dochtml1body1i2nobr0Text0 = dochtml1body1i2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1i2nobr0Text0.TextContent);
        }

        [TestMethod]
        public void TreeMisopenedCodeTagInParagraph()
        {
            var doc = DocumentBuilder.Html(@"<p><code x</code></p>
");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0code0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0code0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1p0code0.Attributes.Length);
            Assert.AreEqual("code", dochtml0body1p0code0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p0code0.NodeType);
            Assert.IsNotNull(dochtml0body1p0code0.Attributes["code"]);
            Assert.AreEqual("code", dochtml0body1p0code0.Attributes["code"].Name);
            Assert.AreEqual("", dochtml0body1p0code0.Attributes["code"].Value);
            Assert.IsNotNull(dochtml0body1p0code0.Attributes["x<"]);
            Assert.AreEqual("x<", dochtml0body1p0code0.Attributes["x<"].Name);
            Assert.AreEqual("", dochtml0body1p0code0.Attributes["x<"].Value);

            var dochtml0body1code1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1code1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1code1.Attributes.Length);
            Assert.AreEqual("code", dochtml0body1code1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1code1.NodeType);
            Assert.IsNotNull(dochtml0body1code1.Attributes["code"]);
            Assert.AreEqual("code", dochtml0body1code1.Attributes["code"].Name);
            Assert.AreEqual("", dochtml0body1code1.Attributes["code"].Value);
            Assert.IsNotNull(dochtml0body1code1.Attributes["x<"]);
            Assert.AreEqual("x<", dochtml0body1code1.Attributes["x<"].Name);
            Assert.AreEqual("", dochtml0body1code1.Attributes["x<"].Value);

            var dochtml0body1code1Text0 = dochtml0body1code1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1code1Text0.NodeType);
            Assert.AreEqual("\n", dochtml0body1code1Text0.TextContent);
        }

        [TestMethod]
        public void TreeItalicInParagraphInForeignObjectInSvg()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><svg><foreignObject><p><i></p>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0foreignObject0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject0.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0.NodeType);

            var dochtml1body1svg0foreignObject0p0 = dochtml1body1svg0foreignObject0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject0p0.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1svg0foreignObject0p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0p0.NodeType);

            var dochtml1body1svg0foreignObject0p0i0 = dochtml1body1svg0foreignObject0p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg0foreignObject0p0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject0p0i0.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1svg0foreignObject0p0i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0p0i0.NodeType);

            var dochtml1body1svg0foreignObject0i1 = dochtml1body1svg0foreignObject0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1svg0foreignObject0i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject0i1.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1svg0foreignObject0i1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0i1.NodeType);

            var dochtml1body1svg0foreignObject0i1Text0 = dochtml1body1svg0foreignObject0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0foreignObject0i1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1svg0foreignObject0i1Text0.TextContent);
        }

        [TestMethod]
        public void TreeTableWithSvgInTableCell()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><table><tr><td><svg><foreignObject><p><i></p>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1table0tbody0tr0td0svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0 = dochtml1body1table0tbody0tr0td0svg0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0foreignObject0.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml1body1table0tbody0tr0td0svg0foreignObject0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0foreignObject0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0p0 = dochtml1body1table0tbody0tr0td0svg0foreignObject0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0 = dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0i1 = dochtml1body1table0tbody0tr0td0svg0foreignObject0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0i1Text0 = dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0svg0foreignObject0i1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1table0tbody0tr0td0svg0foreignObject0i1Text0.TextContent);
        }

        [TestMethod]
        public void TreeItalicInParagraphInMtextInMath()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><math><mtext><p><i></p>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mtext0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mtext0.Attributes.Length);
            Assert.AreEqual("mtext", dochtml1body1math0mtext0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mtext0.NodeType);

            var dochtml1body1math0mtext0p0 = dochtml1body1math0mtext0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0mtext0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mtext0p0.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1math0mtext0p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mtext0p0.NodeType);

            var dochtml1body1math0mtext0p0i0 = dochtml1body1math0mtext0p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0mtext0p0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mtext0p0i0.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1math0mtext0p0i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mtext0p0i0.NodeType);

            var dochtml1body1math0mtext0i1 = dochtml1body1math0mtext0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1math0mtext0i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mtext0i1.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1math0mtext0i1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mtext0i1.NodeType);

            var dochtml1body1math0mtext0i1Text0 = dochtml1body1math0mtext0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mtext0i1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1math0mtext0i1Text0.TextContent);
        }

        [TestMethod]
        public void TreeTableWithMathInTableCell()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><table><tr><td><math><mtext><p><i></p>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1table0tbody0tr0td0math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mtext0.Attributes.Length);
            Assert.AreEqual("mtext", dochtml1body1table0tbody0tr0td0math0mtext0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mtext0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0p0 = dochtml1body1table0tbody0tr0td0math0mtext0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mtext0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mtext0p0.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1table0tbody0tr0td0math0mtext0p0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mtext0p0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0p0i0 = dochtml1body1table0tbody0tr0td0math0mtext0p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mtext0p0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mtext0p0i0.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0math0mtext0p0i0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mtext0p0i0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0i1 = dochtml1body1table0tbody0tr0td0math0mtext0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mtext0i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mtext0i1.Attributes.Length);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0math0mtext0i1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mtext0i1.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0i1Text0 = dochtml1body1table0tbody0tr0td0math0mtext0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mtext0i1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1table0tbody0tr0td0math0mtext0i1Text0.TextContent);
        }

        [TestMethod]
        public void TreeDivWithMisclosedTagInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><div><!/div>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1div0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml1body1div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1div0.NodeType);

            var dochtml1body1div0Comment0 = dochtml1body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml1body1div0Comment0.NodeType);
            Assert.AreEqual(@"/div", dochtml1body1div0Comment0.TextContent);

            var dochtml1body1div0Text1 = dochtml1body1div0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1div0Text1.NodeType);
            Assert.AreEqual("a", dochtml1body1div0Text1.TextContent);
        }
    }
}
