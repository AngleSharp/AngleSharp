namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The tt HTML element.
    /// </summary>
    sealed class HtmlTeletypeTextElement : HtmlElement
    {
        public HtmlTeletypeTextElement(Document owner)
            : base(owner, Tags.Tt, NodeFlags.HtmlFormatting)
        {
        }
    }
}
