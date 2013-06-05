using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLLabelElement : HTMLElement
    {
        public const string Tag = "label";

        public HTMLLabelElement()
        {
            NodeName = Tag;
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
