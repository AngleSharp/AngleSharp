namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-timing-function
    /// </summary>
    public sealed class CSSTransitionTimingFunctionProperty : CSSProperty
    {
        #region Fields

        List<CSSTimingValue> _functions;

        #endregion

        #region ctor

        internal CSSTransitionTimingFunctionProperty()
            : base(PropertyNames.TransitionTimingFunction)
        {
            _inherited = false;
            _functions = new List<CSSTimingValue>();
            _functions.Add(CSSTimingValue.Ease);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration over all timing functions.
        /// </summary>
        public IEnumerable<CSSTimingValue> TimingFunctions
        {
            get { return _functions; }
        }

        #endregion

        #region Methods

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
