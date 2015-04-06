namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML caption element.
    /// </summary>
    sealed class HtmlTableCaptionElement : HtmlElement, IHtmlTableCaptionElement
    {
        #region ctor

        public HtmlTableCaptionElement(Document owner, String prefix = null)
            : base(owner, Tags.Caption, prefix, NodeFlags.Special | NodeFlags.Scoped)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        public String Align
        {
            get { return GetOwnAttribute(AttributeNames.Align) ?? Keywords.Top; }
            set { SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        #endregion
    }
}
