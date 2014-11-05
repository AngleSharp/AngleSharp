namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

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
