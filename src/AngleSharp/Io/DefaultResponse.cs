﻿namespace AngleSharp.Io
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    /// <summary>
    /// The default HTTP response encapsulation object.
    /// </summary>
    public sealed class DefaultResponse : IResponse
    {
        #region ctor

        /// <summary>
        /// Creates a new default response object.
        /// </summary>
        public DefaultResponse()
        {
            Headers = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);
            StatusCode = HttpStatusCode.Accepted;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the url of the response.
        /// </summary>
        public Url Address
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the headers (key-value pairs) of the response.
        /// </summary>
        public IDictionary<String, String> Headers
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a stream for content of the response.
        /// </summary>
        public Stream Content
        {
            get;
            set;
        }

        #endregion

        #region Methods

        void IDisposable.Dispose()
        {
            Content?.Dispose();
            Headers.Clear();
        }

        #endregion
    }
}
