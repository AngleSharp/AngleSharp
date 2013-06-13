using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML dl element.
    /// </summary>
    public sealed class HTMLDListElement : HTMLElement
    {
        /// <summary>
        /// The dl tag.
        /// </summary>
        internal const string Tag = "dl";

        internal HTMLDListElement()
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
