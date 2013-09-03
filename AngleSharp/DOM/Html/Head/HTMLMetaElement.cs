using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML meta element.
    /// </summary>
    [DOM("HTMLMetaElement")]
    public sealed class HTMLMetaElement : HTMLElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML meta element.
        /// </summary>
        internal HTMLMetaElement()
        {
            _name = Tags.META;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the content attribute, with associated information.
        /// </summary>
        [DOM("content")]
        public String Content
        {
            get { return GetAttribute(AttributeNames.CONTENT); }
            set { SetAttribute(AttributeNames.CONTENT, value); }
        }

        /// <summary>
        /// Gets or sets the HTTP response header name.
        /// </summary>
        [DOM("httpEquiv")]
        public String HttpEquiv
        {
            get { return GetAttribute(AttributeNames.HTTP_EQUIV); }
            set { SetAttribute(AttributeNames.HTTP_EQUIV, value); }
        }

        /// <summary>
        /// Gets or sets the select form of content.
        /// </summary>
        [DOM("scheme")]
        public String Scheme
        {
            get { return GetAttribute(AttributeNames.SCHEME); }
            set { SetAttribute(AttributeNames.SCHEME, value); }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        [DOM("name")]
        public String Name
        {
            get { return GetAttribute(AttributeNames.NAME); }
            set { SetAttribute(AttributeNames.NAME, value); }
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
