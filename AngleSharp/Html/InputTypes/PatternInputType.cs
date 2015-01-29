namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
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

        public override void Check(ValidityState state)
        {
            var value = Input.Value ?? String.Empty;
            state.IsPatternMismatch = IsInvalidPattern(Input.Pattern, value);
        }

        #endregion
    }
}
