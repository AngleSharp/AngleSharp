using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The DOM Object representing the unordered list.
    /// </summary>
    public sealed class HTMLUListElement : HTMLElement
    {
        /// <summary>
        /// The tag ul.
        /// </summary>
        internal const string Tag = "ul";

        internal HTMLUListElement()
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
