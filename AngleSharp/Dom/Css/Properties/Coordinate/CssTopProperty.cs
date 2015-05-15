namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/top
    /// </summary>
    sealed class CssTopProperty : CssProperty
    {
        #region ctor

        internal CssTopProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Top, rule, PropertyFlags.Unitless | PropertyFlags.Animatable)
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
            return Converters.AutoLengthOrPercentConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.AutoLengthOrPercentConverter.Validate(value);
        }

        #endregion
    }
}
