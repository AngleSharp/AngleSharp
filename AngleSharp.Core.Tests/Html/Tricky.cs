using AngleSharp.Dom;
using AngleSharp.Extensions;
using NUnit.Framework;
using System;
using System.IO;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tricky01.dat
    /// </summary>
    [TestFixture]
    public class TrickyTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void BoldAndNotBold()
        {
            var doc = Html(@"<b><p>Bold </b> Not bold</p>
Also not bold.");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodyb = dochtmlbody.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodyb.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodyb.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyb.NodeType);

            var dochtmlbodyp = dochtmlbody.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtmlbodyp.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyp.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyp.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyp.NodeType);

            var dochtmlbodypb = dochtmlbodyp.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodypb.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodypb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodypb.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodypb.NodeType);

            var text1 = dochtmlbodypb.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("Bold ", text1.TextContent);

            var text2 = dochtmlbodyp.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(" Not bold", text2.TextContent);

            var text3 = dochtmlbody.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("\nAlso not bold.", text3.TextContent);
        }

        [Test]
        public void ItalicAndOrRed()
        {
            var doc = Html(@"<html>
<font color=red><i>Italic and Red<p>Italic and Red </font> Just italic.</p> Italic only.</i> Plain
<p>I should not be red. <font color=red>Red. <i>Italic and red.</p>
<p>Italic and red. </i> Red.</font> I should not be red.</p>
<b>Bold <i>Bold and italic</b> Only Italic </i> Plain");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(10, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodyfont1 = dochtmlbody.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodyfont1.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlbodyfont1.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont1.NodeType);
            Assert.AreEqual("red", dochtmlbodyfont1.Attributes.Get("color").Value);

            var dochtmlbodyfonti1 = dochtmlbodyfont1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodyfonti1.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyfonti1.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodyfonti1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfonti1.NodeType);

            var text1 = dochtmlbodyfonti1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual(@"Italic and Red", text1.TextContent);

            var dochtmlbodyi1 = dochtmlbody.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtmlbodyi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyi1.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodyi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyi1.NodeType);

            var dochtmlbodyip = dochtmlbodyi1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtmlbodyip.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyip.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyip.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyip.NodeType);

            var dochtmlbodyipfont = dochtmlbodyip.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodyipfont.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlbodyipfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyipfont.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyipfont.NodeType);
            Assert.AreEqual("red", dochtmlbodyipfont.Attributes.Get("color").Value);

            var text2 = dochtmlbodyipfont.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"Italic and Red ", text2.TextContent);

            var dochtmlbodyfont2 = dochtmlbody.ChildNodes[4] as Element;
            Assert.AreEqual(1, dochtmlbodyfont2.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlbodyfont2.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont2.NodeType);
            Assert.AreEqual("red", dochtmlbodyfont2.Attributes.Get("color").Value);

            var dochtmlbodyfonti2 = dochtmlbodyfont2.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodyfonti2.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyfonti2.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodyfonti2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfonti2.NodeType);

            var text3 = dochtmlbodyfonti2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("\n", text3.TextContent);

            var dochtmlbodyp = dochtmlbody.ChildNodes[5] as Element;
            Assert.AreEqual(2, dochtmlbodyp.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyp.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyp.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyp.NodeType);

            var dochtmlbodypfont = dochtmlbodyp.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtmlbodypfont.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlbodypfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodypfont.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodypfont.NodeType);
            Assert.AreEqual("red", dochtmlbodypfont.Attributes.Get("color").Value);

            var dochtmlbodypfonti = dochtmlbodypfont.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodypfonti.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodypfonti.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodypfonti.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodypfonti.NodeType);

            var text4 = dochtmlbodypfonti.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual(@"Italic and red. ", text4.TextContent);

            var text5 = dochtmlbodypfont.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, text5.NodeType);
            Assert.AreEqual(@" Red.", text5.TextContent);

            var text6 = dochtmlbodyp.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, text6.NodeType);
            Assert.AreEqual(@" I should not be red.", text6.TextContent);

            var text7 = dochtmlbody.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, text7.NodeType);
            Assert.AreEqual("\n", text7.TextContent);

            var dochtmlbodyb = dochtmlbody.ChildNodes[7] as Element;
            Assert.AreEqual(2, dochtmlbodyb.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodyb.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyb.NodeType);

            var text8 = dochtmlbodyb.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text8.NodeType);
            Assert.AreEqual(@"Bold ", text8.TextContent);

            var dochtmlbodybi = dochtmlbodyb.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodybi.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodybi.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodybi.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodybi.NodeType);

            var text9 = dochtmlbodybi.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text9.NodeType);
            Assert.AreEqual(@"Bold and italic", text9.TextContent);

            var dochtmlbodyi3 = dochtmlbody.ChildNodes[8] as Element;
            Assert.AreEqual(1, dochtmlbodyi3.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyi3.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodyi3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyi3.NodeType);

            var text10 = dochtmlbodyi3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text10.NodeType);
            Assert.AreEqual(@" Only Italic ", text10.TextContent);

            var text11 = dochtmlbody.ChildNodes[9];
            Assert.AreEqual(NodeType.Text, text11.NodeType);
            Assert.AreEqual(@" Plain", text11.TextContent);
        }

        [Test]
        public void FormattingParagraphs()
        {
            var doc = Html(@"<html><body>
<p><font size=""7"">First paragraph.</p>
<p>Second paragraph.</p></font>
<b><p><i>Bold and Italic</b> Italic</p>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(6, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text1 = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("\n", text1.TextContent);

            var dochtmlbodyp1 = dochtmlbody.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodyp1.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyp1.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyp1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyp1.NodeType);

            var dochtmlbodypfont = dochtmlbodyp1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodypfont.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlbodypfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodypfont.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodypfont.NodeType);
            Assert.AreEqual("7", dochtmlbodypfont.Attributes.Get("size").Value);

            var text2 = dochtmlbodypfont.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"First paragraph.", text2.TextContent);

            var dochtmlbodyfont = dochtmlbody.ChildNodes[2] as Element;
            Assert.AreEqual(2, dochtmlbodyfont.ChildNodes.Length);
            Assert.AreEqual(1, dochtmlbodyfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont.NodeType);
            Assert.AreEqual("7", dochtmlbodyfont.Attributes.Get("size").Value);

            var text3 = dochtmlbodyfont.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("\n", text3.TextContent);

            var dochtmlbodyfontp = dochtmlbodyfont.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodyfontp.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyfontp.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyfontp.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfontp.NodeType);

            var text4 = dochtmlbodyfontp.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual(@"Second paragraph.", text4.TextContent);

            var text5 = dochtmlbody.ChildNodes[3];
            Assert.AreEqual(NodeType.Text, text5.NodeType);
            Assert.AreEqual("\n", text5.TextContent);

            var dochtmlbodyb = dochtmlbody.ChildNodes[4] as Element;
            Assert.AreEqual(0, dochtmlbodyb.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodyb.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyb.NodeType);

            var dochtmlbodyp2 = dochtmlbody.ChildNodes[5] as Element;
            Assert.AreEqual(2, dochtmlbodyp2.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyp2.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyp2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyp2.NodeType);

            var dochtmlbodypb = dochtmlbodyp2.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodypb.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodypb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodypb.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodypb.NodeType);

            var dochtmlbodypbi = dochtmlbodypb.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodypbi.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodypbi.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodypbi.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodypbi.NodeType);

            var text6 = dochtmlbodypbi.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text6.NodeType);
            Assert.AreEqual(@"Bold and Italic", text6.TextContent);

            var dochtmlbodypi = dochtmlbodyp2.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodypi.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodypi.Attributes.Count);
            Assert.AreEqual("i", dochtmlbodypi.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodypi.NodeType);

            var text7 = dochtmlbodypi.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text7.NodeType);
            Assert.AreEqual(@" Italic", text7.TextContent);
        }

        [Test]
        public void DefinitionListWithFormatting()
        {
            var doc = Html(@"<html>
<dl>
<dt><b>Boo
<dd>Goo?
</dl>
</html>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodydl = dochtmlbody.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtmlbodydl.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydl.Attributes.Count);
            Assert.AreEqual("dl", dochtmlbodydl.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydl.NodeType);

            var text1 = dochtmlbodydl.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("\n", text1.TextContent);

            var dochtmlbodydldt = dochtmlbodydl.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodydldt.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydldt.Attributes.Count);
            Assert.AreEqual("dt", dochtmlbodydldt.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydldt.NodeType);

            var dochtmlbodydldtb = dochtmlbodydldt.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodydldtb.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydldtb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodydldtb.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydldtb.NodeType);

            var text2 = dochtmlbodydldtb.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual("Boo\n", text2.TextContent);

            var dochtmlbodydldd = dochtmlbodydl.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtmlbodydldd.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydldd.Attributes.Count);
            Assert.AreEqual("dd", dochtmlbodydldd.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydldd.NodeType);

            var dochtmlbodydlddb = dochtmlbodydldd.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodydlddb.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydlddb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodydlddb.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydlddb.NodeType);

            var text3 = dochtmlbodydlddb.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("Goo?\n", text3.TextContent);

            var dochtmlbodyb = dochtmlbody.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodyb.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodyb.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyb.NodeType);

            var text4 = dochtmlbodyb.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual("\n", text4.TextContent);
        }

        [Test]
        public void HelloWorldWithSomeDivs()
        {
            var doc = Html(@"<html><body>
<label><a><div>Hello<div>World</div></a></label>  
</body></html>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text1 = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("\n", text1.TextContent);

            var dochtmlbodylabel = dochtmlbody.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtmlbodylabel.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodylabel.Attributes.Count);
            Assert.AreEqual("label", dochtmlbodylabel.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodylabel.NodeType);

            var dochtmlbodylabela = dochtmlbodylabel.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodylabela.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodylabela.Attributes.Count);
            Assert.AreEqual("a", dochtmlbodylabela.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodylabela.NodeType);

            var dochtmlbodylabeldiv = dochtmlbodylabel.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtmlbodylabeldiv.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodylabeldiv.Attributes.Count);
            Assert.AreEqual("div", dochtmlbodylabeldiv.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodylabeldiv.NodeType);

            var dochtmlbodylabeldiva = dochtmlbodylabeldiv.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtmlbodylabeldiva.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodylabeldiva.Attributes.Count);
            Assert.AreEqual("a", dochtmlbodylabeldiva.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodylabeldiva.NodeType);

            var text2 = dochtmlbodylabeldiva.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"Hello", text2.TextContent);

            var dochtmlbodylabeldivadiv = dochtmlbodylabeldiva.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodylabeldivadiv.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodylabeldivadiv.Attributes.Count);
            Assert.AreEqual("div", dochtmlbodylabeldivadiv.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodylabeldivadiv.NodeType);

            var text3 = dochtmlbodylabeldivadiv.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual(@"World", text3.TextContent);

            var text4 = dochtmlbodylabeldiv.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual("  \n", text4.TextContent);
        }

        [Test]
        public void TableFormattingGoneWild()
        {
            var doc = Html(@"<table><center> <font>a</center> <img> <tr><td> </td> </tr> </table>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodycenter = dochtmlbody.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtmlbodycenter.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodycenter.Attributes.Count);
            Assert.AreEqual("center", dochtmlbodycenter.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodycenter.NodeType);

            var text1 = dochtmlbodycenter.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual(@" ", text1.TextContent);

            var dochtmlbodycenterfont = dochtmlbodycenter.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodycenterfont.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodycenterfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodycenterfont.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodycenterfont.NodeType);

            var text2 = dochtmlbodycenterfont.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"a", text2.TextContent);

            var dochtmlbodyfont = dochtmlbody.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtmlbodyfont.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont.NodeType);

            var dochtmlbodyfontimg = dochtmlbodyfont.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodyfontimg.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyfontimg.Attributes.Count);
            Assert.AreEqual("img", dochtmlbodyfontimg.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfontimg.NodeType);

            var text3 = dochtmlbodyfont.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual(@" ", text3.TextContent);

            var dochtmlbodytable = dochtmlbody.ChildNodes[2] as Element;
            Assert.AreEqual(2, dochtmlbodytable.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytable.Attributes.Count);
            Assert.AreEqual("table", dochtmlbodytable.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytable.NodeType);

            var text4 = dochtmlbodytable.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual(@" ", text4.TextContent);

            var dochtmlbodytabletbody = dochtmlbodytable.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtmlbodytabletbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytabletbody.Attributes.Count);
            Assert.AreEqual("tbody", dochtmlbodytabletbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbody.NodeType);

            var dochtmlbodytabletbodytr = dochtmlbodytabletbody.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtmlbodytabletbodytr.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytr.Attributes.Count);
            Assert.AreEqual("tr", dochtmlbodytabletbodytr.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytr.NodeType);

            var dochtmlbodytabletbodytrtd = dochtmlbodytabletbodytr.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodytabletbodytrtd.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytrtd.Attributes.Count);
            Assert.AreEqual("td", dochtmlbodytabletbodytrtd.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytrtd.NodeType);

            var text5 = dochtmlbodytabletbodytrtd.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text5.NodeType);
            Assert.AreEqual(@" ", text5.TextContent);

            var text6 = dochtmlbodytabletbodytr.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, text6.NodeType);
            Assert.AreEqual(@" ", text6.TextContent);

            var text7 = dochtmlbodytabletbody.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, text7.NodeType);
            Assert.AreEqual(@" ", text7.TextContent);
        }

        [Test]
        public void YouShouldSeeThisText()
        {
            var doc = Html(@"<table><tr><p><a><p>You should see this text.");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodyp1 = dochtmlbody.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodyp1.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyp1.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyp1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyp1.NodeType);

            var dochtmlbodypa1 = dochtmlbodyp1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodypa1.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodypa1.Attributes.Count);
            Assert.AreEqual("a", dochtmlbodypa1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodypa1.NodeType);

            var dochtmlbodyp2 = dochtmlbody.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodyp2.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyp2.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyp2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyp2.NodeType);

            var dochtmlbodypa2 = dochtmlbodyp2.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodypa2.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodypa2.Attributes.Count);
            Assert.AreEqual("a", dochtmlbodypa2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodypa2.NodeType);

            var text1 = dochtmlbodypa2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual(@"You should see this text.", text1.TextContent);

            var dochtmlbodytable = dochtmlbody.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtmlbodytable.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytable.Attributes.Count);
            Assert.AreEqual("table", dochtmlbodytable.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytable.NodeType);

            var dochtmlbodytabletbody = dochtmlbodytable.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodytabletbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytabletbody.Attributes.Count);
            Assert.AreEqual("tbody", dochtmlbodytabletbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbody.NodeType);

            var dochtmlbodytabletbodytr = dochtmlbodytabletbody.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodytabletbodytr.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytr.Attributes.Count);
            Assert.AreEqual("tr", dochtmlbodytabletbodytr.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytr.NodeType);

        }

        [Test]
        public void InsanelyBadlyNestedTagSequence()
        {
            var doc = Html(@"<TABLE>
<TR>
<CENTER><CENTER><TD></TD></TR><TR>
<FONT>
<TABLE><tr></tr></TABLE>
</P>
<a></font><font></a>
This page contains an insanely badly-nested tag sequence.");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(7, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var dochtmlbodycenter = dochtmlbody.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodycenter.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodycenter.Attributes.Count);
            Assert.AreEqual("center", dochtmlbodycenter.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodycenter.NodeType);

            var dochtmlbodycentercenter = dochtmlbodycenter.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodycentercenter.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodycentercenter.Attributes.Count);
            Assert.AreEqual("center", dochtmlbodycentercenter.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodycentercenter.NodeType);

            var dochtmlbodyfont = dochtmlbody.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodyfont.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyfont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont.NodeType);

            var text1 = dochtmlbodyfont.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("\n", text1.TextContent);

            var dochtmlbodytable = dochtmlbody.ChildNodes[2] as Element;
            Assert.AreEqual(2, dochtmlbodytable.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytable.Attributes.Count);
            Assert.AreEqual("table", dochtmlbodytable.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytable.NodeType);

            var text2 = dochtmlbodytable.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual("\n", text2.TextContent);

            var dochtmlbodytabletbody1 = dochtmlbodytable.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtmlbodytabletbody1.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytabletbody1.Attributes.Count);
            Assert.AreEqual("tbody", dochtmlbodytabletbody1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbody1.NodeType);

            var dochtmlbodytabletbodytr1 = dochtmlbodytabletbody1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtmlbodytabletbodytr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytr1.Attributes.Count);
            Assert.AreEqual("tr", dochtmlbodytabletbodytr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytr1.NodeType);

            var text3 = dochtmlbodytabletbodytr1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("\n", text3.TextContent);

            var dochtmlbodytabletbodytrtd = dochtmlbodytabletbodytr1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbodytabletbodytrtd.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytrtd.Attributes.Count);
            Assert.AreEqual("td", dochtmlbodytabletbodytrtd.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytrtd.NodeType);

            var dochtmlbodytabletbodytr2 = dochtmlbodytabletbody1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodytabletbodytr2.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytr2.Attributes.Count);
            Assert.AreEqual("tr", dochtmlbodytabletbodytr2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytr2.NodeType);

            var text4 = dochtmlbodytabletbodytr2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual("\n", text4.TextContent);

            var dochtmlbodytable2 = dochtmlbody.ChildNodes[3] as Element;
            Assert.AreEqual(1, dochtmlbodytable2.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytable2.Attributes.Count);
            Assert.AreEqual("table", dochtmlbodytable2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytable2.NodeType);

            var dochtmlbodytabletbody = dochtmlbodytable2.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodytabletbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytabletbody.Attributes.Count);
            Assert.AreEqual("tbody", dochtmlbodytabletbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbody.NodeType);

            var dochtmlbodytabletbodytr = dochtmlbodytabletbody.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodytabletbodytr.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodytabletbodytr.Attributes.Count);
            Assert.AreEqual("tr", dochtmlbodytabletbodytr.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodytabletbodytr.NodeType);

            var dochtmlbodyfont1 = dochtmlbody.ChildNodes[4] as Element;
            Assert.AreEqual(4, dochtmlbodyfont1.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyfont1.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont1.NodeType);

            var text5 = dochtmlbodyfont1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text5.NodeType);
            Assert.AreEqual("\n", text5.TextContent);

            var dochtmlbodyfontp = dochtmlbodyfont1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtmlbodyfontp.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyfontp.Attributes.Count);
            Assert.AreEqual("p", dochtmlbodyfontp.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfontp.NodeType);

            var text6 = dochtmlbodyfont1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, text6.NodeType);
            Assert.AreEqual("\n", text6.TextContent);

            var dochtmlbodyfonta = dochtmlbodyfont1.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtmlbodyfonta.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyfonta.Attributes.Count);
            Assert.AreEqual("a", dochtmlbodyfonta.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfonta.NodeType);

            var dochtmlbodya = dochtmlbody.ChildNodes[5] as Element;
            Assert.AreEqual(1, dochtmlbodya.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodya.Attributes.Count);
            Assert.AreEqual("a", dochtmlbodya.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodya.NodeType);

            var dochtmlbodyafont = dochtmlbodya.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodyafont.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyafont.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyafont.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyafont.NodeType);

            var dochtmlbodyfont2 = dochtmlbody.ChildNodes[6] as Element;
            Assert.AreEqual(1, dochtmlbodyfont2.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyfont2.Attributes.Count);
            Assert.AreEqual("font", dochtmlbodyfont2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyfont2.NodeType);

            var text7 = dochtmlbodyfont2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text7.NodeType);
            Assert.AreEqual("\nThis page contains an insanely badly-nested tag sequence.", text7.TextContent);
        }

        [Test]
        public void ImplicitlyClosingDivs()
        {
            var doc = Html(@"<html>
<body>
<b><nobr><div>This text is in a div inside a nobr</nobr>More text that should not be in the nobr, i.e., the
nobr should have closed the div inside it implicitly. </b><pre>A pre tag outside everything else.</pre>
</body>
</html>");

            var dochtml = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml.ChildNodes.Length);
            Assert.AreEqual(0, dochtml.Attributes.Count);
            Assert.AreEqual("html", dochtml.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml.NodeType);

            var dochtmlhead = dochtml.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlhead.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlhead.Attributes.Count);
            Assert.AreEqual("head", dochtmlhead.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlhead.NodeType);

            var dochtmlbody = dochtml.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtmlbody.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbody.Attributes.Count);
            Assert.AreEqual("body", dochtmlbody.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbody.NodeType);

            var text1 = dochtmlbody.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text1.NodeType);
            Assert.AreEqual("\n", text1.TextContent);

            var dochtmlbodyb = dochtmlbody.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodyb.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodyb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodyb.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodyb.NodeType);

            var dochtmlbodybnobr = dochtmlbodyb.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtmlbodybnobr.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodybnobr.Attributes.Count);
            Assert.AreEqual("nobr", dochtmlbodybnobr.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodybnobr.NodeType);

            var dochtmlbodydiv = dochtmlbody.ChildNodes[2] as Element;
            Assert.AreEqual(3, dochtmlbodydiv.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydiv.Attributes.Count);
            Assert.AreEqual("div", dochtmlbodydiv.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydiv.NodeType);

            var dochtmlbodydivb = dochtmlbodydiv.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtmlbodydivb.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydivb.Attributes.Count);
            Assert.AreEqual("b", dochtmlbodydivb.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydivb.NodeType);

            var dochtmlbodydivbnobr = dochtmlbodydivb.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtmlbodydivbnobr.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydivbnobr.Attributes.Count);
            Assert.AreEqual("nobr", dochtmlbodydivbnobr.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydivbnobr.NodeType);

            var text2 = dochtmlbodydivbnobr.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text2.NodeType);
            Assert.AreEqual(@"This text is in a div inside a nobr", text2.TextContent);

            var text3 = dochtmlbodydivb.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, text3.NodeType);
            Assert.AreEqual("More text that should not be in the nobr, i.e., the\nnobr should have closed the div inside it implicitly. ", text3.TextContent);

            var dochtmlbodydivpre = dochtmlbodydiv.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtmlbodydivpre.ChildNodes.Length);
            Assert.AreEqual(0, dochtmlbodydivpre.Attributes.Count);
            Assert.AreEqual("pre", dochtmlbodydivpre.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtmlbodydivpre.NodeType);

            var text4 = dochtmlbodydivpre.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, text4.NodeType);
            Assert.AreEqual(@"A pre tag outside everything else.", text4.TextContent);

            var text5 = dochtmlbodydiv.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, text5.NodeType);
            Assert.AreEqual("\n\n", text5.TextContent);
        }

        [Test]
        public void HtmlDomConsturctionFromBytesOnlyZerosLeadsToInfiniteLoop()
        {
            var bs = new Byte[5509];

            using (var memoryStream = new MemoryStream(bs, false))
            {
                var document = memoryStream.ToHtmlDocument();
                Assert.IsNotNull(document);
            }
        }
    }
}
