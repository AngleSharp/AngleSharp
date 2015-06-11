namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-width
    /// Gets the width of the column-rule.
    /// </summary>
    sealed class CssColumnRuleWidthProperty : CssProperty
    {
        #region ctor

        internal CssColumnRuleWidthProperty()
            : base(PropertyNames.ColumnRuleWidth, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Length.Medium
            get { return Converters.LineWidthConverter; }
        }

        #endregion
    }
}
