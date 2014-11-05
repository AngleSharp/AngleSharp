namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

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
