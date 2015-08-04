namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using System;

    class ButtonInputType : BaseInputType
    {
        #region ctor

        public ButtonInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: false)
        {
        }

        #endregion

        #region Methods

        public override Boolean IsAppendingData(IHtmlElement submitter)
        {
            return Name.Is(InputTypeNames.Reset) == false || Object.ReferenceEquals(submitter, Input);
        }

        #endregion
    }
}
