namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/widows
    /// </summary>
    sealed class CSSWidowsProperty : CSSProperty
    {
        #region Fields

        /// <summary>
        /// Denotes the minimum amount of lines that can stay alone
        /// on the top of a new page. If the value is not positive,
        /// the declaration is invalid.
        /// </summary>
        Int32 _value;

        #endregion

        #region ctor

        public CSSWidowsProperty()
            : base(PropertyNames.Widows)
        {
            _inherited = true;
            _value = 2;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSNumberValue)
            {
                var num = (Int32)((CSSNumberValue)value).Value;

                if (num < 0)
                    return false;

                _value = num;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
