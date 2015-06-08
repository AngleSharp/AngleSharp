namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-size
    /// Gets the selected font-size.
    /// </summary>
    sealed class CssFontSizeProperty : CssProperty
    {
        #region ctor

        internal CssFontSizeProperty()
            : base(PropertyNames.FontSize, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.FontSizeConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return FontSize.Medium.ToLength();
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.FontSizeConverter.Validate(value);
        }

        #endregion
    }
}
