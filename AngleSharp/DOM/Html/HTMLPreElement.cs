using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLPreElement : HTMLElement
    {
        public const string Tag = "pre";

        public HTMLPreElement()
        {
            NodeName = Tag;
        }

        protected internal override bool IsSpecial
        {
            get
            {
                return true;
            }
        }
    }
}
