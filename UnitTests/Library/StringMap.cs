namespace AngleSharp.DOM.Collections.Tests
{
    using AngleSharp.DOM.Html;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StringMapTests
    {
        HTMLElement a;
        StringMap stringMap;

        [TestInitialize]
        public void CreateMap()
        {
            a = new HTMLElement("a");
            a.SetAttribute("data-test1", "test");
            a.SetAttribute("data-b", "b");
            stringMap = new StringMap("data-", a);
        }

        [TestMethod]
        public void RemoveTest()
        {
            stringMap.Remove("b");
            Assert.AreEqual(a.GetAttribute("data-b"), null);
        }

        [TestMethod]
        public void ContainsTest()
        {
            Assert.IsTrue(stringMap.Contains("b"));
            Assert.AreEqual(a.GetAttribute("data-b"), "b");
        }

        [TestMethod]
        public void GetEnumeratorTest()
        {
            foreach (var str in stringMap)
                Assert.AreEqual(a.GetAttribute("data-" + str.Key), str.Value);
        }
    }
}
