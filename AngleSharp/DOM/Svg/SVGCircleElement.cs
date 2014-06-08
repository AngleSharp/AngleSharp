using System;

namespace AngleSharp.DOM.Svg
{
    /// <summary>
    /// Represents the circle element of the SVG DOM.
    /// </summary>
    [DomName("SVGCircleElement")]
    public class SVGCircleElement : SVGElement
    {
        internal SVGCircleElement()
        {
            _name = Tags.Circle;
        }
    }
}
