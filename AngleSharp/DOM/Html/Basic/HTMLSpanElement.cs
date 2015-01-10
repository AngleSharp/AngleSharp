namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML span element.
    /// </summary>
    sealed class HTMLSpanElement : HTMLElement, IHtmlSpanElement
    {
        #region ctor

        public HTMLSpanElement(Document owner)
            : base(Tags.Span)
        {
            Owner = owner;
        }

        #endregion
    }
}
