namespace AngleSharp.Io
{
    /// <summary>
    /// Represents the usable methods for transmitting HTTP forms.
    /// </summary>
    public enum HttpMethod : byte
    {
        /// <summary>
        /// The GET method.
        /// </summary>
        Get,
        /// <summary>
        /// The POST method.
        /// </summary>
        Post,
        /// <summary>
        /// The PUT method.
        /// </summary>
        Put,
        /// <summary>
        /// The DELETE method.
        /// </summary>
        Delete,
        /// <summary>
        /// The OPTIONS method.
        /// </summary>
        Options,
        /// <summary>
        /// The HEAD method.
        /// </summary>
        Head,
        /// <summary>
        /// The TRACE method.
        /// </summary>
        Trace,
        /// <summary>
        /// The CONNECT method.
        /// </summary>
        Connect
    }
}
