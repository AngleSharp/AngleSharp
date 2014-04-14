namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Basis for all elementary margin properties.
    /// </summary>
    class CSSMarginPartProperty : CSSProperty
    {
        #region Fields

        static readonly AutoMarginMode _auto = new AutoMarginMode();
        static readonly CalcMarginMode _default = new CalcMarginMode(CSSCalcValue.Zero);
        MarginMode _mode;

        #endregion

        #region ctor

        protected CSSMarginPartProperty(String name)
            : base(name)
        {
            _inherited = false;
            _mode = _default;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var calc = value.AsCalc();

            if (calc != null)
                _mode = new CalcMarginMode(calc);
            else if (value.Is("auto"))
                _mode = _auto;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Mode

        abstract class MarginMode
        {
            //TODO add members
        }

        /// <summary>
        /// auto is replaced by some suitable value, e.g. it can be used for centering of blocks.
        /// </summary>
        sealed class AutoMarginMode : MarginMode
        {
        }

        /// <summary>
        /// Relative to the width of the containing block. Negative values are allowed.
        /// OR: Specifies a fixed width. Negative Values are allowed.
        /// </summary>
        sealed class CalcMarginMode : MarginMode
        {
            CSSCalcValue _calc;

            public CalcMarginMode(CSSCalcValue calc)
            {
                _calc = calc;
            }
        }

        #endregion
    }
}
