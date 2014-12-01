namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-width
    /// </summary>
    sealed class CSSMinWidthProperty : CSSProperty, ICssMinWidthProperty
    {
        #region Fields

        internal static readonly IDistance Default = Percent.Zero;
        internal static readonly IValueConverter<IDistance> Converter = WithDistance();
        IDistance _mode;

        #endregion

        #region ctor

        internal CSSMinWidthProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.MinWidth, rule, PropertyFlags.Animatable)
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
            _mode = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetLimit);
        }

        #endregion
    }
}
