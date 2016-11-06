namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Browser;
    using AngleSharp.Common;
    using System;
    using System.Threading;

    sealed class StandingEventLoop : IEventLoop
    {
        public ICancellable Enqueue(Action<CancellationToken> action, TaskPriority priority)
        {
            return null;
        }

        public void Spin()
        {
        }

        public void CancelAll()
        {
        }
    }
}
