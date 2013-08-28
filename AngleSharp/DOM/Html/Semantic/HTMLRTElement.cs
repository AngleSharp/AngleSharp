using System;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLRTElement : HTMLElement, IImplClosed
    {
        internal HTMLRTElement()
        {
            _name = Tags.RT;
        }
    }
}
