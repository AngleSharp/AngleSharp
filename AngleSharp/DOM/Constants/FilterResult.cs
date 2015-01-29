namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// An enumeration that contains the valid results of examining a node.
    /// </summary>
    public enum FilterResult : ushort
    {
        /// <summary>
        /// The Node is accepted.
        /// </summary>
        [DomName("FILTER_ACCEPT")]
        Accept = 1,
        /// <summary>
        /// The Node (and its children) are rejected.
        /// </summary>
        [DomName("FILTER_REJECT")]
        Reject = 2,
        /// <summary>
        /// The Node is skipped. Children are still considered.
        /// </summary>
        [DomName("FILTER_SKIP")]
        Skip = 3
    }
}
