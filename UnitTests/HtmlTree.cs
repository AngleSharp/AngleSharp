using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
