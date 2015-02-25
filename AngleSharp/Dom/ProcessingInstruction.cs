namespace AngleSharp.Dom
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

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">
        /// Optional value: true if the children of the node should also be
        /// cloned, or false to clone only the specified node.
        /// </param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = new ProcessingInstruction(Owner, Target);
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml(IMarkupFormatter formatter)
        {
            return formatter.Processing(this);
        }

        #endregion
    }
}
