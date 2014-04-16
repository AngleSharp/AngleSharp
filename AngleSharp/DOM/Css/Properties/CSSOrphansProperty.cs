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
            : base(PropertyNames.Orphans)
        {
            _inherited = true;
            _value = 2;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the minimum number of lines in a block container
        /// that must be left at the bottom of the page. 
        /// </summary>
        public Int32 Count
        {
            get { return _value; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var num = value.ToInteger();

            if (num.HasValue && num.Value >= 0)
                _value = num.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
