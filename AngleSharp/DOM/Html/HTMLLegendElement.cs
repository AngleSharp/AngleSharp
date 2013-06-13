using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML legend element.
    /// </summary>
    public sealed class HTMLLegendElement : HTMLElement
    {
        /// <summary>
        /// The legend tag.
        /// </summary>
        internal const string Tag = "legend";

        internal HTMLLegendElement()
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
