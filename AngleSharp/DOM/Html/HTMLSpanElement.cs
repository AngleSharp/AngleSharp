using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML span element.
    /// </summary>
    public sealed class HTMLSpanElement : HTMLElement
    {
        /// <summary>
        /// The span tag.
        /// </summary>
        internal const string Tag = "span";

        internal HTMLSpanElement()
        {
            _name = Tag;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return false; }
        }
    }
}
