namespace AngleSharp.Services
{
    using AngleSharp.Infrastructure;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an event loop.
    /// See 7.1.4.2 Processing model.
    /// </summary>
    public interface IEventService : IService, IDisposable
    {
        /// <summary>
        /// Enqueues a given task with the associated document.
        /// </summary>
        /// <param name="task">The task to enqueue.</param>
        void Enqueue<T>(T task)
            where T : DomTask;

        /// <summary>
        /// Gets the currently running task.
        /// </summary>
        Task Current { get; }

        /// <summary>
        /// Performs a microtask checkpoint, which cleans up pending
        /// microtasks.
        /// </summary>
        /// <returns>An awaitable task.</returns>
        Task Check();

        /// <summary>
        /// Executes the compound subtask by invoking the series of
        /// steps from a microtask source.
        /// </summary>
        /// <param name="steps">The steps to run.</param>
        /// <returns>An awaitable task.</returns>
        Task Execute(Action steps);

        /// <summary>
        /// Spins the event loop until the provided condition is met.
        /// </summary>
        /// <param name="condition">The condition that needs to be fulfilled.</param>
        /// <returns>An awaitable task.</returns>
        Task Spin(Task<Boolean> condition);
    }
}
