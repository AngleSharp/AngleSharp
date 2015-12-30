namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents a noframes HTML element.
    /// </summary>
    sealed class HtmlNoFramesElement : HtmlElement
    {
        public HtmlNoFramesElement(Document owner, String prefix = null)
            : base(owner, TagNames.NoFrames, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
