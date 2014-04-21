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
        /// Gets or sets the used request method.
        /// </summary>
        HttpMethod Method { get; set; }

        /// <summary>
        /// Gets or sets the specified request url.
        /// </summary>
        Uri Address { get; set; }

        /// <summary>
        /// Gets or sets the headers to send with the request.
        /// </summary>
        Dictionary<String, String> Headers { get; set; }

        /// <summary>
        /// Gets or sets the content to send with the request.
        /// </summary>
        Stream Content { get; set; }
    }
}
