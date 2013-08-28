using System;

namespace AngleSharp.DOM.Svg
{
    [DOM("SVGSVGElement")]
    public class SVGSVGElement : SVGElement
    {
        internal SVGSVGElement()
        {
            _name = Tags.SVG;
        }
    }
}
