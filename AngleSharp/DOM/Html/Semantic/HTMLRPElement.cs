namespace AngleSharp.DOM.Html
{
    using System;

    sealed class HTMLRPElement : HTMLElement, IImpliedEnd
    {
        internal HTMLRPElement()
        {
            _name = Tags.RP;
        }
    }
}
