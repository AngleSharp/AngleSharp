namespace AngleSharp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class CancellableTasks : IDisposable, IEnumerable<Task>
    {
        readonly List<Tuple<Task, CancellationTokenSource>> _tasks;

        public CancellableTasks()
        {
            _tasks = new List<Tuple<Task, CancellationTokenSource>>();
        }

        public void Add(Task task, CancellationTokenSource cts)
        {
            var tuple = Tuple.Create(task, cts);
            _tasks.Add(tuple);
            task.ContinueWith(m => _tasks.Remove(tuple));
        }

        public Task Add(Func<CancellationToken, Task> creator)
        {
            var cts = new CancellationTokenSource();
            var task = creator(cts.Token);
            Add(task, cts);
            return task;
        }

        public Task<T> Add<T>(Func<CancellationToken, Task<T>> creator)
        {
            var cts = new CancellationTokenSource();
            var task = creator(cts.Token);
            Add(task, cts);
            return task;
        }

        public void Cancel(Task task)
        {
            if (task != null)
            {
                for (int i = _tasks.Count - 1; i >= 0; i--)
                {
                    if (_tasks[i].Item1 == task)
                    {
                        var tuple = _tasks[i];
                        tuple.Item2.Cancel();
                        _tasks.Remove(tuple);
                        break;
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (var task in _tasks)
            {
                task.Item2.Cancel();
            }

            _tasks.Clear();
        }

        public IEnumerator<Task> GetEnumerator()
        {
            return _tasks.Select(m => m.Item1).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
