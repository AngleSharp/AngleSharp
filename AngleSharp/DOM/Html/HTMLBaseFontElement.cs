using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLBaseFontElement : HTMLElement
    {
        public const string Tag = "basefont";

        public HTMLBaseFontElement()
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
