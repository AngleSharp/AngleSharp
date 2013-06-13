using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Rperesents the HTML quote element.
    /// </summary>
    public sealed class HTMLQuoteElement : HTMLElement
    {
        /// <summary>
        /// The quote tag.
        /// </summary>
        internal const string NormalTag = "quote";

        /// <summary>
        /// The blockquote tag.
        /// </summary>
        internal const string BlockTag = "blockquote";

        /// <summary>
        /// The q tag.
        /// </summary>
        internal const string ShortTag = "q";

        internal HTMLQuoteElement()
        {
            _name = NormalTag;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return _name.Equals(BlockTag); }
        }
    }
}
