namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-shadow
    /// Gets an enumeration over all the set shadows.
    /// </summary>
    sealed class CssTextShadowProperty : CssProperty
    {
        #region ctor

        internal CssTextShadowProperty()
            : base(PropertyNames.TextShadow, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.MultipleShadowConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return new Shadow[0];
        }

        protected override Object Compute(IElement element)
        {
            return Converters.MultipleShadowConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.MultipleShadowConverter.Validate(value);
        }

        #endregion
    }
}
