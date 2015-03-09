namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-outset
    /// </summary>
    sealed class CssBorderImageOutsetProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Length, Length, Length, Length>> Converter = 
            Converters.LengthOrPercentConverter.Periodic();

        #endregion

        #region ctor

        internal CssBorderImageOutsetProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderImageOutset, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Tuple.Create(Length.Zero, Length.Zero, Length.Zero, Length.Zero);
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
