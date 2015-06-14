namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-color
    /// Gets the color for the vertical column rule.
    /// </summary>
    sealed class CssColumnRuleColorProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.ColorConverter.OrDefault(Color.Transparent);

        #endregion

        #region ctor

        internal CssColumnRuleColorProperty()
            : base(PropertyNames.ColumnRuleColor, PropertyFlags.Animatable)
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
