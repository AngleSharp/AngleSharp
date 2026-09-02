namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class StringMapTests
    {
        private HtmlElement a;
        private StringMap stringMap;

        [SetUp]
        public void CreateMap()
        {
            var document = new HtmlDocument();
            a = new HtmlElement(document, "a");
            a.SetAttribute("data-test1", "test");
            a.SetAttribute("data-b", "b");
            stringMap = new StringMap("data-", a);
        }

        [Test]
        public void RemoveTest()
        {
            stringMap.Remove("b");
            Assert.AreEqual(a.GetAttribute("data-b"), null);
        }

        [Test]
        public void RemoveDeletesTheAttribute()
        {
            stringMap.Remove("b");
            Assert.IsFalse(a.HasAttribute("data-b"));
            Assert.IsFalse(stringMap.Contains("b"));
        }

        [Test]
        public void RemoveDropsTheNameFromTheEnumeration()
        {
            stringMap.Remove("b");
            Assert.AreEqual(1, stringMap.Count());
            Assert.AreEqual("test1", stringMap.Single().Key);
        }

        [Test]
        public void RemoveOfUnknownNameKeepsTheOtherAttributes()
        {
            stringMap.Remove("c");
            Assert.AreEqual(2, stringMap.Count());
            Assert.IsTrue(a.HasAttribute("data-b"));
        }

        [Test]
        public void ContainsTest()
        {
            Assert.IsTrue(stringMap.Contains("b"));
            Assert.AreEqual(a.GetAttribute("data-b"), "b");
        }

        [Test]
        public void GetEnumeratorTest()
        {
            foreach (var str in stringMap)
            {
                Assert.AreEqual(a.GetAttribute("data-" + str.Key), str.Value);
            }
        }
    }
}
