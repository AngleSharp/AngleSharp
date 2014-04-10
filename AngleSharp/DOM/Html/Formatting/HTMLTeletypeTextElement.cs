using System;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLTeletypeTextElement : HTMLElement, IFormatting
    {
        internal HTMLTeletypeTextElement()
        {
            _name = Tags.Tt;
        }
    }
}
