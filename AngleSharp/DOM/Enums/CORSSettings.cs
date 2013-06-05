using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// This enumeration controls whether error information will be exposed for cross origins.
    /// </summary>
    public enum CORSSettings : ushort
    {
        /// <summary>
        /// Cross-origin CORS requests for the element will have the omit credentials flag set.
        /// </summary>
        Anonymous = 0,
        /// <summary>
        /// Cross-origin CORS requests for the element will not have the omit credentials flag set
        /// </summary>
        UseCredentials = 1
    }
}
