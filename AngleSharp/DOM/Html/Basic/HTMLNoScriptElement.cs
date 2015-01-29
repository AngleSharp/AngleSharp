namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents a noscript HTML element.
    /// </summary>
    sealed class HTMLNoScriptElement : HTMLElement
    {
        #region ctor

        public HTMLNoScriptElement(Document owner)
            : base(owner, Tags.NoScript, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }

        #endregion
    }
}
