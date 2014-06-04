namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// A processing instruction provides an opportunity for application-specific
    /// instructions to be embedded within XML and which can be ignored by XML
    /// processors which do not support processing their instructions (outside
    /// of their having a place in the DOM).
    /// </summary>
    [DOM("ProcessingInstruction")]
    public interface IProcessingInstruction : ICharacterData
    {
        /// <summary>
        /// Gets the target of the processing instruction.
        /// </summary>
        [DOM("target")]
        String Target { get; }
    }
}
