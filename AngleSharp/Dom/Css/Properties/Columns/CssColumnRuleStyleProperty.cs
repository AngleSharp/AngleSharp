namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-style
    /// Gets the selected column-rule line style.
    /// </summary>
    sealed class CssColumnRuleStyleProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<LineStyle> Converter = 
            Map.LineStyles.ToConverter();

        #endregion

        #region ctor

        internal CssColumnRuleStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ColumnRuleStyle, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return LineStyle.None;
        }

        protected override Object Compute(IElement element)
        {
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}
