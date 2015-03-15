using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class DOMTable
    {
        [Test]
        public void ChildrenOfTable()
        {
            var document = DocumentBuilder.Html("");
            var table = document.CreateElement("table");
            SimpleTableTest(document, table, table);
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
