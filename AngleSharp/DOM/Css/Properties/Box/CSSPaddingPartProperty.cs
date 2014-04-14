namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Basis for all elementary padding properties.
    /// </summary>
    class CSSPaddingPartProperty : CSSProperty
    {
        #region Fields

        CSSCalcValue _mode;

        #endregion

        #region ctor

        protected CSSPaddingPartProperty(String name)
            : base(name)
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
