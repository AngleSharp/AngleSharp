namespace AngleSharp.Html.InputTypes
{
    using System;

    class SubmitInputType : BaseInputType
    {
        #region ctor

        public SubmitInputType(String name)
            : base(name, validate: true)
        {
        }

        #endregion
    }
}
