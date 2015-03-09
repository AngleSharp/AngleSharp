namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/word-spacing
    /// Gets if normal inter-word space, as defined by the current font
    /// and/or the browser, is active.
    /// Gets the defined custom spacing, if any.
    /// </summary>
    sealed class CssWordSpacingProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Length?> Converter = 
            Converters.LengthConverter.ToNullable().Or(Keywords.Normal, null);

        #endregion

        #region ctor

        internal CssWordSpacingProperty(CssStyleDeclaration rule)
            : base(PropertyNames.WordSpacing, rule, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
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
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}
