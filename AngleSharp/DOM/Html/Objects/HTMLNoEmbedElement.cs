namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a noembed HTML element.
    /// </summary>
    sealed class HTMLNoEmbedElement : HTMLElement
    {
        #region ctor

        internal HTMLNoEmbedElement()
            : base(Tags.NoEmbed, NodeFlags.Special | NodeFlags.LiteralText)
        { }

        #endregion
    }
}
