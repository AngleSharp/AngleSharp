namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-iteration-count
    /// </summary>
    sealed class CSSAnimationIterationCountProperty : CSSProperty, ICssAnimationIterationCountProperty
    {
        #region Fields

        readonly List<Int32> _iterations;

        #endregion

        #region ctor

        internal CSSAnimationIterationCountProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.AnimationIterationCount, rule)
        {
            _iterations = new List<Int32>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the iteration count of the covered animations.
        /// </summary>
        public IEnumerable<Int32> Iterations
        {
            get { return _iterations; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _iterations.Clear();
            _iterations.Add(1);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var values = value.AsList(ToNumber);

            if (values != null)
            {
                var iterations = new List<Int32>();

                foreach (var v in values)
                {
                    var n = v.ToInteger();

                    if (n == null)
                        return false;

                    iterations.Add(n.Value);
                }


                _iterations.Clear();
                _iterations.AddRange(iterations);
                return true;
            }
            
            return false;
        }

        static CSSPrimitiveValue ToNumber(CSSValue value)
        {
            var number = value.ToInteger();

            if (number != null)
            {
                var n = number.Value;

                if (n >= 0)
                    return (CSSPrimitiveValue)value;
            }
            else if (value.Is(Keywords.Infinite))
                return new CSSPrimitiveValue(Number.Infinite);

            return null;
        }

        #endregion
    }
}
