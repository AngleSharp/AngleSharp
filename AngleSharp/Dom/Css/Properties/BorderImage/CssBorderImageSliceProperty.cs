namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-slice
    /// or even better:
    /// http://dev.w3.org/csswg/css-backgrounds/#border-image-slice
    /// </summary>
    sealed class CssBorderImageSliceProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<Length, Length?, Length?, Length?, Boolean>> StyleConverter = 
            Converters.WithAny(
                Converters.BorderSliceConverter.Option(new Length(100f, Length.Unit.Percent)),
                Converters.BorderSliceConverter.ToNullable().Option(null),
                Converters.BorderSliceConverter.ToNullable().Option(null),
                Converters.BorderSliceConverter.ToNullable().Option(null),
                Converters.Assign(Keywords.Fill, true).Option(false));

        #endregion

        #region ctor

        internal CssBorderImageSliceProperty()
            : base(PropertyNames.BorderImageSlice)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return new Length(100f, Length.Unit.Percent);
        }

        protected override Object Compute(IElement element)
        {
            return StyleConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return StyleConverter.Validate(value);
        }

        #endregion
    }
}
