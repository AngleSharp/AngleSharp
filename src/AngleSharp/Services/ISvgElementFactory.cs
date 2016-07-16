namespace AngleSharp.Services
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Svg;
    using System;

    interface ISvgElementFactory
    {
        SvgElement Create(Document document, String localName, String prefix = null);
    }
}
