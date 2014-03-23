using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using AngleSharp.Interfaces;

namespace AngleSharp
{
    /// <summary>
    /// The default HTTP response encapsulation object.
    /// </summary>
    sealed class DefaultHttpResponse : IHttpResponse
    {
        #region ctor

        public DefaultHttpResponse()
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
