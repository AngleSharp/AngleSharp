using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML wbr (word-break-opportunity) element.
    /// This element is used to indicate that the position is a good
    /// point for inserting a possible line-break.
    /// </summary>
    sealed class HTMLWbrElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The wbr tag.
        /// </summary>
        internal const String Tag = "wbr";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML wbr element.
        /// </summary>
        internal HTMLWbrElement()
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
