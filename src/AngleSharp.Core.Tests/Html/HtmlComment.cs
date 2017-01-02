namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/comments01.dat
    /// </summary>
    [TestFixture]
    public class HtmlCommentTests
    {
        [Test]
        public void ValidCommentInText()
        {
            var doc = (@"FOO<!-- BAR -->BAZ").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@" BAR ", dochtml0body1Comment1.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAZ", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ToleratedComment()
        {
            var doc = (@"FOO<!-- BAR --!>BAZ").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@" BAR ", dochtml0body1Comment1.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAZ", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void InvalidComment()
        {
            var doc = (@"FOO<!-- BAR --   >BAZ").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@" BAR --   >BAZ", dochtml0body1Comment1.TextContent);
        }

        [Test]
        public void ValidCommentWithTagInside()
        {
            var doc = (@"FOO<!-- BAR -- <QUX> -- MUX -->BAZ").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@" BAR -- <QUX> -- MUX ", dochtml0body1Comment1.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAZ", dochtml0body1Text2.TextContent);

        }

        [Test]
        public void ToleratedCommentWithTagInside()
        {
            var doc = (@"FOO<!-- BAR -- <QUX> -- MUX --!>BAZ").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@" BAR -- <QUX> -- MUX ", dochtml0body1Comment1.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAZ", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void InvalidCommentWithTagInside()
        {
            var doc = (@"FOO<!-- BAR -- <QUX> -- MUX -- >BAZ").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@" BAR -- <QUX> -- MUX -- >BAZ", dochtml0body1Comment1.TextContent);
        }

        [Test]
        public void ToleratedInvalidEmptyComment4Dashes()
        {
            var doc = (@"FOO<!---->BAZ").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@"", dochtml0body1Comment1.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAZ", dochtml0body1Text2.TextContent);
        }

        [Test]
        public void ToleratedInvalidEmptyComment3Dashes()
        {
            var doc = (@"FOO<!--->BAZ").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@"", dochtml0body1Comment1.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAZ", dochtml0body1Text2.TextContent);

        }

        [Test]
        public void ToleratedInvalidEmptyComment2Dashes()
        {
            var doc = (@"FOO<!-->BAZ").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@"", dochtml0body1Comment1.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAZ", dochtml0body1Text2.TextContent);


        }

        [Test]
        public void XmlPreambleAsBogusCommentFollowedByText()
        {
            var doc = (@"<?xml version=""1.0"">Hi").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"?xml version=""1.0""", docComment0.TextContent);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hi", dochtml1body1Text0.TextContent);
        }

        [Test]
        public void XmlPreambleAsBogusCommentStandalone()
        {
            var doc = (@"<?xml version=""1.0"">").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"?xml version=""1.0""", docComment0.TextContent);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

        }

        [Test]
        public void XmlPreambleFragmentWithEOF()
        {
            var doc = (@"<?xml version").ToHtmlDocument();

            var docComment0 = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, docComment0.NodeType);
            Assert.AreEqual(@"?xml version", docComment0.TextContent);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }

        [Test]
        public void ToleratedInvalidEmptyComment5Dashes()
        {
            var doc = (@"FOO<!----->BAZ").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("FOO", dochtml0body1Text0.TextContent);

            var dochtml0body1Comment1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml0body1Comment1.NodeType);
            Assert.AreEqual(@"-", dochtml0body1Comment1.TextContent);

            var dochtml0body1Text2 = dochtml0body1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text2.NodeType);
            Assert.AreEqual("BAZ", dochtml0body1Text2.TextContent);

        }

        [Test]
        public void ValidCommentInRoot()
        {
            var doc = (@"<html><!-- comment --><title>Comment before head</title>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0Comment0 = dochtml0.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, dochtml0Comment0.NodeType);
            Assert.AreEqual(@" comment ", dochtml0Comment0.TextContent);

            var dochtml0head1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0head1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head1.Attributes.Length);
            Assert.AreEqual("head", dochtml0head1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head1.NodeType);

            var dochtml0head1title0 = dochtml0head1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head1title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head1title0.Attributes.Length);
            Assert.AreEqual("title", dochtml0head1title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head1title0.NodeType);

            var dochtml0head1title0Text0 = dochtml0head1title0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head1title0Text0.NodeType);
            Assert.AreEqual("Comment before head", dochtml0head1title0Text0.TextContent);

            var dochtml0body2 = dochtml0.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml0body2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body2.Attributes.Length);
            Assert.AreEqual("body", dochtml0body2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body2.NodeType);

        }

        [Test]
        public void MassiveCommentInNBCPage()
        {
            try
            {
                var doc = (Assets.nbc).ToHtmlDocument();
                Assert.IsNotNull(doc);
                Assert.AreEqual(1152, doc.QuerySelectorAll("*").Length);
            }
            catch (StackOverflowException)
            {
                Assert.Fail("The parsing resulted in a stackoverflow.");
            }
        }

        [Test]
        public void CommentInHtmlCausesException()
        {
            var source = "<!DOCTYPE html><body><!---------------------GA--------------------------- --></body></html>";
            var doc = (source).ToHtmlDocument();
            Assert.IsNotNull(doc);
        }

        [Test]
        public void CommentAtTheEndOfXHtmlDocumentShouldNotCauseException()
        {
            var source = @"<html xmlns=""http://www.w3.org/1999/xhtml"">
<head></head>
<body></body>
</html>
<!-- Comment -->";
            var document = source.ToHtmlDocument();
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.ToHtml());
        }
    }
}
