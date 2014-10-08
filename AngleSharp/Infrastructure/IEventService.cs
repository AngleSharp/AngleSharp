namespace AngleSharp.Infrastructure
{
    using System;

    /// <summary>
    /// Represents an event loop.
    /// </summary>
    public interface IEventService : IService
    {
        /// <summary>
        /// Enqueues a given action to the event loop.
        /// </summary>
        /// <param name="action">The action to enqueue.</param>
        void Enqueue(Action action);
    }
}
