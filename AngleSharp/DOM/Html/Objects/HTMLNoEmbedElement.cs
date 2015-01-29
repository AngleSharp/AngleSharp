namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents a noembed HTML element.
    /// </summary>
    sealed class HTMLNoEmbedElement : HTMLElement
    {
        #region ctor

        public HTMLNoEmbedElement(Document owner)
            : base(owner, Tags.NoEmbed, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }

        #endregion
    }
}
