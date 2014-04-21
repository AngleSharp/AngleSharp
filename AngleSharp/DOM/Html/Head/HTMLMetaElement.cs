namespace AngleSharp.DOM.Html
{
    using System;

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
            _name = Tags.Meta;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the content attribute, with associated information.
        /// </summary>
        [DOM("content")]
        public String Content
        {
            get { return GetAttribute(AttributeNames.Content); }
            set { SetAttribute(AttributeNames.Content, value); }
        }

        /// <summary>
        /// Gets or sets the HTTP response header name.
        /// </summary>
        [DOM("httpEquiv")]
        public String HttpEquiv
        {
            get { return GetAttribute(AttributeNames.HttpEquiv); }
            set { SetAttribute(AttributeNames.HttpEquiv, value); }
        }

        /// <summary>
        /// Gets or sets the select form of content.
        /// </summary>
        [DOM("scheme")]
        public String Scheme
        {
            get { return GetAttribute(AttributeNames.Scheme); }
            set { SetAttribute(AttributeNames.Scheme, value); }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        [DOM("name")]
        public String Name
        {
            get { return GetAttribute(AttributeNames.Name); }
            set { SetAttribute(AttributeNames.Name, value); }
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
