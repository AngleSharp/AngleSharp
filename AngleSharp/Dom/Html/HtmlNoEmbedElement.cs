namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents a noembed HTML element.
    /// </summary>
    sealed class HtmlNoEmbedElement : HtmlElement
    {
        #region ctor

        public HtmlNoEmbedElement(Document owner)
            : base(owner, Tags.NoEmbed, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }

        #endregion
    }
}
