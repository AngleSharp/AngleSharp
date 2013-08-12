using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML li, dd or dt tag.
    /// </summary>
    [DOM("HTMLLIElement")]
    public sealed class HTMLLIElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The li tag.
        /// </summary>
        internal const String ItemTag = "li";

        /// <summary>
        /// The dd tag.
        /// </summary>
        internal const String DefinitionTag = "dd";

        /// <summary>
        /// The dt tag.
        /// </summary>
        internal const String DescriptionTag = "dt";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new item tag.
        /// </summary>
        internal HTMLLIElement()
        {
            _name = ItemTag;
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
