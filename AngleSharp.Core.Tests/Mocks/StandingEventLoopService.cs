namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Dom;
    using AngleSharp.Services;
    using System;
    using System.Threading.Tasks;

    class StandingEventLoopService : IEventService
    {
        public IEventLoop Create(IDocument document)
        {
            return new EventLoop();
        }

        sealed class EventLoop : IEventLoop
        {
            public Task Enqueue(Action task)
            {
                return Task.FromResult(false);
            }

            public Task Execute(Action microtask)
            {
                return Task.FromResult(false);
            }

            public void Shutdown()
            {
            }
        }
    }
}
