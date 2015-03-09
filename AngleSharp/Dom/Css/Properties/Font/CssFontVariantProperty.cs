namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant
    /// Gets the selected font variant transformation, if any.
    /// </summary>
    sealed class CssFontVariantProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<FontVariant> Converter = 
            Converters.Assign(Keywords.Normal, FontVariant.Normal).Or(Keywords.SmallCaps, FontVariant.SmallCaps);

        #endregion

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
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}
