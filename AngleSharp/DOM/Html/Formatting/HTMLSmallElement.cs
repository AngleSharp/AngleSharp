namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The small HTML element.
    /// </summary>
    sealed class HTMLSmallElement : HTMLElement
    {
        internal HTMLSmallElement()
            : base(Tags.Small, NodeFlags.HtmlFormatting)
        {
        }
    }
}
