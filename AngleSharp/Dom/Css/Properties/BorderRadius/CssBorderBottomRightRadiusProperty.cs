namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-right-radius
    /// </summary>
    sealed class CssBorderBottomRightRadiusProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Length, Length?>> Converter = Converters.WithOrder(
            Converters.LengthOrPercentConverter.Required(),
            Converters.LengthOrPercentConverter.ToNullable().Option(null));

        #endregion

        #region ctor

        internal CssBorderBottomRightRadiusProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderBottomRightRadius, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Zero;
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
