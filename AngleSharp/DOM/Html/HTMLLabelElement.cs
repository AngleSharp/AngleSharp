using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML label element.
    /// </summary>
    public sealed class HTMLLabelElement : HTMLElement
    {
        /// <summary>
        /// The label tag.
        /// </summary>
        internal const string Tag = "label";

        internal HTMLLabelElement()
        {
            _name = Tag;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return false; }
        }
    }
}
