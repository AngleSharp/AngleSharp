using AngleSharp;
using AngleSharp.DOM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests (*)
    /// to be more specific: (*)/blob/master/tree-construction/domjs-unsafe.dat
    /// </summary>
    [TestClass]
    public class UserJsUnsafeTests
    {
        [TestMethod]
        public void Html5LibSvgCdata()
        {
            var doc = DocumentBuilder.Html(@"<svg><![CDATA[foo
bar]]>");
            var html = doc.Childs[0] as Element;
            Assert.AreEqual(2, html.Childs.Length);
            Assert.AreEqual(0, html.Attributes.Count);
            Assert.AreEqual(NodeType.Element, html.NodeType);

            var htmlhead = html.Childs[0] as Element;
            Assert.AreEqual(0, htmlhead.Childs.Length);
            Assert.AreEqual(0, htmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, htmlhead.NodeType);

            var htmlbody = html.Childs[1] as Element;
            Assert.AreEqual(1, htmlbody.Childs.Length);
            Assert.AreEqual(0, htmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, htmlbody.NodeType);

            var htmlbodysvg = htmlbody.Childs[0] as Element;
            Assert.IsTrue(htmlbodysvg.IsInSvg);
            Assert.AreEqual(Namespaces.Svg, htmlbodysvg.NamespaceUri);
            Assert.AreEqual(1, htmlbodysvg.Childs.Length);
            Assert.AreEqual(0, htmlbodysvg.Attributes.Count);
            Assert.AreEqual(NodeType.Element, htmlbodysvg.NodeType);

            var text = htmlbodysvg.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual("foo\nbar", text.TextContent);
        }

        [TestMethod]
        public void Html5LibScriptDataCommentStarted()
        {
            var doc = DocumentBuilder.Html(@"<script type=""data""><!--foo" + Specification.Null.ToString() + "</script>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlheadscript.Childs.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes["type"].Value);

            var text = dochtmlheadscript.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!--foo" + Specification.Replacement.ToString(), text.TextContent);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(0, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void Html5LibScriptDataCommentFinishing()
        {
            var doc = DocumentBuilder.Html(@"<script type=""data""><!-- foo--" + Specification.Null.ToString() + "</script>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlheadscript.Childs.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes["type"].Value);

            var text = dochtmlheadscript.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!-- foo--" + Specification.Replacement.ToString(), text.TextContent);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(0, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void Html5LibScriptDataEnding()
        {
            var doc = DocumentBuilder.Html(@"<script type=""data""><!-- foo-<</script>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlheadscript.Childs.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes["type"].Value);

            var text = dochtmlheadscript.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!-- foo-<", text.TextContent);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(0, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void Html5LibScriptDataParagraph()
        {
            var doc = DocumentBuilder.Html(@"<script type=""data""><!--<p></script>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlheadscript = dochtmlhead.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlheadscript.Childs.Length);
            Assert.AreEqual(1, dochtmlheadscript.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlheadscript.NodeType);
            Assert.AreEqual("data", dochtmlheadscript.Attributes["type"].Value);

            var text = dochtmlheadscript.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"<!--<p>", text.TextContent);
                        
            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(0, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void Html5LibDoctypeInHeadImplicit()
        {
            var doc = DocumentBuilder.Html(@"<html><!DOCTYPE html>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(0, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void Html5LibDoctypeInBodyImplicit()
        {
            var doc = DocumentBuilder.Html(@"<html><head></head><!DOCTYPE html>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(0, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

        }
    }
}
