namespace AngleSharp.DOM.Html
{
    sealed class HTMLCodeElement : HTMLElement, IFormatting
    {
        internal HTMLCodeElement()
        {
            _name = Tags.Code;
        }
    }
}
