namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule
    /// </summary>
    sealed class CssColumnRuleProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.WithAny(
            Converters.ColorConverter.Option().For(PropertyNames.ColumnRuleColor),
            Converters.LineWidthConverter.Option().For(PropertyNames.ColumnRuleWidth),
            Converters.LineStyleConverter.Option().For(PropertyNames.ColumnRuleStyle)).OrDefault();

        #endregion

        #region ctor

        internal CssColumnRuleProperty()
            : base(PropertyNames.ColumnRule, PropertyFlags.Animatable)
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
