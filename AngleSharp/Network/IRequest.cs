namespace AngleSharp.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Specifies what is used for requesting data.
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Gets the used request method.
        /// </summary>
        HttpMethod Method { get; }

        /// <summary>
        /// Gets the specified request url.
        /// </summary>
        Url Address { get; }

        /// <summary>
        /// Gets the headers to send with the request.
        /// </summary>
        Dictionary<String, String> Headers { get; }

        /// <summary>
        /// Gets content to send with the request.
        /// </summary>
        Stream Content { get; }
    }
}
