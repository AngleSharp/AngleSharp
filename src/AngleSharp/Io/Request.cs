﻿namespace AngleSharp.Io
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The default HTTP request encapsulation type.
    /// </summary>
    public sealed class Request
    {
        #region ctor

        /// <summary>
        /// Creates a new default requests.
        /// </summary>
        public Request()
        {
            Headers = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the method that should be used.
        /// </summary>
        public HttpMethod Method
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the address should be requested.
        /// </summary>
        public Url Address
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a list of headers (key-values) that should be used.
        /// </summary>
        public IDictionary<String, String> Headers
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a stream to the content (positioned at the origin).
        /// </summary>
        public Stream Content
        {
            get;
            set;
        }

        #endregion
    }
}
