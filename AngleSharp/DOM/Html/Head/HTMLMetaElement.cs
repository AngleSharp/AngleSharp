namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML meta element.
    /// </summary>
    sealed class HTMLMetaElement : HTMLElement, IHtmlMetaElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML meta element.
        /// </summary>
        public HTMLMetaElement()
            : base(Tags.Meta, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the content attribute, with associated information.
        /// </summary>
        public String Content
        {
            get { return GetAttribute(AttributeNames.Content); }
            set { SetAttribute(AttributeNames.Content, value); }
        }

        /// <summary>
        /// Gets or sets the HTTP response header name.
        /// </summary>
        public String HttpEquivalent
        {
            get { return GetAttribute(AttributeNames.HttpEquiv); }
            set { SetAttribute(AttributeNames.HttpEquiv, value); }
        }

        /// <summary>
        /// Gets or sets the select form of content.
        /// </summary>
        public String Scheme
        {
            get { return GetAttribute(AttributeNames.Scheme); }
            set { SetAttribute(AttributeNames.Scheme, value); }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public String Name
        {
            get { return GetAttribute(AttributeNames.Name); }
            set { SetAttribute(AttributeNames.Name, value); }
        }

        #endregion
    }
}
