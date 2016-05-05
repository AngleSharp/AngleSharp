namespace AngleSharp.DTD
{
    /// <summary>
    /// The quantifier in the element definition.
    /// </summary>
    public enum ElementQuantifier
    {
        /// <summary>
        /// Nothing specified.
        /// </summary>
        One,
        /// <summary>
        /// Questionmark specified.
        /// </summary>
        ZeroOrOne,
        /// <summary>
        /// Asterisk specified.
        /// </summary>
        ZeroOrMore,
        /// <summary>
        /// Plus specified.
        /// </summary>
        OneOrMore
    }
}
