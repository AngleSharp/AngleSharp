using System;

namespace AngleSharp.DOM.Svg
{
    /// <summary>
    /// Represents the svg element of the SVG DOM.
    /// </summary>
    [DomName("SVGSVGElement")]
    public class SVGSVGElement : SVGElement
    {
        internal SVGSVGElement()
        {
            _name = Tags.Svg;
        }
    }
}
