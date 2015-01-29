namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Enumeration with the various media error codes.
    /// </summary>
    public enum MediaErrorCode : ushort
    {
        /// <summary>
        /// The transfer has been aborted.
        /// </summary>
        [DomName("MEDIA_ERR_ABORTED")]
        Aborted = 1,
        /// <summary>
        /// The network is unreachable.
        /// </summary>
        [DomName("MEDIA_ERR_NETWORK")]
        Network = 2,
        /// <summary>
        /// The decoding process failed.
        /// </summary>
        [DomName("MEDIA_ERR_DECODE")]
        Decode = 3,
        /// <summary>
        /// The source format is not supported.
        /// </summary>
        [DomName("MEDIA_ERR_SRC_NOT_SUPPORTED")]
        SourceNotSupported = 4
    }
}
