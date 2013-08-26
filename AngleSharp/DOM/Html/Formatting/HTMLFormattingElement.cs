using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the object for HTML formatting (b, big, strike, code,
    /// em, i, s, small, strong, u, tt, nobr) elements.
    /// </summary>
    public class HTMLFormattingElement : HTMLElement
    {
        #region ctor

        internal HTMLFormattingElement()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion
    }
}
