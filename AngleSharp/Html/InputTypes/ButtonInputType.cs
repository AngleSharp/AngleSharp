namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
    using System;

    class ButtonInputType : BaseInputType
    {
        #region ctor

        public ButtonInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: false)
        {
        }

        #endregion
    }
}
