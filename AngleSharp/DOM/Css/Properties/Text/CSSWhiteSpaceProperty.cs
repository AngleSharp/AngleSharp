namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/white-space
    /// </summary>
    sealed class CSSWhiteSpaceProperty : CSSProperty, ICssWhitespaceProperty
    {
        #region Fields

        internal static readonly Whitespace Default = Whitespace.Normal;
        internal static readonly IValueConverter<Whitespace> Converter = Map.WhitespaceModes.ToConverter();
        Whitespace _mode;

        #endregion

        #region ctor

        internal CSSWhiteSpaceProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.WhiteSpace, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected whitespace handling mode.
        /// </summary>
        public Whitespace State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetState(Whitespace mode)
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
