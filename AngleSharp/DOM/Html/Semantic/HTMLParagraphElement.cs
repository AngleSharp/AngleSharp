using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML paragraph element.
    /// </summary>
    public sealed class HTMLParagraphElement : HTMLElement
    {
        /// <summary>
        /// The p tag.
        /// </summary>
        internal const string Tag = "p";

        /// <summary>
        /// Creates a new HTML paragraph element.
        /// </summary>
        internal HTMLParagraphElement()
        {
            _name = Tag;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }
    }
}
