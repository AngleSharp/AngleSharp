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

        #region Properties

        /// <summary>
        /// Gets the value that should be used for the opacity.
        /// </summary>
        public Single Opacity
        {
            get { return _value; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var num = value.ToNumber();

            if (num.HasValue)
                _value = num.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
