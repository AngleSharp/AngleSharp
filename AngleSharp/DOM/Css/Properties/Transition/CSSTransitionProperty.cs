namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition
    /// </summary>
    sealed class CSSTransitionProperty : CSSShorthandProperty, ICssTransitionProperty
    {
        #region Fields

        readonly CSSTransitionDelayProperty _delay;
        readonly CSSTransitionDurationProperty _duration;
        readonly CSSTransitionTimingFunctionProperty _timingFunction;
        readonly CSSTransitionPropertyProperty _property;

        #endregion

        #region ctor

        internal CSSTransitionProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Transition, rule)
        {
            _delay = Get<CSSTransitionDelayProperty>();
            _duration = Get<CSSTransitionDurationProperty>();
            _timingFunction = Get<CSSTransitionTimingFunctionProperty>();
            _property = Get<CSSTransitionPropertyProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the durations for the transitions.
        /// </summary>
        public IEnumerable<Time> Durations
        {
            get { return _duration.Durations; }
        }

        /// <summary>
        /// Gets the offsets for the transitions.
        /// </summary>
        public IEnumerable<Time> Delays
        {
            get { return _delay.Delays; }
        }

        /// <summary>
        /// Gets the timing-functions for the transitions.
        /// </summary>
        public IEnumerable<TransitionFunction> TimingFunctions
        {
            get { return _timingFunction.TimingFunctions; }
        }

        /// <summary>
        /// Gets the properties for the transitions.
        /// </summary>
        public IEnumerable<String> Properties
        {
            get { return _property.Properties; }
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
            var items = (value as CSSValueList ?? new CSSValueList(value)).ToList();
            var delays = new CSSValueList();
            var durations = new CSSValueList();
            var timingFunctions = new CSSValueList();
            var properties = new CSSValueList();

            foreach (var list in items)
            {
                if (list.Length > 8)
                    return false;

                if (delays.Length != 0)
                {
                    delays.Add(CSSValue.Separator);
                    durations.Add(CSSValue.Separator);
                    timingFunctions.Add(CSSValue.Separator);
                    properties.Add(CSSValue.Separator);
                }

                CSSValue delay = null, duration = null, timingFunction = null, property = null;

                foreach (var item in list)
                {
                    if (!_property.CanStore(item, ref property) && !_duration.CanStore(item, ref duration) &&
                        !_timingFunction.CanStore(item, ref timingFunction) && !_delay.CanStore(item, ref delay))
                        return false;
                }

                delays.Add(delay ?? new CSSPrimitiveValue(Time.Zero));
                durations.Add(duration ?? new CSSPrimitiveValue(Time.Zero));
                timingFunctions.Add(timingFunction ?? new CSSPrimitiveValue(TransitionFunction.Ease));
                properties.Add(property ?? new CSSPrimitiveValue(new CssIdentifier(Keywords.All)));
            }

            return _property.TrySetValue(properties.Reduce()) && _delay.TrySetValue(delays.Reduce()) &&
                   _duration.TrySetValue(durations.Reduce()) && _timingFunction.TrySetValue(timingFunctions.Reduce());
        }

        #endregion
    }
}
