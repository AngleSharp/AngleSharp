using System;

namespace AngleSharp.DOM.Html
{
    interface IValidation
    {
        bool WillValidate { get; }
        ValidityState Validity { get; }
        string ValidationMessage { get; }

        bool CheckValidity();
        void SetCustomValidity(string error);
    }
}
