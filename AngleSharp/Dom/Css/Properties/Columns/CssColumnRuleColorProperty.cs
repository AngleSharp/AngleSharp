namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-color
    /// Gets the color for the vertical column rule.
    /// </summary>
    sealed class CssColumnRuleColorProperty : CssProperty
    {
        #region ctor

        internal CssColumnRuleColorProperty()
            : base(PropertyNames.ColumnRuleColor, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Color.Transparent
            get { return Converters.ColorConverter; }
        }

        #endregion
    }
}
