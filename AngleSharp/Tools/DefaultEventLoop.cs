namespace AngleSharp.Tools
{
    using AngleSharp.Infrastructure;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the default event loop, that just executes actions
    /// in the given order.
    /// </summary>
    sealed class DefaultEventLoop : IEventService
    {
        #region Fields

        Task _queue;

        #endregion

        #region ctor

        public DefaultEventLoop()
        {
            _queue = new Task(() => { });
            _queue.Start();
        }

        #endregion

        #region Methods

        public void Enqueue(Action action)
        {
            _queue = _queue.ContinueWith(_ => action());
        }

        #endregion
    }
}
