using AngleSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace AngleSharp
{
    /// <summary>
    /// The default HTTP request encapsulation type.
    /// </summary>
    sealed class DefaultHttpRequest : IHttpRequest
    {
        #region ctor

        public DefaultHttpRequest()
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
