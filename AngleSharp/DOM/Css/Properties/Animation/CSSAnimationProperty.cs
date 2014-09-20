namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation
    /// </summary>
    sealed class CSSAnimationProperty : CSSProperty, ICssAnimationProperty
    {
        #region Fields

        List<Animation> _animations;

        #endregion

        #region ctor

        internal CSSAnimationProperty()
            : base(PropertyNames.Animation)
        {
            _animations = new List<Animation>();
            _animations.Add(new Animation
            {
                Delay = Time.Zero,
                Timing = CSSTimingValue.Ease,
                Duration = Time.Zero,
                FillMode = AnimationFillStyle.None,
                IterationCount = 1,
                Direction = AnimationDirection.Normal,
                Name = Keywords.None
            });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the durations for the animations.
        /// </summary>
        public IEnumerable<Time> Durations
        {
            get
            {
                foreach (var time in _animations)
                    yield return time.Duration;
            }
        }

        /// <summary>
        /// Gets the offsets for the animations.
        /// </summary>
        public IEnumerable<Time> Delays
        {
            get
            {
                foreach (var time in _animations)
                    yield return time.Delay;
            }
        }

        /// <summary>
        /// Gets the timing-functions for the animations.
        /// </summary>
        internal IEnumerable<CSSTimingValue> TimingFunctions
        {
            get
            {
                foreach (var time in _animations)
                    yield return time.Timing;
            }
        }

        /// <summary>
        /// Gets the names of the animations.
        /// </summary>
        public IEnumerable<String> Names
        {
            get
            {
                foreach (var time in _animations)
                    yield return time.Name;
            }
        }

        /// <summary>
        /// Gets the fill modes of the animations.
        /// </summary>
        public IEnumerable<AnimationFillStyle> FillModes
        {
            get
            {
                foreach (var time in _animations)
                    yield return time.FillMode;
            }
        }

        /// <summary>
        /// Gets the directions of the animations.
        /// </summary>
        public IEnumerable<AnimationDirection> Directions
        {
            get
            {
                foreach (var time in _animations)
                    yield return time.Direction;
            }
        }

        /// <summary>
        /// Gets the iteraction counts of the animations.
        /// </summary>
        public IEnumerable<Int32> Iterations
        {
            get
            {
                foreach (var time in _animations)
                    yield return time.IterationCount;
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
            var transition = ParseValue(value);

            if (transition.HasValue)
            {
                _animations.Clear();
                _animations.Add(transition.Value);
            }
            else if (value is CSSValueList)
            {
                var values = ((CSSValueList)value).ToList();
                var animations = new List<Animation>();

                foreach (var item in values)
                {
                    var t = item.Length == 1 ? ParseValue(item[0]) : ParseValue(item);

                    if (!t.HasValue)
                        return false;

                    animations.Add(t.Value);
                }

                _animations = animations;
            }
            else if (value != CSSValueList.Inherit)
                return false;

            return true;
        }

        Animation? ParseValue(CSSValue value)
        {
            Time? duration = null;
            Int32? iterationCount = null;
            var function = value.ToTimingFunction();
            var name = Keywords.None;

            if (function == null)
            {
                function = CSSTimingValue.Ease;

                if (value is CSSIdentifierValue)
                    name = ((CSSIdentifierValue)value).Value;
                else if (!(duration = value.ToTime()).HasValue && !(iterationCount = value.ToInteger()).HasValue)
                    return null;
            }

            return new Animation
            {
                Delay = Time.Zero,
                Duration = duration ?? Time.Zero,
                Timing = function,
                Name = name,
                IterationCount = iterationCount ?? 1,
                FillMode = AnimationFillStyle.None,
                Direction = AnimationDirection.Normal
            };
        }

        Animation? ParseValue(CSSValueList values)
        {
            Time? delay = null;
            Time? duration = null;
            Int32? iterationCount = null;
            CSSTimingValue function = null;
            AnimationFillStyle? fillMode = null;
            AnimationDirection? direction = null;
            String name = null;

            for (var i = 0; i < values.Length; i++)
            {
                if (function == null && (function = values[i].ToTimingFunction()) != null)
                    continue;

                if (values[i] is CSSIdentifierValue)
                {
                    if (name == null)
                    {
                        name = ((CSSIdentifierValue)values[i]).Value;
                        continue;
                    }

                    if (!fillMode.HasValue && (fillMode = values[i].ToFillMode()).HasValue)
                        continue;

                    if (!direction.HasValue && (direction = values[i].ToDirection()).HasValue)
                        continue;
                }

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
                else if (!iterationCount.HasValue && (iterationCount = values[i].ToInteger()).HasValue)
                    continue;

                return null;
            }

            return new Animation
            {
                Delay = delay ?? Time.Zero,
                Duration = duration ?? Time.Zero,
                Timing = function ?? CSSTimingValue.Ease,
                Name = name ?? Keywords.None,
                IterationCount = iterationCount ?? 1,
                FillMode = fillMode.HasValue ? fillMode.Value : AnimationFillStyle.None,
                Direction = direction.HasValue ? direction.Value : AnimationDirection.Normal
            };
        }

        #endregion

        #region Animation

        struct Animation
        {
            public Time Delay;
            public Time Duration;
            public CSSTimingValue Timing;
            public Int32 IterationCount;
            public AnimationDirection Direction;
            public AnimationFillStyle FillMode;
            public String Name;
        }

        #endregion
    }
}
