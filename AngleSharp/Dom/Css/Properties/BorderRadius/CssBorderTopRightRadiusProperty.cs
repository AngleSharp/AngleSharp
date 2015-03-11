namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-right-radius
    /// </summary>
    sealed class CssBorderTopRightRadiusProperty : CssProperty
    {
        #region ctor

        internal CssBorderTopRightRadiusProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderTopRightRadius, rule, PropertyFlags.Animatable)
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
