namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Basis for all properties that have either a length
    /// or percentage value or an auto value - nothing else.
    /// </summary>
    public class CSSCoordinateProperty : CSSProperty
    {
        #region Fields

        static readonly AutoCoordinateMode _auto = new AutoCoordinateMode();
        CoordinateMode _mode;

        #endregion

        #region ctor

        protected CSSCoordinateProperty(String name)
            : base(name)
        {
            _inherited = false;
            _mode = _auto;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the position is automatically calculated.
        /// </summary>
        public Boolean IsAuto
        {
            get { return _mode is AutoCoordinateMode; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            //TODO
            //UNITLESS in QUIRKSMODE
            var calc = value.AsCalc();

            if (calc != null)
                _mode = new CalcCoordinateMode(calc);
            else if (value.Is("auto"))
                _mode = _auto;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class CoordinateMode
        {
            //TODO Add members that make sense
        }

        sealed class AutoCoordinateMode : CoordinateMode
        {
        }

        sealed class CalcCoordinateMode : CoordinateMode
        {
            CSSCalcValue _calc;

            public CalcCoordinateMode(CSSCalcValue calc)
            {
                _calc = calc;
            }
        }

        #endregion
    }
}
