namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-bottom
    /// Gets the padding relative to the height of the containing block or a
    /// fixed height.
    /// </summary>
    sealed class CssPaddingBottomProperty : CssProperty
    {
        #region ctor

        internal CssPaddingBottomProperty()
            : base(PropertyNames.PaddingBottom, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
