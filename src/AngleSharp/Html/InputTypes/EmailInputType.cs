namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Html.Dom;
    using System;
    using System.Text.RegularExpressions;

    class EmailInputType : BaseInputType
    {
        #region Fields

        static readonly Regex email = new Regex("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");

        #endregion

        #region ctor

        public EmailInputType(IHtmlInputElement input, String name)
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

            if (IsInvalidEmail(Input.IsMultiple, value) && !String.IsNullOrEmpty(value))
            {
                result ^= ValidationErrors.TypeMismatch | ValidationErrors.BadInput;
            }

            return result;
        }

        static Boolean IsInvalidEmail(Boolean multiple, String value)
        {
            if (multiple)
            {
                var mails = value.Split(',');

                foreach (var mail in mails)
                {
                    if (!email.IsMatch(mail.Trim()))
                    {
                        return true;
                    }
                }

                return false;
            }

            return !email.IsMatch(value.Trim());
        }

        #endregion
    }
}
