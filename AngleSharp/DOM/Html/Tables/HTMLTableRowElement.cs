using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML tr element.
    /// </summary>
    public sealed class HTMLTableRowElement : HTMLElement
    {
        /// <summary>
        /// The tr tag.
        /// </summary>
        internal const string Tag = "tr";

        internal HTMLTableRowElement()
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
