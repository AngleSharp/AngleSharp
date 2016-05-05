namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// An enumeration with all possible text transformations.
    /// </summary>
    public enum TextTransform : ushort
    {
        /// <summary>
        /// Is a keyword preventing the case of all characters to be changed.
        /// </summary>
        None,
        /// <summary>
        /// Is a keyword forcing the first letter of each word to be converted
        /// to uppercase. Other characters are unchanged; that is, they retain
        /// their original case as written in the element's text.
        /// </summary>
        Capitalize,
        /// <summary>
        /// Is a keyword forcing all characters to be converted to uppercase.
        /// </summary>
        Uppercase,
        /// <summary>
        /// Is a keyword forcing all characters to be converted to lowercase.
        /// </summary>
        Lowercase,
        /// <summary>
        /// Is a keyword forcing the writing of a character, mainly ideograms and
        /// latin scripts inside a square, allowing them to be aligned in the
        /// usual East Asian scripts (like Chinese or Japanese).
        /// </summary>
        FullWidth
    }
}
