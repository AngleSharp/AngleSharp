namespace AngleSharp.DOM
{
    /// <summary>
    /// Defines the document readiness.
    /// </summary>
    [DomName("DocumentReadyState")]
    public enum DocumentReadyState : ushort
    {
        /// <summary>
        /// The document is still loading.
        /// </summary>
        [DomName("loading")]
        Loading,
        /// <summary>
        /// The document is interactive, i.e. interaction possible.
        /// </summary>
        [DomName("interactive")]
        Interactive,
        /// <summary>
        /// Loading is complete.
        /// </summary>
        [DomName("complete")]
        Complete
    }
}
