namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The nobr HTML element.
    /// </summary>
    sealed class HTMLNoNewlineElement : HTMLElement
    {
        internal HTMLNoNewlineElement()
            : base(Tags.NoBr, NodeFlags.HtmlFormatting)
        {
        }
    }
}
