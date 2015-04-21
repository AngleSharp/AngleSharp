using System;
using System.Linq;
using AngleSharp.Core.Tests.Mocks;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/isindex.dat,
    /// tree-construction/tests3.dat,
    /// tree-construction/tests16.dat,
    /// tree-construction/inbody01.dat,
    /// tree-construction/webkit01.dat,
    /// tree-construction/webkit02.dat
    /// </summary>
    [TestFixture]
    public class HtmlTreeTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void TreeHasOneBangComment()
        {
            var doc = Html("<!-- BANG IT --!>");
            Assert.AreEqual(2, doc.ChildNodes.Length);
        }

        [Test]
        public void Html5DocumentType()
        {
            var dom = Html("<!doctype html >");

            Assert.AreEqual("html", dom.Doctype.Name);
            Assert.AreEqual("", dom.Doctype.PublicIdentifier);
            Assert.AreEqual("", dom.Doctype.SystemIdentifier);

            Assert.AreEqual("<!DOCTYPE html>", dom.Doctype.ToHtml());
        }

        [Test]
        public void HtmlDocumenTypeWithPublicIdentifier()
        {
            var xhtmlType = "<!DOCTYPE html PUBLIC \"-//w3c//dtd xhtml 1.0\">";

            var dom = Html(xhtmlType);

            Assert.AreEqual("html", dom.Doctype.Name);
            Assert.AreEqual("-//w3c//dtd xhtml 1.0", dom.Doctype.PublicIdentifier);
            Assert.AreEqual("", dom.Doctype.SystemIdentifier);
            Assert.AreEqual(xhtmlType, dom.Doctype.ToHtml());
        }

        [Test]
        public void TreeNonConformingTable()
        {
            var expected = @"<html><head></head><body><a href=""a"">a<a href=""b"">b</a><table></table></a><a href=""b"">x</a></body></html>";
            var source = @"<a href=""a"">a<table><a href=""b"">b</table>x";
            //8.2.5.4.7 The "in body" insertion mode - "In the non-conforming ..."
            var doc = Html(source);

            Assert.AreEqual(expected, doc.DocumentElement.OuterHtml);
        }

        [Test]
        public void TreeIFrameWithAnotherIFramePairInsideComment()
        {
            var doc = Html(@"<!doctype html><iframe><!--<iframe></iframe>--></iframe>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count());
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count());
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count());
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1iframe0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1iframe0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1iframe0.Attributes.Count());
            Assert.AreEqual("iframe", dochtml1body1iframe0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1iframe0.NodeType);

            var dochtml1body1iframe0Text0 = dochtml1body1iframe0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1iframe0Text0.NodeType);
            Assert.AreEqual("<!--<iframe>", dochtml1body1iframe0Text0.TextContent);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("-->", dochtml1body1Text1.TextContent);
        }

        [Test]
        public void TreeIFrameWithTextAndStrangeComment()
        {
            var doc = Html(@"<!doctype html><iframe>...<!--X->...<!--/X->...</iframe>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count());
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count());
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count());
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1iframe0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1iframe0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1iframe0.Attributes.Count());
            Assert.AreEqual("iframe", dochtml1body1iframe0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1iframe0.NodeType);

            var dochtml1body1iframe0Text0 = dochtml1body1iframe0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1iframe0Text0.NodeType);
            Assert.AreEqual("...<!--X->...<!--/X->...", dochtml1body1iframe0Text0.TextContent);
        }

        [Test]
        public void TreeXmpWithAnotherXmpPairInsideComment()
        {
            var doc = Html(@"<!doctype html><xmp><!--<xmp></xmp>--></xmp>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count());
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count());
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count());
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1xmp0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1xmp0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1xmp0.Attributes.Count());
            Assert.AreEqual("xmp", dochtml1body1xmp0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1xmp0.NodeType);

            var dochtml1body1xmp0Text0 = dochtml1body1xmp0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1xmp0Text0.NodeType);
            Assert.AreEqual("<!--<xmp>", dochtml1body1xmp0Text0.TextContent);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("-->", dochtml1body1Text1.TextContent);
        }

        [Test]
        public void TreeNoEmbedWithAnotherNoEmbedPairInsideComment()
        {
            var doc = Html(@"<!doctype html><noembed><!--<noembed></noembed>--></noembed>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1noembed0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1noembed0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1noembed0.Attributes.Count);
            Assert.AreEqual("noembed", dochtml1body1noembed0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1noembed0.NodeType);

            var dochtml1body1noembed0Text0 = dochtml1body1noembed0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1noembed0Text0.NodeType);
            Assert.AreEqual("<!--<noembed>", dochtml1body1noembed0Text0.TextContent);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("-->", dochtml1body1Text1.TextContent);
        }

        [Test]
        public void TreeOneTextNodeTableBeforeABCD()
        {
            var expected = "<html><head></head><body>ABCD<table><tbody><tr></tr></tbody></table></body></html>";
            var source = @"A<table>B<tr>C</tr>D</table>";
            //One Text node before the table, containing "ABCD"
            var doc = Html(source);

            Assert.AreEqual(expected, doc.DocumentElement.OuterHtml);
        }

        [Test]
        public void TreeOneTextNodeTableBeforeAspaceBspaceC()
        {
            var expected = "<html><head></head><body>A B C<table><tbody><tr></tr></tbody></table></body></html>";
            var source = @"A<table><tr> B</tr> C</table>";
            //One Text node before the table, containing "A B C" (A-space-B-space-C).
            var doc = Html(source);

            Assert.AreEqual(expected, doc.DocumentElement.OuterHtml);
        }

        [Test]
        public void TreeOneTextNodeTableBeforeAspaceBC()
        {
            var expected = "<html><head></head><body>A BC<table><tbody><tr></tr> </tbody></table></body></html>";
            var source = @"A<table><tr> B</tr> </em>C</table>";
            //One Text node before the table, containing "A BC" (A-space-B-C), and one Text node inside the table (as a child of a tbody) with a single space character.
            var doc = Html(source);

            Assert.AreEqual(expected, doc.DocumentElement.OuterHtml);
        }

        [Test]
        public void TreeUnexpectedTableMarkup()
        {
            var expected = "<html><head></head><body><b></b><b>bbb</b><table><tbody><tr><td>aaa</td></tr></tbody></table><b>ccc</b></body></html>";
            var source = @"<table><b><tr><td>aaa</td></tr>bbb</table>ccc";
            //8.2.8.3 Unexpected markup in tables
            var doc = Html(source);

            Assert.AreEqual(expected, doc.DocumentElement.OuterHtml);
        }

        [Test]
        public void TreeMisnestedTagsHeisenbergNoFurthest()
        {
            var expected = "<html><head></head><body><p>1<b>2<i>3</i></b><i>4</i>5</p></body></html>";
            var source = @"<p>1<b>2<i>3</b>4</i>5</p>";
            //8.2.8.1 Misnested tags: <b><i></b></i>
            var doc = Html(source);

            Assert.AreEqual(expected, doc.DocumentElement.OuterHtml);
        }

        [Test]
        public void TreeMisnestedTagsHeisenbergWithFurthest()
        {
            var expected = "<html><head></head><body><b>1</b><p><b>2</b>3</p></body></html>";
            var source = @"<b>1<p>2</b>3</p>";
            //8.2.8.2 Misnested tags: <b><p></b></p>
            var doc = Html(source);

            Assert.AreEqual(expected, doc.DocumentElement.OuterHtml);
        }

        [Test]
        public void TreeUnclosedFormattingElements()
        {
            var expected = "<html><head></head><body><p><b class=\"x\"><b class=\"x\"><b><b class=\"x\"><b class=\"x\"><b>X</b></b></b></b></b></b></p><p><b class=\"x\"><b><b class=\"x\"><b class=\"x\"><b>X</b></b></b></b></b></p><p><b class=\"x\"><b><b class=\"x\"><b class=\"x\"><b><b><b class=\"x\"><b>X</b></b></b></b></b></b></b></b></p><p>X</p></body></html>";
            var source = @"<!DOCTYPE html>
<p><b class=x><b class=x><b><b class=x><b class=x><b>X<p>X<p><b><b class=x><b>X<p></b></b></b></b></b></b>X";
            //8.2.8.6 Unclosed formatting elements
            var doc = Html(source);

            Assert.AreEqual(expected, doc.DocumentElement.OuterHtml);
        }

        [Test]
        public void HeisenbergAlgorithmStrong()
        {
            var doc = Html(@"<p>1<s id=""A"">2<b id=""B"">3</p>4</s>5</b>");
            var body = doc.Body;
            Assert.AreEqual(3, body.ChildNodes.Length);

            var p = body.ChildNodes[0] as Element;
            Assert.AreEqual(NodeType.Element, p.NodeType);
            Assert.AreEqual(2, p.ChildNodes.Length);

            var pt = p.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, pt.NodeType);
            Assert.AreEqual("1", pt.TextContent);

            var ps = p.ChildNodes[1] as Element;
            Assert.AreEqual("A", ps.GetAttribute("id"));
            Assert.AreEqual(NodeType.Element, ps.NodeType);
            Assert.AreEqual(2, ps.ChildNodes.Length);

            var pst = ps.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, pst.NodeType);
            Assert.AreEqual("2", pst.TextContent);

            var psb = ps.ChildNodes[1] as Element;
            Assert.AreEqual(NodeType.Element, psb.NodeType);
            Assert.AreEqual(1, psb.ChildNodes.Length);
            Assert.AreEqual("B", psb.GetAttribute("id"));

            var psbt = psb.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, psbt.NodeType);
            Assert.AreEqual("3", psbt.TextContent);

            var s = body.ChildNodes[1] as Element;
            Assert.AreEqual("A", s.Attributes.Get("id").Value);
            Assert.AreEqual(NodeType.Element, s.NodeType);
            Assert.AreEqual(1, s.ChildNodes.Length);

            var sb = s.ChildNodes[0] as Element;
            Assert.AreEqual(NodeType.Element, sb.NodeType);
            Assert.AreEqual(1, sb.ChildNodes.Length);
            Assert.AreEqual("B", sb.GetAttribute("id"));

            var sbt = sb.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, sbt.NodeType);
            Assert.AreEqual("4", sbt.TextContent);

            var b = body.ChildNodes[2] as Element; ;
            Assert.AreEqual(NodeType.Element, b.NodeType);
            Assert.AreEqual(1, b.ChildNodes.Length);
            Assert.AreEqual("B", b.GetAttribute("id"));

            var bt = b.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, bt.NodeType);
            Assert.AreEqual("5", bt.TextContent);
        }

        [Test]
        public void OpenButtonWrongClosingTag()
        {
            var doc = Html(@"<button>1</foo>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1button0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1button0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1button0.Attributes.Count);
            Assert.AreEqual("button", dochtml0body1button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1button0.NodeType);

            var dochtml0body1button0Text0 = dochtml0body1button0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1button0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1button0Text0.TextContent);
        }

        [Test]
        public void UnknownTagWithParagraphChild()
        {
            var doc = Html(@"<foo>1<p>2</foo>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1foo0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1foo0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1foo0.Attributes.Count);
            Assert.AreEqual("foo", dochtml0body1foo0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0.NodeType);

            var dochtml0body1foo0Text0 = dochtml0body1foo0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1foo0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1foo0Text0.TextContent);

            var dochtml0body1foo0p1 = dochtml0body1foo0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1foo0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1foo0p1.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1foo0p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0p1.NodeType);

            var dochtml0body1foo0p1Text0 = dochtml0body1foo0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1foo0p1Text0.NodeType);
            Assert.AreEqual("2", dochtml0body1foo0p1Text0.TextContent);
        }

        [Test]
        public void OpenDefinitionWrongClosingTag()
        {
            var doc = Html(@"<dd>1</foo>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1dd0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1dd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dd0.Attributes.Count);
            Assert.AreEqual("dd", dochtml0body1dd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1dd0.NodeType);

            var dochtml0body1dd0Text0 = dochtml0body1dd0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1dd0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1dd0Text0.TextContent);
        }

        [Test]
        public void UnknownTagWithDefinitionChild()
        {
            var doc = Html(@"<foo>1<dd>2</foo>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1foo0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1foo0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1foo0.Attributes.Count);
            Assert.AreEqual("foo", dochtml0body1foo0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0.NodeType);

            var dochtml0body1foo0Text0 = dochtml0body1foo0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1foo0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1foo0Text0.TextContent);

            var dochtml0body1foo0dd1 = dochtml0body1foo0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1foo0dd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1foo0dd1.Attributes.Count);
            Assert.AreEqual("dd", dochtml0body1foo0dd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0dd1.NodeType);

            var dochtml0body1foo0dd1Text0 = dochtml0body1foo0dd1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1foo0dd1Text0.NodeType);
            Assert.AreEqual("2", dochtml0body1foo0dd1Text0.TextContent);
        }

        [Test]
        public void IsIndexStandalone()
        {
            var doc = Html(@"<isindex>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1form0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml0body1form0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0.Attributes.Count);
            Assert.AreEqual("form", dochtml0body1form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1form0.NodeType);

            var dochtml0body1form0hr0 = dochtml0body1form0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1form0hr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0hr0.Attributes.Count);
            Assert.AreEqual("hr", dochtml0body1form0hr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1form0hr0.NodeType);

            var dochtml0body1form0label1 = dochtml0body1form0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1form0label1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0label1.Attributes.Count);
            Assert.AreEqual("label", dochtml0body1form0label1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1form0label1.NodeType);

            var dochtml0body1form0label1Text0 = dochtml0body1form0label1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1form0label1Text0.NodeType);
            Assert.AreEqual("This is a searchable index. Enter search keywords: ", dochtml0body1form0label1Text0.TextContent);

            var dochtml0body1form0label1input1 = dochtml0body1form0label1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1form0label1input1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1form0label1input1.Attributes.Count);
            Assert.AreEqual("input", dochtml0body1form0label1input1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1form0label1input1.NodeType);
            Assert.AreEqual("isindex", dochtml0body1form0label1input1.Attributes.Get("name").Value);

            var dochtml0body1form0hr2 = dochtml0body1form0.ChildNodes[2] as Element; ;
            Assert.AreEqual(0, dochtml0body1form0hr2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0hr2.Attributes.Count);
            Assert.AreEqual("hr", dochtml0body1form0hr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1form0hr2.NodeType);
        }

        [Test]
        public void IsIndexWithAttributes()
        {
            var doc = Html(@"<isindex name=""A"" action=""B"" prompt=""C"" foo=""D"">");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1form0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml0body1form0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1form0.Attributes.Count);
            Assert.AreEqual("form", dochtml0body1form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1form0.NodeType);
            Assert.AreEqual("B", dochtml0body1form0.Attributes.Get("action").Value);

            var dochtml0body1form0hr0 = dochtml0body1form0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1form0hr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0hr0.Attributes.Count);
            Assert.AreEqual("hr", dochtml0body1form0hr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1form0hr0.NodeType);

            var dochtml0body1form0label1 = dochtml0body1form0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1form0label1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0label1.Attributes.Count);
            Assert.AreEqual("label", dochtml0body1form0label1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1form0label1.NodeType);

            var dochtml0body1form0label1Text0 = dochtml0body1form0label1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1form0label1Text0.NodeType);
            Assert.AreEqual("C", dochtml0body1form0label1Text0.TextContent);

            var dochtml0body1form0label1input1 = dochtml0body1form0label1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1form0label1input1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1form0label1input1.Attributes.Count);
            Assert.AreEqual("input", dochtml0body1form0label1input1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1form0label1input1.NodeType);
            Assert.AreEqual("D", dochtml0body1form0label1input1.Attributes.Get("foo").Value);
            Assert.AreEqual("isindex", dochtml0body1form0label1input1.Attributes.Get("name").Value);

            var dochtml0body1form0hr2 = dochtml0body1form0.ChildNodes[2] as Element; ;
            Assert.AreEqual(0, dochtml0body1form0hr2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0hr2.Attributes.Count);
            Assert.AreEqual("hr", dochtml0body1form0hr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1form0hr2.NodeType);
        }

        [Test]
        public void IsIndexWithinForm()
        {
            var doc = Html(@"<form><isindex>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1form0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1form0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1form0.Attributes.Count);
            Assert.AreEqual("form", dochtml0body1form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1form0.NodeType);
        }

        [Test]
        public void TreeSingleTextNode()
        {
            var doc = Html(@"Test");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("Test", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void TreeSingleDivElement()
        {
            var doc = Html(@"<div></div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);
        }

        [Test]
        public void TreeSingleDivWithTextNode()
        {
            var doc = Html(@"<div>Test</div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("Test", dochtml0body1div0Text0.TextContent);
        }

        [Test]
        public void TreeTagStartedUnexpectedEof()
        {
            var doc = Html(@"<di");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TreeDivsWithContentAndScript()
        {
            var doc = Html(@"<div>Hello</div>
<script>
console.log(""PASS"");
</script>
<div>Bye</div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(5, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("Hello", dochtml0body1div0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("\n", dochtml0body1Text1.TextContent);

            var dochtml0body1script2 = dochtml0body1.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtml0body1script2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script2.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script2.NodeType);

            var dochtml0body1script2Text0 = dochtml0body1script2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script2Text0.NodeType);
            Assert.AreEqual("\nconsole.log(\"PASS\");\n", dochtml0body1script2Text0.TextContent);

            var dochtml0body1div3 = dochtml0body1.ChildNodes[4];
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div3Text0 = dochtml0body1div3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div3Text0.NodeType);
            Assert.AreEqual("Bye", dochtml0body1div3Text0.TextContent);
        }

        [Test]
        public void TreeDivWithAttributeAndTextNode()
        {
            var doc = Html(@"<div foo=""bar"">Hello</div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);
            Assert.AreEqual("bar", dochtml0body1div0.Attributes.Get("foo").Value);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("Hello", dochtml0body1div0Text0.TextContent);
        }

        [Test]
        public void TreeScriptElementWithTagInside()
        {
            var doc = Html(@"<div>Hello</div>
<script>
console.log(""FOO<span>BAR</span>BAZ"");
</script>
<div>Bye</div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(5, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0Text0 = dochtml0body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0Text0.NodeType);
            Assert.AreEqual("Hello", dochtml0body1div0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("\n", dochtml0body1Text1.TextContent);

            var dochtml0body1script2 = dochtml0body1.ChildNodes[2] as Element; ;
            Assert.AreEqual(1, dochtml0body1script2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1script2.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1script2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1script2.NodeType);

            var dochtml0body1script2Text0 = dochtml0body1script2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1script2Text0.NodeType);
            Assert.AreEqual("\nconsole.log(\"FOO<span>BAR</span>BAZ\");\n", dochtml0body1script2Text0.TextContent);

            var dochtml0body1div3 = dochtml0body1.ChildNodes[4];
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div3Text0 = dochtml0body1div3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div3Text0.NodeType);
            Assert.AreEqual("Bye", dochtml0body1div3Text0.TextContent);
        }

        [Test]
        public void TreeUnknownElementsInContent()
        {
            var doc = Html(@"<foo bar=""baz""></foo><potato quack=""duck""></potato>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1foo0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1foo0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1foo0.Attributes.Count);
            Assert.AreEqual("foo", dochtml0body1foo0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0.NodeType);
            Assert.AreEqual("baz", dochtml0body1foo0.Attributes.Get("bar").Value);

            var dochtml0body1potato1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1potato1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1potato1.Attributes.Count);
            Assert.AreEqual("potato", dochtml0body1potato1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1potato1.NodeType);
            Assert.AreEqual("duck", dochtml0body1potato1.Attributes.Get("quack").Value);
        }

        [Test]
        public void TreeUnknownElementsSurrounding()
        {
            var doc = Html(@"<foo bar=""baz""><potato quack=""duck""></potato></foo>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1foo0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1foo0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1foo0.Attributes.Count);
            Assert.AreEqual("foo", dochtml0body1foo0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0.NodeType);
            Assert.AreEqual("baz", dochtml0body1foo0.Attributes.Get("bar").Value);

            var dochtml0body1foo0potato0 = dochtml0body1foo0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1foo0potato0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1foo0potato0.Attributes.Count);
            Assert.AreEqual("potato", dochtml0body1foo0potato0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0potato0.NodeType);
            Assert.AreEqual("duck", dochtml0body1foo0potato0.Attributes.Get("quack").Value);
        }

        [Test]
        public void TreeUnknownElementsWithAttributesInClosing()
        {
            var doc = Html(@"<foo></foo bar=""baz""><potato></potato quack=""duck"">");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1foo0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1foo0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1foo0.Attributes.Count);
            Assert.AreEqual("foo", dochtml0body1foo0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0.NodeType);

            var dochtml0body1potato1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1potato1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1potato1.Attributes.Count);
            Assert.AreEqual("potato", dochtml0body1potato1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1potato1.NodeType);
        }

        [Test]
        public void TreeInvalidClosingTag()
        {
            var doc = Html(@"</ tttt>");

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@" tttt", docComment0.TextContent);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void TreeDivWithAttributeAndImages()
        {
            var doc = Html(@"<div FOO ><img><img></div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);
            Assert.AreEqual("", dochtml0body1div0.Attributes.Get("foo").Value);

            var dochtml0body1div0img0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0img0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0img0.Attributes.Count);
            Assert.AreEqual("img", dochtml0body1div0img0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0img0.NodeType);

            var dochtml0body1div0img1 = dochtml0body1div0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1div0img1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0img1.Attributes.Count);
            Assert.AreEqual("img", dochtml0body1div0img1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0img1.NodeType);
        }

        [Test]
        public void TreeParagraphsWithTypo()
        {
            var doc = Html(@"<p>Test</p<p>Test2</p>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0Text0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0Text0.NodeType);
            Assert.AreEqual("TestTest2", dochtml0body1p0Text0.TextContent);
        }

        [Test]
        public void TreeInvalidStartTag()
        {
            var doc = Html(@"<rdar://problem/6869687>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1rdar0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1rdar0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1rdar0.Attributes.Count);
            Assert.AreEqual("rdar:", dochtml0body1rdar0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1rdar0.NodeType);
            Assert.AreEqual("", dochtml0body1rdar0.GetAttribute("6869687"));
            Assert.AreEqual("", dochtml0body1rdar0.GetAttribute("problem"));

        }

        [Test]
        public void TreeAnchorTagWrongClosing()
        {
            var doc = Html(@"<A>test< /A>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Count);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1a0Text0 = dochtml0body1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1a0Text0.NodeType);
            Assert.AreEqual("test< /A>", dochtml0body1a0Text0.TextContent);
        }

        [Test]
        public void TreeSingleEntity()
        {
            var doc = Html(@"&lt;");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("<", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void TreeDoubleBodiesWithAttributes()
        {
            var doc = Html(@"<body foo='bar'><body foo='baz' yo='mama'>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
            Assert.AreEqual("bar", dochtml0body1.Attributes.Get("foo").Value);
            Assert.AreEqual("mama", dochtml0body1.Attributes.Get("yo").Value);
        }

        [Test]
        public void TreeClosingBrWithAttribute()
        {
            var doc = Html(@"<body></br foo=""bar""></body>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1br0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1br0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1br0.Attributes.Count);
            Assert.AreEqual("br", dochtml0body1br0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1br0.NodeType);
        }

        [Test]
        public void TreeBodyTypoWithBrAttributes()
        {
            var doc = Html(@"<bdy><br foo=""bar""></body>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1bdy0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1bdy0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1bdy0.Attributes.Count);
            Assert.AreEqual("bdy", dochtml0body1bdy0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1bdy0.NodeType);

            var dochtml0body1bdy0br0 = dochtml0body1bdy0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1bdy0br0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1bdy0br0.Attributes.Count);
            Assert.AreEqual("br", dochtml0body1bdy0br0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1bdy0br0.NodeType);
            Assert.AreEqual("bar", dochtml0body1bdy0br0.Attributes.Get("foo").Value);
        }

        [Test]
        public void TreeBrClosingWithAttributesOutsideBodyTag()
        {
            var doc = Html(@"<body></body></br foo=""bar"">");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1br0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1br0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1br0.Attributes.Count);
            Assert.AreEqual("br", dochtml0body1br0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1br0.NodeType);
        }

        [Test]
        public void TreeBodyTpyoWithBrOutside()
        {
            var doc = Html(@"<bdy></body><br foo=""bar"">");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1bdy0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1bdy0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1bdy0.Attributes.Count);
            Assert.AreEqual("bdy", dochtml0body1bdy0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1bdy0.NodeType);

            var dochtml0body1bdy0br0 = dochtml0body1bdy0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1bdy0br0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1bdy0br0.Attributes.Count);
            Assert.AreEqual("br", dochtml0body1bdy0br0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1bdy0br0.NodeType);
            Assert.AreEqual("bar", dochtml0body1bdy0br0.Attributes.Get("foo").Value);
        }

        [Test]
        public void TreeCommentAfterDocumentRoot()
        {
            var doc = Html(@"<html><body></body></html><!-- Hi there -->");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var docComment1 = doc.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, docComment1.NodeType);
            Assert.AreEqual(@" Hi there ", docComment1.TextContent);
        }

        [Test]
        public void TreeTextAndCommentAfterDocumentRoot()
        {
            var doc = Html(@"<html><body></body></html>x<!-- Hi there -->");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("x", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@" Hi there ", dochtml0body1Comment1.TextContent);
        }

        [Test]
        public void TreeTextAndCommentAfterDocumentRootTwice()
        {
            var doc = Html(@"<html><body></body></html>x<!-- Hi there --></html><!-- Again -->");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
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

        [Test]
        public void TreeTextAndCommentAfterDocumentRootWithRedundantClosingTags()
        {
            var doc = Html(@"<html><body></body></html>x<!-- Hi there --></body></html><!-- Again -->");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
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

        [Test]
        public void TreeRubyWithDivs()
        {
            var doc = Html(@"<html><body><ruby><div><rp>xx</rp></div></ruby></body></html>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1ruby0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ruby0.Attributes.Count);
            Assert.AreEqual("ruby", dochtml0body1ruby0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0.NodeType);

            var dochtml0body1ruby0div0 = dochtml0body1ruby0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1ruby0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ruby0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1ruby0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0div0.NodeType);

            var dochtml0body1ruby0div0rp0 = dochtml0body1ruby0div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1ruby0div0rp0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ruby0div0rp0.Attributes.Count);
            Assert.AreEqual("rp", dochtml0body1ruby0div0rp0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0div0rp0.NodeType);

            var dochtml0body1ruby0div0rp0Text0 = dochtml0body1ruby0div0rp0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0div0rp0Text0.NodeType);
            Assert.AreEqual("xx", dochtml0body1ruby0div0rp0Text0.TextContent);
        }

        [Test]
        public void TreeRubyAndRtElements()
        {
            var doc = Html(@"<html><body><ruby><div><rt>xx</rt></div></ruby></body></html>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1ruby0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ruby0.Attributes.Count);
            Assert.AreEqual("ruby", dochtml0body1ruby0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0.NodeType);

            var dochtml0body1ruby0div0 = dochtml0body1ruby0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1ruby0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ruby0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1ruby0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0div0.NodeType);

            var dochtml0body1ruby0div0rt0 = dochtml0body1ruby0div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1ruby0div0rt0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ruby0div0rt0.Attributes.Count);
            Assert.AreEqual("rt", dochtml0body1ruby0div0rt0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0div0rt0.NodeType);

            var dochtml0body1ruby0div0rt0Text0 = dochtml0body1ruby0div0rt0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0div0rt0Text0.NodeType);
            Assert.AreEqual("xx", dochtml0body1ruby0div0rt0Text0.TextContent);
        }

        [Test]
        public void TreeFramesetAndNoframesElements()
        {
            var doc = Html(@"<html><frameset><!--1--><noframes>A</noframes><!--2--></frameset><!--3--><noframes>B</noframes><!--4--></html><!--5--><noframes>C</noframes><!--6-->");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(6, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0frameset1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml0frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0frameset1.Attributes.Count);
            Assert.AreEqual("frameset", dochtml0frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1.NodeType);

            var dochtml0frameset1Comment0 = dochtml0frameset1.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml0frameset1Comment0.NodeType);
            Assert.AreEqual(@"1", dochtml0frameset1Comment0.TextContent);

            var dochtml0frameset1noframes1 = dochtml0frameset1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0frameset1noframes1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0frameset1noframes1.Attributes.Count);
            Assert.AreEqual("noframes", dochtml0frameset1noframes1.GetTagName());
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

            var dochtml0noframes3 = dochtml0.ChildNodes[3] as Element;
            Assert.AreEqual(1, dochtml0noframes3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0noframes3.Attributes.Count);
            Assert.AreEqual("noframes", dochtml0noframes3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0noframes3.NodeType);

            var dochtml0noframes3Text0 = dochtml0noframes3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0noframes3Text0.NodeType);
            Assert.AreEqual("B", dochtml0noframes3Text0.TextContent);

            var dochtml0Comment4 = dochtml0.ChildNodes[4];
            Assert.AreEqual(NodeType.Comment, dochtml0Comment4.NodeType);
            Assert.AreEqual(@"4", dochtml0Comment4.TextContent);

            var dochtml0noframes5 = dochtml0.ChildNodes[5] as Element;
            Assert.AreEqual(1, dochtml0noframes5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0noframes5.Attributes.Count);
            Assert.AreEqual("noframes", dochtml0noframes5.GetTagName());
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

        [Test]
        public void TreeSelectOptions()
        {
            var doc = Html(@"<select><option>A<select><option>B<select><option>C<select><option>D<select><option>E<select><option>F<select><option>G<select>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(4, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0option0 = dochtml0body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0option0.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option0.NodeType);

            var dochtml0body1select0option0Text0 = dochtml0body1select0option0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1select0option0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1select0option0Text0.TextContent);

            var dochtml0body1option1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1option1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option1.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1option1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option1.NodeType);

            var dochtml0body1option1Text0 = dochtml0body1option1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option1Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1option1Text0.TextContent);

            var dochtml0body1option1select1 = dochtml0body1option1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1option1select1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option1select1.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1option1select1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option1select1.NodeType);

            var dochtml0body1option1select1option0 = dochtml0body1option1select1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1option1select1option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option1select1option0.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1option1select1option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option1select1option0.NodeType);

            var dochtml0body1option1select1option0Text0 = dochtml0body1option1select1option0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option1select1option0Text0.NodeType);
            Assert.AreEqual("C", dochtml0body1option1select1option0Text0.TextContent);

            var dochtml0body1option2 = dochtml0body1.ChildNodes[2] as Element; ;
            Assert.AreEqual(2, dochtml0body1option2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option2.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1option2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option2.NodeType);

            var dochtml0body1option2Text0 = dochtml0body1option2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option2Text0.NodeType);
            Assert.AreEqual("D", dochtml0body1option2Text0.TextContent);

            var dochtml0body1option2select1 = dochtml0body1option2.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1option2select1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option2select1.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1option2select1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option2select1.NodeType);

            var dochtml0body1option2select1option0 = dochtml0body1option2select1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1option2select1option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option2select1option0.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1option2select1option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option2select1option0.NodeType);

            var dochtml0body1option2select1option0Text0 = dochtml0body1option2select1option0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option2select1option0Text0.NodeType);
            Assert.AreEqual("E", dochtml0body1option2select1option0Text0.TextContent);

            var dochtml0body1option3 = dochtml0body1.ChildNodes[3] as Element;
            Assert.AreEqual(2, dochtml0body1option3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option3.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1option3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option3.NodeType);

            var dochtml0body1option3Text0 = dochtml0body1option3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option3Text0.NodeType);
            Assert.AreEqual("F", dochtml0body1option3Text0.TextContent);

            var dochtml0body1option3select1 = dochtml0body1option3.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1option3select1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option3select1.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1option3select1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option3select1.NodeType);

            var dochtml0body1option3select1option0 = dochtml0body1option3select1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1option3select1option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1option3select1option0.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1option3select1option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option3select1option0.NodeType);

            var dochtml0body1option3select1option0Text0 = dochtml0body1option3select1option0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1option3select1option0Text0.NodeType);
            Assert.AreEqual("G", dochtml0body1option3select1option0Text0.TextContent);
        }

        [Test]
        public void TreeDefinitionList()
        {
            var doc = Html(@"<dd><dd><dt><dt><dd><li><li>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(5, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
            var dochtml0body1dd0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1dd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dd0.Attributes.Count);
            Assert.AreEqual("dd", dochtml0body1dd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1dd0.NodeType);

            var dochtml0body1dd1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1dd1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dd1.Attributes.Count);
            Assert.AreEqual("dd", dochtml0body1dd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1dd1.NodeType);

            var dochtml0body1dt2 = dochtml0body1.ChildNodes[2] as Element; ;
            Assert.AreEqual(0, dochtml0body1dt2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dt2.Attributes.Count);
            Assert.AreEqual("dt", dochtml0body1dt2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1dt2.NodeType);

            var dochtml0body1dt3 = dochtml0body1.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtml0body1dt3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dt3.Attributes.Count);
            Assert.AreEqual("dt", dochtml0body1dt3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1dt3.NodeType);

            var dochtml0body1dd4 = dochtml0body1.ChildNodes[4] as Element;
            Assert.AreEqual(2, dochtml0body1dd4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dd4.Attributes.Count);
            Assert.AreEqual("dd", dochtml0body1dd4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1dd4.NodeType);

            var dochtml0body1dd4li0 = dochtml0body1dd4.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1dd4li0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dd4li0.Attributes.Count);
            Assert.AreEqual("li", dochtml0body1dd4li0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1dd4li0.NodeType);

            var dochtml0body1dd4li1 = dochtml0body1dd4.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1dd4li1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1dd4li1.Attributes.Count);
            Assert.AreEqual("li", dochtml0body1dd4li1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1dd4li1.NodeType);
        }

        [Test]
        public void TreeDivsAndFormatting()
        {
            var doc = Html(@"<div><b></div><div><nobr>a<nobr>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0b0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0b0.Attributes.Count);
            Assert.AreEqual("b", dochtml0body1div0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0b0.NodeType);

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

            var dochtml0body1div1b0 = dochtml0body1div1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1div1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml0body1div1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0.NodeType);

            var dochtml0body1div1b0nobr0 = dochtml0body1div1b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1b0nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml0body1div1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0nobr0.NodeType);

            var dochtml0body1div1b0nobr0Text0 = dochtml0body1div1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div1b0nobr0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1div1b0nobr0Text0.TextContent);

            var dochtml0body1div1b0nobr1 = dochtml0body1div1b0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1div1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div1b0nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml0body1div1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1b0nobr1.NodeType);
        }

        [Test]
        public void TreeStandardStructureWithoutRoot()
        {
            var doc = Html(@"<head></head>
<body></body>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0Text1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0Text1.NodeType);
            Assert.AreEqual("\n", dochtml0Text1.TextContent);

            var dochtml0body2 = dochtml0.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml0body2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body2.Attributes.Count);
            Assert.AreEqual("body", dochtml0body2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body2.NodeType);
        }

        [Test]
        public void TreeStyleTagOutsideHead()
        {
            var doc = Html(@"<head></head> <style></style>ddd");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0style0.Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0Text1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0Text1.NodeType);
            Assert.AreEqual(" ", dochtml0Text1.TextContent);

            var dochtml0body2 = dochtml0.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtml0body2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body2.Attributes.Count);
            Assert.AreEqual("body", dochtml0body2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body2.NodeType);

            var dochtml0body2Text0 = dochtml0body2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body2Text0.NodeType);
            Assert.AreEqual("ddd", dochtml0body2Text0.TextContent);
        }

        [Test]
        public void TreeTableElementMisnestedWithUnknownElement()
        {
            var doc = Html(@"<kbd><table></kbd><col><select><tr>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1kbd0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1kbd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0.Attributes.Count);
            Assert.AreEqual("kbd", dochtml0body1kbd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0.NodeType);

            var dochtml0body1kbd0select0 = dochtml0body1kbd0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1kbd0select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1kbd0select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0select0.NodeType);

            var dochtml0body1kbd0table1 = dochtml0body1kbd0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1kbd0table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1kbd0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1.NodeType);

            var dochtml0body1kbd0table1colgroup0 = dochtml0body1kbd0table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1kbd0table1colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1colgroup0.Attributes.Count);
            Assert.AreEqual("colgroup", dochtml0body1kbd0table1colgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1colgroup0.NodeType);

            var dochtml0body1kbd0table1colgroup0col0 = dochtml0body1kbd0table1colgroup0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1kbd0table1colgroup0col0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1colgroup0col0.Attributes.Count);
            Assert.AreEqual("col", dochtml0body1kbd0table1colgroup0col0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1colgroup0col0.NodeType);

            var dochtml0body1kbd0table1tbody1 = dochtml0body1kbd0table1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1kbd0table1tbody1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1tbody1.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1kbd0table1tbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1tbody1.NodeType);

            var dochtml0body1kbd0table1tbody1tr0 = dochtml0body1kbd0table1tbody1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1kbd0table1tbody1tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1tbody1tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1kbd0table1tbody1tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1tbody1tr0.NodeType);
        }

        [Test]
        public void TreeTableAndSelectElementMistnestedInUnknownElement()
        {
            var doc = Html(@"<kbd><table></kbd><col><select><tr></table><div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1kbd0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml0body1kbd0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0.Attributes.Count);
            Assert.AreEqual("kbd", dochtml0body1kbd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0.NodeType);

            var dochtml0body1kbd0select0 = dochtml0body1kbd0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1kbd0select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1kbd0select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0select0.NodeType);

            var dochtml0body1kbd0table1 = dochtml0body1kbd0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1kbd0table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1kbd0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1.NodeType);

            var dochtml0body1kbd0table1colgroup0 = dochtml0body1kbd0table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1kbd0table1colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1colgroup0.Attributes.Count);
            Assert.AreEqual("colgroup", dochtml0body1kbd0table1colgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1colgroup0.NodeType);

            var dochtml0body1kbd0table1colgroup0col0 = dochtml0body1kbd0table1colgroup0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1kbd0table1colgroup0col0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1colgroup0col0.Attributes.Count);
            Assert.AreEqual("col", dochtml0body1kbd0table1colgroup0col0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1colgroup0col0.NodeType);

            var dochtml0body1kbd0table1tbody1 = dochtml0body1kbd0table1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1kbd0table1tbody1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1tbody1.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1kbd0table1tbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1tbody1.NodeType);

            var dochtml0body1kbd0table1tbody1tr0 = dochtml0body1kbd0table1tbody1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1kbd0table1tbody1tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0table1tbody1tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1kbd0table1tbody1tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0table1tbody1tr0.NodeType);

            var dochtml0body1kbd0div2 = dochtml0body1kbd0.ChildNodes[2] as Element; ;
            Assert.AreEqual(0, dochtml0body1kbd0div2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1kbd0div2.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1kbd0div2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1kbd0div2.NodeType);
        }

        [Test]
        public void TreeVariousTagsInsideAnchorElement()
        {
            var doc = Html(@"<a><li><style></style><title></title></a>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Count);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1li1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1li1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1li1.Attributes.Count);
            Assert.AreEqual("li", dochtml0body1li1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1li1.NodeType);

            var dochtml0body1li1a0 = dochtml0body1li1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1li1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1li1a0.Attributes.Count);
            Assert.AreEqual("a", dochtml0body1li1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1li1a0.NodeType);

            var dochtml0body1li1a0style0 = dochtml0body1li1a0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1li1a0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1li1a0style0.Attributes.Count);
            Assert.AreEqual("style", dochtml0body1li1a0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1li1a0style0.NodeType);

            var dochtml0body1li1a0title1 = dochtml0body1li1a0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1li1a0title1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1li1a0title1.Attributes.Count);
            Assert.AreEqual("title", dochtml0body1li1a0title1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1li1a0title1.NodeType);
        }

        [Test]
        public void TreeVariousTagsInsideFontElement()
        {
            var doc = Html(@"<font></p><p><meta><title></title></font>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1font0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1font0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1font0.Attributes.Count);
            Assert.AreEqual("font", dochtml0body1font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1font0.NodeType);

            var dochtml0body1font0p0 = dochtml0body1font0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1font0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1font0p0.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1font0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1font0p0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1font0 = dochtml0body1p1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1p1font0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1font0.Attributes.Count);
            Assert.AreEqual("font", dochtml0body1p1font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0.NodeType);

            var dochtml0body1p1font0meta0 = dochtml0body1p1font0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1p1font0meta0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1font0meta0.Attributes.Count);
            Assert.AreEqual("meta", dochtml0body1p1font0meta0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0meta0.NodeType);

            var dochtml0body1p1font0title1 = dochtml0body1p1font0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1p1font0title1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1font0title1.Attributes.Count);
            Assert.AreEqual("title", dochtml0body1p1font0title1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1font0title1.NodeType);
        }

        [Test]
        public void TreeCenterTitleElementInAnchorElement()
        {
            var doc = Html(@"<a><center><title></title><a>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1a0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1a0.Attributes.Count);
            Assert.AreEqual("a", dochtml0body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1a0.NodeType);

            var dochtml0body1center1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1center1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1center1.Attributes.Count);
            Assert.AreEqual("center", dochtml0body1center1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1center1.NodeType);

            var dochtml0body1center1a0 = dochtml0body1center1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1center1a0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1center1a0.Attributes.Count);
            Assert.AreEqual("a", dochtml0body1center1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1center1a0.NodeType);

            var dochtml0body1center1a0title0 = dochtml0body1center1a0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1center1a0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1center1a0title0.Attributes.Count);
            Assert.AreEqual("title", dochtml0body1center1a0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1center1a0title0.NodeType);

            var dochtml0body1center1a1 = dochtml0body1center1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1center1a1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1center1a1.Attributes.Count);
            Assert.AreEqual("a", dochtml0body1center1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1center1a1.NodeType);
        }

        [Test]
        public void TreeSvgElementWithTitleAndDiv()
        {
            var doc = Html(@"<svg><title><div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0title0 = dochtml0body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1svg0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0.Attributes.Count);
            Assert.AreEqual("title", dochtml0body1svg0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0.NodeType);

            var dochtml0body1svg0title0div0 = dochtml0body1svg0title0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1svg0title0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1svg0title0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0div0.NodeType);
        }

        [Test]
        public void TreeSvgElementWithTitleAndRectAndDiv()
        {
            var doc = Html(@"<svg><title><rect><div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0title0 = dochtml0body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1svg0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0.Attributes.Count);
            Assert.AreEqual("title", dochtml0body1svg0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0.NodeType);

            var dochtml0body1svg0title0rect0 = dochtml0body1svg0title0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1svg0title0rect0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0rect0.Attributes.Count);
            Assert.AreEqual("rect", dochtml0body1svg0title0rect0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0rect0.NodeType);

            var dochtml0body1svg0title0rect0div0 = dochtml0body1svg0title0rect0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1svg0title0rect0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0rect0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1svg0title0rect0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0rect0div0.NodeType);
        }

        [Test]
        public void TreeSvgElementWithTitleRepeated()
        {
            var doc = Html(@"<svg><title><svg><div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0title0 = dochtml0body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1svg0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0.Attributes.Count);
            Assert.AreEqual("title", dochtml0body1svg0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0.NodeType);

            var dochtml0body1svg0title0svg0 = dochtml0body1svg0title0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1svg0title0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0title0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0svg0.NodeType);

            var dochtml0body1svg0title0div1 = dochtml0body1svg0title0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1svg0title0div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0title0div1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1svg0title0div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0title0div1.NodeType);
        }

        [Test]
        public void TreeImageWithFailedContent()
        {
            var doc = Html(@"<img <="""" FAIL>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1img0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1img0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1img0.Attributes.Count);
            Assert.AreEqual("img", dochtml0body1img0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1img0.NodeType);
            Assert.AreEqual("", dochtml0body1img0.GetAttribute("fail"));
            Assert.AreEqual("", dochtml0body1img0.GetAttribute("<"));
        }

        [Test]
        public void TreeUnorderedListWithDivElements()
        {
            var doc = Html(@"<ul><li><div id='foo'/>A</li><li>B<div>C</div></li></ul>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1ul0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1ul0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ul0.Attributes.Count);
            Assert.AreEqual("ul", dochtml0body1ul0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0.NodeType);

            var dochtml0body1ul0li0 = dochtml0body1ul0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1ul0li0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ul0li0.Attributes.Count);
            Assert.AreEqual("li", dochtml0body1ul0li0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li0.NodeType);

            var dochtml0body1ul0li0div0 = dochtml0body1ul0li0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1ul0li0div0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1ul0li0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1ul0li0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li0div0.NodeType);
            Assert.AreEqual("foo", dochtml0body1ul0li0div0.Attributes.Get("id").Value);

            var dochtml0body1ul0li0div0Text0 = dochtml0body1ul0li0div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ul0li0div0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1ul0li0div0Text0.TextContent);

            var dochtml0body1ul0li1 = dochtml0body1ul0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1ul0li1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ul0li1.Attributes.Count);
            Assert.AreEqual("li", dochtml0body1ul0li1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li1.NodeType);

            var dochtml0body1ul0li1Text0 = dochtml0body1ul0li1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ul0li1Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1ul0li1Text0.TextContent);

            var dochtml0body1ul0li1div1 = dochtml0body1ul0li1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1ul0li1div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1ul0li1div1.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1ul0li1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ul0li1div1.NodeType);

            var dochtml0body1ul0li1div1Text0 = dochtml0body1ul0li1div1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ul0li1div1Text0.NodeType);
            Assert.AreEqual("C", dochtml0body1ul0li1div1Text0.TextContent);
        }

        [Test]
        public void TreeSvgWithEmAndDescElements()
        {
            var doc = Html(@"<svg><em><desc></em>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1em1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1em1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1em1.Attributes.Count);
            Assert.AreEqual("em", dochtml0body1em1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1em1.NodeType);

            var dochtml0body1em1desc0 = dochtml0body1em1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1em1desc0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1em1desc0.Attributes.Count);
            Assert.AreEqual("desc", dochtml0body1em1desc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1em1desc0.NodeType);
        }

        [Test]
        public void TreeSvgWithTfootAndClosingMiElement()
        {
            var doc = Html(@"<svg><tfoot></mi><td>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0tfoot0 = dochtml0body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1svg0tfoot0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0tfoot0.Attributes.Count);
            Assert.AreEqual("tfoot", dochtml0body1svg0tfoot0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0tfoot0.NodeType);

            var dochtml0body1svg0tfoot0td0 = dochtml0body1svg0tfoot0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1svg0tfoot0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0tfoot0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1svg0tfoot0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0tfoot0td0.NodeType);
        }

        [Test]
        public void TreeMathWithMrowsAndOtherElements()
        {
            var doc = Html(@"<math><mrow><mrow><mn>1</mn></mrow><mi>a</mi></mrow></math>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mrow0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0mrow0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mrow0.Attributes.Count);
            Assert.AreEqual("mrow", dochtml0body1math0mrow0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mrow0.NodeType);

            var dochtml0body1math0mrow0mrow0 = dochtml0body1math0mrow0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mrow0mrow0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mrow0mrow0.Attributes.Count);
            Assert.AreEqual("mrow", dochtml0body1math0mrow0mrow0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mrow0mrow0.NodeType);

            var dochtml0body1math0mrow0mrow0mn0 = dochtml0body1math0mrow0mrow0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mrow0mrow0mn0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mrow0mrow0mn0.Attributes.Count);
            Assert.AreEqual("mn", dochtml0body1math0mrow0mrow0mn0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mrow0mrow0mn0.NodeType);

            var dochtml0body1math0mrow0mrow0mn0Text0 = dochtml0body1math0mrow0mrow0mn0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1math0mrow0mrow0mn0Text0.NodeType);
            Assert.AreEqual("1", dochtml0body1math0mrow0mrow0mn0Text0.TextContent);

            var dochtml0body1math0mrow0mi1 = dochtml0body1math0mrow0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1math0mrow0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mrow0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mrow0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mrow0mi1.NodeType);

            var dochtml0body1math0mrow0mi1Text0 = dochtml0body1math0mrow0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1math0mrow0mi1Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1math0mrow0mi1Text0.TextContent);
        }

        [Test]
        public void TreeDocTypeWithInputHiddenAndFrameset()
        {
            var doc = Html(@"<!doctype html><input type=""hidden""><frameset>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1frameset1.Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);
        }

        [Test]
        public void TreeDocTypeWithInputButtonAndFrameset()
        {
            var doc = Html(@"<!doctype html><input type=""button""><frameset>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1input0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1input0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1input0.Attributes.Count);
            Assert.AreEqual("input", dochtml1body1input0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1input0.NodeType);
            Assert.AreEqual("button", dochtml1body1input0.Attributes.Get("type").Value);
        }

        [Test]
        public void TreeUnknownTagSelfClosing()
        {
            var doc = Html(@"<foo bar=qux/>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1foo0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1foo0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1foo0.Attributes.Count);
            Assert.AreEqual("foo", dochtml0body1foo0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1foo0.NodeType);
            Assert.AreEqual("qux/", dochtml0body1foo0.Attributes.Get("bar").Value);
        }

        [Test]
        public void TreeParagraphWithTightAttributesAndNoScriptTagScriptingEnabled()
        {
            var source = @"<p id=""status""><noscript><strong>A</strong></noscript><span>B</span></p>";
            var config = new Configuration().With(new EnableScripting());
            var parser = new HtmlParser(source, config);
            var doc = parser.Parse();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);
            Assert.AreEqual("status", dochtml0body1p0.Attributes.Get("id").Value);

            var dochtml0body1p0noscript0 = dochtml0body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0noscript0.Attributes.Count);
            Assert.AreEqual("noscript", dochtml0body1p0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0noscript0.NodeType);

            var dochtml0body1p0noscript0Text0 = dochtml0body1p0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0noscript0Text0.NodeType);
            Assert.AreEqual("<strong>A</strong>", dochtml0body1p0noscript0Text0.TextContent);

            var dochtml0body1p0span1 = dochtml0body1p0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1p0span1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0span1.Attributes.Count);
            Assert.AreEqual("span", dochtml0body1p0span1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0span1.NodeType);

            var dochtml0body1p0span1Text0 = dochtml0body1p0span1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0span1Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1p0span1Text0.TextContent);
        }

        [Test]
        public void TreeParagraphWithTightAttributesAndNoScriptTagScriptingDisabled()
        {
            //Scripting is disabled by default
            var doc = Html(@"<p id=""status""><noscript><strong>A</strong></noscript><span>B</span></p>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1p0.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);
            Assert.AreEqual("status", dochtml0body1p0.Attributes.Get("id").Value);

            var dochtml0body1p0noscript0 = dochtml0body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0noscript0.Attributes.Count);
            Assert.AreEqual("noscript", dochtml0body1p0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0noscript0.NodeType);

            var dochtml0body1p0noscript0Strong0 = dochtml0body1p0noscript0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0noscript0Strong0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0noscript0Strong0.Attributes.Count);
            Assert.AreEqual("strong", dochtml0body1p0noscript0Strong0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0noscript0Strong0.NodeType);

            var dochtml0body1p0noscript0Strong0Text = dochtml0body1p0noscript0Strong0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0noscript0Strong0Text.NodeType);
            Assert.AreEqual("A", dochtml0body1p0noscript0Strong0Text.TextContent);

            var dochtml0body1p0span1 = dochtml0body1p0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1p0span1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0span1.Attributes.Count);
            Assert.AreEqual("span", dochtml0body1p0span1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0span1.NodeType);

            var dochtml0body1p0span1Text0 = dochtml0body1p0span1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p0span1Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1p0span1Text0.TextContent);
        }

        [Test]
        public void TreeSarcasmTagUsed()
        {
            var doc = Html(@"<div><sarcasm><div></div></sarcasm></div>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0sarcasm0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0sarcasm0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0sarcasm0.Attributes.Count);
            Assert.AreEqual("sarcasm", dochtml0body1div0sarcasm0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0sarcasm0.NodeType);

            var dochtml0body1div0sarcasm0div0 = dochtml0body1div0sarcasm0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0sarcasm0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0sarcasm0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0sarcasm0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0sarcasm0div0.NodeType);
        }

        [Test]
        public void TreeImageWithOpeningDoubleQuotesAltAttribute()
        {
            var doc = Html(@"<html><body><img src="""" border=""0"" alt=""><div>A</div></body></html>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TreeWithMisnestedClosingTableBodySection()
        {
            var doc = Html(@"<table><td></tbody>A");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table1tbody0 = dochtml0body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0.NodeType);

            var dochtml0body1table1tbody0tr0 = dochtml0body1table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0.NodeType);

            var dochtml0body1table1tbody0tr0td0 = dochtml0body1table1tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table1tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0td0.NodeType);
        }

        [Test]
        public void TreeWithMisnestedClosingTableHeadSection()
        {
            var doc = Html(@"<table><td></thead>A");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0Text0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1table0tbody0tr0td0Text0.TextContent);
        }

        [Test]
        public void TreeWithMisnestedClosingTableFootSection()
        {
            var doc = Html(@"<table><td></tfoot>A");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0Text0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1table0tbody0tr0td0Text0.TextContent);
        }

        [Test]
        public void TreeWithTableHeadSectionAndMisnestedClosingTableBodySection()
        {
            var doc = Html(@"<table><thead><td></tbody>A");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0thead0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0thead0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0.Attributes.Count);
            Assert.AreEqual("thead", dochtml0body1table0thead0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0.NodeType);

            var dochtml0body1table0thead0tr0 = dochtml0body1table0thead0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0thead0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0thead0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0tr0.NodeType);

            var dochtml0body1table0thead0tr0td0 = dochtml0body1table0thead0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0thead0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0thead0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0thead0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0thead0tr0td0.NodeType);

            var dochtml0body1table0thead0tr0td0Text0 = dochtml0body1table0thead0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0thead0tr0td0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1table0thead0tr0td0Text0.TextContent);
        }

        [Test]
        public void TreeNobrTagsInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><a href='#1'><nobr>1<nobr></a><br><a href='#2'><nobr>2<nobr></a><br><a href='#3'><nobr>3<nobr></a>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(5, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1a0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1a0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1a0.Attributes.Count);
            Assert.AreEqual("a", dochtml1body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a0.NodeType);
            Assert.IsNotNull(dochtml1body1a0.Attributes.Get("href"));
            Assert.AreEqual("href", dochtml1body1a0.Attributes.Get("href").Name);
            Assert.AreEqual("#1", dochtml1body1a0.Attributes.Get("href").Value);

            var dochtml1body1a0nobr0 = dochtml1body1a0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1a0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1a0nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1a0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a0nobr0.NodeType);

            var dochtml1body1a0nobr0Text0 = dochtml1body1a0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1a0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1a0nobr0Text0.TextContent);

            var dochtml1body1a0nobr1 = dochtml1body1a0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1a0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1a0nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1a0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a0nobr1.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1br0 = dochtml1body1nobr1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1nobr1br0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1br0.Attributes.Count);
            Assert.AreEqual("br", dochtml1body1nobr1br0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1br0.NodeType);

            var dochtml1body1nobr1a1 = dochtml1body1nobr1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1nobr1a1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1nobr1a1.Attributes.Count);
            Assert.AreEqual("a", dochtml1body1nobr1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1a1.NodeType);
            Assert.IsNotNull(dochtml1body1nobr1a1.Attributes.Get("href"));
            Assert.AreEqual("href", dochtml1body1nobr1a1.Attributes.Get("href").Name);
            Assert.AreEqual("#2", dochtml1body1nobr1a1.Attributes.Get("href").Value);

            var dochtml1body1a2 = dochtml1body1.ChildNodes[2] as Element; ;
            Assert.AreEqual(2, dochtml1body1a2.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1a2.Attributes.Count);
            Assert.AreEqual("a", dochtml1body1a2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a2.NodeType);
            Assert.IsNotNull(dochtml1body1a2.Attributes.Get("href"));
            Assert.AreEqual("href", dochtml1body1a2.Attributes.Get("href").Name);
            Assert.AreEqual("#2", dochtml1body1a2.Attributes.Get("href").Value);

            var dochtml1body1a2nobr0 = dochtml1body1a2.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1a2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1a2nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1a2nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a2nobr0.NodeType);

            var dochtml1body1a2nobr0Text0 = dochtml1body1a2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1a2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1a2nobr0Text0.TextContent);

            var dochtml1body1a2nobr1 = dochtml1body1a2.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1a2nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1a2nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1a2nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a2nobr1.NodeType);

            var dochtml1body1nobr3 = dochtml1body1.ChildNodes[3] as Element;
            Assert.AreEqual(2, dochtml1body1nobr3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr3.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr3.NodeType);

            var dochtml1body1nobr3br0 = dochtml1body1nobr3.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1nobr3br0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr3br0.Attributes.Count);
            Assert.AreEqual("br", dochtml1body1nobr3br0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr3br0.NodeType);

            var dochtml1body1nobr3a1 = dochtml1body1nobr3.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1nobr3a1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1nobr3a1.Attributes.Count);
            Assert.AreEqual("a", dochtml1body1nobr3a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr3a1.NodeType);
            Assert.IsNotNull(dochtml1body1nobr3a1.Attributes.Get("href"));
            Assert.AreEqual("href", dochtml1body1nobr3a1.Attributes.Get("href").Name);
            Assert.AreEqual("#3", dochtml1body1nobr3a1.Attributes.Get("href").Value);

            var dochtml1body1a4 = dochtml1body1.ChildNodes[4] as Element;
            Assert.AreEqual(2, dochtml1body1a4.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1a4.Attributes.Count);
            Assert.AreEqual("a", dochtml1body1a4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a4.NodeType);
            Assert.IsNotNull(dochtml1body1a4.Attributes.Get("href"));
            Assert.AreEqual("href", dochtml1body1a4.Attributes.Get("href").Name);
            Assert.AreEqual("#3", dochtml1body1a4.Attributes.Get("href").Value);

            var dochtml1body1a4nobr0 = dochtml1body1a4.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1a4nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1a4nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1a4nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a4nobr0.NodeType);

            var dochtml1body1a4nobr0Text0 = dochtml1body1a4nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1a4nobr0Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1a4nobr0Text0.TextContent);

            var dochtml1body1a4nobr1 = dochtml1body1a4.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1a4nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1a4nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1a4nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a4nobr1.NodeType);
        }

        [Test]
        public void TreeNobrAndFormattingTagsInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<nobr></b><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(4, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1i0 = dochtml1body1nobr1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1i0.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1nobr1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1i0.NodeType);

            var dochtml1body1i2 = dochtml1body1.ChildNodes[2] as Element; ;
            Assert.AreEqual(2, dochtml1body1i2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2.NodeType);

            var dochtml1body1i2nobr0 = dochtml1body1i2.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1i2nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2nobr0.NodeType);

            var dochtml1body1i2nobr0Text0 = dochtml1body1i2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1i2nobr0Text0.TextContent);

            var dochtml1body1i2nobr1 = dochtml1body1i2.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1i2nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1i2nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2nobr1.NodeType);

            var dochtml1body1nobr3 = dochtml1body1.ChildNodes[3] as Element;
            Assert.AreEqual(1, dochtml1body1nobr3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr3.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr3.NodeType);

            var dochtml1body1nobr3Text0 = dochtml1body1nobr3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1nobr3Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1nobr3Text0.TextContent);
        }

        [Test]
        public void TreeNobrAndTableTagsInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<table><nobr></b><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0] as Element;
            Assert.AreEqual(5, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr0nobr1 = dochtml1body1b0nobr0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0nobr1.NodeType);

            var dochtml1body1b0nobr0nobr1i0 = dochtml1body1b0nobr0nobr1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1b0nobr0nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0nobr1i0.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1b0nobr0nobr1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0nobr1i0.NodeType);

            var dochtml1body1b0nobr0i2 = dochtml1body1b0nobr0.ChildNodes[2] as Element;
            Assert.AreEqual(2, dochtml1body1b0nobr0i2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0i2.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1b0nobr0i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0i2.NodeType);

            var dochtml1body1b0nobr0i2nobr0 = dochtml1body1b0nobr0i2.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr0i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0i2nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0i2nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0i2nobr0.NodeType);

            var dochtml1body1b0nobr0i2nobr0Text0 = dochtml1body1b0nobr0i2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0i2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1b0nobr0i2nobr0Text0.TextContent);

            var dochtml1body1b0nobr0i2nobr1 = dochtml1body1b0nobr0i2.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1b0nobr0i2nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0i2nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0i2nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0i2nobr1.NodeType);

            var dochtml1body1b0nobr0nobr3 = dochtml1body1b0nobr0.ChildNodes[3] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr0nobr3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0nobr3.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0nobr3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0nobr3.NodeType);

            var dochtml1body1b0nobr0nobr3Text0 = dochtml1body1b0nobr0nobr3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0nobr3Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1b0nobr0nobr3Text0.TextContent);

            var dochtml1body1b0nobr0table4 = dochtml1body1b0nobr0.ChildNodes[4] as Element;
            Assert.AreEqual(0, dochtml1body1b0nobr0table4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table4.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1b0nobr0table4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table4.NodeType);
        }

        [Test]
        public void TreeNoBrAndTableCellTagsInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<table><tr><td><nobr></b><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr0table1 = dochtml1body1b0nobr0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr0table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1b0nobr0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1.NodeType);

            var dochtml1body1b0nobr0table1tbody0 = dochtml1body1b0nobr0table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1b0nobr0table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0 = dochtml1body1b0nobr0table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1b0nobr0table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0 = dochtml1body1b0nobr0table1tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml1body1b0nobr0table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml1body1b0nobr0table1tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0nobr0 = dochtml1body1b0nobr0table1tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0tr0td0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0table1tbody0tr0td0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0nobr0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0 = dochtml1body1b0nobr0table1tbody0tr0td0nobr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0nobr0i0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0i1 = dochtml1body1b0nobr0table1tbody0tr0td0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1b0nobr0table1tbody0tr0td0i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0i1.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1b0nobr0table1tbody0tr0td0i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0i1.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0 = dochtml1body1b0nobr0table1tbody0tr0td0i1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0Text0 = dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1b0nobr0table1tbody0tr0td0i1nobr0Text0.TextContent);

            var dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1 = dochtml1body1b0nobr0table1tbody0tr0td0i1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0i1nobr1.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0nobr2 = dochtml1body1b0nobr0table1tbody0tr0td0.ChildNodes[2] as Element; ;
            Assert.AreEqual(1, dochtml1body1b0nobr0table1tbody0tr0td0nobr2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0table1tbody0tr0td0nobr2.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0table1tbody0tr0td0nobr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0table1tbody0tr0td0nobr2.NodeType);

            var dochtml1body1b0nobr0table1tbody0tr0td0nobr2Text0 = dochtml1body1b0nobr0table1tbody0tr0td0nobr2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0table1tbody0tr0td0nobr2Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1b0nobr0table1tbody0tr0td0nobr2Text0.TextContent);
        }

        [Test]
        public void TreeNobrAndDivTagsInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<div><nobr></b><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1div1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(4, dochtml1body1div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1.Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1.NodeType);

            var dochtml1body1div1b0 = dochtml1body1div1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1div1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml1body1div1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1b0.NodeType);

            var dochtml1body1div1b0nobr0 = dochtml1body1div1b0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1div1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1b0nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1b0nobr0.NodeType);

            var dochtml1body1div1b0nobr1 = dochtml1body1div1b0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1div1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1b0nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1b0nobr1.NodeType);

            var dochtml1body1div1nobr1 = dochtml1body1div1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1div1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr1.NodeType);

            var dochtml1body1div1nobr1i0 = dochtml1body1div1nobr1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1div1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1nobr1i0.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1div1nobr1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr1i0.NodeType);

            var dochtml1body1div1i2 = dochtml1body1div1.ChildNodes[2] as Element; ;
            Assert.AreEqual(2, dochtml1body1div1i2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1i2.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1div1i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i2.NodeType);

            var dochtml1body1div1i2nobr0 = dochtml1body1div1i2.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1div1i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1i2nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1i2nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i2nobr0.NodeType);

            var dochtml1body1div1i2nobr0Text0 = dochtml1body1div1i2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div1i2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1div1i2nobr0Text0.TextContent);

            var dochtml1body1div1i2nobr1 = dochtml1body1div1i2.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1div1i2nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1i2nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1i2nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i2nobr1.NodeType);

            var dochtml1body1div1nobr3 = dochtml1body1div1.ChildNodes[3] as Element;
            Assert.AreEqual(1, dochtml1body1div1nobr3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1nobr3.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1nobr3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr3.NodeType);

            var dochtml1body1div1nobr3Text0 = dochtml1body1div1nobr3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div1nobr3Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1div1nobr3Text0.TextContent);
        }

        [Test]
        public void TreeNobrAndBoldAndDivTagsInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<nobr></b><div><i><nobr>2<nobr></i>3");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1div1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml1body1div1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1.Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1.NodeType);

            var dochtml1body1div1nobr0 = dochtml1body1div1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1div1nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr0.NodeType);

            var dochtml1body1div1nobr0i0 = dochtml1body1div1nobr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1div1nobr0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1nobr0i0.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1div1nobr0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr0i0.NodeType);

            var dochtml1body1div1i1 = dochtml1body1div1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1div1i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1i1.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1div1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i1.NodeType);

            var dochtml1body1div1i1nobr0 = dochtml1body1div1i1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1div1i1nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1i1nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1i1nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i1nobr0.NodeType);

            var dochtml1body1div1i1nobr0Text0 = dochtml1body1div1i1nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div1i1nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1div1i1nobr0Text0.TextContent);

            var dochtml1body1div1i1nobr1 = dochtml1body1div1i1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1div1i1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1i1nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1i1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1i1nobr1.NodeType);

            var dochtml1body1div1nobr2 = dochtml1body1div1.ChildNodes[2] as Element; ;
            Assert.AreEqual(1, dochtml1body1div1nobr2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div1nobr2.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1div1nobr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div1nobr2.NodeType);

            var dochtml1body1div1nobr2Text0 = dochtml1body1div1nobr2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div1nobr2Text0.NodeType);
            Assert.AreEqual("3", dochtml1body1div1nobr2Text0.TextContent);
        }

        [Test]
        public void TreeNobrAndInsTagInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<nobr><ins></b><i><nobr>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1b0nobr1ins0 = dochtml1body1b0nobr1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1b0nobr1ins0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr1ins0.Attributes.Count);
            Assert.AreEqual("ins", dochtml1body1b0nobr1ins0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1ins0.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1i0 = dochtml1body1nobr1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1i0.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1nobr1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1i0.NodeType);

            var dochtml1body1i2 = dochtml1body1.ChildNodes[2] as Element; ;
            Assert.AreEqual(1, dochtml1body1i2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2.NodeType);

            var dochtml1body1i2nobr0 = dochtml1body1i2.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1i2nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2nobr0.NodeType);
        }

        [Test]
        public void TreeNobrAndInsTagWithBoldInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><b><nobr>1<ins><nobr></b><i>2");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0nobr0 = dochtml1body1b0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1b0nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0.NodeType);

            var dochtml1body1b0nobr0Text0 = dochtml1body1b0nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0nobr0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0nobr0Text0.TextContent);

            var dochtml1body1b0nobr0ins1 = dochtml1body1b0nobr0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1b0nobr0ins1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr0ins1.Attributes.Count);
            Assert.AreEqual("ins", dochtml1body1b0nobr0ins1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr0ins1.NodeType);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1i0 = dochtml1body1nobr1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1i0.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1nobr1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1i0.NodeType);

            var dochtml1body1nobr1i0Text0 = dochtml1body1nobr1i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1nobr1i0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1nobr1i0Text0.TextContent);
        }

        [Test]
        public void TreeNobrAndItalicTagsInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><b>1<nobr></b><i><nobr>2</i>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0.Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0Text0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0Text0.NodeType);
            Assert.AreEqual("1", dochtml1body1b0Text0.TextContent);

            var dochtml1body1b0nobr1 = dochtml1body1b0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1b0nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1b0nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1b0nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0nobr1.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr1i0 = dochtml1body1nobr1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1nobr1i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1nobr1i0.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1nobr1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1i0.NodeType);

            var dochtml1body1i2 = dochtml1body1.ChildNodes[2] as Element; ;
            Assert.AreEqual(1, dochtml1body1i2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2.NodeType);

            var dochtml1body1i2nobr0 = dochtml1body1i2.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1i2nobr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1i2nobr0.Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1i2nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i2nobr0.NodeType);

            var dochtml1body1i2nobr0Text0 = dochtml1body1i2nobr0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i2nobr0Text0.NodeType);
            Assert.AreEqual("2", dochtml1body1i2nobr0Text0.TextContent);
        }

        [Test]
        public void TreeMisopenedCodeTagInParagraph()
        {
            var doc = Html(@"<p><code x</code></p>
");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p0.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0code0 = dochtml0body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1p0code0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1p0code0.Attributes.Count);
            Assert.AreEqual("code", dochtml0body1p0code0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0code0.NodeType);
            Assert.IsNotNull(dochtml0body1p0code0.Attributes.Get("code"));
            Assert.AreEqual("code", dochtml0body1p0code0.Attributes.Get("code").Name);
            Assert.AreEqual("", dochtml0body1p0code0.Attributes.Get("code").Value);
            Assert.IsNotNull(dochtml0body1p0code0.Attributes.Get("x<"));
            Assert.AreEqual("x<", dochtml0body1p0code0.Attributes.Get("x<").Name);
            Assert.AreEqual("", dochtml0body1p0code0.Attributes.Get("x<").Value);

            var dochtml0body1code1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1code1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml0body1code1.Attributes.Count);
            Assert.AreEqual("code", dochtml0body1code1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1code1.NodeType);
            Assert.IsNotNull(dochtml0body1code1.Attributes.Get("code"));
            Assert.AreEqual("code", dochtml0body1code1.Attributes.Get("code").Name);
            Assert.AreEqual("", dochtml0body1code1.Attributes.Get("code").Value);
            Assert.IsNotNull(dochtml0body1code1.Attributes.Get("x<"));
            Assert.AreEqual("x<", dochtml0body1code1.Attributes.Get("x<").Name);
            Assert.AreEqual("", dochtml0body1code1.Attributes.Get("x<").Value);

            var dochtml0body1code1Text0 = dochtml0body1code1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1code1Text0.NodeType);
            Assert.AreEqual("\n", dochtml0body1code1Text0.TextContent);
        }

        [Test]
        public void TreeItalicInParagraphInForeignObjectInSvg()
        {
            var doc = Html(@"<!DOCTYPE html><svg><foreignObject><p><i></p>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0foreignObject0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject0.Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0.NodeType);

            var dochtml1body1svg0foreignObject0p0 = dochtml1body1svg0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject0p0.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1svg0foreignObject0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0p0.NodeType);

            var dochtml1body1svg0foreignObject0p0i0 = dochtml1body1svg0foreignObject0p0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0foreignObject0p0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject0p0i0.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1svg0foreignObject0p0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0p0i0.NodeType);

            var dochtml1body1svg0foreignObject0i1 = dochtml1body1svg0foreignObject0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0foreignObject0i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject0i1.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1svg0foreignObject0i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0i1.NodeType);

            var dochtml1body1svg0foreignObject0i1Text0 = dochtml1body1svg0foreignObject0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0foreignObject0i1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1svg0foreignObject0i1Text0.TextContent);
        }

        [Test]
        public void TreeTableWithSvgInTableCell()
        {
            var doc = Html(@"<!DOCTYPE html><table><tr><td><svg><foreignObject><p><i></p>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1table0tbody0tr0td0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0 = dochtml1body1table0tbody0tr0td0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0foreignObject0.Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml1body1table0tbody0tr0td0svg0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0foreignObject0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0p0 = dochtml1body1table0tbody0tr0td0svg0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0 = dochtml1body1table0tbody0tr0td0svg0foreignObject0p0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0foreignObject0p0i0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0i1 = dochtml1body1table0tbody0tr0td0svg0foreignObject0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0foreignObject0i1Text0 = dochtml1body1table0tbody0tr0td0svg0foreignObject0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0svg0foreignObject0i1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1table0tbody0tr0td0svg0foreignObject0i1Text0.TextContent);
        }

        [Test]
        public void TreeItalicInParagraphInMtextInMath()
        {
            var doc = Html(@"<!DOCTYPE html><math><mtext><p><i></p>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mtext0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mtext0.Attributes.Count);
            Assert.AreEqual("mtext", dochtml1body1math0mtext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mtext0.NodeType);

            var dochtml1body1math0mtext0p0 = dochtml1body1math0mtext0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mtext0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mtext0p0.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1math0mtext0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mtext0p0.NodeType);

            var dochtml1body1math0mtext0p0i0 = dochtml1body1math0mtext0p0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mtext0p0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mtext0p0i0.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1math0mtext0p0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mtext0p0i0.NodeType);

            var dochtml1body1math0mtext0i1 = dochtml1body1math0mtext0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mtext0i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mtext0i1.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1math0mtext0i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mtext0i1.NodeType);

            var dochtml1body1math0mtext0i1Text0 = dochtml1body1math0mtext0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mtext0i1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1math0mtext0i1Text0.TextContent);
        }

        [Test]
        public void TreeTableWithMathInTableCell()
        {
            var doc = Html(@"<!DOCTYPE html><table><tr><td><math><mtext><p><i></p>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1table0tbody0tr0td0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mtext0.Attributes.Count);
            Assert.AreEqual("mtext", dochtml1body1table0tbody0tr0td0math0mtext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mtext0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0p0 = dochtml1body1table0tbody0tr0td0math0mtext0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mtext0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mtext0p0.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1table0tbody0tr0td0math0mtext0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mtext0p0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0p0i0 = dochtml1body1table0tbody0tr0td0math0mtext0p0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mtext0p0i0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mtext0p0i0.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0math0mtext0p0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mtext0p0i0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0i1 = dochtml1body1table0tbody0tr0td0math0mtext0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mtext0i1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mtext0i1.Attributes.Count);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0math0mtext0i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mtext0i1.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mtext0i1Text0 = dochtml1body1table0tbody0tr0td0math0mtext0i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mtext0i1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1table0tbody0tr0td0math0mtext0i1Text0.TextContent);
        }

        [Test]
        public void TreeDivWithMisclosedTagInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><div><!/div>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1div0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0.NodeType);

            var dochtml1body1div0Comment0 = dochtml1body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml1body1div0Comment0.NodeType);
            Assert.AreEqual(@"/div", dochtml1body1div0Comment0.TextContent);

            var dochtml1body1div0Text1 = dochtml1body1div0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1div0Text1.NodeType);
            Assert.AreEqual("a", dochtml1body1div0Text1.TextContent);
        }

        [Test]
        public void TreeStyleWithCommentThatOpensAndClosesAStyleTagInside()
        {
            var doc = Html(@"<style><!--<style></style>--></style>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0style0.Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual("<!--<style>", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void TreeStyleWithOpeningCommentAndClosingStyleInside()
        {
            var doc = Html(@"<style><!--</style>X");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0style0.Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void TreeStyleWithClosingStyleInCommentInside()
        {
            var doc = Html(@"<style><!--...</style>...--></style>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0style0.Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual("<!--...", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("...-->", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void TreeStyleWithTagsInside()
        {
            var doc = Html(@"<style><!--<br><html xmlns:v=""urn:schemas-microsoft-com:vml""><!--[if !mso]><style></style>X");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0style0.Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual("<!--<br><html xmlns:v=\"urn:schemas-microsoft-com:vml\"><!--[if !mso]><style>", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void TreeStyleWithStyleCommentsInside()
        {
            var doc = Html(@"<style><!--...<style><!--...--!></style>--></style>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0style0.Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual("<!--...<style><!--...--!>", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void TreeStyleWithClosingStyleCommentsInside()
        {
            var doc = Html(@"<style><!--...</style><!-- --><style>@import ...</style>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0style0.Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual("<!--...", dochtml0head0style0Text0.TextContent);

            var dochtml0head0Comment1 = dochtml0head0.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0head0Comment1.NodeType);
            Assert.AreEqual(@" ", dochtml0head0Comment1.TextContent);

            var dochtml0head0style2 = dochtml0head0.ChildNodes[2] as Element; ;
            Assert.AreEqual(1, dochtml0head0style2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0style2.Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style2.NodeType);

            var dochtml0head0style2Text0 = dochtml0head0style2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style2Text0.NodeType);
            Assert.AreEqual("@import ...", dochtml0head0style2Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TreeStyleWithNestedStyleInside()
        {
            var doc = Html(@"<style>...<style><!--...</style><!-- --></style>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0style0.Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual("...<style><!--...", dochtml0head0style0Text0.TextContent);

            var dochtml0head0Comment1 = dochtml0head0.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0head0Comment1.NodeType);
            Assert.AreEqual(@" ", dochtml0head0Comment1.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TreeStyleWithIEConditionalCommentsInside()
        {
            var doc = Html(@"<style>...<!--[if IE]><style>...</style>X");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0style0.Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0style0Text0 = dochtml0head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0style0Text0.NodeType);
            Assert.AreEqual("...<!--[if IE]><style>...", dochtml0head0style0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void TreeTitleWithTitleCommentInside()
        {
            var doc = Html(@"<title><!--<title></title>--></title>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0title0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0title0.Attributes.Count);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0head0title0Text0 = dochtml0head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title0Text0.NodeType);
            Assert.AreEqual("<!--<title>", dochtml0head0title0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void TreeTitleWronglyAndCorrectlyClosed()
        {
            var doc = Html(@"<title>&lt;/title></title>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0title0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0title0.Attributes.Count);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0head0title0Text0 = dochtml0head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title0Text0.NodeType);
            Assert.AreEqual("</title>", dochtml0head0title0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TreeTitleWronglyClosedWithLinkAndClosingHead()
        {
            var doc = Html(@"<title>foo/title><link></head><body>X");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0title0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0title0.Attributes.Count);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0head0title0Text0 = dochtml0head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title0Text0.NodeType);
            Assert.AreEqual("foo/title><link></head><body>X", dochtml0head0title0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TreeNoScriptWithNoScriptCommentInside()
        {
            var source = @"<noscript><!--<noscript></noscript>--></noscript>";
            var config = Configuration.Default.With(new EnableScripting());
            var parser = new HtmlParser(source, config);
            var doc = parser.Parse();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0noscript0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0noscript0.Attributes.Count);
            Assert.AreEqual("noscript", dochtml0head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0noscript0.NodeType);

            var dochtml0head0noscript0Text0 = dochtml0head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0noscript0Text0.NodeType);
            Assert.AreEqual("<!--<noscript>", dochtml0head0noscript0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void TreeNoScriptWithCommentAndClosingNoScriptInside()
        {
            var source = @"<noscript><!--</noscript>X<noscript>--></noscript>";
            var config = Configuration.Default.With(new EnableScripting());
            var parser = new HtmlParser(source, config);
            var doc = parser.Parse();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0noscript0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0noscript0.Attributes.Count);
            Assert.AreEqual("noscript", dochtml0head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0noscript0.NodeType);

            var dochtml0head0noscript0Text0 = dochtml0head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0noscript0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml0head0noscript0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1Text0.TextContent);

            var dochtml0body1noscript1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1noscript1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1noscript1.Attributes.Count);
            Assert.AreEqual("noscript", dochtml0body1noscript1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1noscript1.NodeType);

            var dochtml0body1noscript1Text0 = dochtml0body1noscript1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1noscript1Text0.NodeType);
            Assert.AreEqual("-->", dochtml0body1noscript1Text0.TextContent);
        }

        [Test]
        public void TreeNoScriptWithIFrameInside()
        {
            var source = @"<noscript><iframe></noscript>X";
            var config = Configuration.Default.With(new EnableScripting());
            var parser = new HtmlParser(source, config);
            var doc = parser.Parse();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0noscript0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0noscript0.Attributes.Count);
            Assert.AreEqual("noscript", dochtml0head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0noscript0.NodeType);

            var dochtml0head0noscript0Text0 = dochtml0head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0noscript0Text0.NodeType);
            Assert.AreEqual("<iframe>", dochtml0head0noscript0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void TreeNoFramesWithNoFramesComment()
        {
            var doc = Html(@"<noframes><!--<noframes></noframes>--></noframes>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0noframes0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0noframes0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0noframes0.Attributes.Count);
            Assert.AreEqual("noframes", dochtml0head0noframes0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0noframes0.NodeType);

            var dochtml0head0noframes0Text0 = dochtml0head0noframes0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0noframes0Text0.NodeType);
            Assert.AreEqual("<!--<noframes>", dochtml0head0noframes0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml0body1Text0.TextContent);
        }

        [Test]
        public void TreeNoframesWithBodyAndScriptWithComment()
        {
            var doc = Html(@"<noframes><body><script><!--...</script></body></noframes></html>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0noframes0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0noframes0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0noframes0.Attributes.Count);
            Assert.AreEqual("noframes", dochtml0head0noframes0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0noframes0.NodeType);

            var dochtml0head0noframes0Text0 = dochtml0head0noframes0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0noframes0Text0.NodeType);
            Assert.AreEqual("<body><script><!--...</script></body>", dochtml0head0noframes0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void TreeTextareaWithIllegalComment()
        {
            var doc = Html(@"<textarea><!--<textarea></textarea>--></textarea>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1textarea0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1textarea0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1textarea0.Attributes.Count);
            Assert.AreEqual("textarea", dochtml0body1textarea0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1textarea0.NodeType);

            var dochtml0body1textarea0Text0 = dochtml0body1textarea0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1textarea0Text0.NodeType);
            Assert.AreEqual("<!--<textarea>", dochtml0body1textarea0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("-->", dochtml0body1Text1.TextContent);
        }

        [Test]
        public void TreeTextareaWithLegalComment()
        {
            var doc = Html(@"<textarea>&lt;/textarea></textarea>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1textarea0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1textarea0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1textarea0.Attributes.Count);
            Assert.AreEqual("textarea", dochtml0body1textarea0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1textarea0.NodeType);

            var dochtml0body1textarea0Text0 = dochtml0body1textarea0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1textarea0Text0.NodeType);
            Assert.AreEqual("</textarea>", dochtml0body1textarea0Text0.TextContent);
        }

        [Test]
        public void TreeIFrameWithTextAndIFrameComment()
        {
            var doc = Html(@"<iframe><!--<iframe></iframe>--></iframe>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1iframe0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1iframe0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1iframe0.Attributes.Count);
            Assert.AreEqual("iframe", dochtml0body1iframe0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1iframe0.NodeType);

            var dochtml0body1iframe0Text0 = dochtml0body1iframe0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1iframe0Text0.NodeType);
            Assert.AreEqual("<!--<iframe>", dochtml0body1iframe0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("-->", dochtml0body1Text1.TextContent);
        }

        [Test]
        public void TreeIFrameWithTextAndXComment()
        {
            var doc = Html(@"<iframe>...<!--X->...<!--/X->...</iframe>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1iframe0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1iframe0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1iframe0.Attributes.Count);
            Assert.AreEqual("iframe", dochtml0body1iframe0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1iframe0.NodeType);

            var dochtml0body1iframe0Text0 = dochtml0body1iframe0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1iframe0Text0.NodeType);
            Assert.AreEqual("...<!--X->...<!--/X->...", dochtml0body1iframe0Text0.TextContent);
        }

        [Test]
        public void treeXmpWithComment()
        {
            var doc = Html(@"<xmp><!--<xmp></xmp>--></xmp>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1xmp0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1xmp0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1xmp0.Attributes.Count);
            Assert.AreEqual("xmp", dochtml0body1xmp0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1xmp0.NodeType);

            var dochtml0body1xmp0Text0 = dochtml0body1xmp0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1xmp0Text0.NodeType);
            Assert.AreEqual("<!--<xmp>", dochtml0body1xmp0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("-->", dochtml0body1Text1.TextContent);
        }

        [Test]
        public void TreeNoEmbedWithComments()
        {
            var doc = Html(@"<noembed><!--<noembed></noembed>--></noembed>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1noembed0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1noembed0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1noembed0.Attributes.Count);
            Assert.AreEqual("noembed", dochtml0body1noembed0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1noembed0.NodeType);

            var dochtml0body1noembed0Text0 = dochtml0body1noembed0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1noembed0Text0.NodeType);
            Assert.AreEqual("<!--<noembed>", dochtml0body1noembed0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("-->", dochtml0body1Text1.TextContent);
        }

        [Test]
        public void TreeTableWithNewLine()
        {
            var doc = Html(@"<!doctype html><table>
");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0Text0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0Text0.NodeType);
            Assert.AreEqual("\n", dochtml1body1table0Text0.TextContent);
        }

        [Test]
        public void TreeSpanAndFontInMisnestedTableCell()
        {
            var doc = Html(@"<!doctype html><table><td><span><font></span><span>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0span0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0span0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0span0.Attributes.Count);
            Assert.AreEqual("span", dochtml1body1table0tbody0tr0td0span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0span0.NodeType);

            var dochtml1body1table0tbody0tr0td0span0font0 = dochtml1body1table0tbody0tr0td0span0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0span0font0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0span0font0.Attributes.Count);
            Assert.AreEqual("font", dochtml1body1table0tbody0tr0td0span0font0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0span0font0.NodeType);

            var dochtml1body1table0tbody0tr0td0font1 = dochtml1body1table0tbody0tr0td0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0font1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0font1.Attributes.Count);
            Assert.AreEqual("font", dochtml1body1table0tbody0tr0td0font1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0font1.NodeType);

            var dochtml1body1table0tbody0tr0td0font1span0 = dochtml1body1table0tbody0tr0td0font1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0font1span0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0font1span0.Attributes.Count);
            Assert.AreEqual("span", dochtml1body1table0tbody0tr0td0font1span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0font1span0.NodeType);
        }

        [Test]
        public void TreeTableInFormDoubleMisnested()
        {
            var doc = Html(@"<!doctype html><form><table></form><form></table></form>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1form0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1form0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1form0.Attributes.Count);
            Assert.AreEqual("form", dochtml1body1form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0.NodeType);

            var dochtml1body1form0table0 = dochtml1body1form0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1form0table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1form0table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1form0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0table0.NodeType);

            var dochtml1body1form0table0form0 = dochtml1body1form0table0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1form0table0form0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1form0table0form0.Attributes.Count);
            Assert.AreEqual("form", dochtml1body1form0table0form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0table0form0.NodeType);
        }

        [Test]
        public void PlaceStyleAfterHead()
        {
            var doc = Html(@"<head></head><style></style>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void PlaceScriptAfterHead()
        {
            var doc = Html(@"<head></head><script></script>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0script0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0script0).Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void PlaceCommentAfterHeadAndStyleAfterHeadAndScriptAfterHead()
        {
            var doc = Html(@"<head></head><!-- --><style></style><!-- --><script></script>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(4, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0style0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0style0).Attributes.Count);
            Assert.AreEqual("style", dochtml0head0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0style0.NodeType);

            var dochtml0head0script1 = dochtml0head0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0head0script1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0script1).Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script1.NodeType);

            var dochtml0Comment1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0Comment1.NodeType);
            Assert.AreEqual(@" ", dochtml0Comment1.TextContent);

            var dochtml0Comment2 = dochtml0.ChildNodes[2];
            Assert.AreEqual(NodeType.Comment, dochtml0Comment2.NodeType);
            Assert.AreEqual(@" ", dochtml0Comment2.TextContent);

            var dochtml0body3 = dochtml0.ChildNodes[3];
            Assert.AreEqual(0, dochtml0body3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body3).Attributes.Count);
            Assert.AreEqual("body", dochtml0body3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body3.NodeType);

        }

        [Test]
        public void PlaceCommentAndTextAfterHeadAndStyleAndScriptAfterHead()
        {
            var doc = Html(@"<head></head><!-- -->x<style></style><!-- --><script></script>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(3, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0Comment1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0Comment1.NodeType);
            Assert.AreEqual(@" ", dochtml0Comment1.TextContent);

            var dochtml0body2 = dochtml0.ChildNodes[2];
            Assert.AreEqual(4, dochtml0body2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body2).Attributes.Count);
            Assert.AreEqual("body", dochtml0body2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body2.NodeType);

            var dochtml0body2Text0 = dochtml0body2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body2Text0.NodeType);
            Assert.AreEqual("x", dochtml0body2Text0.TextContent);

            var dochtml0body2style1 = dochtml0body2.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body2style1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body2style1).Attributes.Count);
            Assert.AreEqual("style", dochtml0body2style1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body2style1.NodeType);

            var dochtml0body2Comment2 = dochtml0body2.ChildNodes[2];
            Assert.AreEqual(NodeType.Comment, dochtml0body2Comment2.NodeType);
            Assert.AreEqual(@" ", dochtml0body2Comment2.TextContent);

            var dochtml0body2script3 = dochtml0body2.ChildNodes[3];
            Assert.AreEqual(0, dochtml0body2script3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body2script3).Attributes.Count);
            Assert.AreEqual("script", dochtml0body2script3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body2script3.NodeType);
        }

        [Test]
        public void SkipInitialNewlineInPreElement()
        {
            var doc = Html(@"<!DOCTYPE html><html><head></head><body><pre>
</pre></body></html>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1pre0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1pre0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1pre0).Attributes.Count);
            Assert.AreEqual("pre", dochtml1body1pre0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1pre0.NodeType);
        }

        [Test]
        public void SkipInitialNewLineInPreElementWithText()
        {
            var doc = Html(@"<!DOCTYPE html><html><head></head><body><pre>
foo</pre></body></html>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1pre0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1pre0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1pre0).Attributes.Count);
            Assert.AreEqual("pre", dochtml1body1pre0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1pre0.NodeType);

            var dochtml1body1pre0Text0 = dochtml1body1pre0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1pre0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1pre0Text0.TextContent);
        }

        [Test]
        public void SkipInitialNewLineInPreElementWithTextThatStartsWithANewLine()
        {
            var doc = Html(@"<!DOCTYPE html><html><head></head><body><pre>

foo</pre></body></html>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1pre0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1pre0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1pre0).Attributes.Count);
            Assert.AreEqual("pre", dochtml1body1pre0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1pre0.NodeType);

            var dochtml1body1pre0Text0 = dochtml1body1pre0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1pre0Text0.NodeType);
            Assert.AreEqual("\nfoo", dochtml1body1pre0Text0.TextContent);
        }

        [Test]
        public void SkipInitialNewLineInPreElementWithTextThatEndsWithANewLine()
        {
            var doc = Html(@"<!DOCTYPE html><html><head></head><body><pre>
foo
</pre></body></html>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1pre0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1pre0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1pre0).Attributes.Count);
            Assert.AreEqual("pre", dochtml1body1pre0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1pre0.NodeType);

            var dochtml1body1pre0Text0 = dochtml1body1pre0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1pre0Text0.NodeType);
            Assert.AreEqual("foo\n", dochtml1body1pre0Text0.TextContent);
        }

        [Test]
        public void DontSkipAnythingInPreElementWithText()
        {
            var doc = Html(@"<!DOCTYPE html><html><head></head><body><pre>x</pre><span>
</span></body></html>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1pre0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1pre0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1pre0).Attributes.Count);
            Assert.AreEqual("pre", dochtml1body1pre0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1pre0.NodeType);

            var dochtml1body1pre0Text0 = dochtml1body1pre0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1pre0Text0.NodeType);
            Assert.AreEqual("x", dochtml1body1pre0Text0.TextContent);

            var dochtml1body1span1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1span1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1span1).Attributes.Count);
            Assert.AreEqual("span", dochtml1body1span1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1span1.NodeType);

            var dochtml1body1span1Text0 = dochtml1body1span1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1span1Text0.NodeType);
            Assert.AreEqual("\n", dochtml1body1span1Text0.TextContent);
        }

        [Test]
        public void DontSkipAnythingInPreElementWithTextThatContainsALineBreak()
        {
            var doc = Html(@"<!DOCTYPE html><html><head></head><body><pre>x
y</pre></body></html>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1pre0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1pre0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1pre0).Attributes.Count);
            Assert.AreEqual("pre", dochtml1body1pre0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1pre0.NodeType);

            var dochtml1body1pre0Text0 = dochtml1body1pre0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1pre0Text0.NodeType);
            Assert.AreEqual("x\ny", dochtml1body1pre0Text0.TextContent);
        }

        [Test]
        public void ClosePreElementWhenDivElementIsOpenedInside()
        {
            var doc = Html(@"<!DOCTYPE html><html><head></head><body><pre>x<div>
y</pre></body></html>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1pre0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1pre0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1pre0).Attributes.Count);
            Assert.AreEqual("pre", dochtml1body1pre0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1pre0.NodeType);

            var dochtml1body1pre0Text0 = dochtml1body1pre0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1pre0Text0.NodeType);
            Assert.AreEqual("x", dochtml1body1pre0Text0.TextContent);

            var dochtml1body1pre0div1 = dochtml1body1pre0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1pre0div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1pre0div1).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1pre0div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1pre0div1.NodeType);

            var dochtml1body1pre0div1Text0 = dochtml1body1pre0div1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1pre0div1Text0.NodeType);
            Assert.AreEqual("\ny", dochtml1body1pre0div1Text0.TextContent);
        }

        [Test]
        public void DoNotSkipFirstLineInPreElementWhenGeneratedViaEntities()
        {
            var doc = Html(@"<!DOCTYPE html><pre>&#x0a;&#x0a;A</pre>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1pre0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1pre0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1pre0).Attributes.Count);
            Assert.AreEqual("pre", dochtml1body1pre0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1pre0.NodeType);

            var dochtml1body1pre0Text0 = dochtml1body1pre0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1pre0Text0.NodeType);
            Assert.AreEqual("\nA", dochtml1body1pre0Text0.TextContent);
        }

        [Test]
        public void ConvertUpperCaseTagsToLowerCaseTags()
        {
            var doc = Html(@"<!DOCTYPE html><HTML><META><HEAD></HEAD></HTML>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0meta0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0meta0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0meta0).Attributes.Count);
            Assert.AreEqual("meta", dochtml1head0meta0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0meta0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void ConvertMixedCaseTagsToLowerCaseTags()
        {
            var doc = Html(@"<!DOCTYPE html><HTML><HEAD><head></HEAD></HTML>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void SwitchToRawtextModeInTextareaElement()
        {
            var doc = Html(@"<textarea>foo<span>bar</span><i>baz");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1textarea0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1textarea0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1textarea0).Attributes.Count);
            Assert.AreEqual("textarea", dochtml0body1textarea0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1textarea0.NodeType);

            var dochtml0body1textarea0Text0 = dochtml0body1textarea0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1textarea0Text0.NodeType);
            Assert.AreEqual("foo<span>bar</span><i>baz", dochtml0body1textarea0Text0.TextContent);
        }

        [Test]
        public void SwitchToRawtextModeInTitleElement()
        {
            var doc = Html(@"<title>foo<span>bar</em><i>baz");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0head0title0 = dochtml0head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0head0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0title0).Attributes.Count);
            Assert.AreEqual("title", dochtml0head0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0title0.NodeType);

            var dochtml0head0title0Text0 = dochtml0head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0title0Text0.NodeType);
            Assert.AreEqual("foo<span>bar</em><i>baz", dochtml0head0title0Text0.TextContent);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void IgnoreInitialLineInTextareaElement()
        {
            var doc = Html(@"<!DOCTYPE html><textarea>
</textarea>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1textarea0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1textarea0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1textarea0).Attributes.Count);
            Assert.AreEqual("textarea", dochtml1body1textarea0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1textarea0.NodeType);
        }

        [Test]
        public void IgnoreInitialLineInTextareaElementWithText()
        {
            var doc = Html(@"<!DOCTYPE html><textarea>
foo</textarea>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1textarea0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1textarea0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1textarea0).Attributes.Count);
            Assert.AreEqual("textarea", dochtml1body1textarea0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1textarea0.NodeType);

            var dochtml1body1textarea0Text0 = dochtml1body1textarea0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1textarea0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1textarea0Text0.TextContent);
        }

        [Test]
        public void IgnoreInitialLineInTextareaElementWithNewLineAndText()
        {
            var doc = Html(@"<!DOCTYPE html><textarea>

foo</textarea>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1textarea0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1textarea0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1textarea0).Attributes.Count);
            Assert.AreEqual("textarea", dochtml1body1textarea0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1textarea0.NodeType);

            var dochtml1body1textarea0Text0 = dochtml1body1textarea0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1textarea0Text0.NodeType);
            Assert.AreEqual("\nfoo", dochtml1body1textarea0Text0.TextContent);
        }

        [Test]
        public void GeneratedImpliedEndTagsForListItemsAndParagraphs()
        {
            var doc = Html(@"<!DOCTYPE html><html><head></head><body><ul><li><div><p><li></ul></body></html>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1ul0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1ul0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ul0).Attributes.Count);
            Assert.AreEqual("ul", dochtml1body1ul0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ul0.NodeType);

            var dochtml1body1ul0li0 = dochtml1body1ul0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1ul0li0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ul0li0).Attributes.Count);
            Assert.AreEqual("li", dochtml1body1ul0li0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ul0li0.NodeType);

            var dochtml1body1ul0li0div0 = dochtml1body1ul0li0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1ul0li0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ul0li0div0).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1ul0li0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ul0li0div0.NodeType);

            var dochtml1body1ul0li0div0p0 = dochtml1body1ul0li0div0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1ul0li0div0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ul0li0div0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1ul0li0div0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ul0li0div0p0.NodeType);

            var dochtml1body1ul0li1 = dochtml1body1ul0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1ul0li1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ul0li1).Attributes.Count);
            Assert.AreEqual("li", dochtml1body1ul0li1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ul0li1.NodeType);
        }

        [Test]
        public void UseSelfClosingElementWithoutClosingSlash()
        {
            var doc = Html(@"<!doctype html><nobr><nobr><nobr>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1nobr0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr0.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1nobr2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr2).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr2.NodeType);
        }

        [Test]
        public void WronglyUseClosingTagForSelfClosingElement()
        {
            var doc = Html(@"<!doctype html><nobr><nobr></nobr><nobr>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1nobr0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1nobr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr0).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr0.NodeType);

            var dochtml1body1nobr1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1nobr1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr1).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr1.NodeType);

            var dochtml1body1nobr2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1nobr2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1nobr2).Attributes.Count);
            Assert.AreEqual("nobr", dochtml1body1nobr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1nobr2.NodeType);
        }

        [Test]
        public void GenerateImpliedEndTagForParagraphElementWithTableNoQuirksmode()
        {
            var doc = Html(@"<!doctype html><html><body><p><table></table></body></html>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void DoNotGenerateImpliedEndTagForParagraphElementWithTableLimitedQuirksmode()
        {
            var doc = Html(@"<p><table></table>");

            var dochtml0 = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0).Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0head0).Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0table0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0table0).Attributes.Count);
            Assert.AreEqual("table", dochtml0body1p0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0table0.NodeType);
        }
    }
}
