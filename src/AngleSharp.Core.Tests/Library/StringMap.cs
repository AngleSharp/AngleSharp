namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;

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
