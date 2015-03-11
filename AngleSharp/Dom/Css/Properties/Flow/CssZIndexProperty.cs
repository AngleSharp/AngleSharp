namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/z-index
    /// Gets the index in the stacking order, if any.
    /// </summary>
    sealed class CssZIndexProperty : CssProperty
    {
        #region ctor

        internal CssZIndexProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ZIndex, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.OptionalIntegerConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.OptionalIntegerConverter.Validate(value);
        }

        #endregion
    }
}
