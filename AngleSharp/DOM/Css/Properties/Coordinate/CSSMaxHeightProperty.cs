namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/max-height
    /// </summary>
    public sealed class CSSMaxHeightProperty : CSSProperty
    {
        #region Fields

        /// <summary>
        /// No limit on the height of the box if _mode == null.
        /// </summary>
        CSSCalcValue _mode;

        #endregion

        #region ctor

        internal CSSMaxHeightProperty()
            : base(PropertyNames.MaxHeight)
        {
            _inherited = false;
            _mode = null;
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets if a limit has been specified, otherwise the value is none.
        /// </summary>
        public Boolean IsLimited
        {
            get { return _mode != null; }
        }

        /// <summary>
        /// Gets the specified max-height of the element. A percentage is calculated
        /// with respect to the height of the containing block. If the height of the
        /// containing block is not specified explicitly, the percentage value is
        /// treated as none.
        /// </summary>
        internal CSSCalcValue Limit
        {
            get { return _mode; }
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
            var calc = value.AsCalc();

            if (calc != null)
                _mode = calc;
            else if (value.Is("none"))
                _mode = null;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
