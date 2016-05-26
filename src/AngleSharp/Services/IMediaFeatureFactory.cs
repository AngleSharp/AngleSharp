namespace AngleSharp.Services
{
    using AngleSharp.Dom.Css;
    using System;

    interface IMediaFeatureFactory
    {
        MediaFeature Create(String name);
    }
}
