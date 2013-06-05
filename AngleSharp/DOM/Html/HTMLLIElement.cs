using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML li, dd or dt tag.
    /// </summary>
    public class HTMLLIElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The li tag.
        /// </summary>
        public const string ItemTag = "li";

        /// <summary>
        /// The dd tag.
        /// </summary>
        public const string DefinitionTag = "dd";

        /// <summary>
        /// The dt tag.
        /// </summary>
        public const string DescriptionTag = "dt";

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

        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
