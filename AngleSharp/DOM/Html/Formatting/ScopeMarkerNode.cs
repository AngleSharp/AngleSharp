using System;

namespace AngleSharp.DOM.Html
{
    class ScopeMarkerNode : HTMLFormattingElement
    {
        static readonly ScopeMarkerNode element = new ScopeMarkerNode();

        public static ScopeMarkerNode Element
        {
            get { return element; }
        }
    }
}
