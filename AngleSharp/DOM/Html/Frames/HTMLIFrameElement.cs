using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML iframe element.
    /// </summary>
    public sealed class HTMLIFrameElement : HTMLElement
    {
        /// <summary>
        /// The iframe tag.
        /// </summary>
        internal const string Tag = "iframe";

        internal HTMLIFrameElement()
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
