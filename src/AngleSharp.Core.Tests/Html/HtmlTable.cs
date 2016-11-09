namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tables01.dat
    /// </summary>
    [TestFixture]
    public class HtmlTableTests
    {
        [Test]
        public void TableWithSingleTh()
        {
            var doc = (@"<table><th>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0th0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0th0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0th0.Attributes.Length);
            Assert.AreEqual("th", dochtml0body1table0tbody0tr0th0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0th0.NodeType);
        }

        [Test]
        public void TableWithSingleTd()
        {
            var doc = (@"<table><td>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);
        }

        [Test]
        public void TableWithSingleCol()
        {
            var doc = (@"<table><col foo='bar'>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0colgroup0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0colgroup0.Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table0colgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0.NodeType);

            var dochtml0body1table0colgroup0col0 = dochtml0body1table0colgroup0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0colgroup0col0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml0body1table0colgroup0col0.Attributes.Length);
            Assert.AreEqual("col", dochtml0body1table0colgroup0col0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0colgroup0col0.NodeType);
            Assert.AreEqual("bar", dochtml0body1table0colgroup0col0.Attributes.GetNamedItem("foo").Value);
        }

        [Test]
        public void TableWithSingleColgroupClosingHtml()
        {
            var doc = (@"<table><colgroup></html>foo").ToHtmlDocument();

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
            Assert.AreEqual("foo", dochtml0body1Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table1colgroup0 = dochtml0body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table1colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1colgroup0.Attributes.Length);
            Assert.AreEqual("colgroup", dochtml0body1table1colgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1colgroup0.NodeType);
        }

        [Test]
        public void TableClosedFollowedByParagraph()
        {
            var doc = (@"<table></table><p>foo").ToHtmlDocument();

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

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1p1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1p1.NodeType);

            var dochtml0body1p1Text0 = dochtml0body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1p1Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1p1Text0.TextContent);
        }

        [Test]
        public void TableClosingEveryhingUnorderedOpenedTd()
        {
            var doc = (@"<table></body></caption></col></colgroup></html></tbody></td></tfoot></th></thead></tr><td>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);
        }

        [Test]
        public void TableWithSelectOption()
        {
            var doc = (@"<table><select><option>3</select></table>").ToHtmlDocument();

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

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Length);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0option0 = dochtml0body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0option0.Attributes.Length);
            Assert.AreEqual("option", dochtml0body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option0.NodeType);

            var dochtml0body1select0option0Text0 = dochtml0body1select0option0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1select0option0Text0.NodeType);
            Assert.AreEqual("3", dochtml0body1select0option0Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);
        }

        [Test]
        public void TableWithSelectAndTable()
        {
            var doc = (@"<table><select><table></table></select></table>").ToHtmlDocument();

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

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Length);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table2 = dochtml0body1.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml0body1table2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table2.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table2.NodeType);
        }

        [Test]
        public void TableWithSelectClosed()
        {
            var doc = (@"<table><select></table>").ToHtmlDocument();

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

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Length);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);
        }

        [Test]
        public void TableWithSelectOptionAndContent()
        {
            var doc = (@"<table><select><option>A<tr><td>B</td></tr></table>").ToHtmlDocument();

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

            var dochtml0body1select0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0.Attributes.Length);
            Assert.AreEqual("select", dochtml0body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0.NodeType);

            var dochtml0body1select0option0 = dochtml0body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1select0option0.Attributes.Length);
            Assert.AreEqual("option", dochtml0body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1select0option0.NodeType);

            var dochtml0body1select0option0Text0 = dochtml0body1select0option0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1select0option0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1select0option0Text0.TextContent);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table1tbody0 = dochtml0body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0.NodeType);

            var dochtml0body1table1tbody0tr0 = dochtml0body1table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0.NodeType);

            var dochtml0body1table1tbody0tr0td0 = dochtml0body1table1tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table1tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table1tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0td0.NodeType);

            var dochtml0body1table1tbody0tr0td0Text0 = dochtml0body1table1tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table1tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("B", dochtml0body1table1tbody0tr0td0Text0.TextContent);
        }

        [Test]
        public void TableWithTdClosedEverything()
        {
            var doc = (@"<table><td></body></caption></col></colgroup></html>foo").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0Text0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1table0tbody0tr0td0Text0.TextContent);
        }

        [Test]
        public void TableWithCellContent()
        {
            var doc = (@"<table><td>A</table>B").ToHtmlDocument();

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

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0Text0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("A", dochtml0body1table0tbody0tr0td0Text0.TextContent);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("B", dochtml0body1Text1.TextContent);
        }

        [Test]
        public void TableWithRowAndCaption()
        {
            var doc = (@"<table><tr><caption>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0caption1 = dochtml0body1table0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table0caption1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0caption1.Attributes.Length);
            Assert.AreEqual("caption", dochtml0body1table0caption1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0caption1.NodeType);
        }

        [Test]
        public void TableWithRowClosedEverythingOpenedTd()
        {
            var doc = (@"<table><tr></body></caption></col></colgroup></html></td></th><td>foo").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0Text0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1table0tbody0tr0td0Text0.NodeType);
            Assert.AreEqual("foo", dochtml0body1table0tbody0tr0td0Text0.TextContent);
        }

        [Test]
        public void TableWithTdAndTr()
        {
            var doc = (@"<table><td><tr>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr1 = dochtml0body1table0tbody0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr1.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr1.NodeType);
        }

        [Test]
        public void TableWithTdButtonAndTd()
        {
            var doc = (@"<table><td><button><td>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0button0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0button0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0button0.Attributes.Length);
            Assert.AreEqual("button", dochtml0body1table0tbody0tr0td0button0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0button0.NodeType);

            var dochtml0body1table0tbody0tr0td1 = dochtml0body1table0tbody0tr0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td1.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td1.NodeType);
        }

        [Test]
        public void TableWithRowCellAndSvgDescTd()
        {
            var doc = (@"<table><tr><td><svg><desc><td>").ToHtmlDocument();

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
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1table0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0.NodeType);

            var dochtml0body1table0tbody0 = dochtml0body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0.NodeType);

            var dochtml0body1table0tbody0tr0 = dochtml0body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0.NodeType);

            var dochtml0body1table0tbody0tr0td0 = dochtml0body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0.NodeType);

            var dochtml0body1table0tbody0tr0td0svg0 = dochtml0body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table0tbody0tr0td0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1table0tbody0tr0td0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0svg0.NodeType);

            var dochtml0body1table0tbody0tr0td0svg0desc0 = dochtml0body1table0tbody0tr0td0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0svg0desc0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td0svg0desc0.Attributes.Length);
            Assert.AreEqual("desc", dochtml0body1table0tbody0tr0td0svg0desc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td0svg0desc0.NodeType);

            var dochtml0body1table0tbody0tr0td1 = dochtml0body1table0tbody0tr0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table0tbody0tr0td1.Attributes.Length);
            Assert.AreEqual("td", dochtml0body1table0tbody0tr0td1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table0tbody0tr0td1.NodeType);
        }

        [Test]
        public void TableInParagraphElementInQuirksMode()
        {
            var doc = ("<p><table>").ToHtmlDocument();
            Assert.AreEqual(1, doc.Body.ChildElementCount);
            Assert.IsInstanceOf<HtmlParagraphElement>(doc.Body.Children[0]);
            Assert.AreEqual(1, doc.Body.Children[0].ChildElementCount);
            Assert.IsInstanceOf<HtmlTableElement>(doc.Body.Children[0].Children[0]);
        }

        [Test]
        public void TableInParagraphElementInStandardMode()
        {
            var doc = (@"<!doctype html><p><table>").ToHtmlDocument();
            Assert.AreEqual(2, doc.Body.ChildElementCount);
            Assert.IsInstanceOf<HtmlParagraphElement>(doc.Body.Children[0]);
            Assert.IsInstanceOf<HtmlTableElement>(doc.Body.Children[1]);
        }
    }
}