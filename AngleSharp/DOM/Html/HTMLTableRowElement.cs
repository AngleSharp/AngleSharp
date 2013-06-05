using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLTableRowElement : HTMLElement
    {
        public const string Tag = "tr";

        public HTMLTableRowElement()
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
