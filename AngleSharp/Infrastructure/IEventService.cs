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

        /// <summary>
        /// Gets the running time of the currently running action.
        /// </summary>
        TimeSpan ActionTime { get; }

        /// <summary>
        /// Gets if there are no actions running.
        /// </summary>
        Boolean IsIdle { get; }

        /// <summary>
        /// Cancels the currently running action.
        /// </summary>
        void Cancel();
    }
}
