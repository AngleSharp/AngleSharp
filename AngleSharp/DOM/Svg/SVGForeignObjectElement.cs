using System;

namespace AngleSharp.DOM.Svg
{
    /// <summary>
    /// Represents the foreign object element of the SVG DOM.
    /// </summary>
    [DOM("SVGForeignObjectElement")]
    public sealed class SVGForeignObjectElement : SVGElement, IScopeElement
    {
        internal SVGForeignObjectElement()
        {
            _name = Tags.FOREIGNOBJECT;
        }

        /// <summary>
        /// Gets the status if the node is an HTML text integration point.
        /// </summary>
        protected internal override Boolean IsHtmlTIP
        {
            get { return true; }
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
