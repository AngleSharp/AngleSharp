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

        #region Methods

        public override Boolean IsAppendingData(IHtmlElement submitter)
        {
            return Object.ReferenceEquals(submitter, Input);
        }

        #endregion
    }
}
