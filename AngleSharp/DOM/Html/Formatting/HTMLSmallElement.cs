using System;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLSmallElement : HTMLElement, IFormatting
    {
        internal HTMLSmallElement()
        {
            _name = Tags.Small;
        }
    }
}
