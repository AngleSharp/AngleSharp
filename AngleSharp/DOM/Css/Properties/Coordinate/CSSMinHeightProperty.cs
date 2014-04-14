namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-height
    /// </summary>
    sealed class CSSMinHeightProperty : CSSProperty
    {
        #region Fields

        CSSCalcValue _mode;

        #endregion

        #region ctor

        public CSSMinHeightProperty()
            : base(PropertyNames.MinHeight)
        {
            _inherited = false;
            _mode = CSSCalcValue.Zero;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var calc = value.AsCalc();

            if (calc != null)
                _mode = calc;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
