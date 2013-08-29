using System;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLRTElement : HTMLElement, IImpliedEnd
    {
        internal HTMLRTElement()
        {
            _name = Tags.RT;
        }
    }
}
