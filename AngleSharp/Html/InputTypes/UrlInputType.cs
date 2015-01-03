namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class UrlInputType : BaseInputType
    {
        #region ctor

        public UrlInputType()
            : base(validate: true)
        {
        }

        #endregion

        #region Methods

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value ?? String.Empty;
            state.IsPatternMismatch = IsInvalidPattern(input.Pattern, value);

            if (IsInvalidUrl(value))
            {
                state.IsTypeMismatch = !String.IsNullOrEmpty(value);
                state.IsBadInput = state.IsTypeMismatch;
            }
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
