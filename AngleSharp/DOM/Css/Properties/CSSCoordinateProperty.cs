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
            var length = value.ToLength();

            if (length.HasValue)
                _mode = new AbsoluteCoordinateMode(length.Value);
            else if (value is CSSPercentValue)
                _mode = new RelativeCoordinateMode(((CSSPercentValue)value).Value);
            else if (value is CSSIdentifierValue && (((CSSIdentifierValue)value).Value).Equals("auto", StringComparison.OrdinalIgnoreCase))
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
            Length _value;

            public AbsoluteCoordinateMode(Length value)
            {
                _value = value;
            }
        }

        #endregion
    }
}
