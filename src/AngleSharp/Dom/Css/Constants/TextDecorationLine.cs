namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// An enumeration that defines the various line styles.
    /// </summary>
    public enum TextDecorationLine : ushort
    {
        /// <summary>
        /// Each line of text is underlined.
        /// </summary>
        Underline,
        /// <summary>
        /// Each line of text has a line above it.
        /// </summary>
        Overline,
        /// <summary>
        /// Each line of text has a line through the middle.
        /// </summary>
        LineThrough,
        /// <summary>
        /// The text blinks (alternates between visible and invisible). Conforming user agents may simply not blink the text.
        /// </summary>
        Blink
    }
}
