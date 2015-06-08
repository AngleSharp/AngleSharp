namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-width
    /// </summary>
    sealed class CssBorderLeftWidthProperty : CssProperty
    {
        #region ctor

        internal CssBorderLeftWidthProperty()
            : base(PropertyNames.BorderLeftWidth, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.LineWidthConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Medium;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.LineWidthConverter.Validate(value);
        }

        #endregion
    }
}
