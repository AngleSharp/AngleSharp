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

        CSSCalcValue _value;

        #endregion

        #region ctor

        internal CSSCoordinateProperty(String name)
            : base(name, PropertyFlags.Unitless)
        {
            _value = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the position is automatically calculated.
        /// </summary>
        public Boolean IsAuto
        {
            get { return _value == null; }
        }

        /// <summary>
        /// Gets the position if a fixed position has been set.
        /// </summary>
        internal CSSCalcValue Position
        {
            get { return _value; }
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
                _value = calc;
            else if (value.Is("auto"))
                _value = null;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
