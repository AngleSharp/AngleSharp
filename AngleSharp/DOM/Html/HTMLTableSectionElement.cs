using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the object for HTML table section (thead / tbody / tfoot) elements.
    /// </summary>
    public sealed class HTMLTableSectionElement : HTMLElement
    {
        /// <summary>
        /// The dialog tag.
        /// </summary>
        internal const string HeadTag = "thead";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        internal const string BodyTag = "tbody";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        internal const string FootTag = "tfoot";

        internal HTMLTableSectionElement()
        {
            _name = BodyTag;
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
