using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLLegendElement : HTMLElement
    {
        public const string Tag = "legend";

        public HTMLLegendElement()
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
