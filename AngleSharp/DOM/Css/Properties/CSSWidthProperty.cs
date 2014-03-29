namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/width
    /// </summary>
    sealed class CSSWidthProperty : CSSProperty
    {
        #region Fields

        static readonly AutoWidthMode _auto = new AutoWidthMode();
        WidthMode _mode;

        #endregion

        #region ctor

        public CSSWidthProperty()
            : base(PropertyNames.WIDTH)
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
                _mode = new AbsoluteWidthMode(length.Value, length.PrimitiveType);
            }
            else if (value is CSSIdentifier && (((CSSIdentifier)value).Identifier).Equals("auto", StringComparison.OrdinalIgnoreCase))
                _mode = _auto;
            else if (value is CSSPercent)
                _mode = new RelativeWidthMode(((CSSPercent)value).Value);
            else if (value == CSSValue.Inherit)
                return true;
            else
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class WidthMode
        {
            //TODO Add members that make sense
        }

        class AutoWidthMode : WidthMode
        {
        }

        class RelativeWidthMode : WidthMode
        {
            Single _value;

            public RelativeWidthMode(Single value)
            {
                _value = value;
            }
        }

        class AbsoluteWidthMode : WidthMode
        {
            Single _value;
            CssUnit _unit;

            public AbsoluteWidthMode(Single value, CssUnit unit)
            {
                _value = value;
                _unit = unit;
            }
        }

        #endregion
    }
}
