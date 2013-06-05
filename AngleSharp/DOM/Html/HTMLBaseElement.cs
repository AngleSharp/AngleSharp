using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML base element.
    /// </summary>
    public class HTMLBaseElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The base tag.
        /// </summary>
        public const string Tag = "base";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a HTML base element.
        /// </summary>
        public HTMLBaseElement()
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

        protected internal override bool IsSpecial
        {
            get { return true; }
        }
        #endregion
    }
}
