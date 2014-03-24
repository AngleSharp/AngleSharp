namespace AngleSharp.DOM.Html
{
    using System;

    interface IValidation
    {
        Boolean WillValidate { get; }
        ValidityState Validity { get; }
        String ValidationMessage { get; }

        Boolean CheckValidity();
        void SetCustomValidity(String error);
    }
}
