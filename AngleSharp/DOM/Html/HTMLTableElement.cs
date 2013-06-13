using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML table element.
    /// </summary>
    public sealed class HTMLTableElement : HTMLElement
    {
        /// <summary>
        /// The table tag.
        /// </summary>
        internal const string Tag = "table";

        internal HTMLTableElement()
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
