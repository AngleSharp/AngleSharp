namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
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

        public override void Check(ValidityState state)
        {
            var value = Input.Value ?? String.Empty;
            state.IsBadInput = color.IsMatch(value) == false;
            state.IsValueMissing = Input.IsRequired && state.IsBadInput;
        }

        #endregion
    }
}
