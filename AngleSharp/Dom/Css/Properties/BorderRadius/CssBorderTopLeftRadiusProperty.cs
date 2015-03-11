namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-left-radius
    /// </summary>
    sealed class CssBorderTopLeftRadiusProperty : CssProperty
    {
        #region ctor

        internal CssBorderTopLeftRadiusProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderTopLeftRadius, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Zero;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.BorderRadiusConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.BorderRadiusConverter.Validate(value);
        }

        #endregion
    }
}
