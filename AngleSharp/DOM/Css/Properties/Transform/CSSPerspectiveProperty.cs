namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective
    /// </summary>
    sealed class CSSPerspectiveProperty : CSSProperty, ICssPerspectiveProperty
    {
        #region Fields

        Length _distance;

        #endregion

        #region ctor

        internal CSSPerspectiveProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Perspective, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the distance from the user to the z=0 plane.
        /// It is used to apply a perspective transform to the element and
        /// its content. If it 0 or a negative value, no perspective transform
        /// is applied.
        /// </summary>
        public Length Distance
        {
            get { return _distance; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _distance = Length.Zero;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var distance = value.ToLength();

            if (distance.HasValue)
                _distance = distance.Value;
            //Is a keyword indicating that no perspective transform has to be applied.
            else if (value.Is(Keywords.None))
                _distance = Length.Zero;
            else
                return false;

            return true;
        }

        #endregion
    }
}
