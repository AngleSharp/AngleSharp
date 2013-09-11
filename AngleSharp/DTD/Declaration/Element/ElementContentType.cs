namespace AngleSharp.DTD
{
    /// <summary>
    /// The content type of the element definition.
    /// </summary>
    enum ElementContentType
    {
        /// <summary>
        /// EMPTY
        /// </summary>
        Empty,
        /// <summary>
        /// ANY
        /// </summary>
        Any,
        /// <summary>
        /// (#PCDATA|name|name|...)*
        /// </summary>
        Mixed,
        /// <summary>
        /// ((a,b,(c?,d*),(e|f+))?,g)+
        /// </summary>
        Children
    }
}