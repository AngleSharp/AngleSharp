using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLUListElement : HTMLElement
    {
        public const string Tag = "ul";

        public HTMLUListElement()
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
