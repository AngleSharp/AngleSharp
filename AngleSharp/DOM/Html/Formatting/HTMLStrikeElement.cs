using System;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLStrikeElement : HTMLElement, IFormatting
    {
        internal HTMLStrikeElement()
        {
            _name = Tags.STRIKE;
        }
    }
}
