namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents a noembed HTML element.
    /// </summary>
    sealed class HTMLNoEmbedElement : HTMLElement
    {
        #region ctor

        public HTMLNoEmbedElement(Document owner)
            : base(Tags.NoEmbed, NodeFlags.Special | NodeFlags.LiteralText)
        {
            Owner = owner;
        }

        #endregion
    }
}
