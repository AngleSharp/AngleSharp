namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the HTML html element.
    /// </summary>
    sealed class HtmlHtmlElement : HtmlElement, IHtmlHtmlElement
    {
        #region ctor

        public HtmlHtmlElement(Document owner, String prefix = null)
            : base(owner, TagNames.Html, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
        {
        }

        #endregion

        #region Properties

        public String Manifest
        {
            get => this.GetOwnAttribute(AttributeNames.Manifest);
            set => this.SetOwnAttribute(AttributeNames.Manifest, value);
        }

        #endregion
    }
}
