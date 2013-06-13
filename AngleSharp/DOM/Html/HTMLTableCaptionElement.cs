using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML caption element.
    /// </summary>
    public sealed class HTMLTableCaptionElement : HTMLElement
    {
        /// <summary>
        /// The caption tag.
        /// </summary>
        internal const string Tag = "caption";

        internal HTMLTableCaptionElement()
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
