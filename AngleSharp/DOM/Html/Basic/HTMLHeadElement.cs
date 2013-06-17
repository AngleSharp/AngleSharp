using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML head element.
    /// </summary>
    public sealed class HTMLHeadElement : HTMLElement
    {
        /// <summary>
        /// The head tag.
        /// </summary>
        internal const string Tag = "head";

        internal HTMLHeadElement()
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