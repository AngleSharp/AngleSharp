using System;

namespace AngleSharp
{
    /// <summary>
    /// Represents the usable methods for transmitting HTTP forms.
    /// </summary>
    public enum HttpMethod : ushort
    {
        /// <summary>
        /// The GET method.
        /// </summary>
        GET,
        /// <summary>
        /// The POST method.
        /// </summary>
        POST,
        /// <summary>
        /// The PUT method.
        /// </summary>
        PUT,
        /// <summary>
        /// The DELETE method.
        /// </summary>
        DELETE
    }
}
