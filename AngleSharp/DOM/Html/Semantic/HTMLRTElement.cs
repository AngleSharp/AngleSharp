namespace AngleSharp.DOM.Html
{
    using System;

    sealed class HTMLRTElement : HTMLElement, IImpliedEnd
    {
        internal HTMLRTElement()
        {
            _name = Tags.RT;
        }
    }
}
