namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/max-height
    /// Gets the specified max-height of the element. A percentage is
    /// calculated with respect to the height of the containing block. If
    /// the height of the containing block is not specified explicitly, the
    /// percentage value is treated as none.
    /// </summary>
    sealed class CssMaxHeightProperty : CssProperty
    {
        #region ctor

        internal CssMaxHeightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.MaxHeight, rule, PropertyFlags.Animatable)
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
            return Converters.OptionalLengthOrPercentConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.OptionalLengthOrPercentConverter.Validate(value);
        }

        #endregion
    }
}
