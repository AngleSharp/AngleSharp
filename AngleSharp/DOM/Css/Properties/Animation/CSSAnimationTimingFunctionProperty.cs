namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-timing-function
    /// </summary>
    sealed class CSSAnimationTimingFunctionProperty : CSSProperty, ICssAnimationTimingFunctionProperty
    {
        #region Fields

        internal static readonly IValueConverter<TransitionFunction> SingleConverter = WithTransition();
        internal static readonly IValueConverter<TransitionFunction[]> Converter = TakeList(SingleConverter);
        internal static readonly TransitionFunction Default = TransitionFunction.Ease;
        readonly List<TransitionFunction> _functions;

        #endregion

        #region ctor

        internal CSSAnimationTimingFunctionProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.AnimationTimingFunction, rule)
        {
            _functions = new List<TransitionFunction>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration over all timing functions.
        /// </summary>
        public IEnumerable<TransitionFunction> TimingFunctions
        {
            get { return _functions; }
        }

        #endregion

        #region Methods

        public void SetTimingFunctions(IEnumerable<TransitionFunction> functions)
        {
            _functions.Clear();
            _functions.AddRange(functions);
        }

        internal override void Reset()
        {
            _functions.Clear();
            _functions.Add(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetTimingFunctions);
        }

        #endregion
    }
}
