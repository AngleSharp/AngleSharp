namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-width
    /// </summary>
    sealed class CssColumnWidthProperty : CssProperty, ICssColumnWidthProperty
    {
        #region Fields

        internal static readonly Length? Default = null;
        internal static readonly IValueConverter<Length?> Converter = Converters.LengthConverter.OrNullDefault();
        /// <summary>
        /// Null indicates that other properties (column-count) should be considered.
        /// </summary>
        Length? _width;

        #endregion

        #region ctor

        internal CssColumnWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ColumnWidth, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of a single columns.
        /// </summary>
        public Length? Width
        {
            get { return _width; }
        }

        #endregion

        #region Methods

        public void SetWidth(Length? width)
        {
            _width = width;
        }

        internal override void Reset()
        {
            _width = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetWidth);
        }

        #endregion
    }
}
