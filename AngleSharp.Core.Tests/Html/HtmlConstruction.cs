using System;
using AngleSharp.Dom;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests15.dat,
    /// tree-construction/tests18.dat,
    /// tree-construction/tests19.dat,
    /// tree-construction/tests20.dat
    /// </summary>
    [TestFixture]
    public class HtmlConstructionTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void ParagraphWithNewFormattingElements()
        {
            var doc = Html(@"<!DOCTYPE html><p><b><i><u></p> <p>X");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());

            var dochtml1body1p0b0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1p0b0.GetTagName());

            var dochtml1body1p0b0i0 = dochtml1body1p0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0b0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0b0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1p0b0i0.GetTagName());

            var dochtml1body1p0b0i0u0 = dochtml1body1p0b0i0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0b0i0u0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0b0i0u0).Attributes.Count);
            Assert.AreEqual("u", dochtml1body1p0b0i0u0.GetTagName());

            var dochtml1body1b1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b1.GetTagName());

            var dochtml1body1b1i0 = dochtml1body1b1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1b1i0.GetTagName());

            var dochtml1body1b1i0u0 = dochtml1body1b1i0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1b1i0u0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b1i0u0).Attributes.Count);
            Assert.AreEqual("u", dochtml1body1b1i0u0.GetTagName());

            var dochtml1body1b1i0u0Text0 = dochtml1body1b1i0u0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b1i0u0Text0.NodeType);
            Assert.AreEqual(" ", dochtml1body1b1i0u0Text0.TextContent);

            var dochtml1body1b1i0u0p1 = dochtml1body1b1i0u0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1b1i0u0p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b1i0u0p1).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1b1i0u0p1.GetTagName());

            var dochtml1body1b1i0u0p1Text0 = dochtml1body1b1i0u0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b1i0u0p1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1b1i0u0p1Text0.TextContent);
        }

        [Test]
        public void NewLineAfterParagraphWithNewFormattingElements()
        {
            var doc = Html(@"<p><b><i><u></p>
<p>X");

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1p0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0b0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0b0).Attributes.Count);
            Assert.AreEqual("b", dochtml0body1p0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0.NodeType);

            var dochtml0body1p0b0i0 = dochtml0body1p0b0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1p0b0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0b0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml0body1p0b0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0i0.NodeType);

            var dochtml0body1p0b0i0u0 = dochtml0body1p0b0i0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0b0i0u0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0b0i0u0).Attributes.Count);
            Assert.AreEqual("u", dochtml0body1p0b0i0u0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0b0i0u0.NodeType);

            var dochtml0body1b1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b1).Attributes.Count);
            Assert.AreEqual("b", dochtml0body1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1.NodeType);

            var dochtml0body1b1i0 = dochtml0body1b1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1b1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml0body1b1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1i0.NodeType);

            var dochtml0body1b1i0u0 = dochtml0body1b1i0.ChildNodes[0];
            Assert.AreEqual(2, dochtml0body1b1i0u0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b1i0u0).Attributes.Count);
            Assert.AreEqual("u", dochtml0body1b1i0u0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1i0u0.NodeType);

            var dochtml0body1b1i0u0Text0 = dochtml0body1b1i0u0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b1i0u0Text0.NodeType);
            Assert.AreEqual("\n", dochtml0body1b1i0u0Text0.TextContent);

            var dochtml0body1b1i0u0p1 = dochtml0body1b1i0u0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1b1i0u0p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1b1i0u0p1).Attributes.Count);
            Assert.AreEqual("p", dochtml0body1b1i0u0p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1b1i0u0p1.NodeType);

            var dochtml0body1b1i0u0p1Text0 = dochtml0body1b1i0u0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1b1i0u0p1Text0.NodeType);
            Assert.AreEqual("X", dochtml0body1b1i0u0p1Text0.TextContent);
        }

        [Test]
        public void WronglyClosedHtmlTagAndSpaceBeforeHead()
        {
            var doc = Html(@"<!doctype html></html> <head>");

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

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual(" ", dochtml1body1Text0.TextContent);
        }

        [Test]
        public void WronglyClosedBodyTagAndStandaloneMetaElement()
        {
            var doc = Html(@"<!doctype html></body><meta>");

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

            var dochtml1body1meta0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1meta0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1meta0).Attributes.Count);
            Assert.AreEqual("meta", dochtml1body1meta0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1meta0.NodeType);
        }

        [Test]
        public void CommentAfterClosedHtmlTag()
        {
            var doc = Html(@"<html></html><!-- foo -->");

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
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);


            var docComment1 = doc.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, docComment1.NodeType);
            Assert.AreEqual(@" foo ", docComment1.TextContent);
        }

        [Test]
        public void WronglyClosedBodyElementAndTitleElement()
        {
            var doc = Html(@"<!doctype html></body><title>X</title>");

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

            var dochtml1body1title0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1title0).Attributes.Count);
            Assert.AreEqual("title", dochtml1body1title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1title0.NodeType);

            var dochtml1body1title0Text0 = dochtml1body1title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1title0Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1title0Text0.TextContent);
        }

        [Test]
        public void TableWithMissingBracketAndWronglyClosedMetaElement()
        {
            var doc = Html(@"<!doctype html><table> X<meta></table>");

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

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual(" X", dochtml1body1Text0.TextContent);

            var dochtml1body1meta1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1meta1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1meta1).Attributes.Count);
            Assert.AreEqual("meta", dochtml1body1meta1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1meta1.NodeType);

            var dochtml1body1table2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1table2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table2).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table2.NodeType);
        }

        [Test]
        public void FosterParentedTextInTableElement()
        {
            var doc = Html(@"<!doctype html><table> x</table>");

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

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual(" x", dochtml1body1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void FosterParentedTextWithTrailingSpaceInTableElement()
        {
            var doc = Html(@"<!doctype html><table> x </table>");

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

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual(" x ", dochtml1body1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void FosterParentedTextInRowElement()
        {
            var doc = Html(@"<!doctype html><table><tr> x</table>");

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

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual(" x", dochtml1body1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);
        }

        [Test]
        public void WronglyPlacedStyleElementInTableElementWithFosteredText()
        {
            var doc = Html(@"<!doctype html><table>X<style> <tr>x </style> </table>");

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

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1style0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1style0).Attributes.Count);
            Assert.AreEqual("style", dochtml1body1table1style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1style0.NodeType);

            var dochtml1body1table1style0Text0 = dochtml1body1table1style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table1style0Text0.NodeType);
            Assert.AreEqual(" <tr>x ", dochtml1body1table1style0Text0.TextContent);

            var dochtml1body1table1Text1 = dochtml1body1table1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1table1Text1.NodeType);
            Assert.AreEqual(" ", dochtml1body1table1Text1.TextContent);
        }

        [Test]
        public void TableInDivWithMultipleFosteredElements()
        {
            var doc = Html(@"<!doctype html><div><table><a>foo</a> <tr><td>bar</td> </tr></table></div>");

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

            var dochtml1body1div0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0.NodeType);

            var dochtml1body1div0a0 = dochtml1body1div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1div0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0a0).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1div0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0a0.NodeType);

            var dochtml1body1div0a0Text0 = dochtml1body1div0a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div0a0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1div0a0Text0.TextContent);

            var dochtml1body1div0table1 = dochtml1body1div0.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1div0table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1div0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0table1.NodeType);

            var dochtml1body1div0table1Text0 = dochtml1body1div0table1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div0table1Text0.NodeType);
            Assert.AreEqual(" ", dochtml1body1div0table1Text0.TextContent);

            var dochtml1body1div0table1tbody1 = dochtml1body1div0table1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1div0table1tbody1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0table1tbody1).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1div0table1tbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0table1tbody1.NodeType);

            var dochtml1body1div0table1tbody1tr0 = dochtml1body1div0table1tbody1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div0table1tbody1tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0table1tbody1tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1div0table1tbody1tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0table1tbody1tr0.NodeType);

            var dochtml1body1div0table1tbody1tr0td0 = dochtml1body1div0table1tbody1tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1div0table1tbody1tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0table1tbody1tr0td0).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1div0table1tbody1tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0table1tbody1tr0td0.NodeType);

            var dochtml1body1div0table1tbody1tr0td0Text0 = dochtml1body1div0table1tbody1tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div0table1tbody1tr0td0Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1div0table1tbody1tr0td0Text0.TextContent);

            var dochtml1body1div0table1tbody1tr0Text1 = dochtml1body1div0table1tbody1tr0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1div0table1tbody1tr0Text1.NodeType);
            Assert.AreEqual(" ", dochtml1body1div0table1tbody1tr0Text1.TextContent);
        }

        [Test]
        public void FrameAndFramesetUsedPartiallyWrong()
        {
            var doc = Html(@"<frame></frame></frame><frameset><frame><frameset><frame></frameset><noframes></frameset><noframes>");

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

            var dochtml0frameset1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(3, dochtml0frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml0frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1.NodeType);

            var dochtml0frameset1frame0 = dochtml0frameset1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0frameset1frame0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0frameset1frame0).Attributes.Count);
            Assert.AreEqual("frame", dochtml0frameset1frame0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1frame0.NodeType);

            var dochtml0frameset1frameset1 = dochtml0frameset1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0frameset1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0frameset1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml0frameset1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1frameset1.NodeType);

            var dochtml0frameset1frameset1frame0 = dochtml0frameset1frameset1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0frameset1frameset1frame0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0frameset1frameset1frame0).Attributes.Count);
            Assert.AreEqual("frame", dochtml0frameset1frameset1frame0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1frameset1frame0.NodeType);

            var dochtml0frameset1noframes2 = dochtml0frameset1.ChildNodes[2];
            Assert.AreEqual(1, dochtml0frameset1noframes2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0frameset1noframes2).Attributes.Count);
            Assert.AreEqual("noframes", dochtml0frameset1noframes2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1noframes2.NodeType);

            var dochtml0frameset1noframes2Text0 = dochtml0frameset1noframes2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0frameset1noframes2Text0.NodeType);
            Assert.AreEqual("</frameset><noframes>", dochtml0frameset1noframes2Text0.TextContent);
        }

        [Test]
        public void ObjectElementClosedByHtml()
        {
            var doc = Html(@"<!DOCTYPE html><object></html>");

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

            var dochtml1body1object0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1object0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1object0).Attributes.Count);
            Assert.AreEqual("object", dochtml1body1object0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1object0.NodeType);
        }

        [Test]
        public void PlaintextElementCannotBeClosedAnymore()
        {
            var doc = Html(@"<!doctype html><plaintext></plaintext>");

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

            var dochtml1body1plaintext0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1plaintext0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1plaintext0).Attributes.Count);
            Assert.AreEqual("plaintext", dochtml1body1plaintext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1plaintext0.NodeType);

            var dochtml1body1plaintext0Text0 = dochtml1body1plaintext0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1plaintext0Text0.NodeType);
            Assert.AreEqual("</plaintext>", dochtml1body1plaintext0Text0.TextContent);
        }

        [Test]
        public void FosteredPlaintextElementInTableThatCannotBeClosedAnymore()
        {
            var doc = Html(@"<!doctype html><table><plaintext></plaintext>");

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

            var dochtml1body1plaintext0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1plaintext0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1plaintext0).Attributes.Count);
            Assert.AreEqual("plaintext", dochtml1body1plaintext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1plaintext0.NodeType);

            var dochtml1body1plaintext0Text0 = dochtml1body1plaintext0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1plaintext0Text0.NodeType);
            Assert.AreEqual("</plaintext>", dochtml1body1plaintext0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void FosteredPlaintextElementInTBodyThatCannotBeClosedAnymore()
        {
            var doc = Html(@"<!doctype html><table><tbody><plaintext></plaintext>");

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

            var dochtml1body1plaintext0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1plaintext0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1plaintext0).Attributes.Count);
            Assert.AreEqual("plaintext", dochtml1body1plaintext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1plaintext0.NodeType);

            var dochtml1body1plaintext0Text0 = dochtml1body1plaintext0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1plaintext0Text0.NodeType);
            Assert.AreEqual("</plaintext>", dochtml1body1plaintext0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);
        }

        [Test]
        public void FosteredPlaintextElementInRowThatCannotBeClosedAnymore()
        {
            var doc = Html(@"<!doctype html><table><tbody><tr><plaintext></plaintext>");

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

            var dochtml1body1plaintext0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1plaintext0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1plaintext0).Attributes.Count);
            Assert.AreEqual("plaintext", dochtml1body1plaintext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1plaintext0.NodeType);

            var dochtml1body1plaintext0Text0 = dochtml1body1plaintext0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1plaintext0Text0.NodeType);
            Assert.AreEqual("</plaintext>", dochtml1body1plaintext0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);
        }

        [Test]
        public void PlaintextElementInTableCellThatCannotBeClosedAnymore()
        {
            var doc = Html(@"<!doctype html><table><td><plaintext></plaintext>");

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

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0plaintext0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0plaintext0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0plaintext0).Attributes.Count);
            Assert.AreEqual("plaintext", dochtml1body1table0tbody0tr0td0plaintext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0plaintext0.NodeType);

            var dochtml1body1table0tbody0tr0td0plaintext0Text0 = dochtml1body1table0tbody0tr0td0plaintext0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0plaintext0Text0.NodeType);
            Assert.AreEqual("</plaintext>", dochtml1body1table0tbody0tr0td0plaintext0Text0.TextContent);
        }

        [Test]
        public void PlaintextElementInTableCaptionThatCannotBeClosedAnymore()
        {
            var doc = Html(@"<!doctype html><table><caption><plaintext></plaintext>");

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

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0caption0).Attributes.Count);
            Assert.AreEqual("caption", dochtml1body1table0caption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0plaintext0 = dochtml1body1table0caption0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0caption0plaintext0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0caption0plaintext0).Attributes.Count);
            Assert.AreEqual("plaintext", dochtml1body1table0caption0plaintext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0plaintext0.NodeType);

            var dochtml1body1table0caption0plaintext0Text0 = dochtml1body1table0caption0plaintext0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0plaintext0Text0.NodeType);
            Assert.AreEqual("</plaintext>", dochtml1body1table0caption0plaintext0Text0.TextContent);
        }

        [Test]
        public void StyleTagInRowSwitchesToRawtextAndAbsorbsClosedScriptTag()
        {
            var doc = Html(@"<!doctype html><table><tr><style></script></style>abc");

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

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("abc", dochtml1body1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);

            var dochtml1body1table1tbody0tr0style0 = dochtml1body1table1tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0tr0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0tr0style0).Attributes.Count);
            Assert.AreEqual("style", dochtml1body1table1tbody0tr0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0style0.NodeType);

            var dochtml1body1table1tbody0tr0style0Text0 = dochtml1body1table1tbody0tr0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table1tbody0tr0style0Text0.NodeType);
            Assert.AreEqual("</script>", dochtml1body1table1tbody0tr0style0Text0.TextContent);
        }

        [Test]
        public void ScriptTagInRowSwitchesToRawtextAndAbsorbsClosedStyleTag()
        {
            var doc = Html(@"<!doctype html><table><tr><script></style></script>abc");

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

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("abc", dochtml1body1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);

            var dochtml1body1table1tbody0tr0script0 = dochtml1body1table1tbody0tr0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0tr0script0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0tr0script0).Attributes.Count);
            Assert.AreEqual("script", dochtml1body1table1tbody0tr0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0script0.NodeType);

            var dochtml1body1table1tbody0tr0script0Text0 = dochtml1body1table1tbody0tr0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table1tbody0tr0script0Text0.NodeType);
            Assert.AreEqual("</style>", dochtml1body1table1tbody0tr0script0Text0.TextContent);

        }

        [Test]
        public void StyleTagInCaptionSwitchesToRawtextAndAbsorbsClosedScriptTag()
        {
            var doc = Html(@"<!doctype html><table><caption><style></script></style>abc");

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

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0caption0).Attributes.Count);
            Assert.AreEqual("caption", dochtml1body1table0caption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0style0 = dochtml1body1table0caption0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0caption0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0caption0style0).Attributes.Count);
            Assert.AreEqual("style", dochtml1body1table0caption0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0style0.NodeType);

            var dochtml1body1table0caption0style0Text0 = dochtml1body1table0caption0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0style0Text0.NodeType);
            Assert.AreEqual("</script>", dochtml1body1table0caption0style0Text0.TextContent);

            var dochtml1body1table0caption0Text1 = dochtml1body1table0caption0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0Text1.NodeType);
            Assert.AreEqual("abc", dochtml1body1table0caption0Text1.TextContent);
        }

        [Test]
        public void StyleTagInCellSwitchesToRawtextAndAbsorbsClosedScriptTag()
        {
            var doc = Html(@"<!doctype html><table><td><style></script></style>abc");

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

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0style0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0style0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0style0).Attributes.Count);
            Assert.AreEqual("style", dochtml1body1table0tbody0tr0td0style0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0style0.NodeType);

            var dochtml1body1table0tbody0tr0td0style0Text0 = dochtml1body1table0tbody0tr0td0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0style0Text0.NodeType);
            Assert.AreEqual("</script>", dochtml1body1table0tbody0tr0td0style0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0Text1 = dochtml1body1table0tbody0tr0td0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0Text1.NodeType);
            Assert.AreEqual("abc", dochtml1body1table0tbody0tr0td0Text1.TextContent);
        }

        [Test]
        public void ScriptTagInSelectSwitchesToRawtextAndAbsorbsClosedStyleTag()
        {
            var doc = Html(@"<!doctype html><select><script></style></script>abc");

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

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0script0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1select0script0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0script0).Attributes.Count);
            Assert.AreEqual("script", dochtml1body1select0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0script0.NodeType);

            var dochtml1body1select0script0Text0 = dochtml1body1select0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1select0script0Text0.NodeType);
            Assert.AreEqual("</style>", dochtml1body1select0script0Text0.TextContent);

            var dochtml1body1select0Text1 = dochtml1body1select0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1select0Text1.NodeType);
            Assert.AreEqual("abc", dochtml1body1select0Text1.TextContent);
        }

        [Test]
        public void ScriptTagInSelectLocatedInTableSwitchesToRawtextAndAbsorbsClosedStyleTag()
        {
            var doc = Html(@"<!doctype html><table><select><script></style></script>abc");

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

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0script0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1select0script0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0script0).Attributes.Count);
            Assert.AreEqual("script", dochtml1body1select0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0script0.NodeType);

            var dochtml1body1select0script0Text0 = dochtml1body1select0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1select0script0Text0.NodeType);
            Assert.AreEqual("</style>", dochtml1body1select0script0Text0.TextContent);

            var dochtml1body1select0Text1 = dochtml1body1select0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1select0Text1.NodeType);
            Assert.AreEqual("abc", dochtml1body1select0Text1.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void ScriptTagInSelectLocatedInRowSwitchesToRawtextAndAbsorbsClosedStyleTag()
        {
            var doc = Html(@"<!doctype html><table><tr><select><script></style></script>abc");

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

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0script0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1select0script0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0script0).Attributes.Count);
            Assert.AreEqual("script", dochtml1body1select0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0script0.NodeType);

            var dochtml1body1select0script0Text0 = dochtml1body1select0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1select0script0Text0.NodeType);
            Assert.AreEqual("</style>", dochtml1body1select0script0Text0.TextContent);

            var dochtml1body1select0Text1 = dochtml1body1select0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1select0Text1.NodeType);
            Assert.AreEqual("abc", dochtml1body1select0Text1.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);
        }

        [Test]
        public void NoframesAfterFramesetElement()
        {
            var doc = Html(@"<!doctype html><frameset></frameset><noframes>abc");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(3, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

            var dochtml1noframes2 = dochtml1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1noframes2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1noframes2).Attributes.Count);
            Assert.AreEqual("noframes", dochtml1noframes2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1noframes2.NodeType);

            var dochtml1noframes2Text0 = dochtml1noframes2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1noframes2Text0.NodeType);
            Assert.AreEqual("abc", dochtml1noframes2Text0.TextContent);
        }

        [Test]
        public void NoframesAfterFramesetElementWithFollowingComment()
        {
            var doc = Html(@"<!doctype html><frameset></frameset><noframes>abc</noframes><!--abc-->");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(4, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

            var dochtml1noframes2 = dochtml1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1noframes2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1noframes2).Attributes.Count);
            Assert.AreEqual("noframes", dochtml1noframes2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1noframes2.NodeType);

            var dochtml1noframes2Text0 = dochtml1noframes2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1noframes2Text0.NodeType);
            Assert.AreEqual("abc", dochtml1noframes2Text0.TextContent);

            var dochtml1Comment3 = dochtml1.ChildNodes[3];
            Assert.AreEqual(NodeType.Comment, dochtml1Comment3.NodeType);
            Assert.AreEqual(@"abc", dochtml1Comment3.TextContent);
        }

        [Test]
        public void NoframesAfterHtmlElement()
        {
            var doc = Html(@"<!doctype html><frameset></frameset></html><noframes>abc");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(3, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

            var dochtml1noframes2 = dochtml1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1noframes2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1noframes2).Attributes.Count);
            Assert.AreEqual("noframes", dochtml1noframes2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1noframes2.NodeType);

            var dochtml1noframes2Text0 = dochtml1noframes2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1noframes2Text0.NodeType);
            Assert.AreEqual("abc", dochtml1noframes2Text0.TextContent);
        }

        [Test]
        public void TestMethod30()
        {
            var doc = Html(@"<!doctype html><frameset></frameset></html><noframes>abc</noframes><!--abc-->");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(3, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

            var dochtml1noframes2 = dochtml1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1noframes2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1noframes2).Attributes.Count);
            Assert.AreEqual("noframes", dochtml1noframes2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1noframes2.NodeType);

            var dochtml1noframes2Text0 = dochtml1noframes2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1noframes2Text0.NodeType);
            Assert.AreEqual("abc", dochtml1noframes2Text0.TextContent);


            var docComment2 = doc.ChildNodes[2];
            Assert.AreEqual(NodeType.Comment, docComment2.NodeType);
            Assert.AreEqual(@"abc", docComment2.TextContent);

        }

        [Test]
        public void TestMethod31()
        {
            var doc = Html(@"<!doctype html><table><tr></tbody><tfoot>");

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

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tfoot1 = dochtml1body1table0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table0tfoot1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tfoot1).Attributes.Count);
            Assert.AreEqual("tfoot", dochtml1body1table0tfoot1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tfoot1.NodeType);

        }

        [Test]
        public void TestMethod32()
        {
            var doc = Html(@"<!doctype html><table><td><svg></svg>abc<td>");

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

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1table0tbody0tr0td0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0.NodeType);

            var dochtml1body1table0tbody0tr0td0Text1 = dochtml1body1table0tbody0tr0td0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0Text1.NodeType);
            Assert.AreEqual("abc", dochtml1body1table0tbody0tr0td0Text1.TextContent);

            var dochtml1body1table0tbody0tr0td1 = dochtml1body1table0tbody0tr0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td1).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td1.NodeType);

        }

        [Test]
        public void TestMethod33()
        {
            var doc = Html(@"<!doctype html><math><mn DefinitionUrl=""foo"">");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math0).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mn0 = dochtml1body1math0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0mn0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1body1math0mn0).Attributes.Count);
            Assert.AreEqual("mn", dochtml1body1math0mn0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mn0.NodeType);

            Assert.IsNotNull(((Element)dochtml1body1math0mn0).GetAttribute("definitionURL"));
            Assert.AreEqual("foo", ((Element)dochtml1body1math0mn0).GetAttribute("definitionURL"));

        }

        [Test]
        public void TestMethod34()
        {
            var doc = Html(@"<!doctype html><html></p><!--foo-->");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(3, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1Comment0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml1Comment0.NodeType);
            Assert.AreEqual(@"foo", dochtml1Comment0.TextContent);

            var dochtml1head1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1head1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head1).Attributes.Count);
            Assert.AreEqual("head", dochtml1head1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head1.NodeType);

            var dochtml1body2 = dochtml1.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body2).Attributes.Count);
            Assert.AreEqual("body", dochtml1body2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body2.NodeType);

        }

        [Test]
        public void TestMethod35()
        {
            var doc = Html(@"<!doctype html><head></head></p><!--foo-->");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(3, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1Comment1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml1Comment1.NodeType);
            Assert.AreEqual(@"foo", dochtml1Comment1.TextContent);

            var dochtml1body2 = dochtml1.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body2).Attributes.Count);
            Assert.AreEqual("body", dochtml1body2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body2.NodeType);

        }

        [Test]
        public void TestMethod36()
        {
            var doc = Html(@"<!doctype html><body><p><pre>");

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

            var dochtml1body1pre1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1pre1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1pre1).Attributes.Count);
            Assert.AreEqual("pre", dochtml1body1pre1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1pre1.NodeType);

        }

        [Test]
        public void TestMethod37()
        {
            var doc = Html(@"<!doctype html><body><p><listing>");

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

            var dochtml1body1listing1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1listing1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1listing1).Attributes.Count);
            Assert.AreEqual("listing", dochtml1body1listing1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1listing1.NodeType);

        }

        [Test]
        public void TestMethod38()
        {
            var doc = Html(@"<!doctype html><p><plaintext>");

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

            var dochtml1body1plaintext1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1plaintext1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1plaintext1).Attributes.Count);
            Assert.AreEqual("plaintext", dochtml1body1plaintext1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1plaintext1.NodeType);

        }

        [Test]
        public void TestMethod39()
        {
            var doc = Html(@"<!doctype html><p><h1>");

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

            var dochtml1body1h11 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1h11.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1h11).Attributes.Count);
            Assert.AreEqual("h1", dochtml1body1h11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1h11.NodeType);

        }

        [Test]
        public void TestMethod40()
        {
            var doc = Html(@"<!doctype html><form><isindex>");

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

            var dochtml1body1form0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1form0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0).Attributes.Count);
            Assert.AreEqual("form", dochtml1body1form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0.NodeType);

        }

        [Test]
        public void TestMethod41()
        {
            var doc = Html(@"<!doctype html><isindex action=""POST"">");

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

            var dochtml1body1form0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml1body1form0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1body1form0).Attributes.Count);
            Assert.AreEqual("form", dochtml1body1form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0.NodeType);

            Assert.IsNotNull(((Element)dochtml1body1form0).GetAttribute("action"));
            Assert.AreEqual("POST", ((Element)dochtml1body1form0).GetAttribute("action"));

            var dochtml1body1form0hr0 = dochtml1body1form0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1form0hr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0hr0).Attributes.Count);
            Assert.AreEqual("hr", dochtml1body1form0hr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0hr0.NodeType);

            var dochtml1body1form0label1 = dochtml1body1form0.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1form0label1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0label1).Attributes.Count);
            Assert.AreEqual("label", dochtml1body1form0label1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0label1.NodeType);

            var dochtml1body1form0label1Text0 = dochtml1body1form0label1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1form0label1Text0.NodeType);
            Assert.AreEqual("This is a searchable index. Enter search keywords: ", dochtml1body1form0label1Text0.TextContent);

            var dochtml1body1form0label1input1 = dochtml1body1form0label1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1form0label1input1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1body1form0label1input1).Attributes.Count);
            Assert.AreEqual("input", dochtml1body1form0label1input1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0label1input1.NodeType);

            Assert.IsNotNull(((Element)dochtml1body1form0label1input1).GetAttribute("name"));
            Assert.AreEqual("isindex", ((Element)dochtml1body1form0label1input1).GetAttribute("name"));

            var dochtml1body1form0hr2 = dochtml1body1form0.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1form0hr2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0hr2).Attributes.Count);
            Assert.AreEqual("hr", dochtml1body1form0hr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0hr2.NodeType);

        }

        [Test]
        public void TestMethod42()
        {
            var doc = Html(@"<!doctype html><isindex prompt=""this is isindex"">");

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

            var dochtml1body1form0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml1body1form0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0).Attributes.Count);
            Assert.AreEqual("form", dochtml1body1form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0.NodeType);

            var dochtml1body1form0hr0 = dochtml1body1form0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1form0hr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0hr0).Attributes.Count);
            Assert.AreEqual("hr", dochtml1body1form0hr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0hr0.NodeType);

            var dochtml1body1form0label1 = dochtml1body1form0.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1form0label1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0label1).Attributes.Count);
            Assert.AreEqual("label", dochtml1body1form0label1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0label1.NodeType);

            var dochtml1body1form0label1Text0 = dochtml1body1form0label1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1form0label1Text0.NodeType);
            Assert.AreEqual("this is isindex", dochtml1body1form0label1Text0.TextContent);

            var dochtml1body1form0label1input1 = dochtml1body1form0label1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1form0label1input1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1body1form0label1input1).Attributes.Count);
            Assert.AreEqual("input", dochtml1body1form0label1input1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0label1input1.NodeType);

            Assert.IsNotNull(((Element)dochtml1body1form0label1input1).GetAttribute("name"));
            Assert.AreEqual("isindex", ((Element)dochtml1body1form0label1input1).GetAttribute("name"));

            var dochtml1body1form0hr2 = dochtml1body1form0.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1form0hr2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0hr2).Attributes.Count);
            Assert.AreEqual("hr", dochtml1body1form0hr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0hr2.NodeType);

        }

        [Test]
        public void TestMethod43()
        {
            var doc = Html(@"<!doctype html><isindex type=""hidden"">");

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

            var dochtml1body1form0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml1body1form0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0).Attributes.Count);
            Assert.AreEqual("form", dochtml1body1form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0.NodeType);

            var dochtml1body1form0hr0 = dochtml1body1form0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1form0hr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0hr0).Attributes.Count);
            Assert.AreEqual("hr", dochtml1body1form0hr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0hr0.NodeType);

            var dochtml1body1form0label1 = dochtml1body1form0.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1form0label1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0label1).Attributes.Count);
            Assert.AreEqual("label", dochtml1body1form0label1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0label1.NodeType);

            var dochtml1body1form0label1Text0 = dochtml1body1form0label1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1form0label1Text0.NodeType);
            Assert.AreEqual("This is a searchable index. Enter search keywords: ", dochtml1body1form0label1Text0.TextContent);

            var dochtml1body1form0label1input1 = dochtml1body1form0label1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1form0label1input1.ChildNodes.Length);
            Assert.AreEqual(2, ((Element)dochtml1body1form0label1input1).Attributes.Count);
            Assert.AreEqual("input", dochtml1body1form0label1input1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0label1input1.NodeType);

            Assert.IsNotNull(((Element)dochtml1body1form0label1input1).GetAttribute("name"));
            Assert.AreEqual("isindex", ((Element)dochtml1body1form0label1input1).GetAttribute("name"));

            Assert.IsNotNull(((Element)dochtml1body1form0label1input1).GetAttribute("type"));
            Assert.AreEqual("hidden", ((Element)dochtml1body1form0label1input1).GetAttribute("type"));

            var dochtml1body1form0hr2 = dochtml1body1form0.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1form0hr2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0hr2).Attributes.Count);
            Assert.AreEqual("hr", dochtml1body1form0hr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0hr2.NodeType);

        }

        [Test]
        public void TestMethod44()
        {
            var doc = Html(@"<!doctype html><isindex name=""foo"">");

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

            var dochtml1body1form0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml1body1form0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0).Attributes.Count);
            Assert.AreEqual("form", dochtml1body1form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0.NodeType);

            var dochtml1body1form0hr0 = dochtml1body1form0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1form0hr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0hr0).Attributes.Count);
            Assert.AreEqual("hr", dochtml1body1form0hr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0hr0.NodeType);

            var dochtml1body1form0label1 = dochtml1body1form0.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1form0label1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0label1).Attributes.Count);
            Assert.AreEqual("label", dochtml1body1form0label1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0label1.NodeType);

            var dochtml1body1form0label1Text0 = dochtml1body1form0label1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1form0label1Text0.NodeType);
            Assert.AreEqual("This is a searchable index. Enter search keywords: ", dochtml1body1form0label1Text0.TextContent);

            var dochtml1body1form0label1input1 = dochtml1body1form0label1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1form0label1input1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1body1form0label1input1).Attributes.Count);
            Assert.AreEqual("input", dochtml1body1form0label1input1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0label1input1.NodeType);

            Assert.IsNotNull(((Element)dochtml1body1form0label1input1).GetAttribute("name"));
            Assert.AreEqual("isindex", ((Element)dochtml1body1form0label1input1).GetAttribute("name"));

            var dochtml1body1form0hr2 = dochtml1body1form0.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1form0hr2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0hr2).Attributes.Count);
            Assert.AreEqual("hr", dochtml1body1form0hr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0hr2.NodeType);

        }

        [Test]
        public void TestMethod45()
        {
            var doc = Html(@"<!doctype html><ruby><p><rp>");

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

            var dochtml1body1ruby0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0).Attributes.Count);
            Assert.AreEqual("ruby", dochtml1body1ruby0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0.NodeType);

            var dochtml1body1ruby0p0 = dochtml1body1ruby0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1ruby0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1ruby0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0p0.NodeType);

            var dochtml1body1ruby0rp1 = dochtml1body1ruby0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1ruby0rp1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0rp1).Attributes.Count);
            Assert.AreEqual("rp", dochtml1body1ruby0rp1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0rp1.NodeType);

        }

        [Test]
        public void TestMethod46()
        {
            var doc = Html(@"<!doctype html><ruby><div><span><rp>");

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

            var dochtml1body1ruby0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0).Attributes.Count);
            Assert.AreEqual("ruby", dochtml1body1ruby0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0.NodeType);

            var dochtml1body1ruby0div0 = dochtml1body1ruby0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1ruby0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0div0).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1ruby0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0div0.NodeType);

            var dochtml1body1ruby0div0span0 = dochtml1body1ruby0div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1ruby0div0span0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0div0span0).Attributes.Count);
            Assert.AreEqual("span", dochtml1body1ruby0div0span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0div0span0.NodeType);

            var dochtml1body1ruby0div0span0rp0 = dochtml1body1ruby0div0span0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1ruby0div0span0rp0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0div0span0rp0).Attributes.Count);
            Assert.AreEqual("rp", dochtml1body1ruby0div0span0rp0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0div0span0rp0.NodeType);

        }

        [Test]
        public void TestMethod47()
        {
            var doc = Html(@"<!doctype html><ruby><div><p><rp>");

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

            var dochtml1body1ruby0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0).Attributes.Count);
            Assert.AreEqual("ruby", dochtml1body1ruby0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0.NodeType);

            var dochtml1body1ruby0div0 = dochtml1body1ruby0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1ruby0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0div0).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1ruby0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0div0.NodeType);

            var dochtml1body1ruby0div0p0 = dochtml1body1ruby0div0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1ruby0div0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0div0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1ruby0div0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0div0p0.NodeType);

            var dochtml1body1ruby0div0rp1 = dochtml1body1ruby0div0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1ruby0div0rp1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0div0rp1).Attributes.Count);
            Assert.AreEqual("rp", dochtml1body1ruby0div0rp1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0div0rp1.NodeType);

        }

        [Test]
        public void TestMethod48()
        {
            var doc = Html(@"<!doctype html><ruby><p><rt>");

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

            var dochtml1body1ruby0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0).Attributes.Count);
            Assert.AreEqual("ruby", dochtml1body1ruby0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0.NodeType);

            var dochtml1body1ruby0p0 = dochtml1body1ruby0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1ruby0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1ruby0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0p0.NodeType);

            var dochtml1body1ruby0rt1 = dochtml1body1ruby0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1ruby0rt1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0rt1).Attributes.Count);
            Assert.AreEqual("rt", dochtml1body1ruby0rt1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0rt1.NodeType);

        }

        [Test]
        public void TestMethod49()
        {
            var doc = Html(@"<!doctype html><ruby><div><span><rt>");

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

            var dochtml1body1ruby0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0).Attributes.Count);
            Assert.AreEqual("ruby", dochtml1body1ruby0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0.NodeType);

            var dochtml1body1ruby0div0 = dochtml1body1ruby0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1ruby0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0div0).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1ruby0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0div0.NodeType);

            var dochtml1body1ruby0div0span0 = dochtml1body1ruby0div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1ruby0div0span0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0div0span0).Attributes.Count);
            Assert.AreEqual("span", dochtml1body1ruby0div0span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0div0span0.NodeType);

            var dochtml1body1ruby0div0span0rt0 = dochtml1body1ruby0div0span0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1ruby0div0span0rt0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0div0span0rt0).Attributes.Count);
            Assert.AreEqual("rt", dochtml1body1ruby0div0span0rt0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0div0span0rt0.NodeType);

        }

        [Test]
        public void TestMethod50()
        {
            var doc = Html(@"<!doctype html><ruby><div><p><rt>");

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

            var dochtml1body1ruby0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0).Attributes.Count);
            Assert.AreEqual("ruby", dochtml1body1ruby0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0.NodeType);

            var dochtml1body1ruby0div0 = dochtml1body1ruby0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1ruby0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0div0).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1ruby0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0div0.NodeType);

            var dochtml1body1ruby0div0p0 = dochtml1body1ruby0div0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1ruby0div0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0div0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1ruby0div0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0div0p0.NodeType);

            var dochtml1body1ruby0div0rt1 = dochtml1body1ruby0div0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1ruby0div0rt1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1ruby0div0rt1).Attributes.Count);
            Assert.AreEqual("rt", dochtml1body1ruby0div0rt1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1ruby0div0rt1.NodeType);

        }

        [Test]
        public void TestMethod51()
        {
            var doc = Html(@"<html><ruby>a<rb>b<rt></ruby></html>");

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

            var dochtml0body1ruby0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0).Attributes.Count);
            Assert.AreEqual("ruby", dochtml0body1ruby0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0.NodeType);

            var dochtml0body1ruby0Text0 = dochtml0body1ruby0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1ruby0Text0.TextContent);

            var dochtml0body1ruby0rb1 = dochtml0body1ruby0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1ruby0rb1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0rb1).Attributes.Count);
            Assert.AreEqual("rb", dochtml0body1ruby0rb1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0rb1.NodeType);

            var dochtml0body1ruby0rb1Text0 = dochtml0body1ruby0rb1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0rb1Text0.NodeType);
            Assert.AreEqual("b", dochtml0body1ruby0rb1Text0.TextContent);

            var dochtml0body1ruby0rt2 = dochtml0body1ruby0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1ruby0rt2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0rt2).Attributes.Count);
            Assert.AreEqual("rt", dochtml0body1ruby0rt2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0rt2.NodeType);

        }

        [Test]
        public void TestMethod52()
        {
            var doc = Html(@"<html><ruby>a<rp>b<rt></ruby></html>");

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

            var dochtml0body1ruby0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0).Attributes.Count);
            Assert.AreEqual("ruby", dochtml0body1ruby0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0.NodeType);

            var dochtml0body1ruby0Text0 = dochtml0body1ruby0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1ruby0Text0.TextContent);

            var dochtml0body1ruby0rp1 = dochtml0body1ruby0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1ruby0rp1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0rp1).Attributes.Count);
            Assert.AreEqual("rp", dochtml0body1ruby0rp1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0rp1.NodeType);

            var dochtml0body1ruby0rp1Text0 = dochtml0body1ruby0rp1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0rp1Text0.NodeType);
            Assert.AreEqual("b", dochtml0body1ruby0rp1Text0.TextContent);

            var dochtml0body1ruby0rt2 = dochtml0body1ruby0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1ruby0rt2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0rt2).Attributes.Count);
            Assert.AreEqual("rt", dochtml0body1ruby0rt2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0rt2.NodeType);

        }

        [Test]
        public void TestMethod53()
        {
            var doc = Html(@"<html><ruby>a<rt>b<rt></ruby></html>");

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

            var dochtml0body1ruby0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0).Attributes.Count);
            Assert.AreEqual("ruby", dochtml0body1ruby0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0.NodeType);

            var dochtml0body1ruby0Text0 = dochtml0body1ruby0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1ruby0Text0.TextContent);

            var dochtml0body1ruby0rt1 = dochtml0body1ruby0.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1ruby0rt1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0rt1).Attributes.Count);
            Assert.AreEqual("rt", dochtml0body1ruby0rt1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0rt1.NodeType);

            var dochtml0body1ruby0rt1Text0 = dochtml0body1ruby0rt1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0rt1Text0.NodeType);
            Assert.AreEqual("b", dochtml0body1ruby0rt1Text0.TextContent);

            var dochtml0body1ruby0rt2 = dochtml0body1ruby0.ChildNodes[2];
            Assert.AreEqual(0, dochtml0body1ruby0rt2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0rt2).Attributes.Count);
            Assert.AreEqual("rt", dochtml0body1ruby0rt2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0rt2.NodeType);

        }

        [Test]
        public void TestMethod54()
        {
            var doc = Html(@"<html><ruby>a<rtc>b<rt>c<rb>d</ruby></html>");

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

            var dochtml0body1ruby0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(3, dochtml0body1ruby0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0).Attributes.Count);
            Assert.AreEqual("ruby", dochtml0body1ruby0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0.NodeType);

            var dochtml0body1ruby0Text0 = dochtml0body1ruby0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1ruby0Text0.TextContent);

            var dochtml0body1ruby0rtc1 = dochtml0body1ruby0.ChildNodes[1];
            Assert.AreEqual(2, dochtml0body1ruby0rtc1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0rtc1).Attributes.Count);
            Assert.AreEqual("rtc", dochtml0body1ruby0rtc1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0rtc1.NodeType);

            var dochtml0body1ruby0rtc1Text0 = dochtml0body1ruby0rtc1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0rtc1Text0.NodeType);
            Assert.AreEqual("b", dochtml0body1ruby0rtc1Text0.TextContent);

            var dochtml0body1ruby0rtc1rt1 = dochtml0body1ruby0rtc1.ChildNodes[1];
            Assert.AreEqual(1, dochtml0body1ruby0rtc1rt1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0rtc1rt1).Attributes.Count);
            Assert.AreEqual("rt", dochtml0body1ruby0rtc1rt1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0rtc1rt1.NodeType);

            var dochtml0body1ruby0rtc1rt1Text0 = dochtml0body1ruby0rtc1rt1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0rtc1rt1Text0.NodeType);
            Assert.AreEqual("c", dochtml0body1ruby0rtc1rt1Text0.TextContent);

            var dochtml0body1ruby0rb2 = dochtml0body1ruby0.ChildNodes[2];
            Assert.AreEqual(1, dochtml0body1ruby0rb2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1ruby0rb2).Attributes.Count);
            Assert.AreEqual("rb", dochtml0body1ruby0rb2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1ruby0rb2.NodeType);

            var dochtml0body1ruby0rb2Text0 = dochtml0body1ruby0rb2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1ruby0rb2Text0.NodeType);
            Assert.AreEqual("d", dochtml0body1ruby0rb2Text0.TextContent);

        }

        [Test]
        public void TestMethod55()
        {
            var doc = Html(@"<!doctype html><math/><foo>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math0).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1foo1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1foo1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1foo1).Attributes.Count);
            Assert.AreEqual("foo", dochtml1body1foo1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1foo1.NodeType);

        }

        [Test]
        public void TestMethod56()
        {
            var doc = Html(@"<!doctype html><svg/><foo>");

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1foo1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1foo1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1foo1).Attributes.Count);
            Assert.AreEqual("foo", dochtml1body1foo1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1foo1.NodeType);

        }

        [Test]
        public void TestMethod57()
        {
            var doc = Html(@"<!doctype html><div></body><!--foo-->");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(3, dochtml1.ChildNodes.Length);
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

            var dochtml1body1div0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0.NodeType);

            var dochtml1Comment2 = dochtml1.ChildNodes[2];
            Assert.AreEqual(NodeType.Comment, dochtml1Comment2.NodeType);
            Assert.AreEqual(@"foo", dochtml1Comment2.TextContent);

        }

        [Test]
        public void TestMethod58()
        {
            var doc = Html(@"<!doctype html><h1><div><h3><span></h1>foo");

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

            var dochtml1body1h10 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1h10.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1h10).Attributes.Count);
            Assert.AreEqual("h1", dochtml1body1h10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1h10.NodeType);

            var dochtml1body1h10div0 = dochtml1body1h10.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1h10div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1h10div0).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1h10div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1h10div0.NodeType);

            var dochtml1body1h10div0h30 = dochtml1body1h10div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1h10div0h30.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1h10div0h30).Attributes.Count);
            Assert.AreEqual("h3", dochtml1body1h10div0h30.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1h10div0h30.NodeType);

            var dochtml1body1h10div0h30span0 = dochtml1body1h10div0h30.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1h10div0h30span0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1h10div0h30span0).Attributes.Count);
            Assert.AreEqual("span", dochtml1body1h10div0h30span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1h10div0h30span0.NodeType);

            var dochtml1body1h10div0Text1 = dochtml1body1h10div0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1h10div0Text1.NodeType);
            Assert.AreEqual("foo", dochtml1body1h10div0Text1.TextContent);

        }

        [Test]
        public void TestMethod59()
        {
            var doc = Html(@"<!doctype html><p></h3>foo");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0Text0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1p0Text0.TextContent);

        }

        [Test]
        public void TestMethod60()
        {
            var doc = Html(@"<!doctype html><h3><li>abc</h2>foo");

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

            var dochtml1body1h30 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1h30.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1h30).Attributes.Count);
            Assert.AreEqual("h3", dochtml1body1h30.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1h30.NodeType);

            var dochtml1body1h30li0 = dochtml1body1h30.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1h30li0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1h30li0).Attributes.Count);
            Assert.AreEqual("li", dochtml1body1h30li0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1h30li0.NodeType);

            var dochtml1body1h30li0Text0 = dochtml1body1h30li0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1h30li0Text0.NodeType);
            Assert.AreEqual("abc", dochtml1body1h30li0Text0.TextContent);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("foo", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void TestMethod61()
        {
            var doc = Html(@"<!doctype html><table>abc<!--foo-->");

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

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("abc", dochtml1body1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1Comment0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml1body1table1Comment0.NodeType);
            Assert.AreEqual(@"foo", dochtml1body1table1Comment0.TextContent);

        }

        [Test]
        public void TestMethod62()
        {
            var doc = Html(@"<!doctype html><table>  <!--foo-->");

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

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0Text0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0Text0.NodeType);
            Assert.AreEqual("  ", dochtml1body1table0Text0.TextContent);

            var dochtml1body1table0Comment1 = dochtml1body1table0.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml1body1table0Comment1.NodeType);
            Assert.AreEqual(@"foo", dochtml1body1table0Comment1.TextContent);

        }

        [Test]
        public void TestMethod63()
        {
            var doc = Html(@"<!doctype html><table> b <!--foo-->");

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

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual(" b ", dochtml1body1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table1).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1Comment0 = dochtml1body1table1.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml1body1table1Comment0.NodeType);
            Assert.AreEqual(@"foo", dochtml1body1table1Comment0.TextContent);

        }

        [Test]
        public void TestMethod64()
        {
            var doc = Html(@"<!doctype html><select><option><option>");

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

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0option0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0option0).Attributes.Count);
            Assert.AreEqual("option", dochtml1body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0option0.NodeType);

            var dochtml1body1select0option1 = dochtml1body1select0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1select0option1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0option1).Attributes.Count);
            Assert.AreEqual("option", dochtml1body1select0option1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0option1.NodeType);

        }

        [Test]
        public void TestMethod65()
        {
            var doc = Html(@"<!doctype html><select><option></optgroup>");

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

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0option0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0option0).Attributes.Count);
            Assert.AreEqual("option", dochtml1body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0option0.NodeType);

        }

        [Test]
        public void TestMethod66()
        {
            var doc = Html(@"<!doctype html><select><option></optgroup>");

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

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0option0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0option0).Attributes.Count);
            Assert.AreEqual("option", dochtml1body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0option0.NodeType);

        }

        [Test]
        public void TestMethod67()
        {
            var doc = Html(@"<!doctype html><dd><optgroup><dd>");

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

            var dochtml1body1dd0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1dd0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1dd0).Attributes.Count);
            Assert.AreEqual("dd", dochtml1body1dd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1dd0.NodeType);

            var dochtml1body1dd0optgroup0 = dochtml1body1dd0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1dd0optgroup0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1dd0optgroup0).Attributes.Count);
            Assert.AreEqual("optgroup", dochtml1body1dd0optgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1dd0optgroup0.NodeType);

            var dochtml1body1dd1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1dd1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1dd1).Attributes.Count);
            Assert.AreEqual("dd", dochtml1body1dd1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1dd1.NodeType);

        }

        [Test]
        public void TestMethod68()
        {
            var doc = Html(@"<!doctype html><p><math><mi><p><h1>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0math0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1p0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0.NodeType);

            var dochtml1body1p0math0mi0 = dochtml1body1p0math0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1p0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mi0).Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1p0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mi0.NodeType);

            var dochtml1body1p0math0mi0p0 = dochtml1body1p0math0mi0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0math0mi0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mi0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0math0mi0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mi0p0.NodeType);

            var dochtml1body1p0math0mi0h11 = dochtml1body1p0math0mi0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1p0math0mi0h11.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mi0h11).Attributes.Count);
            Assert.AreEqual("h1", dochtml1body1p0math0mi0h11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mi0h11.NodeType);

        }

        [Test]
        public void TestMethod69()
        {
            var doc = Html(@"<!doctype html><p><math><mo><p><h1>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0math0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1p0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0.NodeType);

            var dochtml1body1p0math0mo0 = dochtml1body1p0math0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1p0math0mo0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mo0).Attributes.Count);
            Assert.AreEqual("mo", dochtml1body1p0math0mo0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mo0.NodeType);

            var dochtml1body1p0math0mo0p0 = dochtml1body1p0math0mo0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0math0mo0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mo0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0math0mo0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mo0p0.NodeType);

            var dochtml1body1p0math0mo0h11 = dochtml1body1p0math0mo0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1p0math0mo0h11.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mo0h11).Attributes.Count);
            Assert.AreEqual("h1", dochtml1body1p0math0mo0h11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mo0h11.NodeType);

        }

        [Test]
        public void TestMethod70()
        {
            var doc = Html(@"<!doctype html><p><math><mn><p><h1>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0math0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1p0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0.NodeType);

            var dochtml1body1p0math0mn0 = dochtml1body1p0math0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1p0math0mn0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mn0).Attributes.Count);
            Assert.AreEqual("mn", dochtml1body1p0math0mn0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mn0.NodeType);

            var dochtml1body1p0math0mn0p0 = dochtml1body1p0math0mn0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0math0mn0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mn0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0math0mn0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mn0p0.NodeType);

            var dochtml1body1p0math0mn0h11 = dochtml1body1p0math0mn0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1p0math0mn0h11.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mn0h11).Attributes.Count);
            Assert.AreEqual("h1", dochtml1body1p0math0mn0h11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mn0h11.NodeType);

        }

        [Test]
        public void TestMethod71()
        {
            var doc = Html(@"<!doctype html><p><math><ms><p><h1>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0math0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1p0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0.NodeType);

            var dochtml1body1p0math0ms0 = dochtml1body1p0math0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1p0math0ms0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0ms0).Attributes.Count);
            Assert.AreEqual("ms", dochtml1body1p0math0ms0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0ms0.NodeType);

            var dochtml1body1p0math0ms0p0 = dochtml1body1p0math0ms0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0math0ms0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0ms0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0math0ms0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0ms0p0.NodeType);

            var dochtml1body1p0math0ms0h11 = dochtml1body1p0math0ms0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1p0math0ms0h11.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0ms0h11).Attributes.Count);
            Assert.AreEqual("h1", dochtml1body1p0math0ms0h11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0ms0h11.NodeType);

        }

        [Test]
        public void TestMethod72()
        {
            var doc = Html(@"<!doctype html><p><math><mtext><p><h1>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0math0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1p0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0.NodeType);

            var dochtml1body1p0math0mtext0 = dochtml1body1p0math0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1p0math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mtext0).Attributes.Count);
            Assert.AreEqual("mtext", dochtml1body1p0math0mtext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mtext0.NodeType);

            var dochtml1body1p0math0mtext0p0 = dochtml1body1p0math0mtext0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0math0mtext0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mtext0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0math0mtext0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mtext0p0.NodeType);

            var dochtml1body1p0math0mtext0h11 = dochtml1body1p0math0mtext0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1p0math0mtext0h11.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mtext0h11).Attributes.Count);
            Assert.AreEqual("h1", dochtml1body1p0math0mtext0h11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mtext0h11.NodeType);

        }

        [Test]
        public void TestMethod73()
        {
            var doc = Html(@"<!doctype html><frameset></noframes>");

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

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

        }

        [Test]
        public void TestMethod74()
        {
            var doc = Html(@"<!doctype html><html c=d><body></html><html a=b>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(2, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            Assert.IsNotNull(((Element)dochtml1).GetAttribute("a"));
            Assert.AreEqual("b", ((Element)dochtml1).GetAttribute("a"));

            Assert.IsNotNull(((Element)dochtml1).GetAttribute("c"));
            Assert.AreEqual("d", ((Element)dochtml1).GetAttribute("c"));

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
        public void TestMethod75()
        {
            var doc = Html(@"<!doctype html><html c=d><frameset></frameset></html><html a=b>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(2, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            Assert.IsNotNull(((Element)dochtml1).GetAttribute("a"));
            Assert.AreEqual("b", ((Element)dochtml1).GetAttribute("a"));

            Assert.IsNotNull(((Element)dochtml1).GetAttribute("c"));
            Assert.AreEqual("d", ((Element)dochtml1).GetAttribute("c"));

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

        }

        [Test]
        public void TestMethod76()
        {
            var doc = Html(@"<!doctype html><html><frameset></frameset></html><!--foo-->");

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

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);


            var docComment2 = doc.ChildNodes[2];
            Assert.AreEqual(NodeType.Comment, docComment2.NodeType);
            Assert.AreEqual(@"foo", docComment2.TextContent);

        }

        [Test]
        public void TestMethod77()
        {
            var doc = Html(@"<!doctype html><html><frameset></frameset></html>  ");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(3, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

            var dochtml1Text2 = dochtml1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml1Text2.NodeType);
            Assert.AreEqual("  ", dochtml1Text2.TextContent);

        }

        [Test]
        public void TestMethod78()
        {
            var doc = Html(@"<!doctype html><html><frameset></frameset></html>abc");

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

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

        }

        [Test]
        public void TestMethod79()
        {
            var doc = Html(@"<!doctype html><html><frameset></frameset></html><p>");

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

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

        }

        [Test]
        public void TestMethod80()
        {
            var doc = Html(@"<!doctype html><html><frameset></frameset></html></p>");

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

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

        }

        [Test]
        public void TestMethod81()
        {
            var doc = Html(@"<html><frameset></frameset></html><!doctype html>");

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

            var dochtml0frameset1 = dochtml0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml0frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0frameset1.NodeType);

        }

        [Test]
        public void TestMethod82()
        {
            var doc = Html(@"<!doctype html><body><frameset>");

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
        public void TestMethod83()
        {
            var doc = Html(@"<!doctype html><p><frameset><frame>");

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

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

            var dochtml1frameset1frame0 = dochtml1frameset1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1frameset1frame0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1frame0).Attributes.Count);
            Assert.AreEqual("frame", dochtml1frameset1frame0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1frame0.NodeType);

        }

        [Test]
        public void TestMethod84()
        {
            var doc = Html(@"<!doctype html><p>a<frameset>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0Text0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p0Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1p0Text0.TextContent);

        }

        [Test]
        public void TestMethod85()
        {
            var doc = Html(@"<!doctype html><p> <frameset><frame>");

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

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

            var dochtml1frameset1frame0 = dochtml1frameset1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1frameset1frame0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1frame0).Attributes.Count);
            Assert.AreEqual("frame", dochtml1frameset1frame0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1frame0.NodeType);

        }

        [Test]
        public void TestMethod86()
        {
            var doc = Html(@"<!doctype html><pre><frameset>");

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
        public void TestMethod87()
        {
            var doc = Html(@"<!doctype html><listing><frameset>");

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

            var dochtml1body1listing0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1listing0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1listing0).Attributes.Count);
            Assert.AreEqual("listing", dochtml1body1listing0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1listing0.NodeType);

        }

        [Test]
        public void TestMethod88()
        {
            var doc = Html(@"<!doctype html><li><frameset>");

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

            var dochtml1body1li0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1li0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1li0).Attributes.Count);
            Assert.AreEqual("li", dochtml1body1li0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1li0.NodeType);

        }

        [Test]
        public void TestMethod89()
        {
            var doc = Html(@"<!doctype html><dd><frameset>");

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

            var dochtml1body1dd0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1dd0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1dd0).Attributes.Count);
            Assert.AreEqual("dd", dochtml1body1dd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1dd0.NodeType);

        }

        [Test]
        public void TestMethod90()
        {
            var doc = Html(@"<!doctype html><dt><frameset>");

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

            var dochtml1body1dt0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1dt0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1dt0).Attributes.Count);
            Assert.AreEqual("dt", dochtml1body1dt0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1dt0.NodeType);

        }

        [Test]
        public void TestMethod91()
        {
            var doc = Html(@"<!doctype html><button><frameset>");

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

            var dochtml1body1button0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1button0.NodeType);

        }

        [Test]
        public void TestMethod92()
        {
            var doc = Html(@"<!doctype html><applet><frameset>");

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

            var dochtml1body1applet0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1applet0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1applet0).Attributes.Count);
            Assert.AreEqual("applet", dochtml1body1applet0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1applet0.NodeType);

        }

        [Test]
        public void TestMethod93()
        {
            var doc = Html(@"<!doctype html><marquee><frameset>");

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

            var dochtml1body1marquee0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1marquee0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1marquee0).Attributes.Count);
            Assert.AreEqual("marquee", dochtml1body1marquee0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1marquee0.NodeType);

        }

        [Test]
        public void TestMethod94()
        {
            var doc = Html(@"<!doctype html><object><frameset>");

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

            var dochtml1body1object0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1object0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1object0).Attributes.Count);
            Assert.AreEqual("object", dochtml1body1object0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1object0.NodeType);

        }

        [Test]
        public void TestMethod95()
        {
            var doc = Html(@"<!doctype html><table><frameset>");

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

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

        }

        [Test]
        public void TestMethod96()
        {
            var doc = Html(@"<!doctype html><area><frameset>");

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

            var dochtml1body1area0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1area0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1area0).Attributes.Count);
            Assert.AreEqual("area", dochtml1body1area0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1area0.NodeType);

        }

        [Test]
        public void TestMethod97()
        {
            var doc = Html(@"<!doctype html><basefont><frameset>");

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

            var dochtml1head0basefont0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0basefont0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0basefont0).Attributes.Count);
            Assert.AreEqual("basefont", dochtml1head0basefont0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0basefont0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

        }

        [Test]
        public void TestMethod98()
        {
            var doc = Html(@"<!doctype html><bgsound><frameset>");

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

            var dochtml1head0bgsound0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0bgsound0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0bgsound0).Attributes.Count);
            Assert.AreEqual("bgsound", dochtml1head0bgsound0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0bgsound0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

        }

        [Test]
        public void TestMethod99()
        {
            var doc = Html(@"<!doctype html><br><frameset>");

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

            var dochtml1body1br0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1br0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1br0).Attributes.Count);
            Assert.AreEqual("br", dochtml1body1br0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1br0.NodeType);

        }

        [Test]
        public void TestMethod100()
        {
            var doc = Html(@"<!doctype html><embed><frameset>");

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

            var dochtml1body1embed0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1embed0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1embed0).Attributes.Count);
            Assert.AreEqual("embed", dochtml1body1embed0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1embed0.NodeType);

        }

        [Test]
        public void TestMethod101()
        {
            var doc = Html(@"<!doctype html><img><frameset>");

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

            var dochtml1body1img0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1img0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1img0).Attributes.Count);
            Assert.AreEqual("img", dochtml1body1img0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1img0.NodeType);

        }

        [Test]
        public void TestMethod102()
        {
            var doc = Html(@"<!doctype html><input><frameset>");

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

            var dochtml1body1input0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1input0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1input0).Attributes.Count);
            Assert.AreEqual("input", dochtml1body1input0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1input0.NodeType);

        }

        [Test]
        public void TestMethod103()
        {
            var doc = Html(@"<!doctype html><keygen><frameset>");

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

            var dochtml1body1keygen0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1keygen0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1keygen0).Attributes.Count);
            Assert.AreEqual("keygen", dochtml1body1keygen0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1keygen0.NodeType);

        }

        [Test]
        public void TestMethod104()
        {
            var doc = Html(@"<!doctype html><wbr><frameset>");

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

            var dochtml1body1wbr0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1wbr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1wbr0).Attributes.Count);
            Assert.AreEqual("wbr", dochtml1body1wbr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1wbr0.NodeType);

        }

        [Test]
        public void TestMethod105()
        {
            var doc = Html(@"<!doctype html><hr><frameset>");

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

            var dochtml1body1hr0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1hr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1hr0).Attributes.Count);
            Assert.AreEqual("hr", dochtml1body1hr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1hr0.NodeType);

        }

        [Test]
        public void TestMethod106()
        {
            var doc = Html(@"<!doctype html><textarea></textarea><frameset>");

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
        public void TestMethod107()
        {
            var doc = Html(@"<!doctype html><xmp></xmp><frameset>");

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

            var dochtml1body1xmp0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1xmp0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1xmp0).Attributes.Count);
            Assert.AreEqual("xmp", dochtml1body1xmp0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1xmp0.NodeType);

        }

        [Test]
        public void TestMethod108()
        {
            var doc = Html(@"<!doctype html><iframe></iframe><frameset>");

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

            var dochtml1body1iframe0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1iframe0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1iframe0).Attributes.Count);
            Assert.AreEqual("iframe", dochtml1body1iframe0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1iframe0.NodeType);

        }

        [Test]
        public void TestMethod109()
        {
            var doc = Html(@"<!doctype html><select></select><frameset>");

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

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1select0).Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

        }

        [Test]
        public void TestMethod110()
        {
            var doc = Html(@"<!doctype html><svg></svg><frameset><frame>");

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

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

            var dochtml1frameset1frame0 = dochtml1frameset1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1frameset1frame0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1frame0).Attributes.Count);
            Assert.AreEqual("frame", dochtml1frameset1frame0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1frame0.NodeType);

        }

        [Test]
        public void TestMethod111()
        {
            var doc = Html(@"<!doctype html><math></math><frameset><frame>");

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

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

            var dochtml1frameset1frame0 = dochtml1frameset1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1frameset1frame0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1frame0).Attributes.Count);
            Assert.AreEqual("frame", dochtml1frameset1frame0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1frame0.NodeType);

        }

        [Test]
        public void TestMethod112()
        {
            var doc = Html(@"<!doctype html><svg><foreignObject><div> <frameset><frame>");

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

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

            var dochtml1frameset1frame0 = dochtml1frameset1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1frameset1frame0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1frame0).Attributes.Count);
            Assert.AreEqual("frame", dochtml1frameset1frame0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1frame0.NodeType);

        }

        [Test]
        public void TestMethod113()
        {
            var doc = Html(@"<!doctype html><svg>a</svg><frameset><frame>");

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0Text0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1svg0Text0.TextContent);

        }

        [Test]
        public void TestMethod114()
        {
            var doc = Html(@"<!doctype html><svg> </svg><frameset><frame>");

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

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

            var dochtml1frameset1frame0 = dochtml1frameset1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1frameset1frame0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1frame0).Attributes.Count);
            Assert.AreEqual("frame", dochtml1frameset1frame0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1frame0.NodeType);

        }

        [Test]
        public void TestMethod115()
        {
            var doc = Html(@"<html>aaa<frameset></frameset>");

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("aaa", dochtml0body1Text0.TextContent);

        }

        [Test]
        public void TestMethod116()
        {
            var doc = Html(@"<html> a <frameset></frameset>");

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

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("a ", dochtml0body1Text0.TextContent);

        }

        [Test]
        public void TestMethod117()
        {
            var doc = Html(@"<!doctype html><div><frameset>");

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

            var dochtml1frameset1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1frameset1).Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);

        }

        [Test]
        public void TestMethod118()
        {
            var doc = Html(@"<!doctype html><div><body><frameset>");

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

            var dochtml1body1div0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0.NodeType);

        }

        [Test]
        public void TestMethod119()
        {
            var doc = Html(@"<!doctype html><p><math></p>a");

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
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0math0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1p0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("a", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void TestMethod120()
        {
            var doc = Html(@"<!doctype html><p><math><mn><span></p>a");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0math0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1p0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0.NodeType);

            var dochtml1body1p0math0mn0 = dochtml1body1p0math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0math0mn0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mn0).Attributes.Count);
            Assert.AreEqual("mn", dochtml1body1p0math0mn0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mn0.NodeType);

            var dochtml1body1p0math0mn0span0 = dochtml1body1p0math0mn0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1p0math0mn0span0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mn0span0).Attributes.Count);
            Assert.AreEqual("span", dochtml1body1p0math0mn0span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mn0span0.NodeType);

            var dochtml1body1p0math0mn0span0p0 = dochtml1body1p0math0mn0span0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0math0mn0span0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0math0mn0span0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0math0mn0span0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0math0mn0span0p0.NodeType);

            var dochtml1body1p0math0mn0span0Text1 = dochtml1body1p0math0mn0span0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1p0math0mn0span0Text1.NodeType);
            Assert.AreEqual("a", dochtml1body1p0math0mn0span0Text1.TextContent);

        }

        [Test]
        public void TestMethod121()
        {
            var doc = Html(@"<!doctype html><math></html>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1math0).Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

        }

        [Test]
        public void TestMethod122()
        {
            var doc = Html(@"<!doctype html><meta charset=""ascii"">");

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
            Assert.AreEqual(1, ((Element)dochtml1head0meta0).Attributes.Count);
            Assert.AreEqual("meta", dochtml1head0meta0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0meta0.NodeType);

            Assert.IsNotNull(((Element)dochtml1head0meta0).GetAttribute("charset"));
            Assert.AreEqual("ascii", ((Element)dochtml1head0meta0).GetAttribute("charset"));

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

        }

        [Test]
        public void TestMethod123()
        {
            var doc = Html(@"<!doctype html><meta http-equiv=""content-type"" content=""text/html;charset=ascii"">");

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
            Assert.AreEqual(2, ((Element)dochtml1head0meta0).Attributes.Count);
            Assert.AreEqual("meta", dochtml1head0meta0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0meta0.NodeType);

            Assert.IsNotNull(((Element)dochtml1head0meta0).GetAttribute("content"));
            Assert.AreEqual("text/html;charset=ascii", ((Element)dochtml1head0meta0).GetAttribute("content"));

            Assert.IsNotNull(((Element)dochtml1head0meta0).GetAttribute("http-equiv"));
            Assert.AreEqual("content-type", ((Element)dochtml1head0meta0).GetAttribute("http-equiv"));

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

        }

        [Test]
        public void TestMethod124()
        {
            var doc = Html(@"<!doctype html><head><!--aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa--><meta charset=""utf8"">");

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
            Assert.AreEqual(2, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0Comment0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml1head0Comment0.NodeType);
            Assert.AreEqual(@"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", dochtml1head0Comment0.TextContent);

            var dochtml1head0meta1 = dochtml1head0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1head0meta1.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml1head0meta1).Attributes.Count);
            Assert.AreEqual("meta", dochtml1head0meta1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0meta1.NodeType);

            Assert.IsNotNull(((Element)dochtml1head0meta1).GetAttribute("charset"));
            Assert.AreEqual("utf8", ((Element)dochtml1head0meta1).GetAttribute("charset"));

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

        }

        [Test]
        public void TestMethod125()
        {
            var doc = Html(@"<!doctype html><html a=b><head></head><html c=d>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(2, ((Element)dochtml1).Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            Assert.IsNotNull(((Element)dochtml1).GetAttribute("a"));
            Assert.AreEqual("b", ((Element)dochtml1).GetAttribute("a"));

            Assert.IsNotNull(((Element)dochtml1).GetAttribute("c"));
            Assert.AreEqual("d", ((Element)dochtml1).GetAttribute("c"));

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
        public void TestMethod126()
        {
            var doc = Html(@"<!doctype html><image/>");

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

            var dochtml1body1img0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1img0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1img0).Attributes.Count);
            Assert.AreEqual("img", dochtml1body1img0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1img0.NodeType);

        }

        [Test]
        public void TestMethod127()
        {
            var doc = Html(@"<!doctype html>a<i>b<table>c<b>d</i>e</b>f");

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

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1Text0.TextContent);

            var dochtml1body1i1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(4, dochtml1body1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i1).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i1.NodeType);

            var dochtml1body1i1Text0 = dochtml1body1i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i1Text0.NodeType);
            Assert.AreEqual("bc", dochtml1body1i1Text0.TextContent);

            var dochtml1body1i1b1 = dochtml1body1i1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1i1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i1b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1i1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i1b1.NodeType);

            var dochtml1body1i1b1Text0 = dochtml1body1i1b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i1b1Text0.NodeType);
            Assert.AreEqual("de", dochtml1body1i1b1Text0.TextContent);

            var dochtml1body1i1Text2 = dochtml1body1i1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml1body1i1Text2.NodeType);
            Assert.AreEqual("f", dochtml1body1i1Text2.TextContent);

            var dochtml1body1i1table3 = dochtml1body1i1.ChildNodes[3];
            Assert.AreEqual(0, dochtml1body1i1table3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i1table3).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1i1table3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i1table3.NodeType);

        }

        [Test]
        public void TestMethod128()
        {
            var doc = Html(@"<!doctype html><table><i>a<b>b<div>c<a>d</i>e</b>f");

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
            Assert.AreEqual(4, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1i0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i0.NodeType);

            var dochtml1body1i0Text0 = dochtml1body1i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i0Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1i0Text0.TextContent);

            var dochtml1body1i0b1 = dochtml1body1i0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1i0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i0b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1i0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i0b1.NodeType);

            var dochtml1body1i0b1Text0 = dochtml1body1i0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i0b1Text0.NodeType);
            Assert.AreEqual("b", dochtml1body1i0b1Text0.TextContent);

            var dochtml1body1b1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b1.NodeType);

            var dochtml1body1div2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(2, dochtml1body1div2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2.NodeType);

            var dochtml1body1div2b0 = dochtml1body1div2.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div2b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1div2b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2b0.NodeType);

            var dochtml1body1div2b0i0 = dochtml1body1div2b0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div2b0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2b0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1div2b0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2b0i0.NodeType);

            var dochtml1body1div2b0i0Text0 = dochtml1body1div2b0i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div2b0i0Text0.NodeType);
            Assert.AreEqual("c", dochtml1body1div2b0i0Text0.TextContent);

            var dochtml1body1div2b0i0a1 = dochtml1body1div2b0i0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1div2b0i0a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2b0i0a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1div2b0i0a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2b0i0a1.NodeType);

            var dochtml1body1div2b0i0a1Text0 = dochtml1body1div2b0i0a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div2b0i0a1Text0.NodeType);
            Assert.AreEqual("d", dochtml1body1div2b0i0a1Text0.TextContent);

            var dochtml1body1div2b0a1 = dochtml1body1div2b0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1div2b0a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2b0a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1div2b0a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2b0a1.NodeType);

            var dochtml1body1div2b0a1Text0 = dochtml1body1div2b0a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div2b0a1Text0.NodeType);
            Assert.AreEqual("e", dochtml1body1div2b0a1Text0.TextContent);

            var dochtml1body1div2a1 = dochtml1body1div2.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1div2a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1div2a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2a1.NodeType);

            var dochtml1body1div2a1Text0 = dochtml1body1div2a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div2a1Text0.NodeType);
            Assert.AreEqual("f", dochtml1body1div2a1Text0.TextContent);

            var dochtml1body1table3 = dochtml1body1.ChildNodes[3];
            Assert.AreEqual(0, dochtml1body1table3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table3).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table3.NodeType);

        }

        [Test]
        public void TestMethod129()
        {
            var doc = Html(@"<!doctype html><i>a<b>b<div>c<a>d</i>e</b>f");

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

            var dochtml1body1i0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i0.NodeType);

            var dochtml1body1i0Text0 = dochtml1body1i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i0Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1i0Text0.TextContent);

            var dochtml1body1i0b1 = dochtml1body1i0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1i0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i0b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1i0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i0b1.NodeType);

            var dochtml1body1i0b1Text0 = dochtml1body1i0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i0b1Text0.NodeType);
            Assert.AreEqual("b", dochtml1body1i0b1Text0.TextContent);

            var dochtml1body1b1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b1.NodeType);

            var dochtml1body1div2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(2, dochtml1body1div2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2.NodeType);

            var dochtml1body1div2b0 = dochtml1body1div2.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div2b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1div2b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2b0.NodeType);

            var dochtml1body1div2b0i0 = dochtml1body1div2b0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div2b0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2b0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1div2b0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2b0i0.NodeType);

            var dochtml1body1div2b0i0Text0 = dochtml1body1div2b0i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div2b0i0Text0.NodeType);
            Assert.AreEqual("c", dochtml1body1div2b0i0Text0.TextContent);

            var dochtml1body1div2b0i0a1 = dochtml1body1div2b0i0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1div2b0i0a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2b0i0a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1div2b0i0a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2b0i0a1.NodeType);

            var dochtml1body1div2b0i0a1Text0 = dochtml1body1div2b0i0a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div2b0i0a1Text0.NodeType);
            Assert.AreEqual("d", dochtml1body1div2b0i0a1Text0.TextContent);

            var dochtml1body1div2b0a1 = dochtml1body1div2b0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1div2b0a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2b0a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1div2b0a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2b0a1.NodeType);

            var dochtml1body1div2b0a1Text0 = dochtml1body1div2b0a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div2b0a1Text0.NodeType);
            Assert.AreEqual("e", dochtml1body1div2b0a1Text0.TextContent);

            var dochtml1body1div2a1 = dochtml1body1div2.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1div2a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1div2a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2a1.NodeType);

            var dochtml1body1div2a1Text0 = dochtml1body1div2a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div2a1Text0.NodeType);
            Assert.AreEqual("f", dochtml1body1div2a1Text0.TextContent);

        }

        [Test]
        public void TestMethod130()
        {
            var doc = Html(@"<!doctype html><table><i>a<b>b<div>c</i>");

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

            var dochtml1body1i0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i0.NodeType);

            var dochtml1body1i0Text0 = dochtml1body1i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i0Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1i0Text0.TextContent);

            var dochtml1body1i0b1 = dochtml1body1i0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1i0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i0b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1i0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i0b1.NodeType);

            var dochtml1body1i0b1Text0 = dochtml1body1i0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i0b1Text0.NodeType);
            Assert.AreEqual("b", dochtml1body1i0b1Text0.TextContent);

            var dochtml1body1b1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b1.NodeType);

            var dochtml1body1b1div0 = dochtml1body1b1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b1div0).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1b1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b1div0.NodeType);

            var dochtml1body1b1div0i0 = dochtml1body1b1div0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b1div0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b1div0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1b1div0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b1div0i0.NodeType);

            var dochtml1body1b1div0i0Text0 = dochtml1body1b1div0i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b1div0i0Text0.NodeType);
            Assert.AreEqual("c", dochtml1body1b1div0i0Text0.TextContent);

            var dochtml1body1table2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1table2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table2).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table2.NodeType);

        }

        [Test]
        public void TestMethod131()
        {
            var doc = Html(@"<!doctype html><table><i>a<b>b<div>c<a>d</i>e</b>f");

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
            Assert.AreEqual(4, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1i0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i0.NodeType);

            var dochtml1body1i0Text0 = dochtml1body1i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i0Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1i0Text0.TextContent);

            var dochtml1body1i0b1 = dochtml1body1i0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1i0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i0b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1i0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i0b1.NodeType);

            var dochtml1body1i0b1Text0 = dochtml1body1i0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i0b1Text0.NodeType);
            Assert.AreEqual("b", dochtml1body1i0b1Text0.TextContent);

            var dochtml1body1b1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b1.NodeType);

            var dochtml1body1div2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(2, dochtml1body1div2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2.NodeType);

            var dochtml1body1div2b0 = dochtml1body1div2.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div2b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1div2b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2b0.NodeType);

            var dochtml1body1div2b0i0 = dochtml1body1div2b0.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div2b0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2b0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1div2b0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2b0i0.NodeType);

            var dochtml1body1div2b0i0Text0 = dochtml1body1div2b0i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div2b0i0Text0.NodeType);
            Assert.AreEqual("c", dochtml1body1div2b0i0Text0.TextContent);

            var dochtml1body1div2b0i0a1 = dochtml1body1div2b0i0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1div2b0i0a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2b0i0a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1div2b0i0a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2b0i0a1.NodeType);

            var dochtml1body1div2b0i0a1Text0 = dochtml1body1div2b0i0a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div2b0i0a1Text0.NodeType);
            Assert.AreEqual("d", dochtml1body1div2b0i0a1Text0.TextContent);

            var dochtml1body1div2b0a1 = dochtml1body1div2b0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1div2b0a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2b0a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1div2b0a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2b0a1.NodeType);

            var dochtml1body1div2b0a1Text0 = dochtml1body1div2b0a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div2b0a1Text0.NodeType);
            Assert.AreEqual("e", dochtml1body1div2b0a1Text0.TextContent);

            var dochtml1body1div2a1 = dochtml1body1div2.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1div2a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div2a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1div2a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div2a1.NodeType);

            var dochtml1body1div2a1Text0 = dochtml1body1div2a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div2a1Text0.NodeType);
            Assert.AreEqual("f", dochtml1body1div2a1Text0.TextContent);

            var dochtml1body1table3 = dochtml1body1.ChildNodes[3];
            Assert.AreEqual(0, dochtml1body1table3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table3).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table3.NodeType);

        }

        [Test]
        public void TestMethod132()
        {
            var doc = Html(@"<!doctype html><table><i>a<div>b<tr>c<b>d</i>e");

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
            Assert.AreEqual(4, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1i0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i0.NodeType);

            var dochtml1body1i0Text0 = dochtml1body1i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i0Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1i0Text0.TextContent);

            var dochtml1body1i0div1 = dochtml1body1i0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1i0div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i0div1).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1i0div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i0div1.NodeType);

            var dochtml1body1i0div1Text0 = dochtml1body1i0div1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i0div1Text0.NodeType);
            Assert.AreEqual("b", dochtml1body1i0div1Text0.TextContent);

            var dochtml1body1i1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1i1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i1).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1i1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i1.NodeType);

            var dochtml1body1i1Text0 = dochtml1body1i1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i1Text0.NodeType);
            Assert.AreEqual("c", dochtml1body1i1Text0.TextContent);

            var dochtml1body1i1b1 = dochtml1body1i1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1i1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1i1b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1i1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1i1b1.NodeType);

            var dochtml1body1i1b1Text0 = dochtml1body1i1b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1i1b1Text0.NodeType);
            Assert.AreEqual("d", dochtml1body1i1b1Text0.TextContent);

            var dochtml1body1b2 = dochtml1body1.ChildNodes[2];
            Assert.AreEqual(1, dochtml1body1b2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b2).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1b2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b2.NodeType);

            var dochtml1body1b2Text0 = dochtml1body1b2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b2Text0.NodeType);
            Assert.AreEqual("e", dochtml1body1b2Text0.TextContent);

            var dochtml1body1table3 = dochtml1body1.ChildNodes[3];
            Assert.AreEqual(1, dochtml1body1table3.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table3).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table3.NodeType);

            var dochtml1body1table3tbody0 = dochtml1body1table3.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table3tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table3tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table3tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table3tbody0.NodeType);

            var dochtml1body1table3tbody0tr0 = dochtml1body1table3tbody0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table3tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table3tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table3tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table3tbody0tr0.NodeType);

        }

        [Test]
        public void TestMethod133()
        {
            var doc = Html(@"<!doctype html><table><td><table><i>a<div>b<b>c</i>d");

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

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0).Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0).Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0];
            Assert.AreEqual(3, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0).Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0i0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0i0.NodeType);

            var dochtml1body1table0tbody0tr0td0i0Text0 = dochtml1body1table0tbody0tr0td0i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0i0Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1table0tbody0tr0td0i0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0div1 = dochtml1body1table0tbody0tr0td0.ChildNodes[1];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0div1).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1table0tbody0tr0td0div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0div1.NodeType);

            var dochtml1body1table0tbody0tr0td0div1i0 = dochtml1body1table0tbody0tr0td0div1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0div1i0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0div1i0).Attributes.Count);
            Assert.AreEqual("i", dochtml1body1table0tbody0tr0td0div1i0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0div1i0.NodeType);

            var dochtml1body1table0tbody0tr0td0div1i0Text0 = dochtml1body1table0tbody0tr0td0div1i0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0div1i0Text0.NodeType);
            Assert.AreEqual("b", dochtml1body1table0tbody0tr0td0div1i0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0div1i0b1 = dochtml1body1table0tbody0tr0td0div1i0.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0div1i0b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0div1i0b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1table0tbody0tr0td0div1i0b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0div1i0b1.NodeType);

            var dochtml1body1table0tbody0tr0td0div1i0b1Text0 = dochtml1body1table0tbody0tr0td0div1i0b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0div1i0b1Text0.NodeType);
            Assert.AreEqual("c", dochtml1body1table0tbody0tr0td0div1i0b1Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0div1b1 = dochtml1body1table0tbody0tr0td0div1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0div1b1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0div1b1).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1table0tbody0tr0td0div1b1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0div1b1.NodeType);

            var dochtml1body1table0tbody0tr0td0div1b1Text0 = dochtml1body1table0tbody0tr0td0div1b1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0div1b1Text0.NodeType);
            Assert.AreEqual("d", dochtml1body1table0tbody0tr0td0div1b1Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0table2 = dochtml1body1table0tbody0tr0td0.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0table2.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0tbody0tr0td0table2).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0tbody0tr0td0table2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0table2.NodeType);

        }

        [Test]
        public void TestMethod134()
        {
            var doc = Html(@"<!doctype html><body><bgsound>");

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

            var dochtml1body1bgsound0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1bgsound0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1bgsound0).Attributes.Count);
            Assert.AreEqual("bgsound", dochtml1body1bgsound0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1bgsound0.NodeType);

        }

        [Test]
        public void TestMethod135()
        {
            var doc = Html(@"<!doctype html><body><basefont>");

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

            var dochtml1body1basefont0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1basefont0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1basefont0).Attributes.Count);
            Assert.AreEqual("basefont", dochtml1body1basefont0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1basefont0.NodeType);

        }

        [Test]
        public void TestMethod136()
        {
            var doc = Html(@"<!doctype html><a><b></a><basefont>");

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

            var dochtml1body1a0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1a0).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a0.NodeType);

            var dochtml1body1a0b0 = dochtml1body1a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1a0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1a0b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1a0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a0b0.NodeType);

            var dochtml1body1basefont1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1basefont1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1basefont1).Attributes.Count);
            Assert.AreEqual("basefont", dochtml1body1basefont1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1basefont1.NodeType);

        }

        [Test]
        public void TestMethod137()
        {
            var doc = Html(@"<!doctype html><a><b></a><bgsound>");

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

            var dochtml1body1a0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1a0).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a0.NodeType);

            var dochtml1body1a0b0 = dochtml1body1a0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1a0b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1a0b0).Attributes.Count);
            Assert.AreEqual("b", dochtml1body1a0b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a0b0.NodeType);

            var dochtml1body1bgsound1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1bgsound1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1bgsound1).Attributes.Count);
            Assert.AreEqual("bgsound", dochtml1body1bgsound1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1bgsound1.NodeType);

        }

        [Test]
        public void TestMethod138()
        {
            var doc = Html(@"<!doctype html><figcaption><article></figcaption>a");

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

            var dochtml1body1figcaption0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1figcaption0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1figcaption0).Attributes.Count);
            Assert.AreEqual("figcaption", dochtml1body1figcaption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1figcaption0.NodeType);

            var dochtml1body1figcaption0article0 = dochtml1body1figcaption0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1figcaption0article0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1figcaption0article0).Attributes.Count);
            Assert.AreEqual("article", dochtml1body1figcaption0article0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1figcaption0article0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("a", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void TestMethod139()
        {
            var doc = Html(@"<!doctype html><summary><article></summary>a");

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

            var dochtml1body1summary0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1summary0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1summary0).Attributes.Count);
            Assert.AreEqual("summary", dochtml1body1summary0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1summary0.NodeType);

            var dochtml1body1summary0article0 = dochtml1body1summary0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1summary0article0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1summary0article0).Attributes.Count);
            Assert.AreEqual("article", dochtml1body1summary0article0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1summary0article0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("a", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void TestMethod140()
        {
            var doc = Html(@"<!doctype html><p><a><plaintext>b");

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
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0a0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0a0).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1p0a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0a0.NodeType);

            var dochtml1body1plaintext1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1plaintext1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1plaintext1).Attributes.Count);
            Assert.AreEqual("plaintext", dochtml1body1plaintext1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1plaintext1.NodeType);

            var dochtml1body1plaintext1a0 = dochtml1body1plaintext1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1plaintext1a0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1plaintext1a0).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1plaintext1a0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1plaintext1a0.NodeType);

            var dochtml1body1plaintext1a0Text0 = dochtml1body1plaintext1a0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1plaintext1a0Text0.NodeType);
            Assert.AreEqual("b", dochtml1body1plaintext1a0Text0.TextContent);

        }

        [Test]
        public void TestMethod141()
        {
            var doc = Html(@"<!DOCTYPE html><div>a<a></div>b<p>c</p>d");

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

            var dochtml1body1div0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0).Attributes.Count);
            Assert.AreEqual("div", dochtml1body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0.NodeType);

            var dochtml1body1div0Text0 = dochtml1body1div0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1div0Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1div0Text0.TextContent);

            var dochtml1body1div0a1 = dochtml1body1div0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1div0a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1div0a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1div0a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1div0a1.NodeType);

            var dochtml1body1a1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(3, dochtml1body1a1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1a1).Attributes.Count);
            Assert.AreEqual("a", dochtml1body1a1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a1.NodeType);

            var dochtml1body1a1Text0 = dochtml1body1a1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1a1Text0.NodeType);
            Assert.AreEqual("b", dochtml1body1a1Text0.TextContent);

            var dochtml1body1a1p1 = dochtml1body1a1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1a1p1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1a1p1).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1a1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1a1p1.NodeType);

            var dochtml1body1a1p1Text0 = dochtml1body1a1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1a1p1Text0.NodeType);
            Assert.AreEqual("c", dochtml1body1a1p1Text0.TextContent);

            var dochtml1body1a1Text2 = dochtml1body1a1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml1body1a1Text2.NodeType);
            Assert.AreEqual("d", dochtml1body1a1Text2.TextContent);

        }

        [Test]
        public void TestMethod142()
        {
            var doc = Html(@"<!doctype html><p><button><button>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(2, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button1 = dochtml1body1p0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1p0button1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button1).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button1.NodeType);

        }

        [Test]
        public void TestMethod143()
        {
            var doc = Html(@"<!doctype html><p><button><address>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0address0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0address0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0address0).Attributes.Count);
            Assert.AreEqual("address", dochtml1body1p0button0address0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0address0.NodeType);

        }

        [Test]
        public void TestMethod144()
        {
            var doc = Html(@"<!doctype html><p><button><blockquote>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0blockquote0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0blockquote0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0blockquote0).Attributes.Count);
            Assert.AreEqual("blockquote", dochtml1body1p0button0blockquote0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0blockquote0.NodeType);

        }

        [Test]
        public void TestMethod145()
        {
            var doc = Html(@"<!doctype html><p><button><menu>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0menu0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0menu0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0menu0).Attributes.Count);
            Assert.AreEqual("menu", dochtml1body1p0button0menu0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0menu0.NodeType);

        }

        [Test]
        public void TestMethod146()
        {
            var doc = Html(@"<!doctype html><p><button><p>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0p0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0button0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0p0.NodeType);

        }

        [Test]
        public void TestMethod147()
        {
            var doc = Html(@"<!doctype html><p><button><ul>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0ul0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0ul0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0ul0).Attributes.Count);
            Assert.AreEqual("ul", dochtml1body1p0button0ul0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0ul0.NodeType);

        }

        [Test]
        public void TestMethod148()
        {
            var doc = Html(@"<!doctype html><p><button><h1>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0h10 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0h10.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0h10).Attributes.Count);
            Assert.AreEqual("h1", dochtml1body1p0button0h10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0h10.NodeType);

        }

        [Test]
        public void TestMethod149()
        {
            var doc = Html(@"<!doctype html><p><button><h6>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0h60 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0h60.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0h60).Attributes.Count);
            Assert.AreEqual("h6", dochtml1body1p0button0h60.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0h60.NodeType);

        }

        [Test]
        public void TestMethod150()
        {
            var doc = Html(@"<!doctype html><p><button><listing>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0listing0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0listing0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0listing0).Attributes.Count);
            Assert.AreEqual("listing", dochtml1body1p0button0listing0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0listing0.NodeType);

        }

        [Test]
        public void TestMethod151()
        {
            var doc = Html(@"<!doctype html><p><button><pre>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0pre0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0pre0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0pre0).Attributes.Count);
            Assert.AreEqual("pre", dochtml1body1p0button0pre0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0pre0.NodeType);

        }

        [Test]
        public void TestMethod152()
        {
            var doc = Html(@"<!doctype html><p><button><form>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0form0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0form0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0form0).Attributes.Count);
            Assert.AreEqual("form", dochtml1body1p0button0form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0form0.NodeType);

        }

        [Test]
        public void TestMethod153()
        {
            var doc = Html(@"<!doctype html><p><button><li>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0li0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0li0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0li0).Attributes.Count);
            Assert.AreEqual("li", dochtml1body1p0button0li0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0li0.NodeType);

        }

        [Test]
        public void TestMethod154()
        {
            var doc = Html(@"<!doctype html><p><button><dd>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0dd0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0dd0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0dd0).Attributes.Count);
            Assert.AreEqual("dd", dochtml1body1p0button0dd0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0dd0.NodeType);

        }

        [Test]
        public void TestMethod155()
        {
            var doc = Html(@"<!doctype html><p><button><dt>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0dt0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0dt0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0dt0).Attributes.Count);
            Assert.AreEqual("dt", dochtml1body1p0button0dt0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0dt0.NodeType);

        }

        [Test]
        public void TestMethod156()
        {
            var doc = Html(@"<!doctype html><p><button><plaintext>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0plaintext0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0plaintext0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0plaintext0).Attributes.Count);
            Assert.AreEqual("plaintext", dochtml1body1p0button0plaintext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0plaintext0.NodeType);

        }

        [Test]
        public void TestMethod157()
        {
            var doc = Html(@"<!doctype html><p><button><table>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0table0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1p0button0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0table0.NodeType);

        }

        [Test]
        public void TestMethod158()
        {
            var doc = Html(@"<!doctype html><p><button><hr>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0hr0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0hr0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0hr0).Attributes.Count);
            Assert.AreEqual("hr", dochtml1body1p0button0hr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0hr0.NodeType);

        }

        [Test]
        public void TestMethod159()
        {
            var doc = Html(@"<!doctype html><p><button><xmp>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0xmp0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0xmp0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0xmp0).Attributes.Count);
            Assert.AreEqual("xmp", dochtml1body1p0button0xmp0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0xmp0.NodeType);

        }

        [Test]
        public void TestMethod160()
        {
            var doc = Html(@"<!doctype html><p><button></p>");

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0button0 = dochtml1body1p0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1p0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1p0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0.NodeType);

            var dochtml1body1p0button0p0 = dochtml1body1p0button0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1p0button0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1p0button0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0button0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0button0p0.NodeType);

        }

        [Test]
        public void TestMethod161()
        {
            var doc = Html(@"<!doctype html><address><button></address>a");

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

            var dochtml1body1address0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1address0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1address0).Attributes.Count);
            Assert.AreEqual("address", dochtml1body1address0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1address0.NodeType);

            var dochtml1body1address0button0 = dochtml1body1address0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1address0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1address0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1address0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1address0button0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("a", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void TestMethod162()
        {
            var doc = Html(@"<!doctype html><address><button></address>a");

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

            var dochtml1body1address0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1address0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1address0).Attributes.Count);
            Assert.AreEqual("address", dochtml1body1address0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1address0.NodeType);

            var dochtml1body1address0button0 = dochtml1body1address0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1address0button0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1address0button0).Attributes.Count);
            Assert.AreEqual("button", dochtml1body1address0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1address0button0.NodeType);

            var dochtml1body1Text1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text1.NodeType);
            Assert.AreEqual("a", dochtml1body1Text1.TextContent);

        }

        [Test]
        public void TestMethod163()
        {
            var doc = Html(@"<p><table></p>");

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
            Assert.AreEqual(2, dochtml0body1p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0).Attributes.Count);
            Assert.AreEqual("p", dochtml0body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0.NodeType);

            var dochtml0body1p0p0 = dochtml0body1p0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1p0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml0body1p0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0p0.NodeType);

            var dochtml0body1p0table1 = dochtml0body1p0.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1p0table1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1p0table1).Attributes.Count);
            Assert.AreEqual("table", dochtml0body1p0table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p0table1.NodeType);

        }

        [Test]
        public void TestMethod164()
        {
            var doc = Html(@"<!doctype html><svg>");

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

        }

        [Test]
        public void TestMethod165()
        {
            var doc = Html(@"<!doctype html><p><figcaption>");

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

            var dochtml1body1figcaption1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1figcaption1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1figcaption1).Attributes.Count);
            Assert.AreEqual("figcaption", dochtml1body1figcaption1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1figcaption1.NodeType);

        }

        [Test]
        public void TestMethod166()
        {
            var doc = Html(@"<!doctype html><p><summary>");

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

            var dochtml1body1summary1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1summary1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1summary1).Attributes.Count);
            Assert.AreEqual("summary", dochtml1body1summary1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1summary1.NodeType);

        }

        [Test]
        public void TestMethod167()
        {
            var doc = Html(@"<!doctype html><form><table><form>");

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

            var dochtml1body1form0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1form0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0).Attributes.Count);
            Assert.AreEqual("form", dochtml1body1form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0.NodeType);

            var dochtml1body1form0table0 = dochtml1body1form0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1form0table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1form0table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1form0table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1form0table0.NodeType);

        }

        [Test]
        public void TestMethod168()
        {
            var doc = Html(@"<!doctype html><table><form><form>");

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

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0form0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0form0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0form0).Attributes.Count);
            Assert.AreEqual("form", dochtml1body1table0form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0form0.NodeType);

        }

        [Test]
        public void TestMethod169()
        {
            var doc = Html(@"<!doctype html><table><form></table><form>");

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

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0).Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0form0 = dochtml1body1table0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1table0form0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1table0form0).Attributes.Count);
            Assert.AreEqual("form", dochtml1body1table0form0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0form0.NodeType);

        }

        [Test]
        public void TestMethod170()
        {
            var doc = Html(@"<!doctype html><svg><foreignObject><p>");

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0foreignObject0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0foreignObject0).Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0.NodeType);

            var dochtml1body1svg0foreignObject0p0 = dochtml1body1svg0foreignObject0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg0foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0foreignObject0p0).Attributes.Count);
            Assert.AreEqual("p", dochtml1body1svg0foreignObject0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject0p0.NodeType);

        }

        [Test]
        public void TestMethod171()
        {
            var doc = Html(@"<!doctype html><svg><title>abc");

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0).Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0title0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0title0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1svg0title0).Attributes.Count);
            Assert.AreEqual("title", dochtml1body1svg0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0title0.NodeType);

            var dochtml1body1svg0title0Text0 = dochtml1body1svg0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0title0Text0.NodeType);
            Assert.AreEqual("abc", dochtml1body1svg0title0Text0.TextContent);

        }

        [Test]
        public void TestMethod172()
        {
            var doc = Html(@"<option><span><option>");

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

            var dochtml0body1option0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1option0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1option0).Attributes.Count);
            Assert.AreEqual("option", dochtml0body1option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option0.NodeType);

            var dochtml0body1option0span0 = dochtml0body1option0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1option0span0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1option0span0).Attributes.Count);
            Assert.AreEqual("span", dochtml0body1option0span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option0span0.NodeType);

            var dochtml0body1option0span0option0 = dochtml0body1option0span0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1option0span0option0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1option0span0option0).Attributes.Count);
            Assert.AreEqual("option", dochtml0body1option0span0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option0span0option0.NodeType);

        }

        [Test]
        public void TestMethod173()
        {
            var doc = Html(@"<option><option>");

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1option0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1option0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1option0).Attributes.Count);
            Assert.AreEqual("option", dochtml0body1option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option0.NodeType);

            var dochtml0body1option1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1option1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1option1).Attributes.Count);
            Assert.AreEqual("option", dochtml0body1option1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1option1.NodeType);

        }

        [Test]
        public void TestMethod174()
        {
            var doc = Html(@"<math><annotation-xml><div>");

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0).Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0annotationxml0).Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1).Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

        }

        [Test]
        public void TestMethod175()
        {
            var doc = Html(@"<math><annotation-xml encoding=""application/svg+xml""><div>");

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0).Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1math0annotationxml0).Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1math0annotationxml0).GetAttribute("encoding"));
            Assert.AreEqual("application/svg+xml", ((Element)dochtml0body1math0annotationxml0).GetAttribute("encoding"));

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1).Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);

        }

        [Test]
        public void SetIncomingDivElementAsChildOfAnnotationXmlWithLowerEncodingXmlElement()
        {
            var doc = Html(@"<math><annotation-xml encoding=""application/xhtml+xml""><div>");

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

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0).Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1math0annotationxml0).Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1math0annotationxml0).GetAttribute("encoding"));
            Assert.AreEqual("application/xhtml+xml", ((Element)dochtml0body1math0annotationxml0).GetAttribute("encoding"));

            var dochtml0body1math0annotationxml0div0 = dochtml0body1math0annotationxml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1math0annotationxml0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0annotationxml0div0).Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0annotationxml0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0div0.NodeType);
        }

        [Test]
        public void SetIncomingDivElementAsChildOfAnnotationXmlWithMixedEncodingXmlElement()
        {
            var doc = Html(@"<math><annotation-xml encoding=""aPPlication/xhtmL+xMl""><div>");

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

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0).Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1math0annotationxml0).Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1math0annotationxml0).GetAttribute("encoding"));
            Assert.AreEqual("aPPlication/xhtmL+xMl", ((Element)dochtml0body1math0annotationxml0).GetAttribute("encoding"));

            var dochtml0body1math0annotationxml0div0 = dochtml0body1math0annotationxml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1math0annotationxml0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0annotationxml0div0).Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0annotationxml0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0div0.NodeType);
        }

        [Test]
        public void SetIncomingDivElementAsChildOfAnnotationXmlWithLowerEncodingHtmlElement()
        {
            var doc = Html(@"<math><annotation-xml encoding=""text/html""><div>");

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

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0).Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1math0annotationxml0).Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1math0annotationxml0).GetAttribute("encoding"));
            Assert.AreEqual("text/html", ((Element)dochtml0body1math0annotationxml0).GetAttribute("encoding"));

            var dochtml0body1math0annotationxml0div0 = dochtml0body1math0annotationxml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1math0annotationxml0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0annotationxml0div0).Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0annotationxml0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0div0.NodeType);
        }

        [Test]
        public void SetIncomingDivElementAsChildOfAnnotationXmlWithMixedEncodingHtmlElement()
        {
            var doc = Html(@"<math><annotation-xml encoding=""Text/htmL""><div>");

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

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0).Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1math0annotationxml0).Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1math0annotationxml0).GetAttribute("encoding"));
            Assert.AreEqual("Text/htmL", ((Element)dochtml0body1math0annotationxml0).GetAttribute("encoding"));

            var dochtml0body1math0annotationxml0div0 = dochtml0body1math0annotationxml0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1math0annotationxml0div0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0annotationxml0div0).Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0annotationxml0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0div0.NodeType);
        }

        [Test]
        public void DoNotSetIncomingDivElementAsChildOfAnnotationXmlElementDueToInvalidEncoding()
        {
            var doc = Html(@"<math><annotation-xml encoding="" text/html ""><div>");

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1).Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1math0).Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0];
            Assert.AreEqual(0, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(1, ((Element)dochtml0body1math0annotationxml0).Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            Assert.IsNotNull(((Element)dochtml0body1math0annotationxml0).GetAttribute("encoding"));
            Assert.AreEqual(" text/html ", ((Element)dochtml0body1math0annotationxml0).GetAttribute("encoding"));

            var dochtml0body1div1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(0, dochtml0body1div1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml0body1div1).Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div1.NodeType);
        }
    }
}