namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-size
    /// Gets the selected font-size.
    /// </summary>
    sealed class CssFontSizeProperty : CssProperty
    {
        #region ctor

        internal CssFontSizeProperty(CssStyleDeclaration rule)
            : base(PropertyNames.FontSize, rule, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return FontSize.Medium.ToLength();
        }

        protected override Object Compute(IElement element)
        {
            return Converters.FontSizeConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.FontSizeConverter.Validate(value);
        }

        #endregion
    }
}
