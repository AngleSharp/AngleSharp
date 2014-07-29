namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Represents a processing instruction node.
    /// </summary>
    sealed class ProcessingInstruction : CharacterData, IProcessingInstruction
    {
        #region ctor

        /// <summary>
        /// Creates a new processing instruction node.
        /// </summary>
        internal ProcessingInstruction(String name)
            : base(name, NodeType.ProcessingInstruction)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the target.
        /// </summary>
        public String Target
        {
            get { return NodeName; }
        }

        #endregion
    }
}
