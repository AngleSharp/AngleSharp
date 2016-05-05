namespace AngleSharp.Services
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Svg;
    using System;

    interface ISvgElementFactory : IService
    {
        SvgElement Create(Document document, String localName, String prefix = null);
    }
}
