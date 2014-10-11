namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/widows
    /// </summary>
    sealed class CSSWidowsProperty : CSSProperty, ICssWidowsProperty
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
            : base(PropertyNames.Widows, PropertyFlags.Inherited)
        {
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

        internal override void Reset()
        {
            _value = 2;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var num = value.ToInteger();

            if (num.HasValue && num.Value >= 0)
            {
                _value = num.Value;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
