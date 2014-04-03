namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/opacity
    /// </summary>
    sealed class CSSOpacityProperty : CSSProperty
    {
        #region Fields

        Single _value;

        #endregion

        #region ctor

        public CSSOpacityProperty()
            : base(PropertyNames.Opacity)
        {
            _inherited = false;
            _value = 1f;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSNumberValue)
            {
                _value = ((CSSNumberValue)value).Value;
                return true;
            }
            else if (value == CSSValue.Inherit)
                return true;

            return false;
        }

        #endregion
    }
}
