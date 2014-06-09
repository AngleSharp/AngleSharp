using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;

namespace UnitTests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests (*)
    /// to be more specific: (*)/blob/master/tree-construction/tricky01.dat
    /// </summary>
    [TestClass]
    public class TrickyTests
    {
        [TestMethod]
        public void BoldAndNotBold()
        {
            var doc = DocumentBuilder.Html(@"<b><p>Bold </b> Not bold</p>
Also not bold.");

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

            var dochtmlbodyb = dochtmlbody.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodyb.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodyb.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyb.NodeType);

            var dochtmlbodyp = dochtmlbody.Childs[1] as Element;
            Assert.AreEqual(2, dochtmlbodyp.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyp.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyp.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyp.NodeType);

            var dochtmlbodypb = dochtmlbodyp.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodypb.Childs.Length);
            Assert.AreEqual(0, dochtmlbodypb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodypb.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodypb.NodeType);

            var text1 = dochtmlbodypb.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("Bold ", text1.TextContent);

            var text2 = dochtmlbodyp.Childs[1];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(" Not bold", text2.TextContent);

            var text3 = dochtmlbody.Childs[2];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("\nAlso not bold.", text3.TextContent);
        }

        [TestMethod]
        public void ItalicAndOrRed()
        {
            var doc = DocumentBuilder.Html(@"<html>
<font color=red><i>Italic and Red<p>Italic and Red </font> Just italic.</p> Italic only.</i> Plain
<p>I should not be red. <font color=red>Red. <i>Italic and red.</p>
<p>Italic and red. </i> Red.</font> I should not be red.</p>
<b>Bold <i>Bold and italic</b> Only Italic </i> Plain");

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
            Assert.AreEqual(10, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodyfont1 = dochtmlbody.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodyfont1.Childs.Length);
            Assert.AreEqual(1, dochtmlbodyfont1.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont1.NodeType);
            Assert.AreEqual("red", dochtmlbodyfont1.Attributes["color"].Value);

            var dochtmlbodyfonti1 = dochtmlbodyfont1.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodyfonti1.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyfonti1.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodyfonti1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfonti1.NodeType);

            var text1 = dochtmlbodyfonti1.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual(@"Italic and Red", text1.TextContent);

            var dochtmlbodyi1 = dochtmlbody.Childs[1] as Element;
            Assert.AreEqual(2, dochtmlbodyi1.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyi1.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodyi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyi1.NodeType);

            var dochtmlbodyip = dochtmlbodyi1.Childs[0] as Element;
            Assert.AreEqual(2, dochtmlbodyip.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyip.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyip.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyip.NodeType);

            var dochtmlbodyipfont = dochtmlbodyip.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodyipfont.Childs.Length);
            Assert.AreEqual(1, dochtmlbodyipfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyipfont.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyipfont.NodeType);
            Assert.AreEqual("red", dochtmlbodyipfont.Attributes["color"].Value);

            var text2 = dochtmlbodyipfont.Childs[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"Italic and Red ", text2.TextContent);

            var dochtmlbodyfont2 = dochtmlbody.Childs[4] as Element;
            Assert.AreEqual(1, dochtmlbodyfont2.Childs.Length);
            Assert.AreEqual(1, dochtmlbodyfont2.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont2.NodeType);
            Assert.AreEqual("red", dochtmlbodyfont2.Attributes["color"].Value);

            var dochtmlbodyfonti2 = dochtmlbodyfont2.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodyfonti2.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyfonti2.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodyfonti2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfonti2.NodeType);

            var text3 = dochtmlbodyfonti2.Childs[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("\n", text3.TextContent);

            var dochtmlbodyp = dochtmlbody.Childs[5] as Element;
            Assert.AreEqual(2, dochtmlbodyp.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyp.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyp.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyp.NodeType);

            var dochtmlbodypfont = dochtmlbodyp.Childs[0] as Element;
            Assert.AreEqual(2, dochtmlbodypfont.Childs.Length);
            Assert.AreEqual(1, dochtmlbodypfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodypfont.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodypfont.NodeType);
            Assert.AreEqual("red", dochtmlbodypfont.Attributes["color"].Value);

            var dochtmlbodypfonti = dochtmlbodypfont.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodypfonti.Childs.Length);
            Assert.AreEqual(0, dochtmlbodypfonti.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodypfonti.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodypfonti.NodeType);

            var text4 = dochtmlbodypfonti.Childs[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual(@"Italic and red. ", text4.TextContent);

            var text5 = dochtmlbodypfont.Childs[1];
            Assert.AreEqual(NodeType.Text, text5.NodeType);
            Assert.AreEqual(@" Red.", text5.TextContent);

            var text6 = dochtmlbodyp.Childs[1];
            Assert.AreEqual(NodeType.Text, text6.NodeType);
            Assert.AreEqual(@" I should not be red.", text6.TextContent);

            var text7 = dochtmlbody.Childs[6];
            Assert.AreEqual(NodeType.Text, text7.NodeType);
            Assert.AreEqual("\n", text7.TextContent);

            var dochtmlbodyb = dochtmlbody.Childs[7] as Element;
            Assert.AreEqual(2, dochtmlbodyb.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodyb.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyb.NodeType);

            var text8 = dochtmlbodyb.Childs[0];
            Assert.AreEqual(NodeType.Text, text8.NodeType);
            Assert.AreEqual(@"Bold ", text8.TextContent);

            var dochtmlbodybi = dochtmlbodyb.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodybi.Childs.Length);
            Assert.AreEqual(0, dochtmlbodybi.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodybi.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodybi.NodeType);

            var text9 = dochtmlbodybi.Childs[0];
            Assert.AreEqual(NodeType.Text, text9.NodeType);
            Assert.AreEqual(@"Bold and italic", text9.TextContent);

            var dochtmlbodyi3 = dochtmlbody.Childs[8] as Element;
            Assert.AreEqual(1, dochtmlbodyi3.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyi3.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodyi3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyi3.NodeType);

            var text10 = dochtmlbodyi3.Childs[0];
            Assert.AreEqual(NodeType.Text, text10.NodeType);
            Assert.AreEqual(@" Only Italic ", text10.TextContent);

            var text11 = dochtmlbody.Childs[9];
            Assert.AreEqual(NodeType.Text, text11.NodeType);
            Assert.AreEqual(@" Plain", text11.TextContent);
        }

        [TestMethod]
        public void FormattingParagraphs()
        {
            var doc = DocumentBuilder.Html(@"<html><body>
<p><font size=""7"">First paragraph.</p>
<p>Second paragraph.</p></font>
<b><p><i>Bold and Italic</b> Italic</p>");

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
            Assert.AreEqual(6, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text1 = dochtmlbody.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("\n", text1.TextContent);

            var dochtmlbodyp1 = dochtmlbody.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodyp1.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyp1.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyp1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyp1.NodeType);

            var dochtmlbodypfont = dochtmlbodyp1.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodypfont.Childs.Length);
            Assert.AreEqual(1, dochtmlbodypfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodypfont.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodypfont.NodeType);
            Assert.AreEqual("7", dochtmlbodypfont.Attributes["size"].Value);

            var text2 = dochtmlbodypfont.Childs[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"First paragraph.", text2.TextContent);

            var dochtmlbodyfont = dochtmlbody.Childs[2] as Element;
            Assert.AreEqual(2, dochtmlbodyfont.Childs.Length);
            Assert.AreEqual(1, dochtmlbodyfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont.NodeType);
            Assert.AreEqual("7", dochtmlbodyfont.Attributes["size"].Value);

            var text3 = dochtmlbodyfont.Childs[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("\n", text3.TextContent);

            var dochtmlbodyfontp = dochtmlbodyfont.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodyfontp.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyfontp.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyfontp.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfontp.NodeType);

            var text4 = dochtmlbodyfontp.Childs[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual(@"Second paragraph.", text4.TextContent);

            var text5 = dochtmlbody.Childs[3];
            Assert.AreEqual(NodeType.Text, text5.NodeType);
            Assert.AreEqual("\n", text5.TextContent);

            var dochtmlbodyb = dochtmlbody.Childs[4] as Element;
            Assert.AreEqual(0, dochtmlbodyb.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodyb.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyb.NodeType);

            var dochtmlbodyp2 = dochtmlbody.Childs[5] as Element;
            Assert.AreEqual(2, dochtmlbodyp2.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyp2.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyp2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyp2.NodeType);

            var dochtmlbodypb = dochtmlbodyp2.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodypb.Childs.Length);
            Assert.AreEqual(0, dochtmlbodypb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodypb.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodypb.NodeType);

            var dochtmlbodypbi = dochtmlbodypb.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodypbi.Childs.Length);
            Assert.AreEqual(0, dochtmlbodypbi.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodypbi.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodypbi.NodeType);

            var text6 = dochtmlbodypbi.Childs[0];
            Assert.AreEqual(NodeType.Text, text6.NodeType);
            Assert.AreEqual(@"Bold and Italic", text6.TextContent);

            var dochtmlbodypi = dochtmlbodyp2.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodypi.Childs.Length);
            Assert.AreEqual(0, dochtmlbodypi.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodypi.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodypi.NodeType);

            var text7 = dochtmlbodypi.Childs[0];
            Assert.AreEqual(NodeType.Text, text7.NodeType);
            Assert.AreEqual(@" Italic", text7.TextContent);
        }

        [TestMethod]
        public void DefinitionListWithFormatting()
        {
            var doc = DocumentBuilder.Html(@"<html>
<dl>
<dt><b>Boo
<dd>Goo?
</dl>
</html>");

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

            var dochtmlbodydl = dochtmlbody.Childs[0] as Element;
            Assert.AreEqual(3, dochtmlbodydl.Childs.Length);
            Assert.AreEqual(0, dochtmlbodydl.Attributes.Count);
            Assert.AreEqual("dl", dochtmlbodydl.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydl.NodeType);

            var text1 = dochtmlbodydl.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("\n", text1.TextContent);

            var dochtmlbodydldt = dochtmlbodydl.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodydldt.Childs.Length);
            Assert.AreEqual(0, dochtmlbodydldt.Attributes.Count);
            Assert.AreEqual("dt", dochtmlbodydldt.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydldt.NodeType);

            var dochtmlbodydldtb = dochtmlbodydldt.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodydldtb.Childs.Length);
            Assert.AreEqual(0, dochtmlbodydldtb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodydldtb.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydldtb.NodeType);

            var text2 = dochtmlbodydldtb.Childs[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual("Boo\n", text2.TextContent);

            var dochtmlbodydldd = dochtmlbodydl.Childs[2] as Element;
            Assert.AreEqual(1, dochtmlbodydldd.Childs.Length);
            Assert.AreEqual(0, dochtmlbodydldd.Attributes.Count);
            Assert.AreEqual("dd", dochtmlbodydldd.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydldd.NodeType);

            var dochtmlbodydlddb = dochtmlbodydldd.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodydlddb.Childs.Length);
            Assert.AreEqual(0, dochtmlbodydlddb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodydlddb.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydlddb.NodeType);

            var text3 = dochtmlbodydlddb.Childs[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("Goo?\n", text3.TextContent);

            var dochtmlbodyb = dochtmlbody.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodyb.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodyb.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyb.NodeType);

            var text4 = dochtmlbodyb.Childs[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual("\n", text4.TextContent);
        }

        [TestMethod]
        public void HelloWorldWithSomeDivs()
        {
            var doc = DocumentBuilder.Html(@"<html><body>
<label><a><div>Hello<div>World</div></a></label>  
</body></html>");

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

            var text1 = dochtmlbody.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("\n", text1.TextContent);

            var dochtmlbodylabel = dochtmlbody.Childs[1] as Element;
            Assert.AreEqual(2, dochtmlbodylabel.Childs.Length);
            Assert.AreEqual(0, dochtmlbodylabel.Attributes.Count);
            Assert.AreEqual("label", dochtmlbodylabel.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodylabel.NodeType);

            var dochtmlbodylabela = dochtmlbodylabel.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodylabela.Childs.Length);
            Assert.AreEqual(0, dochtmlbodylabela.Attributes.Count);
            Assert.AreEqual("a", dochtmlbodylabela.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodylabela.NodeType);

            var dochtmlbodylabeldiv = dochtmlbodylabel.Childs[1] as Element;
            Assert.AreEqual(2, dochtmlbodylabeldiv.Childs.Length);
            Assert.AreEqual(0, dochtmlbodylabeldiv.Attributes.Count);
            Assert.AreEqual("div", dochtmlbodylabeldiv.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodylabeldiv.NodeType);

            var dochtmlbodylabeldiva = dochtmlbodylabeldiv.Childs[0] as Element;
            Assert.AreEqual(2, dochtmlbodylabeldiva.Childs.Length);
            Assert.AreEqual(0, dochtmlbodylabeldiva.Attributes.Count);
            Assert.AreEqual("a", dochtmlbodylabeldiva.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodylabeldiva.NodeType);

            var text2 = dochtmlbodylabeldiva.Childs[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"Hello", text2.TextContent);

            var dochtmlbodylabeldivadiv = dochtmlbodylabeldiva.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodylabeldivadiv.Childs.Length);
            Assert.AreEqual(0, dochtmlbodylabeldivadiv.Attributes.Count);
            Assert.AreEqual("div", dochtmlbodylabeldivadiv.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodylabeldivadiv.NodeType);

            var text3 = dochtmlbodylabeldivadiv.Childs[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual(@"World", text3.TextContent);

            var text4 = dochtmlbodylabeldiv.Childs[1];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual("  \n", text4.TextContent);
        }

        [TestMethod]
        public void TableFormattingGoneWild()
        {
            var doc = DocumentBuilder.Html(@"<table><center> <font>a</center> <img> <tr><td> </td> </tr> </table>");

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

            var dochtmlbodycenter = dochtmlbody.Childs[0] as Element;
            Assert.AreEqual(2, dochtmlbodycenter.Childs.Length);
            Assert.AreEqual(0, dochtmlbodycenter.Attributes.Count);
            Assert.AreEqual("center", dochtmlbodycenter.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodycenter.NodeType);

            var text1 = dochtmlbodycenter.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual(@" ", text1.TextContent);

            var dochtmlbodycenterfont = dochtmlbodycenter.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodycenterfont.Childs.Length);
            Assert.AreEqual(0, dochtmlbodycenterfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodycenterfont.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodycenterfont.NodeType);

            var text2 = dochtmlbodycenterfont.Childs[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"a", text2.TextContent);

            var dochtmlbodyfont = dochtmlbody.Childs[1] as Element;
            Assert.AreEqual(2, dochtmlbodyfont.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont.NodeType);

            var dochtmlbodyfontimg = dochtmlbodyfont.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodyfontimg.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyfontimg.Attributes.Count);
            Assert.AreEqual("img", dochtmlbodyfontimg.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfontimg.NodeType);

            var text3 = dochtmlbodyfont.Childs[1];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual(@" ", text3.TextContent);

            var dochtmlbodytable = dochtmlbody.Childs[2] as Element;
            Assert.AreEqual(2, dochtmlbodytable.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytable.Attributes.Count);
            Assert.AreEqual("table", dochtmlbodytable.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytable.NodeType);

            var text4 = dochtmlbodytable.Childs[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual(@" ", text4.TextContent);

            var dochtmlbodytabletbody = dochtmlbodytable.Childs[1] as Element;
            Assert.AreEqual(2, dochtmlbodytabletbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytabletbody.Attributes.Count);
            Assert.AreEqual("tbody", dochtmlbodytabletbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbody.NodeType);

            var dochtmlbodytabletbodytr = dochtmlbodytabletbody.Childs[0] as Element;
            Assert.AreEqual(2, dochtmlbodytabletbodytr.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytr.Attributes.Count);
            Assert.AreEqual("tr", dochtmlbodytabletbodytr.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytr.NodeType);

            var dochtmlbodytabletbodytrtd = dochtmlbodytabletbodytr.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodytabletbodytrtd.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytrtd.Attributes.Count);
            Assert.AreEqual("td", dochtmlbodytabletbodytrtd.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytrtd.NodeType);

            var text5 = dochtmlbodytabletbodytrtd.Childs[0];
            Assert.AreEqual(NodeType.Text, text5.NodeType);
            Assert.AreEqual(@" ", text5.TextContent);

            var text6 = dochtmlbodytabletbodytr.Childs[1];
            Assert.AreEqual(NodeType.Text, text6.NodeType);
            Assert.AreEqual(@" ", text6.TextContent);

            var text7 = dochtmlbodytabletbody.Childs[1];
            Assert.AreEqual(NodeType.Text, text7.NodeType);
            Assert.AreEqual(@" ", text7.TextContent);
        }

        [TestMethod]
        public void YouShouldSeeThisText()
        {
            var doc = DocumentBuilder.Html(@"<table><tr><p><a><p>You should see this text.");

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

            var dochtmlbodyp1 = dochtmlbody.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodyp1.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyp1.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyp1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyp1.NodeType);

            var dochtmlbodypa1 = dochtmlbodyp1.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodypa1.Childs.Length);
            Assert.AreEqual(0, dochtmlbodypa1.Attributes.Count);
            Assert.AreEqual("a", dochtmlbodypa1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodypa1.NodeType);

            var dochtmlbodyp2 = dochtmlbody.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodyp2.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyp2.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyp2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyp2.NodeType);

            var dochtmlbodypa2 = dochtmlbodyp2.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodypa2.Childs.Length);
            Assert.AreEqual(0, dochtmlbodypa2.Attributes.Count);
            Assert.AreEqual("a", dochtmlbodypa2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodypa2.NodeType);

            var text1 = dochtmlbodypa2.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual(@"You should see this text.", text1.TextContent);

            var dochtmlbodytable = dochtmlbody.Childs[2] as Element;
            Assert.AreEqual(1, dochtmlbodytable.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytable.Attributes.Count);
            Assert.AreEqual("table", dochtmlbodytable.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytable.NodeType);

            var dochtmlbodytabletbody = dochtmlbodytable.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodytabletbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytabletbody.Attributes.Count);
            Assert.AreEqual("tbody", dochtmlbodytabletbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbody.NodeType);

            var dochtmlbodytabletbodytr = dochtmlbodytabletbody.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodytabletbodytr.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytr.Attributes.Count);
            Assert.AreEqual("tr", dochtmlbodytabletbodytr.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytr.NodeType);

        }

        [TestMethod]
        public void InsanelyBadlyNestedTagSequence()
        {
            var doc = DocumentBuilder.Html(@"<TABLE>
<TR>
<CENTER><CENTER><TD></TD></TR><TR>
<FONT>
<TABLE><tr></tr></TABLE>
</P>
<a></font><font></a>
This page contains an insanely badly-nested tag sequence.");

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
            Assert.AreEqual(7, dochtmlbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodycenter = dochtmlbody.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodycenter.Childs.Length);
            Assert.AreEqual(0, dochtmlbodycenter.Attributes.Count);
            Assert.AreEqual("center", dochtmlbodycenter.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodycenter.NodeType);

            var dochtmlbodycentercenter = dochtmlbodycenter.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodycentercenter.Childs.Length);
            Assert.AreEqual(0, dochtmlbodycentercenter.Attributes.Count);
            Assert.AreEqual("center", dochtmlbodycentercenter.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodycentercenter.NodeType);

            var dochtmlbodyfont = dochtmlbody.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodyfont.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont.NodeType);

            var text1 = dochtmlbodyfont.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("\n", text1.TextContent);

            var dochtmlbodytable = dochtmlbody.Childs[2] as Element;
            Assert.AreEqual(2, dochtmlbodytable.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytable.Attributes.Count);
            Assert.AreEqual("table", dochtmlbodytable.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytable.NodeType);

            var text2 = dochtmlbodytable.Childs[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual("\n", text2.TextContent);

            var dochtmlbodytabletbody1 = dochtmlbodytable.Childs[1] as Element;
            Assert.AreEqual(2, dochtmlbodytabletbody1.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytabletbody1.Attributes.Count);
            Assert.AreEqual("tbody", dochtmlbodytabletbody1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbody1.NodeType);

            var dochtmlbodytabletbodytr1 = dochtmlbodytabletbody1.Childs[0] as Element;
            Assert.AreEqual(2, dochtmlbodytabletbodytr1.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytr1.Attributes.Count);
            Assert.AreEqual("tr", dochtmlbodytabletbodytr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytr1.NodeType);

            var text3 = dochtmlbodytabletbodytr1.Childs[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("\n", text3.TextContent);

            var dochtmlbodytabletbodytrtd = dochtmlbodytabletbodytr1.Childs[1] as Element;
            Assert.AreEqual(0, dochtmlbodytabletbodytrtd.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytrtd.Attributes.Count);
            Assert.AreEqual("td", dochtmlbodytabletbodytrtd.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytrtd.NodeType);

            var dochtmlbodytabletbodytr2 = dochtmlbodytabletbody1.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodytabletbodytr2.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytr2.Attributes.Count);
            Assert.AreEqual("tr", dochtmlbodytabletbodytr2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytr2.NodeType);

            var text4 = dochtmlbodytabletbodytr2.Childs[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual("\n", text4.TextContent);

            var dochtmlbodytable2 = dochtmlbody.Childs[3] as Element;
            Assert.AreEqual(1, dochtmlbodytable2.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytable2.Attributes.Count);
            Assert.AreEqual("table", dochtmlbodytable2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytable2.NodeType);

            var dochtmlbodytabletbody = dochtmlbodytable2.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodytabletbody.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytabletbody.Attributes.Count);
            Assert.AreEqual("tbody", dochtmlbodytabletbody.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbody.NodeType);

            var dochtmlbodytabletbodytr = dochtmlbodytabletbody.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodytabletbodytr.Childs.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytr.Attributes.Count);
            Assert.AreEqual("tr", dochtmlbodytabletbodytr.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytr.NodeType);

            var dochtmlbodyfont1 = dochtmlbody.Childs[4] as Element;
            Assert.AreEqual(4, dochtmlbodyfont1.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyfont1.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont1.NodeType);

            var text5 = dochtmlbodyfont1.Childs[0];
            Assert.AreEqual(NodeType.Text, text5.NodeType);
            Assert.AreEqual("\n", text5.TextContent);

            var dochtmlbodyfontp = dochtmlbodyfont1.Childs[1] as Element;
            Assert.AreEqual(0, dochtmlbodyfontp.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyfontp.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyfontp.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfontp.NodeType);

            var text6 = dochtmlbodyfont1.Childs[2];
            Assert.AreEqual(NodeType.Text, text6.NodeType);
            Assert.AreEqual("\n", text6.TextContent);

            var dochtmlbodyfonta = dochtmlbodyfont1.Childs[3] as Element;
            Assert.AreEqual(0, dochtmlbodyfonta.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyfonta.Attributes.Count);
            Assert.AreEqual("a", dochtmlbodyfonta.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfonta.NodeType);

            var dochtmlbodya = dochtmlbody.Childs[5] as Element;
            Assert.AreEqual(1, dochtmlbodya.Childs.Length);
            Assert.AreEqual(0, dochtmlbodya.Attributes.Count);
            Assert.AreEqual("a", dochtmlbodya.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodya.NodeType);

            var dochtmlbodyafont = dochtmlbodya.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodyafont.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyafont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyafont.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyafont.NodeType);

            var dochtmlbodyfont2 = dochtmlbody.Childs[6] as Element;
            Assert.AreEqual(1, dochtmlbodyfont2.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyfont2.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont2.NodeType);

            var text7 = dochtmlbodyfont2.Childs[0];
            Assert.AreEqual(NodeType.Text, text7.NodeType);
            Assert.AreEqual("\nThis page contains an insanely badly-nested tag sequence.", text7.TextContent);
        }

        [TestMethod]
        public void ImplicitlyClosingDivs()
        {
            var doc = DocumentBuilder.Html(@"<html>
<body>
<b><nobr><div>This text is in a div inside a nobr</nobr>More text that should not be in the nobr, i.e., the
nobr should have closed the div inside it implicitly. </b><pre>A pre tag outside everything else.</pre>
</body>
</html>");

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

            var text1 = dochtmlbody.Childs[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("\n", text1.TextContent);

            var dochtmlbodyb = dochtmlbody.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodyb.Childs.Length);
            Assert.AreEqual(0, dochtmlbodyb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodyb.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodyb.NodeType);

            var dochtmlbodybnobr = dochtmlbodyb.Childs[0] as Element;
            Assert.AreEqual(0, dochtmlbodybnobr.Childs.Length);
            Assert.AreEqual(0, dochtmlbodybnobr.Attributes.Count);
            Assert.AreEqual("nobr", dochtmlbodybnobr.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodybnobr.NodeType);

            var dochtmlbodydiv = dochtmlbody.Childs[2] as Element;
            Assert.AreEqual(3, dochtmlbodydiv.Childs.Length);
            Assert.AreEqual(0, dochtmlbodydiv.Attributes.Count);
            Assert.AreEqual("div", dochtmlbodydiv.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydiv.NodeType);

            var dochtmlbodydivb = dochtmlbodydiv.Childs[0] as Element;
            Assert.AreEqual(2, dochtmlbodydivb.Childs.Length);
            Assert.AreEqual(0, dochtmlbodydivb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodydivb.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydivb.NodeType);

            var dochtmlbodydivbnobr = dochtmlbodydivb.Childs[0] as Element;
            Assert.AreEqual(1, dochtmlbodydivbnobr.Childs.Length);
            Assert.AreEqual(0, dochtmlbodydivbnobr.Attributes.Count);
            Assert.AreEqual("nobr", dochtmlbodydivbnobr.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydivbnobr.NodeType);

            var text2 = dochtmlbodydivbnobr.Childs[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"This text is in a div inside a nobr", text2.TextContent);

            var text3 = dochtmlbodydivb.Childs[1];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("More text that should not be in the nobr, i.e., the\nnobr should have closed the div inside it implicitly. ", text3.TextContent);

            var dochtmlbodydivpre = dochtmlbodydiv.Childs[1] as Element;
            Assert.AreEqual(1, dochtmlbodydivpre.Childs.Length);
            Assert.AreEqual(0, dochtmlbodydivpre.Attributes.Count);
            Assert.AreEqual("pre", dochtmlbodydivpre.NodeName);
            Assert.AreEqual(NodeType.Element, dochtmlbodydivpre.NodeType);

            var text4 = dochtmlbodydivpre.Childs[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual(@"A pre tag outside everything else.", text4.TextContent);

            var text5 = dochtmlbodydiv.Childs[2];
            Assert.AreEqual(NodeType.Text, text5.NodeType);
            Assert.AreEqual("\n\n", text5.TextContent);
        }
    }
}
