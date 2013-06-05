using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLModElement : HTMLElement
    {
        public const string InsTag = "ins";
        public const string DelTag = "del";

        public HTMLModElement()
        {
        }

        protected internal override bool IsSpecial
        {
            get
            {
                return false;
            }
        }
    }
}
