namespace AngleSharp.Services
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Mathml;
    using System;

    interface IMathElementFactory : IService
    {
        MathElement Create(Document document, String localName, String prefix = null);
    }
}
