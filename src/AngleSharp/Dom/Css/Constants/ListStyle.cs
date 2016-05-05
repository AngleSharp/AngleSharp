namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// An enumeration over possible list styles.
    /// </summary>
    public enum ListStyle : ushort
    {
        /// <summary>
        /// No list style at all.
        /// </summary>
        None,
        /// <summary>
        /// A filled circle (default value)
        /// </summary>
        Disc,
        /// <summary>
        /// A hollow circle
        /// </summary>
        Circle,
        /// <summary>
        /// A filled square
        /// </summary>
        Square,
        /// <summary>
        /// Han decimal numbers, Beginning with 1.
        /// </summary>
        Decimal,
        /// <summary>
        /// Decimal numbers, Padded by initial zeros, E.g. 01, 02, 03, ... 98, 99
        /// </summary>
        DecimalLeadingZero,
        /// <summary>
        /// Lowercase roman numerals, E.g.i, ii, iii, iv, v, ...
        /// </summary>
        LowerRoman,
        /// <summary>
        /// Uppercase roman numerals, E.g.I, II, III, IV, V ...
        /// </summary>
        UpperRoman,
        /// <summary>
        /// Lowercase classical Greek, alpha, beta, gamma…, E.g.α, β, γ ...
        /// </summary>
        LowerGreek,
        /// <summary>
        /// Lowercase ASCII letters, E.g.a, b, c, ... z
        /// </summary>
        LowerLatin,
        /// <summary>
        /// Uppercase ASCII letters, E.g.A, B, C, ... Z
        /// </summary>
        UpperLatin,
        /// <summary>
        /// Traditional Armenian numbering, (ayb/ayp, ben/pen, gim/keem ... )
        /// </summary>
        Armenian,
        /// <summary>
        /// Traditional Georgian numbering, E.g.an, ban, gan, ... he, tan, in…
        /// </summary>
        Georgian
    }
}
