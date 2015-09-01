namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Services;
    using System;
    using System.Threading.Tasks;

    class StandingEventLoopService : IEventService
    {
        public IEventLoop Create(IBrowsingContext context)
        {
            return new EventLoop();
        }

        sealed class EventLoop : IEventLoop
        {
            public void Enqueue(Task task)
            {
            }

            public Task Execute(Action steps)
            {
                return Task.FromResult(false);
            }

            public void Dispose()
            {
            }
        }
    }
}
