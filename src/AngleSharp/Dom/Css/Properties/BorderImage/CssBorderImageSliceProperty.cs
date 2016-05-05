namespace AngleSharp.Dom.Css
{
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

        internal static readonly IValueConverter TheConverter = Converters.WithAny(
            Converters.BorderSliceConverter.Option(new Length(100f, Length.Unit.Percent)),
            Converters.BorderSliceConverter.Option(),
            Converters.BorderSliceConverter.Option(),
            Converters.BorderSliceConverter.Option(),
            Converters.Assign(Keywords.Fill, true).Option(false));

        static readonly IValueConverter StyleConverter = TheConverter.OrDefault(Length.Full);

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
    }
}
