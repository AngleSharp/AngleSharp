namespace AngleSharp.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    /// <summary>
    /// The default HTTP response encapsulation object.
    /// </summary>
    public class DefaultHttpResponse : IHttpResponse
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
