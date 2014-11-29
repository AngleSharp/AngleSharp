namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/overflow
    /// </summary>
    sealed class CSSOverflowProperty : CSSProperty, ICssOverflowProperty
    {
        #region Fields

        internal static readonly OverflowMode Default = OverflowMode.Visible;
        internal static readonly IValueConverter<OverflowMode> Converter = From(Map.OverflowModes);
        OverflowMode _mode;

        #endregion

        #region ctor

        internal CSSOverflowProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Overflow, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        public OverflowMode State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetState(OverflowMode mode)
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
        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, SetState);
        }

        #endregion
    }
}
