using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML base element.
    /// </summary>
    public sealed class HTMLBaseElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The base tag.
        /// </summary>
        internal const string Tag = "base";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a HTML base element.
        /// </summary>
        internal HTMLBaseElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the base URI.
        /// </summary>
        public string Href
        {
            get { return GetAttribute("href"); }
            set { SetAttribute("href", value); }
        }

        /// <summary>
        /// Gets or sets the default target frame.
        /// </summary>
        public string Target
        {
            get { return GetAttribute("target"); }
            set { SetAttribute("target", value); }
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
