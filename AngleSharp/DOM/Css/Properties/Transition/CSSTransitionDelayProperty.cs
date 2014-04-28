namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-delay
    /// </summary>
    public sealed class CSSTransitionDelayProperty : CSSProperty
    {
        #region Fields

        List<CSSPrimitiveValue<Time>> _times;

        #endregion

        #region ctor

        internal CSSTransitionDelayProperty()
            : base(PropertyNames.TransitionDelay)
        {
            _inherited = false;
            _times = new List<CSSPrimitiveValue<Time>>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the delays for the transitions.
        /// </summary>
        public IEnumerable<Time> Delays
        {
            get
            {
                foreach (var time in _times)
                    yield return time.Value;
            }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var values = value.AsList<CSSPrimitiveValue<Time>>();

            if (values != null)
                _times = values;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
