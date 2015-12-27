namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML source element.
    /// </summary>
    sealed class HtmlSourceElement : HtmlElement, IHtmlSourceElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML source element.
        /// </summary>
        public HtmlSourceElement(Document owner, String prefix = null)
            : base(owner, Tags.Source, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the URL for the media resource.
        /// </summary>
        public String Source
        {
            get { return this.GetUrlAttribute(AttributeNames.Src); }
            set { this.SetOwnAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the intended type of the media resource.
        /// </summary>
        public String Media
        {
            get { return this.GetOwnAttribute(AttributeNames.Media); }
            set { this.SetOwnAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the type of the media source.
        /// </summary>
        public String Type
        {
            get { return this.GetOwnAttribute(AttributeNames.Type); }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets the source of an HTML picture element.
        /// </summary>
        public String SourceSet
        {
            get { return this.GetOwnAttribute(AttributeNames.SrcSet); }
            set { this.SetOwnAttribute(AttributeNames.SrcSet, value); }
        }

        /// <summary>
        /// Gets or sets the sizes to use for an HTML picture element.
        /// </summary>
        public String Sizes
        {
            get { return this.GetOwnAttribute(AttributeNames.Sizes); }
            set { this.SetOwnAttribute(AttributeNames.Sizes, value); }
        }

        #endregion
    }
}
