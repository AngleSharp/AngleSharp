namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/height
    /// </summary>
    sealed class CSSHeightProperty : CSSProperty
    {
        #region Fields

        static readonly AutoHeightMode _auto = new AutoHeightMode();
        HeightMode _mode;

        #endregion

        #region ctor

        public CSSHeightProperty()
            : base(PropertyNames.HEIGHT)
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
                _mode = new AbsoluteHeightMode(length.Value, length.PrimitiveType);
            }
            else if (value is CSSIdentifier && (((CSSIdentifier)value).Identifier).Equals("auto", StringComparison.OrdinalIgnoreCase))
                _mode = _auto;
            else if (value is CSSPercent)
                _mode = new RelativeHeightMode(((CSSPercent)value).Value);
            else if (value == CSSValue.Inherit)
                return true;
            else
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class HeightMode
        {
            //TODO Add members that make sense
        }

        class AutoHeightMode : HeightMode
        {
        }

        class RelativeHeightMode : HeightMode
        {
            Single _value;

            public RelativeHeightMode(Single value)
            {
                _value = value;
            }
        }

        class AbsoluteHeightMode : HeightMode
        {
            Single _value;
            CssUnit _unit;

            public AbsoluteHeightMode(Single value, CssUnit unit)
            {
                _value = value;
                _unit = unit;
            }
        }

        #endregion
    }
}
