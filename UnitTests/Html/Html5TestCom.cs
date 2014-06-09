using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp.DOM;
using AngleSharp;

namespace UnitTests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests (*)
    /// to be more specific: (*)/blob/master/tree-construction/html5test-com.dat
    /// </summary>
    [TestClass]
    public class Html5TestComTests
    {
        [TestMethod]
        public void WrongDivTagMistake()
        {
            var doc = DocumentBuilder.Html(@"<div<div>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodydivdiv = dochtmlbody.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodydivdiv.Childs.Length);
            Assert.AreEqual(0, dochtmlbodydivdiv.Attributes.Count);
            Assert.AreEqual("div<div", dochtmlbodydivdiv.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydivdiv.NodeType);
        }

        [TestMethod]
        public void WrongDivAttributeMistake()
        {
            var doc = DocumentBuilder.Html(@"<div foo<bar=''>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodydiv = dochtmlbody.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodydiv.Childs.Length);
            Assert.AreEqual(1, dochtmlbodydiv.Attributes.Count);
            Assert.AreEqual("div", dochtmlbodydiv.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydiv.NodeType);
            Assert.AreEqual("", dochtmlbodydiv.Attributes["foo<bar"].Value);
        }

        [TestMethod]
        public void WrongDivLetterInAttributeMistake()
        {
            var doc = DocumentBuilder.Html(@"<div foo=`bar`>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodydiv = dochtmlbody.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodydiv.Childs.Length);
            Assert.AreEqual(1, dochtmlbodydiv.Attributes.Count);
            Assert.AreEqual("div", dochtmlbodydiv.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydiv.NodeType);
            Assert.AreEqual("`bar`", dochtmlbodydiv.Attributes["foo"].Value);
        }

        [TestMethod]
        public void EntitiesAngles()
        {
            var doc = DocumentBuilder.Html(@"&lang;&rang;");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text = dochtmlbody.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"⟨⟩", text.TextContent);
        }

        [TestMethod]
        public void EntitiesApos()
        {
            var doc = DocumentBuilder.Html(@"&apos;");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text = dochtmlbody.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"'", text.TextContent);
        }

        [TestMethod]
        public void EntitiesKopf()
        {
            var doc = DocumentBuilder.Html(@"&Kopf;");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text = dochtmlbody.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"𝕂", text.TextContent);
        }

        [TestMethod]
        public void EntitiesNotinva()
        {
            var doc = DocumentBuilder.Html(@"&notinva;");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text = dochtmlbody.Childs[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"∉", text.TextContent);
        }

        [TestMethod]
        public void BogusCommentAsDoctype()
        {
            var doc = DocumentBuilder.Html(@"<?import namespace=""foo"" implementation=""#bar"">");

            var comment = doc.Childs[0];
            Assert.AreEqual(NodeType.Comment, comment.NodeType);
            Assert.AreEqual(@"?import namespace=""foo"" implementation=""#bar""", comment.TextContent);

            var dochtml = doc.Childs[1] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(0, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void MisplacedCdataSection()
        {
            var doc = DocumentBuilder.Html(@"<![CDATA[x]]>");
            var cdata = doc.Childs[0];
            Assert.AreEqual(0, cdata.Childs.Length);
            Assert.AreEqual("[CDATA[x]]", cdata.TextContent);
            Assert.AreEqual(NodeType.Comment, cdata.NodeType);

            var dochtml = doc.Childs[1] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(0, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void TextAreaWithComments()
        {
            var doc = DocumentBuilder.Html(@"<textarea><!--</textarea>--></textarea>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(2, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodytextarea = dochtmlbody.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodytextarea.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytextarea.Attributes.Count);
            Assert.AreEqual("textarea", dochtmlbodytextarea.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytextarea.NodeType);

            var text1 = dochtmlbodytextarea.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual(@"<!--", text1.TextContent);

            var text2 = dochtmlbody.Childs[1];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"-->", text2.TextContent);
        }

        [TestMethod]
        public void UnsortedListWithEntries()
        {
            var doc = DocumentBuilder.Html(@"<ul><li>A </li> <li>B</li></ul>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodyul = dochtmlbody.Childs[0] as Element;
            Assert.AreEqual(3, dochtmlbodyul.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyul.Attributes.Count);
            Assert.AreEqual("ul", dochtmlbodyul.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyul.NodeType);

            var dochtmlbodyulli1 = dochtmlbodyul.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodyulli1.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyulli1.Attributes.Count);
            Assert.AreEqual("li", dochtmlbodyulli1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyulli1.NodeType);

            var text1 = dochtmlbodyulli1.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual(@"A ", text1.TextContent);

            var text2 = dochtmlbodyul.Childs[1];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@" ", text2.TextContent);

            var dochtmlbodyulli2 = dochtmlbodyul.Childs[2] as Element;
            Assert.AreEqual(1, dochtmlbodyulli2.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyulli2.Attributes.Count);
            Assert.AreEqual("li", dochtmlbodyulli2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyulli2.NodeType);

            var text3 = dochtmlbodyulli2.Childs[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual(@"B", text3.TextContent);
        }

        [TestMethod]
        public void TableWithFormAndInputs()
        {
            var doc = DocumentBuilder.Html(@"<table><form><input type=hidden><input></form><div></div></table>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(3, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodyinput = dochtmlbody.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodyinput.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyinput.Attributes.Count);
            Assert.AreEqual("input", dochtmlbodyinput.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyinput.NodeType);

            var dochtmlbodydiv = dochtmlbody.Childs[1] as Element;
            Assert.AreEqual(0, dochtmlbodydiv.Childs.Length);
            Assert.AreEqual(0, dochtmlbodydiv.Attributes.Count);
            Assert.AreEqual("div", dochtmlbodydiv.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydiv.NodeType);

            var dochtmlbodytable = dochtmlbody.Childs[2] as Element;
            Assert.AreEqual(2, dochtmlbodytable.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytable.Attributes.Count);
            Assert.AreEqual("table", dochtmlbodytable.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytable.NodeType);

            var dochtmlbodytableform = dochtmlbodytable.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodytableform.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytableform.Attributes.Count);
            Assert.AreEqual("form", dochtmlbodytableform.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytableform.NodeType);

            var dochtmlbodytableinput = dochtmlbodytable.Childs[1] as Element;
            Assert.AreEqual(0, dochtmlbodytableinput.Childs.Length);
            Assert.AreEqual(1, dochtmlbodytableinput.Attributes.Count);
            Assert.AreEqual("input", dochtmlbodytableinput.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytableinput.NodeType);
            Assert.AreEqual("hidden", dochtmlbodytableinput.Attributes["type"].Value);
        }

        [TestMethod]
        public void MathMLTag()
        {
            var doc = DocumentBuilder.Html(@"<math></math>");

            var dochtml = doc.Childs[0] as Element;
            Assert.AreEqual(2, dochtml.Childs.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);
            var dochtmlhead = dochtml.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlhead.Childs.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodymath = dochtmlbody.Childs[0] as Element;
            Assert.IsTrue(dochtmlbodymath.IsInMathML);
            Assert.AreEqual(Namespaces.MathML, dochtmlbodymath.NamespaceUri);
            Assert.AreEqual(0, dochtmlbodymath.Childs.Length);
            Assert.AreEqual(0, dochtmlbodymath.Attributes.Count);
            Assert.AreEqual("math", dochtmlbodymath.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodymath.NodeType);
        }
    }
}
