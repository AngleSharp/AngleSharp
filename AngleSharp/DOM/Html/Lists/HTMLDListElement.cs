using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML dl element.
    /// </summary>
    [DOM("HTMLDListElement")]
    public sealed class HTMLDListElement : HTMLElement
    {
        internal HTMLDListElement()
        {
            _name = Tags.DL;
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
