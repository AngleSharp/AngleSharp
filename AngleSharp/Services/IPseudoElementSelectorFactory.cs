namespace AngleSharp.Services
{
    using AngleSharp.Dom.Css;
    using System;

    interface IPseudoElementSelectorFactory : IService
    {
        ISelector Create(String name);
    }
}
