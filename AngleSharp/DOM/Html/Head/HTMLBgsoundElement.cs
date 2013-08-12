using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML bgsound element.
    /// </summary>
    sealed class HTMLBgsoundElement : HTMLElement
    {
        /// <summary>
        /// The bgsound tag.
        /// </summary>
        internal const String Tag = "bgsound";

        internal HTMLBgsoundElement()
        {
            _name = Tag;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }
    }
}
