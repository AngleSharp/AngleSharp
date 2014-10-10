namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-width
    /// </summary>
    sealed class CSSMinWidthProperty : CSSProperty, ICssMinWidthProperty
    {
        #region Fields

        IDistance _mode;

        #endregion

        #region ctor

        internal CSSMinWidthProperty()
            : base(PropertyNames.MinWidth, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the minimum height of the element.
        /// </summary>
        public IDistance Limit
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _mode = Percent.Zero;
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
            {
                _mode = distance;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
