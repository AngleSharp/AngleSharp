using AngleSharp.DOM.Css;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML link element.
    /// </summary>
    public sealed class HTMLLinkElement : HTMLElement, IStyleSheet
    {
        #region Constant

        /// <summary>
        /// The link tag.
        /// </summary>
        internal const string Tag = "link";

        #endregion

        #region Members

        StyleSheet _sheet;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML link element.
        /// </summary>
        internal HTMLLinkElement()
        {
            _name = Tag;
            Type = "text/css";
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

        #region Design properties

        /// <summary>
        /// Gets or sets if the link has been visited.
        /// </summary>
        internal bool IsVisited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the link is currently active.
        /// </summary>
        internal bool IsActive
        {
            get;
            set;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the URI for the target resource.
        /// </summary>
        public string Href
        {
            get { return GetAttribute("href"); }
            set { SetAttribute("href", value); }
        }

        /// <summary>
        /// Gets or sets the language code for the linked resource.
        /// </summary>
        public string Hreflang
        {
            get { return GetAttribute("hreflang"); }
            set { SetAttribute("hreflang", value); }
        }

        /// <summary>
        /// Gets or sets the character encoding for the target resource.
        /// </summary>
        public string Charset
        {
            get { return GetAttribute(HtmlEncoding.CHARSET); }
            set { SetAttribute(HtmlEncoding.CHARSET, value); }
        }

        /// <summary>
        /// Gets or sets the forward relationship of the linked resource from the document to the resource.
        /// </summary>
        public string Rel
        {
            get { return GetAttribute("rel"); }
            set { SetAttribute("rel", value); }
        }

        /// <summary>
        /// Gets or sets the reverse relationship of the linked resource from the document to the resource.
        /// </summary>
        public string Rev
        {
            get { return GetAttribute("rev"); }
            set { SetAttribute("rev", value); }
        }

        /// <summary>
        /// Gets or sets if the stylesheet is enabled or disabled.
        /// </summary>
        public bool Disabled
        {
            get { return Sheet.Disabled; }
            set { Sheet.Disabled = value; }
        }

        /// <summary>
        /// Gets or sets the name of the target frame to which the resource applies.
        /// </summary>
        public string Target
        {
            get { return GetAttribute("target"); }
            set { SetAttribute("target", value); }
        }

        /// <summary>
        /// Gets or sets the use with one or more target media.
        /// </summary>
        public string Media
        {
            get { return GetAttribute("media"); }
            set { SetAttribute("media", value); }
        }

        /// <summary>
        /// Gets or sets the content type of the style sheet language.
        /// </summary>
        public string Type
        {
            get { return GetAttribute("type"); }
            set { SetAttribute("type", value); }
        }

        /// <summary>
        /// Gets the associated stylesheet.
        /// </summary>
        public StyleSheet Sheet
        {
            get { return _sheet ?? (_sheet = Builder.Style(this)); }
        }

        #endregion
    }
}
