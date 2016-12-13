namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Html.Dom;
    using System;

    class UrlInputType : BaseInputType
    {
        #region ctor

        public UrlInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override ValidationErrors Check(IValidityState current)
        {
            var value = Input.Value ?? String.Empty;
            var result = GetErrorsFrom(current);

            if (IsInvalidPattern(Input.Pattern, value))
            {
                result ^= ValidationErrors.PatternMismatch;
            }

            if (IsInvalidUrl(value) && !String.IsNullOrEmpty(value))
            {
                result ^= ValidationErrors.TypeMismatch | ValidationErrors.BadInput;
            }

            return result;
        }

        static Boolean IsInvalidUrl(String value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var url = new Url(value);
                return url.IsInvalid || url.IsRelative;
            }

            return false;
        }

        #endregion
    }
}
