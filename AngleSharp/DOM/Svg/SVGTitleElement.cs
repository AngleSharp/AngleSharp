using System;

namespace AngleSharp.DOM.Svg
{
    [DOM("SVGTitleElement")]
    public class SVGTitleElement : SVGElement
    {
        internal SVGTitleElement()
        {
            _name = Tags.TITLE;
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
