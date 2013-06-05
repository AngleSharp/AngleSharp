using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLTableElement : HTMLElement
    {
        public const string Tag = "table";

        public HTMLTableElement()
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
