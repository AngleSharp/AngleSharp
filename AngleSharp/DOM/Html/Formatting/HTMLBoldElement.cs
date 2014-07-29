namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The bold HTML element.
    /// </summary>
    sealed class HTMLBoldElement : HTMLElement, IFormatting
    {
        internal HTMLBoldElement()
            : base(Tags.B)
        {
        }
    }
}
