namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/overflow
    /// </summary>
    sealed class CssOverflowProperty : CssProperty
    {
        #region ctor

        internal CssOverflowProperty()
            : base(PropertyNames.Overflow)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return OverflowMode.Visible;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.OverflowModeConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.OverflowModeConverter.Validate(value);
        }

        #endregion
    }
}
