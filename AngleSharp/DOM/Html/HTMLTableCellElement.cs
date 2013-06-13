using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the object for HTML table cell (td / th) elements.
    /// </summary>
    public sealed class HTMLTableCellElement : HTMLElement
    {
        /// <summary>
        /// The th tag.
        /// </summary>
        internal const string HeadTag = "th";

        /// <summary>
        /// The td tag.
        /// </summary>
        internal const string NormalTag = "td";

        internal HTMLTableCellElement()
        {
            _name = NormalTag;
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
