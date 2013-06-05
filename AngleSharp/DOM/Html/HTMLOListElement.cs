using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLOListElement : HTMLElement
    {
        public const string Tag = "ol";

        public HTMLOListElement()
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
