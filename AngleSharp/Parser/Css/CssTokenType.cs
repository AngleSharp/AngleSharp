namespace AngleSharp.Parser.Css
{
    /// <summary>
    /// An enumation of all possible tokens.
    /// </summary>
    enum CssTokenType
    {
        /// <summary>
        /// A string token (usually in quotation marks).
        /// </summary>
        String,
        /// <summary>
        /// A URL token.
        /// </summary>
        Url,
        /// <summary>
        /// A URL-PREFIX token.
        /// </summary>
        UrlPrefix,
        /// <summary>
        /// A DOMAIN token.
        /// </summary>
        Domain,
        /// <summary>
        /// A hash token (starts with #).
        /// </summary>
        Hash,
        /// <summary>
        /// An @-keyword token (starts with @).
        /// </summary>
        AtKeyword,
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
        /// An percentage token.
        /// </summary>
        Percentage,
        /// <summary>
        /// An dimension token.
        /// </summary>
        Dimension,
        /// <summary>
        /// An unicode range token.
        /// </summary>
        Range,
        /// <summary>
        /// The comment open token to start comments.
        /// </summary>
        Cdo,
        /// <summary>
        /// The comment close to end comments.
        /// </summary>
        Cdc,
        /// <summary>
        /// The colum token.
        /// </summary>
        Column,
        /// <summary>
        /// The delimiter token to delimiter character.
        /// </summary>
        Delim,
        /// <summary>
        /// The include match ~= token (with attributes, i.e. spaces).
        /// </summary>
        IncludeMatch,
        /// <summary>
        /// The dash match |= token (with attributes, i.e. hyphen).
        /// </summary>
        DashMatch,
        /// <summary>
        /// The prefix match ^= token (with attributes, i.e. beginning).
        /// </summary>
        PrefixMatch,
        /// <summary>
        /// The suffix match $= token (with attributes, i.e. ending).
        /// </summary>
        SuffixMatch,
        /// <summary>
        /// The substring match *= token (with attributes, i.e. somewhere).
        /// </summary>
        SubstringMatch,
        /// <summary>
        /// The not match != token (with attributes, i.e. somewhere).
        /// </summary>
        NotMatch,
        /// <summary>
        /// The RoundBracketOpen ( ( ) token.
        /// </summary>
        RoundBracketOpen,
        /// <summary>
        /// The RoundBracketClose ( ) ) token.
        /// </summary>
        RoundBracketClose,
        /// <summary>
        /// The CurlyBracketOpen ( { ) token.
        /// </summary>
        CurlyBracketOpen,
        /// <summary>
        /// The CurlyBracketClose ( } ) token.
        /// </summary>
        CurlyBracketClose,
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
        /// The special character semi-colon ( ; ).
        /// </summary>
        Semicolon,
        /// <summary>
        /// The special character whitespace ( ).
        /// </summary>
        Whitespace,
        /// <summary>
        /// The end-of-file marker.
        /// </summary>
        Eof
    }
}
