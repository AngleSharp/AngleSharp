namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-gap
    /// Gets the selected width of gaps between columns.
    /// </summary>
    sealed class CssColumnGapProperty : CssProperty
    {
        #region ctor

        internal CssColumnGapProperty()
            : base(PropertyNames.ColumnGap, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: new Length(1f, Length.Unit.Em)
            get { return Converters.LengthOrNormalConverter; }
        }

        #endregion
    }
}
