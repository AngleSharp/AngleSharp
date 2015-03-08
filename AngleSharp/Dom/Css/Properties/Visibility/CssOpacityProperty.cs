namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/opacity
    /// Gets the value that should be used for the opacity.
    /// </summary>
    sealed class CssOpacityProperty : CssProperty
    {
        #region ctor

        internal CssOpacityProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Opacity, rule, PropertyFlags.Animatable)
        {
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

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.NumberConverter.Validate(value);
        }

        #endregion
    }
}
