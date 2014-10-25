namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation
    /// </summary>
    sealed class CSSAnimationProperty : CSSShorthandProperty, ICssAnimationProperty
    {
        #region Fields

        readonly CSSAnimationDelayProperty _delay;
        readonly CSSAnimationDirectionProperty _direction;
        readonly CSSAnimationDurationProperty _duration;
        readonly CSSAnimationFillModeProperty _fillMode;
        readonly CSSAnimationIterationCountProperty _iterationCount;
        readonly CSSAnimationNameProperty _name;
        readonly CSSAnimationTimingFunctionProperty _timingFunction;
        readonly CSSAnimationPlayStateProperty _playState;

        #endregion

        #region ctor

        internal CSSAnimationProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Animation, rule)
        {
            _delay = Get<CSSAnimationDelayProperty>();
            _direction = Get<CSSAnimationDirectionProperty>();
            _duration = Get<CSSAnimationDurationProperty>();
            _fillMode = Get<CSSAnimationFillModeProperty>();
            _iterationCount = Get<CSSAnimationIterationCountProperty>();
            _name = Get<CSSAnimationNameProperty>();
            _timingFunction = Get<CSSAnimationTimingFunctionProperty>();
            _playState = Get<CSSAnimationPlayStateProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the durations for the animations.
        /// </summary>
        public IEnumerable<Time> Durations
        {
            get { return _duration.Durations; }
        }

        /// <summary>
        /// Gets the offsets for the animations.
        /// </summary>
        public IEnumerable<Time> Delays
        {
            get { return _delay.Delays; }
        }

        /// <summary>
        /// Gets the timing-functions for the animations.
        /// </summary>
        public IEnumerable<TransitionFunction> TimingFunctions
        {
            get { return _timingFunction.TimingFunctions; }
        }

        /// <summary>
        /// Gets the names of the animations.
        /// </summary>
        public IEnumerable<String> Names
        {
            get { return _name.Names; }
        }

        /// <summary>
        /// Gets the fill modes of the animations.
        /// </summary>
        public IEnumerable<AnimationFillStyle> FillModes
        {
            get { return _fillMode.FillModes; }
        }

        /// <summary>
        /// Gets the directions of the animations.
        /// </summary>
        public IEnumerable<AnimationDirection> Directions
        {
            get { return _direction.Directions; }
        }

        /// <summary>
        /// Gets the iteraction counts of the animations.
        /// </summary>
        public IEnumerable<Int32> Iterations
        {
            get { return _iterationCount.Iterations; }
        }

        public IEnumerable<PlayState> States
        {
            get { return _playState.States; }
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
            var directions = new CSSValueList();
            var durations = new CSSValueList();
            var fillModes = new CSSValueList();
            var iterationCounts = new CSSValueList();
            var names = new CSSValueList();
            var timingFunctions = new CSSValueList();
            var playStates = new CSSValueList();

            foreach (var list in items)
            {
                if (list.Length > 8)
                    return false;

                CSSValue delay = null;
                CSSValue direction = null;
                CSSValue duration = null;
                CSSValue fillMode = null;
                CSSValue iterationCount = null;
                CSSValue name = null;
                CSSValue timingFunction = null;
                CSSValue playState = null;

                foreach (var item in list)
                {
                    if (!_name.CanStore(item, ref name) &&
                        !_duration.CanStore(item, ref duration) &&
                        !_timingFunction.CanStore(item, ref timingFunction) &&
                        !_delay.CanStore(item, ref delay) &&
                        !_iterationCount.CanStore(item, ref iterationCount) &&
                        !_direction.CanStore(item, ref direction) &&
                        !_fillMode.CanStore(item, ref fillMode) &&
                        !_playState.CanStore(item, ref playState))
                        return false;
                }
            }

            return _delay.TrySetValue(delays.Reduce()) && _direction.TrySetValue(directions.Reduce()) &&
                   _duration.TrySetValue(durations.Reduce()) && _fillMode.TrySetValue(fillModes.Reduce()) &&
                   _iterationCount.TrySetValue(iterationCounts.Reduce()) && _name.TrySetValue(names.Reduce()) &&
                   _timingFunction.TrySetValue(timingFunctions.Reduce()) && _playState.TrySetValue(playStates.Reduce());
        }

        #endregion
    }
}
