namespace AngleSharp
{
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
        private TaskEventLoopEntry _current;

        public TaskEventLoop()
        {
            _queues = new Dictionary<TaskPriority, Queue<TaskEventLoopEntry>>();
            _current = null;
        }

        public IEventLoopEntry Enqueue(Action<CancellationToken> task, TaskPriority priority)
        {
            var entry = new TaskEventLoopEntry(task);

            lock (this)
            {
                var entries = default(Queue<TaskEventLoopEntry>);

                if (!_queues.TryGetValue(priority, out entries))
                {
                    entries = new Queue<TaskEventLoopEntry>();
                    _queues.Add(priority, entries);
                }

                if (_current == null)
                {
                    _current = entry;
                    RunCurrent();
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
            lock (this)
            {
                if (_current != null && _current.IsRunning)
                {
                    return;
                }

                _current = Dequeue(TaskPriority.Critical) ?? 
                           Dequeue(TaskPriority.Microtask) ?? 
                           Dequeue(TaskPriority.Normal) ?? 
                           Dequeue(TaskPriority.None);
            }

            RunCurrent();
        }

        public void CancelAll()
        {
            lock (this)
            {
                foreach (var queue in _queues)
                {
                    var entries = queue.Value;

                    foreach (var entry in entries)
                    {
                        entry.Cancel();
                    }

                    entries.Clear();
                }

                _queues.Clear();

                if (_current != null)
                {
                    _current.Cancel();
                }
            }
        }

        private void RunCurrent()
        {
            _current?.Run(() =>
            {
                lock (this)
                {
                    _current = null;
                }

                Spin();
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

        private sealed class TaskEventLoopEntry : IEventLoopEntry
        {
            private readonly Task _task;
            private readonly CancellationTokenSource _cts;
            
            private Boolean _started;
            private DateTime _created;

            public TaskEventLoopEntry(Action<CancellationToken> action)
            {
                _cts = new CancellationTokenSource();
                _task = new Task(() => action(_cts.Token), _cts.Token);
            }

            public Boolean IsRunning
            {
                get 
                { 
                    return _task.Status == TaskStatus.Running || 
                           _task.Status == TaskStatus.WaitingForActivation || 
                           _task.Status == TaskStatus.WaitingToRun; 
                }
            }

            public void Run(Action callback)
            {
                if (!_started)
                {
                    _created = DateTime.Now;
                    _task.Start();
                    _task.ContinueWith(_ => callback());
                    _started = true;
                }
            }

            public void Cancel()
            {
                _cts.Cancel();
            }

            public DateTime? Started
            {
                get 
                {
                    if (!IsRunning)
                    {
                        return null;
                    }

                    return _created;
                }
            }
        }
    }
}
