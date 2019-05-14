namespace AngleSharp.Browser
{
    using AngleSharp.Common;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The default event loop.
    /// </summary>
    sealed class TaskEventLoop : IEventLoop
    {
        private readonly Dictionary<TaskPriority, Queue<TaskEventLoopEntry>> _queues;
        private readonly Object _lockObj = new Object();
        private TaskEventLoopEntry _current;

        public TaskEventLoop(IBrowsingContext context)
        {
            _queues = new Dictionary<TaskPriority, Queue<TaskEventLoopEntry>>();
            _current = null;
        }

        public ICancellable Enqueue(Action<CancellationToken> task, TaskPriority priority)
        {
            var entry = new TaskEventLoopEntry(task);

            lock (_lockObj)
            {
                if (!_queues.TryGetValue(priority, out var entries))
                {
                    entries = new Queue<TaskEventLoopEntry>();
                    _queues.Add(priority, entries);
                }

                if (_current == null)
                {
                    SetCurrent(entry);
                }
                else
                {
                    entries.Enqueue(entry);
                }
            }

            return entry;
        }

        public void Spin()
        {
            lock (_lockObj)
            {
                SpinInternal();
            }
        }

        public void CancelAll()
        {
            lock (_lockObj)
            {
                foreach (var queue in _queues)
                {
                    var entries = queue.Value;

                    while (entries.Count > 0)
                    {
                        entries.Dequeue().Cancel();
                    }
                }

                _queues.Clear();
                _current?.Cancel();
            }
        }

        private void SpinInternal()
        {
            var completed = _current?.IsCompleted ?? true;

            if (completed)
            {
                SetCurrent(
                    Dequeue(TaskPriority.Critical) ??
                    Dequeue(TaskPriority.Microtask) ??
                    Dequeue(TaskPriority.Normal) ??
                    Dequeue(TaskPriority.None));
            }
        }

        private void SetCurrent(TaskEventLoopEntry entry)
        {
            _current = entry;
            entry?.Run(() =>
            {
                lock (_lockObj)
                {
                    _current = null;
                    SpinInternal();
                }
            });
        }

        private TaskEventLoopEntry Dequeue(TaskPriority priority)
        {
            if (_queues.ContainsKey(priority) && _queues[priority].Count != 0)
            {
                return _queues[priority].Dequeue();
            }

            return null;
        }

        private sealed class TaskEventLoopEntry : ICancellable
        {
            private readonly CancellationTokenSource _cts;
            private readonly Action<CancellationToken> _action;
            private Task _task;

            public TaskEventLoopEntry(Action<CancellationToken> action)
            {
                _cts = new CancellationTokenSource();
                _action = action;
            }

            public Boolean IsCompleted => _task != null && _task.IsCompleted;

            public Boolean IsRunning => _task != null &&
                           (_task.Status == TaskStatus.Running ||
                           _task.Status == TaskStatus.WaitingForActivation ||
                           _task.Status == TaskStatus.WaitingToRun ||
                           _task.Status == TaskStatus.WaitingForChildrenToComplete);

            public void Run(Action callback)
            {
                if (_task == null)
                {
                    _task = Task.Run(() => _action.Invoke(_cts.Token), _cts.Token);
                    _task.ContinueWith(_ => callback.Invoke());
                }
            }

            public void Cancel() => _cts.Cancel();
        }
    }
}
