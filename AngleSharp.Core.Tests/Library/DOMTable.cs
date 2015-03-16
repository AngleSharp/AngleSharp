using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class DOMTable
    {
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
            var orphan1 = table.AppendChild(document.CreateElement("tr")) as IElement;
            orphan1.Id = "orphan1";
            var foot1 = table.AppendChild(document.CreateElement("tfoot"));
            var orphan2 = table.AppendChild(document.CreateElement("tr")) as IElement;
            orphan2.Id = "orphan2";
            var foot2 = table.AppendChild(document.CreateElement("tfoot"));
            var orphan3 = table.AppendChild(document.CreateElement("tr")) as IElement;
            orphan3.Id = "orphan3";
            var body1 = table.AppendChild(document.CreateElement("tbody"));
            var orphan4 = table.AppendChild(document.CreateElement("tr")) as IElement;
            orphan4.Id = "orphan4";
            var body2 = table.AppendChild(document.CreateElement("tbody"));
            var orphan5 = table.AppendChild(document.CreateElement("tr")) as IElement;
            orphan5.Id = "orphan5";
            var head1 = table.AppendChild(document.CreateElement("thead"));
            var orphan6 = table.AppendChild(document.CreateElement("tr")) as IElement;
            orphan6.Id = "orphan6";
            var head2 = table.AppendChild(document.CreateElement("thead"));
            var orphan7 = table.AppendChild(document.CreateElement("tr")) as IElement;
            orphan7.Id = "orphan7";
            var foot1row1 = foot1.AppendChild(document.CreateElement("tr")) as IElement;
            foot1row1.Id = "foot1row1";
            var foot1row2 = foot1.AppendChild(document.CreateElement("tr")) as IElement;
            foot1row2.Id = "foot1row2";
            var foot2row1 = foot2.AppendChild(document.CreateElement("tr")) as IElement;
            foot2row1.Id = "foot2row1";
            var foot2row2 = foot2.AppendChild(document.CreateElement("tr")) as IElement;
            foot2row2.Id = "foot2row2";
            var body1row1 = body1.AppendChild(document.CreateElement("tr")) as IElement;
            body1row1.Id = "body1row1";
            var body1row2 = body1.AppendChild(document.CreateElement("tr")) as IElement;
            body1row2.Id = "body1row2";
            var body2row1 = body2.AppendChild(document.CreateElement("tr")) as IElement;
            body2row1.Id = "body2row1";
            var body2row2 = body2.AppendChild(document.CreateElement("tr")) as IElement;
            body2row2.Id = "body2row2";
            var head1row1 = head1.AppendChild(document.CreateElement("tr")) as IElement;
            head1row1.Id = "head1row1";
            var head1row2 = head1.AppendChild(document.CreateElement("tr")) as IElement;
            head1row2.Id = "head1row2";
            var head2row1 = head2.AppendChild(document.CreateElement("tr")) as IElement;
            head2row1.Id = "head2row1";
            var head2row2 = head2.AppendChild(document.CreateElement("tr")) as IElement;
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

            CollectionAssert.AreEquivalent(new[] {
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
            }, rows);

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
            CollectionAssert.AreEquivalent(new[] { before }, table.ChildNodes);

            var tbody = table.CreateBody() as IHtmlTableSectionElement;
            CollectionAssert.AreEquivalent(new[] { before, tbody }, table.ChildNodes);
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyTwoTbodyChildNodes()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before1 = table.AppendChild(document.CreateElement("tbody")) as IHtmlTableSectionElement;
            var before2 = table.AppendChild(document.CreateElement("tbody")) as IHtmlTableSectionElement;
            CollectionAssert.AreEquivalent(new[] { before1, before2 }, table.ChildNodes);

            var tbody = table.CreateBody();
            CollectionAssert.AreEquivalent(new[] { before1, before2, tbody }, table.ChildNodes);
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyTheadAndTbodyChildNode()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before1 = table.AppendChild(document.CreateElement("thead")) as IHtmlTableSectionElement;
            var before2 = table.AppendChild(document.CreateElement("tbody")) as IHtmlTableSectionElement;
            CollectionAssert.AreEquivalent(new[] { before1, before2 }, table.ChildNodes);

            var tbody = table.CreateBody();
            CollectionAssert.AreEquivalent(new[] { before1, before2, tbody }, table.ChildNodes);
            AssertTableBody(tbody);
        }

        [Test]
        public void TableBodyTbodyAndTfootChildNode()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table") as IHtmlTableElement;
            var before1 = table.AppendChild(document.CreateElement("tfoot"));
            var before2 = table.AppendChild(document.CreateElement("tbody"));
            CollectionAssert.AreEquivalent(new[] { before1, before2 }, table.ChildNodes);

            var tbody = table.CreateBody();
            CollectionAssert.AreEquivalent(new[] { before1, before2, tbody }, table.ChildNodes);
            AssertTableBody(tbody);
        }

        static void AssertTableBody(IHtmlTableSectionElement body)
        {
            Assert.AreEqual("tbody", body.LocalName);
            Assert.AreEqual("http://www.w3.org/1999/xhtml", body.NamespaceUri);
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
