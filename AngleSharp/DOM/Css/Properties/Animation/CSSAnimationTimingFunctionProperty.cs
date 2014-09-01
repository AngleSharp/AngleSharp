namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-timing-function
    /// </summary>
    sealed class CSSAnimationTimingFunctionProperty : CSSProperty, ICssAnimationTimingFunctionProperty
    {
        #region Fields

        List<CSSTimingValue> _functions;

        #endregion

        #region ctor

        internal CSSAnimationTimingFunctionProperty()
            : base(PropertyNames.AnimationTimingFunction)
        {
            _functions = new List<CSSTimingValue>();
            _functions.Add(CSSTimingValue.Ease);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration over all timing functions.
        /// </summary>
        internal IEnumerable<CSSTimingValue> TimingFunctions
        {
            get { return _functions; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var values = value.AsList(ValueExtensions.ToTimingFunction);

            if (values != null)
                _functions = values;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
