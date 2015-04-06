namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The tt HTML element.
    /// </summary>
    sealed class HtmlTeletypeTextElement : HtmlElement
    {
        public HtmlTeletypeTextElement(Document owner, String prefix = null)
            : base(owner, Tags.Tt, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
