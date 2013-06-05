using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLTableCellElement : HTMLElement
    {
        public const string HeadTag = "th";
        public const string NormalTag = "td";

        public HTMLTableCellElement()
        {
            NodeName = NormalTag;
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
