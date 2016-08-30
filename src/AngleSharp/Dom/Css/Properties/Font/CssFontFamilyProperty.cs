namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-family
    /// Gets an enumeration over all font names.
    /// </summary>
    sealed class CssFontFamilyProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.FontFamiliesConverter.OrDefault("Times New Roman");

        #endregion

        #region ctor

        internal CssFontFamilyProperty()
            : base(PropertyNames.FontFamily, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Modes

        enum SystemFonts : byte
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
