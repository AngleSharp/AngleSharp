namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.DOM.Html;
    using System;

    class PatternInputType : BaseInputType
    {
        #region ctor

        public PatternInputType(String name)
            : base(name, validate: true)
        {
        }

        #endregion

        #region Methods

        public override void Check(IHtmlInputElement input, ValidityState state)
        {
            var value = input.Value ?? String.Empty;
            state.IsPatternMismatch = IsInvalidPattern(input.Pattern, value);
        }

        #endregion
    }
}
