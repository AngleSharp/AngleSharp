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
            : base(PropertyNames.MinWidth)
        {
            _mode = Percent.Zero;
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
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
