namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-indent
    /// </summary>
    public sealed class CSSTextIndentProperty : CSSProperty
    {
        #region Fields

        CSSCalcValue _indent;

        #endregion

        #region ctor

        internal CSSTextIndentProperty()
            : base(PropertyNames.TextIndent)
        {
            _inherited = true;
            _indent = CSSCalcValue.Zero;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the indentation, which is either a percentage of the containing block width
        /// or specified as fixed length. Negative values are allowed.
        /// </summary>
        public CSSCalcValue Indent
        {
            get { return _indent; }
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
            var indent = value.AsCalc();

            if (indent != null)
                _indent = indent;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
