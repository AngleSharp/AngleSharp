namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-style
    /// </summary>
    public sealed class CSSColumnRuleStyleProperty : CSSProperty
    {
        #region Fields

        LineStyle _style;

        #endregion

        #region ctor

        internal CSSColumnRuleStyleProperty()
            : base(PropertyNames.ColumnRuleStyle)
        {
            _style = LineStyle.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected column-rule line style.
        /// </summary>
        public LineStyle Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var style = value.ToLineStyle();

            if (style.HasValue)
                _style = style.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
