namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/orphans
    /// </summary>
    sealed class CSSOrphansProperty : CSSProperty, ICssOrphansProperty
    {
        #region Fields

        Int32 _value;

        #endregion

        #region ctor

        internal CSSOrphansProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Orphans, rule, PropertyFlags.Inherited)
        {
            Reset();
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
