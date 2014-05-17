namespace AngleSharp.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    /// <summary>
    /// The default HTTP response encapsulation object.
    /// </summary>
    public class DefaultResponse : IResponse
    {
        #region ctor

        /// <summary>
        /// Creates a new default response object.
        /// </summary>
        public DefaultResponse()
        {
            Headers = new Dictionary<String, String>();
            StatusCode = HttpStatusCode.Accepted;
        }

        #endregion

        #region Properties

        public HttpStatusCode StatusCode
        {
            get;
            set;
        }

        public Dictionary<String, String> Headers
        {
            get;
            set;
        }

        public Stream Content
        {
            get;
            set;
        }

        #endregion
    }
}
