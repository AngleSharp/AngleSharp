namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Represents a processing instruction node.
    /// </summary>
    public sealed class ProcessingInstruction : CharacterData, IProcessingInstruction
    {
        #region ctor

        /// <summary>
        /// Creates a new processing instruction node.
        /// </summary>
        internal ProcessingInstruction(String name)
        {
            _type = NodeType.ProcessingInstruction;
            _name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the target.
        /// </summary>
        public String Target
        {
            get { return _name; }
        }

        #endregion
    }
}
