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
        public const string Tag = "wbr";

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

        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
