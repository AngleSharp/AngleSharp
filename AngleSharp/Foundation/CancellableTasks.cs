namespace AngleSharp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class CancellableTasks : IDisposable, IEnumerable<Task>
    {
        #region Fields

        readonly List<CancellableTask> _tasks;

        #endregion

        #region ctor

        public CancellableTasks()
        {
            _tasks = new List<CancellableTask>();
        }

        #endregion

        #region Methods

        public Task<T> Add<T>(Func<CancellationToken, Task<T>> creator)
        {
            var cts = new CancellationTokenSource();
            var task = creator(cts.Token);
            return Add(task, cts);
        }

        public void Cancel(Task task)
        {
            if (task == null)
                return;

            for (int i = _tasks.Count - 1; i >= 0; i--)
            {
                if (_tasks[i].Is(task))
                {
                    _tasks.Remove(_tasks[i].Cancel());
                    break;
                }
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < _tasks.Count; i++)
                _tasks[i].Cancel();

            _tasks.Clear();
        }

        #endregion

        #region Enumerator

        public IEnumerator<Task> GetEnumerator()
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                var task = _tasks[i];

                if (task.IsActive)
                    yield return task;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Helpers

        Task<T> Add<T>(Task<T> task, CancellationTokenSource cts)
        {
            _tasks.RemoveAll(m => m.IsActive == false);
            _tasks.Add(new CancellableTask(task, cts, ""));
            return task;
        }

        #endregion

        #region Struct

        struct CancellableTask
        {
            readonly Task _task;
            readonly CancellationTokenSource _cts;
            readonly String _group;

            public CancellableTask(Task task, CancellationTokenSource cts, String group)
            {
                _task = task;
                _cts = cts;
                _group = group;
            }

            public CancellableTask Cancel()
            {
                _cts.Cancel();
                return this;
            }

            public Boolean Is(Task task)
            {
                return _task == task;
            }

            public static implicit operator Task(CancellableTask t)
            {
                return t._task;
            }

            public Boolean IsActive
            {
                get
                {
                    return _task.Status != TaskStatus.RanToCompletion && _task.Status != TaskStatus.Faulted;
                }
            }
        }

        #endregion
    }
}
