namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-left-radius
    /// </summary>
    sealed class CssBorderBottomLeftRadiusProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Length, Length?>> Converter = Converters.WithOrder(
            Converters.LengthOrPercentConverter.Required(),
            Converters.LengthOrPercentConverter.ToNullable().Option(null));

        #endregion

        #region ctor

        internal CssBorderBottomLeftRadiusProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderBottomLeftRadius, rule, PropertyFlags.Animatable)
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
