namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Browser;
    using AngleSharp.Common;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Runs the tasks one after another on a thread of its own, the ones of
    /// a higher priority first (like the event loop of AngleSharp.Js).
    /// </summary>
    sealed class PriorityEventLoop : IEventLoop, IDisposable
    {
        private readonly SortedDictionary<TaskPriority, Queue<Action<CancellationToken>>> _queues = new SortedDictionary<TaskPriority, Queue<Action<CancellationToken>>>();
        private readonly Thread _thread;
        private Boolean _disposed;

        public PriorityEventLoop()
        {
            _thread = new Thread(Run) { IsBackground = true };
            _thread.Start();
        }

        public ICancellable Enqueue(Action<CancellationToken> action, TaskPriority priority)
        {
            lock (_queues)
            {
                if (!_queues.TryGetValue(priority, out var queue))
                {
                    queue = new Queue<Action<CancellationToken>>();
                    _queues[priority] = queue;
                }

                queue.Enqueue(action);
                Monitor.Pulse(_queues);
            }

            return new DoneTask();
        }

        public void Spin()
        {
        }

        public void CancelAll()
        {
        }

        public void Dispose()
        {
            lock (_queues)
            {
                _disposed = true;
                Monitor.Pulse(_queues);
            }
        }

        private void Run()
        {
            while (true)
            {
                var action = default(Action<CancellationToken>);

                lock (_queues)
                {
                    while (!_disposed && (action = Dequeue()) is null)
                    {
                        Monitor.Wait(_queues);
                    }

                    if (_disposed)
                    {
                        return;
                    }
                }

                action.Invoke(CancellationToken.None);
            }
        }

        private Action<CancellationToken> Dequeue()
        {
            var next = default(Queue<Action<CancellationToken>>);

            // sorted by priority, so the last queue with tasks is the most important one
            foreach (var queue in _queues.Values)
            {
                if (queue.Count > 0)
                {
                    next = queue;
                }
            }

            return next?.Dequeue();
        }

        sealed class DoneTask : ICancellable
        {
            public Boolean IsCompleted => false;

            public Boolean IsRunning => false;

            public void Cancel()
            {
            }
        }
    }
}
