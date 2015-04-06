namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The em HTML element.
    /// </summary>
    sealed class HtmlEmphasizeElement : HtmlElement
    {
        public HtmlEmphasizeElement(Document owner, String prefix)
            : base(owner, Tags.Em, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
