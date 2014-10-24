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

        readonly List<Transition> _transitions;

        #endregion

        #region ctor

        internal CSSTransitionProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Transition, rule)
        {
            _transitions = new List<Transition>();
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
                foreach (var time in _transitions)
                    yield return time.Duration;
            }
        }

        /// <summary>
        /// Gets the offsets for the transitions.
        /// </summary>
        public IEnumerable<Time> Delays
        {
            get
            {
                foreach (var time in _transitions)
                    yield return time.Delay;
            }
        }

        /// <summary>
        /// Gets the timing-functions for the transitions.
        /// </summary>
        public IEnumerable<TransitionFunction> TimingFunctions
        {
            get
            {
                foreach (var time in _transitions)
                    yield return time.Timing;
            }
        }

        /// <summary>
        /// Gets the properties for the transitions.
        /// </summary>
        public IEnumerable<String> Properties
        {
            get
            {
                foreach (var time in _transitions)
                    yield return time.Property;
            }
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
            if (value.Is(Keywords.None))
            {
                _transitions.Clear();
                return true;
            }
            
            var transition = ParseValue(value);

            if (transition.HasValue)
            {
                _transitions.Clear();
                _transitions.Add(transition.Value);
            }
            else if (value is CSSValueList)
            {
                var values = ((CSSValueList)value).ToList();
                var transitions = new List<Transition>();

                foreach (var item in values)
                {
                    var t = item.Length == 1 ? ParseValue(item[0]) : ParseValue(item);

                    if (!t.HasValue)
                        return false;

                    transitions.Add(t.Value);
                }

                _transitions.Clear();
                _transitions.AddRange(transitions);
            }
            else
                return false;

            return true;
        }

        Transition? ParseValue(CSSValue value)
        {
            Time? duration = null;
            String property = null;
            var function = value.ToTimingFunction();

            if (function == null && (property = value.ToIdentifier()) == null && !(duration = value.ToTime()).HasValue)
                return null;

            return new Transition
            {
                Delay = Time.Zero,
                Duration = duration ?? Time.Zero,
                Timing = function ?? TransitionFunction.Ease,
                Property = property ?? Keywords.All
            };
        }

        Transition? ParseValue(CSSValueList values)
        {
            Time? delay = null;
            Time? duration = null;
            TransitionFunction function = null;
            String property = null;

            for (var i = 0; i < values.Length; i++)
            {
                if (function == null && (function = values[i].ToTimingFunction()) != null)
                    continue;

                if (property == null && (property = values[i].ToIdentifier()) != null)
                    continue;

                var time = values[i].ToTime();

                if (time.HasValue)
                {
                    if (duration == null)
                    {
                        duration = time;
                        continue;
                    }
                    else if (delay == null)
                    {
                        delay = time;
                        continue;
                    }
                }

                return null;
            }

            return new Transition
            {
                Delay = delay ?? Time.Zero,
                Duration = duration ?? Time.Zero,
                Timing = function ?? TransitionFunction.Ease,
                Property = property ?? Keywords.All
            };
        }

        #endregion

        #region Transition

        struct Transition
        {
            public Time Delay;
            public Time Duration;
            public TransitionFunction Timing;
            public String Property;
        }

        #endregion
    }
}
