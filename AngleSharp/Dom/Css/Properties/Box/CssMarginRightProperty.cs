namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-right
    /// Gets the margin relative to the width of the containing block or a
    /// fixed width, if any.
    /// Gets if the margin is automatically determined.
    /// </summary>
    sealed class CssMarginRightProperty : CssProperty
    {
        #region ctor

        internal CssMarginRightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.MarginRight, rule, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
            return Converters.AutoLengthOrPercentConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.AutoLengthOrPercentConverter.Validate(value);
        }

        #endregion
    }
}
