namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Html.Dom;
    using System;

    class PatternInputType : BaseInputType
    {
        #region ctor

        public PatternInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override ValidationErrors Check(IValidityState current)
        {
            var result = GetErrorsFrom(current);

            if (IsInvalidPattern(Input.Pattern, Input.Value ?? String.Empty))
            {
                result ^= ValidationErrors.PatternMismatch;
            }

            return result;
        }

        #endregion
    }
}
