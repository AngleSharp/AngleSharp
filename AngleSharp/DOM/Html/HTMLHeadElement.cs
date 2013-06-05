using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLHeadElement : HTMLElement
    {
        public const string Tag = "head";

        public HTMLHeadElement()
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