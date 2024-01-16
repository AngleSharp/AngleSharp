namespace AngleSharp.Io
{
    /// <summary>
    /// The default origin behavior states.
    /// </summary>
    public enum OriginBehavior : System.Byte
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
