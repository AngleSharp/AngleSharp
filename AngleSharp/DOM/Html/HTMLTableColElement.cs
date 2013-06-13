using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML column (col / colgroup) element.
    /// </summary>
    public sealed class HTMLTableColElement : HTMLElement
    {
        /// <summary>
        /// The col tag.
        /// </summary>
        internal const string ColTag = "col";

        /// <summary>
        /// The colgroup tag.
        /// </summary>
        internal const string ColgroupTag = "colgroup";

        internal HTMLTableColElement()
        {
            _name = ColTag;
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
