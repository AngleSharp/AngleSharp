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
    sealed class CSSAnimationIterationCountProperty : CssProperty, ICssAnimationIterationCountProperty
    {
        #region Fields

        internal static readonly IValueConverter<Single> SingleConverter = Converters.NumberConverter.Constraint(m => m >= 0f).Or(Keywords.Infinite, Single.PositiveInfinity);
        internal static readonly IValueConverter<Single[]> Converter = SingleConverter.FromList();
        internal static readonly Single Default = 1f;
        readonly List<Single> _iterations;

        #endregion

        #region ctor

        internal CSSAnimationIterationCountProperty(CssStyleDeclaration rule)
            : base(PropertyNames.AnimationIterationCount, rule)
        {
            _iterations = new List<Single>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the iteration count of the covered animations.
        /// </summary>
        public IEnumerable<Single> Iterations
        {
            get { return _iterations; }
        }

        #endregion

        #region Methods

        public void SetIterations(IEnumerable<Single> iterations)
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
