using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLFrameSetElement : HTMLElement
    {
        public const string Tag = "frameset";

        public HTMLFrameSetElement()
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
