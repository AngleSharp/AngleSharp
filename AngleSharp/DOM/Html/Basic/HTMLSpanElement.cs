namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML span element.
    /// </summary>
    sealed class HTMLSpanElement : HTMLElement, IHtmlSpanElement
    {
        #region ctor

        internal HTMLSpanElement()
            : base(Tags.Span)
        {
        }

        #endregion
    }
}
