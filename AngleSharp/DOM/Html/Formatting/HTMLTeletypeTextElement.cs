namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The tt HTML element.
    /// </summary>
    sealed class HTMLTeletypeTextElement : HTMLElement
    {
        internal HTMLTeletypeTextElement()
            : base(Tags.Tt, NodeFlags.HtmlFormatting)
        {
        }
    }
}
