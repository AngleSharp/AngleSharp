namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/bottom
    /// </summary>
    sealed class CssBottomProperty : CssProperty
    {
        #region ctor

        internal CssBottomProperty()
            : base(PropertyNames.Bottom, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.AutoLengthOrPercentConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.AutoLengthOrPercentConverter.Validate(value);
        }

        #endregion
    }
}
