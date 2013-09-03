using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;

namespace UnitTests
{
    [TestClass]
    public class HtmlStyle
    {
        [TestMethod]
        public void StyleWithCommentThatContainsClosingStyleTag()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><style><!--...</style>...--></style>");

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
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0style0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0style0.Attributes.Length);
            Assert.AreEqual("style", dochtml1head0style0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0style0.NodeType);

            var dochtml1head0style0Text0 = dochtml1head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0style0Text0.NodeType);
            Assert.AreEqual("<!--...", dochtml1head0style0Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("...-->", dochtml1body1Text0.TextContent);
        }

        [TestMethod]
        public void StyleWithCommentsAndText()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><style><!--<br><html xmlns:v=""urn:schemas-microsoft-com:vml""><!--[if !mso]><style></style>X");

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
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0style0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0style0.Attributes.Length);
            Assert.AreEqual("style", dochtml1head0style0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0style0.NodeType);

            var dochtml1head0style0Text0 = dochtml1head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0style0Text0.NodeType);
            Assert.AreEqual("<!--<br><html xmlns:v=\"urn:schemas-microsoft-com:vml\"><!--[if !mso]><style>", dochtml1head0style0Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1Text0.TextContent);
        }

        [TestMethod]
        public void StyleWithCommentsAndNestedStyles()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><style><!--...<style><!--...--!></style>--></style>");

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
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0style0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0style0.Attributes.Length);
            Assert.AreEqual("style", dochtml1head0style0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0style0.NodeType);

            var dochtml1head0style0Text0 = dochtml1head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0style0Text0.NodeType);
            Assert.AreEqual("<!--...<style><!--...--!>", dochtml1head0style0Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml1body1Text0.TextContent);
        }

        [TestMethod]
        public void StyleWithNestedCommentAndOtherStyles()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><style><!--...</style><!-- --><style>@import ...</style>");

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
            Assert.AreEqual(3, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0style0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0style0.Attributes.Length);
            Assert.AreEqual("style", dochtml1head0style0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0style0.NodeType);

            var dochtml1head0style0Text0 = dochtml1head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0style0Text0.NodeType);
            Assert.AreEqual("<!--...", dochtml1head0style0Text0.TextContent);

            var dochtml1head0Comment1 = dochtml1head0.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml1head0Comment1.NodeType);
            Assert.AreEqual(@" ", dochtml1head0Comment1.TextContent);

            var dochtml1head0style2 = dochtml1head0.ChildNodes[2];
            Assert.AreEqual(1, dochtml1head0style2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0style2.Attributes.Length);
            Assert.AreEqual("style", dochtml1head0style2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0style2.NodeType);

            var dochtml1head0style2Text0 = dochtml1head0style2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0style2Text0.NodeType);
            Assert.AreEqual("@import ...", dochtml1head0style2Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [TestMethod]
        public void StyleWithNestedElementAndComment()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><style>...<style><!--...</style><!-- --></style>");

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
            Assert.AreEqual(2, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0style0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0style0.Attributes.Length);
            Assert.AreEqual("style", dochtml1head0style0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0style0.NodeType);

            var dochtml1head0style0Text0 = dochtml1head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0style0Text0.NodeType);
            Assert.AreEqual("...<style><!--...", dochtml1head0style0Text0.TextContent);

            var dochtml1head0Comment1 = dochtml1head0.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml1head0Comment1.NodeType);
            Assert.AreEqual(@" ", dochtml1head0Comment1.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [TestMethod]
        public void StyleWithCommentInsideThatHostsIEConditional()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><style>...<!--[if IE]><style>...</style>X");

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
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0style0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0style0.Attributes.Length);
            Assert.AreEqual("style", dochtml1head0style0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0style0.NodeType);

            var dochtml1head0style0Text0 = dochtml1head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0style0Text0.NodeType);
            Assert.AreEqual("...<!--[if IE]><style>...", dochtml1head0style0Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1Text0.TextContent);
        }

        [TestMethod]
        public void TitleWithCommentInsideThatHostsAnotherTitlePair()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><title><!--<title></title>--></title>");

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
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0title0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0title0.Attributes.Length);
            Assert.AreEqual("title", dochtml1head0title0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0title0.NodeType);

            var dochtml1head0title0Text0 = dochtml1head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0title0Text0.NodeType);
            Assert.AreEqual("<!--<title>", dochtml1head0title0Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml1body1Text0.TextContent);
        }

        [TestMethod]
        public void TitleWithEntityThatIsWronglyClosed()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><title>&lt;/title></title>");

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
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0title0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0title0.Attributes.Length);
            Assert.AreEqual("title", dochtml1head0title0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0title0.NodeType);

            var dochtml1head0title0Text0 = dochtml1head0title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0title0Text0.NodeType);
            Assert.AreEqual("</title>", dochtml1head0title0Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [TestMethod]
        public void StyleWithCommentInsideThatContainsAnotherStylePair()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><style><!--<style></style>--></style>");

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
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0style0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0style0.Attributes.Length);
            Assert.AreEqual("style", dochtml1head0style0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0style0.NodeType);

            var dochtml1head0style0Text0 = dochtml1head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0style0Text0.NodeType);
            Assert.AreEqual("<!--<style>", dochtml1head0style0Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml1body1Text0.TextContent);
        }

        [TestMethod]
        public void StyleWithOpeningCommentAndClosedStyleInside()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><style><!--</style>X");

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
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1head0style0 = dochtml1head0.ChildNodes[0];
            Assert.AreEqual(1, dochtml1head0style0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0style0.Attributes.Length);
            Assert.AreEqual("style", dochtml1head0style0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0style0.NodeType);

            var dochtml1head0style0Text0 = dochtml1head0style0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0style0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml1head0style0Text0.TextContent);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1Text0.TextContent);
        }

    }
}
