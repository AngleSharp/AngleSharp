namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents a noscript HTML element.
    /// </summary>
    sealed class HtmlNoScriptElement : HtmlElement
    {
        #region ctor

        public HtmlNoScriptElement(Document owner)
            : base(owner, Tags.NoScript, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }

        #endregion
    }
}
