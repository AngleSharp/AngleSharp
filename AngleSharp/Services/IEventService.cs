namespace AngleSharp.Services
{
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
        void Enqueue(Task task);

        /// <summary>
        /// Executes the compound subtask by invoking the series of
        /// steps from a microtask source.
        /// </summary>
        /// <param name="steps">The steps to run.</param>
        /// <returns>An awaitable task.</returns>
        Task Execute(Action steps);
    }
}
