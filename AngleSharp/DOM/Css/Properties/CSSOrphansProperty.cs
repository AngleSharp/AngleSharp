namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/orphans
    /// </summary>
    sealed class CSSOrphansProperty : CSSProperty
    {
        #region Fields

        Int32 _value;

        #endregion

        #region ctor

        public CSSOrphansProperty()
            : base(PropertyNames.ORPHANS)
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
