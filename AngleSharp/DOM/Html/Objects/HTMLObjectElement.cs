using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML object element.
    /// </summary>
    public sealed class HTMLObjectElement : HTMLElement
    {
        /// <summary>
        /// The object tag.
        /// </summary>
        internal const string Tag = "object";

        internal HTMLObjectElement()
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
