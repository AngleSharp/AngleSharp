namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-line
    /// Gets the enumeration over all selected styles for text decoration
    /// lines.
    /// </summary>
    sealed class CssTextDecorationLineProperty : CssProperty
    {
        #region Fields

        static readonly TextDecorationLine[] Default = new TextDecorationLine[0];
        static readonly IValueConverter<TextDecorationLine[]> SingleConverter = 
            Map.TextDecorationLines.ToConverter().Many();
        internal static readonly IValueConverter<TextDecorationLine[]> Converter = 
            SingleConverter.Or(Keywords.None, Default);

        #endregion

        #region ctor

        internal CssTextDecorationLineProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TextDecorationLine, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Default;
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
