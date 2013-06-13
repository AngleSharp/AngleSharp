using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML dir element.
    /// </summary>
    public sealed class HTMLDirectoryElement : HTMLElement
    {
        /// <summary>
        /// The dir tag.
        /// </summary>
        internal const string Tag = "dir";

        internal HTMLDirectoryElement()
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
