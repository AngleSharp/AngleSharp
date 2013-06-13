using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML pre element.
    /// </summary>
    public sealed class HTMLPreElement : HTMLElement
    {
        /// <summary>
        /// The pre tag.
        /// </summary>
        internal const string Tag = "pre";

        internal HTMLPreElement()
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
