namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-duration
    /// </summary>
    public sealed class CSSTransitionDurationProperty : CSSProperty
    {
        #region Fields

        List<CSSPrimitiveValue<Time>> _times;

        #endregion

        #region ctor

        internal CSSTransitionDurationProperty()
            : base(PropertyNames.TransitionDuration)
        {
            _inherited = false;
            _times = new List<CSSPrimitiveValue<Time>>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the durations for the transitions.
        /// </summary>
        public IEnumerable<Time> Durations
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
