namespace AngleSharp
{
    using System;

    /// <summary>
    /// Represents a listed event loop entry.
    /// </summary>
    public interface IEventLoopEntry
    {
        /// <summary>
        /// Gets if the entry is currently running.
        /// </summary>
        Boolean IsRunning { get; }

        /// <summary>
        /// Cancels the running entry.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Gets the starting time, if any.
        /// </summary>
        DateTime? Started { get; }
    }
}
