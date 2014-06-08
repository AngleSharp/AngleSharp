using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML legend element.
    /// </summary>
    [DomName("HTMLLegendElement")]
    public sealed class HTMLLegendElement : HTMLElement
    {
        internal HTMLLegendElement()
        {
            _name = Tags.Legend;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }
    }
}
