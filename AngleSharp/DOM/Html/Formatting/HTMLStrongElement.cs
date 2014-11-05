namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

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
