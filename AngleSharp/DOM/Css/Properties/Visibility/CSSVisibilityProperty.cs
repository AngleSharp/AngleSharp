namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/visibility
    /// </summary>
    sealed class CSSVisibilityProperty : CSSProperty, ICssVisibilityProperty
    {
        #region Fields

        Visibility _mode;

        #endregion

        #region ctor

        internal CSSVisibilityProperty()
            : base(PropertyNames.Visibility, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the visibility mode.
        /// </summary>
        public Visibility Visibility
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _mode = Visibility.Visible;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var mode = value.ToVisibility();

            if (mode.HasValue)
            {
                _mode = mode.Value;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
