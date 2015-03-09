namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-width
    /// </summary>
    sealed class CssBorderLeftWidthProperty : CssProperty
    {
        #region ctor

        internal CssBorderLeftWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderLeftWidth, rule, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Medium;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.LineWidthConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.LineWidthConverter.Validate(value);
        }

        #endregion
    }
}
