namespace AngleSharp.Dom.Services
{
    using System;

    interface IElementFactory<TElement>
        where TElement : Element
    {
        TElement Create(Document document, String localName, String prefix = null);
    }
}
