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
                        _tasks.Remove(tuple);
                        tuple.Item2.Cancel();
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
            for (int i = 0; i < _tasks.Count; i++)
            {
                var tuple = _tasks[i];

                if (tuple.Item1.Status != TaskStatus.RanToCompletion)
                    yield return tuple.Item1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
