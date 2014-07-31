namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The strong HTML element.
    /// </summary>
    sealed class HTMLStrongElement : HTMLElement
    {
        internal HTMLStrongElement()
            : base(Tags.Strong, NodeFlags.HtmlFormatting)
        {
        }
    }
}
