namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/word-spacing
    /// Gets if normal inter-word space, as defined by the current font
    /// and/or the browser, is active.
    /// Gets the defined custom spacing, if any.
    /// </summary>
    sealed class CssWordSpacingProperty : CssProperty
    {
        #region ctor

        internal CssWordSpacingProperty()
            : base(PropertyNames.WordSpacing, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.OptionalLengthConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.OptionalLengthConverter.Validate(value);
        }

        #endregion
    }
}
