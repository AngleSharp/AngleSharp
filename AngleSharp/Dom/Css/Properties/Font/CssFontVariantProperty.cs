namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant
    /// Gets the selected font variant transformation, if any.
    /// </summary>
    sealed class CssFontVariantProperty : CssProperty
    {
        #region ctor

        internal CssFontVariantProperty(CssStyleDeclaration rule)
            : base(PropertyNames.FontVariant, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return FontVariant.Normal;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.FontVariantConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.FontVariantConverter.Validate(value);
        }

        #endregion
    }
}
