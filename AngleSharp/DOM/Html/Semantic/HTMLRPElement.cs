using System;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLRPElement : HTMLElement, IImplClosed
    {
        internal HTMLRPElement()
        {
            _name = Tags.RP;
        }
    }
}
