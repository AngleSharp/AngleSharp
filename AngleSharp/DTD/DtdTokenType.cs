namespace AngleSharp.DTD
{
    /// <summary>
    /// An enumation of all possible tokens.
    /// </summary>
    enum DtdTokenType
    {
        /// <summary>
        /// The prolog text declaration token (for external subsets).
        /// </summary>
        TextDecl,
        /// <summary>
        /// The element token for element definitions.
        /// </summary>
        Element,
        /// <summary>
        /// The attribute token for attribute definitions.
        /// </summary>
        Attribute,
        /// <summary>
        /// The notation token for notation definitions.
        /// </summary>
        Notation,
        /// <summary>
        /// The entity token for entity definitions.
        /// </summary>
        Entity,
        /// <summary>
        /// The comment token to mark a comment.
        /// </summary>
        Comment,
        /// <summary>
        /// The processing instruction token to mark a PI.
        /// </summary>
        ProcessingInstruction,
        /// <summary>
        /// The End-Of-File token to mark the end.
        /// </summary>
        EOF
    }
}
