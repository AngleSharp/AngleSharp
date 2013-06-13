using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML modifier (ins / del) element.
    /// </summary>
    public sealed class HTMLModElement : HTMLElement
    {
        /// <summary>
        /// The ins tag.
        /// </summary>
        internal const string InsTag = "ins";

        /// <summary>
        /// The del tag.
        /// </summary>
        internal const string DelTag = "del";

        internal HTMLModElement()
        {
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
