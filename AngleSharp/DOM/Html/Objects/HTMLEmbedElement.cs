namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the embed element.
    /// </summary>
    [DOM("HTMLEmbedElement")]
    public sealed class HTMLEmbedElement : HTMLElement
    {
        #region ctor

        /// <summary>
        /// Creates a new Embed element.
        /// </summary>
        internal HTMLEmbedElement()
        {
            _name = Tags.Embed;
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
