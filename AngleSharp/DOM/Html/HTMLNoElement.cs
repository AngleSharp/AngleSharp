using System;

namespace AngleSharp.DOM.Html
{
    class HTMLNoElement : HTMLElement
    {
        public const string NoEmbedTag = "noembed";
        public const string NoScriptTag = "noscript";
        public const string NoFramesTag = "noframes";

        public HTMLNoElement()
        {
            NodeName = NoScriptTag;
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
