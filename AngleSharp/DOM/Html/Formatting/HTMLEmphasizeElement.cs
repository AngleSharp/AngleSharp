namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The em HTML element.
    /// </summary>
    sealed class HTMLEmphasizeElement : HTMLElement
    {
        internal HTMLEmphasizeElement()
            : base(Tags.Em, NodeFlags.HtmlFormatting)
        {
        }
    }
}
