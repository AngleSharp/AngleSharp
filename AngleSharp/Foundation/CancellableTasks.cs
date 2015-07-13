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
        #region Fields

        readonly List<Tuple<Task, CancellationTokenSource>> _tasks;

        #endregion

        #region ctor

        public CancellableTasks()
        {
            _tasks = new List<Tuple<Task, CancellationTokenSource>>();
        }

        #endregion

        #region Methods

        public Task Add(Func<CancellationToken, Task> creator)
        {
            var cts = new CancellationTokenSource();
            var task = creator(cts.Token);
            return Add(task, cts);
        }

        public Task<T> Add<T>(Func<CancellationToken, Task<T>> creator)
        {
            var cts = new CancellationTokenSource();
            var task = creator(cts.Token);
            return Add(task, cts);
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
                task.Item2.Cancel();

            _tasks.Clear();
        }

        #endregion

        #region Enumerator

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

        #endregion

        #region Helpers

        T Add<T>(T task, CancellationTokenSource cts)
            where T : Task
        {
            var tuple = Tuple.Create<Task, CancellationTokenSource>(task, cts);
            _tasks.RemoveAll(m => m.Item1.Status == TaskStatus.RanToCompletion || m.Item1.Status == TaskStatus.Faulted);
            _tasks.Add(tuple);
            return task;
        }

        #endregion
    }
}
