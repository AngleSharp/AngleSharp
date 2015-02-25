namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML span element.
    /// </summary>
    sealed class HtmlSpanElement : HtmlElement, IHtmlSpanElement
    {
        #region ctor

        public HtmlSpanElement(Document owner)
            : base(owner, Tags.Span)
        {
            Owner = owner;
        }

        #endregion
    }
}
