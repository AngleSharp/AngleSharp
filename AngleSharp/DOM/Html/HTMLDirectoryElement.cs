using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLDirectoryElement : HTMLElement
    {
        public const string Tag = "dir";

        public HTMLDirectoryElement()
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
