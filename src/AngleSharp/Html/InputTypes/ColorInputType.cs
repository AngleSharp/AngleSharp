namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Html.Dom;
    using System;
    using System.Text.RegularExpressions;

    class ColorInputType : BaseInputType
    {
        #region Fields

        static readonly Regex color = new Regex("^\\#[0-9A-Fa-f]{6}$");

        #endregion

        #region ctor

        public ColorInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override ValidationErrors Check(IValidityState current)
        {
            var result = GetErrorsFrom(current);

            if (!color.IsMatch(Input.Value ?? String.Empty))
            {
                result ^= ValidationErrors.BadInput;

                if (Input.IsRequired)
                {
                    result ^= ValidationErrors.ValueMissing;
                }

                return result;
            }

            return ValidationErrors.None;
        }

        #endregion
    }
}
