namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Basis for all elementary padding properties.
    /// </summary>
    public abstract class CSSPaddingPartProperty : CSSProperty
    {
        #region Fields

        CSSCalcValue _padding;

        #endregion

        #region ctor

        internal CSSPaddingPartProperty(String name)
            : base(name)
        {
            _inherited = false;
            _padding = CSSCalcValue.Zero;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the padding relative to the width of the containing block or
        /// a fixed width.
        /// </summary>
        internal CSSCalcValue Padding
        {
            get { return _padding; }
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
                _padding = calc;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
