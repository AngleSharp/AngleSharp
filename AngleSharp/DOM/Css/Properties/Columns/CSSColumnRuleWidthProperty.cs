namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-width
    /// </summary>
    public sealed class CSSColumnRuleWidthProperty : CSSProperty
    {
        #region Fields

        /// <summary>
        /// Describes the width of the rule separating two columns.
        /// </summary>
        Length _width;

        #endregion

        #region ctor

        internal CSSColumnRuleWidthProperty()
            : base(PropertyNames.ColumnRuleWidth)
        {
            _width = Length.Medium;
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the column-rule.
        /// </summary>
        public Length Width
        {
            get { return _width; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var width = value.ToBorderWidth();

            if (width.HasValue)
                _width = width.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
