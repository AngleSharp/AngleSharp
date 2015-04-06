using System;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using AngleSharp.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/domjs-unsafe.dat
    /// </summary>
    [TestFixture]
    public class UserJsUnsafeTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void Html5LibSvgCdata()
        {
            var doc = Html(@"<svg><![CDATA[foo
bar]]>");
            var html = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, html.ChildNodes.Length);
            Assert.AreEqual(0, html.Attributes.Count);
            Assert.AreEqual(NodeType.Element, html.NodeType);

            var htmlhead = html.ChildNodes[0] as Element;
            Assert.AreEqual(0, htmlhead.ChildNodes.Length);
            Assert.AreEqual(0, htmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, htmlhead.NodeType);

            var htmlbody = html.ChildNodes[1] as Element;
            Assert.AreEqual(1, htmlbody.ChildNodes.Length);
            Assert.AreEqual(0, htmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, htmlbody.NodeType);

            var htmlbodysvg = htmlbody.ChildNodes[0] as Element;
            Assert.IsTrue(htmlbodysvg.Flags.HasFlag(NodeFlags.SvgMember));
            Assert.AreEqual(Namespaces.SvgUri, htmlbodysvg.NamespaceUri);
            Assert.AreEqual(1, htmlbodysvg.ChildNodes.Length);
            Assert.AreEqual(0, htmlbodysvg.Attributes.Count);
            Assert.AreEqual(NodeType.Element, htmlbodysvg.NodeType);

            var text = htmlbodysvg.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("foo\nbar", text.TextContent);
        }

        [Test]
        public void Html5LibScriptDataCommentStarted()
        {
            var doc = Html(@"<script type=""data""><!--foo" + Symbols.Null.ToString() + "</script>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlheadscript.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes.Get("type").Value);

            var text = dochtmlheadscript.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!--foo" + Symbols.Replacement.ToString(), text.TextContent);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void Html5LibScriptDataCommentFinishing()
        {
            var doc = Html(@"<script type=""data""><!-- foo--" + Symbols.Null.ToString() + "</script>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlheadscript.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes.Get("type").Value);

            var text = dochtmlheadscript.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!-- foo--" + Symbols.Replacement.ToString(), text.TextContent);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void Html5LibScriptDataEnding()
        {
            var doc = Html(@"<script type=""data""><!-- foo-<</script>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlheadscript.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes.Get("type").Value);

            var text = dochtmlheadscript.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!-- foo-<", text.TextContent);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void Html5LibScriptDataParagraph()
        {
            var doc = Html(@"<script type=""data""><!--<p></script>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlheadscript.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes.Get("type").Value);

            var text = dochtmlheadscript.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!--<p>", text.TextContent);
                        
            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void Html5LibDoctypeInHeadImplicit()
        {
            var doc = Html(@"<html><!DOCTYPE html>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [Test]
        public void Html5LibDoctypeInBodyImplicit()
        {
            var doc = Html(@"<html><head></head><!DOCTYPE html>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

        }
    }
}
