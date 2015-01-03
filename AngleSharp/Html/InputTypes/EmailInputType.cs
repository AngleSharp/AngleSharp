namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;
    using System.Text.RegularExpressions;

    class EmailInputType : BaseInputType
    {
        #region Fields

        static readonly Regex email = new Regex("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$");

        #endregion

        #region ctor

        public EmailInputType(String name)
            : base(name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value ?? String.Empty;
            state.IsPatternMismatch = IsInvalidPattern(input.Pattern, value);

            if (IsInvalidEmail(input.IsMultiple, value))
            {
                state.IsTypeMismatch = !String.IsNullOrEmpty(value);
                state.IsBadInput = state.IsTypeMismatch;
            }
        }

        static Boolean IsInvalidEmail(Boolean multiple, String value)
        {
            if (multiple)
            {
                var mails = value.Split(',');

                foreach (var mail in mails)
                {
                    if (email.IsMatch(mail.Trim()) == false)
                        return true;
                }

                return false;
            }

            return email.IsMatch(value.Trim()) == false;
        }

        #endregion
    }
}
