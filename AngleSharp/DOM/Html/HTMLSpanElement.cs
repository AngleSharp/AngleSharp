using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLSpanElement : HTMLElement
    {
        public const string Tag = "span";

        public HTMLSpanElement()
        {
            NodeName = Tag;
        }

        protected internal override bool IsSpecial
        {
            get
            {
                return false;
            }
        }
    }
}
