namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents a noembed HTML element.
    /// </summary>
    sealed class HtmlNoEmbedElement : HtmlElement
    {
        #region ctor

        public HtmlNoEmbedElement(Document owner, String prefix = null)
            : base(owner, Tags.NoEmbed, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }

        #endregion
    }
}
