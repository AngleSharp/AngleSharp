namespace AngleSharp.Network
{
    /// <summary>
    /// The default origin behavior states.
    /// </summary>
    public enum OriginBehavior
    {
        /// <summary>
        /// Data is gathered.
        /// </summary>
        Taint,
        /// <summary>
        /// Data is discarded in NO CORS.
        /// </summary>
        Fail
    }
}
