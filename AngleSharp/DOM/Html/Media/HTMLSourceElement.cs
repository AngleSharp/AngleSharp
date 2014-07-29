namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML source element.
    /// </summary>
    sealed class HTMLSourceElement : HTMLElement, IHtmlSourceElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML source element.
        /// </summary>
        internal HTMLSourceElement()
            : base(Tags.Source, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the URL for the media resource.
        /// </summary>
        public String Source
        {
            get { return GetAttribute(AttributeNames.Src); }
            set { SetAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the intended type of the media resource.
        /// </summary>
        public String Media
        {
            get { return GetAttribute(AttributeNames.Media); }
            set { SetAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the type of the media source.
        /// </summary>
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        #endregion
    }
}
