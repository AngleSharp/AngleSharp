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
            : base(owner, Tags.Dl, prefix, NodeFlags.Special)
        {
        }
    }
}
