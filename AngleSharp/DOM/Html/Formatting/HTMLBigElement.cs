namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The big HTML element.
    /// </summary>
    sealed class HTMLBigElement : HTMLElement
    {
        internal HTMLBigElement()
            : base(Tags.Big, NodeFlags.HtmlFormatting)
        {
        }
    }
}
