namespace AngleSharp
{
    using System;
    using System.Threading.Tasks;

    sealed class TaskEventLoop : IEventLoop
    {
        Task _current;

        public TaskEventLoop()
        {
            _current = TaskEx.FromResult(false);
        }

        public void Enqueue(Task task)
        {
            if (task.Status == TaskStatus.Created)
            {
                _current = _current.ContinueWith(_ => task.Start());
            }
            else
            {
                _current = task;
            }
        }

        public Task Execute(Action steps)
        {
            var task = new Task(steps);
            Enqueue(task);
            return task;
        }

        public void Dispose()
        {
        }
    }
}
