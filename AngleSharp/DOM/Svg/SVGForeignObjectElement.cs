namespace AngleSharp.DOM.Svg
{
    using System;

    /// <summary>
    /// Represents the foreign object element of the SVG DOM.
    /// </summary>
    [DomName("SVGForeignObjectElement")]
    public sealed class SVGForeignObjectElement : SVGElement, IScopeElement
    {
        internal SVGForeignObjectElement()
        {
            _name = Tags.ForeignObject;
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
