namespace AngleSharp.Core.Tests.Mocks
{
    using System;
    using System.Threading;

    sealed class StandingEventLoop : IEventLoop
    {
        public IEventLoopEntry Enqueue(Action<CancellationToken> action, TaskPriority priority)
        {
            return null;
        }

        public void Spin()
        {
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}
