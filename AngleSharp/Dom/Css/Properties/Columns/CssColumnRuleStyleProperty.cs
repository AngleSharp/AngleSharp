namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-style
    /// Gets the selected column-rule line style.
    /// </summary>
    sealed class CssColumnRuleStyleProperty : CssProperty
    {
        #region ctor

        internal CssColumnRuleStyleProperty()
            : base(PropertyNames.ColumnRuleStyle)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: LineStyle.None
            get { return Converters.LineStyleConverter; }
        }

        #endregion
    }
}
