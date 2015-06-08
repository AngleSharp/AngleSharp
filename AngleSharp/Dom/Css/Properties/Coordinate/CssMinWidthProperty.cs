namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-width
    /// Gets the minimum height of the element.
    /// </summary>
    sealed class CssMinWidthProperty : CssProperty
    {
        #region ctor

        internal CssMinWidthProperty()
            : base(PropertyNames.MinWidth, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.LengthOrPercentConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Zero;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.LengthOrPercentConverter.Validate(value);
        }

        #endregion
    }
}
