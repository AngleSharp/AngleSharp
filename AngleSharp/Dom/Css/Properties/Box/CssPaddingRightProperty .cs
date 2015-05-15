namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-right
    /// Gets the padding relative to the width of the containing block or a
    /// fixed width.
    /// </summary>
    sealed class CssPaddingRightProperty : CssProperty
    {
        #region ctor

        internal CssPaddingRightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.PaddingRight, rule, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
            return Converters.LengthOrPercentConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.LengthOrPercentConverter.Validate(value);
        }

        #endregion
    }
}
