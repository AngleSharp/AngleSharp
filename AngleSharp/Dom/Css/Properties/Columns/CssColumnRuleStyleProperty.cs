namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-style
    /// Gets the selected column-rule line style.
    /// </summary>
    sealed class CssColumnRuleStyleProperty : CssProperty
    {
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
            return Converters.LineStyleConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.LineStyleConverter.Validate(value);
        }

        #endregion
    }
}
