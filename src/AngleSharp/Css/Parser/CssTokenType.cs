namespace AngleSharp.Css.Parser
{
    /// <summary>
    /// An enumation of all possible tokens.
    /// </summary>
    enum CssTokenType : byte
    {
        /// <summary>
        /// A string token (usually in quotation marks).
        /// </summary>
        String,
        /// <summary>
        /// A hash token (starts with #).
        /// </summary>
        Hash,
        /// <summary>
        /// A class token (starts with .).
        /// </summary>
        Class,
        /// <summary>
        /// An identifier token.
        /// </summary>
        Ident,
        /// <summary>
        /// An function token.
        /// </summary>
        Function,
        /// <summary>
        /// An number token.
        /// </summary>
        Number,
        /// <summary>
        /// An dimension token.
        /// </summary>
        Dimension,
        /// <summary>
        /// The column ( || ) token.
        /// </summary>
        Column,
        /// <summary>
        /// The descendent ( >> ) token.
        /// </summary>
        Descendent,
        /// <summary>
        /// The deep ( >>> ) token.
        /// </summary>
        Deep,
        /// <summary>
        /// The delimiter token to delimiter character.
        /// </summary>
        Delim,
        /// <summary>
        /// The match token (~=, |=, $=, ^=, *=, or !=).
        /// </summary>
        Match,
        /// <summary>
        /// The RoundBracketClose ( ) ) token.
        /// </summary>
        RoundBracketClose,
        /// <summary>
        /// The SquareBracketOpen ( [ ) token.
        /// </summary>
        SquareBracketOpen,
        /// <summary>
        /// The SquareBracketClose ( ] ) token.
        /// </summary>
        SquareBracketClose,
        /// <summary>
        /// The special character colon ( : ).
        /// </summary>
        Colon,
        /// <summary>
        /// The special character comma ( , ).
        /// </summary>
        Comma,
        /// <summary>
        /// The special character whitespace ( ).
        /// </summary>
        Whitespace,
        /// <summary>
        /// The invalid token (any).
        /// </summary>
        Invalid,
        /// <summary>
        /// The end-of-file marker.
        /// </summary>
        EndOfFile,
    }
}
