using System;

namespace AngleSharp.DOM.Html
{
    public sealed class HTMLQuoteElement : HTMLElement
    {
        public const string NormalTag = "quote";
        public const string BlockTag = "blockquote";
        public const string ShortTag = "q";

        internal HTMLQuoteElement()
        {
            _name = NormalTag;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get
            {
                return NodeName == BlockTag;
            }
        }
    }
}
