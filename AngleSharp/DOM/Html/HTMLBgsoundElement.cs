using System;

namespace AngleSharp.DOM.Html
{
    class HTMLBgsoundElement : HTMLElement
    {
        public const string Tag = "bgsound";

        public HTMLBgsoundElement()
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
