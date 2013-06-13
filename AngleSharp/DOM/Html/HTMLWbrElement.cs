using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML wbr element.
    /// </summary>
    sealed class HTMLWbrElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The wbr tag.
        /// </summary>
        internal const string Tag = "wbr";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML wbr element.
        /// </summary>
        public HTMLWbrElement()
        {
            _name = Tag;
        }

        #endregion

        #region Internal properties

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
