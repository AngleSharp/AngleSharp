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

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodydivdiv = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(0, dochtmlbodydivdiv.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydivdiv.Attributes.Length);
            Assert.AreEqual("div<div", dochtmlbodydivdiv.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydivdiv.NodeType);
        }

        [TestMethod]
        public void WrongDivAttributeMistake()
        {
            var doc = DocumentBuilder.Html(@"<div foo<bar=''>");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodydiv = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(0, dochtmlbodydiv.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlbodydiv.Attributes.Length);
            Assert.AreEqual("div", dochtmlbodydiv.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydiv.NodeType);
            Assert.AreEqual("", dochtmlbodydiv.Attributes["foo<bar"].Value);
        }

        [TestMethod]
        public void WrongDivLetterInAttributeMistake()
        {
            var doc = DocumentBuilder.Html(@"<div foo=`bar`>");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodydiv = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(0, dochtmlbodydiv.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlbodydiv.Attributes.Length);
            Assert.AreEqual("div", dochtmlbodydiv.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydiv.NodeType);
            Assert.AreEqual("`bar`", dochtmlbodydiv.Attributes["foo"].Value);
        }

        [TestMethod]
        public void EntitiesAngles()
        {
            var doc = DocumentBuilder.Html(@"&lang;&rang;");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"⟨⟩", text.TextContent);
        }

        [TestMethod]
        public void EntitiesApos()
        {
            var doc = DocumentBuilder.Html(@"&apos;");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"'", text.TextContent);
        }

        [TestMethod]
        public void EntitiesKopf()
        {
            var doc = DocumentBuilder.Html(@"&Kopf;");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"𝕂", text.TextContent);
        }

        [TestMethod]
        public void EntitiesNotinva()
        {
            var doc = DocumentBuilder.Html(@"&notinva;");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text.NodeType);
            Assert.AreEqual(@"∉", text.TextContent);
        }

        [TestMethod]
        public void BogusCommentAsDoctype()
        {
            var doc = DocumentBuilder.Html(@"<?import namespace=""foo"" implementation=""#bar"">");

            var comment = doc.ChildNodes[0];
            Assert.AreEqual(NodeType.Comment, comment.NodeType);
            Assert.AreEqual(@"?import namespace=""foo"" implementation=""#bar""", comment.TextContent);

            var dochtml = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void MisplacedCdataSection()
        {
            var doc = DocumentBuilder.Html(@"<![CDATA[x]]>");
            var cdata = doc.ChildNodes[0];
            Assert.AreEqual(0, cdata.ChildNodes.Length);
            Assert.AreEqual(0, cdata.Attributes.Length);
            Assert.AreEqual("[CDATA[x]]", cdata.TextContent);
            Assert.AreEqual(NodeType.Comment, cdata.NodeType);

            var dochtml = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(0, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);
        }

        [TestMethod]
        public void TextAreaWithComments()
        {
            var doc = DocumentBuilder.Html(@"<textarea><!--</textarea>--></textarea>");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(2, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodytextarea = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(1, dochtmlbodytextarea.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytextarea.Attributes.Length);
            Assert.AreEqual("textarea", dochtmlbodytextarea.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytextarea.NodeType);

            var text1 = dochtmlbodytextarea.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual(@"<!--", text1.TextContent);

            var text2 = dochtmlbody.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"-->", text2.TextContent);
        }

        [TestMethod]
        public void UnsortedListWithEntries()
        {
            var doc = DocumentBuilder.Html(@"<ul><li>A </li> <li>B</li></ul>");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodyul = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(3, dochtmlbodyul.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyul.Attributes.Length);
            Assert.AreEqual("ul", dochtmlbodyul.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyul.NodeType);

            var dochtmlbodyulli1 = dochtmlbodyul.ChildNodes[0];
            Assert.AreEqual(1, dochtmlbodyulli1.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyulli1.Attributes.Length);
            Assert.AreEqual("li", dochtmlbodyulli1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyulli1.NodeType);

            var text1 = dochtmlbodyulli1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual(@"A ", text1.TextContent);

            var text2 = dochtmlbodyul.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@" ", text2.TextContent);

            var dochtmlbodyulli2 = dochtmlbodyul.ChildNodes[2];
            Assert.AreEqual(1, dochtmlbodyulli2.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyulli2.Attributes.Length);
            Assert.AreEqual("li", dochtmlbodyulli2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyulli2.NodeType);

            var text3 = dochtmlbodyulli2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual(@"B", text3.TextContent);
        }

        [TestMethod]
        public void TableWithFormAndInputs()
        {
            var doc = DocumentBuilder.Html(@"<table><form><input type=hidden><input></form><div></div></table>");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(3, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodyinput = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(0, dochtmlbodyinput.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyinput.Attributes.Length);
            Assert.AreEqual("input", dochtmlbodyinput.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyinput.NodeType);

            var dochtmlbodydiv = dochtmlbody.ChildNodes[1];
            Assert.AreEqual(0, dochtmlbodydiv.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydiv.Attributes.Length);
            Assert.AreEqual("div", dochtmlbodydiv.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydiv.NodeType);

            var dochtmlbodytable = dochtmlbody.ChildNodes[2];
            Assert.AreEqual(2, dochtmlbodytable.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytable.Attributes.Length);
            Assert.AreEqual("table", dochtmlbodytable.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytable.NodeType);

            var dochtmlbodytableform = dochtmlbodytable.ChildNodes[0];
            Assert.AreEqual(0, dochtmlbodytableform.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytableform.Attributes.Length);
            Assert.AreEqual("form", dochtmlbodytableform.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytableform.NodeType);

            var dochtmlbodytableinput = dochtmlbodytable.ChildNodes[1];
            Assert.AreEqual(0, dochtmlbodytableinput.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlbodytableinput.Attributes.Length);
            Assert.AreEqual("input", dochtmlbodytableinput.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytableinput.NodeType);
            Assert.AreEqual("hidden", dochtmlbodytableinput.Attributes["type"].Value);
        }

        [TestMethod]
        public void MathMLTag()
        {
            var doc = DocumentBuilder.Html(@"<math></math>");

            var dochtml = doc.ChildNodes[0];
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Length);
            Assert.AreEqual("html", dochtml.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);
            var dochtmlhead = dochtml.ChildNodes[0];
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Length);
            Assert.AreEqual("head", dochtmlhead.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1];
            Assert.AreEqual(1, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Length);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodymath = dochtmlbody.ChildNodes[0];
            Assert.IsTrue(dochtmlbodymath.IsInMathML);
            Assert.AreEqual(Namespaces.MathML, dochtmlbodymath.NamespaceURI);
            Assert.AreEqual(0, dochtmlbodymath.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodymath.Attributes.Length);
            Assert.AreEqual("math", dochtmlbodymath.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodymath.NodeType);
        }
    }
}
