namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents a noframes HTML element.
    /// </summary>
    sealed class HTMLNoFramesElement : HTMLElement
    {
        #region ctor

        public HTMLNoFramesElement(Document owner)
            : base(owner, Tags.NoFrames, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }

        #endregion
    }
}
