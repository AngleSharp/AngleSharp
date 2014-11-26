namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-height
    /// </summary>
    sealed class CSSMinHeightProperty : CSSProperty, ICssMinHeightProperty
    {
        #region Fields

        IDistance _mode;

        #endregion

        #region ctor

        internal CSSMinHeightProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.MinHeight, rule, PropertyFlags.Animatable)
        {
            Reset();
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

        public void SetLimit(IDistance mode)
        {
            _mode = mode;
        }

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
            return this.WithDistance().TryConvert(value, SetLimit);
        }

        #endregion
    }
}
