using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML frameset element.
    /// </summary>
    public sealed class HTMLFrameSetElement : HTMLElement
    {
        /// <summary>
        /// The frameset tag.
        /// </summary>
        internal const string Tag = "frameset";

        internal HTMLFrameSetElement()
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
