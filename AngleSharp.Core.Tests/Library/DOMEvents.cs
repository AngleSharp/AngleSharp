namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using NUnit.Framework;

    [TestFixture]
    public class DOMEventsTests
    {
        IDocument document;

        [SetUp]
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
            document = source.ToHtmlDocument();
        }

        [Test]
        public void EventsAddHandler()
        {
            var evName = "click";
            var element = document.QuerySelector("img");
            var args = document.CreateEvent("event");
            args.Init(evName, true, true);
            var count = 0;
            DomEventHandler listener1 = (s, ev) => count++;
            element.AddEventListener(evName, listener1);
            element.Dispatch(args);
            Assert.AreEqual(1, count);
            Assert.AreEqual(evName, args.Type);
            Assert.IsFalse(args.IsTrusted);
        }

        [Test]
        public void EventsAwaitedTriggered()
        {
            var evName = "click";
            var element = document.QuerySelector("img");
            var ev = document.CreateEvent("event");
            ev.Init(evName, true, true);
            var task = document.AwaitEvent(evName);
            Assert.IsFalse(task.IsCompleted);
            document.Dispatch(ev);
            Assert.IsTrue(task.IsCompleted);
            Assert.IsFalse(task.IsFaulted);
            Assert.AreEqual(evName, task.Result.Type);
        }

        [Test]
        public void EventsRemoveHandler()
        {
            var evName = "click";
            var element = document.QuerySelector("img");
            var args = document.CreateEvent("event");
            args.Init(evName, true, true);
            var count = 0;
            DomEventHandler listener1 = (s, ev) => count++;
            element.AddEventListener(evName, listener1);
            element.RemoveEventListener(evName, listener1);
            element.Dispatch(args);
            Assert.AreEqual(0, count);
            Assert.AreEqual(evName, args.Type);
            Assert.IsFalse(args.IsTrusted);
        }

        [Test]
        public void EventsCapturingDispatchHandler()
        {
            var evName = "click";
            var element = document.QuerySelector("img");
            var args = document.CreateEvent("event");
            var beforeOther = true;
            args.Init(evName, true, true);
            DomEventHandler listener1 = (s, ev) =>
            {
                Assert.AreEqual(evName, ev.Type);
                Assert.AreEqual(EventPhase.AtTarget, ev.Phase);
                Assert.AreEqual(element, ev.CurrentTarget);
                Assert.AreEqual(element, ev.OriginalTarget);
                Assert.IsFalse(beforeOther);
            };
            DomEventHandler listener2 = (s, ev) =>
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

        [Test]
        public void EventsBubblingDispatchHandler()
        {
            var evName = "click";
            var element = document.QuerySelector("img");
            var args = document.CreateEvent("event");
            var beforeOther = true;
            args.Init(evName, true, true);
            DomEventHandler listener1 = (s, ev) =>
            {
                Assert.AreEqual(evName, ev.Type);
                Assert.AreEqual(EventPhase.AtTarget, ev.Phase);
                Assert.AreEqual(element, ev.CurrentTarget);
                Assert.AreEqual(element, ev.OriginalTarget);
                Assert.IsTrue(beforeOther);
            };
            DomEventHandler listener2 = (s, ev) =>
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

        [Test]
        public void EventsCustomHandlerViaFactory()
        {
            var evName = "myevent";
            var element = document.QuerySelector("img");
            var args = document.CreateEvent("customevent") as CustomEvent;
            Assert.IsNotNull(args);
            var mydetails = new object();
            args.Init(evName, true, true, mydetails);
            DomEventHandler listener = (s, ev) =>
            {
                Assert.AreEqual(args, ev);
                Assert.AreEqual(evName, ev.Type);
                Assert.AreEqual(EventPhase.AtTarget, ev.Phase);
                Assert.AreEqual(element, ev.CurrentTarget);
                Assert.AreEqual(element, ev.OriginalTarget);
                Assert.AreEqual(mydetails, args.Details);
            };
            element.AddEventListener(evName, listener);
            element.Dispatch(args);
        }

        [Test]
        public void EventsCustomHandlerViaConstructor()
        {
            var evName = "myevent";
            var element = document.QuerySelector("img");
            var args = new CustomEvent();
            var mydetails = new object();
            args.Init(evName, true, true, mydetails);
            DomEventHandler listener = (s, ev) =>
            {
                Assert.AreEqual(args, ev);
                Assert.AreEqual(evName, ev.Type);
                Assert.AreEqual(EventPhase.AtTarget, ev.Phase);
                Assert.AreEqual(element, ev.CurrentTarget);
                Assert.AreEqual(element, ev.OriginalTarget);
                Assert.AreEqual(mydetails, args.Details);
            };
            element.AddEventListener(evName, listener);
            element.Dispatch(args);
        }

        [Test]
        public void EventsFactory()
        {
            var invalid = Factory.Events.Create("invalid");
            var @event = Factory.Events.Create("event");
            var events = Factory.Events.Create("events");
            var wheelevent = Factory.Events.Create("wheelevent");

            Assert.IsNull(invalid);
            Assert.IsNotNull(@event);
            Assert.IsNotNull(events);
            Assert.IsNotNull(wheelevent);

            Assert.IsInstanceOf<Event>(@event);
            Assert.IsInstanceOf<Event>(events);
            Assert.IsInstanceOf<WheelEvent>(wheelevent);
        }

        [Test]
        public void EventsDocumentFinished()
        {
            document.ReadyStateChanged += (s, ev) =>
            {
                Assert.AreEqual(DocumentReadyState.Complete, document.ReadyState);
            };

            document.Loaded += (s, ev) =>
            {
                Assert.AreNotEqual(DocumentReadyState.Complete, document.ReadyState);
            };
        }
    }
}
