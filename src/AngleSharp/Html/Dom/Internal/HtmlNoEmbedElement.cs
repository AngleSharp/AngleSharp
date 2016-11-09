namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents a noembed HTML element.
    /// </summary>
    sealed class HtmlNoEmbedElement : HtmlElement
    {
        public HtmlNoEmbedElement(Document owner, String prefix = null)
            : base(owner, TagNames.NoEmbed, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
