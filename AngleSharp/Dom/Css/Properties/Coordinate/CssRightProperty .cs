namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/right
    /// </summary>
    sealed class CssRightProperty : CssProperty
    {
        #region ctor

        internal CssRightProperty()
            : base(PropertyNames.Right, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
