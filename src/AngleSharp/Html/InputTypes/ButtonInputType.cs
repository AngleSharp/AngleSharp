namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
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
            return !Name.Is(InputTypeNames.Reset) || Object.ReferenceEquals(submitter, Input);
        }

        #endregion
    }
}
