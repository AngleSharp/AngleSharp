namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;
    using System.Text.RegularExpressions;

    class ColorInputType : BaseInputType
    {
        #region Fields

        static readonly Regex color = new Regex("^\\#[0-9A-Fa-f]{6}$");

        #endregion

        #region ctor

        public ColorInputType(String name)
            : base(name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value ?? String.Empty;
            state.IsBadInput = color.IsMatch(value) == false;
            state.IsValueMissing = input.IsRequired && state.IsBadInput;
        }

        #endregion
    }
}
