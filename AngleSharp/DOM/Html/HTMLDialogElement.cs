using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLDialogElement : HTMLElement
    {
        public const string Tag = "dialog";

        public HTMLDialogElement()
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
