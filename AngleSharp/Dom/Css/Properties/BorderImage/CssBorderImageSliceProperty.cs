namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-slice
    /// or even better:
    /// http://dev.w3.org/csswg/css-backgrounds/#border-image-slice
    /// </summary>
    sealed class CssBorderImageSliceProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Length, Length?, Length?, Length?, Boolean>> Converter = 
            Converters.WithAny(
                Converters.BorderSliceConverter.Option(new Length(100f, Length.Unit.Percent)),
                Converters.BorderSliceConverter.ToNullable().Option(null),
                Converters.BorderSliceConverter.ToNullable().Option(null),
                Converters.BorderSliceConverter.ToNullable().Option(null),
                Converters.Assign(Keywords.Fill, true).Option(false));

        #endregion

        #region ctor

        internal CssBorderImageSliceProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderImageSlice, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return new Length(100f, Length.Unit.Percent);
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
