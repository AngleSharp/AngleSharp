namespace AngleSharp.DOM.Svg
{
    using System;

    /// <summary>
    /// Represents the desc element of the SVG DOM.
    /// </summary>
    sealed class SVGDescElement : SVGElement, IScopeElement
    {
        internal SVGDescElement()
        {
            _name = Tags.Desc;
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
