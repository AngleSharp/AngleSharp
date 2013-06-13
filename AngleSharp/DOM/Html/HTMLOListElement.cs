using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML ordered list (ol) element.
    /// </summary>
    public sealed class HTMLOListElement : HTMLElement
    {
        /// <summary>
        /// The ol tag.
        /// </summary>
        internal const string Tag = "ol";

        internal HTMLOListElement()
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
