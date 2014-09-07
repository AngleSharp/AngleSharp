namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Provides a way to cache web resources to improve performance,
    /// reduce server loads, and enable access to content when there
    /// is no network connectivity.
    /// </summary>
    [DomName("ApplicationCache")]
    public interface IApplicationCache : IEventTarget
    {
        /// <summary>
        /// Gets the status of the application cache.
        /// </summary>
        [DomName("status")]
        CacheStatus Status { get; }

        /// <summary>
        /// Performs an update of the application cache.
        /// </summary>
        [DomName("update")]
        void Update();

        /// <summary>
        /// Aborts the current action (download / update) of the cache.
        /// </summary>
        [DomName("abort")]
        void Abort();

        /// <summary>
        /// Swaps the application's cache.
        /// </summary>
        [DomName("swapCache")]
        void Swap();

        /// <summary>
        /// Event triggered when the cache is being checked.
        /// </summary>
        [DomName("onchecking")]
        event EventListener Checking;

        /// <summary>
        /// Event triggered after an error occurred.
        /// </summary>
        [DomName("onerror")]
        event EventListener Error;

        /// <summary>
        /// Event triggered after no update is available.
        /// </summary>
        [DomName("onnoupdate")]
        event EventListener NoUpdate;

        /// <summary>
        /// Event triggered when the download started.
        /// </summary>
        [DomName("ondownloading")]
        event EventListener Downloading;

        /// <summary>
        /// Event triggered after progress.
        /// </summary>
        [DomName("onprogress")]
        event EventListener Progress;

        /// <summary>
        /// Event triggered after an update is ready to be applied.
        /// </summary>
        [DomName("onupdateready")]
        event EventListener UpdateReady;

        /// <summary>
        /// Event triggered after the cache has been updated.
        /// </summary>
        [DomName("oncached")]
        event EventListener Cached;

        /// <summary>
        /// Event triggered after the cache has been marked obsolete.
        /// </summary>
        [DomName("onobsolete")]
        event EventListener Obsolete;
    }
}
