namespace AngleSharp
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the DOM event loop.
    /// See 7.1.4.2 Processing model.
    /// </summary>
    public interface IEventLoop
    {
        /// <summary>
        /// Enqueues a given task with the associated document.
        /// </summary>
        /// <param name="task">The task to enqueue.</param>
        /// <returns>An awaitable task.</returns>
        Task Enqueue(Action task);

        /// <summary>
        /// Executes the compound subtask by invoking the series of
        /// steps from a microtask source.
        /// </summary>
        /// <param name="microtask">The steps to run.</param>
        /// <returns>An awaitable task.</returns>
        Task Execute(Action microtask);

        /// <summary>
        /// Potentially closes the IEventLoop.
        /// </summary>
        void Shutdown();
    }
}
