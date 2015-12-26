namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Dom;
    using AngleSharp.Services;
    using System;
    using System.Threading;

    class StandingEventLoopService : IEventService
    {
        public IEventLoop Create(IDocument document)
        {
            return new EventLoop();
        }

        sealed class EventLoop : IEventLoop
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
}
