namespace AngleSharp.Html.InputTypes
{
    using System;

    class ButtonInputType : BaseInputType
    {
        #region ctor

        public ButtonInputType(String name)
            : base(name, validate: false)
        {
        }

        #endregion
    }
}
