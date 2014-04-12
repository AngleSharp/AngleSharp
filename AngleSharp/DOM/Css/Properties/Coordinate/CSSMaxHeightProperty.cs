namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/max-height
    /// </summary>
    sealed class CSSMaxHeightProperty : CSSProperty
    {
        #region Fields

        static readonly NoMaxHeightMode _none = new NoMaxHeightMode();
        MaxHeightMode _mode;

        #endregion

        #region ctor

        public CSSMaxHeightProperty()
            : base(PropertyNames.MaxHeight)
        {
            _inherited = false;
            _mode = _none;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var calc = value.ToCalc();

            if (calc != null)
                _mode = new CalcMaxHeightMode(calc);
            else if (value.Is("none"))
                _mode = _none;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class MaxHeightMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// No limit on the height of the box.
        /// </summary>
        sealed class NoMaxHeightMode : MaxHeightMode
        {
        }

        /// <summary>
        /// The percentage is calculated with respect to the height of the
        /// containing block. If the height of the containing block is not
        /// specified explicitly, the percentage value is treated as none.
        /// OR: A fixed maximum height.
        /// </summary>
        sealed class CalcMaxHeightMode : MaxHeightMode
        {
            CSSCalcValue _calc;
            
            public CalcMaxHeightMode(CSSCalcValue calc)
            {
                _calc = calc;
            }
        }

        #endregion
    }
}
