namespace AngleSharp.Dom
{
    /// <summary>
    /// Defines the document readiness.
    /// </summary>
    public enum DocumentReadyState : byte
    {
        /// <summary>
        /// The document is still loading.
        /// </summary>
        Loading,
        /// <summary>
        /// The document is interactive, i.e. interaction possible.
        /// </summary>
        Interactive,
        /// <summary>
        /// Loading is complete.
        /// </summary>
        Complete
    }
}
