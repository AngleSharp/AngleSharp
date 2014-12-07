namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition
    /// </summary>
    sealed class CSSTransitionProperty : CSSShorthandProperty, ICssTransitionProperty
    {
        #region Fields

        internal static readonly IValueConverter<Tuple<String, Time, TransitionFunction, Time>[]> Converter = TakeList(WithAny(
            CSSTransitionPropertyProperty.SingleConverter.Option(CSSTransitionPropertyProperty.Default),
            CSSTransitionDurationProperty.SingleConverter.Option(CSSTransitionDurationProperty.Default),
            CSSTransitionTimingFunctionProperty.SingleConverter.Option(CSSTransitionTimingFunctionProperty.Default),
            CSSTransitionDelayProperty.SingleConverter.Option(CSSTransitionDelayProperty.Default)));

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
        IEnumerable<String> ICssTransitionPropertyProperty.Properties
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
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, t =>
            {
                _property.SetProperties(t.Select(m => m.Item1));
                _duration.SetDurations(t.Select(m => m.Item2));
                _timingFunction.SetTimingFunctions(t.Select(m => m.Item3));
                _delay.SetDelays(t.Select(m => m.Item4));
            });
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!properties.Contains(_property) || !properties.Contains(_duration))
                return String.Empty;

            var values = new List<String>();
            values.Add(_property.SerializeValue());
            values.Add(_duration.SerializeValue());

            if (_timingFunction.HasValue && properties.Contains(_timingFunction))
                values.Add(_timingFunction.SerializeValue());

            if (_delay.HasValue && properties.Contains(_delay))
                values.Add(_delay.SerializeValue());

            return String.Join(" ", values);
        }

        #endregion
    }
}
