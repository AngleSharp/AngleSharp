using System;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLBigElement : HTMLElement, IFormatting
    {
        internal HTMLBigElement()
        {
            _name = Tags.BIG;
        }
    }
}
