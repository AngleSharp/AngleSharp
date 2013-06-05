using System;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Any DOMCollection has to derive from this class.
    /// </summary>
    public abstract class DOMCollection
    {
        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the nodelist.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public abstract string ToHtml();

        #endregion
    }
}
