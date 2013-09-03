using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AngleSharp.Interfaces
{
    /// <summary>
    /// Specifies what is stored when receiving data.
    /// </summary>
    public interface IHttpResponse
    {
        /// <summary>
        /// Gets the status code that has been send with the response.
        /// </summary>
        HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets the headers that have been send with the response.
        /// </summary>
        Dictionary<String, String> Headers { get; set; }

        /// <summary>
        /// Gets the content that has been send with the response.
        /// </summary>
        Stream Content { get; set; }
    }
}
