namespace AngleSharp.DOM.Html
{
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
