using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML meta element.
    /// </summary>
    public sealed class HTMLMetaElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The meta tag.
        /// </summary>
        internal const string Tag = "meta";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML meta element.
        /// </summary>
        internal HTMLMetaElement()
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
