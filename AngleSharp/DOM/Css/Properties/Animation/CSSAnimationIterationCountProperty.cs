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

        public void SetIterations(IEnumerable<Int32> iterations)
        {
            _iterations.Clear();
            _iterations.AddRange(iterations);
        }

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
            return this.TakeList(this.WithInteger().Constraint(m => m >= 0).Or(
                   this.TakeOne(Keywords.Infinite, -1))).TryConvert(value, SetIterations);
        }

        #endregion
    }
}
