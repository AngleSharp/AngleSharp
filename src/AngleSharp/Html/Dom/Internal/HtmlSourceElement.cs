namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
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
            get => this.GetUrlAttribute(AttributeNames.Src);
            set => this.SetOwnAttribute(AttributeNames.Src, value);
        }

        public String Media
        {
            get => this.GetOwnAttribute(AttributeNames.Media);
            set => this.SetOwnAttribute(AttributeNames.Media, value);
        }

        public String Type
        {
            get => this.GetOwnAttribute(AttributeNames.Type);
            set => this.SetOwnAttribute(AttributeNames.Type, value);
        }

        public String SourceSet
        {
            get => this.GetOwnAttribute(AttributeNames.SrcSet);
            set => this.SetOwnAttribute(AttributeNames.SrcSet, value);
        }

        public String Sizes
        {
            get => this.GetOwnAttribute(AttributeNames.Sizes);
            set => this.SetOwnAttribute(AttributeNames.Sizes, value);
        }

        #endregion
    }
}
