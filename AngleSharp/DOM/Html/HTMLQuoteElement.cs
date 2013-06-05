using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLQuoteElement : HTMLElement
    {
        public const string NormalTag = "quote";
        public const string BlockTag = "blockquote";
        public const string ShortTag = "q";

        public HTMLQuoteElement()
        {
            NodeName = NormalTag;
        }

        protected internal override bool IsSpecial
        {
            get
            {
                return NodeName == BlockTag;
            }
        }
    }
}
