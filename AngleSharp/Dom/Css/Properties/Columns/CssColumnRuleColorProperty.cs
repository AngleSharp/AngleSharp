namespace AngleSharp.Dom.Css
{
    using System;
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
        #region ctor

        internal CssColumnRuleColorProperty()
            : base(PropertyNames.ColumnRuleColor, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.ColorConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Color.Transparent;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.ColorConverter.Validate(value);
        }

        #endregion
    }
}
