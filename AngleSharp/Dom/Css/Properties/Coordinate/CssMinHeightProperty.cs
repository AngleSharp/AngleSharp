namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-height
    /// Gets the minimum height of the element.
    /// </summary>
    sealed class CssMinHeightProperty : CssProperty
    {
        #region ctor

        internal CssMinHeightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.MinHeight, rule, PropertyFlags.Animatable)
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

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.LengthOrPercentConverter.Validate(value);
        }

        #endregion
    }
}
