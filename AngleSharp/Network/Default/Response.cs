namespace AngleSharp.Network.Default
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    /// <summary>
    /// The default HTTP response encapsulation object.
    /// </summary>
    sealed class Response : IResponse
    {
        #region ctor

        /// <summary>
        /// Creates a new default response object.
        /// </summary>
        public Response()
        {
            Headers = new Dictionary<String, String>();
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
        public Dictionary<String, String> Headers
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

        public void Dispose()
        {
            if (Content != null)
                Content.Dispose();

            Headers.Clear();
        }

        #endregion
    }
}
