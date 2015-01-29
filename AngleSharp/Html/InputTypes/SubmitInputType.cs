namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
    using System;

    class SubmitInputType : BaseInputType
    {
        #region ctor

        public SubmitInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion
    }
}
