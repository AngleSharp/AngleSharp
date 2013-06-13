using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML li, dd or dt tag.
    /// </summary>
    public sealed class HTMLLIElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The li tag.
        /// </summary>
        internal const string ItemTag = "li";

        /// <summary>
        /// The dd tag.
        /// </summary>
        internal const string DefinitionTag = "dd";

        /// <summary>
        /// The dt tag.
        /// </summary>
        internal const string DescriptionTag = "dt";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new item tag.
        /// </summary>
        public HTMLLIElement()
        {
            _name = ItemTag;
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
