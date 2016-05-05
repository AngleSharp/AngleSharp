namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
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

        public override void Check(ValidityState state)
        {
            var value = Input.Value ?? String.Empty;
            state.IsPatternMismatch = IsInvalidPattern(Input.Pattern, value);

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
