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
                IsObservingChildNodes = true
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
                IsObservingAttributes = true
            });

            document.Body.SetAttribute(attrName, attrValue);
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }

        [Test]
        public void ConnectMutationObserverMultipleAttributesDescendentTriggerManually()
        {
            var called1 = false;
            var called2 = false;
            var called3 = false;
            var attrName = "something";
            var attrValue = "test";

            var document = DocumentBuilder.Html("");

            var observer1 = new MutationObserver((mut, obs) =>
            {
                called1 = true;
                Assert.AreEqual(1, mut.Length);
            });

            observer1.Connect(document.DocumentElement, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsObservingSubtree = true
            });

            var observer2 = new MutationObserver((mut, obs) =>
            {
                called2 = true;
                Assert.AreEqual(0, mut.Length);
            });

            observer2.Connect(document.DocumentElement, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsObservingSubtree = false
            });

            var observer3 = new MutationObserver((mut, obs) =>
            {
                called3 = true;
                Assert.AreEqual(1, mut.Length);
            });

            observer3.Connect(document.Body, new MutationObserverInit
            {
                IsObservingAttributes = true
            });

            document.Body.SetAttribute(attrName, attrValue);
            observer1.TriggerWith(observer1.Flush().ToArray());
            observer2.TriggerWith(observer2.Flush().ToArray());
            observer3.TriggerWith(observer3.Flush().ToArray());
            Assert.IsTrue(called1);
            Assert.IsTrue(called2);
            Assert.IsTrue(called3);
        }

        [Test]
        public void ConnectMutationObserverTextWithDescendentsTriggerManually()
        {
            var called = false;
            var text = "something";
            var replaced = "different";

            var observer = new MutationObserver((mut, obs) =>
            {
                called = true;
                Assert.AreEqual(1, mut.Length);
                Assert.AreEqual(text, mut[0].PreviousValue);
                var tn = mut[0].Target as TextNode;
                Assert.IsNotNull(tn);
                Assert.AreEqual(text + replaced, tn.TextContent);
            });

            var document = DocumentBuilder.Html("");

            observer.Connect(document.Body, new MutationObserverInit
            {
                IsObservingCharacterData = true,
                IsObservingSubtree = true
            });

            document.Body.TextContent = text;
            var textNode = document.Body.ChildNodes[0] as TextNode;
            textNode.Replace(text.Length, 0, replaced);
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }

        [Test]
        public void ConnectMutationObserverTextNoDescendentsTriggerManually()
        {
            var called = false;
            var text = "something";
            var replaced = "different";

            var observer = new MutationObserver((mut, obs) =>
            {
                called = true;
                Assert.AreEqual(0, mut.Length);
            });

            var document = DocumentBuilder.Html("");

            observer.Connect(document.Body, new MutationObserverInit
            {
                IsObservingCharacterData = true,
                IsObservingSubtree = false
            });

            document.Body.TextContent = text;
            var textNode = document.Body.ChildNodes[0] as TextNode;
            textNode.Replace(text.Length, 0, replaced);
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }

        [Test]
        public void ConnectMutationObserverTextNoDescendentsButCreatedTriggerManually()
        {
            var called = false;
            var text = "something";

            var observer = new MutationObserver((mut, obs) =>
            {
                called = true;
                Assert.AreEqual(1, mut.Length);
                Assert.AreEqual(1, mut[0].Added.Length);
                Assert.AreEqual(text, mut[0].Added[0].TextContent);
            });

            var document = DocumentBuilder.Html("");

            observer.Connect(document.Body, new MutationObserverInit
            {
                IsObservingSubtree = false,
                IsObservingChildNodes = true
            });

            document.Body.TextContent = text;
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }
    }
}
