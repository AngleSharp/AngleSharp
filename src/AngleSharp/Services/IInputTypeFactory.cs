namespace AngleSharp.Services
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Html.InputTypes;
    using System;

    interface IInputTypeFactory
    {
        BaseInputType Create(IHtmlInputElement input, String type);
    }
}
