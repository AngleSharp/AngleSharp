namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/line-height
    /// </summary>
    sealed class CssLineHeightProperty : CssProperty
    {
        #region ctor

        internal CssLineHeightProperty()
            : base(PropertyNames.LineHeight, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: new Length(120f, Length.Unit.Percent)
            get { return Converters.LineHeightConverter; }
        }

        #endregion
    }
}
