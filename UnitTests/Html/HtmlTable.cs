using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;

namespace UnitTests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests (*)
    /// to be more specific: (*)/blob/master/tree-construction/tables01.dat
    /// </summary>
    [TestClass]
    public class HtmlTableTests
    {
        [TestMethod]
        public void TableWithSingleTh()
        {
            var doc = DocumentBuilder.Html(@"<table><th>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0th0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0th0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0th0.Attributes.Count);
            Assert.AreEqual("th", dochtml0body1table0tbody0tr0th0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0th0.NodeType);
        }

        [TestMethod]
        public void TableWithSingleTd()
        {
            var doc = DocumentBuilder.Html(@"<table><td>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);
        }

        [TestMethod]
        public void TableWithSingleCol()
        {
            var doc = DocumentBuilder.Html(@"<table><col foo='bar'>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0colgroup0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0colgroup0.Attributes.Count);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0.NodeType);

            var dochtml0body1table0colgroup0col0 = dochtml0body1table0colgroup0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0colgroup0col0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1table0colgroup0col0.Attributes.Count);
            Assert.AreEqual("col", dochtml0body1table0colgroup0col0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0col0.NodeType);
            Assert.AreEqual("bar", dochtml0body1table0colgroup0col0.Attributes["foo"].Value);
        }

        [TestMethod]
        public void TableWithSingleColgroupClosingHtml()
        {
            var doc = DocumentBuilder.Html(@"<table><colgroup></html>foo");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1Text0 = dochtml0body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table1colgroup0 = dochtml0body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table1colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1colgroup0.Attributes.Count);
            Assert.AreEqual("colgroup", dochtml0body1table1colgroup0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1colgroup0.NodeType);
        }

        [TestMethod]
        public void TableClosedFollowedByParagraph()
        {
            var doc = DocumentBuilder.Html(@"<table></table><p>foo");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1Text0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1p1Text0.TextContent);
        }

        [TestMethod]
        public void TableClosingEveryhingUnorderedOpenedTd()
        {
            var doc = DocumentBuilder.Html(@"<table></body></caption></col></colgroup></html></tbody></td></tfoot></th></thead></tr><td>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);
        }

        [TestMethod]
        public void TableWithSelectOption()
        {
            var doc = DocumentBuilder.Html(@"<table><select><option>3</select></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0option0 = dochtml0body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0option0.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1select0option0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option0.NodeType);

            var dochtml0body1select0option0Text0 = dochtml0body1select0option0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1select0option0Text0.NodeType);
            Assert.AreEqual("3", dochtml0body1select0option0Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);
        }

        [TestMethod]
        public void TableWithSelectAndTable()
        {
            var doc = DocumentBuilder.Html(@"<table><select><table></table></select></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table2 = dochtml0body1.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml0body1table2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table2.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table2.NodeType);
        }

        [TestMethod]
        public void TableWithSelectClosed()
        {
            var doc = DocumentBuilder.Html(@"<table><select></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);
        }

        [TestMethod]
        public void TableWithSelectOptionAndContent()
        {
            var doc = DocumentBuilder.Html(@"<table><select><option>A<tr><td>B</td></tr></table>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml0body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0option0 = dochtml0body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0option0.Attributes.Count);
            Assert.AreEqual("option", dochtml0body1select0option0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option0.NodeType);

            var dochtml0body1select0option0Text0 = dochtml0body1select0option0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1select0option0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1select0option0Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table1tbody0 = dochtml0body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table1tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0.NodeType);

            var dochtml0body1table1tbody0tr0 = dochtml0body1table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table1tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0.NodeType);

            var dochtml0body1table1tbody0tr0td0 = dochtml0body1table1tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table1tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0td0.NodeType);

            var dochtml0body1table1tbody0tr0td0Text0 = dochtml0body1table1tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table1tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1table1tbody0tr0td0Text0.TextContent);
        }

        [TestMethod]
        public void TableWithTdClosedEverything()
        {
            var doc = DocumentBuilder.Html(@"<table><td></body></caption></col></colgroup></html>foo");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0Text0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1table0tbody0tr0td0Text0.TextContent);
        }

        [TestMethod]
        public void TableWithCellContent()
        {
            var doc = DocumentBuilder.Html(@"<table><td>A</table>B");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0Text0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1table0tbody0tr0td0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("B", dochtml0body1Text1.TextContent);
        }

        [TestMethod]
        public void TableWithRowAndCaption()
        {
            var doc = DocumentBuilder.Html(@"<table><tr><caption>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0caption1 = dochtml0body1table0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table0caption1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0caption1.Attributes.Count);
            Assert.AreEqual("caption", dochtml0body1table0caption1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0caption1.NodeType);
        }

        [TestMethod]
        public void TableWithRowClosedEverythingOpenedTd()
        {
            var doc = DocumentBuilder.Html(@"<table><tr></body></caption></col></colgroup></html></td></th><td>foo");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0Text0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1table0tbody0tr0td0Text0.TextContent);
        }

        [TestMethod]
        public void TableWithTdAndTr()
        {
            var doc = DocumentBuilder.Html(@"<table><td><tr>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr1 = dochtml0body1table0tbody0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr1.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr1.NodeType);
        }

        [TestMethod]
        public void TableWithTdButtonAndTd()
        {
            var doc = DocumentBuilder.Html(@"<table><td><button><td>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0button0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0button0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0button0.Attributes.Count);
            Assert.AreEqual("button", dochtml0body1table0tbody0tr0td0button0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0button0.NodeType);

            var dochtml0body1table0tbody0tr0td1 = dochtml0body1table0tbody0tr0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td1.NodeType);
        }

        [TestMethod]
        public void TableWithRowCellAndSvgDescTd()
        {
            var doc = DocumentBuilder.Html(@"<table><tr><td><svg><desc><td>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0svg0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1table0tbody0tr0td0svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0svg0.NodeType);

            var dochtml0body1table0tbody0tr0td0svg0desc0 = dochtml0body1table0tbody0tr0td0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0svg0desc0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0svg0desc0.Attributes.Count);
            Assert.AreEqual("desc", dochtml0body1table0tbody0tr0td0svg0desc0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0svg0desc0.NodeType);

            var dochtml0body1table0tbody0tr0td1 = dochtml0body1table0tbody0tr0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td1.Attributes.Count);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td1.NodeType);
        }

        [TestMethod]
        public void TableWithTableRowThatHasStyle()
        {
            var doc = DocumentBuilder.Html(@"<table><tr style=""display: none;"">");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var styleAttribute = dochtml0body1table0tbody0tr0.Attributes[0];
            Assert.AreEqual("style", styleAttribute.Name);
            Assert.AreEqual("display: none;", styleAttribute.Value);

            var style = ((IHtmlElement)dochtml0body1table0tbody0tr0).Style;
            Assert.AreEqual("none", style.Display);
        }

        [TestMethod]
        public void TableWithTableRowThatHasStyleAndChanged()
        {
            var doc = DocumentBuilder.Html(@"<table><tr style=""display: none;"">");

            var html = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, html.ChildNodes.Length);
            Assert.AreEqual(0, html.Attributes.Count);
            Assert.AreEqual("html", html.NodeName);
            Assert.AreEqual(NodeType.Element, html.NodeType);

            var body = html.ChildNodes[1] as Element;
            Assert.AreEqual(1, body.ChildNodes.Length);
            Assert.AreEqual(0, body.Attributes.Count);
            Assert.AreEqual("body", body.NodeName);
            Assert.AreEqual(NodeType.Element, body.NodeType);

            var table = body.ChildNodes[0] as Element;
            Assert.AreEqual(1, table.ChildNodes.Length);
            Assert.AreEqual(0, table.Attributes.Count);
            Assert.AreEqual("table", table.NodeName);
            Assert.AreEqual(NodeType.Element, table.NodeType);

            var tableBody = table.ChildNodes[0] as Element;
            Assert.AreEqual(1, tableBody.ChildNodes.Length);
            Assert.AreEqual(0, tableBody.Attributes.Count);
            Assert.AreEqual("tbody", tableBody.NodeName);
            Assert.AreEqual(NodeType.Element, tableBody.NodeType);

            var tableRow = tableBody.ChildNodes[0] as Element;
            Assert.AreEqual(0, tableRow.ChildNodes.Length);
            Assert.AreEqual(1, tableRow.Attributes.Count);
            Assert.AreEqual("tr", tableRow.NodeName);
            Assert.AreEqual(NodeType.Element, tableRow.NodeType);

            var tr = (IHtmlElement)tableRow;
            var style = tr.Style;
            Assert.AreEqual("none", style.Display);

            tr.Style.Display = "block";
            Assert.AreEqual("block", tr.Style.Display);
        }

        [TestMethod]
        public void TableWithTableRowThatHasNoStyleAndChanged()
        {
            var doc = DocumentBuilder.Html(@"<table><tr>");

            var html = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, html.ChildNodes.Length);
            Assert.AreEqual(0, html.Attributes.Count);
            Assert.AreEqual("html", html.NodeName);
            Assert.AreEqual(NodeType.Element, html.NodeType);

            var body = html.ChildNodes[1] as Element;
            Assert.AreEqual(1, body.ChildNodes.Length);
            Assert.AreEqual(0, body.Attributes.Count);
            Assert.AreEqual("body", body.NodeName);
            Assert.AreEqual(NodeType.Element, body.NodeType);

            var table = body.ChildNodes[0] as Element;
            Assert.AreEqual(1, table.ChildNodes.Length);
            Assert.AreEqual(0, table.Attributes.Count);
            Assert.AreEqual("table", table.NodeName);
            Assert.AreEqual(NodeType.Element, table.NodeType);

            var tableBody = table.ChildNodes[0] as Element;
            Assert.AreEqual(1, tableBody.ChildNodes.Length);
            Assert.AreEqual(0, tableBody.Attributes.Count);
            Assert.AreEqual("tbody", tableBody.NodeName);
            Assert.AreEqual(NodeType.Element, tableBody.NodeType);

            var tableRow = tableBody.ChildNodes[0] as Element;
            Assert.AreEqual(0, tableRow.ChildNodes.Length);
            Assert.AreEqual(0, tableRow.Attributes.Count);
            Assert.AreEqual("tr", tableRow.NodeName);
            Assert.AreEqual(NodeType.Element, tableRow.NodeType);

            var tr = (IHtmlElement)tableRow;

            tr.Style.Display = "none";
            Assert.AreEqual("none", tr.Style.Display);
        }
    }
}
