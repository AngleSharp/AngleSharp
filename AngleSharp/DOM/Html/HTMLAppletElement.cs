using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLAppletElement : HTMLElement
    {
        public const string Tag = "applet";

        public HTMLAppletElement()
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
