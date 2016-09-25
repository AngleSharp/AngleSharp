namespace AngleSharp
{
    using System;
    using System.Threading;

    /// <summary>
    /// Represents the DOM event loop processing stages and steps of algorithms.
    /// See 7.1.4.2 Processing model.
    /// </summary>
    public interface IEventLoop
    {
        /// <summary>
        /// Enqueues a given task with the associated document.
        /// </summary>
        /// <param name="action">The continuation action to enqueue.</param>
        /// <param name="priority">The priority to use.</param>
        /// <returns>The created loop entry.</returns>
        ICancellable Enqueue(Action<CancellationToken> action, TaskPriority priority);

        /// <summary>
        /// Spins the event loop.
        /// </summary>
        void Spin();

        /// <summary>
        /// Cancels all running and remaining tasks.
        /// </summary>
        void CancelAll();
    }
}
