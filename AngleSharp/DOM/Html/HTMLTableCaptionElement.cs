using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLTableCaptionElement : HTMLElement
    {
        public const string Tag = "caption";

        public HTMLTableCaptionElement()
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
