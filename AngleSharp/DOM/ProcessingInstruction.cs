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
        internal ProcessingInstruction(Document owner, String name)
            : base(owner, name, NodeType.ProcessingInstruction)
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

        #region Methods

        public override INode Clone(Boolean deep = true)
        {
            var node = new ProcessingInstruction(Owner, Target);
            CopyProperties(this, node, deep);
            return node;
        }

        #endregion

        #region String Representation

        public override String ToHtml()
        {
            return String.Format("<?{0} {1}>", Target, Data);
        }

        #endregion
    }
}
