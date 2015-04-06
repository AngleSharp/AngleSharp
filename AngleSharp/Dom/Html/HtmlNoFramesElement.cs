namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents a noframes HTML element.
    /// </summary>
    sealed class HtmlNoFramesElement : HtmlElement
    {
        #region ctor

        public HtmlNoFramesElement(Document owner, String prefix = null)
            : base(owner, Tags.NoFrames, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }

        #endregion
    }
}
