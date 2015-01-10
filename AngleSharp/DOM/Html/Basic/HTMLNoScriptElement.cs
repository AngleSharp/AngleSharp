namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents a noscript HTML element.
    /// </summary>
    sealed class HTMLNoScriptElement : HTMLElement
    {
        #region ctor

        public HTMLNoScriptElement(Document owner)
            : base(Tags.NoScript, NodeFlags.Special | NodeFlags.LiteralText)
        {
            Owner = owner;
        }

        #endregion
    }
}
