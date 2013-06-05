using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLTableSectionElement : HTMLElement
    {
        public const string HeadTag = "thead";
        public const string BodyTag = "tbody";
        public const string FootTag = "tfoot";

        public HTMLTableSectionElement()
        {
            NodeName = BodyTag;
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
