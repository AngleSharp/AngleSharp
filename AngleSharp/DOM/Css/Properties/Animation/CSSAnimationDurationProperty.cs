namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-duration
    /// </summary>
    sealed class CSSAnimationDurationProperty : CSSProperty, ICssAnimationDurationProperty
    {
        #region Fields

        List<Time> _times;

        #endregion

        #region ctor

        internal CSSAnimationDurationProperty()
            : base(PropertyNames.AnimationDuration)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the durations for the animations.
        /// </summary>
        public IEnumerable<Time> Durations
        {
            get { return _times; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            if (_times == null)
                _times = new List<Time>();
            else
                _times.Clear();

            _times.Add(Time.Zero);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var values = value.AsList<CSSPrimitiveValue>();

            if (values != null)
            {
                var times = new List<Time>();

                foreach (var v in values)
                {
                    var time = v.ToTime();

                    if (time == null)
                        return false;

                    times.Add(time.Value);
                }

                _times = times;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
