namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-color
    /// </summary>
    sealed class CssOutlineColorProperty : CssProperty, ICssOutlineColorProperty
    {
        #region Fields

        internal static readonly Color? Default = null;
        internal static readonly IValueConverter<Color?> Converter = Converters.ColorConverter.WithCurrentColor().ToNullable().Or(Keywords.Invert, Default);
        Color? _color;

        #endregion

        #region ctor

        internal CssOutlineColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.OutlineColor, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the color of the outline.
        /// </summary>
        public Color Color
        {
            get { return _color.HasValue ? _color.Value : Color.Transparent; }
        }

        /// <summary>
        /// Gets if the color is inverted.
        /// </summary>
        public Boolean IsInverted
        {
            get { return !_color.HasValue; }
        }

        #endregion

        #region Methods

        public void SetColor(Color? color)
        {
            _color = color;
        }

        internal override void Reset()
        {
            _color = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetColor);
        }

        #endregion
    }
}
