namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/position
    /// </summary>
    sealed class CSSPositionProperty : CSSProperty, ICssPositionProperty
    {
        #region Fields

        internal static readonly PositionMode Default = PositionMode.Static;
        internal static readonly IValueConverter<PositionMode> Converter = From(Map.PositionModes);
        PositionMode _mode;

        #endregion

        #region ctor

        internal CSSPositionProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Position, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the currently selected position mode.
        /// </summary>
        public PositionMode State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetState(PositionMode mode)
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
