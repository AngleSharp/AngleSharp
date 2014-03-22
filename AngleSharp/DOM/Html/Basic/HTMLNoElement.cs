using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a no* HTML element (noembed, noscript, noframes).
    /// </summary>
    sealed class HTMLNoElement : HTMLElement
    {
        #region ctor

        internal HTMLNoElement()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
