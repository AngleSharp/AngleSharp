namespace AngleSharp.Network
{
    using AngleSharp.Dom;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Basic contract for a currently active download.
    /// </summary>
    public interface IDownload
    {
        /// <summary>
        /// Cancels the active download.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Gets if the download has already completed.
        /// </summary>
        Boolean IsCompleted { get; }

        /// <summary>
        /// Gets the target of the download.
        /// </summary>
        Url Target { get; }

        /// <summary>
        /// Gets if the download is (still) running.
        /// </summary>
        Boolean IsRunning { get; }

        /// <summary>
        /// Gets the originator of the download, if any.
        /// </summary>
        INode Originator { get; }

        /// <summary>
        /// Gets the associated awaitable task.
        /// </summary>
        Task<IResponse> Task { get; }
    }
}
