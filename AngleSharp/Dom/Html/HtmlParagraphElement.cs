namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Extensions;
    using AngleSharp.Html;

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
            get { return this.GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left); }
            set { this.SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        #endregion
    }
}
