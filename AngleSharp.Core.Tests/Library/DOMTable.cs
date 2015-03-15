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
