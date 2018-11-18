namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents a noscript HTML element.
    /// </summary>
    sealed class HtmlNoScriptElement : HtmlElement
    {
        public HtmlNoScriptElement(Document owner, String prefix = null)
            : base(owner, TagNames.NoScript, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
