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
        readonly Dictionary<TaskPriority, Queue<TaskEventLoopEntry>> _queues;
        TaskEventLoopEntry _current;

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

            if (_current != null)
            {
                RunCurrent();
            }
        }

        public void Shutdown()
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

        void RunCurrent()
        {
            _current.Run(() =>
            {
                lock (this)
                {
                    _current = null;
                    Spin();
                }
            });
        }

        TaskEventLoopEntry Dequeue(TaskPriority priority)
        {
            if (_queues.ContainsKey(priority) && _queues[priority].Count != 0)
            {
                return _queues[priority].Dequeue();
            }

            return null;
        }

        sealed class TaskEventLoopEntry : IEventLoopEntry
        {
            readonly Task _task;
            readonly CancellationTokenSource _cts;
            
            Boolean _started;
            DateTime _created;

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
