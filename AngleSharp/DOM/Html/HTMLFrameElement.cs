using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLFrameElement : HTMLElement
    {
        public const string Tag = "frame";

        public HTMLFrameElement()
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
