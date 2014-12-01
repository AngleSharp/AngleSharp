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

        internal static readonly IValueConverter<Int32> SingleConverter = WithInteger().Constraint(m => m >= 0).Or(TakeOne(Keywords.Infinite, -1));
        internal static readonly IValueConverter<Int32[]> Converter = TakeList(SingleConverter);
        internal static readonly Int32 Default = 1;
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
            _iterations.Add(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetIterations);
        }

        #endregion
    }
}
