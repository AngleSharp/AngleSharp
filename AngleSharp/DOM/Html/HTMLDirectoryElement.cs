using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML dir element.
    /// </summary>
    [DOM("HTMLDirectoryElement")]
    public sealed class HTMLDirectoryElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The dir tag.
        /// </summary>
        internal const String Tag = "dir";

        #endregion

        #region ctor

        internal HTMLDirectoryElement()
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
