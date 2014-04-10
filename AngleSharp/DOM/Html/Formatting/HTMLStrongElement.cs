using System;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLStrongElement : HTMLElement, IFormatting
    {
        internal HTMLStrongElement()
        {
            _name = Tags.Strong;
        }
    }
}
