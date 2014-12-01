namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-size
    /// </summary>
    sealed class CSSFontSizeProperty : CSSProperty, ICssFontSizeProperty
    {
        #region Fields

        internal static readonly IDistance Default = FontSize.Medium.ToDistance();
        internal static readonly IValueConverter<IDistance> Converter = WithDistance().Or(From(Map.FontSizes).To(m => m.ToDistance()));
        IDistance _size;

        #endregion

        #region ctor

        internal CSSFontSizeProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.FontSize, rule, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected font-size.
        /// </summary>
        public IDistance Size
        {
            get { return _size; }
        }

        #endregion

        #region Methods

        public void SetSize(IDistance size)
        {
            _size = size;
        }

        internal override void Reset()
        {
            _size = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetSize);
        }

        #endregion
    }
}
