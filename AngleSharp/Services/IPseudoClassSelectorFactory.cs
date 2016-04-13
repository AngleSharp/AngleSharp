namespace AngleSharp.Services
{
    using AngleSharp.Dom.Css;
    using System;

    interface IPseudoClassSelectorFactory : IService
    {
        ISelector Create(String name);
    }
}
