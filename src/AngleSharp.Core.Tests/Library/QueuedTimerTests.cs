namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Browser;
    using AngleSharp.Common;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    [TestFixture]
    public class QueuedTimerTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public void QueuedTimeoutHonorsCancellationBeforeExecution(Boolean disposeWindow)
        {
            var loop = new ManualEventLoop();
            var context = BrowsingContext.New(Configuration.Default.With<IEventLoop>(_ => loop));
            var document = new HtmlParser(new HtmlParserOptions(), context).ParseDocument("<!doctype html><body>Timer");
            loop.Drain();
            var calls = 0;
            var window = document.DefaultView;
            var handle = window.SetTimeout(_ => calls++, 0);
            Assert.Greater(loop.Pending, 0, "The timer must already be queued before cancellation.");
            if (disposeWindow) window.Dispose();
            else window.ClearTimeout(handle);
            loop.Drain();
            Assert.AreEqual(0, calls);
        }

        private sealed class ManualEventLoop : IEventLoop
        {
            private readonly Queue<Action<CancellationToken>> _pending = new Queue<Action<CancellationToken>>();
            public Int32 Pending => _pending.Count;
            public ICancellable Enqueue(Action<CancellationToken> action, TaskPriority priority)
            {
                _pending.Enqueue(action);
                return new PendingTask();
            }
            public void Drain()
            {
                while (_pending.Count > 0) _pending.Dequeue()(CancellationToken.None);
            }
            public void Spin() { }
            public void CancelAll() => _pending.Clear();
            private sealed class PendingTask : ICancellable
            {
                public Boolean IsCompleted => false;
                public Boolean IsRunning => false;
                public void Cancel() { }
            }
        }
    }
}
