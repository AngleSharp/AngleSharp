namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents a noscript HTML element.
    /// </summary>
    sealed class HtmlNoScriptElement : HtmlElement
    {
        public HtmlNoScriptElement(Document owner, String? prefix = null)
            : base(owner, TagNames.NoScript, prefix, GetFlags(owner.Context))
        {
        }

        private static NodeFlags GetFlags(IBrowsingContext context)
        {
            if (context.IsScripting())
            {
                return NodeFlags.Special | NodeFlags.LiteralText;
            }

            return NodeFlags.Special;
        }
    }
}
