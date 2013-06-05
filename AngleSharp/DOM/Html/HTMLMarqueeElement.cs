using System;

namespace AngleSharp.DOM.Html
{
    class HTMLMarqueeElement : HTMLElement
    {
        public const string Tag = "marquee";

        public HTMLMarqueeElement()
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
