namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-stretch
    /// Gets the selected font stretch setting.
    /// </summary>
    sealed class CssFontStretchProperty : CssProperty
    {
        #region ctor

        internal CssFontStretchProperty(CssStyleDeclaration rule)
            : base(PropertyNames.FontStretch, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return FontStretch.Normal;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.FontStretchConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.FontStretchConverter.Validate(value);
        }

        #endregion
    }
}
