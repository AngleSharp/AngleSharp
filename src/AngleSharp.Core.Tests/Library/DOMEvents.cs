namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Html.Dom.Events;
    using NUnit.Framework;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

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
        public void WindowUnloadingHandlerUsesBeforeUnloadEventName()
        {
            var calls = 0;
            DomEventHandler handler = (_, ev) => { calls++; ev.Cancel(); };
            document.DefaultView.Unloading += handler;
            var ev = new Event("beforeunload", false, true);
            document.DefaultView.Dispatch(ev);
            Assert.AreEqual(1, calls);
            Assert.IsTrue(ev.IsDefaultPrevented);
            document.DefaultView.Unloading -= handler;
            document.DefaultView.Dispatch(new Event("beforeunload", false, true));
            Assert.AreEqual(1, calls);
        }
      
        [Test]
        public void DispatchReturnsFalseWhenAListenerCanceled()
        {
            var element = document.QuerySelector("#first");
            element.AddEventListener("click", (_, ev) => ev.Cancel());

            var result = element.Dispatch(new Event("click", true, true));

            Assert.IsFalse(result);
        }

        [Test]
        public void DispatchReturnsTrueWhenNotCanceled()
        {
            var element = document.QuerySelector("#first");
            element.AddEventListener("click", (_, _) => { });

            var result = element.Dispatch(new Event("click", true, true));

            Assert.IsTrue(result);
        }

        [Test]
        public void DispatchReturnsTrueWhenCancelingANonCancelableEvent()
        {
            var element = document.QuerySelector("#first");
            element.AddEventListener("click", (_, ev) => ev.Cancel());

            var result = element.Dispatch(new Event("click", true, false));

            Assert.IsTrue(result);
        }

        [Test]
        public void NativeListenerResetNotifiesOnceAfterClearingListeners()
        {
            var target = (EventTarget)document;
            var notifications = 0;
            var invoked = 0;
            target.AddEventListener("probe", (_, _) => invoked++);
            target.OnReset += (sender, args) =>
            {
                Assert.AreSame(target, sender);
                Assert.AreSame(EventArgs.Empty, args);
                Assert.IsFalse(target.HasEventListener("probe"));
                notifications++;
            };

            target.RemoveEventListeners();
            Assert.AreEqual(1, notifications);

            var ev = document.CreateEvent("event");
            ev.Init("probe", true, true);
            target.Dispatch(ev);
            Assert.AreEqual(0, invoked);
        }

        [Test]
        public void NativeIndividualRemovalDoesNotNotifyResetSubscribers()
        {
            var target = (EventTarget)document;
            DomEventHandler listener = (_, _) => { };
            var notifications = 0;
            target.OnReset += (_, _) => notifications++;
            target.AddEventListener("probe", listener);

            target.RemoveEventListener("probe", listener);

            Assert.AreEqual(0, notifications);
            Assert.IsFalse(target.HasEventListener("probe"));
            target.RemoveEventListeners();
            Assert.AreEqual(1, notifications);
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
            // Completed by the dispatch itself, whatever synchronization context is current -
            // only continuations of the task are posted elsewhere.
            Assert.IsTrue(task.IsCompleted);
            Assert.IsFalse(task.IsFaulted);
            Assert.AreEqual(evName, task.Result.Type);
        }

        [Test]
        public void EventsAwaitedListenerRemovedByDispatch()
        {
            var target = (EventTarget)document;
            var ev = new Event("click", true, true);
            var task = document.AwaitEventAsync("click");
            Assert.IsTrue(target.HasEventListener("click"));
            document.Dispatch(ev);
            Assert.IsTrue(task.IsCompleted);
            Assert.IsFalse(target.HasEventListener("click"));
        }

        [Test]
        public void EventsAwaitedCancelledRemovesListener()
        {
            var target = (EventTarget)document;
            using var cts = new CancellationTokenSource();
            var task = document.AwaitEventAsync("click", cts.Token);
            Assert.IsTrue(target.HasEventListener("click"));
            cts.Cancel();
            Assert.IsTrue(task.IsCanceled);
            Assert.IsFalse(target.HasEventListener("click"));
            Assert.ThrowsAsync<TaskCanceledException>(async () => await task);
        }

        [Test]
        public void EventsAwaitedCancelledFromAnotherThreadRemovesListener()
        {
            var target = (EventTarget)document;
            using var cts = new CancellationTokenSource();
            var task = document.AwaitEventAsync("click", cts.Token);
            Task.Run(() => cts.Cancel()).Wait();
            Assert.IsTrue(task.IsCanceled);
            Assert.IsFalse(target.HasEventListener("click"));
        }

        [Test]
        public void EventsAwaitedWithCancelledTokenRegistersNothing()
        {
            var target = (EventTarget)document;
            var task = document.AwaitEventAsync("click", new CancellationToken(true));
            Assert.IsTrue(task.IsCanceled);
            Assert.IsFalse(target.HasEventListener("click"));
        }

        [Test]
        public void EventsAwaitedFiredBeforeCancellationIsNotCancelled()
        {
            using var cts = new CancellationTokenSource();
            var task = document.AwaitEventAsync("click", cts.Token);
            document.Dispatch(new Event("click", true, true));
            cts.Cancel();
            Assert.AreEqual(TaskStatus.RanToCompletion, task.Status);
        }

        [Test]
        public void EventsAwaitedWithNullArgumentsReturnsFaultedTask()
        {
            var nodeTask = default(IDocument).AwaitEventAsync("click");
            var nameTask = document.AwaitEventAsync(null);
            Assert.IsTrue(nodeTask.IsFaulted);
            Assert.IsInstanceOf<ArgumentNullException>(nodeTask.Exception.InnerException);
            Assert.IsTrue(nameTask.IsFaulted);
            Assert.IsInstanceOf<ArgumentNullException>(nameTask.Exception.InnerException);
        }

        [Test]
        public void EventsListenersAddedAndRemovedConcurrentlyWithDispatch()
        {
            var target = (EventTarget)document;
            var stop = 0;
            var dispatcher = Task.Run(() =>
            {
                while (Volatile.Read(ref stop) == 0)
                {
                    document.Dispatch(new Event("click", true, true));
                }
            });
            var workers = new Task[4];

            for (var w = 0; w < workers.Length; w++)
            {
                workers[w] = Task.Run(() =>
                {
                    for (var i = 0; i < 2000; i++)
                    {
                        DomEventHandler handler = (_, _) => { };
                        document.AddEventListener("click", handler);
                        document.RemoveEventListener("click", handler);
                    }
                });
            }

            Task.WaitAll(workers);
            Volatile.Write(ref stop, 1);
            dispatcher.Wait();
            Assert.IsFalse(target.HasEventListener("click"));
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
