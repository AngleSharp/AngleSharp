namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// The abstract base class for all border-color sub-properties.
    /// </summary>
    abstract class CSSBorderPartColorProperty : CSSProperty
    {
        #region Fields

        internal static readonly Color Default = Color.Transparent;
        internal static readonly IValueConverter<Color> Converter = Converters.ColorConverter;
        Color _color;

        #endregion

        #region ctor

        internal CSSBorderPartColorProperty(String name, CSSStyleDeclaration rule)
            : base(name, rule, PropertyFlags.Hashless | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected color for the border.
        /// </summary>
        public Color Color
        {
            get { return _color; }
        }

        #endregion

        #region Methods

        void SetColor(Color color)
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
