namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/clear
    /// </summary>
    sealed class CssClearProperty : CssProperty, ICssClearProperty
    {
        #region Fields

        internal static readonly ClearMode Default = ClearMode.None;
        internal static readonly IValueConverter<ClearMode> Converter = Map.ClearModes.ToConverter();
        ClearMode _mode;

        #endregion

        #region ctor

        internal CssClearProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Clear, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the clear mode.
        /// </summary>
        public ClearMode State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetState(ClearMode mode)
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
