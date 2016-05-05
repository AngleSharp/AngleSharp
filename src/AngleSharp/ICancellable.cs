namespace AngleSharp
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a cancellable task.
    /// </summary>
    public interface ICancellable<T>
    {
        /// <summary>
        /// Cancels the covered task.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Gets if the task has already completed.
        /// </summary>
        Boolean IsCompleted { get; }

        /// <summary>
        /// Gets if the task is (still) running.
        /// </summary>
        Boolean IsRunning { get; }

        /// <summary>
        /// Gets the associated awaitable task.
        /// </summary>
        Task<T> Task { get; }
    }
}
