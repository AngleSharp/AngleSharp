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

        public Task Enqueue(Action task)
        {
            _current = _current.ContinueWith(_ => task());
            return _current;
        }

        public Task Execute(Action microtask)
        {
            return Enqueue(microtask);
        }

        public void Shutdown()
        {
        }
    }
}
