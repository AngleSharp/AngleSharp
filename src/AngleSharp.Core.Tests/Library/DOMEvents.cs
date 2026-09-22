namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Html.Dom.Events;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class DOMEventsTests
    {
        private IDocument document;

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
        public void NativeListenerRemovalNotifiesCachesAndPreservesReentrantRegistrations()
        {
            var target = (EventTarget)document;
            DomEventHandler listener = (_, _) => { };
            DomEventHandler other = (_, _) => { };
            var removed = new List<(String Type, DomEventHandler Callback, Boolean Capture)>();
            target.EventListenerRemoved += (type, callback, capture) =>
            {
                removed.Add((type, callback, capture));
                if (ReferenceEquals(callback, other))
                {
                    target.AddEventListener("next", listener);
                }
            };

            target.AddEventListener("probe", listener, true);
            target.AddEventListener("probe", other);
            target.RemoveEventListener("Probe", listener, true);
            Assert.IsEmpty(removed);
            target.RemoveEventListener("probe", listener, true);
            Assert.AreEqual(("probe", listener, true), removed[0]);
            target.RemoveEventListeners();
            Assert.AreEqual(2, removed.Count);
            Assert.AreEqual(("probe", other, false), removed[1]);

            var invoked = 0;
            target.RemoveEventListener("next", listener);
            Assert.AreEqual(3, removed.Count);
            target.AddEventListener("next", (_, _) => invoked++);
            var ev = document.CreateEvent("event");
            ev.Init("next", true, true);
            target.Dispatch(ev);
            Assert.AreEqual(1, invoked);
        }

        [Test]
        public void NativeListenerRemovalReportsStoredDelegateIdentity()
        {
            static void Handle(Object sender, Event ev) { }
            var target = (EventTarget)document;
            var stored = new DomEventHandler(Handle);
            var equal = new DomEventHandler(Handle);
            DomEventHandler notified = null;
            Assert.AreNotSame(stored, equal);
            Assert.AreEqual(stored, equal);
            target.EventListenerRemoved += (_, callback, _) => notified = callback;

            target.AddEventListener("probe", stored);
            target.RemoveEventListener("probe", equal);

            Assert.AreSame(stored, notified);
        }

        [Test]
        public void NativeBulkResetDoesNotNotifyAReaddedRegistration()
        {
            var target = (EventTarget)document;
            DomEventHandler first = (_, _) => { };
            DomEventHandler later = (_, _) => { };
            var notified = new List<DomEventHandler>();
            target.EventListenerRemoved += (type, callback, capture) =>
            {
                notified.Add(callback);
                if (ReferenceEquals(callback, first))
                {
                    target.AddEventListener("later", later);
                }
            };
            target.AddEventListener("first", first);
            target.AddEventListener("later", later);

            target.RemoveEventListeners();
            Assert.AreEqual(1, notified.Count);
            Assert.AreSame(first, notified[0]);

            target.RemoveEventListener("later", later);
            Assert.AreEqual(2, notified.Count);
            Assert.AreSame(later, notified[1]);
        }

        [Test]
        public void NativeIndividualRemovalStopsNotifyingAfterReRegistration()
        {
            var target = (EventTarget)document;
            DomEventHandler listener = (_, _) => { };
            var notifications = 0;
            target.EventListenerRemoved += (_, _, _) => target.AddEventListener("probe", listener);
            target.EventListenerRemoved += (_, _, _) => notifications++;
            target.AddEventListener("probe", listener);

            target.RemoveEventListener("probe", listener);

            Assert.AreEqual(0, notifications);
            Assert.IsTrue(target.HasEventListener("probe"));
        }

        [Test]
        public void EventsAddHandler()
        {
            var evName = "click";
            var element = document.QuerySelector("img");
            var args = document.CreateEvent("event");
            args.Init(evName, true, true);
            var count = 0;
            DomEventHandler listener1 = (_, _) => count++;
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
            document.QuerySelector("img");
            var ev = document.CreateEvent("event");
            ev.Init(evName, true, true);
            var task = document.AwaitEventAsync(evName);
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
            DomEventHandler listener1 = (_, _) => count++;
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
            DomEventHandler listener1 = (_, ev) =>
            {
                Assert.AreEqual(evName, ev.Type);
                Assert.AreEqual(EventPhase.AtTarget, ev.Phase);
                Assert.AreEqual(element, ev.CurrentTarget);
                Assert.AreEqual(element, ev.OriginalTarget);
                Assert.IsFalse(beforeOther);
            };
            DomEventHandler listener2 = (_, ev) =>
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

            void listener1(object s, Event ev)
            {
                Assert.AreEqual(evName, ev.Type);
                Assert.AreEqual(EventPhase.AtTarget, ev.Phase);
                Assert.AreEqual(element, ev.CurrentTarget);
                Assert.AreEqual(element, ev.OriginalTarget);
                Assert.IsTrue(beforeOther);
            }

            void listener2(object s, Event ev)
            {
                Assert.AreEqual(evName, ev.Type);
                Assert.AreEqual(EventPhase.Bubbling, ev.Phase);
                Assert.AreEqual(element.Parent, ev.CurrentTarget);
                Assert.AreEqual(element, ev.OriginalTarget);
                beforeOther = false;
            }
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
            DomEventHandler listener = (_, ev) =>
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
            DomEventHandler listener = (_, ev) =>
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
            var factory = new DefaultEventFactory();
            var invalid = factory.Create("invalid");
            var @event = factory.Create("event");
            var events = factory.Create("events");
            var wheelevent = factory.Create("wheelevent");

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
            document.ReadyStateChanged += (_, _) =>
            {
                Assert.AreEqual(DocumentReadyState.Complete, document.ReadyState);
            };

            document.Loaded += (_, _) =>
            {
                Assert.AreNotEqual(DocumentReadyState.Complete, document.ReadyState);
            };
        }
    }
}
