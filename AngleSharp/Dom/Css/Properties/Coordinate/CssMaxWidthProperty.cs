namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/max-width
    /// Gets the specified max-width of the element. A percentage is
    /// calculated with respect to the width of the containing block.
    /// </summary>
    sealed class CssMaxWidthProperty : CssProperty
    {
        #region ctor

        internal CssMaxWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.MaxWidth, rule, PropertyFlags.Animatable)
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
