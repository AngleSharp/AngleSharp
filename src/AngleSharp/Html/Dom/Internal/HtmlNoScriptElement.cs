namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents a noscript HTML element.
    /// </summary>
    sealed class HtmlNoScriptElement : HtmlElement
    {
        public HtmlNoScriptElement(Document owner, String? prefix = null, Boolean? scripting = false)
            : base(owner, TagNames.NoScript, prefix, GetFlags(scripting ?? owner.Context.IsScripting()))
        {
        }

        private static NodeFlags GetFlags(Boolean scripting)
        {
            if (scripting)
            {
                return NodeFlags.Special | NodeFlags.LiteralText;
            }

            return NodeFlags.Special;
        }
    }
}
