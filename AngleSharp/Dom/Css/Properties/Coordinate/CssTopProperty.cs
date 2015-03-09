namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/top
    /// </summary>
    sealed class CssTopProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Length?> Converter =
            Converters.AutoLengthOrPercentConverter;

        #endregion

        #region ctor

        internal CssTopProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Top, rule, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
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
