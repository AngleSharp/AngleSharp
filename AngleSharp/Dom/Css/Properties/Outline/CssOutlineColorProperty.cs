namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-color
    /// Gets the color of the outline.
    /// Gets if the color is inverted.
    /// </summary>
    sealed class CssOutlineColorProperty : CssProperty
    {
        #region ctor

        internal CssOutlineColorProperty()
            : base(PropertyNames.OutlineColor, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.InvertedColorConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Color.Transparent;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.InvertedColorConverter.Validate(value);
        }

        #endregion
    }
}
