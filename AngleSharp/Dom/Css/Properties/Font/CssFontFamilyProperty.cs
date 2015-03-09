namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-family
    /// Gets an enumeration over all font names.
    /// </summary>
    sealed class CssFontFamilyProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<String[]> Converter = 
            Map.DefaultFontFamilies.ToConverter().Or(
                Converters.StringConverter).Or(
                Converters.IdentifierConverter.Many().To(names => String.Join(" ", names))).FromList();

        #endregion

        #region ctor

        internal CssFontFamilyProperty(CssStyleDeclaration rule)
            : base(PropertyNames.FontFamily, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return "Times New Roman";
        }

        protected override Object Compute(IElement element)
        {
            var values = Converter.Convert(Value);
            return values[0];
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion

        #region Modes

        enum SystemFonts
        {
            /// <summary>
            /// Glyphs have finishing strokes, flared or tapering ends, or have actual serifed endings.
            /// E.g.  Palatino, "Palatino Linotype", Palladio, "URW Palladio", serif
            /// </summary>
            Serif,
            /// <summary>
            /// Glyphs have stroke endings that are plain.
            /// E.g. 'Trebuchet MS', 'Liberation Sans', 'Nimbus Sans L', sans-serif
            /// </summary>
            SansSerif,
            /// <summary>
            /// All glyphs have the same fixed width.
            /// E.g. "DejaVu Sans Mono", Menlo, Consolas, "Liberation Mono", Monaco, "Lucida Console", monospace
            /// </summary>
            Monospace,
            /// <summary>
            /// Glyphs in cursive fonts generally have either joining strokes or other cursive characteristics
            /// beyond those of italic typefaces. The glyphs are partially or completely connected, and the
            /// result looks more like handwritten pen or brush writing than printed letterwork.
            /// </summary>
            Cursive,
            /// <summary>
            /// Fantasy fonts are primarily decorative fonts that contain playful representations of characters.
            /// </summary>
            Fantasy
        }

        #endregion
    }
}
