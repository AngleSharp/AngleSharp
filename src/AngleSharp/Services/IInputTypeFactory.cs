namespace AngleSharp.Services
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Html.InputTypes;
    using System;

    interface IInputTypeFactory : IService
    {
        BaseInputType Create(IHtmlInputElement input, String type);
    }
}
