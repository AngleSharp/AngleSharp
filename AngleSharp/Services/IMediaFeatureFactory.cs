namespace AngleSharp.Services
{
    using AngleSharp.Dom.Css;
    using System;

    interface IMediaFeatureFactory : IService
    {
        MediaFeature Create(String name);
    }
}
