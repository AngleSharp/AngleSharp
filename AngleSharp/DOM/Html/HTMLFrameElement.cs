using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML frame element.
    /// </summary>
    public sealed class HTMLFrameElement : HTMLElement
    {
        /// <summary>
        /// The frame tag.
        /// </summary>
        internal const string Tag = "frame";

        internal HTMLFrameElement()
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
