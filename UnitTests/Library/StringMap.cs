namespace AngleSharp.DOM.Collections.Tests
{
    using AngleSharp.DOM.Html;
    using NUnit.Framework;

    [TestFixture]
    public class StringMapTests
    {
        HTMLElement a;
        StringMap stringMap;

        [SetUp]
        public void CreateMap()
        {
            var document = new Document();
            a = new HTMLElement(document, "a");
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
        public void ContainsTest()
        {
            Assert.IsTrue(stringMap.Contains("b"));
            Assert.AreEqual(a.GetAttribute("data-b"), "b");
        }

        [Test]
        public void GetEnumeratorTest()
        {
            foreach (var str in stringMap)
                Assert.AreEqual(a.GetAttribute("data-" + str.Key), str.Value);
        }
    }
}
