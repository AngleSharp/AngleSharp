namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-gap
    /// Gets the selected width of gaps between columns.
    /// </summary>
    sealed class CssColumnGapProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.LengthOrNormalConverter.OrDefault(new Length(1f, Length.Unit.Em));

        #endregion

        #region ctor

        internal CssColumnGapProperty()
            : base(PropertyNames.ColumnGap, PropertyFlags.Animatable)
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
