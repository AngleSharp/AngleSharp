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

        public HtmlSourceElement(Document owner, String prefix = null)
            : base(owner, TagNames.Source, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        public String Source
        {
            get { return this.GetUrlAttribute(AttributeNames.Src); }
            set { this.SetOwnAttribute(AttributeNames.Src, value); }
        }

        public String Media
        {
            get { return this.GetOwnAttribute(AttributeNames.Media); }
            set { this.SetOwnAttribute(AttributeNames.Media, value); }
        }

        public String Type
        {
            get { return this.GetOwnAttribute(AttributeNames.Type); }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        public String SourceSet
        {
            get { return this.GetOwnAttribute(AttributeNames.SrcSet); }
            set { this.SetOwnAttribute(AttributeNames.SrcSet, value); }
        }

        public String Sizes
        {
            get { return this.GetOwnAttribute(AttributeNames.Sizes); }
            set { this.SetOwnAttribute(AttributeNames.Sizes, value); }
        }

        #endregion
    }
}
