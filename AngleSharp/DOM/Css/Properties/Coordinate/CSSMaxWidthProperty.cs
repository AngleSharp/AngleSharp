namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/max-width
    /// </summary>
    sealed class CSSMaxWidthProperty : CSSProperty, ICssMaxWidthProperty
    {
        #region Fields

        /// <summary>
        /// The width has no maximum value if _mode == null.
        /// </summary>
        IDistance _mode;

        #endregion

        #region ctor

        internal CSSMaxWidthProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.MaxWidth, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specified max-width of the element. A percentage is calculated
        /// with respect to the width of the containing block.
        /// </summary>
        public IDistance Limit
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _mode = null;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var distance = value.ToDistance();

            if (distance != null)
                _mode = distance;
            else if (value.Is(Keywords.None))
                _mode = null;
            else
                return false;

            return true;
        }

        #endregion
    }
}
