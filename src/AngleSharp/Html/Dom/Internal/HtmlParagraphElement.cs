namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the HTML paragraph element.
    /// </summary>
    sealed class HtmlParagraphElement : HtmlElement, IHtmlParagraphElement
    {
        #region ctor

        public HtmlParagraphElement(Document owner, String prefix = null)
            : base(owner, TagNames.P, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }

        #endregion

        #region Properties

        public HorizontalAlignment Align
        {
            get => this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left);
            set => this.SetOwnAttribute(AttributeNames.Align, value.ToString());
        }

        #endregion
    }
}
