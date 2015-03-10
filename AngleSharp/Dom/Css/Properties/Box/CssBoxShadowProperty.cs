namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow
    /// Gets an enumeration over all the set shadows.
    /// </summary>
    sealed class CssBoxShadowProperty : CssProperty
    {
        #region ctor

        internal CssBoxShadowProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BoxShadow, rule, PropertyFlags.Animatable)
        {
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

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.MultipleShadowConverter.Validate(value);
        }

        #endregion
    }
}
