using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML div element.
    /// </summary>
    [DOM("HTMLDivElement")]
    public sealed class HTMLDivElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The div element.
        /// </summary>
        internal const String Tag = "div";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML div element.
        /// </summary>
        internal HTMLDivElement()
        {
            _name = Tag;
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
