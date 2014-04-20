namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/widows
    /// </summary>
    public sealed class CSSWidowsProperty : CSSProperty
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

        internal CSSWidowsProperty()
            : base(PropertyNames.Widows)
        {
            _inherited = true;
            _value = 2;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of lines, which must be left on top
        /// of a new page, on a paged media.
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
