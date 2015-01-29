namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents a noframes HTML element.
    /// </summary>
    sealed class HtmlNoFramesElement : HtmlElement
    {
        #region ctor

        public HtmlNoFramesElement(Document owner)
            : base(owner, Tags.NoFrames, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }

        #endregion
    }
}
