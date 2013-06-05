using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLTableColElement : HTMLElement
    {
        public const string ColTag = "col";
        public const string ColgroupTag = "colgroup";

        public HTMLTableColElement()
        {
            NodeName = ColTag;
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
