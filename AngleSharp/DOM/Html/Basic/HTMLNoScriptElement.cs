namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a noscript HTML element.
    /// </summary>
    sealed class HTMLNoScriptElement : HTMLElement
    {
        #region ctor

        internal HTMLNoScriptElement()
            : base(Tags.NoScript, NodeFlags.Special | NodeFlags.LiteralText)
        { }

        #endregion
    }
}
