using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLObjectElement : HTMLElement
    {
        public const string Tag = "object";

        public HTMLObjectElement()
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
