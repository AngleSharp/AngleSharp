namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/z-index
    /// </summary>
    sealed class CSSZIndexProperty : CSSProperty
    {
        #region Fields

        Int32? _value;

        #endregion

        #region ctor

        public CSSZIndexProperty()
            : base(PropertyNames.Z_INDEX)
        {
            _inherited = false;
            _value = null;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSNumberValue)
                _value = (Int32)((CSSNumberValue)value).Value;
            else if (value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals("auto", StringComparison.OrdinalIgnoreCase))
                _value = null;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
