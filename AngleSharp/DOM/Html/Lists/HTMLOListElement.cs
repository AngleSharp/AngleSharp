using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML ordered list (ol) element.
    /// </summary>
    [DOM("HTMLOListElement")]
    public sealed class HTMLOListElement : HTMLElement
    {
        /// <summary>
        /// The ol tag.
        /// </summary>
        internal const String Tag = "ol";

        internal HTMLOListElement()
        {
            _name = Tag;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }
    }
}
