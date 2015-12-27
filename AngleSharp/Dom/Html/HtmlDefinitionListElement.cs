namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML dl element.
    /// </summary>
    sealed class HtmlDefinitionListElement : HtmlElement
    {
        public HtmlDefinitionListElement(Document owner, String prefix = null)
            : base(owner, TagNames.Dl, prefix, NodeFlags.Special)
        {
        }
    }
}
