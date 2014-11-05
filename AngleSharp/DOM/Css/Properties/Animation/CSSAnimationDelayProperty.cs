namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-delay
    /// </summary>
    sealed class CSSAnimationDelayProperty : CSSProperty, ICssAnimationDelayProperty
    {
        #region Fields

        readonly List<Time> _times;

        #endregion

        #region ctor

        internal CSSAnimationDelayProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.AnimationDelay, rule)
        {
            _times = new List<Time>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the delays for the animations.
        /// </summary>
        public IEnumerable<Time> Delays
        {
            get { return _times; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
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

                _times.Clear();
                _times.AddRange(times);
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
