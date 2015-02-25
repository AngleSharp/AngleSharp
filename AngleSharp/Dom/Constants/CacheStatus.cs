namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// An enumeration over all possible cache states.
    /// </summary>
    public enum CacheStatus : ushort
    {
        /// <summary>
        /// The resource is uncached.
        /// </summary>
        [DomName("UNCACHED")]
        Uncached = 0,
        /// <summary>
        /// The cache status checker is idle.
        /// </summary>
        [DomName("IDLE")]
        Idle = 1,
        /// <summary>
        /// The cache status is being checked.
        /// </summary>
        [DomName("CHECKING")]
        Checking = 2,
        /// <summary>
        /// The resource is being downloaded.
        /// </summary>
        [DomName("DOWNLOADING")]
        Downloading = 3,
        /// <summary>
        /// An update for the resource is available.
        /// </summary>
        [DomName("UPDATEREADY")]
        UpdateReady = 4,
        /// <summary>
        /// The resource is practically obsolete.
        /// </summary>
        [DomName("OBSOLETE")]
        Obsolete = 5
    }
}
