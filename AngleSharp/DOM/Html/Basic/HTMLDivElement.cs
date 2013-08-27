using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML div element.
    /// </summary>
    [DOM("HTMLDivElement")]
    public sealed class HTMLDivElement : HTMLElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML div element.
        /// </summary>
        internal HTMLDivElement()
        {
            _name = Tags.DIV;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
