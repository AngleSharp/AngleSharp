namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/overflow
    /// </summary>
    sealed class CssOverflowProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<OverflowMode> Converter = 
            Map.OverflowModes.ToConverter();

        #endregion

        #region ctor

        internal CssOverflowProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Overflow, rule)
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
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}
