namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Basis for all properties that have either a length
    /// or percentage value or an auto value - nothing else.
    /// </summary>
    abstract class CSSCoordinateProperty : CSSProperty
    {
        #region Fields

        static readonly AutoCoordinateMode _auto = new AutoCoordinateMode();
        CoordinateMode _mode;

        #endregion

        #region ctor

        public CSSCoordinateProperty(String name)
            : base(name)
        {
            _inherited = false;
            _mode = _auto;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSLength)
            {
                var length = (CSSLength)value;
                _mode = new AbsoluteCoordinateMode(length.Value, length.PrimitiveType);
            }
            else if (value is CSSIdentifier && (((CSSIdentifier)value).Identifier).Equals("auto", StringComparison.OrdinalIgnoreCase))
                _mode = _auto;
            else if (value is CSSPercent)
                _mode = new RelativeCoordinateMode(((CSSPercent)value).Value);
            else if (value == CSSValue.Inherit)
                return true;
            else
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class CoordinateMode
        {
            //TODO Add members that make sense
        }

        class AutoCoordinateMode : CoordinateMode
        {
        }

        class RelativeCoordinateMode : CoordinateMode
        {
            Single _value;

            public RelativeCoordinateMode(Single value)
            {
                _value = value;
            }
        }

        class AbsoluteCoordinateMode : CoordinateMode
        {
            Single _value;
            CssUnit _unit;

            public AbsoluteCoordinateMode(Single value, CssUnit unit)
            {
                _value = value;
                _unit = unit;
            }
        }

        #endregion
    }
}
