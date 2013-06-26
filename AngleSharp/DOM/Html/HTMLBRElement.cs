using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML br element.
    /// </summary>
    [DOM("HTMLBRElement")]
    public sealed class HTMLBRElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The br tag.
        /// </summary>
        internal const String Tag = "br";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML br element
        /// </summary>
        internal HTMLBRElement()
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
