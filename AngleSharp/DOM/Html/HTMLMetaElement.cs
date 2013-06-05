using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML meta element.
    /// </summary>
    public class HTMLMetaElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The meta tag.
        /// </summary>
        public const string Tag = "meta";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML meta element.
        /// </summary>
        public HTMLMetaElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the content attribute, with associated information.
        /// </summary>
        public string Content
        {
            get { return GetAttribute("content"); }
            set { SetAttribute("content", value); }
        }

        /// <summary>
        /// Gets or sets the HTTP response header name.
        /// </summary>
        public string HttpEquiv
        {
            get { return GetAttribute("http-equiv"); }
            set { SetAttribute("http-equiv", value); }
        }

        /// <summary>
        /// Gets or sets the select form of content.
        /// </summary>
        public string Scheme
        {
            get { return GetAttribute("scheme"); }
            set { SetAttribute("scheme", value); }
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
