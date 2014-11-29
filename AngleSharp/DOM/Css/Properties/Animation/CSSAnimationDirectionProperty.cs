namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-direction
    /// </summary>
    sealed class CSSAnimationDirectionProperty : CSSProperty, ICssAnimationDirectionProperty
    {
        #region Fields

        internal static readonly IValueConverter<AnimationDirection> SingleConverter = From(Map.AnimationDirections);
        internal static readonly IValueConverter<AnimationDirection[]> Converter = TakeList(SingleConverter);
        internal static readonly AnimationDirection Default = AnimationDirection.Normal;
        readonly List<AnimationDirection> _directions;

        #endregion

        #region ctor

        internal CSSAnimationDirectionProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.AnimationDirection, rule)
        {
            _directions = new List<AnimationDirection>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an iteration over all defined directions.
        /// </summary>
        public IEnumerable<AnimationDirection> Directions
        {
            get { return _directions; }
        }

        #endregion

        #region Methods

        public void SetDirections(IEnumerable<AnimationDirection> directions)
        {
            _directions.Clear();
            _directions.AddRange(directions);
        }

        internal override void Reset()
        {
            _directions.Clear();
            _directions.Add(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, SetDirections);
        }

        #endregion
    }
}
