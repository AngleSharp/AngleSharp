namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-color
    /// </summary>
    sealed class CSSColumnRuleColorProperty : CSSProperty
    {
        #region Fields

        /// <summary>
        /// Represents the color of the rule separating columns.
        /// </summary>
        Color _color;

        #endregion

        #region ctor

        public CSSColumnRuleColorProperty()
            : base(PropertyNames.ColumnRuleColor)
        {
            _color = Color.Transparent;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var color = value.ToColor();

            if (color.HasValue)
                _color = color.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
