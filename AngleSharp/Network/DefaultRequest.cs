namespace AngleSharp.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The default HTTP request encapsulation type.
    /// </summary>
    public class DefaultRequest : IRequest
    {
        #region ctor

        /// <summary>
        /// Creates a new default requests.
        /// </summary>
        public DefaultRequest()
        {
            Headers = new Dictionary<String, String>();
        }

        #endregion

        #region Properties

        public HttpMethod Method
        {
            get;
            set;
        }

        public Uri Address
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
