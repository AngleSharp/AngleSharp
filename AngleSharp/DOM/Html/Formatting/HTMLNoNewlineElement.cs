using System;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLNoNewlineElement : HTMLElement, IFormatting
    {
        internal HTMLNoNewlineElement()
        {
            _name = Tags.NoBr;
        }
    }
}
