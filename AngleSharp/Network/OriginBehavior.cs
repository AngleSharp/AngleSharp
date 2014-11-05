namespace AngleSharp.Network
{
    /// <summary>
    /// The default origin behaviour states.
    /// </summary>
    enum OriginBehavior
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
