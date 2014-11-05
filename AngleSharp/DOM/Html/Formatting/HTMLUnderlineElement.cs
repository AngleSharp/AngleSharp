namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The u HTML element.
    /// </summary>
    sealed class HTMLUnderlineElement : HTMLElement
    {
        internal HTMLUnderlineElement()
            : base(Tags.U, NodeFlags.HtmlFormatting)
        {
        }
    }
}
