namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Basis for all elementary margin properties.
    /// </summary>
    public abstract class CSSMarginPartProperty : CSSProperty
    {
        #region Fields

        CSSCalcValue _margin;

        #endregion

        #region ctor

        internal CSSMarginPartProperty(String name)
            : base(name)
        {
            _inherited = false;
            _margin = CSSCalcValue.Zero;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the margin is automatically determined.
        /// </summary>
        public Boolean IsAuto
        {
            get { return _margin == null; }
        }

        /// <summary>
        /// Gets the margin relative to the width of the containing block or
        /// a fixed width, if any.
        /// </summary>
        public CSSCalcValue Margin
        {
            get { return _margin; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            //TODO
            //UNITLESS in QUIRKSMODE
            var calc = value.AsCalc();

            if (calc != null)
                _margin = calc;
            else if (value.Is("auto"))
                _margin = null;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
