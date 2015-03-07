namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/display
    /// </summary>
    sealed class CssDisplayProperty : CssProperty
    {
        #region Fields

        internal static readonly DisplayMode Default = DisplayMode.Inline;
        internal static readonly IValueConverter<DisplayMode> Converter = Map.DisplayModes.ToConverter();
        DisplayMode _mode;

        #endregion

        #region ctor

        internal CssDisplayProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Display, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the display mode.
        /// </summary>
        public DisplayMode State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetState(DisplayMode mode)
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
            return Converter.TryConvert(value, SetState);
        }

        #endregion
    }
}
