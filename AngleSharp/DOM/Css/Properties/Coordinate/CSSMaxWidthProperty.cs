namespace AngleSharp.DOM.Css.Properties
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

        internal CSSMaxWidthProperty()
            : base(PropertyNames.MaxWidth)
        {
            _mode = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if a limit has been specified, otherwise the value is none.
        /// </summary>
        public Boolean IsLimited
        {
            get { return _mode != null; }
        }

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
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
