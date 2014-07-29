namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The code HTML element.
    /// </summary>
    sealed class HTMLCodeElement : HTMLElement, IFormatting
    {
        internal HTMLCodeElement()
            : base(Tags.Code)
        {
        }
    }
}
