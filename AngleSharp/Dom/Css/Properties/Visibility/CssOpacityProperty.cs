namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/opacity
    /// Gets the value that should be used for the opacity.
    /// </summary>
    sealed class CssOpacityProperty : CssProperty
    {
        #region ctor

        internal CssOpacityProperty()
            : base(PropertyNames.Opacity, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.NumberConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return 1f;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.NumberConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.NumberConverter.Validate(value);
        }

        #endregion
    }
}
