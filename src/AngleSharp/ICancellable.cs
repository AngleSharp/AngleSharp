namespace AngleSharp
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a cancellable task with result.
    /// </summary>
    public interface ICancellable<T> : ICancellable
    {
        /// <summary>
        /// Gets the associated awaitable task.
        /// </summary>
        Task<T> Task { get; }
    }

    /// <summary>
    /// Represents a cancellable task without result.
    /// </summary>
    public interface ICancellable
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
    }
}
