namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-right-radius
    /// </summary>
    sealed class CssBorderBottomRightRadiusProperty : CssProperty
    {
        #region ctor

        internal CssBorderBottomRightRadiusProperty()
            : base(PropertyNames.BorderBottomRightRadius, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.BorderRadiusConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Zero;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.BorderRadiusConverter.Validate(value);
        }

        #endregion
    }
}
