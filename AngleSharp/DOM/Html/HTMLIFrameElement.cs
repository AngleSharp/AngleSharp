using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLIFrameElement : HTMLElement
    {
        public const string Tag = "iframe";

        public HTMLIFrameElement()
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
