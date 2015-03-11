namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-color
    /// Gets the color for the vertical column rule.
    /// </summary>
    sealed class CssColumnRuleColorProperty : CssProperty
    {
        #region ctor

        internal CssColumnRuleColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ColumnRuleColor, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Color.Transparent;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.ColorConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.ColorConverter.Validate(value);
        }

        #endregion
    }
}
