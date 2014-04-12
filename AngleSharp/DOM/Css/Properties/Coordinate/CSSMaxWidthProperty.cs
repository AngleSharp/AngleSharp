namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/max-width
    /// </summary>
    sealed class CSSMaxWidthProperty : CSSProperty
    {
        #region Fields

        static readonly NoMaxWidthMode _none = new NoMaxWidthMode();
        MaxWidthMode _mode;

        #endregion

        #region ctor

        public CSSMaxWidthProperty()
            : base(PropertyNames.MaxWidth)
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
                _mode = new CalcMaxWidthMode(calc);
            else if (value.Is("none"))
                _mode = _none;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class MaxWidthMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// The width has no maximum value.
        /// </summary>
        sealed class NoMaxWidthMode : MaxWidthMode
        {
        }

        /// <summary>
        /// Specified as a relative of containing block's width.
        /// OR: Specified as an absolute length.
        /// </summary>
        sealed class CalcMaxWidthMode : MaxWidthMode
        {
            CSSCalcValue _calc;

            public CalcMaxWidthMode(CSSCalcValue calc)
            {
                _calc = calc;
            }
        }

        #endregion
    }
}
