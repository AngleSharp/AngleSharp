using System;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLUnderlineElement : HTMLElement, IFormatting
    {
        internal HTMLUnderlineElement()
        {
            _name = Tags.U;
        }
    }
}
