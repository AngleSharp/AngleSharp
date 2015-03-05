namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML paragraph element.
    /// </summary>
    sealed class HtmlParagraphElement : HtmlElement, IHtmlParagraphElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML paragraph element.
        /// </summary>
        public HtmlParagraphElement(Document owner)
            : base(owner, Tags.P, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        public HorizontalAlignment Align
        {
            get { return GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left); }
            set { SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        #endregion
    }
}
