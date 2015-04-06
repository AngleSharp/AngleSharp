namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML source element.
    /// </summary>
    sealed class HtmlSourceElement : HtmlElement, IHtmlSourceElement
    {
        #region Fields

        readonly BoundLocation _src;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML source element.
        /// </summary>
        public HtmlSourceElement(Document owner, String prefix = null)
            : base(owner, Tags.Source, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            _src = new BoundLocation(this, AttributeNames.Src);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the URL for the media resource.
        /// </summary>
        public String Source
        {
            get { return _src.Href; }
            set { _src.Href = value; }
        }

        /// <summary>
        /// Gets or sets the intended type of the media resource.
        /// </summary>
        public String Media
        {
            get { return GetOwnAttribute(AttributeNames.Media); }
            set { SetOwnAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the type of the media source.
        /// </summary>
        public String Type
        {
            get { return GetOwnAttribute(AttributeNames.Type); }
            set { SetOwnAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets the source of an HTML picture element.
        /// </summary>
        public String SourceSet
        {
            get { return GetOwnAttribute(AttributeNames.SrcSet); }
            set { SetOwnAttribute(AttributeNames.SrcSet, value); }
        }

        /// <summary>
        /// Gets or sets the sizes to use for an HTML picture element.
        /// </summary>
        public String Sizes
        {
            get { return GetOwnAttribute(AttributeNames.Sizes); }
            set { SetOwnAttribute(AttributeNames.Sizes, value); }
        }

        #endregion
    }
}
