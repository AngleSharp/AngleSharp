namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;

    /// <summary>
    /// An enumeration of possible network states.
    /// </summary>
    public enum MediaNetworkState : ushort
    {
        /// <summary>
        /// The element has not yet been initialized.
        /// Everything is in initial state.
        /// </summary>
        [DomName("NETWORK_EMPTY")]
        Empty = 0,
        /// <summary>
        /// The element's resource selection alg. is active.
        /// No network usage at the moment, but nothing
        /// loaded.
        /// </summary>
        [DomName("NETWORK_IDLE")]
        Idle = 1,
        /// <summary>
        /// The download is in progress.
        /// </summary>
        [DomName("NETWORK_LOADING")]
        Loading = 2,
        /// <summary>
        /// The element's resource selection alg. is active,
        /// but has not yet found a resource to use.
        /// </summary>
        [DomName("NETWORK_NO_SOURCE")]
        NoSource = 3
    }
}
