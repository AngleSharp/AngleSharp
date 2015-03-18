using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NUnit.Framework;
using System;
using System.Linq;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class DOMTable
    {
        static readonly String HTMLNS = "http://www.w3.org/1999/xhtml";
        static readonly String SectionRowIndexCode = @"<table>
  <thead>
    <tr id=ht1></tr>
  </thead>
  <tr id=t1></tr>
  <tr id=t2>
    <td>
      <table>
        <thead>
          <tr id=nht1></tr>
        </thead>
        <tr></tr>
        <tr id=nt1></tr>
        <tbody>
          <tr id=nbt1></tr>
        </tbody>
      </table>
    </td>
  </tr>
  <tbody>
    <tr></tr>
    <tr id=bt1></tr>
  </tbody>
  <tfoot>
    <tr></tr>
    <tr></tr>
    <tr id=ft1></tr>
  </tfoot>
</table> ";

        [Test]
        public void ChildrenOfTableDirectly()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table");
            SimpleTableTest(document, table, table);
        }

        [Test]
        public void ChildrenOfTableHead()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table");
            var group = table.AppendChild(document.CreateElement("thead")) as IElement;
            SimpleTableTest(document, group, table);
        }

        [Test]
        public void ChildrenOfTableFoot()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table");
            var group = table.AppendChild(document.CreateElement("tfoot")) as IElement;
            SimpleTableTest(document, group, table);
        }

        [Test]
        public void ChildrenOfTableBody()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table");
            var group = table.AppendChild(document.CreateElement("tbody")) as IElement;
            SimpleTableTest(document, group, table);
        }

        [Test]
        public void ChildrenOfTableAndVariousSections()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table");
            var orphan1 = table.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            orphan1.Id = "orphan1";
            var foot1 = table.AppendChild(document.CreateElement("tfoot"));
            var orphan2 = table.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            orphan2.Id = "orphan2";
            var foot2 = table.AppendChild(document.CreateElement("tfoot"));
            var orphan3 = table.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            orphan3.Id = "orphan3";
            var body1 = table.AppendChild(document.CreateElement("tbody"));
            var orphan4 = table.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            orphan4.Id = "orphan4";
            var body2 = table.AppendChild(document.CreateElement("tbody"));
            var orphan5 = table.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            orphan5.Id = "orphan5";
            var head1 = table.AppendChild(document.CreateElement("thead"));
            var orphan6 = table.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            orphan6.Id = "orphan6";
            var head2 = table.AppendChild(document.CreateElement("thead"));
            var orphan7 = table.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            orphan7.Id = "orphan7";
            var foot1row1 = foot1.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            foot1row1.Id = "foot1row1";
            var foot1row2 = foot1.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            foot1row2.Id = "foot1row2";
            var foot2row1 = foot2.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            foot2row1.Id = "foot2row1";
            var foot2row2 = foot2.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            foot2row2.Id = "foot2row2";
            var body1row1 = body1.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            body1row1.Id = "body1row1";
            var body1row2 = body1.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            body1row2.Id = "body1row2";
            var body2row1 = body2.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            body2row1.Id = "body2row1";
            var body2row2 = body2.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            body2row2.Id = "body2row2";
            var head1row1 = head1.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            head1row1.Id = "head1row1";
            var head1row2 = head1.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            head1row2.Id = "head1row2";
            var head2row1 = head2.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            head2row1.Id = "head2row1";
            var head2row2 = head2.AppendChild(document.CreateElement("tr")) as IHtmlTableRowElement;
            head2row2.Id = "head2row2";

            // These elements should not end up in any collection.
            table.AppendChild(document.CreateElement("div"))
                 .AppendChild(document.CreateElement("tr"));
            foot1.AppendChild(document.CreateElement("div"))
                 .AppendChild(document.CreateElement("tr"));
            body1.AppendChild(document.CreateElement("div"))
                 .AppendChild(document.CreateElement("tr"));
            head1.AppendChild(document.CreateElement("div"))
                 .AppendChild(document.CreateElement("tr"));
            table.AppendChild(document.CreateElement("http://example.com/test", "tr"));
            foot1.AppendChild(document.CreateElement("http://example.com/test", "tr"));
            body1.AppendChild(document.CreateElement("http://example.com/test", "tr"));
            head1.AppendChild(document.CreateElement("http://example.com/test", "tr"));

            var rows = (table as IHtmlTableElement).Rows;
            Assert.IsNotNull(rows);

            CollectionAssert.AreEqual(new IHtmlTableRowElement[] {
                // thead
                head1row1,
                head1row2,
                head2row1,
                head2row2,
                // tbody + table
                orphan1,
                orphan2,
                orphan3,
                body1row1,
                body1row2,
                orphan4,
                body2row1,
                body2row2,
                orphan5,
                orphan6,
                orphan7,
                // tfoot
                foot1row1,
                foot1row2,
                foot2row1,
                foot2row2
            }, rows.ToArray());

            var ids = new[] {
                "orphan1",
                "orphan2",
                "orphan3",
                "orphan4",
                "orphan5",
                "orphan6",
                "orphan7",
                "foot1row1",
                "foot1row2",
                "foot2row1",
                "foot2row2",
                "body1row1",
                "body1row2",
                "body2row1",
                "body2row2",
                "head1row1",
                "head1row2",
                "head2row1",
                "head2row2"
            };

            foreach (var id in ids)
                Assert.AreEqual(id, rows[id].Id);

            while (table.FirstChild != null)
                table.RemoveChild(table.FirstChild);

            foreach (var id in ids)
                Assert.IsNull(rows[id]);
        }

        [Test]
        public void MoveTableAndAppendCells()
        {
            var text =
              "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
              "  <head>" +
              "    <title>Virtual Library</title>" +
              "  </head>" +
              "  <body>" +
              "    <table id=\"mytable\" border=\"1\">" +
              "      <tbody>" +
              "        <tr><td>Cell 1</td><td>Cell 2</td></tr>" +
              "        <tr><td>Cell 3</td><td>Cell 4</td></tr>" +
              "      </tbody>" +
              "    </table>" +
              "  </body>" +
              "</html>";
            var document = DocumentBuilder.Html("");
            var doc = DocumentBuilder.Html(text);
            // import <table>
            var table = doc.DocumentElement.GetElementsByTagName("table")[0];
            var mytable = document.Body.AppendChild(document.Import(table, true)) as IHtmlTableElement;
            Assert.IsNotNull(mytable);
            Assert.AreEqual(1, mytable.Bodies.Length);
            var tbody = document.CreateElement("tbody") as IHtmlTableSectionElement;
            mytable.AppendChild(tbody);
            var tr = tbody.InsertRowAt(-1);
            tr.InsertCellAt(-1).AppendChild(document.CreateTextNode("Cell 5"));
            tr.InsertCellAt(-1).AppendChild(document.CreateTextNode("Cell 6"));
            Assert.AreEqual(2, mytable.Bodies.Length);
            Assert.AreEqual(3, mytable.Rows.Length);
            Assert.AreEqual(2, tr.Index);
        }

        [Test]
        public void TableBodyNoChildNodes()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var tbody = table.CreateBody();
            Assert.AreEqual(table.FirstChild, tbody);
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyOneTbodyChildNode()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before = table.AppendChild(document.CreateElement("tbody")) as IHtmlTableSectionElement;
            CollectionAssert.AreEqual(new[] { before }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody() as IHtmlTableSectionElement;
            CollectionAssert.AreEqual(new[] { before, tbody }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyTwoTbodyChildNodes()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before1 = table.AppendChild(document.CreateElement("tbody")) as IHtmlTableSectionElement;
            var before2 = table.AppendChild(document.CreateElement("tbody")) as IHtmlTableSectionElement;
            CollectionAssert.AreEqual(new[] { before1, before2 }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody();
            CollectionAssert.AreEqual(new[] { before1, before2, tbody }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyTheadAndTbodyChildNode()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before1 = table.AppendChild(document.CreateElement("thead")) as IHtmlTableSectionElement;
            var before2 = table.AppendChild(document.CreateElement("tbody")) as IHtmlTableSectionElement;
            CollectionAssert.AreEqual(new[] { before1, before2 }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody();
            CollectionAssert.AreEqual(new[] { before1, before2, tbody }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyTfootAndTbodyChildNode()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before1 = table.AppendChild(document.CreateElement("tfoot"));
            var before2 = table.AppendChild(document.CreateElement("tbody"));
            CollectionAssert.AreEqual(new[] { before1, before2 }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody();
            CollectionAssert.AreEqual(new[] { before1, before2, tbody }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyTbodyAndTheadChildNode()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before = table.AppendChild(document.CreateElement("tbody")) as IHtmlTableSectionElement;
            var after = table.AppendChild(document.CreateElement("thead")) as IHtmlTableSectionElement;
            CollectionAssert.AreEqual(new INode[] { before, after }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody();
            CollectionAssert.AreEqual(new INode[] { before, tbody, after }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyTbodyAndTfootChildNode()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before = table.AppendChild(document.CreateElement("tbody")) as IHtmlTableSectionElement;
            var after = table.AppendChild(document.CreateElement("tfoot")) as IHtmlTableSectionElement;
            CollectionAssert.AreEqual(new INode[] { before, after }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody();
            CollectionAssert.AreEqual(new INode[] { before, tbody, after }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyTwoTbodyChildNodesAndADiv()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before1 = table.AppendChild(document.CreateElement("tbody")) as IHtmlTableSectionElement;
            var before2 = table.AppendChild(document.CreateElement("tbody")) as IHtmlTableSectionElement;
            var after = table.AppendChild(document.CreateElement("div"));
            CollectionAssert.AreEqual(new INode[] { before1, before2, after }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody();
            CollectionAssert.AreEqual(new INode[] { before1, before2, tbody, after }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyOneHtmlOneNamespacedTBodyElement()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before = table.AppendChild(document.CreateElement("tbody"));
            var after = table.AppendChild(document.CreateElement("x", "tbody"));
            CollectionAssert.AreEqual(new[] { before, after }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody();
            CollectionAssert.AreEqual(new[] { before, tbody, after }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyTwoNestedTBodyChildNodes()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before1 = table.AppendChild(document.CreateElement("tbody"));
            var before2 = before1.AppendChild(document.CreateElement("tbody"));
            CollectionAssert.AreEqual(new INode[] { before1 }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody();
            CollectionAssert.AreEqual(new INode[] { before1, tbody }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyATBodyInsideATHead()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before1 = table.AppendChild(document.CreateElement("thead"));
            var before2 = before1.AppendChild(document.CreateElement("tbody"));
            CollectionAssert.AreEqual(new INode[] { before1 }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody();
            CollectionAssert.AreEqual(new INode[] { before1, tbody }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyATBodyInsideATFoot()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before1 = table.AppendChild(document.CreateElement("tfoot"));
            var before2 = before1.AppendChild(document.CreateElement("tbody"));
            CollectionAssert.AreEqual(new INode[] { before1 }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody();
            CollectionAssert.AreEqual(new INode[] { before1, tbody }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyATBodyNodeInsideATHeadChildNodeAfterATBodyChildNode()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before = table.AppendChild(document.CreateElement("tbody"));
            var after1 = table.AppendChild(document.CreateElement("thead"));
            var after2 = after1.AppendChild(document.CreateElement("tbody"));
            CollectionAssert.AreEqual(new INode[] { before, after1 }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody();
            CollectionAssert.AreEqual(new INode[] { before, tbody, after1 }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyATBodyNodeInsideATFootChildNodeAfterATBodyChildNode()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before = table.AppendChild(document.CreateElement("tbody"));
            var after1 = table.AppendChild(document.CreateElement("tfoot"));
            var after2 = after1.AppendChild(document.CreateElement("tbody"));
            CollectionAssert.AreEqual(new INode[] { before, after1 }, table.ChildNodes.ToArray());

            var tbody = table.CreateBody();
            CollectionAssert.AreEqual(new INode[] { before, tbody, after1 }, table.ChildNodes.ToArray());
            AssertTableBody(tbody);
        }

        [Test]
        public void TableInsertRowShouldNotCopyPrefixes()
        {
            var document = DocumentBuilder.Html("");
            var parentEl = document.CreateElement(HTMLNS, "html:table") as IHtmlTableElement;
            Assert.AreEqual(HTMLNS, parentEl.NamespaceUri);
            Assert.AreEqual("html", parentEl.Prefix);
            Assert.AreEqual("table", parentEl.LocalName);
            Assert.AreEqual("HTML:TABLE", parentEl.TagName);
            var row = parentEl.InsertRowAt(-1);
            Assert.AreEqual(HTMLNS, row.NamespaceUri);
            Assert.IsNull(row.Prefix);
            Assert.AreEqual("tr", row.LocalName);
            Assert.AreEqual("TR", row.TagName);
            var body = row.ParentElement;
            Assert.AreEqual(HTMLNS, body.NamespaceUri);
            Assert.IsNull(body.Prefix);
            Assert.AreEqual("tbody", body.LocalName);
            Assert.AreEqual("TBODY", body.TagName);
            CollectionAssert.AreEqual(new INode[] { body }, parentEl.ChildNodes.ToArray());
            CollectionAssert.AreEqual(new INode[] { row }, body.ChildNodes.ToArray());
            CollectionAssert.AreEqual(new IHtmlTableRowElement[] { row }, parentEl.Rows.ToArray());
        }

        [Test]
        public void TableInsertRowShouldInsertIntoTbodyNotTheadIfTableRowsIsEmpty()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var head = table.AppendChild(document.CreateElement("thead"));
            CollectionAssert.AreEqual(new INode[0], table.Rows.ToArray());
            var row = table.InsertRowAt(-1);
            var body = row.Parent;
            CollectionAssert.AreEqual(new INode[] { head, body }, table.ChildNodes.ToArray());
            CollectionAssert.AreEqual(new INode[0], head.ChildNodes.ToArray());
            CollectionAssert.AreEqual(new INode[] { row }, body.ChildNodes.ToArray());
            CollectionAssert.AreEqual(new INode[] { row }, table.Rows.ToArray());
        }

        [Test]
        public void TableInsertRowShouldInsertIntoTbodyNotTfootIfTableRowsIsEmpty()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var foot = table.AppendChild(document.CreateElement("tfoot"));
            CollectionAssert.AreEqual(new INode[0], table.Rows.ToArray());
            var row = table.InsertRowAt(-1);
            var body = row.Parent;
            CollectionAssert.AreEqual(new INode[] { foot, body }, table.ChildNodes.ToArray());
            CollectionAssert.AreEqual(new INode[0], foot.ChildNodes.ToArray());
            CollectionAssert.AreEqual(new INode[] { row }, body.ChildNodes.ToArray());
            CollectionAssert.AreEqual(new INode[] { row }, table.Rows.ToArray());
        }

        [Test]
        public void TableCreateCaptionReturnsTheFirstCaptionElementChildOfTheTable()
        {
            var document = DocumentBuilder.Html(@"<table>
		<caption>caption</caption>
		<tr>
			<td>cell</td>
			<td>cell</td>
		</tr>
	</table>");
            var table = document.QuerySelector("table") as IHtmlTableElement;
            var testCaption = table.CreateCaption();
            var tableFirstCaption = table.Caption;
            Assert.AreEqual(tableFirstCaption, testCaption);
        }

        [Test]
        public void TableCreateCaptionCreatesNewCaptionAndInsertsItAsTheFirstNodeOfTheTableElement()
        {
            var document = DocumentBuilder.Html(@"<table>
		<tr>
			<td>cell</td>
			<td>cell</td>
		</tr>
	</table>");
            var table = document.QuerySelector("table") as IHtmlTableElement;
            var testCaption = table.CreateCaption();
            var tableFirstNode = table.FirstChild;
            Assert.IsNotNull(testCaption);
            Assert.AreEqual(testCaption, tableFirstNode);
        }

        [Test]
        public void TableDeleteCaptionRemovesTheFirstCaptionElementChildOfTheTableElement()
        {
            var document = DocumentBuilder.Html(@"<table>
		<caption>caption</caption>
		<tr>
			<td>cell</td>
			<td>cell</td>
		</tr>
	</table>");
            var table = document.QuerySelector("table") as IHtmlTableElement;
            Assert.AreEqual("caption", table.Caption.TextContent);
            table.DeleteCaption();
            Assert.IsNull(table.Caption);
        }

        [Test]
        public void TableFirstCaptionElementChild()
        {
            var document = DocumentBuilder.Html(@"<table>
      <tr><td></td></tr>
      <caption>first caption</caption>
      <caption>second caption</caption>
    </table>");
            var table = document.QuerySelector("table") as IHtmlTableElement;
            Assert.AreEqual("first caption", table.Caption.InnerHtml);
        }

        [Test]
        public void TableSettingCaption()
        {
            var document = DocumentBuilder.Html(@"<table>
      <tr><td></td></tr>
      <caption>first caption</caption>
      <caption>second caption</caption>
    </table>");
            var caption = document.CreateElement("caption") as IHtmlTableCaptionElement;
            caption.InnerHtml = "new caption";
            var table = document.QuerySelector("table") as IHtmlTableElement;
            table.Caption = caption;
            Assert.AreEqual(table, caption.Parent);
            Assert.AreEqual("new caption", table.Caption.InnerHtml);
            var captions = table.GetElementsByTagName("caption");
            Assert.AreEqual(2, captions.Length);
            Assert.AreEqual("new caption", captions[0].InnerHtml);
            Assert.AreEqual("second caption", captions[1].InnerHtml);
        }

        [Test]
        public void TableWithNoCaption()
        {
            var document = DocumentBuilder.Html(@"<table>
      <tr><td></td></tr>
    </table>");
            var table = document.QuerySelector("table") as IHtmlTableElement;
            Assert.IsNull(table.Caption);
        }

        [Test]
        public void TableWithNestedCaption()
        {
            var document = DocumentBuilder.Html(@"<table>
      <tr><td></td></tr>
    </table>");
            var table = document.QuerySelector("table") as IHtmlTableElement;
            var caption = document.CreateElement("caption");
            table.Rows[0].AppendChild(caption);
            Assert.IsNull(table.Caption);
        }

        [Test]
        public void TableDynamicallyRemovingTheCaption()
        {
            var document = DocumentBuilder.Html(@"<table>
      <tr><td></td></tr>
      <caption>first caption</caption>
    </table>");
            var table = document.QuerySelector("table") as IHtmlTableElement;
            Assert.IsNotNull(table.Caption);
            table.Caption.Remove();
            Assert.IsNull(table.Caption);
        }

        [Test]
        public void TableRowIndexUndefinedInDiv()
        {
            var document = DocumentBuilder.Html("");
            var row = document.CreateElement("table")
                              .AppendChild(document.CreateElement("div"))
                              .AppendElement(document.CreateElement<IHtmlTableRowElement>());
            Assert.AreEqual(-1, row.Index);
        }

        [Test]
        public void TableRowIndexDefinedInTableHead()
        {
            var document = DocumentBuilder.Html("");
            var row = document.CreateElement("table")
                              .AppendChild(document.CreateElement("thead"))
                              .AppendElement(document.CreateElement<IHtmlTableRowElement>());
            Assert.AreEqual(0, row.Index);
        }

        [Test]
        public void TableRowIndexDefinedInTableBody()
        {
            var document = DocumentBuilder.Html("");
            var row = document.CreateElement("table")
                              .AppendChild(document.CreateElement("tbody"))
                              .AppendElement(document.CreateElement<IHtmlTableRowElement>());
            Assert.AreEqual(0, row.Index);
        }

        [Test]
        public void TableRowIndexDefinedInTableFoot()
        {
            var document = DocumentBuilder.Html("");
            var row = document.CreateElement("table")
                              .AppendChild(document.CreateElement("tfoot"))
                              .AppendElement(document.CreateElement<IHtmlTableRowElement>());
            Assert.AreEqual(0, row.Index);
        }

        [Test]
        public void TableRowIndexDefinedInTable()
        {
            var document = DocumentBuilder.Html("");
            var row = document.CreateElement("table")
                              .AppendElement(document.CreateElement<IHtmlTableRowElement>());
            Assert.AreEqual(0, row.Index);
        }

        [Test]
        public void TableRowIndexUndefinedInTableHeadOfNamespacedTable()
        {
            var document = DocumentBuilder.Html("");
            var row = document.CreateElement("", "table")
                              .AppendChild(document.CreateElement("thead"))
                              .AppendElement(document.CreateElement<IHtmlTableRowElement>());
            Assert.AreEqual(-1, row.Index);
        }

        [Test]
        public void TableRowIndexUndefinedInTableBodyOfNamespacedTable()
        {
            var document = DocumentBuilder.Html("");
            var row = document.CreateElement("", "table")
                              .AppendChild(document.CreateElement("tbody"))
                              .AppendElement(document.CreateElement<IHtmlTableRowElement>());
            Assert.AreEqual(-1, row.Index);
        }

        [Test]
        public void TableRowIndexUndefinedInTableFootOfNamespacedTable()
        {
            var document = DocumentBuilder.Html("");
            var row = document.CreateElement("", "table")
                              .AppendChild(document.CreateElement("tfoot"))
                              .AppendElement(document.CreateElement<IHtmlTableRowElement>());
            Assert.AreEqual(-1, row.Index);
        }

        [Test]
        public void TableRowIndexUndefinedInNamespacedTable()
        {
            var document = DocumentBuilder.Html("");
            var row = document.CreateElement("", "table")
                              .AppendElement(document.CreateElement<IHtmlTableRowElement>());
            Assert.AreEqual(-1, row.Index);
        }

        [Test]
        public void TableRowIndexUndefinedInNamespacedTableHead()
        {
            var document = DocumentBuilder.Html("");
            var row = document.CreateElement("table")
                              .AppendChild(document.CreateElement("", "thead"))
                              .AppendElement(document.CreateElement<IHtmlTableRowElement>());
            Assert.AreEqual(-1, row.Index);
        }

        [Test]
        public void TableRowIndexUndefinedInNamespacedTableBody()
        {
            var document = DocumentBuilder.Html("");
            var row = document.CreateElement("table")
                              .AppendChild(document.CreateElement("", "tbody"))
                              .AppendElement(document.CreateElement<IHtmlTableRowElement>());
            Assert.AreEqual(-1, row.Index);
        }

        [Test]
        public void TableRowIndexUndefinedInNamespacedTableFoot()
        {
            var document = DocumentBuilder.Html("");
            var row = document.CreateElement("table")
                              .AppendChild(document.CreateElement("", "tfoot"))
                              .AppendElement(document.CreateElement<IHtmlTableRowElement>());
            Assert.AreEqual(-1, row.Index);
        }

        [Test]
        public void TableRowInThead()
        {
            var document = DocumentBuilder.Html(SectionRowIndexCode);
            var tHeadRow = document.GetElementById("ht1") as IHtmlTableRowElement;
            Assert.AreEqual(0, tHeadRow.IndexInSection);
        }

        [Test]
        public void TableRowInImplicitTbody()
        {
            var document = DocumentBuilder.Html(SectionRowIndexCode);
            var tRow1 = document.GetElementById("t1") as IHtmlTableRowElement;
            Assert.AreEqual(0, tRow1.IndexInSection);
        }

        [Test]
        public void TableOtherRowInImplicitTbody()
        {
            var document = DocumentBuilder.Html(SectionRowIndexCode);
            var tRow2 = document.GetElementById("t2") as IHtmlTableRowElement;
            Assert.AreEqual(1, tRow2.IndexInSection);
        }

        [Test]
        public void TableRowInExplicitTbody()
        {
            var document = DocumentBuilder.Html(SectionRowIndexCode);
            var tBodyRow = document.GetElementById("bt1") as IHtmlTableRowElement;
            Assert.AreEqual(1, tBodyRow.IndexInSection);
        }

        [Test]
        public void TableRowInTfoot()
        {
            var document = DocumentBuilder.Html(SectionRowIndexCode);
            var tFootRow = document.GetElementById("ft1") as IHtmlTableRowElement;
            Assert.AreEqual(2, tFootRow.IndexInSection);
        }

        [Test]
        public void TableRowInTheadInNestedTable()
        {
            var document = DocumentBuilder.Html(SectionRowIndexCode);
            var childHeadRow = document.GetElementById("nht1") as IHtmlTableRowElement;
            Assert.AreEqual(0, childHeadRow.IndexInSection);
        }

        [Test]
        public void TableRowInImplicitTbodyInNestedTable()
        {
            var document = DocumentBuilder.Html(SectionRowIndexCode);
            var childRow = document.GetElementById("nt1") as IHtmlTableRowElement;
            Assert.AreEqual(1, childRow.IndexInSection);
        }

        [Test]
        public void TableRowInExplicitTbodyInNestedTable()
        {
            var document = DocumentBuilder.Html(SectionRowIndexCode);
            var childBodyRow = document.GetElementById("nbt1") as IHtmlTableRowElement;
            Assert.AreEqual(0, childBodyRow.IndexInSection);
        }

        static void AssertTableBody(IHtmlTableSectionElement body)
        {
            Assert.AreEqual("tbody", body.LocalName);
            Assert.AreEqual(HTMLNS, body.NamespaceUri);
            Assert.IsNull(body.Prefix);
        }

        static void SimpleTableTest(IDocument document, IElement group, IElement table)
        {
            var foo1 = group.AppendChild(document.CreateElement("tr")) as IElement;
            foo1.Id = "foo";
            var bar1 = group.AppendChild(document.CreateElement("tr")) as IElement;
            bar1.Id = "bar";
            var foo2 = group.AppendChild(document.CreateElement("tr")) as IElement;
            foo2.Id = "foo";
            var bar2 = group.AppendChild(document.CreateElement("tr")) as IElement;
            bar2.Id = "bar";
            var rows = (table as IHtmlTableElement).Rows;
            Assert.IsNotNull(rows);
            CollectionAssert.AreEquivalent(new[] { foo1, bar1, foo2, bar2 }, rows);
            Assert.AreEqual(foo1, rows["foo"]);
            Assert.AreEqual(bar1, rows["bar"]);
        }
    }
}
