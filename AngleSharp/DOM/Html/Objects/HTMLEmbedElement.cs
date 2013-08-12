using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the embed element.
    /// </summary>
    [DOM("HTMLEmbedElement")]
    public sealed class HTMLEmbedElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The embed tag.
        /// </summary>
        internal const String Tag = "embed";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new Embed element.
        /// </summary>
        internal HTMLEmbedElement()
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
