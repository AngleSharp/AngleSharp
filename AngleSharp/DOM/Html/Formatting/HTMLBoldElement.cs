namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The bold HTML element.
    /// </summary>
    sealed class HTMLBoldElement : HTMLElement
    {
        internal HTMLBoldElement()
            : base(Tags.B, NodeFlags.HtmlFormatting)
        {
        }
    }
}
