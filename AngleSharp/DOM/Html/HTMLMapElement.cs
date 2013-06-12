using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLMapElement : HTMLElement
    {
        public const string Tag = "map";

        internal HTMLMapElement()
        {
            _name = Tag;
        }
    }
}
