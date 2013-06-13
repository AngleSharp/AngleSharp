using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML html element.
    /// </summary>
    public sealed class HTMLHtmlElement : HTMLElement
    {
        /// <summary>
        /// The html tag.
        /// </summary>
        internal const string Tag = "html";

        /// <summary>
        /// Creates a new HTML html tag.
        /// </summary>
        internal HTMLHtmlElement()
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
