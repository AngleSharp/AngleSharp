namespace AngleSharp.Services
{
    using AngleSharp.Dom.Css;
    using System;

    interface IAttributeSelectorFactory : IService
    {
        ISelector Create(String combinator, String name, String value, String prefix);
    }
}
