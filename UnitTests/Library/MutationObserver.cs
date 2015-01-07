using AngleSharp;
using AngleSharp.DOM;
using NUnit.Framework;
using System.Linq;

namespace UnitTests.Library
{
    [TestFixture]
    public class MutationObserverTests
    {
        [Test]
        public void ConnectMutationObserverChildNodesTriggerManually()
        {
            var called = false;

            var observer = new MutationObserver((mut, obs) =>
            {
                called = true;
                Assert.AreEqual(1, mut.Length);
                Assert.IsNotNull(mut[0].Added);
                Assert.AreEqual(1, mut[0].Added.Length);
            });

            var document = DocumentBuilder.Html("");

            observer.Connect(document.Body, new MutationObserverInit
            {
                ObserveTargetChildNodes = true
            });

            document.Body.AppendChild(document.CreateElement("span"));
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }

        [Test]
        public void ConnectMutationObserverAttributesTriggerManually()
        {
            var called = false;
            var attrName = "something";
            var attrValue = "test";

            var observer = new MutationObserver((mut, obs) =>
            {
                called = true;
                Assert.AreEqual(1, mut.Length);
                Assert.AreEqual(attrName, mut[0].AttributeName);
                Assert.IsNull(mut[0].PreviousValue);
            });

            var document = DocumentBuilder.Html("");

            observer.Connect(document.Body, new MutationObserverInit
            {
                ObserveTargetAttributes = true
            });

            document.Body.SetAttribute(attrName, attrValue);
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }
    }
}
