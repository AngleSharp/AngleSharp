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

        internal CssColumnRuleStyleProperty()
            : base(PropertyNames.ColumnRuleStyle)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.LineStyleConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return LineStyle.None;
        }

        #endregion
    }
}
