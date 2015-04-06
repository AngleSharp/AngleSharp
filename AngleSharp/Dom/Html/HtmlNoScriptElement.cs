namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents a noscript HTML element.
    /// </summary>
    sealed class HtmlNoScriptElement : HtmlElement
    {
        #region ctor

        public HtmlNoScriptElement(Document owner, String prefix = null)
            : base(owner, Tags.NoScript, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }

        #endregion
    }
}
