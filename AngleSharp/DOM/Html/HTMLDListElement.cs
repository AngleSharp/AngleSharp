using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLDListElement : HTMLElement
    {
        public const string Tag = "dl";

        public HTMLDListElement()
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
