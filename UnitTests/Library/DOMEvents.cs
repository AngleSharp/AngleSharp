using AngleSharp;
using AngleSharp.DOM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Library
{
    [TestClass]
    public class DOMEventsTests
    {
        IDocument doc;

        [TestInitialize]
        public void Init()
        {
            var source = @"<!doctype html>
<body>
<div id=first>
<span>
<img />
</span>
</div>
<div id=second>
</div>
</body>";
            doc = DocumentBuilder.Html(source);
        }

        [TestMethod]
        public void EventsAddHandler()
        {
            var evName = "click";
            var element = doc.QuerySelector("img");
            var args = doc.CreateEvent("event");
            args.Init(evName, true, true);
            var count = 0;
            EventListener listener1 = (s, ev) => count++;
            element.AddEventListener(evName, listener1);
            element.Dispatch(args);
            Assert.AreEqual(1, count);
            Assert.AreEqual(evName, args.Type);
            Assert.IsFalse(args.IsTrusted);
        }

        [TestMethod]
        public void EventsRemoveHandler()
        {
            var evName = "click";
            var element = doc.QuerySelector("img");
            var args = doc.CreateEvent("event");
            args.Init(evName, true, true);
            var count = 0;
            EventListener listener1 = (s, ev) => count++;
            element.AddEventListener(evName, listener1);
            element.RemoveEventListener(evName, listener1);
            element.Dispatch(args);
            Assert.AreEqual(0, count);
            Assert.AreEqual(evName, args.Type);
            Assert.IsFalse(args.IsTrusted);
        }

        [TestMethod]
        public void EventsCapturingDispatchHandler()
        {
            var evName = "click";
            var element = doc.QuerySelector("img");
            var args = doc.CreateEvent("event");
            var beforeOther = true;
            args.Init(evName, true, true);
            EventListener listener1 = (s, ev) =>
            {
                Assert.AreEqual(evName, ev.Type);
                Assert.AreEqual(EventPhase.AtTarget, ev.Phase);
                Assert.AreEqual(element, ev.CurrentTarget);
                Assert.AreEqual(element, ev.OriginalTarget);
                Assert.IsFalse(beforeOther);
            };
            EventListener listener2 = (s, ev) =>
            {
                Assert.AreEqual(evName, ev.Type);
                Assert.AreEqual(EventPhase.Capturing, ev.Phase);
                Assert.AreEqual(element.Parent, ev.CurrentTarget);
                Assert.AreEqual(element, ev.OriginalTarget);
                beforeOther = false;
            };
            element.AddEventListener(evName, listener1);
            element.Parent.AddEventListener(evName, listener2, true);
            element.Dispatch(args);
        }

        [TestMethod]
        public void EventsBubblingDispatchHandler()
        {
            var evName = "click";
            var element = doc.QuerySelector("img");
            var args = doc.CreateEvent("event");
            var beforeOther = true;
            args.Init(evName, true, true);
            EventListener listener1 = (s, ev) =>
            {
                Assert.AreEqual(evName, ev.Type);
                Assert.AreEqual(EventPhase.AtTarget, ev.Phase);
                Assert.AreEqual(element, ev.CurrentTarget);
                Assert.AreEqual(element, ev.OriginalTarget);
                Assert.IsTrue(beforeOther);
            };
            EventListener listener2 = (s, ev) =>
            {
                Assert.AreEqual(evName, ev.Type);
                Assert.AreEqual(EventPhase.Bubbling, ev.Phase);
                Assert.AreEqual(element.Parent, ev.CurrentTarget);
                Assert.AreEqual(element, ev.OriginalTarget);
                beforeOther = false;
            };
            element.AddEventListener(evName, listener1);
            element.Parent.AddEventListener(evName, listener2);
            element.Dispatch(args);
        }
    }
}
