namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-width
    /// </summary>
    sealed class CssBorderImageWidthProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Length, Length, Length, Length>> Converter = 
            Converters.ImageBorderWidthConverter.Periodic();

        #endregion

        #region ctor

        internal CssBorderImageWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderImageWidth, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Full;
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
