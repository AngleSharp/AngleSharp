using System;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLEmphasizeElement : HTMLElement, IFormatting
    {
        internal HTMLEmphasizeElement()
        {
            _name = Tags.EM;
        }
    }
}
