using System;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLRPElement : HTMLElement, IImpliedEnd
    {
        internal HTMLRPElement()
        {
            _name = Tags.RP;
        }
    }
}
